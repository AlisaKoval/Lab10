   using System;
using System.Collections.Generic;

namespace Lab10._3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] graph = { { 0, 1, 1, 0, 0, 0, 0, 0 },
                              { 1, 0, 0, 0, 0, 1, 1, 0 },
                              { 1, 0, 0, 1, 0, 1, 0, 1 },
                              { 0, 0, 1, 0, 1, 0, 0, 0 },
                              { 0, 0, 0, 1, 0, 1, 0, 0 },
                              { 0, 1, 1, 0, 1, 0, 0, 0 },
                              { 0, 1, 0, 0, 0, 0, 0, 1 },
                              { 0, 0, 1, 0, 0, 0, 1, 0 } };
            bool flag = false;
            int x = 0;
            int y = 0;
            Graph G = new Graph(graph, 8);
            while (!flag)
            {
                Console.WriteLine("Enter the X:");
                x = int.Parse(Console.ReadLine());
                if (x > 0 && x < 9)
                    flag = true;
            }
            flag = false;
            while (!flag)
            {
                Console.WriteLine("Enter the Y:");
                y = int.Parse(Console.ReadLine());
                if (y > 0 && y < 9)
                    flag = true;
            }
            Console.WriteLine("\nIncidence matrix:");
            Stack<int> DFS = G.DFS(x - 1, y - 1);
            Console.WriteLine("\nDFS:");
            Show(DFS);
            Stack<int> BFS = G.BFS(x - 1, y - 1);
            Console.WriteLine("\nBFS:");
            Show(BFS);

            Console.WriteLine("\n\nLincedList:");
            Dictionary<int, List<int>> Graph = new Dictionary<int, List<int>>();
            Graph[1] = new List<int> { 2, 3 };
            Graph[2] = new List<int> { 1, 6, 7 };
            Graph[3] = new List<int> { 1, 4, 6, 8 };
            Graph[4] = new List<int> { 3, 5 };
            Graph[5] = new List<int> { 4, 6 };
            Graph[6] = new List<int> { 2, 3, 5 };
            Graph[7] = new List<int> { 2, 8 };
            Graph[8] = new List<int> { 3, 7 };
            GraphLink g = new GraphLink(Graph, 8);
            Stack<int> LinkDFS = g.DFS(x - 1, y - 1);
            Console.WriteLine("\nDFS:");
            Show(LinkDFS);
            Stack<int> LinkBFS = g.BFS(x - 1, y - 1);
            Console.WriteLine("\nBFS:");
            Show(LinkBFS);
            Console.ReadKey();
        }
        public static void Show( Stack<int> stack)
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
    }
}
