namespace AreaFinder
{
    //класс стороны - вычисляет квадрат стороны треугольника
    public class Side
    {
        public double SideLength { get; private set; }

        public Side(Point2D first, Point2D second)
        {
           SideLength = first.GetDistancePow2(second);
        }
    }
}