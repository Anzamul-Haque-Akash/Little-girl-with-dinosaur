using DG.Tweening;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Rex
{
    public class RexController : MonoBehaviour
    {
        [SerializeField, BoxGroup("Target Points")] private Transform m_FirstTargetPoint;
        
        private Animator _animator;
        private NavMeshAgent _agent;
        private static readonly int Roar = Animator.StringToHash("Roar");

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            TRexSequence();
        }

        private void TRexSequence()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.AppendCallback(delegate
            {
                _agent.SetDestination(m_FirstTargetPoint.position);
            });

            float waitDuration = (m_FirstTargetPoint.position - transform.position).magnitude / _agent.speed - 0.2f;
            sequence.AppendInterval(waitDuration);
            sequence.AppendCallback(delegate
            {
                _animator.SetTrigger(Roar);
                CameraController.Instance.SwitchToEatingCam();
            });
        }

        [UsedImplicitly]
        private void CameraShake()
        {
            CameraController.Instance.DoShake(2f);
        }
    }
}