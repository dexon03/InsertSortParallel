namespace InsertSortParallel;

public class InsertionSort
{
    private static int Threshold = 1000;
    public static void IterativeSort<T>(T[] array) where T : IComparable<T>
    {
        IterativeSortInternal(array, 0, array.Length - 1);
    }

    public static void ParallelSort<T>(T[] array) where T : IComparable<T>
    {
        if (IsSorted(array, 0, array.Length - 1))
            return;

        ParallelInsertionSortInternal(array, 0, array.Length - 1);
    }

    private static void ParallelInsertionSortInternal<T>(T[] array, int left, int right) where T : IComparable<T>
    {
        if (right - left + 1 <= Threshold)
        {
            if (!IsSorted(array, left, right))
                IterativeSort(array, left, right);
        }
        else
        {
            int mid = (left + right) / 2;
            Parallel.Invoke(
                () => ParallelInsertionSortInternal(array, left, mid),
                () => ParallelInsertionSortInternal(array, mid + 1, right)
                );
            Merge(array, left, mid, right);
        }
    }

    private static void IterativeSortInternal<T>(T[] array, int left, int right) where T : IComparable<T>
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

    private static void IterativeSort<T>(T[] array, int left, int right) where T : IComparable<T>
    {
        for (int i = left + 1; i <= right; i++)
        {
            T key = array[i];
            int j = i - 1;
            while (j >= left && array[j].CompareTo(key) > 0)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }

    private static bool IsSorted<T>(T[] array, int left, int right) where T : IComparable<T>
    {
        for (int i = left + 1; i <= right; i++)
        {
            if (array[i - 1].CompareTo(array[i]) > 0)
                return false;
        }
        return true;
    }

    private static void Merge<T>(T[] array, int left, int mid, int right) where T : IComparable<T>
    {
        T[] leftArray = new T[mid - left + 1];
        T[] rightArray = new T[right - mid];

        Array.Copy(array, left, leftArray, 0, mid - left + 1);
        Array.Copy(array, mid + 1, rightArray, 0, right - mid);

        int i = 0, j = 0, k = left;

        while (i < leftArray.Length && j < rightArray.Length)
        {
            if (leftArray[i].CompareTo(rightArray[j]) <= 0)
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