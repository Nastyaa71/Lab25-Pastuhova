using System;
using System.Collections.Generic;

int ReadCount()
{
    while (true)
    {
        Console.Write("Сколько результатов сохранить (0–10): ");
        string input = Console.ReadLine() ?? "";

        if (int.TryParse(input, out int count) && count >= 0 && count <= 10)
        {
            return count;
        }

        Console.WriteLine("Введите целое число от 0 до 10");
    }
}

void AddScore(List<int> scores, int score)
{
    if (score < 0 || score > 100)
    {
        throw new ArgumentOutOfRangeException(
            nameof(score), "Результат должен быть от 0 до 100");
    }

    scores.Add(score);
}

int SumScores(List<int> scores)
{
    int total = 0;
    for (int i = 0; i < scores.Count; i++)
    {
        total += scores[i];
    }
    return total;
}


int count = ReadCount();
List<int> scores = new List<int>();
int errors = 0;

while (scores.Count < count)
{
    try
    {
        Console.Write($"Результат {scores.Count + 1}: ");
        int score = int.Parse(Console.ReadLine() ?? "");
        AddScore(scores, score);
        Console.WriteLine("Результат сохранён");
    }
    catch (FormatException)
    {
        errors++;
        Console.WriteLine("Ошибка: введите целое число");
    }
    catch (OverflowException)
    {
        errors++;
        Console.WriteLine("Ошибка: число вне диапазона int");
    }
    catch (ArgumentOutOfRangeException)
    {
        errors++;
        Console.WriteLine("Ошибка: результат должен быть от 0 до 100");
    }
    finally
    {
        Console.WriteLine("Попытка ввода завершена");
    }
}

int sum = SumScores(scores);

Console.WriteLine();
Console.WriteLine(">>> Итоги ввода <<<");
Console.WriteLine();
Console.WriteLine($"Сохранено результатов: {scores.Count}");
Console.WriteLine($"Ошибок ввода результатов: {errors}");

if (scores.Count == 0)
{
    Console.WriteLine("Список пуст");
    Console.WriteLine("Сумма: 0");
    Console.WriteLine("Среднее не вычисляется: список пуст");
}
else
{
    Console.WriteLine("Результаты:");
    for (int i = 0; i < scores.Count; i++)
    {
        Console.WriteLine($"{i}: {scores[i]}");
    }
    Console.WriteLine($"Сумма: {sum}");
    Console.WriteLine($"Среднее: {(double)sum / scores.Count}");
}



double CalculateAverage(List<int> scores)
{
    if (scores.Count == 0)
    {
        throw new InvalidOperationException("Список пуст");
    }

    return (double)SumScores(scores) / scores.Count;
}

Console.WriteLine();
Console.WriteLine(">>> Среднее через CalculateAverage <<<");

try
{
    double average = CalculateAverage(scores);
    Console.WriteLine($"Среднее: {average}");
}
catch (InvalidOperationException)
{
    Console.WriteLine("Среднее не вычисляется: список пуст");
}