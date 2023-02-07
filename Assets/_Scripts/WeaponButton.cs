using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Button m_Button;

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(.95f, .15f).SetEase(Ease.Linear);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(1f, .15f).SetEase(Ease.Linear);
    }
}
