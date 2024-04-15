using System.Diagnostics;

class Program
{
    static void Main()
    {
        var arrLength = 1_000_000;
        int[] arr = new int[arrLength];
        int[] arr2 = new int[arrLength];
        Random randNum = new Random();
        for (int i = 0; i < arrLength; i++)
        {
            arr[i] = randNum.Next(0, 1000);
        }

        Array.Copy(arr, arr2, arr.Length);

        var stopWatch = new Stopwatch();
        stopWatch.Start();
        ParallelInsertionSort(arr);
        stopWatch.Stop();
        Console.WriteLine("Time taken: " + stopWatch.ElapsedMilliseconds + "ms");
        stopWatch.Start();
        InsertionSort(arr2, 0, arrLength - 1);
        stopWatch.Stop();
        Console.WriteLine("Time taken: " + stopWatch.ElapsedMilliseconds + "ms");
        //check correct result 
        for (int i = 0; i < arrLength; i++)
        {
            if(arr[i] != arr2[i])
            {
                Console.WriteLine("Error");
                break;
            }
            if(i > 0 && (arr[i] < arr[i-1] || arr2[i] < arr[i-1]))
            {
                Console.WriteLine("Error");
                break;
            }
        }
        // var summary = BenchmarkRunner.Run<Benchmark>();
    }
    
    private const int Threshold = 16;

    public static void ParallelInsertionSort(int[] array)
    {
        ParallelInsertionSortInternal(array, 0, array.Length - 1);
    }

    private static void ParallelInsertionSortInternal(int[] array, int left, int right)
    {
        if (right - left + 1 <= Threshold)
        {
            InsertionSort(array, left, right);
        }
        else
        {
            int mid = (left + right) / 2;
            Task leftTask = Task.Run(() => ParallelInsertionSortInternal(array, left, mid));
            Task rightTask = Task.Run(() => ParallelInsertionSortInternal(array, mid + 1, right));
            Task.WaitAll(leftTask, rightTask);
            Merge(array, left, mid, right);
        }
    }

    public static void InsertionSort(int[] array, int left, int right)
    {
        for (int i = left + 1; i <= right; i++)
        {
            int key = array[i];
            int j = i - 1;
            while (j >= left && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }

    private static void Merge(int[] array, int left, int mid, int right)
    {
        int[] leftArray = new int[mid - left + 1];
        int[] rightArray = new int[right - mid];

        Array.Copy(array, left, leftArray, 0, mid - left + 1);
        Array.Copy(array, mid + 1, rightArray, 0, right - mid);

        int i = 0;
        int j = 0;
        int k = left;

        while (i < leftArray.Length && j < rightArray.Length)
        {
            if (leftArray[i] <= rightArray[j])
            {
                array[k] = leftArray[i];
                i++;
            }
            else
            {
                array[k] = rightArray[j];
                j++;
            }
            k++;
        }

        while (i < leftArray.Length)
        {
            array[k] = leftArray[i];
            i++;
            k++;
        }

        while (j < rightArray.Length)
        {
            array[k] = rightArray[j];
            j++;
            k++;
        }
    }
}