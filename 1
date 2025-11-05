using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

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

    public override string ToString()
    {
        return $"{Year:D4}-{Month:D2}-{Day:D2}";
    }

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

    public int CompareTo(FullName other)
    {
        int lastNameComparison = string.Compare(LastName, other.LastName, StringComparison.Ordinal);
        if (lastNameComparison != 0) return -lastNameComparison;

        int firstNameComparison = string.Compare(FirstName, other.FirstName, StringComparison.Ordinal);
        if (firstNameComparison != 0) return -firstNameComparison;

        return -string.Compare(MiddleName, other.MiddleName, StringComparison.Ordinal);
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

    public override string ToString()
    {
        return $"{Date}\t{Name}\t{OriginalLineNumber}";
    }

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
        int n = 1000000;

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

            // Создание копий для разных алгоритмов сортировки
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

            // Анализ производительности
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

    // Оптимизированное чтение данных из файла
    public static DataRecord[] ReadData(string filename, int n)
    {
        List<DataRecord> records = new List<DataRecord>(n); // Предварительное выделение памяти
        int lineNumber = 1;

        try
        {
            using (var sr = new StreamReader(filename))
            {
                string line;

                while ((line = sr.ReadLine()) != null && records.Count < n)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        lineNumber++;
                        continue;
                    }

                    // Быстрый парсинг с использованием Span
                    ReadOnlySpan<char> lineSpan = line.AsSpan();
                    int firstTab = lineSpan.IndexOf('\t');

                    if (firstTab == -1)
                    {
                        lineNumber++;
                        continue;
                    }

                    // Парсинг даты
                    ReadOnlySpan<char> dateSpan = lineSpan.Slice(0, firstTab);
                    int firstDash = dateSpan.IndexOf('-');
                    int secondDash = dateSpan.Slice(firstDash + 1).IndexOf('-') + firstDash + 1;

                    if (firstDash == -1 || secondDash == -1)
                    {
                        lineNumber++;
                        continue;
                    }

                    // Быстрый парсинг чисел даты
                    if (int.TryParse(dateSpan.Slice(0, firstDash), out int year) &&
                        int.TryParse(dateSpan.Slice(firstDash + 1, secondDash - firstDash - 1), out int month) &&
                        int.TryParse(dateSpan.Slice(secondDash + 1), out int day))
                    {
                        Date date = new Date(day, month, year);

                        // Парсинг ФИО
                        ReadOnlySpan<char> nameSpan = lineSpan.Slice(firstTab + 1);
                        int firstSpace = nameSpan.IndexOf(' ');
                        int secondSpace = nameSpan.Slice(firstSpace + 1).IndexOf(' ') + firstSpace + 1;

                        if (firstSpace != -1 && secondSpace != -1)
                        {
                            string lastName = nameSpan.Slice(0, firstSpace).ToString();
                            string firstName = nameSpan.Slice(firstSpace + 1, secondSpace - firstSpace - 1).ToString();
                            string middleName = nameSpan.Slice(secondSpace + 1).ToString();

                            FullName name = new FullName(lastName, firstName, middleName);
                            records.Add(new DataRecord(date, name, lineNumber));
                        }
                    }

                    lineNumber++;

                    // Более редкий вывод прогресса
                    if (records.Count % 100000 == 0)
                    {
                        Console.WriteLine($"Прочитано записей: {records.Count}");
                    }
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

    // Оптимизированный алгоритм пирамидальной сортировки
    public static void PyramidalSort(DataRecord[] data, int n)
    {
        // Построение max-heap
        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(data, n, i);

        // Последовательное извлечение элементов из пирамиды
        for (int i = n - 1; i > 0; i--)
        {
            Swap(data, 0, i);
            Heapify(data, i, 0);
        }
    }

    // Неинтерактивная версия Heapify для скорости
    private static void Heapify(DataRecord[] data, int n, int i)
    {
        DataRecord temp = data[i];
        int current = i;

        while (true)
        {
            int left = 2 * current + 1;
            int right = 2 * current + 2;
            int largest = current;

            if (left < n && data[left].CompareTo(data[largest]) > 0)
                largest = left;
            if (right < n && data[right].CompareTo(data[largest]) > 0)
                largest = right;

            if (largest == current) break;

            data[current] = data[largest];
            data[largest] = temp;
            current = largest;
        }
    }

    // Оптимизированная процедура сортировки двухпутевыми вставками
    public static long TwoWayInsertionSortProcedure(DataRecord[] data, string outputFile)
    {
        if (data.Length > 50000)
        {
            Console.WriteLine($"Внимание: сортировка {data.Length} записей двухпутевыми вставками может занять значительное время");
            Console.WriteLine("Рекомендуется использовать пирамидальную сортировку для больших объемов данных");
        }

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {
            OptimizedTwoWayInsertionSort(data, data.Length);
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

    // Оптимизированный алгоритм сортировки двухпутевыми вставками
    public static void OptimizedTwoWayInsertionSort(DataRecord[] data, int n)
    {
        if (n <= 1) return;

        // Используем List для более эффективного управления памятью
        List<DataRecord> tempList = new List<DataRecord>(n * 2);

        // Инициализируем список null значениями
        for (int i = 0; i < n * 2; i++)
        {
            tempList.Add(null);
        }

        int left = n;
        int right = n;
        tempList[left] = data[0];

        for (int i = 1; i < n; i++)
        {
            DataRecord current = data[i];

            // Вставка в начало
            if (current.CompareTo(tempList[left]) < 0)
            {
                left--;
                tempList[left] = current;
            }
            // Вставка в конец
            else if (current.CompareTo(tempList[right]) > 0)
            {
                right++;
                tempList[right] = current;
            }
            // Вставка в середину
            else
            {
                // Бинарный поиск для нахождения позиции вставки
                int insertPos = BinarySearchPosition(tempList, left, right, current);

                // Сдвиг элементов вправо
                for (int j = right; j >= insertPos; j--)
                {
                    tempList[j + 1] = tempList[j];
                }
                tempList[insertPos] = current;
                right++;
            }

            // Прогресс обработки для больших массивов
            if (n > 10000 && i % 50000 == 0)
            {
                Console.WriteLine($"Обработано записей двухпутевыми вставками: {i}/{n}");
            }
        }

        // Копирование обратно в исходный массив
        for (int i = 0; i < n; i++)
        {
            data[i] = tempList[left + i];
        }
    }

    // Бинарный поиск для оптимизации вставки
    private static int BinarySearchPosition(List<DataRecord> list, int left, int right, DataRecord target)
    {
        int low = left;
        int high = right;

        while (low <= high)
        {
            int mid = low + (high - low) / 2;
            int comparison = target.CompareTo(list[mid]);

            if (comparison == 0)
                return mid;
            else if (comparison < 0)
                high = mid - 1;
            else
                low = mid + 1;
        }

        return low;
    }

    // Вспомогательный метод для обмена элементов массива
    private static void Swap(DataRecord[] data, int i, int j)
    {
        DataRecord temp = data[i];
        data[i] = data[j];
        data[j] = temp;
    }

    // Оптимизированная запись результатов
    private static void WriteOutput(DataRecord[] data, string filename, long sortingTime)
    {
        try
        {
            string outputDirectory = @"C:\Otvet";
            Directory.CreateDirectory(outputDirectory);

            string fullPath = Path.Combine(outputDirectory, filename);

            using (var writer = new StreamWriter(fullPath, false, Encoding.UTF8, 65536)) // Буфер 64KB
            {
                // Буферизованная запись
                foreach (var record in data)
                {
                    writer.WriteLine($"{record.Date}\t{record.Name}\t{record.OriginalLineNumber}");
                }
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

        bool pyramidalSortStable = IsSortStable(original, pyramidalSorted);
        Console.WriteLine($"Пирамидальная сортировка: {(pyramidalSortStable ? "УСТОЙЧИВА" : "НЕУСТОЙЧИВА")}");

        bool twoWayStable = IsSortStable(original, twoWaySorted);
        Console.WriteLine($"Двухпутевые вставки: {(twoWayStable ? "УСТОЙЧИВА" : "НЕУСТОЙЧИВА")}");

        if (!pyramidalSortStable)
        {
            DemonstratePyramidalSortInstability();
        }
    }

    // Проверка устойчивости сортировки
    private static bool IsSortStable(DataRecord[] original, DataRecord[] sorted)
    {
        var groups = original
            .Select((r, i) => new { Record = r, Index = i })
            .GroupBy(x => $"{x.Record.Date}|{x.Record.Name}");

        foreach (var group in groups.Where(g => g.Count() > 1))
        {
            var originalOrder = group.Select(x => x.Index).ToList();
            var sortedOrder = new List<int>();

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

            if (originalOrder.Count == sortedOrder.Count && !originalOrder.SequenceEqual(sortedOrder))
                return false;
        }

        return true;
    }

    // Демонстрация неустойчивости пирамидальной сортировки
    private static void DemonstratePyramidalSortInstability()
    {
        Console.WriteLine("\nДемонстрация неустойчивости пирамидальной сортировки:");

        Date testDate = new Date(1, 1, 2023);
        DataRecord[] testData = new DataRecord[]
        {
            new DataRecord(testDate, new FullName("Иванов", "Алексей", "Петрович"), 1),
            new DataRecord(testDate, new FullName("Петров", "Иван", "Сергеевич"), 2),
            new DataRecord(testDate, new FullName("Иванов", "Алексей", "Петрович"), 3),
            new DataRecord(testDate, new FullName("Сидоров", "Михаил", "Александрович"), 4)
        };

        Console.WriteLine("Исходный порядок:");
        foreach (var record in testData)
        {
            Console.WriteLine($"  {record}");
        }

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

            if (pyramidalSortTime < twoWayInsertionTime)
            {
                Console.WriteLine($"Пирамидальная сортировка быстрее на {twoWayInsertionTime - pyramidalSortTime} мс");
                Console.WriteLine($"Отношение скорости: {(double)twoWayInsertionTime / pyramidalSortTime:F2}x");
            }
            else
            {
                Console.WriteLine($"Двухпутевые вставки быстрее на {pyramidalSortTime - twoWayInsertionTime} мс");
                Console.WriteLine($"Отношение скорости: {(double)pyramidalSortTime / twoWayInsertionTime:F2}x");
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

        // Ограничение размера для двухпутевых вставок
        int testSize = Math.Min(sampleData.Length, 1000);
        DataRecord[] testData = new DataRecord[testSize];
        Array.Copy(sampleData, testData, testSize);

        DataRecord[] bestCase = testData.OrderBy(x => x).ToArray();
        DataRecord[] worstCase = testData.OrderByDescending(x => x).ToArray();

        TestPerformanceOnCase(bestCase, "Наилучший случай (отсортированные данные)");
        TestPerformanceOnCase(worstCase, "Наихудший случай (обратно отсортированные данные)");
        TestPerformanceOnCase(testData, "Случайные данные");
    }

    // Тестирование производительности на конкретном наборе данных
    private static void TestPerformanceOnCase(DataRecord[] data, string caseName)
    {
        DataRecord[] pyramidalSortData = new DataRecord[data.Length];
        Array.Copy(data, pyramidalSortData, data.Length);

        Stopwatch stopwatch = Stopwatch.StartNew();
        PyramidalSort(pyramidalSortData, pyramidalSortData.Length);
        stopwatch.Stop();
        long pyramidalSortTime = stopwatch.ElapsedMilliseconds;

        DataRecord[] twoWayData = new DataRecord[data.Length];
        Array.Copy(data, twoWayData, data.Length);

        stopwatch.Restart();
        OptimizedTwoWayInsertionSort(twoWayData, twoWayData.Length);
        stopwatch.Stop();
        long twoWayTime = stopwatch.ElapsedMilliseconds;

        Console.WriteLine($"{caseName}:");
        Console.WriteLine($"  Пирамидальная: {pyramidalSortTime} мс");
        Console.WriteLine($"  Двухпутевые: {twoWayTime} мс");
    }
}
