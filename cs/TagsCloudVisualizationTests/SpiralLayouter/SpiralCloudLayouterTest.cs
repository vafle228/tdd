using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.SpiralLayouter;
using TagsCloudVisualization.SpiralLayouter.PointGenerator;

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
        var pointGenerator = new SquareArchimedesSpiral(1);
        var layouter = new SpiralCloudLayouter(center, pointGenerator);
        
        layouter.Center.Should().BeEquivalentTo(center);
    }
    
    [Test]
    public void SpiralCloudLayouter_PutNextRectangle_ShouldPutFirstRectAtCenter()
    {
        var squareSize = new Size(100, 100);
        var pointGenerator = new SquareArchimedesSpiral(1);
        var layouter = new SpiralCloudLayouter(new Point(0, 0), pointGenerator);
        
        var rect = layouter.PutNextRectangle(squareSize);
        
        rect.Location.Should().BeEquivalentTo(new Point(-50, -50));
    }

    [Test]
    public void SpiralCloudLayouter_PutNextRectangle_AllRectsShouldNotIntersect()
    {
        var random = new Random();
        var pointGenerator = new SquareArchimedesSpiral(1);
        var layouter = new SpiralCloudLayouter(new Point(0, 0), pointGenerator);
        var rectangleSizes = new Size[10];
        for (var i = 0; i < rectangleSizes.Length; i++)
        {
            var width = random.Next(1, 100);
            var height = random.Next(1, 100);
            rectangleSizes[i] = new Size(width, height);
        }
        
        var rectangles = rectangleSizes
            .Select(rs => layouter.PutNextRectangle(rs))
            .ToArray();
        
        for (var i = 0; i < rectangles.Length; i++)
        {
            for (var j = i + 1; j < rectangles.Length; j++)
            {
                var isIntersect = rectangles[i].IntersectsWith(rectangles[j]);
                isIntersect.Should().BeFalse($"Rect {rectangles[i]} should not intersect {rectangles[j]}");
            }
        }
    }
}