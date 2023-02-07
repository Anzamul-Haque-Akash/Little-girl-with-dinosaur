using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    
    [SerializeField] private Transform m_Teddy;
    [SerializeField] private Transform m_PickupPoint;
    [SerializeField] private Transform m_ThrowPoint;

    [SerializeField] private GameObject m_Bazooka;
    [SerializeField] private GameObject m_BazookaInWorld;

    [SerializeField] private GameObject m_TeddyInWorld;

    private Animator _animator;
    private static readonly int Throw = Animator.StringToHash("Throw");
    private static readonly int Bazooka = Animator.StringToHash("Bazooka");
    
    private void Awake()
    {
        Instance = this;
        _animator = GetComponent<Animator>();
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
            
        DOVirtual.DelayedCall(.3f, delegate
        {
            CameraController.Instance.SwitchToEatingCam();
        });
    }

    public void ThrowAnimation()
    {
        _animator.SetTrigger(Throw);
    }

    public void EquipBazooka()
    {
        _animator.SetBool(Bazooka, true);
        m_Bazooka.SetActive(true);
        m_BazookaInWorld.SetActive(false);
        
        m_Teddy.gameObject.SetActive(false);
        m_TeddyInWorld.SetActive(true);
    }

    public void EquipTeddy()
    {
        m_Teddy.gameObject.SetActive(true);
        m_TeddyInWorld.SetActive(false);
        
        _animator.SetBool(Bazooka, false);
        m_Bazooka.SetActive(false);
        m_BazookaInWorld.SetActive(true);
    }
}