using System.Globalization;

namespace AdventOfCode2022.Day07;

public class NoSpaceLeftOnDevice : IDay
{
    public int DayNumber => 7;

    public string PartOne(TextReader input)
    {
        Directory root = ReadInput(input);

        long result = root.GetDescendants()
            .OfType<Directory>()
            .Select(d => d.GetSize())
            .Where(s => s <= 100_000)
            .Sum();

        return result.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        Directory root = ReadInput(input);

        long remaining = 70_000_000 - root.GetSize();
        long required = 30_000_000 - remaining;
        long result = root.GetDescendants()
            .OfType<Directory>()
            .Select(d => d.GetSize())
            .Order()
            .First(s => s >= required);

        return result.ToString(CultureInfo.InvariantCulture);
    }

    private static Directory ReadInput(TextReader input)
    {
        var root = new Directory { Name = "/" };
        Directory cd = root;
        while (input.ReadLine() is { } line)
        {
            string[] split = line.Split(' ');
            if (split[0] == "$") // command
            {
                switch (split[1])
                {
                    case "cd":
                        string name = split[2];
                        cd = split[2] switch
                        {
                            "/" => root,
                            ".." => cd.Parent!,
                            _ => cd.Children.OfType<Directory>().First(c => c.Name == name)
                        };
                        break;
                    case "ls":
                        // Do nothing
                        break;
                }
            }
            else // ls output
            {
                string name = split[1];

                Node child;
                if (split[0] == "dir")
                {
                    child = new Directory { Name = name };
                }
                else
                {
                    long size = long.Parse(split[0], CultureInfo.InvariantCulture);
                    child = new File { Name = name, Size = size };
                }

                child.Parent = cd;
            }
        }

        return root;
    }
}
