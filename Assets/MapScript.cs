using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    [SerializeField] int startX, startY;
    [SerializeField] int width, height;
    [SerializeField] int minHeight, maxHeight;
    [SerializeField] int maxVariation;
    [SerializeField] int repeatNum;
    [SerializeField] GameObject Dirt, Grass;

    void Start()
    {
        PreGeneration();
        Generation();
        PostGeneration();
    }

    void PreGeneration()
    {
        int x = startX - 16;
        height = maxHeight + 30;
        while (x < startX)
        {
            GenerateFlatPlatform(x);
            x++;
        }
    }

    void PostGeneration()
    {
        int x = width;
        height = maxHeight + 30;
        while (x < width + 16)
        {
            GenerateFlatPlatform(x);
            x++;
        }
    }

    void Generation()
    {
        int repeatValue = 0;
        height = Random.Range(minHeight, maxHeight);
        for (int x = startX; x < width; x++)
        {
            if (repeatValue == 0)
            {
                height = Random.Range(Mathf.Clamp(height - maxVariation, maxHeight, minHeight), Mathf.Clamp(height + maxVariation, maxHeight, minHeight));
                GenerateFlatPlatform(x);
                repeatValue = repeatNum;
            }
            else
            {
                GenerateFlatPlatform(x);
                repeatValue--;
            }
        }
    }

    void GenerateFlatPlatform(int x)
    {
        for (int y = startY; y < height; y++)
        {
            SpawnObj(Dirt, x, y);
        }
        SpawnObj(Grass, x, height);
    }

    void SpawnObj(GameObject obj, int x, int y)
    {
        // Ajuster la position Y ici pour déplacer la génération vers le bas
        float yOffset = -7f; // Ajustez cette valeur selon vos besoins
        Vector2 spawnPosition = new Vector2(x, y + yOffset);

        obj = Instantiate(obj, spawnPosition, Quaternion.identity);
        obj.transform.parent = this.transform;
    }
}