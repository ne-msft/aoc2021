// There should be a way to solve this algorithmically, but the extra period of 2 days after spawn throws me off.
// This still needs to step through every day, but runtime only depends on number of days, not number of fishes

using System.Diagnostics;

var lanternFish = System.IO.File.ReadLines(@"sample6.txt")
    .Select(x => x.Split(",")).First()
    .Select(x => int.Parse(x))
    .ToList();

const int breedTime = 7;
const int breedPrep = 2;

Int64 solve(List<int> initialFish, int days)
{
    var fishies = new Int64[breedTime + breedPrep];

    foreach (var fish in lanternFish)
    {
        fishies[fish] += 1;
    }

    for (var day = 0; day < days; day++)
    {
#if true
        // Only look at the fishes breeding today and add new fishes to where they will be looked at
        fishies[(day + breedTime) % (breedTime + breedPrep)] += fishies[day % (breedTime + breedPrep)];
        Console.WriteLine($"Day: {day} Fishies: {fishies.Aggregate("", (last, next) => last + "," + next.ToString())}");

#else
        /* This is a rather simple solution, it creates a new list of how many fish will reproduce in X days. For every single day.
         * This is O(1), so already fast enough. */
        var nextFishies = new Int64[fishies.Count()];
        for (var i = 0; i < breedTime + breedPrep; i++)
        {
            if (i == 0)
            {
                nextFishies[breedTime + breedPrep - 1] = fishies[i];
                nextFishies[breedTime - 1] = fishies[i];
            }
            else
            {
                nextFishies[i - 1] += fishies[i];
            }
        }
        fishies = nextFishies;
#endif
    }
    return fishies.Sum();
}

Console.WriteLine($"Test1: 80 days {solve(lanternFish, 80)}");
Console.WriteLine($"Test2: 256 days {solve(lanternFish, 256)}");
