using DG.Tweening;
using UnityEngine;

namespace Others
{
    public class Blinker : MonoBehaviour
    {
        [SerializeField] private Color m_ColorA;
        [ColorUsage(true, true)] [SerializeField] private Color m_ColorB;
        [SerializeField] private float m_BlinkingDuration = .3f;
        [SerializeField] private AnimationCurve m_Curve;

        private MeshRenderer _renderer;
        private MaterialPropertyBlock _block;
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        private void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            _renderer.material.EnableKeyword("_EMISSION");
            _block = new MaterialPropertyBlock();
            BlinkingAnimation();
        }

        private void BlinkingAnimation()
        {
            _renderer.GetPropertyBlock(_block);
            DOVirtual.Color(m_ColorA, m_ColorB, m_BlinkingDuration, delegate(Color value)
            {
                _block.SetColor(BaseColor, value);
                _block.SetColor(EmissionColor, value);
                _renderer.SetPropertyBlock(_block);
            }).SetEase(m_Curve).SetLoops(-1, LoopType.Yoyo);
        }
    }
}