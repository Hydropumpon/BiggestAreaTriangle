using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace AreaFinder
{
    //перечисление для треугольника : является точкой/линией, неравнобедренным треугольником, равнобедренным треугольником)
    public enum TriangleFormEnum { LineDot, Triangle, IsoTriangle};

    public class Triangle
    {
        private Point2D[] vertexes { get;  set; }
        private TriangleFormEnum triangleForm { get; set; }
        private Side[] sides { get; set; }
        public double area { get; private set; }

        private const int VERTEXES_NUMBER = 3;
        public Triangle()
        {
            this.triangleForm = TriangleFormEnum.LineDot;
        }

        public Triangle(Point2D[] vertexes)
        {
            this.vertexes = vertexes;
            this.sides = new Side[VERTEXES_NUMBER];
            //вычисляем стороны треугольника
            for (int i = 0; i < VERTEXES_NUMBER; i++)
            {
                this.sides[i] = new Side(this.vertexes[i], this.vertexes[(i+1) % VERTEXES_NUMBER]);
            }
            //вычисляем, являются ли хотя бы 2 стороны одинаковыми
            bool isIsosceles = (this.sides.Length != this.sides.Distinct(new SideComparer()).Count());
            //вычисляем, является ли треугольник треугольником, или вершины сходятся в линию/точку
            bool isTriangle = (this.sides.OrderBy(sideValue => sideValue.SideLength).First().SideLength != 0);
            //заполняем флаг треугольника
            if (isTriangle)
            {
                if (isIsosceles)
                {
                    triangleForm = TriangleFormEnum.IsoTriangle;
                }
                else
                {
                    triangleForm = TriangleFormEnum.Triangle;
                }
            }
            else
            {
                triangleForm = TriangleFormEnum.LineDot;
            }
            //если треугольник такой, как нам нужен - вычисляем площадь. Area - это удвоенная площадь треугольника. Делить на 2 не имеет смысла, так как мы все равно её не выводим
            if (triangleForm == TriangleFormEnum.IsoTriangle)
            {
                area = Math.Abs(((double)vertexes[0].x - vertexes[2].x) * ((double)vertexes[1].y - vertexes[2].y) -
                                ((double)vertexes[1].x - vertexes[2].x) * ((double)vertexes[0].y - vertexes[2].y));
            }
            //иначе устанавливаем площадь равную 0
            else
            {
                area = 0;
            }
        }

        public override string ToString()
        {
            string result = null;
            if (this.area != 0)
            {
                //переводим координаты вершин в строку
                foreach (var vertex in this.vertexes)
                {
                    result += vertex.ToString() + " ";
                }
                //удаляем последний пробел
                result.TrimEnd(' ');
            }
            return result;
        }
    }
}