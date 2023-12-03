using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public float detectionRange = 1f;
    public float shootingRange = 5f;
    public float approachSpeed = 3f;
    public float fireRate = 1f;
    public float movementSpeed = 1.5f;
    public float JumpForce;
    public Rigidbody2D Rb;
    public int life = 3;

    private float nextFireTime;
    private EnemyShoot enemyShoot;
    private bool isApproaching = false;

    void Start()
    {
        enemyShoot = GetComponent<EnemyShoot>();
        GameObject.Find("Canvas").transform.Find("Text (LifeIA) (1)").GetComponent<TextMeshProUGUI>().text = ((int)life).ToString();
    }

    void Update()
    {
        if (!CanSeePlayer())
        {
            StartApproach();
        }
        else
        {
            StopApproach();
            if (IsInShootingRange())
            {
                AimAndFire();
            }
        }
    }

    bool CanSeePlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        return distanceToPlayer <= detectionRange;
    }

    void AimAndFire()
    {
        if (Time.time >= nextFireTime)
        {
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = direction * projectileSpeed;

            nextFireTime = Time.time + 1f / fireRate;

            // Activer le script de tir de l'ennemi
            if (enemyShoot != null)
            {
                enemyShoot.enabled = true;
            }

            StartCoroutine(EnablePlayerShootAfterDelay(2f)); // Activer le script apr�s 2 secondes
        }
    }

    IEnumerator EnablePlayerShootAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.GetComponent<RockShoot>().enabled = true;
    }

    void StartApproach()
    {
        isApproaching = true;
        ApproachPlayer();
    }

    void StopApproach()
    {
        isApproaching = false;
    }

    void ApproachPlayer()
    {
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * movementSpeed * Time.deltaTime); // Utilisation de la nouvelle variable de vitesse
    }

    bool IsInShootingRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        return distanceToPlayer <= shootingRange;
    }

    public void Jump()
    {
        Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
    }

    public void TakeDamage()
    {
        life--;

        // Vous pouvez ajouter ici d'autres actions � effectuer lorsque le joueur prend des d�g�ts, par exemple, v�rifier s'il est mort.

        if (life <= 0)
        {
            // Le joueur est mort, vous pouvez ajouter ici le code correspondant � la d�faite du joueur.
            Victory();
        }
    }

    void Victory()
    {
        // Mettez ici le code pour la s�quence de Game Over, par exemple, d�sactiver le joueur, afficher un �cran de fin, etc.
        Debug.Log("Victory!");

        // D�sactivez le script Movement pour arr�ter les mouvements du joueur.

        // Affichez un �cran de fin ou un texte "Game Over".
        // Assurez-vous d'avoir un objet Text ou un Canvas avec le texte "Game Over" dans votre sc�ne.
        GameObject.Find("Canvas").transform.Find("Text (Victory)").gameObject.SetActive(true);

        // Arr�tez le temps pour que le jeu ne puisse plus �tre contr�l�.
        Time.timeScale = 0f;
        GameObject.Find("Canvas").transform.Find("Text (LifeIA) (1)").GetComponent<TextMeshProUGUI>().text = ((int)life).ToString();

        // Ajoutez ici d'autres actions � effectuer lors du Game Over.
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.parent != null && collision.transform.parent.gameObject.name == "Tilemap 4")
        {
            Jump();
        }
        if (collision.gameObject.CompareTag("Rock1")) // Assurez-vous que les rochers ont un tag "Rock" dans l'�diteur Unity.
        {
            GameObject.Find("Canvas").transform.Find("Text (LifeIA) (1)").GetComponent<TextMeshProUGUI>().text = ((int)life).ToString();
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }
}

