using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class GirlController : MonoBehaviour
{
    [SerializeField, BoxGroup("Target Points")] private Transform m_TargetPoint1;
    [SerializeField] private Transform m_GirlMesh;
    [SerializeField] private Rigidbody m_Spine;
    private Animator _animator;
    
    private static readonly int SeatPosition = Animator.StringToHash("SeatPosition");
    private static readonly int InMouth = Animator.StringToHash("InMouth");
    public static readonly int Toss = Animator.StringToHash("Toss");
    public static readonly int Crawl = Animator.StringToHash("Crawl");

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
            _animator.SetTrigger(Crawl);
        });
        sequence.AppendInterval(1.2f);
        sequence.Append(transform.DOMove(m_TargetPoint1.position, 2.3f).SetEase(Ease.Linear));
    }

    public void GetEatByDino(Transform parent)
    {
        _animator.SetTrigger(SeatPosition);
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = quaternion.identity;
        DOVirtual.DelayedCall(.4f, delegate
        {
            _animator.SetTrigger(InMouth);
        });
    }

    public void Tossed()
    {
       _animator.SetTrigger(Toss);
    }
}
