using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform m_Teddy;
    [SerializeField] private Transform m_PickupPoint;
    [SerializeField] private Transform m_ThrowPoint;

    private Animator _animator;
    private static readonly int Throw = Animator.StringToHash("Throw");

    private RexController _rexController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _rexController = FindObjectOfType<RexController>();
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
        }).SetEase(Ease.Linear);
        m_Teddy.DORotate(Vector3.up * 180f, 1f).SetEase(Ease.OutSine);
            
        DOVirtual.DelayedCall(.5f, delegate
        {
            CameraController.Instance.SwitchToEatingCam();
        });
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            _animator.SetTrigger(Throw);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            _rexController.Eat();
        }
    }
}