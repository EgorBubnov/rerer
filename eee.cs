using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

// Структура для представления даты
public struct Date : IEquatable<Date>, IComparable<Date>
{
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

    public Date(int day, int month, int year)
    {
        Day = day;
        Month = month;
        Year = year;
    }

    // Формат вывода соответствует входному формату файла
    public override string ToString()
    {
        return $"{Year:D4}-{Month:D2}-{Day:D2}";
    }

    // Реализация интерфейсов для сравнения и равенства
    public bool Equals(Date other)
    {
        return Day == other.Day && Month == other.Month && Year == other.Year;
    }

    public override bool Equals(object obj)
    {
        return obj is Date other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Day, Month, Year);
    }

    // Сравнение дат для сортировки по возрастанию
    public int CompareTo(Date other)
    {
        if (Year != other.Year) return Year.CompareTo(other.Year);
        if (Month != other.Month) return Month.CompareTo(other.Month);
        return Day.CompareTo(other.Day);
    }
}

// Структура для представления ФИО
public struct FullName : IEquatable<FullName>, IComparable<FullName>
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }

    public FullName(string lastName, string firstName, string middleName)
    {
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
    }

    public override string ToString()
    {
        return $"{LastName} {FirstName} {MiddleName}";
    }

    // Реализация интерфейсов для сравнения
    public bool Equals(FullName other)
    {
        return LastName == other.LastName && FirstName == other.FirstName && MiddleName == other.MiddleName;
    }

    public override bool Equals(object obj)
    {
        return obj is FullName other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(LastName, FirstName, MiddleName);
    }

    // Сравнение ФИО для сортировки по убыванию
    public int CompareTo(FullName other)
    {
        int lastNameComparison = string.Compare(LastName, other.LastName, StringComparison.Ordinal);
        if (lastNameComparison != 0) return -lastNameComparison; // Отрицание для убывания

        int firstNameComparison = string.Compare(FirstName, other.FirstName, StringComparison.Ordinal);
        if (firstNameComparison != 0) return -firstNameComparison; // Отрицание для убывания

        return -string.Compare(MiddleName, other.MiddleName, StringComparison.Ordinal); // Отрицание для убывания
    }
}

// Основной класс для хранения данных записи
public class DataRecord : IComparable<DataRecord>
{
    public Date Date { get; set; }
    public FullName Name { get; set; }
    public int OriginalLineNumber { get; set; }

    public DataRecord(Date date, FullName name, int lineNumber)
    {
        Date = date;
        Name = name;
        OriginalLineNumber = lineNumber;
    }

    // Формат вывода: дата ФИО номер строки через табуляцию
    public override string ToString()
    {
        return $"{Date}\t{Name}\t{OriginalLineNumber}";
    }

    // Основной метод сравнения для сортировки: дата по возрастанию ФИО по убыванию
    public int CompareTo(DataRecord other)
    {
        int dateComparison = Date.CompareTo(other.Date);
        if (dateComparison != 0)
            return dateComparison;

        return Name.CompareTo(other.Name);
    }
}

// Главный класс программы
public class Program
{

    public static void Main(string[] args)
    {
        string inputFile = @"C:\output.txt";
        int n = 100000;

        try
        {
          
            Console.WriteLine($"Чтение данных из файла: {inputFile}");
            DataRecord[] data = ReadData(inputFile, n);

            if (data.Length == 0)
            {
                Console.WriteLine("Нет данных для обработки. Программа завершена.");
                return;
            }

            Console.WriteLine($"Прочитано {data.Length} записей");

            
            DataRecord[] dataForPyramidalSort = new DataRecord[data.Length];
            DataRecord[] dataForTwoWayInsertion = new DataRecord[data.Length];
            Array.Copy(data, dataForPyramidalSort, data.Length);
            Array.Copy(data, dataForTwoWayInsertion, data.Length);

          
            Console.WriteLine("\nЗапуск пирамидальной сортировки...");
            string pyramidalSortOutput = "pyramidal_sort_output.txt";
            long pyramidalSortTime = PyramidalSortProcedure(dataForPyramidalSort, pyramidalSortOutput);

    
            Console.WriteLine("\nЗапуск сортировки двухпутевыми вставками...");
            string twoWayInsertionOutput = "two_way_insertion_output.txt";
            long twoWayInsertionTime = TwoWayInsertionSortProcedure(dataForTwoWayInsertion, twoWayInsertionOutput);

            // Анализ устойчивости сортировок
            if (twoWayInsertionTime != -1)
            {
                AnalyzeStability(data, dataForPyramidalSort, dataForTwoWayInsertion);
            }

            // Сравнение времени выполнения алгоритмов
            CompareSortingTimes(pyramidalSortTime, twoWayInsertionTime);

            // результаты 
            AnalyzeBestWorstCases(data);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("\nПрограмма завершена.");
        }
    }

    // Чтение данных из файла
    public static DataRecord[] ReadData(string filename, int n)
    {
        List<DataRecord> records = new List<DataRecord>();
        int lineNumber = 1;

        try
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                line = sr.ReadLine();

                // Чтение строк до конца файла или достижения лимита n
                while (line != null && records.Count < n)
                {
                    string[] parts = line.Split('\t');
                    if (parts.Length >= 2)
                    {
                        // Парсинг даты
                        string[] dateParts = parts[0].Split('-');
                        Date date = new Date(
                            int.Parse(dateParts[2]), // День
                            int.Parse(dateParts[1]), // Месяц
                            int.Parse(dateParts[0])  // Год
                        );

                        // Парсинг ФИО
                        string[] nameParts = parts[1].Split(' ');
                        FullName name = new FullName(
                            nameParts[0], // Фамилия
                            nameParts[1], // Имя
                            nameParts[2]  // Отчество
                        );

                        records.Add(new DataRecord(date, name, lineNumber));

                        // Прогресс чтения каждые 1000 записей
                        if (records.Count % 10000 == 0)
                        {
                            Console.WriteLine($"Прочитано записей: {records.Count}");
                        }
                    }

                    lineNumber++;
                    line = sr.ReadLine();
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Файл {filename} не существует.");
            return new DataRecord[0];
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
            return new DataRecord[0];
        }

        Console.WriteLine($"Всего обработано записей: {records.Count}");
        return records.ToArray();
    }

    // Процедура пирамидальной сортировки с замером времени
    public static long PyramidalSortProcedure(DataRecord[] data, string outputFile)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        PyramidalSort(data, data.Length);

        stopwatch.Stop();
        long elapsedTime = stopwatch.ElapsedMilliseconds;

        WriteOutput(data, outputFile, elapsedTime);

        return elapsedTime;
    }

    // Алгоритм пирамидальной сортировки 
    public static void PyramidalSort(DataRecord[] data, int n)
    {
        // Макс хеп
        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(data, n, i);

        // Последовательное извлечение элементов из пирамиды
        for (int i = n - 1; i > 0; i--)
        {
            Swap(data, 0, i);    // Перемещаем корень в конец
            Heapify(data, i, 0); // Восстанавливаем пирамиду для оставшихся элементов
        }
    }

    // Восстановление свойства пирамиды для поддерева с корнем i
    private static void Heapify(DataRecord[] data, int n, int i)
    {
        int largest = i;        // Инициализируем наибольший элемент как корень
        int left = 2 * i + 1;   // Левый дочерний элемент
        int right = 2 * i + 2;  // Правый дочерний элемент

        // Если левый дочерний элемент больше корня
        if (left < n && data[left].CompareTo(data[largest]) > 0)
            largest = left;

        // Если правый дочерний элемент больше текущего наибольшего
        if (right < n && data[right].CompareTo(data[largest]) > 0)
            largest = right;

        // Если наибольший элемент не корень
        if (largest != i)
        {
            Swap(data, i, largest);
            Heapify(data, n, largest); // Рекурсивно преобразуем затронутое поддерево
        }
    }

    // Процедура сортировки двухпутевыми вставками с замером времени
    public static long TwoWayInsertionSortProcedure(DataRecord[] data, string outputFile)
    {
        
        //if (data.Length > 10000)
        //{
            //Console.WriteLine($"Внимание: сортировка {data.Length} записей двухпутевыми вставками может занять много времени");
        //}

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {
            TwoWayInsertionSort(data, data.Length);
            stopwatch.Stop();
            long elapsedTime = stopwatch.ElapsedMilliseconds;

            WriteOutput(data, outputFile, elapsedTime);
            return elapsedTime;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            Console.WriteLine($"Ошибка при сортировке двухпутевыми вставками: {ex.Message}");
            Console.WriteLine("Сортировка двухпутевыми вставками прервана.");
            return -1;
        }
    }

    // Алгоритм сортировки двухпутевыми вставками
    public static void TwoWayInsertionSort(DataRecord[] data, int n)
    {
        if (n <= 1) return;

        // Временный массив для хранения отсортированных элементов
        DataRecord[] temp = new DataRecord[2 * n + 1];
        int left = n;  // Указатель на начало отсортированной части
        int right = n; // Указатель на конец отсортированной части

        temp[left] = data[0]; // Первый элемент становится началом отсортированной послед

        // Обработка остальных элементов
        for (int i = 1; i < n; i++)
        {
            DataRecord current = data[i];

            // Вставка в начало 
            if (current.CompareTo(temp[left]) < 0)
            {
                left--;
                temp[left] = current;
            }
            // Вставка в конец 
            else if (current.CompareTo(temp[right]) > 0)
            {
                right++;
                temp[right] = current;
            }
            // Вставка в середину со сдвигом
            else
            {
                int j = right;
                while (j >= left && current.CompareTo(temp[j]) < 0)
                {
                    temp[j + 1] = temp[j];
                    j--;
                }
                temp[j + 1] = current;
                right++;
            }

            // Прогресс обработки для больших массивов
            if (n > 1000 && i % 10000 == 0)
            {
                Console.WriteLine($"Обработано записей двухпутевыми вставками: {i}/{n}");
            }
        }

        // Копирование отсортированных данных обратно в исходный массив
        for (int i = 0; i < n; i++)
        {
            data[i] = temp[left + i];
        }
    }

    // Вспомогательный метод для обмена элементов массива
    private static void Swap(DataRecord[] data, int i, int j)
    {
        DataRecord temp = data[i];
        data[i] = data[j];
        data[j] = temp;
    }

    // Запись результатов сортировки в файл
    private static void WriteOutput(DataRecord[] data, string filename, long sortingTime)
    {
        try
        {
            string outputDirectory = @"C:\Otvet";
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
                Console.WriteLine($"Создана папка: {outputDirectory}");
            }

            string fullPath = Path.Combine(outputDirectory, filename);

            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                // Запись отсортированных данных
                foreach (var record in data)
                {
                    writer.WriteLine(record);
                }
                // Запись времени сортировки в последней строке
                writer.WriteLine($"Время сортировки: {sortingTime} мс");
            }
            Console.WriteLine($"Результаты сохранены в файл: {fullPath}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка при записи в файл {filename}: {e.Message}");
        }
    }

    // Анализ устойчивости алгоритмов сортировки
    public static void AnalyzeStability(DataRecord[] original, DataRecord[] pyramidalSorted, DataRecord[] twoWaySorted)
    {
        Console.WriteLine("\n=== АНАЛИЗ УСТОЙЧИВОСТИ ===");

        // Проверка устойчивости пирамидальной сортировки
        bool pyramidalSortStable = IsSortStable(original, pyramidalSorted);
        Console.WriteLine($"Пирамидальная сортировка: {(pyramidalSortStable ? "УСТОЙЧИВА" : "НЕУСТОЙЧИВА")}");

        // Проверка устойчивости двухпутевых вставок
        bool twoWayStable = IsSortStable(original, twoWaySorted);
        Console.WriteLine($"Двухпутевые вставки: {(twoWayStable ? "УСТОЙЧИВА" : "НЕУСТОЙЧИВА")}");

        // Демонстрация неустойчивости пирамидальной сортировки
        if (!pyramidalSortStable)
        {
            DemonstratePyramidalSortInstability();
        }
    }

    // Проверка устойчивости сортировки
    private static bool IsSortStable(DataRecord[] original, DataRecord[] sorted)
    {
        // Группировка записей с одинаковыми ключами сортировки
        var groups = original
            .Select((r, i) => new { Record = r, Index = i })
            .GroupBy(x => $"{x.Record.Date}|{x.Record.Name}");

        // Проверка сохранения порядка в каждой группе
        foreach (var group in groups.Where(g => g.Count() > 1))
        {
            var originalOrder = group.Select(x => x.Index).ToList();
            var sortedOrder = new List<int>();

            // Поиск соответствующих записей в отсортированном массиве
            foreach (var record in group)
            {
                int sortedIndex = Array.FindIndex(sorted, r =>
                    r.Date.Equals(record.Record.Date) &&
                    r.Name.Equals(record.Record.Name) &&
                    r.OriginalLineNumber == record.Record.OriginalLineNumber);

                if (sortedIndex >= 0)
                {
                    sortedOrder.Add(sortedIndex);
                }
            }

            // Если порядок изменился - сортировка неустойчива
            if (originalOrder.Count == sortedOrder.Count && !originalOrder.SequenceEqual(sortedOrder))
                return false;
        }

        return true;
    }

    // Демонстрация неустойчивости пирамидальной сортировки на примере
    private static void DemonstratePyramidalSortInstability()
    {
        Console.WriteLine("\nДемонстрация неустойчивости пирамидальной сортировки:");

        // Создание тестовых данных с одинаковыми ключами
        Date testDate = new Date(1, 1, 2023);
        DataRecord[] testData = new DataRecord[]
        {
            new DataRecord(testDate, new FullName("Иванов", "Алексей", "Петрович"), 1),
            new DataRecord(testDate, new FullName("Петров", "Иван", "Сергеевич"), 2),
            new DataRecord(testDate, new FullName("Иванов", "Алексей", "Петрович"), 3), // Дубликат ключей с записью 1
            new DataRecord(testDate, new FullName("Сидоров", "Михаил", "Александрович"), 4)
        };

        Console.WriteLine("Исходный порядок:");
        foreach (var record in testData)
        {
            Console.WriteLine($"  {record}");
        }

        // Сортировка тестовых данных
        DataRecord[] pyramidalSorted = new DataRecord[testData.Length];
        Array.Copy(testData, pyramidalSorted, testData.Length);
        PyramidalSort(pyramidalSorted, pyramidalSorted.Length);

        Console.WriteLine("После пирамидальной сортировки:");
        foreach (var record in pyramidalSorted)
        {
            Console.WriteLine($"  {record}");
        }

        Console.WriteLine("Заметка: записи с одинаковыми ключами (1 и 3) изменили относительный порядок!");
    }

    // Сравнение времени выполнения алгоритмов сортировки
    public static void CompareSortingTimes(long pyramidalSortTime, long twoWayInsertionTime)
    {
        Console.WriteLine("\n=== СРАВНЕНИЕ ВРЕМЕНИ СОРТИРОВКИ ===");
        Console.WriteLine($"Пирамидальная сортировка: {pyramidalSortTime} мс");

        if (twoWayInsertionTime != -1)
        {
            Console.WriteLine($"Двухпутевые вставки: {twoWayInsertionTime} мс");

            // Определение более быстрого алгоритма
            if (pyramidalSortTime < twoWayInsertionTime)
            {
                Console.WriteLine($"Пирамидальная сортировка быстрее на {twoWayInsertionTime - pyramidalSortTime} мс");
            }
            else
            {
                Console.WriteLine($"Двухпутевые вставки быстрее на {pyramidalSortTime - twoWayInsertionTime} мс");
            }
        }
        else
        {
            Console.WriteLine("Двухпутевые вставки: НЕ ЗАВЕРШЕНА");
        }
    }

    // Анализ производительности в наилучшем и наихудшем случаях
    public static void AnalyzeBestWorstCases(DataRecord[] sampleData)
    {
        Console.WriteLine("\n=== АНАЛИЗ НАИЛУЧШИХ/НАИХУДШИХ СЛУЧАЕВ ===");

        // Ограничение размера тестовых данных для производительности
        int testSize = Math.Min(sampleData.Length, 1000);
        DataRecord[] testData = new DataRecord[testSize];
        Array.Copy(sampleData, testData, testSize);

        // Создание тестовых случаев:
        DataRecord[] bestCase = testData.OrderBy(x => x).ToArray();        // Уже отсортированные данные
        DataRecord[] worstCase = testData.OrderByDescending(x => x).ToArray(); // Обратно отсортированные данные

        // Тестирование производительности на разных случаях
        TestPerformanceOnCase(bestCase, "Наилучший случай (отсортированные данные)");
        TestPerformanceOnCase(worstCase, "Наихудший случай (обратно отсортированные данные)");
        TestPerformanceOnCase(testData, "Случайные данные");
    }

    // Тестирование производительности на конкретном наборе данных
    private static void TestPerformanceOnCase(DataRecord[] data, string caseName)
    {
        // Тестирование пирамидальной сортировки
        DataRecord[] pyramidalSortData = new DataRecord[data.Length];
        Array.Copy(data, pyramidalSortData, data.Length);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        PyramidalSort(pyramidalSortData, pyramidalSortData.Length);
        stopwatch.Stop();
        long pyramidalSortTime = stopwatch.ElapsedMilliseconds;

        // Тестирование двухпутевых вставок (только для небольших наборов)
        long twoWayTime = -1;
        if (data.Length <= 1000)
        {
            DataRecord[] twoWayData = new DataRecord[data.Length];
            Array.Copy(data, twoWayData, data.Length);

            stopwatch.Restart();
            TwoWayInsertionSort(twoWayData, twoWayData.Length);
            stopwatch.Stop();
            twoWayTime = stopwatch.ElapsedMilliseconds;
        }

        // Вывод результатов тестирования
        Console.WriteLine($"{caseName}:");
        Console.WriteLine($"  Пирамидальная: {pyramidalSortTime} мс");
        if (twoWayTime != -1)
        {
            Console.WriteLine($"  Двухпутевые: {twoWayTime} мс");
        }
        else
        {
            Console.WriteLine($"  Двухпутевые: пропущено (слишком много данных для теста)");
        }
    }
}
