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

class StructWithNew
{
    public static void Main()
    {
        Coords p = new Coords(3,4);
        Console.WriteLine($"({p.X}, {p.Y})");  // output: (3, 4)
        
        p.X = 5; p.Y = 7;
        Console.WriteLine(p.ToString()); // output: (5, 7)
    }
}