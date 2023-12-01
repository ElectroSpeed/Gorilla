using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RockShoot : MonoBehaviour
{
    public GameObject Rock;
    private float IsAiming = 0f;
    private float Powered = 0f;
    public float firingAngle = 45f;
    public float firingSpeed = 10f;
    public Vector2 wind;

    void Start()
    {
        Randomwind();
    }

    void Update()
    {
        firingAngle += IsAiming / 5;
        if (firingAngle > 180 + 45)
        {
            firingAngle = 180f + 45f;
        }

        if (firingAngle < -45)
        {
            firingAngle = -45f;
        }

        firingSpeed += Powered / 10;
        if (firingSpeed > 20)
        {
            firingSpeed = 20f;
        }

        if (firingSpeed < 5)
        {
            firingSpeed = 5f;
        }

        GameObject.Find("Canvas").transform.Find("Text (Power) (1)").GetComponent<TextMeshProUGUI>().text = ((int)firingSpeed).ToString();
        GameObject.Find("Canvas").transform.Find("Text (Angle) (1)").GetComponent<TextMeshProUGUI>().text = ((int)firingAngle).ToString();
        GameObject.Find("Canvas").transform.Find("Text (Vent) (1)").GetComponent<TextMeshProUGUI>().text = ((int)wind.x).ToString();
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            float gravity = Physics2D.gravity.magnitude;
            float angle = firingAngle * Mathf.Deg2Rad;

            float x = Mathf.Cos(angle) * firingSpeed;
            float y = Mathf.Sin(angle) * firingSpeed;

            y += 0.5f * gravity * Mathf.Pow((firingSpeed * Mathf.Sin(angle)) / gravity, 2);

            Vector3 projectileVelocity = new Vector3(x, y, 0);

            GameObject rock = Instantiate(Rock, new Vector3(transform.position.x + x / 10, transform.position.y + y / 5 - 0.6f, 0), Quaternion.identity);

            float timeOfFlight = 2 * y / gravity; // Calculating the time of flight

            // Applying wind effect to the curve of the projectile
            rock.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileVelocity.x, projectileVelocity.y) + wind * timeOfFlight;
        }
    }

    public void Aim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsAiming = context.ReadValue<float>();
        }

        if (context.canceled)
        {
            IsAiming = 0;
        }
    }

    public void Power(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Powered = context.ReadValue<float>();
        }

        if (context.canceled)
        {
            Powered = 0;
        }
    }

    public void Randomwind()
    {
        wind = Vector2.right * Random.Range(-5, 5);
    }
}
