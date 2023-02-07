using DG.Tweening;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

 public class RexController : MonoBehaviour
 {
     public static RexController Instance { get; private set; }
        
        #region SF

        [SerializeField, BoxGroup("Target Points")] private Transform m_FirstTargetPoint;
        [SerializeField] private Transform m_MouthTrasform;

        #endregion

        #region AnimParams

        private static readonly int Roar_AP = Animator.StringToHash("Roar");
        private static readonly int Eat1 = Animator.StringToHash("Eat_1");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Eat2 = Animator.StringToHash("Eat_2");
        private static readonly int Hit = Animator.StringToHash("GetHit");
        

        #endregion

        #region Inline Fields

        private GirlController _girlController;
        private Animator _animator;
        private NavMeshAgent _agent;

        #endregion

        private void Awake()
        {
            Instance = this;
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
            
            Walk(sequence);
            Roar(sequence);
            TakeInMouth(sequence);
            HoldingGirlInMouth(sequence);
        }

        private void HoldingGirlInMouth(Sequence sequence)
        {
            sequence.AppendCallback(delegate { _animator.SetTrigger(Idle); });
            sequence.AppendInterval(.3f);
            sequence.AppendCallback(delegate
            {
                CameraController.Instance.SwitchToUIChooseCam();
            });
        }

        private void TakeInMouth(Sequence sequence)
        {
            sequence.AppendCallback(delegate { _animator.SetTrigger(Eat1); });
            sequence.AppendInterval(.2f);
            sequence.AppendCallback(delegate { _girlController.GetEatByDino(m_MouthTrasform); });
            sequence.AppendInterval(.3f);
        }

        private void Roar(Sequence sequence)
        {
            sequence.AppendCallback(delegate { _animator.SetTrigger(Roar_AP); });
            sequence.AppendInterval(2f);
        }

        private void Walk(Sequence sequence)
        {
            sequence.AppendCallback(delegate { _agent.SetDestination(m_FirstTargetPoint.position); });

            float waitDuration = (m_FirstTargetPoint.position - transform.position).magnitude / _agent.speed - 0.35f;
            sequence.AppendInterval(waitDuration);
        }

        [UsedImplicitly]
        private void CameraShake()
        {
            CameraController.Instance.DoShake(2.2f);
        }

        public void TakeInMouth()
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
            _animator.SetTrigger(Roar_AP);
        }
    }