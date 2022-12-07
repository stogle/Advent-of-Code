using AdventOfCode2022.Day12;

namespace AdventOfCode2022.Tests.Day12;

[TestClass]
public class HillClimbingAlgorithmTests : DayTestsBase<HillClimbingAlgorithm>
{
    #region DayNumber

    [TestMethod]
    [DataRow(12)]
    public override void DayNumber_Always_ReturnsExpectedResult(int expectedResult) =>
        base.DayNumber_Always_ReturnsExpectedResult(expectedResult);

    #endregion DayNumber

    #region PartOne

    [TestMethod]
    // ReSharper disable StringLiteralTypo
    [DataRow("Sabqponm\r\nabcryxxl\r\naccszExk\r\nacctuvwj\r\nabdefghi", "31")]
    // ReSharper restore StringLiteralTypo
    public override void PartOne_WhenSampleInput_ReturnsExpectedResult(string sample, string expectedResult) =>
        base.PartOne_WhenSampleInput_ReturnsExpectedResult(sample, expectedResult);

    [TestMethod]
    [DataRow("504")]
    public override void PartOne_WhenRealInput_ReturnsExpectedResult(string expectedResult) =>
        base.PartOne_WhenRealInput_ReturnsExpectedResult(expectedResult);

    #endregion PartOne

    #region PartTwo

    [TestMethod]
    // ReSharper disable StringLiteralTypo
    [DataRow("Sabqponm\r\nabcryxxl\r\naccszExk\r\nacctuvwj\r\nabdefghi", "29")]
    // ReSharper restore StringLiteralTypo
    public override void PartTwo_WhenSampleInput_ReturnsExpectedResult(string sample, string expectedResult) =>
        base.PartTwo_WhenSampleInput_ReturnsExpectedResult(sample, expectedResult);

    [TestMethod]
    [DataRow("500")]
    public override void PartTwo_WhenRealInput_ReturnsExpectedResult(string expectedResult) =>
        base.PartTwo_WhenRealInput_ReturnsExpectedResult(expectedResult);

    #endregion PartTwo
}
