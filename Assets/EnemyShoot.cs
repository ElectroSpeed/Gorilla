using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject player;
    public GameObject projectilePrefab;
    public float firingAngle = 45f;
    public float firingSpeed = 10f;
    public Vector2 wind;

    void Start()
    {
        RandomWind();
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            Shoot();
        }
    }

    void Shoot()
    {
        float gravity = Physics2D.gravity.magnitude;
        float angle = firingAngle * Mathf.Deg2Rad;

        float x = Mathf.Cos(angle) * firingSpeed;
        float y = Mathf.Sin(angle) * firingSpeed;

        Vector3 projectileVelocity = new Vector3(x, y, 0);

        GameObject rock = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        float timeOfFlight = 2 * y / gravity;

        rock.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileVelocity.x, projectileVelocity.y) + wind * timeOfFlight;
    }

    void RandomWind()
    {
        wind = Vector2.right * Random.Range(-5, 5);
    }
}

