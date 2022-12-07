namespace AdventOfCode2022.Tests;

public abstract class DayTestsBase<T> where T : IDay, new()
{
    public virtual T CreateSampleInstance() => new();

    #region DayNumber

    public virtual void DayNumber_Always_ReturnsExpectedResult(int expectedResult)
    {
        // Arrange
        var day = new T();

        // Act
        int result = day.DayNumber;

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    #endregion DayNumber

    #region PartOne

    public virtual void PartOne_WhenSampleInput_ReturnsExpectedResult(string sample, string expectedResult)
    {
        // Arrange
        T day = CreateSampleInstance();
        using var input = new StringReader(sample);

        // Act
        string result = day.PartOne(input);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    public virtual void PartOne_WhenRealInput_ReturnsExpectedResult(string expectedResult)
    {
        // Arrange
        var day = new T();
        using var input = new StreamReader(File.OpenRead($"Day{day.DayNumber:00}\\input.txt"));

        // Act
        string result = day.PartOne(input);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    #endregion PartOne

    #region PartTwo

    public virtual void PartTwo_WhenSampleInput_ReturnsExpectedResult(string sample, string expectedResult)
    {
        // Arrange
        T day = CreateSampleInstance();
        using var input = new StringReader(sample);

        // Act
        string result = day.PartTwo(input);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    public virtual void PartTwo_WhenRealInput_ReturnsExpectedResult(string expectedResult)
    {
        // Arrange
        var day = new T();
        using var input = new StreamReader(File.OpenRead($"Day{day.DayNumber:00}\\input.txt"));

        // Act
        string result = day.PartTwo(input);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    #endregion PartTwo
}
