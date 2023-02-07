using DG.Tweening;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField] private KeyCode m_ThrowKey;
    [SerializeField] private KeyCode m_DinoEatGirlKey;
    [SerializeField] private KeyCode m_UIShowKey;

    private void Update()
    {
        if(Input.GetKeyDown(m_ThrowKey))
        {
            CameraController.Instance.SwitchToShootCam();
            WeaponChooseUI.Instance.Hide();
            DOVirtual.DelayedCall(.5f, delegate
            {
                PlayerController.Instance.ThrowAnimation();
            });
        }

        if (Input.GetKeyDown(m_DinoEatGirlKey))
        {
            RexController.Instance.TakeInMouth();
        }
        
        if (Input.GetKeyDown(m_UIShowKey))
        {
            WeaponChooseUI.Instance.Show();
        }
    }
}