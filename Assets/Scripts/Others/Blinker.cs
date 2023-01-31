using DG.Tweening;
using UnityEngine;

namespace Others
{
    public class Blinker : MonoBehaviour
    {
        [SerializeField] private float m_BlinkingDuration = .3f;
        [SerializeField] private AnimationCurve m_Curve;

        private MeshRenderer _renderer;
        private MaterialPropertyBlock _block;
        private static readonly int Interpolation = Shader.PropertyToID("_Interpolation");

        private void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            _block = new MaterialPropertyBlock();
            BlinkingAnimation();
        }

        private void BlinkingAnimation()
        {
            _renderer.GetPropertyBlock(_block);
            DOVirtual.Float(0f, 1f, m_BlinkingDuration, delegate(float value)
            {
                _block.SetFloat(Interpolation, value);
                _renderer.SetPropertyBlock(_block);
            }).SetEase(m_Curve).SetLoops(-1, LoopType.Yoyo);
        }
    }
}