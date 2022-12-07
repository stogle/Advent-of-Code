using AdventOfCode2022.Day14;

namespace AdventOfCode2022.Tests.Day14;

[TestClass]
public class RegolithReservoirTests : DayTestsBase<RegolithReservoir>
{
    #region DayNumber

    [TestMethod]
    [DataRow(14)]
    public override void DayNumber_Always_ReturnsExpectedResult(int expectedResult) => base.DayNumber_Always_ReturnsExpectedResult(expectedResult);

    #endregion DayNumber

    #region PartOne

    [TestMethod]
    [DataRow("498,4 -> 498,6 -> 496,6\r\n503,4 -> 502,4 -> 502,9 -> 494,9", "24")]
    public override void PartOne_WhenSampleInput_ReturnsExpectedResult(string sample, string expectedResult) => base.PartOne_WhenSampleInput_ReturnsExpectedResult(sample, expectedResult);

    [TestMethod]
    [DataRow("858")]
    public override void PartOne_WhenRealInput_ReturnsExpectedResult(string expectedResult) => base.PartOne_WhenRealInput_ReturnsExpectedResult(expectedResult);

    #endregion PartOne

    #region PartTwo

    [TestMethod]
    [DataRow("498,4 -> 498,6 -> 496,6\r\n503,4 -> 502,4 -> 502,9 -> 494,9", "93")]
    public override void PartTwo_WhenSampleInput_ReturnsExpectedResult(string sample, string expectedResult) => base.PartTwo_WhenSampleInput_ReturnsExpectedResult(sample, expectedResult);

    [TestMethod]
    [DataRow("26845")]
    public override void PartTwo_WhenRealInput_ReturnsExpectedResult(string expectedResult) => base.PartTwo_WhenRealInput_ReturnsExpectedResult(expectedResult);

    #endregion PartTwo
}
