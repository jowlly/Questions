using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Questions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //V12();
            //V14(); - todo
            //V10();
            //V18();
            V19();
        }

        class Pair
        {
            public string First { get; set; }
            public string Second { get; set; }

            public Pair(string first, string second)
            {
                First = first;
                Second = second;
            }
        }
        private static void V19()
        {
            List<Pair> pairs = new List<Pair>();
            int n = 5;
            for (int i = 0; i < n; i++)
            {
                string[] pair = Console.ReadLine().Split(',');
                pairs.Add(new Pair(pair[0], pair[1]));
            }

            bool[] visited = new bool[pairs.Count];
            visited[0] = true;
            List<string> chain = new List<string>();
            chain.Add(pairs[0].First);
            string next = pairs[0].Second;
            int k = 1;
            while (k<pairs.Count)
            {
                for (int j = 0; j < pairs.Count; j++)
                {
                    if (pairs[j].First == next && !visited[j])
                    {
                        chain.Add(next);
                        next = pairs[j].Second;
                        visited[j] = true;
                        break;
                    }
                }
                k++;
            }
            chain.Add(next);


            foreach(string item in chain)
            {
                Console.WriteLine(item);
            }

            if (chain.Count < pairs.Count)
            {
                Console.WriteLine("последовательность не полная");
            }
            else
            {
                Console.WriteLine("последовательность полная");
            }

            Console.ReadKey();
        }

        private static void V18()
        {
            int STENA = 3;
            int YUG = 5;
            int VOSTOK = 3;
            int SEVER = 1;

            // первое перейти за нижнюю границу трапеции  второе - за то ,чтобы не выйти за верхнюю границу
            if ((SEVER - YUG) > (-1) * VOSTOK && (SEVER - YUG) < (VOSTOK + STENA))
            {
                Console.WriteLine("Blue");
            }
            else
            {
                Console.WriteLine("White");
            }

        }

        private static void V10()
        {
            int nComputers = 10;
            int nColors = 5;
            Console.WriteLine(nComputers - nColors == 1);
        }

        private static void V14()
        {
            int v = 5;
            int w = 3;

            int[] x = { 2, 2, 4, 7, 4, 1, -1, 4 };
            int[] y = { 1, 2, -2, 2, 4, 6, 2, 2 };

            List<int> xA = new List<int>();
            List<int> yA = new List<int>();

            //Отбираем точки, которые лежат внутри А
            for (int i = 0; i < x.Length; i++)
            {
                if ((x[i] < v && y[i] < w) && (x[i] > 0 && y[i] > 0))
                {
                    xA.Add(x[i]);
                    yA.Add(y[i]);
                }
            }

            xA.AddRange(new List<int>() { 0, 0, v, v });
            yA.AddRange(new List<int>() { w, 0, 0, w });

            for (int i = 0; i < x.Length; i++)
            {
                //int[] nearestLeft = FindNearestRight(x[i], y[i], xA, yA, v,w);
                //int[] nearestRight = FindNearestLeft(x[i], y[i], xA, yA, v,w);
            }



        }

        private static int[] FindNearestLeft(int x, int y, List<int> xA, List<int> yA, int v, int w)
        {
            int nearestX = 0;
            int nearestY = w;
            for (int i = 0; i < xA.Count; i++)
            {
                if (xA[i] != x && xA[i] < x && yA[i] > y)
                {
                    if (xA[i] > nearestX)
                    {
                        nearestX = xA[i];
                        nearestY = yA[i];
                    }
                }
            }

            return new int[] { nearestX, nearestY };
        }

        private static int[] FindNearestRight(int x, int y, List<int> xA, List<int> yA)
        {
            throw new NotImplementedException();
        }

        private static int GetSquare(int x1, int y1, int x2, int y2)
        {
            return Math.Abs((x2 - x1) * (y2 - y1));
        }

        private static void V12()
        {
            int[] x = { 1, 2, 3, 2, 2, 4 };
            int[] y = { -1, 2, -3, -1, -2, 4 };

            List<int[]> contur = new List<int[]>();

            int x0 = x[0];
            int y0 = y[0];

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] < x0 || (x[i] == x0 && y[i] < y0))
                {
                    x0 = x[i];
                    y0 = y[i];
                }
            }

            int endx = 0;
            int endy = 0;

            do
            {
                contur.Add(new int[] { x0, y0 });
                endx = x[0];
                endy = y[0];

                for (int i = 1; i < x.Length; i++)
                {
                    if ((x0 == endx && y0 == endy) || ((endx - x0) * (y[i] - y0) - (x[i] - x0) * (endy - y0) > 0))
                    {
                        endx = x[i];
                        endy = y[i];
                    }
                }

                x0 = endx;
                y0 = endy;


            } while (!(endx == contur[0][0] && endy == contur[0][1]));

            //while(true)
            //{
            //    int nextx = x0;
            //    int nexty = y0;

            //    for (int i = 0; i < x.Length; i++)
            //    {
            //        if ((nextx - x0) * (y[i] - y0) - (x[i] - x0) * (nexty - y0) > 0)
            //        {
            //            nextx = x[i];
            //            nexty = y[i];
            //        }
            //    }

            //    if (nextx == x0 && nexty == y0)
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        x0 = nextx;
            //        y0 = nexty;
            //        contur.Add(new int[] { nextx, nexty });
            //    }
            //}

            foreach (int[] item in contur)
            {
                Console.WriteLine("X " + item[0] + "  Y " + item[1]);
            }
            Console.ReadLine();
        }
    }
}
