namespace AdventOfCode2022.Day22;

internal delegate (int Row, int Column, Facing Facing) Wrap(Map map, int row, int column, Facing facing);

internal sealed class Map
{
    private readonly IList<string> _map;

    public Map(IList<string> map)
    {
        _map = map;
        Row = 1;
        Column = 1 + _map[0].Count(c => c == ' ');
        Facing = Facing.Right;
        WrapStrategy = (m, row, column, facing) => m.Wrap2D(row, column, facing);
    }

    public int Row { get; private set; }
    
    public int Column { get; private set; }
    
    public Facing Facing { get; private set; }

    public int Height => _map.Count;

    public Wrap WrapStrategy { get; set; }

    public void Forward(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (!ForwardOne())
            {
                break;
            }
        }
    }

    private bool ForwardOne()
    {
        int row = Row;
        int column = Column;
        Facing facing = Facing;
        switch (Facing)
        {
            case Facing.Right:
                column++;
                break;
            case Facing.Down:
                row++;
                break;
            case Facing.Left:
                column--;
                break;
            case Facing.Up:
                row--;
                break;
            default:
                throw new InvalidOperationException();
        }
        (row, column, facing) = WrapStrategy(this, row, column, facing);

        if (_map[row - 1][column - 1] == '#')
        {
            return false;
        }

        Row = row;
        Column = column;
        Facing = facing;
        return true;
    }

    private (int Row, int Column, Facing Facing) Wrap2D(int row, int column, Facing facing)
    {
        switch (facing)
        {
            case Facing.Right:
                if (column > _map[row - 1].Length)
                {
                    column = 1 + _map[row - 1].TakeWhile(c => c == ' ').Count();
                }
                break;
            case Facing.Down:
                if (row > _map.Count || column > _map[row - 1].Length || _map[row - 1][column - 1] == ' ')
                {
                    row = 1 + _map.TakeWhile(r => r[column - 1] == ' ').Count();
                }
                break;
            case Facing.Left:
                if (column < 1 || _map[row - 1][column - 1] == ' ')
                {
                    column = _map[row - 1].Length;
                }
                break;
            case Facing.Up:
                if (row < 1 || _map[row - 1][column - 1] == ' ')
                {
                    row = _map.Count - _map.Reverse().TakeWhile(r => column >= r.Length || r[column - 1] == ' ').Count();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(facing), facing, null);
        }

        return (row, column, facing);
    }

    public void Left() =>
        Facing = Facing switch
        {
            Facing.Right => Facing.Up,
            Facing.Down => Facing.Right,
            Facing.Left => Facing.Down,
            Facing.Up => Facing.Left,
            _ => throw new ArgumentOutOfRangeException()
        };

    public void Right() =>
        Facing = Facing switch
        {
            Facing.Right => Facing.Down,
            Facing.Down => Facing.Left,
            Facing.Left => Facing.Up,
            Facing.Up => Facing.Right,
            _ => throw new ArgumentOutOfRangeException()
        };

    public override string ToString()
    {
        char c = Facing switch
        {
            Facing.Right => '>',
            Facing.Down => 'v',
            Facing.Left => '<',
            Facing.Up => '^',
            _ => throw new ArgumentOutOfRangeException()
        };

        return string.Join("\r\n", _map
            .Select((r, i) => i == Row - 1 ? r[..(Column - 1)] + c + r[Column..] : r));
    }
}
