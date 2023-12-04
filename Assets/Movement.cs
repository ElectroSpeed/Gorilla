using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float PlayerSpeed;
    public float JumpForce;

    float horizontalmovement;

    [HideInInspector]public int life = 3;

    public Rigidbody2D Rb;

    [SerializeField] private Vector3 velocity = Vector3.zero;

    public Transform EarthLeft;
    public Transform EarthRight;

    private bool isJumping = false;
    private bool isEarth = false;

    private void Start()
    {
        GameObject.Find("Canvas").transform.Find("Text (Lifeplayer) (1)").GetComponent<TextMeshProUGUI>().text = ((int)life).ToString();
    }

    void FixedUpdate()
    {
        isEarth = Physics2D.OverlapArea(EarthLeft.position, EarthRight.position);

        if (isEarth == true)
        {
            if (isJumping == true)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
            }
        }

        Vector3 targetVelocity = new Vector2(horizontalmovement, Rb.velocity.y);
        Rb.velocity = Vector3.SmoothDamp(Rb.velocity, targetVelocity, ref velocity, .05f);
    }

    public void TakeDamage()
    {
        life--;


        if (life <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");

        GetComponent<Movement>().enabled = false;

        GameObject.Find("Canvas").transform.Find("Text (GameOver)").gameObject.SetActive(true);

        Time.timeScale = 0f;
        GameObject.Find("Canvas").transform.Find("Text (Lifeplayer) (1)").GetComponent<TextMeshProUGUI>().text = ((int)life).ToString();

    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        horizontalmovement = context.ReadValue<Vector2>().x * PlayerSpeed;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isJumping = true;
        }
        if (context.canceled)
        {
            isJumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("rock"))
        {
            GameObject.Find("Canvas").transform.Find("Text (Lifeplayer) (1)").GetComponent<TextMeshProUGUI>().text = ((int)life).ToString();
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }
}
