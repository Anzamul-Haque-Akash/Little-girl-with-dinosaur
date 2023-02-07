using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField] private KeyCode m_ThrowKey;
    [SerializeField] private KeyCode m_DinoEatGirlKey;

    private void Update()
    {
        if(Input.GetKeyDown(m_ThrowKey))
        {
            PlayerController.Instance.ThrowAnimation();
        }

        if (Input.GetKeyDown(m_DinoEatGirlKey))
        {
            RexController.Instance.TakeInMouth();
        }
    }
}