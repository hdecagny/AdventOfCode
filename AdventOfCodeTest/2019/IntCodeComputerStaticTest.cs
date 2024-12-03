namespace AdventOfCodeTest._2019;

using AdventOfCode2019;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class IntCodeComputerStaticTest
{
    [TestMethod]
    [DataRow(1, 1, ParameterMode.PositionMode, ParameterMode.PositionMode)]
    [DataRow(2, 2, ParameterMode.PositionMode, ParameterMode.PositionMode)]
    [DataRow(10, 10, ParameterMode.PositionMode, ParameterMode.PositionMode)]
    [DataRow(102, 2, ParameterMode.ImmediateMode, ParameterMode.PositionMode)]
    [DataRow(1035, 35, ParameterMode.PositionMode, ParameterMode.ImmediateMode)]
    [DataRow(1110, 10, ParameterMode.ImmediateMode, ParameterMode.ImmediateMode)]
    public void ParseOpCode_worksWell(int input, int operation, ParameterMode parameter1, ParameterMode parameter2)
    {
        // Arrange & Act
        var result = IntCodeComputerStatic.ParseOpCode(input);

        // Assert
        result.operation.Should().Be(operation);
        result.parameterModes[0].Should().Be(parameter1);
        result.parameterModes[1].Should().Be(parameter2);
    }
}