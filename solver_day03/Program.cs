
// Read file, convert string binary to 32 bit integers
var diagnosticsReport = System.IO.File.ReadLines(@"input3.txt")
    .Select(x => x.Trim())
    .ToList();

(string gamma, string epsilon, int result) solve_day3_task1(IEnumerable<string> input)
{
    var stringLen = input.First().Length;
    int[] counters = new int[stringLen];

    foreach (var str in input)
    {
        int index = 0;
        foreach (var c in str)
        {
            counters[index] += c == '1' ? 1 : -1;
            index++;
        }
    }

    // epsilon is just the inverse of gamma, but taking bitlenght into account.
    // For the sample gamma + epsilon = 31
    var gamma = "";
    var epsilon = "";
    foreach (var val in counters)
    {
        if (val > 0)
        {
            gamma += '1';
            epsilon += '0';
        } else
        {
            gamma += '0';
            epsilon += '1';
        }
    }
    return (gamma, epsilon, Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2));
}

string findReport(IList<string> input, bool mostCommon, char bias)
{
    var bitLen = input.First().Length;
    var processList = input.ToList();
    for (int i = 0; i < bitLen; i++)
    {
        var zeroList = processList.Where(x => x[i] == '0').ToList();
        var onesList = processList.Where(x => x[i] == '1').ToList();

        if (zeroList.Count == onesList.Count)
        {
            processList = bias == '0' ? zeroList : onesList;
        }
        else if (zeroList.Count > onesList.Count == mostCommon)
        {
            processList = zeroList;
        }
        else
        {
            processList = onesList;
        }

        if (processList.Count == 1)
        {
            return processList.First();
        }
    }
    return null;
}

// Gamma is the path to oxygen
// epsilon is the path to co2Scrubber
(string o2Generator, string co2Scrubber, int result) solve_day3_task2(IEnumerable<string> input)
{
    var o2Generator = findReport(input.ToList(), true, '1');
    var co2Scrubber = findReport(input.ToList(), false, '0');
    return (o2Generator, co2Scrubber, Convert.ToInt32(o2Generator, 2) * Convert.ToInt32(co2Scrubber, 2));
}

var task1Result = solve_day3_task1(diagnosticsReport);
var task2Result = solve_day3_task2(diagnosticsReport);

Console.WriteLine($"Test1: Total: {diagnosticsReport.Count()} values: {task1Result}");
Console.WriteLine($"Test2: Total: {diagnosticsReport.Count()} values: {task2Result}");

