using System.Text;

namespace AdventOfCode2022.Day23;

internal sealed class Region
{
    private const int North = 0;
    private const int South = 1;
    private const int West = 2;
    private const int East = 3;
    private const int DirectionCount = 4;

    private readonly HashSet<(int Row, int Column)> _elves = new();
    private int _nextProposedDirection = North;

    public void AddElf((int Row, int Column) elf) =>
        _elves.Add(elf);

    public void RemoveElf((int Row, int Column) elf) =>
        _elves.Remove(elf);

    public IDictionary<(int Row, int Column), (int Row, int Column)> ProposeMoves()
    {
        var proposals = new Dictionary<(int Row, int Column), (int Row, int Column)>();
        foreach ((int row, int column) in _elves)
        {
            bool elfNW = _elves.Contains((row - 1, column - 1));
            bool elfN = _elves.Contains((row - 1, column));
            bool elfNE = _elves.Contains((row - 1, column + 1));
            bool elfW = _elves.Contains((row, column - 1));
            bool elfE = _elves.Contains((row, column + 1));
            bool elfSW = _elves.Contains((row + 1, column - 1));
            bool elfS = _elves.Contains((row + 1, column));
            bool elfSE = _elves.Contains((row + 1, column + 1));

            // If no other Elves are in one of those eight positions, the Elf does not do anything during this round
            if (!(elfNW || elfN || elfNE || elfW || elfE || elfSW || elfS || elfSE))
            {
                continue;
            }

            for (int i = North; i <= East; i++)
            {
                int proposedDirection = (_nextProposedDirection + i) % DirectionCount;
                if (proposedDirection == North && !elfN && !elfNE && !elfNW)
                {
                    proposals.Add((row, column), (row - 1, column));
                    break;
                }

                if (proposedDirection == South && !elfS && !elfSE && !elfSW)
                {
                    proposals.Add((row, column), (row + 1, column));
                    break;
                }

                if (proposedDirection == West && !elfW && !elfNW && !elfSW)
                {
                    proposals.Add((row, column), (row, column - 1));
                    break;
                }

                if (proposedDirection == East && !elfE && !elfNE && !elfSE)
                {
                    proposals.Add((row, column), (row, column + 1));
                    break;
                }
            }
        }

        return proposals;
    }

    public void Move(IDictionary<(int Row, int Column), (int Row, int Column)> proposals)
    {
        // If two or move Elves propose moving to the same position, none of those Elves move
        IEnumerable<KeyValuePair<(int Row, int Column), (int Row, int Column)>> moves = proposals
            .GroupBy(entry => entry.Value)
            .Where(group => group.Count() == 1)
            .Select(group => group.First());

        foreach (KeyValuePair<(int Row, int Column), (int Row, int Column)> move in moves)
        {
            RemoveElf(move.Key);
            AddElf(move.Value);
        }

        // At the end of the round, the first direction the Elves considered is moved to the end of the list of directions
        _nextProposedDirection = (_nextProposedDirection + 1) % DirectionCount;
    }

    private (int RowMin, int RowMax, int ColumnMin, int ColumnMax) GetBounds()
    {
        int rowMin = int.MaxValue;
        int rowMax = int.MinValue;
        int columnMin = int.MaxValue;
        int columnMax = int.MinValue;
        foreach ((int row, int column) in _elves)
        {
            rowMin = Math.Min(rowMin, row);
            rowMax = Math.Max(rowMax, row);
            columnMin = Math.Min(columnMin, column);
            columnMax = Math.Max(columnMax, column);
        }

        return (rowMin, rowMax, columnMin, columnMax);
    }

    public int EmptyTileCount()
    {
        (int rowMin, int rowMax, int columnMin, int columnMax) = GetBounds();
        return (rowMax - rowMin + 1) * (columnMax - columnMin + 1) - _elves.Count;
    }

    public override string ToString()
    {
        var result = new StringBuilder();
        (int rowMin, int rowMax, int columnMin, int columnMax) = GetBounds();
        for (int row = rowMin; row <= rowMax; row++)
        {
            for (int column = columnMin; column <= columnMax; column++)
            {
                result.Append(_elves.Contains((row, column)) ? '#' : '.');
            }

            result.AppendLine();
        }

        return result.ToString();
    }
}
