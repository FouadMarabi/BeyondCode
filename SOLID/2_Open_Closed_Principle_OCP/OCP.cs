namespace SOLID._2_Open_Closed_Principle_OCP
{
    //software entities...should be open for extension, but closed for modification.

    //Importance
    //Extensibility: New features can be added without modifying existing code.
    //Stability: Reduces the risk of introducing bugs when making changes.
    //Flexibility: Adapts to changing requirements more easily.

    namespace Problem
    {
        public class Rectangle
        {
            public double Height { get; set; }
            public double Width { get; set; }
        }

        public class AreaCalculator
        {
            public double TotalArea(Rectangle[] arrRectangles)
            {
                double area = 0;
                foreach (Rectangle objRectangle in arrRectangles)
                {
                    area += objRectangle.Height * objRectangle.Width;
                }
                return area;
            }
        }

        namespace NewRequirement_AddAnotherShape_BadSolution
        {
            public class Circle
            {
                public double Radius { get; set; }
            }
            public class AreaCalculator
            {
                public double TotalArea(object[] arrObjects)
                {
                    double area = 0;
                    Rectangle objRectangle;
                    Circle objCircle;
                    foreach (object obj in arrObjects)
                    {
                        if (obj is Rectangle)
                        {
                            objRectangle = (Rectangle)obj;
                            area += objRectangle.Height * objRectangle.Width;
                        }
                        else
                        {
                            objCircle = (Circle)obj;
                            area += objCircle.Radius * objCircle.Radius * Math.PI;
                        }
                    }
                    return area;
                }
            }
        }

    }

    namespace Solution
    {
        public abstract class Shape
        {
            public abstract double Area();
        }

        public class Rectangle : Shape
        {
            public double Height { get; set; }
            public double Width { get; set; }
            public override double Area()
            {
                return Height * Width;
            }
        }
        public class Circle : Shape
        {
            public double Radius { get; set; }
            public override double Area()
            {
                return Radius * Radius * Math.PI;
            }
        }

        public class AreaCalculator
        {
            public double TotalArea(Shape[] arrShapes)
            {
                double area = 0;
                foreach (Shape objShape in arrShapes)
                {
                    area += objShape.Area();
                }
                return area;
            }
        }

        namespace NewRequirement_AddAnotherShape
        {
            // Polymorphism in OOP
            // https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism
            public class Triangle : Shape
            {
                public double Base { get; set; }
                public double Height { get; set; }
                public override double Area()
                {
                    return 0.5 * Base * Height;
                }
            }
        }
    }
}
