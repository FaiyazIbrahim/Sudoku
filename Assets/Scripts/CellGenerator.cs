using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGenerator : MonoBehaviour
{
    [SerializeField] private Cell m_Cell;
    [SerializeField] private CellController m_CellController;

    private int[,] sudokuGrid = new int[9, 9];

    void Start()
    {
        GenerateSudoku();
        PrintSudoku();
    }

    void GenerateSudoku()
    {
        if (SolveSudoku())
        {
            // Successfully generated a solved Sudoku puzzle, now remove some numbers to make it a puzzle
            RemoveNumbers();
        }
        else
        {
            Debug.LogError("Failed to generate Sudoku puzzle. Retry or adjust the algorithm.");
        }
    }

    bool SolveSudoku()
    {
        int row, col;

        if (!FindEmptyCell(out row, out col))
        {
            // No empty cells, puzzle is solved
            return true;
        }

        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Shuffle(numbers);

        for (int num = 1; num <= 9; num++)
        {
            if (IsSafe(row, col, numbers[num - 1]))
            {
                sudokuGrid[row, col] = numbers[num - 1];
                Debug.Log(numbers[num - 1]);



                if (SolveSudoku())
                {
                    return true;
                }

                // If placing num at (row, col) doesn't lead to a solution, backtrack
                sudokuGrid[row, col] = 0;
            }
        }

        // No number can be placed at this cell
        return false;
    }

    bool FindEmptyCell(out int row, out int col)
    {
        for (row = 0; row < 9; row++)
        {
            for (col = 0; col < 9; col++)
            {
                if (sudokuGrid[row, col] == 0)
                {
                    // Found an empty cell
                    return true;
                }
            }
        }

        // No empty cell found
        row = col = -1;
        return false;
    }

    bool IsSafe(int row, int col, int num)
    {
        // Check if 'num' is not already present in the current row, column, and 3x3 subgrid
        return !UsedInRow(row, num) && !UsedInCol(col, num) && !UsedInBox(row - row % 3, col - col % 3, num);
    }

    bool UsedInRow(int row, int num)
    {
        for (int col = 0; col < 9; col++)
        {
            if (sudokuGrid[row, col] == num)
            {
                return true;
            }
        }
        return false;
    }

    bool UsedInCol(int col, int num)
    {
        for (int row = 0; row < 9; row++)
        {
            if (sudokuGrid[row, col] == num)
            {
                return true;
            }
        }
        return false;
    }

    bool UsedInBox(int boxStartRow, int boxStartCol, int num)
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (sudokuGrid[boxStartRow + row, boxStartCol + col] == num)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void RemoveNumbers()
    {
        // Specify the number of cells to be removed (adjust this based on difficulty level)
        int cellsToRemove = Random.Range(40, 55);

        for (int i = 0; i < cellsToRemove; i++)
        {
            int row = Random.Range(0, 9);
            int col = Random.Range(0, 9);

            // Ensure the cell is not already empty
            while (sudokuGrid[row, col] == 0)
            {
                row = Random.Range(0, 9);
                col = Random.Range(0, 9);
            }

            sudokuGrid[row, col] = 0;
        }
    }

    void PrintSudoku()
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                Cell cell = Instantiate(m_Cell, transform);
                int value = sudokuGrid[row, col];
                cell.SetCellText(value == 0? " " : value.ToString());
            }
        }

    }


    //void CheckColumn(int columnIndex)
    //{
    //    for (int row = 0; row < 9; row++) //sudokuGrid.GetLength(0)
    //    {
    //        int value = sudokuGrid[row, columnIndex];
    //        Debug.Log($"myArray[{row},{columnIndex}] = {value}");
    //    }
    //}

    private static void Shuffle<T>(List<T> list)
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

}

