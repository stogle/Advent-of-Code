using AdventOfCode2022.Day20;

namespace AdventOfCode2022.Tests.Day20;

[TestClass]
public class GrovePositioningSystemTests : DayTestsBase<GrovePositioningSystem>
{
    #region DayNumber

    [TestMethod]
    [DataRow(20)]
    public override void DayNumber_Always_ReturnsExpectedResult(int expectedResult) =>
        base.DayNumber_Always_ReturnsExpectedResult(expectedResult);

    #endregion DayNumber

    #region PartOne

    [TestMethod]
    [DataRow("1\r\n2\r\n-3\r\n3\r\n-2\r\n0\r\n4", "3")]
    public override void PartOne_WhenSampleInput_ReturnsExpectedResult(string sample, string expectedResult) =>
        base.PartOne_WhenSampleInput_ReturnsExpectedResult(sample, expectedResult);

    [TestMethod]
    [DataRow("13522")]
    public override void PartOne_WhenRealInput_ReturnsExpectedResult(string expectedResult) =>
        base.PartOne_WhenRealInput_ReturnsExpectedResult(expectedResult);

    #endregion PartOne

    #region PartTwo

    [TestMethod]
    [DataRow("1\r\n2\r\n-3\r\n3\r\n-2\r\n0\r\n4", "1623178306")]
    public override void PartTwo_WhenSampleInput_ReturnsExpectedResult(string sample, string expectedResult) =>
        base.PartTwo_WhenSampleInput_ReturnsExpectedResult(sample, expectedResult);

    [TestMethod]
    [DataRow("17113168880158")]
    public override void PartTwo_WhenRealInput_ReturnsExpectedResult(string expectedResult) =>
        base.PartTwo_WhenRealInput_ReturnsExpectedResult(expectedResult);

    #endregion PartTwo
}
