using System;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    
    [SerializeField] private Transform m_Teddy;
    [SerializeField] private Transform m_PickupPoint;
    [SerializeField] private Transform m_ThrowPoint;

    private Animator _animator;
    private static readonly int Throw = Animator.StringToHash("Throw");
    
    private void Awake()
    {
        Instance = this;
        _animator = GetComponent<Animator>();
    }

    [UsedImplicitly]
    public void PickUpTeddy()
    {
        m_Teddy.SetParent(m_PickupPoint);
        m_Teddy.DOMove(m_PickupPoint.position, 0.0f);
    }

    [UsedImplicitly]
    public void ThrowTeddy()
    {
        m_Teddy.parent = null;
            
        m_Teddy.DOJump(m_ThrowPoint.position, 4f, 1, 1f).OnComplete(delegate
        {
            m_Teddy.GetComponent<Rigidbody>().isKinematic = false;
            RexController.Instance.GetHit();
        }).SetEase(Ease.Linear);
        m_Teddy.DORotate(Vector3.up * 180f, 1f).SetEase(Ease.OutSine);
            
        DOVirtual.DelayedCall(.5f, delegate
        {
            CameraController.Instance.SwitchToEatingCam();
        });
    }

    public void ThrowAnimation()
    {
        _animator.SetTrigger(Throw);
    }
}