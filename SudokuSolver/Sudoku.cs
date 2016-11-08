using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class Sudoku
    {
        private int[] sudoku;
        private int[] result = null;
        private bool isValid;

        public int[] Result
        {
            get
            {
                if (this.isValid)
                {
                    if(this.result == null)
                    {
                        this.result = this.Solve();
                    }
                    return result;
                }
                return null;
            }
        }

        private bool checkRow(int elNum, int val = 0)
        {
            val = val > 0 ? val : this.result[elNum];
            if (val == 0)
            {
                return true;
            }
            int start = (elNum / 9) * 9;
            int end = start + 9;
            for (int i = start; i < end; i++)
            {
                if (i == elNum)
                    continue;
                if (this.result[i] == val)
                    return false;
            }
            return true;
        }

        private bool checkColumn(int elNum, int val = 0)
        {
            val = val > 0 ? val : this.result[elNum];
            if (val == 0)
            {
                return true;
            }
            int start = elNum % 9;
            int end = start + 72;
            for (int i = start; i <= end; i += 9)
            {
                if (i == elNum)
                    continue;
                if (this.result[i] == val)
                    return false;
            }
            return true;
        }

        private bool isEmpty(int elementNumber)
        {
            if (this.sudoku[elementNumber] == 0)
                return true;
            else
                return false;
        }

        private bool checkSquare(int elNum, int val = 0)
        {
            val = val > 0 ? val : this.result[elNum];
            if (val == 0)
            {
                return true;
            }
            int rowNumber = elNum / 9;
            int colNumber = elNum % 9;
            int squareX = colNumber / 3;
            int squareY = rowNumber / 3;
            int index;
            int start = 27 * squareY + squareX * 3;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    index = start + i * 9 + j;
                    if (index == elNum)
                        continue;
                    if (this.result[index] == val)
                        return false;
                }
            }
            return true;
        }

        private bool checkConstraints(int elNum, int val = 0)
        {
            return this.checkColumn(elNum, val) && this.checkRow(elNum, val) && this.checkSquare(elNum, val);
        }

        public Sudoku(int[] sudoku)
        {
            this.sudoku = sudoku;
            this.isValid = this.IsValid(this.sudoku);
        }

        public int[] Solve()
        {
            this.result = (int[])this.sudoku.Clone();
            int correctPositions = 0;
            //foreach(byte val in result)
            //{
            //    if (val > 0)
            //        correctPositions++;
            //}

            int currentPosition = 0;
            while (correctPositions < 81)
            {
                if (this.isEmpty(currentPosition))
                {
                    bool placedSuccessfully = false;
                    for (int i = result[currentPosition] + 1; i < 10; i++)
                    {
                        if (this.checkConstraints(currentPosition, i))
                        {
                            this.result[currentPosition] = i;
                            currentPosition++;
                            correctPositions++;
                            placedSuccessfully = true;
                            break;
                        }
                    }
                    if (!placedSuccessfully)
                    {
                        bool isEmpty = true;
                        do
                        {
                            if (isEmpty)
                                this.result[currentPosition] = 0;
                            currentPosition--;
                            correctPositions--;
                            isEmpty = this.isEmpty(currentPosition);
                        }
                        while (!isEmpty || this.result[currentPosition] > 8);
                    }
                }
                else
                {
                    currentPosition++;
                    correctPositions++;
                }

            }
            return this.result;

        }

        public bool IsValid()
        {
            return this.isValid;
        }

        public bool IsValid(int[] sudoku)
        {
            int[] temp = result;
            this.result = sudoku;
            for (int i = 0; i < 81; i++)
            {
                if (!this.checkConstraints(i))
                {
                    this.result = null;
                    return false;
                }
            }
            this.result = temp;
            return true;
        }

        public void DisplaySudoku(OutputStream stream)
        {
            Sudoku.DisplaySudoku(this.sudoku, stream);
        }

        public static void DisplaySudoku(int[] sudoku, OutputStream stream)
        {
            for (int i = 0; i < 81; i++)
            {
                // spaces between squares
                if (i % 3 == 0 && i % 9 != 0)
                    stream(" ");
                // separate lines
                if (i % 9 == 0)
                    stream(Environment.NewLine);
                // separate squares vertically
                if (i % 27 == 0)
                    stream(Environment.NewLine);
                stream(sudoku[i].ToString());
            }
        }

        public delegate void OutputStream(string message);
    }
}
