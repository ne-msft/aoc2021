
// Read file
var listDirections = System.IO.File.ReadLines(@"input2.txt")
    .Select(x => {
        var items = x.Split(" ");
        return (direction: items[0].ToLower(), value: int.Parse(items[1]));
    });

SimpleSubmarine simpleSubmarine = new SimpleSubmarine();
AimingSubmarine aimingSubmarine = new AimingSubmarine();

foreach( var direction in listDirections)
{
    simpleSubmarine.FollowDirection(direction.direction, direction.value);
    aimingSubmarine.FollowDirection(direction.direction, direction.value);
}

Console.WriteLine($"Test1: Total: {listDirections.Count()} FinalPosition: ({simpleSubmarine.Position}) Result:{simpleSubmarine.depth * simpleSubmarine.horizontal}");
Console.WriteLine($"Test2: Total: {listDirections.Count()} FinalPosition: ({aimingSubmarine.Position}) Result:{aimingSubmarine.depth * aimingSubmarine.horizontal}");

class AimingSubmarine
{
    public int horizontal { get; set; }
    public int depth { get; set; }

    private int aim { get; set; }

    public string Position { get { return $"Horizontal: {horizontal} Depth: {depth} Aim: {aim}"; } }

    public AimingSubmarine(int horizontal = 0, int depth = 0)
    {
        this.horizontal = horizontal;
        this.depth = depth;
    }

    public void FollowDirection(string direction, int val)
    {
        switch (direction)
        {
            case "forward":
                horizontal += val;
                depth += aim * val;
                break;
            case "up":
                aim -= val; break;
            case "down":
                aim += val; break;
        }
    }

}

class SimpleSubmarine
{
    public int horizontal { get; set; }
    public int depth { get; set; }

    public string Position { get { return $"Horizontal: {horizontal} Depth: {depth}"; } }

    public SimpleSubmarine(int horizontal = 0, int depth = 0)
    {
        this.horizontal = horizontal;
        this.depth = depth;
    }

    public void FollowDirection(string direction, int val)
    {
        switch (direction)
        {
            case "forward":
                horizontal += val; break;
            case "up":
                depth -= val; break;
            case "down":
                depth += val; break;
        }
    }
} 
