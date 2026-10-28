using System;
using System.Text;

namespace Lab3;

internal class Arrays
{
    private int[,] _data;

    private static Checked check = new Checked();

    public int Rows
    {
        get
        {
            return _data.GetLength(0);
        }
    }

    public int Cols
    {
        get
        {
            return _data.GetLength(1);
        }
    }

    public Arrays(int n, int m)
    {
        _data = new int[n, m];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                Console.Write(
                    "Элемент [" + i + ", " + j + "]: ");

                _data[i, j] = check.ReadInt(
                    "Введите целое число:");
            }
        }
    }

    public Arrays(int n)
    {
        _data = new int[n, n];
        Random random = new Random();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i > j)
                {
                    _data[i, j] = random.Next(-70, 151);
                }
                else
                {
                    _data[i, j] = random.Next(17, 171);
                }
            }
        }
    }

    public Arrays(int n, bool diamond)
    {
        _data = new int[n, n];

        int[] positions = new int[n * n];
        int[] primes = new int[n * n];
        int count = 0;
        Random random = new Random();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int distance = Math.Abs(i - n / 2)
                    + Math.Abs(j - n / 2);

                if (diamond && distance <= n / 2)
                {
                    positions[count] = i * n + j;
                    count++;
                }
            }
        }

        int candidate = 2;
        int primeCount = 0;

        while (primeCount < count)
        {
            if (IsPrime(candidate))
            {
                primes[primeCount] = candidate;
                primeCount++;
            }

            candidate++;
        }

        for (int i = count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            int temp = primes[i];
            primes[i] = primes[j];
            primes[j] = temp;
        }

        for (int i = 0; i < count; i++)
        {
            int row = positions[i] / n;
            int col = positions[i] % n;

            _data[row, col] = primes[i];
        }
    }

    private Arrays(int[,] values)
    {
        _data = values;
    }

    public static void Task1()
    {
        Console.WriteLine("\nЗадание 1");

        int n = ReadSize(
            "Введите количество строк матрицы: ");

        int m = ReadSize(
            "Введите количество столбцов матрицы: ");

        Arrays first = new Arrays(n, m);
        Arrays second = new Arrays(n);
        Arrays third = new Arrays(n, true);

        Console.WriteLine("\nПервая матрица:");
        Console.WriteLine(first);

        Console.WriteLine("\nВторая матрица:");
        Console.WriteLine(second);

        Console.WriteLine("\nТретья матрица:");
        Console.WriteLine(third);
    }

    public static void Task2()
    {
        Console.WriteLine("\nЗадание 2");

        int n = ReadSize(
            "Введите размер квадратной матрицы: ");

        Arrays matrix = ReadForTask2(n);

        Console.WriteLine("\nИсходная матрица:");
        Console.WriteLine(matrix);
        Console.WriteLine(matrix.GetMaxDiagonal());
    }

    public static void Task3()
    {
        Console.WriteLine("\nЗадание 3");

        int n = ReadSize(
            "Введите размер квадратных матриц: ");

        Arrays a = ReadMatrix(n, "A");
        Arrays b = ReadMatrix(n, "B");
        Arrays c = ReadMatrix(n, "C");

        Arrays result = a.Transpose() * b - c.Transpose();

        Console.WriteLine("\nМатрица A:");
        Console.WriteLine(a);

        Console.WriteLine("\nМатрица B:");
        Console.WriteLine(b);

        Console.WriteLine("\nМатрица C:");
        Console.WriteLine(c);

        Console.WriteLine("\nРезультат A^T * B - C^T:");
        Console.WriteLine(result);
    }

    public static Arrays ReadForTask2(int n)
    {
        Console.WriteLine("Введите элементы матрицы:");

        return new Arrays(n, n);
    }

    public string GetMaxDiagonal()
    {
        int maxSum = int.MinValue;
        int[] bestDiagonal = new int[Rows];
        int bestCount = 0;

        for (int col = 0; col < Cols; col++)
        {
            CheckDiagonal(
                0, col, ref maxSum,
                bestDiagonal, ref bestCount);
        }

        for (int row = 1; row < Rows; row++)
        {
            CheckDiagonal(
                row, 0, ref maxSum,
                bestDiagonal, ref bestCount);
        }

        StringBuilder result = new StringBuilder();

        result.AppendLine(
            "Максимальная сумма диагонали: " + maxSum);

        result.Append("Элементы диагонали: ");

        for (int i = 0; i < bestCount; i++)
        {
            if (i > 0)
            {
                result.Append(" ");
            }

            result.Append(bestDiagonal[i]);
        }

        return result.ToString();
    }

    private void CheckDiagonal(
        int startRow,
        int startCol,
        ref int maxSum,
        int[] bestDiagonal,
        ref int bestCount)
    {
        int sum = 0;
        int count = 0;
        int row = startRow;
        int col = startCol;

        while (row < Rows && col < Cols)
        {
            sum += _data[row, col];
            count++;
            row++;
            col++;
        }

        if (sum > maxSum)
        {
            maxSum = sum;
            bestCount = count;

            row = startRow;
            col = startCol;

            for (int i = 0; i < count; i++)
            {
                bestDiagonal[i] = _data[row, col];

                row++;
                col++;
            }
        }
    }

    public static Arrays ReadMatrix(int n, string name)
    {
        Console.WriteLine("Введите матрицу " + name + ":");

        return new Arrays(n, n);
    }

    public Arrays Transpose()
    {
        int[,] result = new int[Cols, Rows];

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                result[j, i] = _data[i, j];
            }
        }

        return new Arrays(result);
    }

    public static Arrays operator *(Arrays first, Arrays second)
    {
        if (first.Cols != second.Rows)
        {
            throw new ArgumentException(
                "Размеры матриц несовместимы для умножения.");
        }

        int[,] result = new int[first.Rows, second.Cols];

        for (int i = 0; i < first.Rows; i++)
        {
            for (int j = 0; j < second.Cols; j++)
            {
                for (int k = 0; k < first.Cols; k++)
                {
                    result[i, j] +=
                        first._data[i, k] * second._data[k, j];
                }
            }
        }

        return new Arrays(result);
    }

    public static Arrays operator -(Arrays first, Arrays second)
    {
        if (first.Rows != second.Rows
            || first.Cols != second.Cols)
        {
            throw new ArgumentException(
                "Размеры матриц должны совпадать.");
        }

        int[,] result = new int[first.Rows, first.Cols];

        for (int i = 0; i < first.Rows; i++)
        {
            for (int j = 0; j < first.Cols; j++)
            {
                result[i, j] =
                    first._data[i, j] - second._data[i, j];
            }
        }

        return new Arrays(result);
    }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                result.Append(
                    _data[i, j].ToString().PadLeft(6));
            }

            result.AppendLine();
        }

        return result.ToString();
    }

    private static int ReadSize(string message)
    {
        int number = check.NaturalReadInt(message);

        while (number > 99)
        {
            Console.WriteLine(
                "Размер матрицы не должен быть больше 99.");

            number = check.NaturalReadInt(message);
        }

        return number;
    }

    private static bool IsPrime(int number)
    {
        if (number < 2)
        {
            return false;
        }

        for (int i = 2; i <= number / i; i++)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }
}