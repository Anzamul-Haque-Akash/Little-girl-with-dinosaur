using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    [SerializeField] private CinemachineImpulseSource m_ImpulseSource;

    [SerializeField, BoxGroup("Cinemachine VCams")]
    private CinemachineVirtualCamera _TRexCam;
    [SerializeField, BoxGroup("Cinemachine VCams")]
    private CinemachineVirtualCamera m_EatingCam;
    
    [SerializeField, BoxGroup("Cinemachine VCams")]
    private CinemachineVirtualCamera m_UICam;
    
    [SerializeField, BoxGroup("Cinemachine VCams")]
    private CinemachineVirtualCamera m_PlayerCam;


    private CinemachineVirtualCamera _vCam;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SwitchCam(_TRexCam);
    }

    private void SwitchCam(CinemachineVirtualCamera vCam)
    {
        if (_vCam != null) _vCam.Priority = 1;
        _vCam = vCam;
        _vCam.Priority = 101;
    }

    public void DoShake(float intensity = 1f)
    {
        m_ImpulseSource.GenerateImpulse(intensity);
    }

    public void SwitchToEatingCam()
    {
        SwitchCam(m_EatingCam);
    }

    public void SwitchToUIChooseCam()
    {
        SwitchCam(m_UICam);
    }
    
    public void SwitchToShootCam()
    {
        SwitchCam(m_PlayerCam);
    }
}
