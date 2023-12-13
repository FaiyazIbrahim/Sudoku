using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BottomNumberButton : MonoBehaviour
{
    [SerializeField] private int m_Value;
    [SerializeField] private Button m_Button;
    [SerializeField] private TextMeshProUGUI m_Text;

    [SerializeField] private Color m_SelectedColor;
    [SerializeField] private Color m_DeselectedColor;

    private CellController _cellController;

    private void Awake()
    {
        m_Text.text = m_Value.ToString();
        _cellController = GameObject.FindAnyObjectByType<CellController>();
    }

    private void Start()
    {
        m_Button.onClick.AddListener(delegate
        {
            _cellController.SelectBottomNumber(this, m_Value);
        });
    }

    public void SetAsHighlited(bool value)
    {
        m_Button.image.color = value ? m_SelectedColor : m_DeselectedColor;
    }
}
