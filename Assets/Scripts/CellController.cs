using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class CellController : MonoBehaviour
{
    public event Action<int> OnBottomNumberSelected;

    [SerializeField] private Cell m_SelectedCell;
    [SerializeField] private BottomNumberButton m_BottomNumberButton;
    [SerializeField] private int m_SelectedBottomNumber;

    private Cell[,] _cells = new Cell[9 , 9];


    public void AddToCellList(int row, int col, Cell cell)
    {
        _cells[row, col] = cell;
    }

    public Cell GetCell(int row, int col)
    {
        return _cells[row, col];
    }

    public void SelectCell(Cell cell)
    {
        m_SelectedCell?.SetAsHighlited(false);
        m_SelectedCell = cell;
        m_SelectedCell.SetAsHighlited(true);
    }

    public void SelectBottomNumber(BottomNumberButton bottomNumberButton, int value)
    {
        m_BottomNumberButton?.SetAsHighlited(false);
        m_BottomNumberButton = bottomNumberButton;
        m_SelectedBottomNumber = value;
        m_BottomNumberButton?.SetAsHighlited(true);
        FindNumber(m_SelectedBottomNumber);
    }

    private async void FindNumber(int value)
    {
        foreach(Cell c in _cells)
        {
            if(c.TargetValue == value)
            {
                c.SetAsHighlited(true);
            }
        }
        await Task.Yield();
    }
}
