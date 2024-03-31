
class Program
{
    static void Main()
    {
        // List<int> arr = new List<int> { 5, 3, 8, 1, 2, 7, 4, 6 };
        // int numThreads = 4;
        // int[] arr = [5, 3, 8, 1, 2, 7, 4, 6];
        // ParallelInsertionSort<int>(arr);
        // foreach (var item in arr)
        // {
        //     Console.Write(item + " ");
        // }
        
    }

    public static void ParallelInsertionSort<T>(T[] array) where T : IComparable<T>
    {
        if (array.Length < 2)
        {
            return;
        }

        // Divide the array into sub-arrays (assuming equal size for simplicity)
        int subArraySize = array.Length / Environment.ProcessorCount;
        List<T[]> subArrays = new List<T[]>();

        for (int i = 0; i < array.Length; i += subArraySize)
        {
            subArrays.Add(array.Skip(i).Take(subArraySize).ToArray());
        }

        // Parallel sorting of sub-arrays (ignoring potential overhead and synchronization concerns)
        Parallel.ForEach(subArrays, subArray => InsertionSort(subArray));

        // Merging using a temporary array and maintaining sorted order
        T[] tempArray = new T[array.Length];
        int mergedIndex = 0;
        int currentSubArrayIndex = 0; // Index of the current sub-array

        while (mergedIndex < tempArray.Length)
        {
            // Find the minimum element among available sub-arrays
            T minElement = subArrays[currentSubArrayIndex][0];
            int minSubArrayIndex = currentSubArrayIndex;

            for (int i = 0; i < subArrays.Count; i++)
            {
                if (currentSubArrayIndex != i && subArrays[i].Length > 0 && subArrays[i][0].CompareTo(minElement) < 0)
                {
                    minElement = subArrays[i][0];
                    minSubArrayIndex = i;
                }
            }

            // Add the minimum element to the result and update sub-array
            tempArray[mergedIndex++] = minElement;
            subArrays[minSubArrayIndex] = subArrays[minSubArrayIndex].Skip(1).ToArray();

            // Move to the next sub-array if it becomes empty
            if (subArrays[minSubArrayIndex].Length == 0)
            {
                currentSubArrayIndex++;
            }
        }

        Array.Copy(tempArray, array, array.Length);
    }

    private static void InsertionSort<T>(T[] array) where T : IComparable<T>
    {
        for (int i = 1; i < array.Length; i++)
        {
            var current = array[i];
            int j = i - 1;

            while (j >= 0 && array[j].CompareTo(current) > 0)
            {
                array[j + 1] = array[j];
                j--;
            }

            array[j + 1] = current;
        }
    }
}