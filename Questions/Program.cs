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
            V3();
            //V5();
            //V6();
            //V7();
            //V8();
            //V9();
            //V10();
            //V12();
            //V14(); - todo
            //V17();
            //V18();
            //V19();
            //V20();
        }

        private static void V3()
        {
            //Пусть в чемпионате лагеря участвовало n команд. Тогда все встречи всех команд можно представить в виде полного графа, где вершины - команды, а рёбра - игры.
            //Таким образом, количество сыгранных игр - n(n-1)/2. И всего было разыграно n(n-1) очков (т.к. за победу дают 2, а за ничью каждому по 1)
            //Призёры все вместе заработали 15 очков (7+5+3). Значит остальным досталось n(n-1) - 15 очков.
            //При этом каждая из оставшихся n-3 команд набрали 3 или меньше очков (иначе было бы другое третье место). То есть на всех они заработали не больше 3(n-3)
            //Но с другой стороны они не могут набрать больше, чем n(n-1) - 15.
            //Отсюда: n(n-1)-15 <= 3(n-3).
            //Получим n^2-4n-6<=0
            //Тогда -1<=n<=5
            //-1, 0, 1, 2 команды мы не рассматриваем. Сразу не подходят, т.к. противоречат условию
            //3 комнады быть не может, потому как они не смогут разыграть между собой 15 очков (у графа 3 вершины, значит всего 3*2=6 очков)
            //4 команды: 4*3 = 12 по тем же соображениям.
            //Значит всего 5 команд (5*4 = 20, все ОК)
            //и на последние две из них приходится 20-15 = 5 очков. Тогда нужно решить, как распределены очки между последними двумя командами: либо 4-1, либо 3-2.
            //4-1 быть не может, потому как поменяется третье место, а значит берём 3-2 и ответ - у последнего места 2 очка.

            int firstPlacePoints = 14;
            int secondPlacePoints = 12;
            int thirdPlacePoints = 11;
            int sumWinnersPoints = firstPlacePoints + secondPlacePoints + thirdPlacePoints;
            
            // n*(n-1)- firstPlacePoints - secondPlacePoints - thirdPlacePoints <= thirdPlacePoints  * (n-3)
            // n^2 - (1+thirdPlacePoints)*n -  firstPlacePoints - secondPlacePoints + 2*thirdPlacePoints<=0
            
            int b = (-1)*thirdPlacePoints -1;
            int c = 2 * thirdPlacePoints - firstPlacePoints - secondPlacePoints;
            double D = b * b - 4 * c;
            double x1 = 0;
            double x2 = 0;
            if (D >= 0)
            {
                x1 = ((-1) * b - Math.Sqrt(D)) / 2;
                x2 = ((-1) * b + Math.Sqrt(D)) / 2;
            }
            else
            {
                Console.WriteLine("Ошибка");
                Console.ReadKey();
                return;
            }
            //Т.к. A>0 и D>0, то решение неравенства между x1 и x2

            int numOfTeams = 3;
            int end = (int)Math.Truncate(Math.Max(x1, x2));
            for (int i = (int)Math.Truncate(Math.Min(x1,x2)); i <= end; i++)
            {
                if (i * (i - 1) > sumWinnersPoints && i>=3)
                {
                    numOfTeams = i; 
                    break;
                }
            }

            Console.WriteLine("Команды - непризёры: " + (numOfTeams - 3));
            Console.ReadKey();

        }

        private static void V20()
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

            for (int i = 0; i < pairs.Count - 1; i++)
            {
                if (pairs[i].Second == pairs[i + 1].First && pairs[i].First == pairs[i+1].Second)
                {
                    chain.Add(pairs[i].Second);
                }
            }
            chain.Add(pairs[pairs.Count - 1].Second);

            foreach (string item in chain)
            {
                Console.WriteLine(item);
            }

            if (chain.Count < pairs.Count)
            {
                Console.WriteLine("последовательность противоречивая");
            }

            Console.ReadKey();
        }

        private static void V17()
        {
            //Находим контур(оболочку мн-ва точек) после чего перебираем все возможные треугольники, состоящие из точек контура.
            //Это справедливо, так как контур - фигура наибольшей площади, охватывающая все точки, а значит и треугольник может охватить как можно больше точек


            int[] x = { 1, 2, 3, 2, 2, 4, 5,-6 };
            int[] y = { -1, 2, -3, -1, -2, 4,0,-2 };

            List<int[]> contur = new List<int[]>();

            int x0 = x[0];
            int y0 = y[0];

            //ищем самую левую нижнюю точку из множества
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

                //Ищем ближайшую точку с наименьшим полярным углом относительно текущей точки
                for (int i = 1; i < x.Length; i++)
                {
                    if ((x0 == endx && y0 == endy) || ((endx - x0) * (y[i] - y0) - (x[i] - x0) * (endy - y0) > 0))
                    {
                        endx = x[i];
                        endy = y[i];
                    }
                }

                //Переходим к следующей точке
                x0 = endx;
                y0 = endy;


            } while (!(endx == contur[0][0] && endy == contur[0][1]));  //Пока не придём в начало


            int max = 0;
            List<int[]> maxTriangle = new List<int[]>
            {
                new int[] { 0, 0 },
                new int[] { 0, 0 },
                new int[] { 0, 0 }
            };


            for (int i = 0; i < contur.Count-2; i++)
            {
                for (int j = i+1; j < contur.Count-1; j++)
                {
                    for (int k = j+1; k < contur.Count; k++)
                    {
                        int nPoints = CountPointsInTriangle(contur,i,j,k);
                        if(nPoints > max)
                        {
                            max = nPoints;
                            maxTriangle[0] = contur[i];
                            maxTriangle[1] = contur[j];
                            maxTriangle[2] = contur[k];
                        }
                    }
                }
            }

            Console.WriteLine("Треугольник с максимальным количеством точек внутри него:"+ 
                "(" + maxTriangle[0][0] + "," + maxTriangle[0][1] + ")" +
                "(" + maxTriangle[1][0] + "," + maxTriangle[1][1] + ")" + 
                "(" + maxTriangle[2][0] + "," + maxTriangle[2][1] + ")" +
                "\nКоличество точек = " + max);
            Console.ReadKey();
        }

        private static int CountPointsInTriangle(List<int[]> contur, int first, int second, int third)
        {
            int nPoints = 0;
            for (int i = 0; i < contur.Count; i++)
            {
                if (i == first || i == second || i == third) continue;

                double orientFirstSecond = FindOrientation(contur[first][0], contur[first][1], contur[second][0], contur[second][1], contur[i][0], contur[i][1]);
                double orientSecondThird = FindOrientation(contur[second][0], contur[second][1], contur[third][0], contur[third][1], contur[i][0], contur[i][1]);
                double orientFirstThird = FindOrientation(contur[first][0], contur[first][1], contur[third][0], contur[third][1], contur[i][0], contur[i][1]);

                if(orientFirstSecond >= 0 && orientSecondThird >= 0 && orientFirstThird >=0 ||
                    orientFirstSecond < 0 && orientSecondThird < 0 && orientFirstThird < 0)
                {
                    nPoints++;
                }

            }
            return nPoints;
        }

        private static double FindOrientation(int x1, int y1, int x2, int y2, int xt, int yt)
        {
            return xt * (y2 - y1) + yt * (x1 - x2) + y1 * x2 - x1 * y2;
        }

        private static void V9()
        {
            //Пусть сеть - это граф, в которм центры хранения - это вершины, а каналы связи - рёбра.
            //Сказано, что некоторые пары центров соединены каналами и известно, что в сети каждый центр соединён каналами связи
            //с четным числом центров, что означает, что в графе все вершины имеют чётную степень.
            //Т.к. обмен информацией осуществляется либо непосредственно по канали, либо через другие каналы и центры, то граф связный.
            //Так как граф связен и все его вершины имеют чётную степень, то такой граф Эйлеров, а значит в нём есть эйлеров цикл, проходящий по всем вершинам только 1 раз.
            //Представим цикл как цепочку: 1-2-3-4-5-6-1. Допустим удалён канал(ребро или путь) 1-2. Связность графа не нарушается, так как из 2 в 1 есть путь 2-3-4-5-6-1.
            //При этом удалив ещё один канал, нельзя гарантировать, что связность графа сохранится, так как может быть удалён мост.

        }

        private static void V7()
        {
            //Представляем историю деления бактерий в виде дерева.
            //Результатом деления бактерий стало появление 102 бактерий, которые не могут поделиться ещё.
            //Значит, у этого дерева будет 102 листа - вершины с степенью 1.
            //Все бактерии пошли от одной - корня с степенью 3.
            //Тогда у нас ещё есть бактерии, которые делятся на 4 - вершина с степенью 5 (1 входящая и 4 исходящие)
            //и бактерии, которые делятся на 2 - вершина с степенью 3.
            //Пусть n - сколько делилось на 4, тогда 6n - сколько делилось на 2.
            //Тогда всего вершин - n(на 4) + 6n(на 2) + 102(листья) + 1(корень) - 7n+103, а рёбер  - на 1 меньше: 7n+102
            //
            //Лемма о рукопожатиях говорит нам о том, что сумма степеней всех вершин графа - это чётное число, равное удвоенному числу рёбер.
            //То есть мы берём сумму всех вершин, умноженных на их степени и получим число рёбер, умноженное на 2.
            //В нашем случае:
            // 5*n (n вершин степени 5, которые делились на 4)
            // 3*6n (6n вершин степени 3, которые делились на 2)
            // 3 * 1 (корень)
            // 1*102 (листья дерева)
            // Т.о.: 5n + 3*6n + 3 * 1 + 1 * 102 = 2*(7*n+102).
            //
            // Решив уравнение, мы получим ответ.

            int startDivide = 3;
            int leafs = 102;
            int[] dividers = { 4, 2 };
            int dividersCoef = 6;

            int firstDividers = (2 * leafs - leafs - startDivide) / (dividers[0] + 1 + (dividers[1] + 1) * 6 - 2 * (dividersCoef + 1));
            int secondDividers = firstDividers * dividersCoef;
            Console.WriteLine("Бактерий, которые делились на " + dividers[0] + ": " + firstDividers + ". Бактерий, которые делились на " + dividers[1] + ": " + secondDividers);
            Console.ReadKey();

        }

        private static void V8()
        {
            //Пользуемся теоремой Эйлера о соотношении количества вершин, рёбер и граней графа:
            //n-m+f=2 , где n-количество вершин m-количество рёбер f-количество граней.

            //Пусть город - граф, в котором перекрёстки - вершины графа, а отрезки улиц - рёбра, тогда квартал - это внутренняя грань

            int cross = 155;
            int street = 260;

            //-1 говорит о том, что у любого графа всегда есть 1 свободная грань - внешняя,но внешняя грань не является кварталом
            int quarter = 2-cross+street -1;
            Console.WriteLine("Количество универсамов = " + quarter);
            Console.ReadKey();
        }

        private static void V5()
        {
            //Делим изначальное количество участников пополам и считаем,
            //что в эти половинах все со всеми сыграли. - полный граф, где вершины - игроки, а рёбра - отношение сыгранной игры.
            //Получается, что условие про то, что всегда есть тройка, в которой две команды уже сыграли.
            //Посчитать это можно так: k * (k - 1)/2 + m * (m-1),
            //где k и m - кол-ва участников в группах (если брать нечётное количество участников, то исходное множество разобьётся не ровно пополам)

            int teams = 20;

            int nFirst = teams / 2;
            int nSecond = teams / 2;

            if (teams % 2 != 0)
            {
                nSecond++;
            }


            int games = (nFirst * (nFirst - 1)) / 2 + (nSecond * (nSecond - 1)) / 2;
            Console.WriteLine("Количество сыгранных игр = " + games);
            Console.ReadKey();
        }

        #region V6
        private static void V6()
        {

            int nColors = 64;

            if(nColors < 0 && nColors > 64)
            {
                Console.WriteLine("Ошибка ввода");
                Console.ReadKey();
                return;
            }

            if(nColors <= 33)
            {
                Console.WriteLine("Минимальное количество пар = " + (nColors-1));
                Console.ReadKey();
                return;
            }

            int[,] desk = new int[8, 8];

            desk = new int[,]
            {
                { 1,17,1,10,1,5,1,2},
                { 24,1,16,1,9,1,4,1},
                { 1,23,1,15,1,8,1,3},
                { 29,1,22,1,14,1,7,1},
                { 1,28,1,21,1,13,1,6},
                { 32,1,27,1,20,1,12,1},
                { 1,31,1,26,1,19,1,11},
                { 33,1,30,1,25,1,18,1}
            };

            int nPairs = 32;
            nColors -= 33;
            int deskSize = desk.GetLength(0) - 1;
            while (nColors > 0)
            {
                int circleIndex = FindFreeCircle(desk,deskSize); //Находим первое кольцо, где есть "1"

                int[] place = FindPlace(desk, deskSize, circleIndex);

                if (place.Length == 0)
                {
                    Console.WriteLine("Ошибка");
                    return;
                }

                desk[place[0], place[1]] = nColors + 33; //Вместо подходящей единицы ставим новый цвет

                if (CheckCircleCorner(desk, circleIndex, place))   //Поставили в угол кольца
                {
                    nPairs += 2;
                }
                else
                {
                    if (CheckCornerBlocked(desk, circleIndex, place))   //Поставили рядом с углом, рядом с которым уже ставили
                    {
                        nPairs += 2;
                    }
                    else   //На стороне кольца
                    {
                        nPairs += 3;
                    }
                }

                nColors--;
            }

            Console.WriteLine("Количество пар = " + nPairs);

            Console.ReadKey();
        }

        private static bool CheckCornerBlocked(int[,] desk, int circleIndex, int[] place)
        {
            if (place[0] == circleIndex)
            {
                int[] leftCornerCandidate = new int[] { place[0], place[1] - 1 };
                if (CheckCircleCorner(desk, circleIndex, leftCornerCandidate))
                {
                    if (desk[leftCornerCandidate[0] + 1, leftCornerCandidate[1]] != 1)
                    {
                        return true;
                    }
                }
                int[] rightCornerCandidate = new int[] { place[0], place[1] + 1 };
                if (CheckCircleCorner(desk, circleIndex, rightCornerCandidate))
                {
                    if (desk[rightCornerCandidate[0] + 1, rightCornerCandidate[1]] != 1)
                    {
                        return true;
                    }
                }
            }

            if (place[0] == desk.GetLength(0) - 1 - circleIndex)
            {
                int[] leftCornerCandidate = new int[] { place[0], place[1] - 1 };
                if (CheckCircleCorner(desk, circleIndex, leftCornerCandidate))
                {
                    if (desk[leftCornerCandidate[0] - 1, leftCornerCandidate[1]] != 1)
                    {
                        return true;
                    }
                }
                int[] rightCornerCandidate = new int[] { place[0], place[1] + 1 };
                if (CheckCircleCorner(desk, circleIndex, rightCornerCandidate))
                {
                    if (desk[rightCornerCandidate[0] - 1, rightCornerCandidate[1]] != 1)
                    {
                        return true;
                    }
                }
            }

            if (place[1] == circleIndex)
            {
                int[] upperCornerCandidate = new int[] { place[0] + 1, place[1]};
                if (CheckCircleCorner(desk, circleIndex, upperCornerCandidate))
                {
                    if (desk[upperCornerCandidate[0], upperCornerCandidate[1]+1] != 1)
                    {
                        return true;
                    }
                }
                int[] bottomCornerCandidate = new int[] { place[0] - 1, place[1]};
                if (CheckCircleCorner(desk, circleIndex, bottomCornerCandidate))
                {
                    if (desk[bottomCornerCandidate[0], bottomCornerCandidate[1] + 1] != 1)
                    {
                        return true;
                    }
                }
            }

            if (place[1] == desk.GetLength(0) - 1 - circleIndex)
            {
                int[] upperCornerCandidate = new int[] { place[0] + 1, place[1]};
                if (CheckCircleCorner(desk, circleIndex, upperCornerCandidate))
                {
                    if (desk[upperCornerCandidate[0], upperCornerCandidate[1] - 1] != 1)
                    {
                        return true;
                    }
                }
                int[] bottomCornerCandidate = new int[] { place[0] - 1, place[1]};
                if (CheckCircleCorner(desk, circleIndex, bottomCornerCandidate))
                {
                    if (desk[bottomCornerCandidate[0], bottomCornerCandidate[1] - 1] != 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool CheckCircleCorner(int[,] desk, int circleIndex, int[] place)
        {
            if (place[0] == circleIndex && place[1] == circleIndex ||   //Левый верхний
                place[0] == desk.GetLength(0) -1 - circleIndex && place[1] == circleIndex ||   //Левый нижний
                place[0] == desk.GetLength(0) -1 - circleIndex && place[1] == desk.GetLength(0) - 1 - circleIndex ||   //Правый нижний
                place[0] == circleIndex && place[1] == desk.GetLength(0) - 1 - circleIndex)   //Правый верхний
            {
                return true;
            }
            return false;
        }

        private static int[] FindPlace(int[,] desk,int deskSize, int circleIndex)
        {
            if (desk[circleIndex, circleIndex] == 1) return new int[] { circleIndex, circleIndex };
            if (desk[deskSize - circleIndex, deskSize - circleIndex] == 1) return new int[] { deskSize - circleIndex, deskSize - circleIndex };

            for (int i = circleIndex; i < deskSize + 1 - circleIndex; i++)
            {
                if (i == circleIndex || i == deskSize - circleIndex) //Обход верхней или нижней части кольца
                {
                    for (int j = 0; j < deskSize + 1 - circleIndex; j++)
                    {
                        if (desk[i, j] == 1)
                        {
                            return new int[] { i, j };
                        }
                    }
                }
                else if (desk[i,circleIndex] == 1)
                {
                    return new int[] { i, circleIndex };
                }
                else if(desk[i, deskSize - circleIndex] == 1)
                {
                    return new int[] { i, deskSize - circleIndex };
                }
            }
            return new int[0];
        }

        private static int FindFreeCircle(int[,] desk,int deskSize)
        {
            int circleIndex = 0;
            while(circleIndex <= deskSize / 2)
            {
                for (int i = 0; i < deskSize; i++)
                {
                    if (desk[circleIndex, i] == 1) return circleIndex;
                    if (desk[deskSize - circleIndex, i] == 1) return circleIndex;
                    if (desk[i, circleIndex] == 1) return circleIndex;
                    if (desk[i, deskSize - circleIndex] == 1) return circleIndex;
                }
                circleIndex++;
            }
            return -1;
        }
        #endregion
        
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

            for (int i = 0; i < pairs.Count-1; i++)
            {
                if (pairs[i].Second == pairs[i + 1].First)
                {
                    chain.Add(pairs[i].Second);
                }
            }
            chain.Add(pairs[pairs.Count-1].Second);

            foreach (string item in chain)
            {
                Console.WriteLine(item);
            }

            if (chain.Count > pairs.Count) 
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

            //ищем самую левую нижнюю точку из множества
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

                //Ищем ближайшую точку с наименьшим полярным углом относительно текущей точки
                for (int i = 1; i < x.Length; i++)
                {
                    if ((x0 == endx && y0 == endy) || ((endx - x0) * (y[i] - y0) - (x[i] - x0) * (endy - y0) > 0))
                    {
                        endx = x[i];
                        endy = y[i];
                    }
                }

                //Переходим к следующей точке
                x0 = endx;
                y0 = endy;


            } while (!(endx == contur[0][0] && endy == contur[0][1]));  //Пока не придём в начало

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
