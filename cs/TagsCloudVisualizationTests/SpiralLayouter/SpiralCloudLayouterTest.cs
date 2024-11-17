using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualization.SpiralLayouter;
using TagsCloudVisualization.SpiralLayouter.PointGenerator;
using TagsCloudVisualizationTests.Extensions;

namespace TagsCloudVisualizationTests.SpiralLayouter;

[TestFixture]
public class SpiralCloudLayouterTest
{
    private static IEnumerable<TestCaseData> InitCenterAtGivenPointTestCases
    {
        get
        {
            yield return new TestCaseData(new Point(0, 0));
            yield return new TestCaseData(new Point(-10, 10));
            yield return new TestCaseData(new Point(2000, 10000));
        }
    }

    private static IEnumerable<TestCaseData> DifferentPointGeneratorTestCase
    {
        get
        {
            yield return new TestCaseData(new SquareArchimedesSpiral(5));
            yield return new TestCaseData(new PolarArchimedesSpiral(10, 10));
        }
    }
    
    private static IEnumerable<TestCaseData> LayingDensityTestCase
    {
        get
        {
            yield return new TestCaseData(new SquareArchimedesSpiral(5), 0.75);
            yield return new TestCaseData(new PolarArchimedesSpiral(10, 5), 0.65);
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

    [TestCaseSource(nameof(DifferentPointGeneratorTestCase))]
    [Repeat(10)]
    public void SpiralCloudLayouter_PutNextRectangle_AllRectsShouldNotIntersect(IPointGenerator pointGenerator)
    {
        var layouter = new SpiralCloudLayouter(new Point(0, 0), pointGenerator);
        var rectangles = PlaceRectangles(10, layouter);

        var intersectedRect = () => rectangles
            .First(r1 => rectangles
                .Except(Enumerable.Repeat(r1, 1))
                .Any(r1.IntersectsWith)
            );
        
        intersectedRect.Should().Throw<InvalidOperationException>("No intersection found");
    }

    [TestCaseSource(nameof(LayingDensityTestCase))]
    [Repeat(10)]
    public void SpiralCloudLayouter_PutNextRectangle_SatisfyDensityMinimum(IPointGenerator pointGenerator, double min)
    {
        var layouter = new SpiralCloudLayouter(new Point(0, 0), pointGenerator);
        var rectangles = PlaceRectangles(50, layouter);

        var claimedArea = FindClaimedRectangle(rectangles).Area();
        var totalArea = rectangles.Select(r => r.Area()).Sum();

        (totalArea / claimedArea).Should().BeGreaterThan(min);
    }

    private List<Rectangle> PlaceRectangles(int count, ICloudLayouter layouter)
    {
        var rectangleSizes = Enumerable.Range(0, count)
            .Select(_ => GetRandomSize(1, 100))
            .Select(layouter.PutNextRectangle);
        return rectangleSizes.ToList();
    }

    private Size GetRandomSize(int min, int max)
    {
        var random = new Random();
        return new Size(random.Next(min, max), random.Next(min, max));
    }

    private Rectangle FindClaimedRectangle(List<Rectangle> rectangles)
    {
        var leftX = rectangles.Min(r => r.Location.X);
        var leftY = rectangles.Min(r => r.Location.Y);
        
        var width = rectangles.Max(r => r.Location.X) - leftX;
        var height = rectangles.Max(r => r.Location.Y) - leftY;
        
        return new Rectangle(leftX, leftY, width, height);
    }
}