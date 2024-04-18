namespace InsertSortParallel;

public class InsertionSort
{
    private const int Threshold = 50;
    public static void IterativeSort(int[] array)
    {
        IterativeSortInternal(array, 0, array.Length - 1);
    }
    public static void ParallelSort(int[] array)
    {
        if (IsSorted(array, 0, array.Length - 1))
            return;

        ParallelInsertionSortInternal(array, 0, array.Length - 1);
    }

    private static void ParallelInsertionSortInternal(int[] array, int left, int right)
    {
        if (right - left + 1 <= Threshold)
        {
            if (!IsSorted(array, left, right))
                IterativeSort(array, left, right);
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
    
    
    private static void IterativeSortInternal(int[] array, int left, int right)
        {
            if (right - left + 1 <= Threshold)
            {
                IterativeSort(array, left, right);
            }
            else
            {
                int mid = (left + right) / 2;
                IterativeSortInternal(array, left, mid);
                IterativeSortInternal(array, mid + 1, right);
                Merge(array, left, mid, right);
            }
        }

    private static void IterativeSort(int[] array, int left, int right)
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

    private static bool IsSorted(int[] array, int left, int right)
    {
        for (int i = left + 1; i <= right; i++)
        {
            if (array[i - 1] > array[i])
                return false;
        }
        return true;
    }

    private static void Merge(int[] array, int left, int mid, int right)
    {
        if (IsSorted(array, left, right))
            return;

        int[] leftArray = new int[mid - left + 1];
        int[] rightArray = new int[right - mid];

        Array.Copy(array, left, leftArray, 0, mid - left + 1);
        Array.Copy(array, mid + 1, rightArray, 0, right - mid);

        int i = 0, j = 0, k = left;

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