using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChooseUI : MonoBehaviour
{
    public static WeaponChooseUI Instance;

    [SerializeField] private Transform m_UI;
    [SerializeField] private Button m_BazzokaButton;
    [SerializeField] private Button m_TeddyButton;

    private void Start()
    {
        m_BazzokaButton.onClick.AddListener(OnBazzokaButtonClick);
        m_TeddyButton.onClick.AddListener(OnTeddyButtonClick);
    }

    private void OnTeddyButtonClick()
    {
        PlayerController.Instance.EquipTeddy();
    }

    private void OnBazzokaButtonClick()
    {
        PlayerController.Instance.EquipBazooka();
    }

    private void Awake()
    {
        Instance = this;
    }
    
    public void Show()
    {
        m_UI.localScale = Vector3.one * 0.6f;
        m_UI.gameObject.SetActive(true);
        m_UI.DOScale(1f, .3f).SetEase(Ease.OutBack);
    }

    public void Hide()
    {
        m_UI.DOScale(.5f, .2f).SetEase(Ease.Linear).OnComplete(delegate
        {
            m_UI.gameObject.SetActive(false);
        });
    }
}
