using System.Text;

namespace AdventOfCode2022.Day24;

internal sealed record State(Basin Basin, int Row, int Column, int Minute, State? Previous = null, string? Direction = null)
{
    public static IEqualityComparer<State> EqualityComparer = new StateEqualityComparer();

    public IEnumerable<State> GetNextStates()
    {
        // Down
        for (int i = 1, n = Basin.Width * Basin.Height; i <= n; i++)
        {
            if (Basin.IsClear(Row + 1, Column, Minute + i))
            {
                yield return this with { Row = Row + 1, Minute = Minute + i, Previous = this, Direction = "down" };
            }
            if (Basin.IsClear(Row, Column + 1, Minute + i))
            {
                yield return this with { Column = Column + 1, Minute = Minute + i, Previous = this, Direction = "right" };
            }
            if (Basin.IsClear(Row - 1, Column, Minute + i))
            {
                yield return this with { Row = Row - 1, Minute = Minute + i, Previous = this, Direction = "up" };
            }
            if (Basin.IsClear(Row, Column - 1, Minute + i))
            {
                yield return this with { Column = Column - 1, Minute = Minute + i, Previous = this, Direction = "left" };
            }

            // Can't wait here any longer or yielded all possible directions
            if (!(Row < 0 || Row >= Basin.Height || Basin.IsClear(Row, Column, Minute + i)))
            {
                yield break;
            }
        }
    }

    public override string ToString()
    {
        var result = new StringBuilder();
        if (Previous == null)
        {
            result.AppendLine("Initial state:");
        }
        else
        {
            result.AppendLine(Previous.ToString());
            result.Append("Minute ").Append(Minute).Append(", move ").Append(Direction).AppendLine(":");
        }
        result.Append('#').Append(Row == -1 ? 'E' : '.').AppendLine(new string('#', Basin.Width));
        for (int r = 0; r < Basin.Height; r++)
        {
            result.Append('#');
            for (int c = 0; c < Basin.Width; c++)
            {
                result.Append(Row == r && Column == c ? 'E' : Basin.GetCharacter(r, c, Minute));
            }

            result.AppendLine("#");
        }
        result.Append(new string('#', Basin.Width)).Append(Row == Basin.Height ? 'E' : '.').AppendLine("#");
        return result.ToString();
    }

    private sealed class StateEqualityComparer : IEqualityComparer<State>
    {
        public bool Equals(State? x, State? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null))
            {
                return false;
            }

            if (ReferenceEquals(y, null))
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            return x.Basin.Equals(y.Basin) && x.Row == y.Row && x.Column == y.Column;
        }

        public int GetHashCode(State obj)
        {
            return HashCode.Combine(obj.Basin, obj.Row, obj.Column);
        }
    }
}
