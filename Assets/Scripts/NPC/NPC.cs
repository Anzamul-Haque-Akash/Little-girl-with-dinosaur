using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] private Transform m_TargetPoint;
    private NavMeshAgent _navMesh;
    private Animator _animator;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        GoToAPoint();
    }

    private void GoToAPoint()
    {
        _navMesh.SetDestination(m_TargetPoint.position);
    }
}
