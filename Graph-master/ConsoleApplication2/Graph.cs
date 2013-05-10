using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    abstract class Graph<TVertex, TEdge, TData, TWeight>
    {
        public bool Oriented;
        public bool Weighted;
        public bool Type;
        public List<Vertex<TVertex>> Vertexes;
        public int CurrentIndex;
        public int EdgesCount;//количество ребер
        public List<AdjList<TVertex, TEdge, TData, TWeight>> Adj; //вектор смежности
        public Edge<TEdge, TWeight, TVertex>[,] Matrix;

        public Graph()
        {
            Type = false;
            EdgesCount = 0;
            CurrentIndex = 0;
            Vertexes = new List<Vertex<TVertex>>();
            Oriented = false;
            Weighted = false;
        }

        public Graph(bool D)
        {
            Type = false;
            EdgesCount = 0;
            CurrentIndex = 0;
            Vertexes = new List<Vertex<TVertex>>();
            Oriented = D;
        }

        static public Graph<TVertex, TEdge, TData, TWeight> MakeGraph(int V, bool D, bool F)
        {
            if (F)
            {
                return new LGraph<TVertex, TEdge, TData, TWeight>(V, D);
            }
            else
            {
                return new MGraph<TVertex, TEdge, TData, TWeight>(V, D);
            }
        }

        static public void Clear(Graph<TVertex, TEdge, TData, TWeight> graph)
        {
            graph = null;
        }

        static public Graph<TVertex, TEdge, TData, TWeight> MakeGraph(int V, int E, bool D, bool F)
        {
            if (F)
            {
                return new LGraph<TVertex, TEdge, TData, TWeight>(V, E, D);
            }
            else
            {
                return new MGraph<TVertex, TEdge, TData, TWeight>(V, E, D);
            }
        }

        public bool Directed()
        {
            return Oriented;
        }

        public float Saturation()
        {
            if (Oriented)
            {
                return (2*EdgesCount/Vertexes.Count);
            }

            return (EdgesCount / Vertexes.Count);
        }

        public int V()
        {
            return Vertexes.Count;
        }

        public int E()
        {
            return EdgesCount;
        }

        public abstract Edge<TEdge, TWeight, TVertex> AddEdge(Vertex<TVertex> v1, Vertex<TVertex> v2);
        public abstract bool DeleteVertex(Vertex<TVertex> vertex);
        public abstract Vertex<TVertex> AddVertex();
        public abstract bool DeleteEdge(Vertex<TVertex> vertex1, Vertex<TVertex> vertex2);
    }
}
