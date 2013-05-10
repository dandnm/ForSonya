using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class GoGraph<TVertex, TEdge, TData, TWeight>
    {
        public Graph<TVertex, TEdge, TData, TWeight> G;
        public bool Oriented;
        public bool Type;

        public GoGraph()
        {
            G = new LGraph<TVertex, TEdge, TData, TWeight>();
        }

        public GoGraph(int V, bool D, bool F)
        {
            G = Graph<TVertex, TEdge, TData, TWeight>.MakeGraph(V, D, F);
        }

        public GoGraph(int V, int E, bool D, bool F)
        {
            G = Graph<TVertex, TEdge, TData, TWeight>.MakeGraph(V, E, D, F);
        }

        public void Reverse()
        {
            var g = G;
            if (Type)//если M, то в L
            {
                G = new LGraph<TVertex, TEdge, TData, TWeight>();
                G.Oriented = g.Oriented;
                for (int i = 0; i < g.Vertexes.Count; i++)//пополняем вектор смежности
                {
                    var A = new AdjList<TVertex, TEdge, TData, TWeight>();
                    A.AdjIndex = g.Vertexes[i].Index;
                    G.Adj.Add(A);
                }
                
                for (int i = 0; i < g.Vertexes.Count; i++)
                {
                    for (int j = 0; j < g.Vertexes.Count; j++)
                    {
                        if (g.Matrix[i, j] != null)
                        {
                            G.Adj[i].AddNode(g.Matrix[i, j]);
                        }
                    }
                }
                g = null;
            }
            else//из L в M
            {
                G.Matrix = new Edge<TEdge, TWeight, TVertex>[G.Vertexes.Count,G.Vertexes.Count];
                for (int i = 0; i < G.Vertexes.Count; i++)
                {
                    for (var q = G.Adj[i].Head; q.Next != null; q = q.Next)
                    {
                        G.Matrix[i, q.AdjEdge.Vertex2.Index] = q.AdjEdge;
                    }
                }
                G.Adj = null;
            }
        }
        
        public void Print()
        {
            string str = "";
            if (Type)
            {
                for (int i = 0; i < G.Vertexes.Count; i++)
                {
                    for (int j = 0; j < G.Vertexes.Count; j++)
                    {
                        if(G.Matrix[i, j] != null)
                            str = str + " " + G.Matrix[i, j].Vertex1.Index.ToString() + " " + G.Matrix[i, j].Vertex2.Index.ToString();
                        str = str + "    ";
                    }
                    Console.WriteLine(str);
                    str = "";
                }
            }
            else
            {
                for (int i = 0; i < G.Adj.Count; i++)
                {
                    Console.WriteLine(G.Adj[i].AdjIndex.ToString());
                    for (var q = G.Adj[i].Head; q.Next != null; q = q.Next)
                    {
                        str = str + " " + q.AdjEdge.Vertex1.ToString() + " " + q.AdjEdge.Vertex2.ToString();
                    }
                    str = "";
                }
            }
        }
    }
}
