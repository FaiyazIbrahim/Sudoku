using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameDifficultyUI : MonoBehaviour
{
    [SerializeField] private CellController m_CellController;
    [SerializeField] private Button m_EasyMode;
    [SerializeField] private Button m_MediumMode;
    [SerializeField] private Button m_HardMode;
    [SerializeField] private CanvasGroup m_MainPanelCanvasGroup;
    [SerializeField, Space] private Transform m_DifficultyButtonPanel;
    [SerializeField] private float m_DifficultyPanelAnimationDuration;
    [SerializeField] private Ease m_DifficultyPanelAnimationEase;
    [SerializeField, Space] private RectTransform m_TitlePanel;
    [SerializeField] private float m_TitleAnimationDuration;
    [SerializeField] private Ease m_TitleAnimationEase;


    private void Start()
    {
        //m_DifficultyButtonPanel.gameObject.SetActive(false);
        DifficultyButtonsPanelAnimation(Vector2.one);

        m_EasyMode.onClick.AddListener(()=> SetDifficultyMode(1));
        m_MediumMode.onClick.AddListener(()=> SetDifficultyMode(2));
        m_HardMode.onClick.AddListener(()=> SetDifficultyMode(3));
    }

    private void DifficultyButtonsPanelAnimation(Vector2 endPos)
    {
        m_DifficultyButtonPanel.DOScale(endPos, m_DifficultyPanelAnimationDuration).SetEase(m_DifficultyPanelAnimationEase);
    }

    private void TitlePanelAnimation()
    {
        m_TitlePanel.DOAnchorPos(Vector2.zero, m_TitleAnimationDuration).SetEase(m_TitleAnimationEase).OnComplete(delegate
        {
            DOVirtual.DelayedCall(0.5f, FadeOutMainPanel);
        });
    }

    private void FadeOutMainPanel()
    {
        m_MainPanelCanvasGroup.DOFade(0f, 0.3f).OnComplete(delegate
        {
            m_MainPanelCanvasGroup.gameObject.SetActive(false);
        });
    }

    private void SetDifficultyMode(int value)
    {
        Debug.Log(value);
        DifficultyButtonsPanelAnimation(Vector2.zero);
        TitlePanelAnimation();
        switch(value)
        {
            case 1:
                m_CellController.SetDifficulty(UnityEngine.Random.Range(15, 30));
                break;
            case 2:
                m_CellController.SetDifficulty(UnityEngine.Random.Range(30, 45));
                break;
            case 3:
                m_CellController.SetDifficulty(UnityEngine.Random.Range(45, 55));
                break;
        }
    }

}

