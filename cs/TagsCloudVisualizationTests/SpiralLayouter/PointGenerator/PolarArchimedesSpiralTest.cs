using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.SpiralLayouter.PointGenerator;

namespace TagsCloudVisualizationTests.SpiralLayouter.PointGenerator;

[TestFixture]
public class PolarArchimedesSpiralTest
{
    private const string NOT_POSITIVE_ARGUMENT_ERROR = "Spiral params should be positive.";
    
    public static IEnumerable<TestCaseData> InitAtGivenPointAngleAndRadiusTestCases
    {
        get
        {
            yield return new TestCaseData(new Point(0, 0), 2, 3);
            yield return new TestCaseData(new Point(-10, 10), 10.1, 15.6);
            yield return new TestCaseData(new Point(2000, 10000), 200.1111111, 23);
        }
    }

    [TestCaseSource(nameof(InitAtGivenPointAngleAndRadiusTestCases))]
    public void PolarArchimedesSpiral_InitAtGivenPointAngleAndRadius(Point center, double radius, double angleOffset)
    {
        var polarSpiral = new PolarArchimedesSpiral(center, radius, angleOffset);

        polarSpiral.Radius.Should().Be(radius);
        polarSpiral.Center.Should().BeEquivalentTo(center);
        polarSpiral.AngleOffset.Should().Be(angleOffset * Math.PI / 180);
    }
    
    public static IEnumerable<TestCaseData> ThrowErrorOnNotPositiveNumberTestCases
    {
        get
        {
            yield return new TestCaseData(new Point(0, 0), 0.0000, 11);
            yield return new TestCaseData(new Point(0, 0), -10, -100);
            yield return new TestCaseData(new Point(0, 0), 52, double.MinValue);
        }
    }

    [TestCaseSource(nameof(ThrowErrorOnNotPositiveNumberTestCases))]
    public void PolarArchimedesSpiral_ThrowError_OnNotPositiveNumber(Point center, double radius, double angleOffset)
    {
        var negativeParameter = radius <= 0 ? nameof(radius) : nameof(angleOffset);
        var polarSpiralCtor = () => new PolarArchimedesSpiral(center, radius, angleOffset);

        polarSpiralCtor.Should()
            .Throw<ArgumentException>()
            .WithMessage($"{NOT_POSITIVE_ARGUMENT_ERROR} (Parameter '{negativeParameter}')");
    }
    
        
    [Test]
    public void PolarArchimedesSpiral_CalculateSpecialPoints()
    {
        var polarSpiral = new PolarArchimedesSpiral(new Point(0, 0), 2, 360);
        var expected = new[]
        {
            new Point(2, 0), new Point(4, 0), new Point(6, 0), new Point(8, 0)
        };
        
        var expectedAndReceived = expected.Zip(polarSpiral);
        
        polarSpiral.Current.Should().BeEquivalentTo(new Point(0, 0));
        foreach (var (expectedPoint, receivedPoint) in expectedAndReceived)
        {
            expectedPoint.Should().BeEquivalentTo(receivedPoint);
        }
    }
}