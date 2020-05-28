using System;
using System.Collections.Generic;
using System.Text;

namespace Lab10._3
{
    public class Graph 
    {
        public Graph(int[,] adj, int num)
        {
            graph = adj;
            V = num;
        }
        private int V = 0;
        private int[,] graph = null;
        public Stack<int> BFS(int startPos, int endPos)
        {
            Queue<int> q = new Queue<int>();
            int[] vPath = new int[V];
            int[] checkedv = new int[V];
            q.Enqueue(startPos);
            checkedv[startPos] = 1;
            while(q.Count > 0)
            {
                int i = q.Dequeue();
                for(int j =0; j < V; j++)
                {
                    if (graph[i,j] == 1 && checkedv[j] == 0)
                    {
                        checkedv[j] = 1;
                        q.Enqueue(j);
                        vPath[j] = i;
                        if (j == endPos)
                            return backChain(vPath, startPos, endPos);
                    }
                }
            }
            return null;
        }
        public Stack<int> DFS(int startPos, int endPos)
        {
            Stack<int> st = new Stack<int>();
            int[] vPath = new int[V];
            int[] checkedv = new int[V];
            st.Push(startPos);
            checkedv[startPos] = 1;
            while (st.Count > 0)
            {
                int i = st.Pop();
                for (int j = V-1; j >= 0; j--)
                {
                    if (graph[i, j] == 1 && checkedv[j] == 0)
                    {
                        checkedv[j] = 1;
                        st.Push(j);
                        vPath[j] = i;
                        if (j == endPos)
                            return backChain(vPath, startPos, endPos);
                    }
                }
            }
            return null;
        }
        public Stack<int> backChain(int[] p, int startPos, int endPos)
        {
            int pos = endPos;
            Stack<int> pathStack = new Stack<int>();
            pathStack.Push(pos);
            while(pos != startPos)
            {
                pos = p[pos];
                pathStack.Push(pos);
            }
            return pathStack;
        }
    }
}
