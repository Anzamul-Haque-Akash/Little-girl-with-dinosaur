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
    private Animator _animator;
    
    private static readonly int SeatPosition = Animator.StringToHash("SeatPosition");
    private static readonly int InMouth = Animator.StringToHash("InMouth");
    public static readonly int Toss = Animator.StringToHash("Toss");
    public static readonly int Crawl = Animator.StringToHash("Crawl");
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
        
        //Trip
        sequence.AppendCallback(delegate
        {
            _animator.SetTrigger(Tripping);
        });
        
        // sequence.AppendCallback(delegate
        // {
        //     _animator.SetTrigger(Crawl);
        // });
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
       _animator.SetTrigger(Toss);
       DOVirtual.DelayedCall(1.5f, delegate
       {
           gameObject.SetActive(false);
           FindObjectOfType<RexController>().RoarF();
       });
    }
}
