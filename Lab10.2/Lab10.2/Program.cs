using System;

namespace Lab10._2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] demonination = new int[7] { 1, 2, 5, 10, 20, 50, 100 };
            int[] bills = new int[100];
            Random rnd = new Random();
            for(int i =0; i<bills.Length; i++)
            {
                int j = rnd.Next(0, 7);
                bills[i] = demonination[j];
            }
            Sort(bills, 0, bills.Length - 1);
            for (int i = 0; i < bills.Length; i++)
                Console.Write(bills[i] + " ");
            Console.ReadKey();
        }
        public static void Sort(int [] array, int l, int r)
        {
            int min = 0, max = 0;
            for(int i = l; i <= r; i++)
            {
                if (array[i] < min)
                    min = array[i];
                else if (array[i] > max)
                    max = array[i];
            }
            int bn = max - min + 1;
            int[] buckets = new int[bn];
            for (int i = l; i <= r; i++)
                buckets[array[i] - min]++;
            int ind = 0;
            for (int i = min; i <= max; i++)
                for (int j = 0; j < buckets[i - min]; j++)
                    array[ind++] = i;
        }
    }
}
