using System;
using System.Collections.Generic;
using SudokuSolver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;

namespace SudokuTests
{
    [TestClass]
    public class SudokuTests
    {
        private void display(string message)
        {
            Debug.Write(message);
        }

        [TestMethod]
        public void SuperEasyTest()
        {
            int[] array = {
                    //9,2,6, 5,7,1, 4,8,3,
                    //3,5,1, 4,8,6, 2,7,9,
                    //8,7,4, 9,2,3, 5,1,6,
                    0,0,0, 0,0,0, 4,8,3,
                    0,0,0, 0,0,0, 2,7,9,
                    0,0,0, 0,0,0, 5,1,6,

                    5,8,2, 3,6,7, 1,9,4,
                    1,4,9, 2,5,8, 3,6,7,
                    7,6,3, 1,4,9, 8,2,5,

                    2,3,8, 7,9,4, 6,5,1,
                    6,1,7, 8,3,5, 9,4,2,
                    4,9,5, 6,1,2, 7,3,8
                    };
            Sudoku solver = new Sudoku(array);
            int[] res = solver.Solve();
            Sudoku.DisplaySudoku(res, this.display);
            Sudoku solved = new Sudoku(res);
            Assert.IsTrue(solved.IsValid());
        }

        [TestMethod]
        public void MediumTest()
        {
            int[] array = {
                    5,3,0, 0,7,0, 0,0,0,
                    6,0,0, 1,9,5, 0,0,0,
                    0,9,8, 0,0,0, 0,6,0,

                    8,0,0, 0,6,0, 0,0,3,
                    4,0,0, 8,0,3, 0,0,1,
                    7,0,0, 0,2,0, 0,0,6,

                    0,6,0, 0,0,0, 2,8,0,
                    0,0,0, 4,1,9, 0,0,5,
                    0,0,0, 0,8,0, 0,7,9
                    };
            Sudoku solver = new Sudoku(array);
            int[] res = solver.Solve();
            Sudoku.DisplaySudoku(res, this.display);
            Sudoku solved = new Sudoku(res);
            Assert.IsTrue(solved.IsValid());
        }

        [TestMethod]
        public void EmptySudoku()
        {
            int[] array = new int[81];
            Sudoku sudoku = new Sudoku(array);
            int[] res = sudoku.Solve();
            Sudoku.DisplaySudoku(res, this.display);
            Sudoku solution = new Sudoku(res);
            Assert.IsTrue(solution.IsValid());
        }

        [TestMethod]
        public void FileValidation()
        {
            string[] sudokus = File.ReadAllText(@"in.txt").Split("[]".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int[] array = new int[81];
            for (var j = 0; j < sudokus.Length; j += 2)
            {
                string[] temp = sudokus[j + 1].Split(" ,\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < 81; i++)
                {
                    array[i] = Convert.ToInt32(temp[i]);
                }
                int[] results = new int[81];
                temp = sudokus[j + 3].Split(" ,\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < 81; i++)
                {
                    results[i] = Convert.ToInt32(temp[i]);
                }
                Sudoku sudoku = new Sudoku(array);
                CollectionAssert.AreEqual(results, sudoku.Solve());
            }
        }

        [TestMethod]
        public void PablosTest()
        {
            var start = DateTime.Now;
            var sudokus = new List<Sudoku>(50);
            string[] lines = File.ReadAllLines(@"pablo.txt");
            for (var i = 0; i < lines.Length; i += 10)
            {
                int[] array = new int[81];

                // for every row
                for (var j = 0; j < 9; j++)
                {
                    for (var k = 0; k < 9; k++)
                    {
                        array[j * 9 + k] = (int)(lines[i + 1 + j][k]) - (int)'0';
                    }
                }
                sudokus.Add(new Sudoku(array));
            }
            var mid = DateTime.Now;
            sudokus.ForEach(s => s.Solve());
            var end = DateTime.Now;
            Debug.WriteLine((end - start).TotalMilliseconds);
            Debug.WriteLine((mid - start).TotalMilliseconds);
        }
    }
}
