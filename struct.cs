using System;


public struct Coords
{
    public Coords(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; set;}
    public double Y { get; set;}

    public override string ToString() => $"({X}, {Y})";
}

public class Point
{
    public int X { get; }
    public int Y { get; }
    
    public Point(int x, int y) => (X, Y) = (x, y);
}

class StructWithNew
{
    public static void Main()
    {
        Coords p = new Coords(3,4);
        Console.WriteLine($"({p.X}, {p.Y})");  // output: (3, 4)
        
        p.X = 5; p.Y = 7;
        Console.WriteLine(p.ToString()); // output: (5, 7)

        Point i = new Point(5,5);
        Console.WriteLine($"({i.X}, {i.Y})"); // output: (5,5)
    }
}