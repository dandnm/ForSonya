using System;
using System.Collections.Generic;

namespace ConsoleApplication2
{
    class LGraph<TVertex, TEdge, TData, TWeight> : Graph<TVertex, TEdge, TData, TWeight>
    {

        public LGraph()
        {
            Type = false;
            Adj = new List<AdjList<TVertex, TEdge, TData, TWeight>>();
        }

        public LGraph(int numberVertex, bool D) :base(D)
        {
            Type = false;
            Adj = new List<AdjList<TVertex, TEdge, TData, TWeight>>();
            Oriented = D;

            for (int i = 0; i < numberVertex; i++)
            {
                AddVertex();
            }
        }

        public LGraph(int numberVertex, int numberEdge, bool D) : base(D)
        {
            Type = false;
            Adj = new List<AdjList<TVertex, TEdge, TData, TWeight>>();
            Oriented = D;

            for (int i = 0; i < numberVertex; i++)
            {
                AddVertex();
                do
                {
                    Random r = new Random();
                    if (AddEdge(Vertexes[r.Next(Vertexes.Count)], Vertexes[r.Next(Vertexes.Count)]) != null)
                        numberEdge--;  //вставляем ребро со случайными вершинами
                } while (numberEdge != 0);
            }
        }

        public override Vertex<TVertex> AddVertex() //добавление вершины
        {
            var v = new Vertex<TVertex>();
            v.Index = CurrentIndex;
            Vertexes.Add(v);
            var A = new AdjList<TVertex, TEdge, TData, TWeight>();
            A.AdjIndex = CurrentIndex;
            Adj.Add(A);
            CurrentIndex++;
            return v;
        }

        public override bool DeleteVertex(Vertex<TVertex> vertex) //TODO: delete all edges
        {
            bool result = false;
            for (int i = 0; i < Adj.Count; i++)
            {
                var edge = Adj[i];
                if (edge.AdjIndex == vertex.Index)
                {
                    result = true; //нашли
                    if (!Oriented)
                    {
                        var point = edge.Head;
                        for (int j = 0; j < edge.Length; j++)
                        {
                            if (point == null)
                                break;
                            DeleteEdge(point.AdjEdge.Vertex2, vertex);
                        }
                    }
                    Adj.RemoveAt(i);
                }
            }

            if (Oriented)
            {
                if (result == false)
                    return false;
                for (int i = 0; i < Adj.Count; i++)
                {
                    var edge = Adj[i];
                    var point = edge.Head;
                    for (int j = 0; j < edge.Length; j++)
                    {
                        if (point == null)
                            break;
                        if (point.AdjEdge.Vertex2.Equals(vertex))
                            DeleteEdge(point.AdjEdge.Vertex2, vertex);
                        point = point.Next;
                    }
                }
            }

            for (int i = 0; i < Vertexes.Count; i++)
            {
                var v = Vertexes[i];
                if (v.Equals(vertex))
                    Adj.RemoveAt(i);
            }
            return result;
        }

        public override Edge<TEdge, TWeight, TVertex> AddEdge(Vertex<TVertex> v1, Vertex<TVertex> v2)
        {
            if (Oriented)
            {
                for (int i = 0; i < Adj.Count; i++)
                {
                    if (v1.Index == Adj[i].AdjIndex)
                    {
                        var e = new Edge<TEdge, TWeight, TVertex>(v1, v2);//добавить проверку на добавление
                        Adj[i].AddNode(e);
                        EdgesCount++;
                        return e;
                    }
                    return null;
                }
            }

            for (int i = 0; i < Adj.Count; i++)
            {
                if (v1.Index == Adj[i].AdjIndex)
                {
                    var e = new Edge<TEdge, TWeight, TVertex>(v1, v2);
                    Adj[i].AddNode(e);
                }
            }

            for (int i = 0; i < Adj.Count; i++)
            {
                if (v2.Index == Adj[i].AdjIndex)
                {
                    var e = new Edge<TEdge, TWeight, TVertex>(v1, v2);
                    Adj[i].AddNode(e);
                    EdgesCount++;
                    return e;
                }
            }
            return null;
        }

        public override bool DeleteEdge(Vertex<TVertex> vertex1, Vertex<TVertex> vertex2)
        {
            if (Oriented)
            {
                for (int i = 0; i < Adj.Count; i++)
                {
                    var v = Adj[i];
                    if (vertex1.Index == v.AdjIndex) //если нашли исходящую вершину
                    {
                        EdgesCount--;
                        return v.DeleteNode(vertex2);
                    }
                }
                return false;
            }

            else
            {
                bool flag1 = false;
                bool flag2 = false;

                for (int i = 0; i < Adj.Count; i++)
                {
                    var v = Adj[i];
                    if (vertex1.Index == v.AdjIndex) //если нашли исходящую вершину
                    {
                        flag1 = v.DeleteNode(vertex2);
                    }

                    if (vertex2.Index == v.AdjIndex) //если нашли исходящую вершину
                    {
                        flag2 = v.DeleteNode(vertex1);
                    }
                }

                if (flag1 && flag2)
                    EdgesCount--;
                return (flag1 && flag2);
            }
        }
    }
}