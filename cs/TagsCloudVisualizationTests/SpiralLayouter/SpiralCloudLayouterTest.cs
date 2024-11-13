using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.SpiralLayouter;

namespace TagsCloudVisualizationTests.SpiralLayouter;

[TestFixture]
public class SpiralCloudLayouterTest
{
    public static IEnumerable<TestCaseData> InitCenterAtGivenPointTestCases
    {
        get
        {
            yield return new TestCaseData(new Point(0, 0));
            yield return new TestCaseData(new Point(-10, 10));
            yield return new TestCaseData(new Point(2000, 10000));
        }
    }
    
    [TestCaseSource(nameof(InitCenterAtGivenPointTestCases))]
    public void SpiralCloudLayouter_InitCenterAtGivenPoint(Point center)
    {
        var layouter = new SpiralCloudLayouter(center);
        
        layouter.Center.Should().BeEquivalentTo(center);
    }
    
    [Test]
    public void SpiralCloudLayouter_PutNextRectangle_ShouldPutFirstRectAtCenter()
    {
        var squareSize = new Size(100, 100);
        var layouter = new SpiralCloudLayouter(new Point(0, 0));
        
        var rect = layouter.PutNextRectangle(squareSize);
        
        rect.Location.Should().BeEquivalentTo(new Point(-50, -50));
    }
}