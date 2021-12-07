// There should be a way to solve this algorithmically, but the extra period of 2 days after spawn throws me off.
// This still needs to step through every day, but runtime only depends on number of days, not number of fishes

using System.Diagnostics;
using System.Runtime.CompilerServices;

var crabArmy = System.IO.File.ReadLines(@"input7.txt")
    .Select(x => x.Split(",")).First()
    .Select(x => int.Parse(x))
    .ToList();

// Looks for the median on the list and sums up all differences for that median.
(int median, int fuel) solveTest1(List<int> crabs)
{
    var oCrabs = crabs.OrderBy(x => x);
    var median = oCrabs.ElementAt(oCrabs.Count() / 2);
    var fuel = oCrabs.Sum(x => Math.Abs(x - median));
    return (median, fuel);
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
int triangularNumber(int n)
{
    return n * (n + 1) / 2;
}

int solveTest2(List<int> crabs)
{
    var orderedCrabs = crabs.OrderBy(x => x).ToList();
    var bestFuelAverage = int.MaxValue;

#if false
    var bestFuelLoop = int.MaxValue;
    // Walk every position up to the maximum current position and sum up the triangular numbers (1,3,6,10,15 ...) 
    // Fast enough, but far from optimal.
    for (var position = 0; position <= orderedCrabs.Max(); position++)
    {
        var totalFuel = orderedCrabs.Sum(x => triangularNumber(Math.Abs(position - x)));
        // Console.WriteLine($"Pos: {position} total: {totalFuel} bestFuel: {bestFuel}");
        bestFuelLoop = Math.Min(bestFuelLoop, totalFuel);
    }
#else
    // Look around the average position to find the best position
    var startingPos = (int)(Math.Round(orderedCrabs.Average()));
    for (int i = -5 ; i < 5; i++)
    {
        var totalFuel = orderedCrabs.Sum(x => triangularNumber(Math.Abs(startingPos + i - x)));
        // Console.WriteLine($"Pos: {startingPos + i} TotalFuel: {totalFuel} BestFuel: {bestFuel}");
        bestFuelAverage = Math.Min(bestFuelAverage, totalFuel);
    }
#endif
//    Debug.Assert(bestFuelLoop == bestFuelAverage, $"{bestFuelLoop} should equal {bestFuelAverage}");

    return bestFuelAverage;
}

Console.WriteLine($"Test1: {solveTest1(crabArmy)}");
Console.WriteLine($"Test2: {solveTest2(crabArmy)}");