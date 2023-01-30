using UnityEngine;

namespace Others
{
    public class DoorLight : MonoBehaviour
    {
        private Renderer _doorLightRenderer;

        private void Start()
        {
            _doorLightRenderer = GetComponent<Renderer>();
            
            Color customColor = new Color(255f, 255f, 255f, 1.0f);
            
            _doorLightRenderer.material.SetColor("_Color", customColor);
        }
    }
}