using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cell : MonoBehaviour
{
    public int CellIndex;

    [SerializeField] private TextMeshProUGUI m_CellText;
    [SerializeField] private Button m_Button;

    private CellController _cellController;

    private void Start()
    {
        m_Button.onClick.AddListener(delegate
        {

        });
    }

    public void SetCellText(string value)
    {
        m_CellText.text = value.ToString(); ;
    }

    public void SetCellController(CellController cellController)
    {
        _cellController = cellController;
    }
}
