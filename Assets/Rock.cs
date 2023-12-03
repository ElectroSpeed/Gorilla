using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Rock : MonoBehaviour
{
    float TimeToDeath = 4f;
    void Start()
    {
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(TimeToDeath);
        Destroy(gameObject, TimeToDeath);
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.parent != null && collision.transform.parent.gameObject.name == "Tilemap 4")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player")) // Assurez-vous que le joueur a un tag "Player" dans l'éditeur Unity.
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("IA")) // Assurez-vous que le joueur a un tag "IA" dans l'éditeur Unity.
        {
            Destroy(gameObject);
        }
    }
}

