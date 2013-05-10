using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        public void Menu<TVertex, TEdge, TData, TWeight>()
        {
            Console.WriteLine("Выберите пункт меню");
            Console.WriteLine("1 - создать пустой L - граф");
            Console.WriteLine("2 - создать граф из вершин");
            Console.WriteLine("3 - создать граф с вершинами и ребрами");
            Console.WriteLine("4 - добавить вершину");
            Console.WriteLine("5 - добавить ребро");
            Console.WriteLine("6 - преобразовать граф");
            Console.WriteLine("7 - напечатать граф");
            Console.WriteLine("8 - удалить граф");
            Console.WriteLine("9 - выход");
            int m = int.Parse(Console.ReadLine());
            GoGraph<TVertex, TEdge, TData, TWeight> graph;
            switch (m)
            {
                case 1:
                    var graph1 = new GoGraph<TVertex, TEdge, TData, TWeight>();
                    graph = graph1;
                    break;
                case 2:
                    Console.WriteLine("Введите количество вершин, тип и форму");
                    int v = int.Parse(Console.ReadLine());
                    bool d = bool.Parse(Console.ReadLine());
                    bool f = bool.Parse(Console.ReadLine());
                    var graph2 = new GoGraph<TVertex, TEdge, TData, TWeight>(v, d, f);
                    break;
                case 3:
                    Console.WriteLine("Введите количество вершин, ребер, тип и форму");
                    int v1 = int.Parse(Console.ReadLine());
                    int e1 = int.Parse(Console.ReadLine());
                    bool d1 = bool.Parse(Console.ReadLine());
                    bool f1 = bool.Parse(Console.ReadLine());
                    var graph3 = new GoGraph<TVertex, TEdge, TData, TWeight>(v1, e1, d1, f1);
                    
                    break;
          
                default:
                    break;
            }
        }
        static void Main(string[] args)
        {
            
        }
    }
}
