using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    // This script is completely optional - it handles the spawning of plants (trees and bushes) on the tilemap if the graphic designer would like the map to be randomly generated
    [SerializeField] private GameObject tree, bush, berryBush;

    [SerializeField] public Transform blockTransform;


    public bool spawnPlantsLocal;
    public int berryBushCount;


    void Start()
    {
        berryBushCount = 0;
        tree.SetActive(false);
        bush.SetActive(false);
        berryBush.SetActive(false);
        
        if (spawnPlantsLocal)
        {
            StartCoroutine(TrySpawn());
            spawnPlantsLocal = false;
        }
    }

    IEnumerator TrySpawn()
    {
        yield return new WaitForSeconds(0.2f);
        if (bush != null && tree != null)
        {
            int rnd = Random.Range(0, 10);
            if (rnd < 2)
            {
                SpawnTree();
            }
            else if (rnd < 7)
            {
                SpawnBush();
            }
        }
        else
        {
            Debug.LogError("Either bush or tree objects are null in the inspector. Check objects with PlantScript.cs attached!");
        }
    }

    void SpawnTree()
    {
        float numberOfTrees = Random.Range(0, 1 * blockTransform.localScale.x);
        float rndX = CalculateOffset();
        float rndZ = CalculateOffset();
        for (int i = 0; i < numberOfTrees; i++)
        {
            GameObject treePrefab = InstantiateTree(rndX, rndZ);
            treePrefab.SetActive(true);
            rndX = CalculateOffset();
            rndZ = CalculateOffset();
        }
    }

    void SpawnBush()
    {
        float numberOfBush = Random.Range(0, (1 * blockTransform.localScale.x));
        float rndX = CalculateOffset();
        float rndZ = CalculateOffset();
        for (int i = 0; i < numberOfBush; i++)
        {
            if (berryBushCount < 5)
            {
                berryBushCount++;
                GameObject berryBushPrefab = InstantiateBerryBush(rndX, rndZ);
                berryBushPrefab.SetActive(true);
                rndX = CalculateOffset();
                rndZ = CalculateOffset();
            }
            else
            {
                GameObject bushPrefab = InstantiateBush(rndX, rndZ);
                bushPrefab.SetActive(true);
                rndX = CalculateOffset();
                rndZ = CalculateOffset();
            }
        }
    }

    private GameObject InstantiateTree(float rndX, float rndZ)
    {
        return Instantiate(tree, new Vector3((blockTransform.position.x + rndX), 4, (blockTransform.position.z + rndZ)), Quaternion.identity);
    }

    private GameObject InstantiateBush(float rndX, float rndZ)
    {
        return Instantiate(bush, new Vector3((blockTransform.position.x + rndX), 0.8f, (blockTransform.position.z + rndZ)), Quaternion.identity);
    }
    
    private GameObject InstantiateBerryBush(float rndX, float rndZ)
    {
        return Instantiate(berryBush, new Vector3((blockTransform.position.x + rndX), 0.8f, (blockTransform.position.z + rndZ)), Quaternion.identity);
    }



    private float CalculateOffset()
    {
        return Random.Range(-blockTransform.localScale.x / 2, blockTransform.localScale.x / 2);
    }
}


