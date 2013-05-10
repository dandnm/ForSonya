using System;

namespace ConsoleApplication2
{
    class MGraph<TVertex, TEdge, TData, TWeight> : Graph<TVertex, TEdge, TData, TWeight>
    {

        public MGraph(int numberVertex, bool D) :base(D)
        {
            Type = true;
            Matrix = new Edge<TEdge, TWeight, TVertex>[numberVertex, numberVertex];
            Oriented = D;

            for (int i = 0; i < numberVertex; i++)
            {
                 AddVertex();
            }
        }

        public MGraph(int numberVertex, int numberEdge, bool D) : base(D)
        {
            Type = true;
            Oriented = D;
            Matrix = new Edge<TEdge, TWeight, TVertex>[numberVertex, numberVertex];

            for (int i = 0; i < numberVertex; i++) //TODO:написать вставки
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
            var M = Matrix;
            Matrix = new Edge<TEdge, TWeight, TVertex>[Vertexes.Count + 1, Vertexes.Count + 1];

            for (int i = 0; i < Vertexes.Count; i++)
            {
                {
                    for (int j = 0; j < Vertexes.Count; j++)
                    {
                        if (j == Vertexes.Count || i == Vertexes.Count)
                        {
                            Matrix[i, j] = null;
                        }

                        Matrix[i, j] = M[i, j];
                    }   
                }
            }
            CurrentIndex++;
            Vertexes.Add(v);
            return v;
        }

        public override bool DeleteVertex(Vertex<TVertex> vertex)
        {
            var M = Matrix;
            Matrix = new Edge<TEdge, TWeight, TVertex>[Vertexes.Count - 1, Vertexes.Count - 1];
            for (int i = 0; i < Vertexes.Count; i++)
            {
                if (i == Vertexes.IndexOf(vertex))
                    i++;
                for (int j = 0; j < Vertexes.Count; j++)
                {
                    if (j == Vertexes.IndexOf(vertex))
                        j++;
                    Matrix[i, j] = M[i, j];
                }
            }
           return Vertexes.Remove(vertex);
        }

        public override Edge<TEdge, TWeight, TVertex> AddEdge(Vertex<TVertex> v1, Vertex<TVertex> v2)
        {
            if (Oriented)
            {
                if (Matrix[Vertexes.IndexOf(v1), Vertexes.IndexOf(v2)] != null)
                {
                    var e = new Edge<TEdge, TWeight, TVertex>(v1, v2);
                    Matrix[Vertexes.IndexOf(v1), Vertexes.IndexOf(v2)] = e;
                    EdgesCount++;
                    return e;
                }
                return null;
            }
            else
            {
                if (Matrix[Vertexes.IndexOf(v1), Vertexes.IndexOf(v2)] != null)
                {
                    var e = new Edge<TEdge, TWeight, TVertex>(v1, v2);
                    Matrix[Vertexes.IndexOf(v1), Vertexes.IndexOf(v2)] = e;
                    Matrix[Vertexes.IndexOf(v2), Vertexes.IndexOf(v1)] = e;
                    EdgesCount++;
                    return e;
                }
                return null;
            }
        }

        public override bool DeleteEdge(Vertex<TVertex> v1, Vertex<TVertex> v2)
        {
            if (Oriented)
            {
                if (Matrix[Vertexes.IndexOf(v1), Vertexes.IndexOf(v2)] != null)
                {
                    Matrix[Vertexes.IndexOf(v1), Vertexes.IndexOf(v2)] = null;
                    EdgesCount--;
                    return true;
                }
                return false;
            }
            else
            {
                if (Matrix[Vertexes.IndexOf(v1), Vertexes.IndexOf(v2)] != null)
                {
                    Matrix[Vertexes.IndexOf(v1), Vertexes.IndexOf(v2)] = null;
                    Matrix[Vertexes.IndexOf(v2), Vertexes.IndexOf(v1)] = null;
                    EdgesCount--;
                    return true;
                }
                return false;
            }
        }
    }
}