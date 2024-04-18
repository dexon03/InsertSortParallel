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
        TestForStrings();
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
        
        for (int i = 0; i < arrLength; i++)
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
    //
    // private const int Threshold = 16;
    //
    // public static void ParallelInsertionSort(int[] array)
    // {
    //     ParallelInsertionSortInternal(array, 0, array.Length - 1);
    // }
    //
    // private static void ParallelInsertionSortInternal(int[] array, int left, int right)
    // {
    //     if (right - left + 1 <= Threshold)
    //     {
    //         InsertionSort(array, left, right);
    //     }
    //     else
    //     {
    //         int mid = (left + right) / 2;
    //         Task leftTask = Task.Run(() => ParallelInsertionSortInternal(array, left, mid));
    //         Task rightTask = Task.Run(() => ParallelInsertionSortInternal(array, mid + 1, right));
    //         Task.WaitAll(leftTask, rightTask);
    //         Merge(array, left, mid, right);
    //     }
    // }
    //
    // public static void InsertionSort(int[] array, int left, int right)
    // {
    //     for (int i = left + 1; i <= right; i++)
    //     {
    //         int key = array[i];
    //         int j = i - 1;
    //         while (j >= left && array[j] > key)
    //         {
    //             array[j + 1] = array[j];
    //             j--;
    //         }
    //         array[j + 1] = key;
    //     }
    // }
    //
    // private static void Merge(int[] array, int left, int mid, int right)
    // {
    //     int[] leftArray = new int[mid - left + 1];
    //     int[] rightArray = new int[right - mid];
    //
    //     Array.Copy(array, left, leftArray, 0, mid - left + 1);
    //     Array.Copy(array, mid + 1, rightArray, 0, right - mid);
    //
    //     int i = 0;
    //     int j = 0;
    //     int k = left;
    //
    //     while (i < leftArray.Length && j < rightArray.Length)
    //     {
    //         if (leftArray[i] <= rightArray[j])
    //         {
    //             array[k] = leftArray[i];
    //             i++;
    //         }
    //         else
    //         {
    //             array[k] = rightArray[j];
    //             j++;
    //         }
    //         k++;
    //     }
    //
    //     while (i < leftArray.Length)
    //     {
    //         array[k] = leftArray[i];
    //         i++;
    //         k++;
    //     }
    //
    //     while (j < rightArray.Length)
    //     {
    //         array[k] = rightArray[j];
    //         j++;
    //         k++;
    //     }
    // }
    //
    // private static bool IsSorted(int[] array, int left, int right)
    // {
    //     for (int i = left + 1; i <= right; i++)
    //     {
    //         if (array[i - 1] > array[i])
    //             return false;
    //     }
    //     return true;
    // }
}