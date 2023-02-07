using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class GirlController : MonoBehaviour
{
    [SerializeField, BoxGroup("Target Points")] private Transform m_TargetPoint1;
    [SerializeField, BoxGroup("Target Points")] private Transform m_TargetPoint2;
    [SerializeField] private NavMeshAgent m_Agent;

    [SerializeField] private Transform m_RagdollPoint;
    [SerializeField] private Transform m_RagDollRefPoint;

    public float InterpolationSpeed = 0.1f;

    private void Update()
    {
        m_RagdollPoint.parent = null;
        m_RagdollPoint.position = Vector3.Lerp(m_RagdollPoint.position, m_RagDollRefPoint.position, InterpolationSpeed);
        m_RagdollPoint.rotation = Quaternion.Lerp(m_RagdollPoint.rotation, m_RagDollRefPoint.rotation, InterpolationSpeed);
    }

    private Animator _animator;
    
    private static readonly int SeatPosition = Animator.StringToHash("SeatPosition");
    private static readonly int InMouth = Animator.StringToHash("InMouth");
    public static readonly int Tripping = Animator.StringToHash("Tripping");
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        GirlSequence();
    }

    private void GirlSequence()
    {
        Sequence sequence = DOTween.Sequence();
        
        sequence.AppendCallback(delegate
        {
            m_Agent.SetDestination(m_TargetPoint1.position);
        });
            
        float waitDuration = (m_TargetPoint1.position - transform.position).magnitude / m_Agent.speed - 0.35f;
        sequence.AppendInterval(waitDuration);
        
        sequence.AppendCallback(delegate
        {
            _animator.SetTrigger(Tripping);
        });
        
        sequence.AppendInterval(1.2f);
        sequence.AppendCallback(delegate
        {
            m_Agent.enabled = false;
        });
        sequence.Append(transform.DOMove(m_TargetPoint2.position, 1.3f).SetEase(Ease.Linear));
    }

    public void GetEatByDino(Transform parent)
    {
        _animator.SetTrigger(SeatPosition);
        
        DOVirtual.DelayedCall(.25f, delegate
        {
            _animator.SetTrigger(InMouth);
            _animator.enabled = false;
            transform.parent = parent;
            transform.localPosition = Vector3.zero;
            transform.localRotation = quaternion.identity;
        });
    }

    public void Tossed()
    {
       DOVirtual.DelayedCall(1.1f, delegate
       {
           gameObject.SetActive(false);
           FindObjectOfType<RexController>().RoarF();
       });
    }
}
