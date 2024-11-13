using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.SpiralLayouter;

namespace TagsCloudVisualizationTests.SpiralLayouter;

[TestFixture]
public class SquareFibonacciSpiralTest
{
    private const string NOT_POSITIVE_STEP_ERROR = "Step should be positive number";
    
    public static IEnumerable<TestCaseData> InitCenterAtGivenPointAndStepTestCases
    {
        get
        {
            yield return new TestCaseData(new Point(0, 0), 2);
            yield return new TestCaseData(new Point(-10, 10), 10);
            yield return new TestCaseData(new Point(2000, 10000), 200);
        }
    }

    [TestCaseSource(nameof(InitCenterAtGivenPointAndStepTestCases))]
    public void SquareFibonacciSpiral_InitCenterAtGivenPointAndStep(Point center, int step)
    {
        var squareSpiral = new SquareFibonacciSpiral(center, step);

        squareSpiral.Step.Should().Be(step);
        squareSpiral.Center.Should().BeEquivalentTo(center);
    }

    public static IEnumerable<TestCaseData> InitCenterAtGivenPointTestCases
    {
        get
        {
            yield return new TestCaseData(new Point(0, 0), 0);
            yield return new TestCaseData(new Point(0, 0), -10);
            yield return new TestCaseData(new Point(0, 0), int.MinValue);
        }
    }

    [TestCaseSource(nameof(InitCenterAtGivenPointTestCases))]
    public void SquareFibonacciSpiral_ThrowError_OnNotPositiveNumber(Point center, int step)
    {
        var squareSpiralCtor = () => new SquareFibonacciSpiral(center, step);

        squareSpiralCtor.Should()
            .Throw<ArgumentException>()
            .WithMessage(NOT_POSITIVE_STEP_ERROR);
    }
    
    [Test]
    public void SquareFibonacciSpiral_CalculateFiveFirstPoints()
    {
        var squareSpiral = new SquareFibonacciSpiral(new Point(0, 0), 5);
        var expected = new[]
        {
            new Point(0, 5), new Point(-5, 5), new Point(-5, 0), new Point(-5, -5), new Point(0, -5)
        };
        
        var expectedAndReceived = expected.Zip(squareSpiral);
        
        squareSpiral.Current.Should().BeEquivalentTo(new Point(0, 0));
        foreach (var (expectedPoint, receivedPoint) in expectedAndReceived)
        {
            expectedPoint.Should().BeEquivalentTo(receivedPoint);
        }
    }
}