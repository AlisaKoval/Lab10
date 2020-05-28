using System;
using System.Collections.Generic;

namespace Lab10._4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] a = { { -1, 120, 60, -1, -1, -1, -1, -1 },
                         { 120, -1, -1, -1, -1, 80, 100, -1 },
                         { 60, -1, -1, 40, -1, 140, -1, 50 },
                         { -1, -1, 40, -1, 40, -1, -1, -1 },
                         { -1, -1, -1, 40, -1, 160, -1, -1 },
                         { -1, 80, 140, -1, 160, -1, -1, -1 },
                         { -1, 100, -1, -1, -1, -1, -1, 230 },
                         { -1, -1, 50, -1, -1, -1, 230, -1 } };
            Graph G = new Graph(a, 8);
            int N = 0;
            bool flag = false;
            while (!flag)
            {
                Console.WriteLine("Enter city number:");
                N = int.Parse(Console.ReadLine());
                if (N > 0 && N < 9)
                    flag = true;

            }
            Dictionary<string, int> distance = new Dictionary<string, int>();
            for (int i = 0; i < 8; i++)
            {
                if(!distance.ContainsKey( N + " " + i +1))
                {
                    if (N != i+1)
                    {
                        Stack<int> stack = G.BFS(N - 1, i);
                        Add(stack, a, ref distance, N);
                    }
                }
            }
            foreach(KeyValuePair<string, int> keyValue in distance)
            {
                if (keyValue.Value <= 200)
                    Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
            }
            Console.ReadKey();
        }
        public static void Show(Stack<int> stack)
        {
            int cnt = 0;
            foreach (int item in stack)
            {
                if (cnt == 0)
                    Console.Write(item + 1);
                else
                    Console.Write("->" + (item + 1));
                cnt++;
            }
        }
        public static void Add(Stack<int> stack, int[,] a, ref Dictionary<string, int> dictionaty, int startPos)
        {
            int prev = -1;
            int sum = 0;
            foreach (int item in stack)
            {
                if (prev == -1)
                    prev = item;
                else
                {
                    sum += a[prev, item];
                    prev = item;
                    dictionaty[startPos + " to " + (item + 1)] = sum;
                }
            }
        }
    }
}
