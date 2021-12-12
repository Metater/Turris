using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [SerializeField] public Transform point;
    

    // Start is called before the first frame update
    void Awake()
    {
         navMeshAgent = GetComponent<NavMeshAgent>();
        point = GetComponentInParent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = point.position;
    }
}
