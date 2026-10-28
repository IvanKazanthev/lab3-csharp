using System;
using System.Globalization;

namespace Lab3
{
    internal class Checked
    {
        public int ReadInt(string promt)
        {
            Console.WriteLine(promt);
            string? input = Console.ReadLine();
            int x = 0;

            while (true)
            {
                if (int.TryParse(input, out x))
                {
                    return x;
                }
                else
                {
                    Console.WriteLine(
                        "Ошибка! Введите целое число:");
                    input = Console.ReadLine();
                }
            }
        }

        public int NaturalReadInt(string promt)
        {
            Console.WriteLine(promt);
            string? input = Console.ReadLine();
            int x = 0;

            while (true)
            {
                if (int.TryParse(input, out x) && x > 0)
                {
                    return x;
                }
                else
                {
                    Console.WriteLine(
                        "Ошибка! Введите число больше нуля:");
                    input = Console.ReadLine();
                }
            }
        }

        public double ReadDouble(string promt)
        {
            Console.WriteLine(promt);
            string? input = Console.ReadLine();
            double x = 0;

            while (true)
            {
                if (double.TryParse(
                    input,
                    NumberStyles.Float,
                    CultureInfo.CurrentCulture,
                    out x))
                {
                    return x;
                }

                if (double.TryParse(
                    input?.Replace(',', '.'),
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out x))
                {
                    return x;
                }

                Console.WriteLine(
                    "Ошибка! Введите число ещё раз:");
                input = Console.ReadLine();
            }
        }

        public string ReadString(string promt)
        {
            Console.WriteLine(promt);
            string? input = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(
                    "Ошибка! Строка не должна быть пустой:");
                input = Console.ReadLine();
            }

            return input;
        }
    }
}