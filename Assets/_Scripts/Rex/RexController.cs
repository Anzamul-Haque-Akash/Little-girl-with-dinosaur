using _Scripts.Player;
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
        [SerializeField] private Transform m_MouthTrasform;
        private Animator _animator;
        private NavMeshAgent _agent;
        private static readonly int Roar = Animator.StringToHash("Roar");
        private static readonly int Eat1 = Animator.StringToHash("Eat_1");
        private static readonly int Eat2 = Animator.StringToHash("Eat_2");

        private GirlController _girlController;
        private PlayerController _playerController;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _girlController = FindObjectOfType<GirlController>();
            _playerController = FindObjectOfType<PlayerController>();
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

            float waitDuration = (m_FirstTargetPoint.position - transform.position).magnitude / _agent.speed - 0.35f;
            sequence.AppendInterval(waitDuration);
            sequence.AppendCallback(delegate
            {
                _animator.SetTrigger(Roar);
                CameraController.Instance.SwitchToEatingCam();
            });
            sequence.AppendInterval(3.2f);
            sequence.AppendCallback(delegate
            {
                _animator.SetTrigger(Eat1);
            });
            sequence.AppendInterval(.5f);
            sequence.AppendCallback(delegate
            {
                _girlController.GetEatByDino(m_MouthTrasform);
            });
            sequence.AppendInterval(.3f);
            sequence.AppendCallback(delegate
            {
                _animator.SetTrigger(Eat2);
            });
            sequence.AppendInterval(1f);
            sequence.AppendCallback(delegate
            {
                CameraController.Instance.SwitchToPlayerCam();
            });
        }

        [UsedImplicitly]
        private void CameraShake()
        {
            CameraController.Instance.DoShake(2.2f);
        }
    }
}