using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class GirlController : MonoBehaviour
{
    [SerializeField, BoxGroup("Target Points")] private Transform m_TargetPoint1;
    [SerializeField] private Transform m_BoneTarget;
    [SerializeField] private Transform m_GirlMesh;
    [SerializeField] private Rigidbody m_Spine;
    private Animator _animator;
    
    private List<Rigidbody> rigidbodies;
    
    private static readonly int SeatPosition = Animator.StringToHash("SeatPosition");
    private static readonly int InMouth = Animator.StringToHash("InMouth");
    public static readonly int Toss = Animator.StringToHash("Toss");

    public void ActiveRagdoll(bool state)
    {
        _animator.enabled = !state;
            
        foreach (Rigidbody r in rigidbodies)
        {
            r.isKinematic = !state;
        }
    }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        rigidbodies = m_GirlMesh.GetComponentsInChildren<Rigidbody>().Where(r => r.useGravity).ToList();
        GirlSequence();
    }

    private void GirlSequence()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(m_TargetPoint1.position, 3f).SetEase(Ease.Linear));
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
