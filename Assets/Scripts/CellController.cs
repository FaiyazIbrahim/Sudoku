using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class CellController : MonoBehaviour
{
    public event Action<int> OnGameStarted;
    public event Action<bool> OnGameMatched;

    [SerializeField] private Cell m_SelectedCell;
    [SerializeField] private BottomNumberButton m_BottomNumberButton;
    [SerializeField] private int m_SelectedBottomNumber;
    [Space]
    [SerializeField] private Button m_RestartButton;
    [SerializeField] private Button m_ValidateButton;
    [SerializeField] private Button m_RestartLevelButton;

    private Cell[,] _cells = new Cell[9 , 9];
    private bool _SudokuMatched = true;


    private void Start()
    {
        m_RestartButton.onClick.AddListener(RestartSudoku);
        m_ValidateButton.onClick.AddListener(ValidateCells);
        m_RestartLevelButton.onClick.AddListener(RestartLevel);
    }

    public void SetDifficulty(int value)
    {
        OnGameStarted?.Invoke(value);
    }

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
        FindNumber(m_SelectedCell.GetCurrentValue());
        m_SelectedCell.SetAsHighlited(true);
    }

    public void SelectBottomNumber(BottomNumberButton bottomNumberButton, int value)
    {
        m_BottomNumberButton?.SetAsHighlited(false);
        m_BottomNumberButton = bottomNumberButton;
        m_SelectedBottomNumber = value;
        m_BottomNumberButton?.SetAsHighlited(true);
        FindNumber(m_SelectedBottomNumber);
        SetValueToCell(m_SelectedBottomNumber);
    }


    private void SetValueToCell(int value)
    {
        m_SelectedCell?.SetCellTextVisual(value, true);
        m_SelectedCell?.SetAsHighlited(true);
    }

    private async void FindNumber(int value)
    {
        if (value == 0) value = -1;
        foreach(Cell c in _cells)
        {
            if(c.GetCurrentValue() == value)
            {
                c.SetAsHighlited(true, true);
            }
            else
            {
                c.SetAsHighlited(false);
            }
        }
        await Task.Yield();
    }

 
    private void ValidateCells()
    {
        _SudokuMatched = true;
        foreach (Cell c in _cells)
        {
            if(c.CheckAble)
            {
                c.Validate(delegate
                {
                    _SudokuMatched = false;
                });
            }
        }

        if(_SudokuMatched)
        {
            OnGameMatched?.Invoke(true);
        }
        else
        {
            OnGameMatched?.Invoke(false);
        }
    }

    private void RestartSudoku()
    {
        FindNumber(0);
        foreach (Cell c in _cells)
        {
            if (c.CheckAble)
            {
                c.SetCellTextVisual(0, true);
                c.SetAsHighlited(false, true);
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //test
    [ContextMenu("MATCH ALL")]
    private void DebugMatchAll()
    {
        foreach (Cell c in _cells)
        {
            if (c.CheckAble)
            {
                c.DebugMatch();
            }
        }
    }

}
