using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private NavMeshAgent player;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private Image[] menus;

    private Vector3 cameraOffset;

    void Start()
    {
        player = this.GetComponent<NavMeshAgent>();
        if (mapGenerator != null && mapGenerator.spawnPoint != null)
        {
            transform.position = new Vector3(mapGenerator.spawnPoint.position.x + 1, 1, mapGenerator.spawnPoint.position.z + 1); // Sets player's spawn position to the spawn point set in the map generator tool
        }
    }

    void Update()
    {
        cameraOffset = new Vector3(transform.position.x - 3, transform.position.y + 18.5f, transform.position.z); // Offsetting the camera to a certain position to make it look better
        if (mainCamera.transform.position != cameraOffset)
        {
            mainCamera.transform.position = cameraOffset; // Keep camera on player
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            // The code below is used to check if the player is trying to move to a spot with a navmesh on it, and if it does then it moves the player towards it using the SetDestination method
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if (Physics.Raycast(ray, out hitPoint))
            {
                if (!player.isOnNavMesh)
                {
                    player.enabled = false;
                    player.enabled = true;
                }
                player.SetDestination(hitPoint.point);

            }
        }
    }

}
