using System.Collections;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    private bool isPlayerTurn = true;

    void Start()
    {
        StartCoroutine(TurnRoutine());
    }

    IEnumerator TurnRoutine()
    {
        while (true)
        {
            DisableAllControls();
            yield return new WaitForSeconds(5f);
            

            if (isPlayerTurn)
            {
                // C'est le tour du joueur
                GameObject.Find("Canvas").transform.Find("Text (Turn)").GetComponent<TextMeshProUGUI>().text = "Turn Player";
                EnablePlayerControl();
                EnablePlayerControl();
            }
            else
            {
                // C'est le tour de l'IA
                GameObject.Find("Canvas").transform.Find("Text (Turn)").GetComponent<TextMeshProUGUI>().text = "Turn IA";
                EnableEnemyControl();
            }

            yield return new WaitForSeconds(5f); // Durée du tour
            GameObject.Find("Canvas").transform.Find("Text (Turn)").GetComponent<TextMeshProUGUI>().text = "Turn Attend";
            DisableAllControls();
            isPlayerTurn = !isPlayerTurn; // Changer le tour
        }
    }

    void EnablePlayerControl()
    {
        // Activer les scripts de contrôle du joueur
        player.GetComponent<Movement>().enabled = true;
        player.GetComponent<RockShoot>().enabled = true;
    }

    void EnableEnemyControl()
    {
        // Activer les scripts de contrôle de l'IA
        enemy.GetComponent<EnemyController>().enabled = true;
        enemy.GetComponent<EnemyShoot>().enabled = true;
    }

    void DisableAllControls()
    {
        // Désactiver tous les scripts de contrôle
        player.GetComponent<Movement>().enabled = false;
        player.GetComponent<RockShoot>().enabled = false;
        enemy.GetComponent<EnemyController>().enabled = false;
        enemy.GetComponent<EnemyShoot>().enabled = false;
    }
}
