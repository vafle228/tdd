using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.SpiralLayouter.PointGenerator;

namespace TagsCloudVisualizationTests.SpiralLayouter.PointGenerator;

[TestFixture]
public class PolarArchimedesSpiralTest
{
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
    public void PolarArchimedesSpiral_InitAtGivenPointAngleAndRadius(Point center, double radius, double angle)
    {
        var squareSpiral = new PolarArchimedesSpiral(center, radius, angle);

        squareSpiral.Angle.Should().Be(angle);
        squareSpiral.Radius.Should().Be(radius);
        squareSpiral.Center.Should().BeEquivalentTo(center);
    }
}