using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cell : MonoBehaviour
{
    public int TargetValue;

    [SerializeField] private TextMeshProUGUI m_CellText;
    [SerializeField] private Button m_Button;

    [SerializeField] private Color m_SelectedColor;
    [SerializeField] private Color m_DeselectedColor;

    private CellController _cellController;

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
    }

    public void SetCellController(CellController cellController)
    {
        _cellController = cellController;
    }

    public void SetAsHighlited(bool value)
    {
        m_Button.image.color = value ? m_SelectedColor : m_DeselectedColor;
    }

}
