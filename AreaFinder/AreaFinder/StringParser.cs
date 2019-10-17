using System;
using System.IO;

namespace AreaFinder
{
    public class StringParser
    {
        //Флаг, указывающий, все ли токены являются числами
        public bool areAllDigits { get; private set; }
        //Флаг, указзывающий, достаточно ли токенов в строке
        public bool areEnoughTokens { get; private set; }
        //в данный массив складываем полученные координаты вершин
        private long[] points { get; set; }
        //массив вершин
        private Point2D[] vertexes;

        private readonly int numberOfTokens;

        public StringParser(string line, int numberOfTokens)
        {
            this.numberOfTokens = numberOfTokens;
            this.areAllDigits = false;
            this.areEnoughTokens = false;
            //разделяем строку по токенам
            var tokens = line.Split(' ');
            //если токенов не меньше 6
            if (tokens.Length >= this.numberOfTokens)
            {
                areEnoughTokens = true;
                this.points = new long[numberOfTokens];
                for (int i = 0; i < points.Length; i++)
                {
                    //на всякий случай удаляем пробелы
                    tokens[i].Trim(' ');
                    //пытаемся преобразовать строку в число, если переполнение/не число - то areAllDigits = false
                    areAllDigits = Int64.TryParse(tokens[i], out points[i]);
                    if (!areAllDigits)
                    {
                        break;
                    }
                }
                //если все вершины корректные - записываем массив вершин для треугольника
                if (areAllDigits)
                {
                    vertexes = new Point2D[numberOfTokens / 2];
                    for (int i = 0; i < vertexes.Length; i++)
                    {
                        vertexes[i] = new Point2D(points[i * 2], points[i * 2 + 1]);
                    }
                }
            }
        }

        //отдаем массив
        public Point2D[] GetPoints()
        {
            return this.vertexes;
        }

    }
}