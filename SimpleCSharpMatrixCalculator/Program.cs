using System;

namespace SimpleCSharpMatrixCalculator
{
    class Program
    {
        private static string startMessage = "Welcome to simple matrix calculator!\n" +
            "Type number, press enter, then type arithmetic operation (+, -, *, /), \n" +
            "again press enter and again type number with enter to calculate opration.\n" +
            "Type \"matrix\" to type fisrt matrix, and when when the first matrix is entered, type * to start typing second matrix.\n" +
            "When the second matrix is entered, type = to see the result. Type \"arithmetic\" to back to the arifmetic calculator\n" +
            "Type \"quit\" to close the program";
        private const string matrixParameter = "matrix";
        private const string arithmeticParameter = "arithmetic";
        private const string quitParameter = "quit";
        private const string wrongDataFormat = "Wrong  data format, enter data again.";
        private const string wrongArythmeticSymbol = "Wrong  arythmetic symbol, enter data again.";
        private const string zeroDivision = "Division by zero error.";
        private const string wrongNumber = "Parameter should be more than 0! Try again.";
        private const string wrongType = "Parameter should be a number!";
        private const string wrongNumberOfColumnsAndRows = "The number of columns of the first matrix must be equal to the number of rows of the second matrix!";
        private const string inputtedMatrix = "Inputted matrix:";
        private const string multiplicatedMatrix = "Multiplicated matrix:";
        private const string inputNextParametr = "Input the next parameter to menu (matrix, arithmetic, quit)";
        private const string previousMatrixMessage = "The previous matrix is: ";
        private const string previousMatrixChoice = "Type \"*\" to work with previous matrix, or enter to type new matrix ";



        private static int previousArifmeticResult = 0;
        private static int?[,] previousMatrixResult;

        private static string inputtedMenuParameter;

        static void Main(string[] args)
        {
            Console.WriteLine(startMessage);
            inputtedMenuParameter = arithmeticParameter;
            ShowMenu();
        }

        private static void ShowMenu()
        {
            do
            {
                switch (inputtedMenuParameter)
                {
                    case matrixParameter:
                        MatrixCalculator();
                        break;
                    case arithmeticParameter:
                        ArithmeticCalculator();
                        break;
                    case quitParameter:
                        Console.WriteLine("Quit");
                        break;
                }
            } while (!inputtedMenuParameter.Equals(quitParameter));
        }

        private static void ArithmeticCalculator()
        {
            string inputtedParameter = default, arithmeticOperation = default;
            int firstNumber = default, secondNumber = default;
            bool isNumeric, backToMainMenu = false; ;
            do
            {
                Console.WriteLine(previousArifmeticResult);
                do
                {
                    inputtedParameter = Console.ReadLine();
                    if (IsInptuttedValueIsMenuParametrAndSetIfTrue(inputtedParameter))
                    {
                        backToMainMenu = true;
                        break;
                    }

                    isNumeric = int.TryParse(inputtedParameter, out firstNumber);
                    if (!isNumeric)
                    {
                        firstNumber = previousArifmeticResult;
                        arithmeticOperation = inputtedParameter;
                    }
                    else
                    {
                        arithmeticOperation = Console.ReadLine();
                    }

                    if (IsInptuttedValueIsMenuParametrAndSetIfTrue(arithmeticOperation))
                    {
                        backToMainMenu = true;
                        break;
                    }

                    if (!arithmeticOperation.Equals("+") & !arithmeticOperation.Equals("-") & !arithmeticOperation.Equals("*")
                        & !arithmeticOperation.Equals("/"))
                    {
                        Console.WriteLine(wrongArythmeticSymbol);
                    }
                    else
                    {
                        break;
                    }
                } while (true);

                if (backToMainMenu)
                {
                    break;
                }

                do
                {
                    inputtedParameter = Console.ReadLine();

                    if (IsInptuttedValueIsMenuParametrAndSetIfTrue(inputtedParameter))
                    {
                        backToMainMenu = true;
                        break;
                    }

                    isNumeric = int.TryParse(inputtedParameter, out secondNumber);
                    if (!isNumeric)
                    {
                        Console.WriteLine(wrongDataFormat);
                    }
                    else
                    {
                        break;
                    }
                } while (true);

                if (backToMainMenu)
                {
                    break;
                }

                switch (arithmeticOperation)
                {
                    case "+":
                        previousArifmeticResult = firstNumber + secondNumber;
                        break;
                    case "-":
                        previousArifmeticResult = firstNumber - secondNumber;
                        break;
                    case "*":
                        previousArifmeticResult = firstNumber * secondNumber;
                        break;
                    case "/":
                        if (secondNumber == 0)
                        {
                            Console.WriteLine(zeroDivision);
                        }
                        else
                        {
                            previousArifmeticResult = firstNumber / secondNumber;
                        }
                        break;
                }
                if (secondNumber != 0 & !arithmeticParameter.Equals("/"))
                {
                    Console.WriteLine($"{firstNumber} {arithmeticOperation} {secondNumber} = {previousArifmeticResult}");
                }
            } while (!backToMainMenu);
        }

        private static bool IsInptuttedValueIsMenuParametrAndSetIfTrue(string inputtedParameter)
        {
            if (inputtedParameter.Equals(quitParameter) | inputtedParameter.Equals(matrixParameter) | inputtedParameter.Equals(arithmeticParameter))
            {
                inputtedMenuParameter = inputtedParameter;
                return true;
            }
            return false;
        }

        private static void MatrixCalculator()
        {
            string inputtedParameter = "";
            int?[,] firstMatrix;
            int?[,] secondMatrix;
            bool backToMainMenu = false;
            do
            {
                if (previousMatrixResult != null)
                {
                    Console.WriteLine(previousMatrixMessage);
                    MatrixOutput(previousMatrixResult);
                    Console.WriteLine(previousMatrixChoice);
                    inputtedParameter = Console.ReadLine();
                }

                int firstMatrixColumnsNumber, firstMatrixRowsNumber, secondMatrixColumnsNumber, secondMatrixRowsNumber;

                if (inputtedParameter.Equals("*"))
                {
                    firstMatrix = previousMatrixResult;
                    firstMatrixRowsNumber = previousMatrixResult.GetUpperBound(0) + 1;
                    firstMatrixColumnsNumber = previousMatrixResult.Length / firstMatrixRowsNumber;
                } else
                {
                    firstMatrixColumnsNumber = GetMatrixParameters("columns", "first");
                    firstMatrixRowsNumber = GetMatrixParameters("rows", "first");
                    firstMatrix = MatrixInput(firstMatrixColumnsNumber, firstMatrixRowsNumber);
                    Console.WriteLine(inputtedMatrix);
                    MatrixOutput(firstMatrix);
                }               

                secondMatrixColumnsNumber = GetMatrixParameters("columns", "second");

                do
                {
                    secondMatrixRowsNumber = GetMatrixParameters("rows", "second");
                    if (firstMatrixColumnsNumber != secondMatrixRowsNumber)
                    {
                        Console.WriteLine(wrongNumberOfColumnsAndRows);
                    }
                } while (firstMatrixColumnsNumber != secondMatrixRowsNumber);
                secondMatrix = MatrixInput(secondMatrixColumnsNumber, secondMatrixRowsNumber);
                Console.WriteLine(inputtedMatrix);
                MatrixOutput(secondMatrix);

                previousMatrixResult = MatrixMultiplication(firstMatrix, secondMatrix);
                Console.WriteLine(multiplicatedMatrix);
                MatrixOutput(previousMatrixResult);

                Console.WriteLine(inputNextParametr);
                inputtedParameter = Console.ReadLine();
                IsInptuttedValueIsMenuParametrAndSetIfTrue(inputtedParameter);
            } while (inputNextParametr.Equals(matrixParameter));
        }


        private static int GetMatrixParameters(string parameterType, string matrixNumber)
        {
            string inputtedParameter;
            bool isNumeric;
            int result = 0;
            do
            {
                Console.WriteLine($"Input number of {parameterType} of {matrixNumber} matrix.");
                inputtedParameter = Console.ReadLine();
                isNumeric = int.TryParse(inputtedParameter, out result);
                if (!isNumeric)
                {
                    Console.WriteLine(wrongType);
                }
                else if (result < 0)
                {
                    Console.WriteLine(wrongNumber);
                }
            } while (result < 1);
            return result;
        }

        private static int?[,] MatrixInput(int columnsNumber, int rowsNumber)
        {
            int?[,] result = new int?[rowsNumber, columnsNumber];

            for (int i = 0; i < rowsNumber; i++)
            {
                Console.WriteLine($"Enter row №{i} of matrix.");
                for (int j = 0; j < columnsNumber; j++)
                {
                    result[i, j] = int.Parse(Console.ReadLine());
                }
            }
            return result;
        }

        private static void MatrixOutput(int?[,] matrixToPrint)
        {
            int rows = matrixToPrint.GetUpperBound(0) + 1;
            int columns = matrixToPrint.Length / rows;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{matrixToPrint[i, j]} \t");
                }
                Console.WriteLine();
            }
        }

        private static int?[,] MatrixMultiplication(int?[,] firstMatrix, int?[,] secondMatrix)
        {
            int firstMatrixRows = firstMatrix.GetUpperBound(0) + 1;
            int firstMatrixColumns = firstMatrix.Length / firstMatrixRows;
            int secondMatrixRows = secondMatrix.GetUpperBound(0) + 1;
            int secondMatrixColumns = secondMatrix.Length / secondMatrixRows;

            int?[,] result = new int?[firstMatrixRows, secondMatrixColumns];

            for (int i = 0; i < firstMatrixRows; i++)
            {
                for (int j = 0; j < secondMatrixColumns; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < firstMatrixColumns; k++)
                    {
                        result[i, j] += firstMatrix[i, k] * secondMatrix[k, j];
                    }
                }
            }

            return result;
        }
    }
}
