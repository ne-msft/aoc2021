
// Read file, convert string binary to 32 bit integers
var bingoBoardInput = System.IO.File.ReadLines(@"input4.txt");
var bingoBoardEnumerator = bingoBoardInput.GetEnumerator();
bingoBoardEnumerator.MoveNext();
var drawnNumbers = bingoBoardEnumerator.Current.Split(",").Select(x => int.Parse(x)).ToList();
bingoBoardEnumerator.MoveNext();

var bingoBoards = new List<IList<ISet<int>>>();

while (bingoBoardEnumerator.MoveNext())
{
    var bingoBoard = new List<ISet<int>>();
    // Every row and column is an unordered set of numbers
    for (int i = 0; i < 5; i++)
    {
        bingoBoard.Add(new HashSet<int>());
    }
    while(bingoBoardEnumerator.Current != "")
    {
        var values = bingoBoardEnumerator
            .Current
            .Trim()
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x)).ToList();
        bingoBoard.Add(new HashSet<int>(values));
        for (int i = 0; i < 5; i++)
        {
            bingoBoard[i].Add(values[i]);
        }
        if (bingoBoardEnumerator.MoveNext() == false) break;
    }
    bingoBoards.Add(bingoBoard);
}

int sumRemaining(IList<ISet<int>> board)
{
    // The first 5 sets are a represenation of our colums, we can just add them together
    return board
        .Take(5)
        .Sum(x => x.Sum());
}

(int test1, int test2) solveTest1And2(IList<IList<ISet<int>>> boards, IList<int> drawnNumbers)
{
    int result1 = -1, result2 = -1;
    // Keep a list of boards we already solved.
    var solved = new HashSet<IList<ISet<int>>>();

    foreach (var number in drawnNumbers)
    {
        if (result2 != -1) break;
        foreach(var board in boards)
        {
            // Do not check an already solved board
            if (solved.Contains(board)) continue;

            foreach(var rowOrCol in board)
            {
                if (rowOrCol.Contains(number))
                {
                    rowOrCol.Remove(number);
                    if (rowOrCol.Count == 0)
                    {
                        solved.Add(board);
                        if (result1 == -1)
                        {
                            result1 = sumRemaining(board) * number;
                        }
                        if (solved.Count == boards.Count)
                        {
                            result2 = sumRemaining(board) * number;
                        }
                        break;
                    }
                }
            }
        }
    }
    return (result1, result2);
}


Console.WriteLine($"Test1: {solveTest1And2(bingoBoards, drawnNumbers)}");
// Console.WriteLine($"Test2: Total: {diagnosticsReport.Count()} values: {task2Result}");

