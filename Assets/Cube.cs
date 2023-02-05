using System;
using UnityEngine;
using UnityEngine.AI;

public class Cube : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(target.position);
    }
}
