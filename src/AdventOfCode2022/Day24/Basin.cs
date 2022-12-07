namespace AdventOfCode2022.Day24;

internal sealed class Basin
{
    private readonly List<bool[]> _downBlizzards;
    private readonly List<bool[]> _rightBlizzards;
    private readonly List<bool[]> _upBlizzards;
    private readonly List<bool[]> _leftBlizzards;

    public Basin(List<bool[]> downBlizzards, List<bool[]> rightBlizzards, List<bool[]> upBlizzards, List<bool[]> leftBlizzards)
    {
        Width = downBlizzards[0].Length;
        Height = downBlizzards.Count;
        _downBlizzards = downBlizzards;
        _rightBlizzards = rightBlizzards;
        _upBlizzards = upBlizzards;
        _leftBlizzards = leftBlizzards;
    }

    public int Width { get; init; }

    public int Height { get; init; }

    public bool IsClear(int row, int column, int minute) =>
        0 <= row && row < Height && 0 <= column && column < Width &&
        !_downBlizzards[(row + (Height - minute % Height)) % Height][column] &&
        !_rightBlizzards[row][(column + (Width - minute % Width)) % Width] &&
        !_upBlizzards[(row + minute) % Height][column] &&
        !_leftBlizzards[row][(column + minute) % Width];

    public char GetCharacter(int row, int column, int minute)
    {
        bool down = _downBlizzards[(row + (Height - minute % Height)) % Height][column];
        bool right = _rightBlizzards[row][(column + (Width - minute % Width)) % Width];
        bool up = _upBlizzards[(row + minute) % Height][column];
        bool left = _leftBlizzards[row][(column + minute) % Width];
        int count = (down ? 1 : 0) + (right ? 1 : 0) + (up ? 1 : 0) + (left ? 1 : 0);

        if (count > 1)
        {
            return (char)('0' + count);
        }

        if (down)
        {
            return 'v';
        }

        if (right)
        {
            return '>';
        }

        if (up)
        {
            return '^';
        }

        if (left)
        {
            return '<';
        }

        return '.';
    }
}
