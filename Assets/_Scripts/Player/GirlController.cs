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
    private Animator _animator;
    
    private List<Rigidbody> rigidbodies;
    
    private static readonly int SeatPosition = Animator.StringToHash("SeatPosition");

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

        // ActiveRagdoll(false);
        // _animator.enabled = false;
        // transform.parent = parent;
        //
        // DOVirtual.Float(0f, 1f, 0.5f, delegate(float value)
        // {
        //     transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, value);
        // });
        //
        // DOVirtual.DelayedCall(1f, delegate
        // {
        //     m_BoneTarget.parent = parent;
        //     m_BoneTarget.localPosition = Vector3.zero;
        //     ActiveRagdoll(true);
        // });
    }
}
