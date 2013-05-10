using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    internal class AdjList<TVertex, TEdge, TData, TWeight>
    {
        public int AdjIndex;

        public int Length;

        public Node Head;

        public class Node
        {
            public Node Next;
            public Edge<TEdge, TWeight, TVertex> AdjEdge;

            public Node()
            {
                AdjEdge = null;
                Next = null;
            }

            public Node(Edge<TEdge, TWeight, TVertex> edge)
            {
                AdjEdge = edge;
                Next = null;
            }
        }

        public AdjList()
        {
            Length = 0;
            Head = null;
        }

        public bool AddNode(Edge<TEdge, TWeight, TVertex> edge)
        {
            if (Length == 0)
            {
                Head.Next = new Node(edge);
                Length++;
                return true;
            }

            Node q = Head;
            for (int i = 0; i < Length - 1; i++)
            {
                if (q.AdjEdge.Equals(edge))
                    return false;
                q = q.Next;
            }
            q.Next = new Node(edge);
            Length++;
            return true;
        }

        public bool DeleteNode(Vertex<TVertex> vertex)
        {
            if (Length == 0)
                return false;

            if ((Length == 1) && (Head.AdjEdge.Vertex2.Equals(vertex)))
            {
                Length--;
                Head = null;
                return true;
            }

            Node q = Head;

            for (int i = 0; i < Length - 1; i++)
            {
                if (q.Next == null)
                    return false;

                if ((q.Next.AdjEdge.Vertex1.Equals(vertex)) && (q.Next.Next == null))
                {
                    q.Next = null;
                    Length--;
                    return true;
                }

                if (q.Next.AdjEdge.Vertex1.Equals(vertex))
                {
                    q.Next = q.Next.Next;
                    Length--;
                    return true;
                }

                q = q.Next;
            }
            return false;
        }
    }
}
