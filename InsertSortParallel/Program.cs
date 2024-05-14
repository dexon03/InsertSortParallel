using System.Diagnostics;
using InsertSortParallel;
using StringGenerator;

class Program
{
    private static int arrLength = 1_000_000;
    static void Main()
    {
        // TestInBestCase();
        // TestInWorstCase();
        // TestInAverageCase();
        // TestForStrings();
        // TestIterativeForStrings();
        TestParallelForStrings();
    }

    private static void TestIterativeForStrings()
    {
        int[] arrLengths = { 10000, 100000, 1000000, 5000000 };

        foreach (int arrLength in arrLengths)
        {
            var arr = new string[arrLength];
            var generator = new PseudoRandomStringGenerator();
            for (int i = 0; i < arrLength; i++)
            {
                arr[i] = generator.Next();
            }

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            InsertionSort.IterativeSort(arr);
            stopWatch.Stop();

            Console.WriteLine("Array size: " + arrLength);
            Console.WriteLine("Time taken for iterative sort: " + stopWatch.ElapsedMilliseconds + "ms");
            CheckCorrectResult(arr);
        }
        
    }
    
    private static void TestParallelForStrings()
    {
        int[] arrLengths = { 10000, 100000, 1000000, 5000000 };

        foreach (int arrLength in arrLengths)
        {
            Console.WriteLine("Array size: " + arrLength);
            var arr = new string[arrLength];
            var arr2 = new string[arrLength];
            var generator = new PseudoRandomStringGenerator();
            for (int i = 0; i < arrLength; i++)
            {
                arr[i] = generator.Next();
                arr2[i] = arr[i];
            }
            var stopWatch = new Stopwatch();
            //Parallel
            stopWatch.Start();
            InsertionSort.ParallelSort(arr);
            stopWatch.Stop();
            Console.WriteLine("Time taken for Parallel: " + stopWatch.ElapsedMilliseconds + "ms");
        
            //Iterative
            stopWatch.Start();
            InsertionSort.IterativeSort(arr2);
            stopWatch.Stop();
            Console.WriteLine("Time taken for iterative: " + stopWatch.ElapsedMilliseconds + "ms");
        
        
            CheckCorrectResult(arr, arr2);
        }
        
    }

    private static void TestForStrings()
    {
        var arr = new string[arrLength];
        var arr2 = new string[arrLength];
        var generator = new PseudoRandomStringGenerator();
        for (int i = 0; i < arrLength; i++)
        {
            arr[i] = generator.Next();
            arr2[i] = arr[i];
        }
        
        var stopWatch = new Stopwatch();
        //Parallel
        stopWatch.Start();
        InsertionSort.ParallelSort(arr);
        stopWatch.Stop();
        Console.WriteLine("Time taken for Parallel: " + stopWatch.ElapsedMilliseconds + "ms");
        
        //Iterative
        stopWatch.Start();
        InsertionSort.IterativeSort(arr2);
        stopWatch.Stop();
        Console.WriteLine("Time taken for iterative: " + stopWatch.ElapsedMilliseconds + "ms");
        
        
        CheckCorrectResult(arr, arr2);
    }
    
    private static void TestInBestCase()
    {
        var arr = new int[arrLength];
        var arr2 = new int[arrLength];
        for (int i = 0; i < arrLength; i++)
        {
            arr[i] = i;
            arr2[i] = i;
        }
        var stopWatch = new Stopwatch();
        //Parallel
        stopWatch.Start();
        InsertionSort.ParallelSort(arr);
        stopWatch.Stop();
        Console.WriteLine("Time taken for Parallel: " + stopWatch.ElapsedMilliseconds + "ms");
        
        //Iterative
        stopWatch.Start();
        InsertionSort.IterativeSort(arr2);
        stopWatch.Stop();
        Console.WriteLine("Time taken for iterative: " + stopWatch.ElapsedMilliseconds + "ms");
        
        
        CheckCorrectResult(arr, arr2);
    } 
    
    private static void TestInWorstCase()
    {
        var arr = new int[arrLength];
        var arr2 = new int[arrLength];
        for (int i = 0; i < arrLength; i++)
        {
            arr[i] = arrLength - i;
            arr2[i] = arrLength - i;
        }
        var stopWatch = new Stopwatch();
        //Parallel
        stopWatch.Start();
        InsertionSort.ParallelSort(arr);
        stopWatch.Stop();
        Console.WriteLine("Time taken for Parallel: " + stopWatch.ElapsedMilliseconds + "ms");
        
        //Iterative
        stopWatch.Start();
        InsertionSort.IterativeSort(arr2);
        stopWatch.Stop();
        Console.WriteLine("Time taken for iterative: " + stopWatch.ElapsedMilliseconds + "ms");
        
        
        CheckCorrectResult(arr, arr2);
    }
    
    private static void TestInAverageCase()
    {
        var arr = new int[arrLength];
        var arr2 = new int[arrLength];
        Random randNum = new Random();
        for (int i = 0; i < arrLength; i++)
        {
            arr[i] = randNum.Next(0, 1000);
            arr2[i] = arr[i];
        }
        var stopWatch = new Stopwatch();
        //Parallel
        stopWatch.Start();
        InsertionSort.ParallelSort(arr);
        stopWatch.Stop();
        Console.WriteLine("Time taken for Parallel: " + stopWatch.ElapsedMilliseconds + "ms");
        
        //Iterative
        stopWatch.Start();
        InsertionSort.IterativeSort(arr2);
        stopWatch.Stop();
        Console.WriteLine("Time taken for iterative: " + stopWatch.ElapsedMilliseconds + "ms");
        
        
        CheckCorrectResult(arr, arr2);
    }
    
    
    private static void CheckCorrectResult<T>(T[] arr, T[] arr2) where T : IComparable<T>
    {
        if (arr.Length != arr2.Length)
        {
            Console.WriteLine("Error: Arrays do not have the same length.");
            return;
        }
        
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i].CompareTo(arr2[i]) != 0)
            {
                Console.WriteLine("Error: Arrays do not match.");
                break;
            }
            if (i > 0 && (arr[i].CompareTo(arr[i - 1]) < 0 || arr2[i].CompareTo(arr2[i - 1]) < 0))
            {
                Console.WriteLine("Error: Array is not sorted properly.");
                break;
            }
        }
    }
    
    private static void CheckCorrectResult<T>(T[] arr) where T : IComparable<T>
    {
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i].CompareTo(arr[i - 1]) < 0)
            {
                Console.WriteLine("Error: Array is not sorted properly.");
                break;
            }
        }
    }
}