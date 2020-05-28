
using System;
using System.IO;

namespace Lab10._1
{
    class Program
    {
        static void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }
        static void MergeSort(int[] array, int l, int r, out TimeSpan interval, ref int comparisons, ref int swaps)
        {
            if (r <= l)
            {
                interval = TimeSpan.FromSeconds(0);
                return;
            }
            DateTime StartTime;
            StartTime = DateTime.Now;

            int mid = (l + r) / 2;
            MergeSort(array, l, mid, out  interval, ref  comparisons, ref swaps);
            MergeSort(array, mid + 1, r, out interval, ref comparisons, ref swaps);
            Merge(array, l, mid, r, out comparisons, out swaps); 

            DateTime EndTime = DateTime.Now;
            interval = EndTime - StartTime;
        }
        static void Merge(int[] array, int l, int mid, int r, out int comparisons, out int swaps)
        {
            comparisons = 0;
            swaps = 0;

            int[] temp = new int[r - l + 1];
            int i = l, j = mid + 1;
            int k = 0;
            for (k = 0; k< temp.Length; k++)
            {
                comparisons++;
                if (i > mid)
                {
                    temp[k] = array[j++];
                    swaps++;
                }
                else if (j > r)
                {
                    swaps++;
                    temp[k] = array[i++];
                }
                else
                {
                    comparisons++;
                    if (array[i] > array[j])
                        temp[k] = array[i++];
                    else
                        temp[k] = array[j++];
                    swaps++;
                }
            }
            k = 0;
            i = l;
            while (k < temp.Length && i <= r)
            {
                array[i++] = temp[k++];
            }
        }
        static void Heapify(int[] array, int i, int N, ref int comparisons, ref int swaps)
        {
           while (2*i + 1 < N)
            {
                comparisons++;
                int k = 2 * i + 1;
                if (2*i + 2 < N && array[2*i + 2] <= array[k])
                {
                    k = 2 * i + 2;
                }
                comparisons++;
                if (array[i] > array[k])
                {
                    Swap(ref array[i], ref array[k]);
                    i = k;
                    swaps++;
                }
                else
                    break;
            }
        }
        static void HeapSort(int[] array, int l, int r, out TimeSpan interval, ref int comparisons, ref int swaps)
        {
            DateTime StartTime;
            StartTime = DateTime.Now;

            int N = r - l + 1;
            comparisons = 1;
            swaps = 0;
            for (int i = r; i >= l; i--)
            {
                Heapify(array, i, N, ref comparisons, ref swaps);
            }
            while(N > 0)
            {
                Swap(ref array[l], ref array[N - 1]);
                swaps++;
                Heapify(array, l, --N, ref comparisons, ref swaps);
            }
            
            DateTime EndTime = DateTime.Now;
            interval = EndTime - StartTime;
        }
        public static int Partition(int[] array, int l, int r, ref int comparisons, ref int swaps)
        {
            int pivot = array[r];
            int i = l - 1, j = r;
            while(i < j)
            {
                while (array[++i] > pivot);
                while (array[--j] < pivot)
                {
                    comparisons++;
                    if (j == l)
                        break;
                }
                comparisons++;
                if (i < j)
                {
                    swaps++;
                    Swap(ref array[i], ref array[j]);
                }
                else
                    break;
            }
            swaps++;
            Swap(ref array[i], ref array[r]);
            return i;
            
        }
        public static void QuickSort(int[] array, int l, int r,out TimeSpan interval, ref int comparisons, ref int swaps)
        {
            if (r <= l)
            {
                interval = TimeSpan.FromSeconds(0);
                return;
            }
            DateTime StartTime;
            StartTime = DateTime.Now;
            int p = Partition(array, l, r, ref comparisons, ref swaps);
            QuickSort(array, l, p - 1,out interval, ref comparisons, ref swaps);
            QuickSort(array, p + 1, r, out interval, ref comparisons, ref swaps);
            DateTime EndTime = DateTime.Now;
            interval = EndTime - StartTime;

        }
        static void Main(string[] args)
        {
            string path = Environment.CurrentDirectory + "\\sorted.dat";
            Random rnd = new Random();
            int[] mas = new int[1000];
            for (int i = 0; i < mas.Length; i++)
            {
                mas[i] = rnd.Next(1000);
            }
            int[] array = new int[mas.Length];
            using (StreamWriter write = new StreamWriter(path, false))
            {
                int comparisons = 0;
                int swaps = 0;
                int[] MergeSortArray = new int[mas.Length];
                for (int i = 0, k = mas.Length - 1; i < mas.Length && k > 0; i++, k--)
                {
                    MergeSortArray[i] = mas[k];
                }
                MergeSort(MergeSortArray, 0, mas.Length - 1, out TimeSpan interval, ref comparisons, ref swaps);
                for (int i = 0; i < mas.Length; i++)
                {
                    write.Write(MergeSortArray[i] + " ");
                }
                Console.WriteLine("Сортировка слиянием(массив из случайных чисел)\nВремя работы:{0}\nКоличество сравнений:{1}\nКоличество перестановок:{2}", interval, comparisons, swaps);
                write.WriteLine();
                array = MergeSortArray;
            }
            Console.WriteLine();
            using (StreamWriter write = new StreamWriter(path, true))
            {
                int comparisons = 0;
                int swaps = 0;
                int[] MergeSortArray = array;
                MergeSort(MergeSortArray, 0, mas.Length - 1, out TimeSpan interval, ref comparisons, ref swaps);
                for (int i = 0; i < mas.Length; i++)
                {
                    write.Write(MergeSortArray[i] + " ");
                }
                Console.WriteLine("Сортировка слиянием(массив, отсортированный в порядке убывания)\nВремя работы:{0}\nКоличество сравнений:{1}\nКоличество перестановок:{2}", interval, comparisons, swaps);
                write.WriteLine();
            }
            Console.WriteLine();
            using (StreamWriter write = new StreamWriter(path, true))
            {
                int comparisons = 0;
                int swaps = 0;
                int[] MergeSortArray = new int[array.Length];
                for (int i = 0, j = MergeSortArray.Length - 1; i < MergeSortArray.Length; i++, j--)
                {
                    MergeSortArray[i] = array[j];
                }
                MergeSort(MergeSortArray, 0, mas.Length - 1, out TimeSpan interval, ref comparisons, ref swaps);
                for (int i = 0; i < mas.Length; i++)
                {
                    write.Write(MergeSortArray[i] + " ");
                }
                Console.WriteLine("Сортировка слиянием(массив, отсортированный в порядке возрастания)\nВремя работы:{0}\nКоличество сравнений:{1}\nКоличество перестановок:{2}", interval, comparisons, swaps);
                write.WriteLine();
            }
            Console.WriteLine();

            using (StreamWriter write = new StreamWriter(path, true))
            {
                int[] HeapSortArray = new int[mas.Length];
                for (int i = 0, k = mas.Length - 1; i < mas.Length && k > 0; i++, k--)
                {
                    HeapSortArray[i] = mas[k];
                }
                int comparisons = 0;
                int swaps = 0;
                HeapSort(HeapSortArray, 0, mas.Length - 1, out TimeSpan interval, ref comparisons, ref swaps);
                for (int i = 0; i < mas.Length; i++)
                {
                    write.Write(HeapSortArray[i] + " ");
                }
                Console.WriteLine("Сортировка кучей(массив заполнен случайными числами)\nВремя работы:{0}\nКоличество сравнений:{1}\nКоличество перестановок:{2}", interval, comparisons, swaps);
                write.WriteLine();
                array = HeapSortArray;
            }
            Console.WriteLine();
            using (StreamWriter write = new StreamWriter(path, true))
            {
                int comparisons = 0;
                int swaps = 0;
                int[] HeapSortArray = array;
                HeapSort(HeapSortArray, 0, mas.Length - 1, out TimeSpan interval, ref comparisons, ref swaps);
                for (int i = 0; i < mas.Length; i++)
                {
                    write.Write(HeapSortArray[i] + " ");
                }
                Console.WriteLine("Сортировка кучей(массив, отсортированный в порядке убывания)\nВремя работы:{0}\nКоличество сравнений:{1}\nКоличество перестановок:{2}", interval, comparisons, swaps);
                write.WriteLine();
            }
            Console.WriteLine();
            using (StreamWriter write = new StreamWriter(path, true))
            {
                int comparisons = 0;
                int swaps = 0;
                int[] HeapSortArray = new int[mas.Length];
                for (int i = 0, j = HeapSortArray.Length - 1; i < HeapSortArray.Length; i++, j--)
                {
                    HeapSortArray[i] = array[j];
                }
                HeapSort(HeapSortArray, 0, mas.Length - 1, out TimeSpan interval, ref comparisons, ref swaps);
                for (int i = 0; i < mas.Length; i++)
                {
                    write.Write(HeapSortArray[i] + " ");
                }
                Console.WriteLine("Сортировка кучей(массив, отсортированный в порядке возрастания)\n Время работы:{0}\nКоличество сравнений:{1}\nКоличество перестановок:{2}", interval, comparisons, swaps);
                write.WriteLine();
            }
            Console.WriteLine();
            using (StreamWriter write = new StreamWriter(path, true))
            {
                int comparisons = 0;
                int swaps = 0;
                int[] QuickSortArray = new int[mas.Length];
                for (int i = 0, k = mas.Length - 1; i < mas.Length && k > 0; i++, k--)
                {
                    QuickSortArray[i] = mas[k];
                }
                QuickSort(QuickSortArray, 0, mas.Length - 1, out TimeSpan interval, ref comparisons, ref swaps);
                for (int i = 0; i < mas.Length; i++)
                {
                    write.Write(QuickSortArray[i] + " ");
                }
                Console.WriteLine("Быстрая сотрировка(массив заполнен случайными числами)\nВремя работы:{0}\nКоличество сравнений:{1}\nКоличество перестановок:{2}", interval, comparisons, swaps);
                write.WriteLine();
                array = QuickSortArray;
            }
            Console.WriteLine();
            using (StreamWriter write = new StreamWriter(path, true))
            {
                int comparisons = 0;
                int swaps = 0;
                int[] QuickSortArray = array;
                QuickSort(QuickSortArray, 0, mas.Length - 1, out TimeSpan interval, ref comparisons, ref swaps);
                for (int i = 0; i < mas.Length; i++)
                {
                    write.Write(QuickSortArray[i] + " ");
                }
                Console.WriteLine("Быстрая сотрировка(массив, отсортированный в порядке убывания)\nВремя работы:{0}\nКоличество сравнений:{1}\nКоличество перестановок:{2}", interval, comparisons, swaps);
                write.WriteLine();
            }
            Console.WriteLine();
            using (StreamWriter write = new StreamWriter(path, true))
            {
                int comparisons = 0;
                int swaps = 0;
                int[] QuickSortArray = new int[mas.Length];
                for (int i = 0, j = QuickSortArray.Length - 1; i < QuickSortArray.Length; i++, j--)
                {
                    QuickSortArray[i] = array[j];
                }
                QuickSort(QuickSortArray, 0, mas.Length - 1, out TimeSpan interval, ref comparisons, ref swaps);
                for (int i = 0; i < mas.Length; i++)
                {
                    write.Write(QuickSortArray[i] + " ");
                }
                Console.WriteLine("Быстрая сотрировка(массив, отсортированный в порядке возрастания)\n Время работы:{0}\nКоличество сравнений:{1}\nКоличество перестановок:{2}", interval, comparisons, swaps);
                write.WriteLine();
            }

            bool check = false;
            StreamReader readFromFile = new StreamReader(File.Open(path, FileMode.Open));
            int[] arrayOfNumbers = new int[mas.Length];
            string line = "";
            while (readFromFile.Peek() > -1)
            {
                line = readFromFile.ReadLine();
                string[] arrayOfString = line.Split(' ');
                for (int i = 0; i < arrayOfNumbers.Length; i++)
                {
                    arrayOfNumbers[i] = int.Parse(arrayOfString[i]);
                }
                for (int i = 0; i < arrayOfNumbers.Length - 1; i++)
                {
                    if (arrayOfNumbers[i] < arrayOfNumbers[i + 1])
                    {
                        check = true;
                        break;
                    }
                }
            }
            readFromFile.Close();
            if (check)
                Console.WriteLine("Иассив не отсортирован");
            else
                Console.WriteLine("Массив отсортирован");
            Console.ReadKey();
        }
    }
}
