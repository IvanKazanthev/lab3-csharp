using System;
using System.IO;
using System.Xml.Serialization;

namespace Lab3;

internal static class FileTasks
{
    public static void FillNumbers(int count)
    {
        Random random = new Random();

        using (BinaryWriter writer = new BinaryWriter(
            File.Open("numbers.bin", FileMode.Create)))
        {
            writer.Write(count);

            for (int i = 0; i < count; i++)
            {
                writer.Write(random.Next(-50, 51));
            }
        }
    }

    public static int[] Task4()
    {
        int[] numbers;

        using (BinaryReader reader = new BinaryReader(
            File.Open("numbers.bin", FileMode.Open)))
        {
            int count = reader.ReadInt32();
            numbers = new int[count];

            for (int i = 0; i < count; i++)
            {
                numbers[i] = reader.ReadInt32();
            }
        }

        int[] unique = new int[numbers.Length];
        int uniqueCount = 0;

        for (int i = 0; i < numbers.Length; i++)
        {
            bool found = false;

            for (int j = 0; j < uniqueCount; j++)
            {
                if (numbers[i] == unique[j])
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                unique[uniqueCount] = numbers[i];
                uniqueCount++;
            }
        }

        using (BinaryWriter writer = new BinaryWriter(
            File.Open(
                "numbers_result.bin",
                FileMode.Create)))
        {
            writer.Write(uniqueCount);

            for (int i = 0; i < uniqueCount; i++)
            {
                writer.Write(unique[i]);
            }
        }

        int[] result = new int[uniqueCount];

        for (int i = 0; i < uniqueCount; i++)
        {
            result[i] = unique[i];
        }

        return result;
    }

    public static void FillToys(Toy[] toys)
    {
        using (BinaryWriter writer = new BinaryWriter(
            File.Open("toys.bin", FileMode.Create)))
        {
            writer.Write(toys.Length);

            for (int i = 0; i < toys.Length; i++)
            {
                writer.Write(toys[i].Name);
                writer.Write(toys[i].Price);
                writer.Write(toys[i].MinAge);
                writer.Write(toys[i].MaxAge);
            }
        }
    }

    public static Toy[] Task5(double k)
    {
        Toy[] toys;

        using (BinaryReader reader = new BinaryReader(
            File.Open("toys.bin", FileMode.Open)))
        {
            int count = reader.ReadInt32();
            toys = new Toy[count];

            for (int i = 0; i < count; i++)
            {
                toys[i] = new Toy();

                toys[i].Name = reader.ReadString();
                toys[i].Price = reader.ReadDouble();
                toys[i].MinAge = reader.ReadInt32();
                toys[i].MaxAge = reader.ReadInt32();
            }
        }

        XmlSerializer serializer =
            new XmlSerializer(typeof(Toy[]));

        using (FileStream stream = File.Create("toys.xml"))
        {
            serializer.Serialize(stream, toys);
        }

        Toy[] restored;

        using (FileStream stream = File.OpenRead("toys.xml"))
        {
            restored = (Toy[])serializer.Deserialize(stream)!;
        }

        int resultCount = 0;

        for (int i = 0; i < restored.Length; i++)
        {
            bool affordable = restored[i].Price <= k;

            bool suitable = restored[i].MinAge <= 5
                && restored[i].MaxAge >= 5;

            if (affordable && suitable)
            {
                resultCount++;
            }
        }

        Toy[] result = new Toy[resultCount];
        int index = 0;

        for (int i = 0; i < restored.Length; i++)
        {
            bool affordable = restored[i].Price <= k;

            bool suitable = restored[i].MinAge <= 5
                && restored[i].MaxAge >= 5;

            if (affordable && suitable)
            {
                result[index] = restored[i];
                index++;
            }
        }

        return result;
    }

    public static void FillTextNumbers(int count)
    {
        Random random = new Random();

        using (StreamWriter writer = new StreamWriter(
            "data.txt"))
        {
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(random.Next(-100, 101));
            }
        }
    }

    public static bool Task6(int b)
    {
        using (StreamReader reader = new StreamReader(
            "data.txt"))
        {
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                int number;

                if (int.TryParse(line, out number)
                    && number == b)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static void FillTextNumbersMany(int count)
    {
        Random random = new Random();

        using (StreamWriter writer = new StreamWriter(
            "data7.txt"))
        {
            for (int i = 0; i < count; i++)
            {
                writer.Write(random.Next(-100, 101));

                if ((i + 1) % 5 == 0 || i == count - 1)
                {
                    writer.WriteLine();
                }
                else
                {
                    writer.Write(" ");
                }
            }
        }
    }

    public static int Task7(int k)
    {
        int sum = 0;

        using (StreamReader reader = new StreamReader(
            "data7.txt"))
        {
            string text = reader.ReadToEnd();

            string[] parts = text.Split(
                new char[] { ' ', '\r', '\n', '\t' },
                StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < parts.Length; i++)
            {
                int number;

                if (int.TryParse(parts[i], out number)
                    && number % k == 0)
                {
                    sum += number;
                }
            }
        }

        return sum;
    }

    public static void FillText(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(
            fileName))
        {
            while (true)
            {
                string line = Console.ReadLine() ?? "";

                if (line.Length == 0)
                {
                    break;
                }

                writer.WriteLine(line);
            }
        }
    }

    public static void Task8(string source, string dest)
    {
        using (StreamReader reader = new StreamReader(source))
        using (StreamWriter writer = new StreamWriter(dest))
        {
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                bool hasDigit = false;

                for (int i = 0; i < line.Length; i++)
                {
                    if (char.IsDigit(line[i]))
                    {
                        hasDigit = true;
                        break;
                    }
                }

                if (!hasDigit)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }

    public static int[] ReadBinaryNumbers(string fileName)
    {
        using (BinaryReader reader = new BinaryReader(
            File.Open(fileName, FileMode.Open)))
        {
            int count = reader.ReadInt32();
            int[] numbers = new int[count];

            for (int i = 0; i < count; i++)
            {
                numbers[i] = reader.ReadInt32();
            }

            return numbers;
        }
    }

    public static string ReadFileText(string fileName)
    {
        return File.ReadAllText(fileName);
    }
}