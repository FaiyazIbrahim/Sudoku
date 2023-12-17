using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;


public class CellGenerator : MonoBehaviour
{
    [SerializeField] private Cell m_Cell;
    [SerializeField] private CellController m_CellController;
    [SerializeField] private RectTransform m_GridBgImage;
    [SerializeField] private Transform m_GridBgImageParentTransform;

    private int[,] _sudokuGrid = new int[9, 9];
    private int _numberOfCellsCanBeInteractable;

    private void Start()
    {
        m_CellController.OnGameStarted += InitilizeGame;
    }

    private void InitilizeGame(int playableCellsCount)
    {
        _numberOfCellsCanBeInteractable = playableCellsCount;
        GenerateSudokuValues();
        GenerateSudokuCells();
    }

    private void GenerateSudokuValues()
    {
        if (SolveSudoku())
        {
            Debug.Log("Sudoku Initialized");
        }
    }

    private bool SolveSudoku()
    {
        int row, col;

        if (!FindEmptyCell(out row, out col))
        {
            return true;
        }

        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Shuffle(numbers);

        for (int num = 1; num <= 9; num++)
        {
            if (IsSafe(row, col, numbers[num - 1]))
            {
                _sudokuGrid[row, col] = numbers[num - 1];
                



                if (SolveSudoku())
                {
                    return true;
                }

                _sudokuGrid[row, col] = 0;

                
            }
        }

        return false;
    }

    private bool FindEmptyCell(out int row, out int col)
    {
        for (row = 0; row < 9; row++)
        {
            for (col = 0; col < 9; col++)
            {
                if (_sudokuGrid[row, col] == 0)
                {
                    return true;
                }
            }
        }

        row = col = -1;
        return false;
    }

    private bool IsSafe(int row, int col, int num)
    {
        return !UsedInRow(row, num) && !UsedInCol(col, num) && !UsedInBox(row - row % 3, col - col % 3, num);
    }

    private bool UsedInRow(int row, int num)
    {
        for (int col = 0; col < 9; col++)
        {
            if (_sudokuGrid[row, col] == num)
            {
                return true;
            }
        }
        return false;
    }

    private bool UsedInCol(int col, int num)
    {
        for (int row = 0; row < 9; row++)
        {
            if (_sudokuGrid[row, col] == num)
            {
                return true;
            }
        }
        return false;
    }

    private bool UsedInBox(int boxStartRow, int boxStartCol, int num)
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (_sudokuGrid[boxStartRow + row, boxStartCol + col] == num)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void RemoveNumbers()
    {
        for (int i = 0; i < _numberOfCellsCanBeInteractable; i++)
        {
            int row = Random.Range(0, 9);
            int col = Random.Range(0, 9);


            while (_sudokuGrid[row, col] == 0)
            {
                row = Random.Range(0, 9);
                col = Random.Range(0, 9);
            }

            _sudokuGrid[row, col] = 0;
            m_CellController.GetCell(row, col).SetCellTextVisual(0 , true);
        }
    }

    private void GenerateSudokuCells()
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                Cell cell = Instantiate(m_Cell, transform);
                int value = _sudokuGrid[row, col];
                cell.SetCellTextVisual(value);
                cell.SetTargetValue(value);
                cell.SetCellController(m_CellController);
                cell.gameObject.name = value.ToString();
                m_CellController.AddToCellList(row, col, cell);
            }
        }
        
        RemoveNumbers();

        DOVirtual.DelayedCall(0.4f, delegate
        {
            DetectSubgrids();
        });
    }


    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    void DetectSubgrids()
    {
        
        for (int startRow = 0; startRow < 9; startRow += 3)
        {
            for (int startCol = 0; startCol < 9; startCol += 3)
            {
                GenerateGridBgImage(startRow, startCol);
            }
        }
    }

    
    private async void GenerateGridBgImage(int startRow, int startCol)
    {
        //Debug.Log("Row: " + startRow + ", Col: " + startCol);
        int i = 0;
        for (int row = startRow; row < startRow + 3; row++)
        {
            for (int col = startCol; col < startCol + 3; col++)
            {
                i++;

                if(i == 5)
                {
                    RectTransform cellImage = Instantiate(m_GridBgImage, m_GridBgImageParentTransform);
                    Vector3 pos = m_CellController.GetCell(row, col).GetComponent<RectTransform>().anchoredPosition;
                    cellImage.anchoredPosition = new Vector3(pos.x, pos.y, -1);
                    //Debug.Log(m_CellController.GetCell(row, col).gameObject.name + "  " + pos);
                }

                
            }
        }
        await Task.Yield();
    }

}

