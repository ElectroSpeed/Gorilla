using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float PlayerSpeed;
    public float JumpForce;

    float horizontalmovement;

    public int life = 3;

    public Rigidbody2D Rb;

    private Vector3 velocity = Vector3.zero;

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

        // Vous pouvez ajouter ici d'autres actions � effectuer lorsque le joueur prend des d�g�ts, par exemple, v�rifier s'il est mort.

        if (life <= 0)
        {
            // Le joueur est mort, vous pouvez ajouter ici le code correspondant � la d�faite du joueur.
            GameOver();
        }
    }

    void GameOver()
    {
        // Mettez ici le code pour la s�quence de Game Over, par exemple, d�sactiver le joueur, afficher un �cran de fin, etc.
        Debug.Log("Game Over!");

        // D�sactivez le script Movement pour arr�ter les mouvements du joueur.
        GetComponent<Movement>().enabled = false;

        // Affichez un �cran de fin ou un texte "Game Over".
        // Assurez-vous d'avoir un objet Text ou un Canvas avec le texte "Game Over" dans votre sc�ne.
        GameObject.Find("Canvas").transform.Find("Text (GameOver)").gameObject.SetActive(true);

        // Arr�tez le temps pour que le jeu ne puisse plus �tre contr�l�.
        Time.timeScale = 0f;
        GameObject.Find("Canvas").transform.Find("Text (Lifeplayer) (1)").GetComponent<TextMeshProUGUI>().text = ((int)life).ToString();

        // Ajoutez ici d'autres actions � effectuer lors du Game Over.
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
        if (collision.gameObject.CompareTag("rock")) // Assurez-vous que les rochers ont un tag "Rock" dans l'�diteur Unity.
        {
            GameObject.Find("Canvas").transform.Find("Text (Lifeplayer) (1)").GetComponent<TextMeshProUGUI>().text = ((int)life).ToString();
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }
}
