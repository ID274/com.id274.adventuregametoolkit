using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MapMesh : MonoBehaviour
{
    [Header("NavMesh")]
    public NavMeshSurface navSurface;
    public GameObject navSurfaceHolder;

    void Start()
    {
        //The code here can be used to build the navmesh surface at runtime, should be used with the random map generation tool

        //navSurface.BuildNavMesh();
    }
}
