
// Read file and convert to list of integers
var listOfMeasurements = System.IO.File.ReadLines(@"input1.txt")
    .Select(x => int.TryParse(x, out int i) ? i : (int?)null)
    .Where(x => x.HasValue)
    .Select(x => x.Value)
    .ToList();

/// <summary>
/// Process list and return number of items which had smaller precessors.
/// </summary>
int solveDay01(IEnumerable<int> input)
{
    var numberOfIncreasingValues =
        input.Aggregate((last: int.MaxValue, count: 0),
            (runningMemory, nextValue) =>
            {
                if (runningMemory.last < nextValue) { return (nextValue, runningMemory.count + 1); };
                return (nextValue, runningMemory.count);
            },
            result => result.count);

    return numberOfIncreasingValues;
}

/// <summary>
/// Create a list which creates a sliding window sum over 3 values.
/// </summary>
IEnumerable<int> creatingSlidingWindowList(IList<int> input)
{
    return input.SkipLast(2).Select((val, index) =>
        val + input[index + 1] + input[index + 2]
    );
}

Console.WriteLine($"Test1: Total: {listOfMeasurements.Count} Increasing: {solveDay01(listOfMeasurements)}");
Console.WriteLine($"Test2: Total: {listOfMeasurements.Count} Increasing: {solveDay01(creatingSlidingWindowList(listOfMeasurements))}");
