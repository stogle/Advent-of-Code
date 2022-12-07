using AdventOfCode2022.Day06;

namespace AdventOfCode2022.Tests.Day06;

[TestClass]
public class TuningTroubleTests : DayTestsBase<TuningTrouble>
{
    #region DayNumber

    [TestMethod]
    [DataRow(6)]
    public override void DayNumber_Always_ReturnsExpectedResult(int expectedResult) =>
        base.DayNumber_Always_ReturnsExpectedResult(expectedResult);

    #endregion DayNumber

    #region PartOne

    [TestMethod]
    // ReSharper disable once StringLiteralTypo
    [DataRow("mjqjpqmgbljsphdztnvjfqwrcgsmlb", "7")]
    public override void PartOne_WhenSampleInput_ReturnsExpectedResult(string sample, string expectedResult) =>
        base.PartOne_WhenSampleInput_ReturnsExpectedResult(sample, expectedResult);

    [TestMethod]
    [DataRow("1480")]
    public override void PartOne_WhenRealInput_ReturnsExpectedResult(string expectedResult) =>
        base.PartOne_WhenRealInput_ReturnsExpectedResult(expectedResult);

    #endregion PartOne

    #region PartTwo

    [TestMethod]
    // ReSharper disable once StringLiteralTypo
    [DataRow("mjqjpqmgbljsphdztnvjfqwrcgsmlb", "19")]
    public override void PartTwo_WhenSampleInput_ReturnsExpectedResult(string sample, string expectedResult) =>
        base.PartTwo_WhenSampleInput_ReturnsExpectedResult(sample, expectedResult);

    [TestMethod]
    [DataRow("2746")]
    public override void PartTwo_WhenRealInput_ReturnsExpectedResult(string expectedResult) =>
        base.PartTwo_WhenRealInput_ReturnsExpectedResult(expectedResult);

    #endregion PartTwo
}
