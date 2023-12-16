using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ValidationUI : MonoBehaviour
{
    [SerializeField] private CellController m_CellController;
    [SerializeField] private TimeController m_TimeController;
    [SerializeField] private CanvasGroup m_CanvasGroup;
    [SerializeField] private float m_CanvasGroupAlphaFadeDuration;
    [SerializeField] private Transform m_ValidationDone;
    [SerializeField] private Transform m_ValidationFailed;
    [SerializeField] private Button m_RestartButton;

    private void Start()
    {
        m_CanvasGroup.alpha = 0;
        m_CellController.OnGameMatched += ShowValidationUI;

        m_RestartButton.onClick.AddListener(delegate
        {
            m_CellController.RestartLevel();
        });
    }

    private void ShowValidationUI(bool value)
    {
        m_ValidationDone.gameObject.SetActive(value);
        m_ValidationFailed.gameObject.SetActive(!value);
        m_CanvasGroup.DOFade(1, m_CanvasGroupAlphaFadeDuration);
        m_CanvasGroup.interactable = value;
        m_CanvasGroup.blocksRaycasts = value;
        if(!value)
        {
            DOVirtual.DelayedCall(1, delegate
            {
                m_CanvasGroup.DOFade(0, m_CanvasGroupAlphaFadeDuration);
            });
        }
    }

}
