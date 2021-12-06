
// Read file, convert string binary to 32 bit integers
using System.Diagnostics;

var ventLines = System.IO.File.ReadLines(@"input5.txt")
    .Select(x => x.Trim())
    .Select(x => x.Split(" -> ", 2))
    .Select(coordPair => new Path(coordPair[0], coordPair[1]))
    .ToList();

int solve(List<Path> input, bool allowDiagonal = true)
{
    var ventMap = new Dictionary<int, int>();
    foreach (var coordPair in input)
    {
        // Skip diagonal lines, unless allowed

        if (!allowDiagonal && coordPair.IsDiagonal)
            continue;

        var curpos = new Coordinate(coordPair.start);
        while (true)
        {
            var val = ventMap.GetValueOrDefault(curpos.GetHashCode());
            ventMap[curpos.GetHashCode()] = val + 1;
            if (curpos == coordPair.end) break;
            curpos.MoveTowards(coordPair.end);
        };
    }
    return ventMap.Values.Where(x => x > 1).Count();
}

Console.WriteLine($"Test1: {solve(ventLines, false)}");
Console.WriteLine($"Test2: {solve(ventLines, true)}");

[DebuggerDisplay("({x},{y})")]
class Coordinate: IComparable<Coordinate>, IEquatable<Coordinate> {
    public int x, y;

    public Coordinate(int x, int y) { this.x = x; this.y = y; }
    public Coordinate(string startAndEnd) { var coords = startAndEnd.Split(",").Select(x => int.Parse(x)); this.x = coords.First(); this.y = coords.ElementAt(1); }
    public Coordinate(IEnumerable<int> coords) { this.x = coords.First(); this.y = coords.ElementAt(1); }
    public Coordinate(Coordinate coords) { this.x = coords.x; this.y = coords.y; }

    static private int CloseIn(int cur, int target)
    {
        var compared = target.CompareTo(cur);
        if (compared > 0) return cur + 1;
        if (compared < 0) return cur - 1;
        return cur;
    }

    public bool MoveTowards(Coordinate target)
    {
        if (target == this) return true;
        this.x = CloseIn(this.x, target.x);
        this.y = CloseIn(this.y, target.y);


        /*        if (Math.Abs(this.x - target.x) > Math.Abs(this.y - target.y))
                {
                    this.x = CloseIn(this.x, target.x);
                }
                else
                {
                    this.y = CloseIn(this.y, target.y);
                }
        */
        return target == this;
    }

    public int CompareTo(Coordinate? otherCoordinate)
    {
        if (otherCoordinate == null) return 1;
        
        var yCompare = otherCoordinate.y.CompareTo(this.y);
        if (yCompare != 0) return yCompare;
        return otherCoordinate.x.CompareTo(this.x);
    }

    public bool Equals(Coordinate? other)
    {
        return this.CompareTo(other) == 0;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj is Coordinate coordObj) return Equals(coordObj);
        return false;
    }

    public static bool operator == (Coordinate c1, Coordinate c2) {
        if (((object)c1) == null || ((object)c2) == null)
            return Object.Equals(c1, c2);
        return c1.Equals(c2);
    }
    public static bool operator != (Coordinate c1, Coordinate c2) {
        if (((object)c1) == null || ((object)c2) == null)
            return !(Object.Equals(c1, c2));
        return !(c1.Equals(c2));
    }

    public override int GetHashCode()
    {
        var hashCode = this.x << 16 | this.y;
        return hashCode;
    }
}

class Path
{
    public Coordinate start, end;

    public Path(Coordinate start, Coordinate end) { this.start = start; this.end = end; }
    public Path(string start, string end) { this.start = new Coordinate(start); this.end = new Coordinate(end); }
    public Path(IEnumerable<Coordinate> startAndEnd) { this.start = startAndEnd.First(); this.end = startAndEnd.ElementAt(1); }
    public Path(int startx, int starty, int endx, int endy) { this.start = new Coordinate(startx, starty); this.end = new Coordinate(endx, endy); }

    public bool IsHorizontal { get => start.x == end.x; }
    public bool IsVertical { get => start.y == end.y; }
    public bool IsDiagonal { get => !(this.IsVertical || this.IsHorizontal); }
}