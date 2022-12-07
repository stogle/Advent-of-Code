using AdventOfCode2022.Day13;

namespace AdventOfCode2022.Tests.Day13;

[TestClass]
public class DistressSignalTests : DayTestsBase<DistressSignal>
{
    #region DayNumber

    [TestMethod]
    [DataRow(13)]
    public override void DayNumber_Always_ReturnsExpectedResult(int expectedResult) =>
        base.DayNumber_Always_ReturnsExpectedResult(expectedResult);

    #endregion DayNumber

    #region PartOne

    [TestMethod]
    [DataRow("[1,1,3,1,1]\r\n[1,1,5,1,1]\r\n\r\n[[1],[2,3,4]]\r\n[[1],4]\r\n\r\n[9]\r\n[[8,7,6]]\r\n\r\n[[4,4],4,4]\r\n[[4,4],4,4,4]\r\n\r\n[7,7,7,7]\r\n[7,7,7]\r\n\r\n[]\r\n[3]\r\n\r\n[[[]]]\r\n[[]]\r\n\r\n[1,[2,[3,[4,[5,6,7]]]],8,9]\r\n[1,[2,[3,[4,[5,6,0]]]],8,9]", "13")]
    public override void PartOne_WhenSampleInput_ReturnsExpectedResult(string sample, string expectedResult) => base.PartOne_WhenSampleInput_ReturnsExpectedResult(sample, expectedResult);

    [TestMethod]
    [DataRow("5390")]
    public override void PartOne_WhenRealInput_ReturnsExpectedResult(string expectedResult) => base.PartOne_WhenRealInput_ReturnsExpectedResult(expectedResult);

    #endregion PartOne

    #region PartTwo

    [TestMethod]
    [DataRow("[1,1,3,1,1]\r\n[1,1,5,1,1]\r\n\r\n[[1],[2,3,4]]\r\n[[1],4]\r\n\r\n[9]\r\n[[8,7,6]]\r\n\r\n[[4,4],4,4]\r\n[[4,4],4,4,4]\r\n\r\n[7,7,7,7]\r\n[7,7,7]\r\n\r\n[]\r\n[3]\r\n\r\n[[[]]]\r\n[[]]\r\n\r\n[1,[2,[3,[4,[5,6,7]]]],8,9]\r\n[1,[2,[3,[4,[5,6,0]]]],8,9]", "140")]
    public override void PartTwo_WhenSampleInput_ReturnsExpectedResult(string sample, string expectedResult) => base.PartTwo_WhenSampleInput_ReturnsExpectedResult(sample, expectedResult);

    [TestMethod]
    [DataRow("19261")]
    public override void PartTwo_WhenRealInput_ReturnsExpectedResult(string expectedResult) => base.PartTwo_WhenRealInput_ReturnsExpectedResult(expectedResult);

    #endregion PartTwo
}
