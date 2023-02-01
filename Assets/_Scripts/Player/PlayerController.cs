using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform m_Teddy;
        [SerializeField] private Transform m_PickupPoint;

        [UsedImplicitly]
        public void PickUpTeddy()
        {
            m_Teddy.SetParent(m_PickupPoint);
            m_Teddy.DOMove(m_PickupPoint.position, 0.0f);
        }
    }
}
