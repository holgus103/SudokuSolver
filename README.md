# SudokuSolver
C# Sudoku Solver library

# Example usage

```C#
            // Declare sample array, 0 -> empty space
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
            // Initialize an intance of the Sudoku class
            Sudoku solver = new Sudoku(array);
            // The solve method returns an array of integers that represents the first found solution
            int[] res = solver.Solve();
            // The static method formats the array properly for clarity
            Sudoku.DisplaySudoku(res, this.display);
```
