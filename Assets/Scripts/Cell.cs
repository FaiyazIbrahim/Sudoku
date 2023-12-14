using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Cell : MonoBehaviour
{
    [field: SerializeField] public bool CheckAble { get; private set; }

    [SerializeField] private TextMeshProUGUI m_CellText;
    [SerializeField] private Button m_Button;
    [SerializeField] private Color m_SelectedColor;
    [SerializeField] private Color m_DeselectedColor;
    [SerializeField] private Color m_MissMatchedColor;
    [SerializeField] private float m_PopDuration;
    [SerializeField] private Ease m_PopEase;

    private CellController _cellController;
    private int _targetValue;
    private int _currentValue;


    private void Start()
    {
        m_Button.onClick.AddListener(delegate
        {
            _cellController.SelectCell(this);
        });
    }

    public void SetCellTextVisual(int value, bool ButtonInteractivity = false)
    {
        m_CellText.text = value > 0? value.ToString() : "";
        m_Button.interactable = ButtonInteractivity;
        _currentValue = value;
        CheckAble = ButtonInteractivity;
    }

    public void SetCellController(CellController cellController)
    {
        _cellController = cellController;
    }

    public void SetAsHighlited(bool value, bool canPop = false)
    {
        m_Button.image.color = value ? m_SelectedColor : m_DeselectedColor;
        if(canPop) PopEffect();
    }

    public void SetTargetValue(int value) => _targetValue = value;

    public int GetCurrentValue() => _currentValue;

    public void Validate()
    {
        if(_targetValue != _currentValue && _currentValue != 0)
        {
            m_Button.image.color = m_MissMatchedColor;
            PopEffect();
        }
    }

    private void PopEffect()
    {
        transform.localScale = Vector3.one * 0.6f;
        transform.DOScale(Vector3.one, m_PopDuration).SetEase(m_PopEase);
    }
}
