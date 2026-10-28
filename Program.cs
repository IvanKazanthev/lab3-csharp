using System;

namespace Lab3
{
    internal class Program
    {
        private static Checked check = new Checked();

        static void Main(string[] args)
        {
            Arrays.Task1();
            Arrays.Task2();
            Arrays.Task3();

            ShowTask4();
            ShowTask5();
            ShowTask6();
            ShowTask7();
            ShowTask8();
        }

        private static void ShowTask4()
        {
            Console.WriteLine();
            Console.WriteLine("Задание 4");

            int count = check.NaturalReadInt(
                "Сколько чисел записать в файл?");

            FileTasks.FillNumbers(count);

            int[] original =
                FileTasks.ReadBinaryNumbers("numbers.bin");

            Console.WriteLine(
                "\nИсходные числа из numbers.bin:");
            Console.WriteLine(string.Join(" ", original));

            int[] result = FileTasks.Task4();

            Console.WriteLine("\nЧисла без повторений:");
            Console.WriteLine(string.Join(" ", result));

            int[] saved =
                FileTasks.ReadBinaryNumbers(
                    "numbers_result.bin");

            Console.WriteLine(
                "\nПроверка numbers_result.bin:");
            Console.WriteLine(string.Join(" ", saved));

            Console.WriteLine(
                "Количество уникальных чисел: "
                + saved.Length);
        }

        private static void ShowTask5()
        {
            Console.WriteLine();
            Console.WriteLine("Задание 5");

            int count = check.NaturalReadInt(
                "Сколько игрушек добавить?");

            Toy[] toys = new Toy[count];

            for (int i = 0; i < toys.Length; i++)
            {
                Console.WriteLine();
                Console.WriteLine("Игрушка " + (i + 1));

                toys[i].Name = check.ReadString(
                    "Введите название:");

                toys[i].Price = check.ReadDouble(
                    "Введите стоимость:");

                toys[i].MinAge = check.ReadInt(
                    "Введите минимальный возраст:");

                toys[i].MaxAge = check.ReadInt(
                    "Введите максимальный возраст:");
            }

            double k = check.ReadDouble(
                "Введите максимальную стоимость k:");

            FileTasks.FillToys(toys);

            Console.WriteLine("\nИсходные игрушки:");

            for (int i = 0; i < toys.Length; i++)
            {
                Console.WriteLine(
                    toys[i].Name
                    + " | Цена: " + toys[i].Price
                    + " | Возраст: " + toys[i].MinAge
                    + "-" + toys[i].MaxAge);
            }

            Toy[] result = FileTasks.Task5(k);

            Console.WriteLine(
                "\nПодходящие игрушки (цена <= "
                + k + ", возраст 5 лет):");

            if (result.Length == 0)
            {
                Console.WriteLine(
                    "Подходящих игрушек нет.");
            }
            else
            {
                for (int i = 0; i < result.Length; i++)
                {
                    Console.WriteLine(
                        result[i].Name
                        + " | Цена: " + result[i].Price
                        + " | Возраст: " + result[i].MinAge
                        + "-" + result[i].MaxAge);
                }
            }

            Console.WriteLine(
                "Данные сохранены в toys.bin.");
            Console.WriteLine("XML-файл: toys.xml.");
        }

        private static void ShowTask6()
        {
            Console.WriteLine();
            Console.WriteLine("Задание 6");

            int count = check.NaturalReadInt(
                "Сколько чисел записать в файл?");

            FileTasks.FillTextNumbers(count);

            Console.WriteLine("\nСодержимое data.txt:");
            Console.WriteLine(
                FileTasks.ReadFileText("data.txt"));

            int b = check.ReadInt(
                "Какое число нужно найти?");

            bool found = FileTasks.Task6(b);

            Console.WriteLine("Искомое число b = " + b);
            Console.WriteLine(
                "Результат: "
                + (found ? "true" : "false"));
        }

        private static void ShowTask7()
        {
            Console.WriteLine();
            Console.WriteLine("Задание 7");

            int count = check.NaturalReadInt(
                "Сколько чисел записать в файл?");

            FileTasks.FillTextNumbersMany(count);

            Console.WriteLine("\nСодержимое data7.txt:");
            Console.WriteLine(
                FileTasks.ReadFileText("data7.txt"));

            int k = check.ReadInt("Введите k:");

            while (k == 0)
            {
                Console.WriteLine(
                    "k не должно быть равно нулю.");

                k = check.ReadInt("Введите k:");
            }

            string text =
                FileTasks.ReadFileText("data7.txt");

            string[] parts = text.Split(
                new char[] { ' ', '\r', '\n', '\t' },
                StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine(
                "\nЧисла, кратные " + k + ":");

            bool any = false;

            for (int i = 0; i < parts.Length; i++)
            {
                int number;

                if (int.TryParse(parts[i], out number)
                    && number % k == 0)
                {
                    Console.Write(number + " ");
                    any = true;
                }
            }

            if (!any)
            {
                Console.Write("Таких чисел нет.");
            }

            Console.WriteLine();

            int sum = FileTasks.Task7(k);

            Console.WriteLine(
                "Сумма чисел, кратных " + k + ": "
                + sum);
        }

        private static void ShowTask8()
        {
            Console.WriteLine();
            Console.WriteLine("Задание 8");

            Console.WriteLine(
                "Введите строки. Пустая строка"
                + " завершает ввод.");

            FileTasks.FillText("text.txt");

            Console.WriteLine(
                "\nИсходное содержимое text.txt:");

            string original =
                FileTasks.ReadFileText("text.txt");

            Console.WriteLine(original);

            FileTasks.Task8(
                "text.txt",
                "text_result.txt");

            Console.WriteLine(
                "\nСтроки без цифр из text_result.txt:");

            string result =
                FileTasks.ReadFileText("text_result.txt");

            if (string.IsNullOrEmpty(result))
            {
                Console.WriteLine(
                    "Подходящих строк нет.");
            }
            else
            {
                Console.Write(result);
            }

            Console.WriteLine(
                "\nРезультат сохранён в text_result.txt.");
        }
    }
}