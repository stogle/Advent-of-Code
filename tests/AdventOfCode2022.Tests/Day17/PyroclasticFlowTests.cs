using AdventOfCode2022.Day17;

namespace AdventOfCode2022.Tests.Day17;

[TestClass]
public class PyroclasticFlowTests : DayTestsBase<PyroclasticFlow>
{
    #region DayNumber

    [TestMethod]
    [DataRow(17)]
    public override void DayNumber_Always_ReturnsExpectedResult(int expectedResult) =>
        base.DayNumber_Always_ReturnsExpectedResult(expectedResult);

    #endregion DayNumber

    #region PartOne

    [TestMethod]
    [DataRow(">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>", "3068")]
    public override void PartOne_WhenSampleInput_ReturnsExpectedResult(string sample, string expectedResult) =>
        base.PartOne_WhenSampleInput_ReturnsExpectedResult(sample, expectedResult);

    [TestMethod]
    [DataRow("3090")]
    public override void PartOne_WhenRealInput_ReturnsExpectedResult(string expectedResult) =>
        base.PartOne_WhenRealInput_ReturnsExpectedResult(expectedResult);

    #endregion PartOne

    #region PartTwo

    [TestMethod]
    [DataRow(">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>", "1514285714288")]
    public override void PartTwo_WhenSampleInput_ReturnsExpectedResult(string sample, string expectedResult) =>
        base.PartTwo_WhenSampleInput_ReturnsExpectedResult(sample, expectedResult);

    [TestMethod]
    [DataRow("1530057803453")]
    public override void PartTwo_WhenRealInput_ReturnsExpectedResult(string expectedResult) =>
        base.PartTwo_WhenRealInput_ReturnsExpectedResult(expectedResult);

    #endregion PartTwo
}
