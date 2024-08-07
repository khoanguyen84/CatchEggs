namespace CatchEggs;

class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point()
    {
        Random random = new();
        X = 1;
        Y = random.Next(1, 49);;
    }
    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}