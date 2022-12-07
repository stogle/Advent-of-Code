using System.Text;

namespace AdventOfCode2022.Day17;

internal sealed class Chamber
{
    private const int Width = 7;

    private readonly bool[][][] _rocks;
    private readonly string _jetPattern;

    private int _rockIndex;
    private int _jetPatternIndex;
    private readonly List<bool[]> _rows = new();
    private bool[][]? _currentShape;
    private int _currentShapeX;
    private int _currentShapeY;
    private readonly Dictionary<(int RockIndex, int JetPatternIndex, int MaxDrop), (long RockIndex, int Height)> _states = new();

    public Chamber(bool[][][] rocks, string jetPattern)
    {
        _rocks = rocks;
        _jetPattern = jetPattern;
    }

    public void Add()
    {
        _currentShape = _rocks[_rockIndex];
        _currentShapeX = 2; // Each rock appears so that its left edge is two units away from the left wall
        _currentShapeY = _rows.Count + 3; // ...and its bottom edge is three units above the highest rock in the room

        _rows.Add(new bool[Width]);
        _rows.Add(new bool[Width]);
        _rows.Add(new bool[Width]);
        foreach (bool[] shapeRow in _currentShape)
        {
            bool[] row = new bool[Width];
            Array.Copy(shapeRow, 0, row, _currentShapeX, shapeRow.Length);
            _rows.Add(row);
        }
        _rockIndex = (_rockIndex + 1) % _rocks.Length;
    }

    public void Drop()
    {
        do
        {
            switch (_jetPattern[_jetPatternIndex])
            {
                case '<':
                    MoveLeft();
                    break;
                case '>':
                    MoveRight();
                    break;
                default:
                    throw new InvalidOperationException();
            }
            _jetPatternIndex = (_jetPatternIndex + 1) % _jetPattern.Length;
        } while (MoveDown());
    }

    private void MoveLeft()
    {
        if (_currentShapeX <= 0)
        {
            return;
        }

        // Check for obstructions
        for (int i = 0; i < _currentShape!.Length; i++)
        {
            int index = Array.IndexOf(_currentShape[i], true);
            if (_rows[_currentShapeY + i][_currentShapeX + index - 1])
            {
                return;
            }
        }

        // Update
        EraseCurrentShape();
        _currentShapeX--;
        DrawCurrentShape();
    }

    private void MoveRight()
    {
        if (_currentShapeX + _currentShape![0].Length >= Width)
        {
            return;
        }

        // Check for obstructions
        for (int i = 0; i < _currentShape.Length; i++)
        {
            int index = Array.LastIndexOf(_currentShape[i], true);
            if (_rows[_currentShapeY + i][_currentShapeX + index + 1])
            {
                return;
            }
        }

        // Update
        EraseCurrentShape();
        _currentShapeX++;
        DrawCurrentShape();
    }

    private bool MoveDown()
    {
        if (_currentShapeY <= 0)
        {
            return false;
        }

        // Check for obstructions
        for (int j = 0; j < _currentShape![0].Length; j++)
        {
            int index = _currentShape.Select(shapeRow => shapeRow[j]).TakeWhile(p => !p).Count();
            if (_rows[_currentShapeY + index - 1][_currentShapeX + j])
            {
                return false;
            }
        }

        // Update
        EraseCurrentShape();
        _currentShapeY--;
        DrawCurrentShape();
        if (!_rows.Last().Any(p => p))
        {
            _rows.RemoveAt(_rows.Count - 1);
        }

        return true;

    }

    private void EraseCurrentShape()
    {
        for (int i = 0; i < _currentShape!.Length; i++)
        {
            for (int j = 0; j < _currentShape[i].Length; j++)
            {
                if (_currentShape[i][j])
                {
                    _rows[_currentShapeY + i][_currentShapeX + j] = false;
                }
            }
        }
    }

    private void DrawCurrentShape()
    {
        for (int i = 0; i < _currentShape!.Length; i++)
        {
            for (int j = 0; j < _currentShape[i].Length; j++)
            {
                if (_currentShape[i][j])
                {
                    _rows[_currentShapeY + i][_currentShapeX + j] = true;
                }
            }
        }
    }

    public int Height => _rows.Count;

    public bool TryDetectCycle(long rockIndex, out long cycleLength, out long cycleHeight)
    {
        int maxDrop = Enumerable.Range(0, Width)
            .Max(column => ((IEnumerable<bool[]>)_rows).Reverse().TakeWhile(r => !r[column]).Count());
        (int _rockIndex, int _jetPatternIndex, int maxDrop) state = (_rockIndex, _jetPatternIndex, maxDrop);
        if (_states.TryGetValue(state, out (long RockIndex, int Height) value))
        {
            cycleLength = rockIndex - value.RockIndex;
            cycleHeight = Height - value.Height;
            return true;
        }

        _states.Add(state, (rockIndex, Height));
        cycleLength = 0;
        cycleHeight = 0;
        return false;
    }

    public override string ToString()
    {
        var result = new StringBuilder();
        for (int i = _rows.Count - 1; i >= 0; i--)
        {
            result.Append('|');
            for (int j = 0; j < _rows[i].Length; j++)
            {
                result.Append(_rows[i][j] ? '#' : '.');
            }
            result.AppendLine("|");
        }

        result.Append('+');
        result.Append(new string('-', Width));
        result.AppendLine("+");
        return result.ToString();
    }
}
