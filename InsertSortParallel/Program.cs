
int[] arr = new int[5] { 8, 5, 7, 3, 1 };

InsertionSort(arr);
foreach (var ele in arr)
{
    Console.WriteLine(ele);
}



void InsertionSort(int[] arr)
{
    if (IsSorted(arr))
    {
        return;
    }
    for (int i = 1; i < arr.Length; i++)
    {
        int j = i - 1;
        int key = arr[i];
        while (j >= 0 && arr[j] > key)
        {
            arr[j + 1] = arr[j];
            j--;
        }
        arr[j + 1] = key;
    }
}

bool IsSorted(int[] arr)
{
    for (int i = 1; i < arr.Length; i++)
    {
        if (arr[i] < arr[i - 1])
        {
            return false;
        }
    }
    return true;
}