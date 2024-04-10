using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;

public class MapGenerator : MonoBehaviour
{
    // This script is completely optional - it handles random map generation at runtime and allows the player to experience a different world every time
    [Header("Prefabs (each object in the array has the same chance of spawning)")]
    [SerializeField] private GameObject[] blockPrefabs;

    [SerializeField] private GameObject floorBoundaryBlock;

    [Header("Map Settings")]
    [SerializeField] private int mapHeight;
    [SerializeField] private int mapWidth;
    [SerializeField] private bool makeMapOnStart = false;
    [SerializeField] public bool spawnPlants = false;

    [Header("NavMesh")]
    public NavMeshSurface navSurface;
    public GameObject navSurfaceHolder;

    private bool spawnPointSet = false;
    public Transform spawnPoint;
    const int maxMapHeight = 100;
    const int maxMapWidth = 100;

    private int i; // the "i" variable for the SpawnZBlocks method, so that SpawnXBlocks method also has access to the value


    void Start()
    {
        if (mapHeight * blockPrefabs[0].transform.localScale.x > maxMapHeight)
        {
            mapHeight = (int)Mathf.Round(maxMapHeight / blockPrefabs[0].transform.localScale.x);
        }
        if (mapWidth * blockPrefabs[0].transform.localScale.x > maxMapWidth)
        {
            mapWidth = (int)Mathf.Round(maxMapWidth / blockPrefabs[0].transform.localScale.x);
        }



        if (makeMapOnStart)
        {
            SpawnZBlocks();
            navSurface.BuildNavMesh(); // Builds the navmesh at runtime

        }
        
        void SpawnZBlocks() // Spawns blocks on the Z axis, and calls the SpawnXBlocks method for every row
        {
            if (mapHeight * mapWidth != 0)
            {
                for (i = 0; i < mapHeight; i++)
                {
                    int rnd = Random.Range(0, blockPrefabs.Length);
                    GameObject zBlock = Instantiate(blockPrefabs[rnd], new Vector3(0, 0, i * blockPrefabs[rnd].transform.localScale.x), Quaternion.identity);
                    if (!spawnPointSet)
                    {
                        spawnPointSet = true;
                        spawnPoint = zBlock.transform;
                    }
                    zBlock.transform.parent = navSurfaceHolder.transform;
                    PlantScript plantzBlock = zBlock.GetComponent<PlantScript>();

                    if (plantzBlock != null)
                    {
                        plantzBlock.spawnPlantsLocal = spawnPlants;
                    }
                    GameObject zboundaryBlock = Instantiate(floorBoundaryBlock, new Vector3(0, -0.9f, i * blockPrefabs[rnd].transform.localScale.x), Quaternion.identity);
                    SpawnXBlocks();

                }
            }
        }
        void SpawnXBlocks() // Spawns blocks on the X axis row
        {
            if (mapHeight * mapWidth != 0)
            {
                for (int j = 1; j < mapWidth; j++)
                {
                    int rnd = Random.Range(0, blockPrefabs.Length);
                    GameObject xBlock = Instantiate(blockPrefabs[rnd], new Vector3(j * blockPrefabs[rnd].transform.localScale.x, 0, i * blockPrefabs[rnd].transform.localScale.x), Quaternion.identity); // Multiplied by the local scale of the object in order to preserve the grid no matter what scale is chosen within the inspector.
                    xBlock.transform.parent = navSurfaceHolder.transform;
                    PlantScript plantxBlock = xBlock.GetComponent<PlantScript>();
                    if (plantxBlock != null)
                    {
                        plantxBlock.spawnPlantsLocal = spawnPlants;
                    }
                    GameObject xboundaryBlock = Instantiate(floorBoundaryBlock, new Vector3(j * blockPrefabs[rnd].transform.localScale.x, -0.9f, i * blockPrefabs[rnd].transform.localScale.x), Quaternion.identity);
                }
            }
            
        }
    }
}
