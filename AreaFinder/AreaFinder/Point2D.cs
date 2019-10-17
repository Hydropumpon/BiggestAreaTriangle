using System;

namespace AreaFinder
{
    //класс точки в 2D пространстве
    public struct Point2D
    {
        public long x { get; private set; }
        public long y { get; private set; }
        public Point2D(long x, long y)
        {
            this.x = x;
            this.y = y;
        }

        //возвращает квадрат расстояния между точками
        public double GetDistancePow2(Point2D other)
        {
            double distance;
            double xDiff = ((double)this.x - other.x);
            double yDiff = ((double)this.y - other.y);
            distance = (xDiff * xDiff) + (yDiff * yDiff);
            return distance;
        }

        public override string ToString()
        {
            return (x.ToString() + " " + y.ToString());
        }
    }
}