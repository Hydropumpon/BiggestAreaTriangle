using System;
using System.Collections;
using System.Collections.Generic;

namespace AreaFinder
{
    //класс для определения равнобедренности треугольника
    public class SideComparer : IEqualityComparer<Side>
    {
        public bool Equals(Side x, Side y)
        {
            return (x.SideLength == y.SideLength);
        }

        public int GetHashCode(Side obj)
        {
            return obj.SideLength.GetHashCode();
        }
    }
}