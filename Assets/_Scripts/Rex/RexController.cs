using DG.Tweening;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

 public class RexController : MonoBehaviour
    {
        [SerializeField, BoxGroup("Target Points")] private Transform m_FirstTargetPoint;
        [SerializeField] private Transform m_MouthTrasform;
        private Animator _animator;
        private NavMeshAgent _agent;
        private static readonly int Roar = Animator.StringToHash("Roar");
        private static readonly int Eat1 = Animator.StringToHash("Eat_1");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Eat2 = Animator.StringToHash("Eat_2");

        private GirlController _girlController;
        private static readonly int Hit = Animator.StringToHash("GetHit");

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _girlController = FindObjectOfType<GirlController>();
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
                _animator.SetTrigger(Roar);
            });
            sequence.AppendInterval(2.5f);
            sequence.AppendCallback(delegate
            {
                _animator.SetTrigger(Eat1);
            });
            sequence.AppendInterval(.2f);
            sequence.AppendCallback(delegate
            {
                _girlController.GetEatByDino(m_MouthTrasform);
            });
            sequence.AppendInterval(.3f);
            sequence.AppendCallback(delegate
            {
                _animator.SetTrigger(Idle);
            });
            sequence.AppendInterval(.3f);
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

        public void Eat()
        {
            _animator.SetTrigger(Eat2);
            _girlController.Tossed();
        }

        public void GetHit()
        {
            _animator.SetTrigger(Hit);
        }
        
        public void RoarF()
        {
            _animator.SetTrigger(Roar);
        }
    }