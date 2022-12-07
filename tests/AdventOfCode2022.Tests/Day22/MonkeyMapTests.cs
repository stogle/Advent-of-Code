using AdventOfCode2022.Day22;

namespace AdventOfCode2022.Tests.Day22;

[TestClass]
public class MonkeyMapTests : DayTestsBase<MonkeyMap>
{
    #region DayNumber

    [TestMethod]
    [DataRow(22)]
    public override void DayNumber_Always_ReturnsExpectedResult(int expectedResult) =>
        base.DayNumber_Always_ReturnsExpectedResult(expectedResult);

    #endregion DayNumber

    #region PartOne

    [TestMethod]
    [DataRow("        ...#\r\n        .#..\r\n        #...\r\n        ....\r\n...#.......#\r\n........#...\r\n..#....#....\r\n..........#.\r\n        ...#....\r\n        .....#..\r\n        .#......\r\n        ......#.\r\n\r\n10R5L5R10L4R5L5", "6032")]
    public override void PartOne_WhenSampleInput_ReturnsExpectedResult(string sample, string expectedResult) =>
        base.PartOne_WhenSampleInput_ReturnsExpectedResult(sample, expectedResult);

    [TestMethod]
    [DataRow("106094")]
    public override void PartOne_WhenRealInput_ReturnsExpectedResult(string expectedResult) =>
        base.PartOne_WhenRealInput_ReturnsExpectedResult(expectedResult);

    #endregion PartOne

    #region PartTwo

    [TestMethod]
    [DataRow("        ...#\r\n        .#..\r\n        #...\r\n        ....\r\n...#.......#\r\n........#...\r\n..#....#....\r\n..........#.\r\n        ...#....\r\n        .....#..\r\n        .#......\r\n        ......#.\r\n\r\n10R5L5R10L4R5L5", "5031")]
    public override void PartTwo_WhenSampleInput_ReturnsExpectedResult(string sample, string expectedResult) =>
        base.PartTwo_WhenSampleInput_ReturnsExpectedResult(sample, expectedResult);

    [TestMethod]
    [DataRow("162038")]
    public override void PartTwo_WhenRealInput_ReturnsExpectedResult(string expectedResult) =>
        base.PartTwo_WhenRealInput_ReturnsExpectedResult(expectedResult);

    #endregion PartTwo
}
