using System.Drawing;
using TagsCloudVisualization.SpiralLayouter.PointGenerator;

namespace TagsCloudVisualization.SpiralLayouter;

public class SpiralCloudLayouter : ICloudLayouter
{
    private List<Rectangle> placedRectangles;
    private IPointGenerator<Point> pointGenerator;
    
    public Point Center { get; private set; }

    public SpiralCloudLayouter(Point center)
    {
        Center = center;
        placedRectangles = [];
        pointGenerator = new PolarArchimedesSpiral(center, 3, 0.5);
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        using var spiralEnumerator = pointGenerator.GetEnumerator();
        do
        {
            var rectangleCenter = spiralEnumerator.Current;
            var rectangleUpperLeft = rectangleCenter - rectangleSize / 2;
            var rect = new Rectangle(rectangleUpperLeft, rectangleSize);

            if (!placedRectangles.Any(r => r.IntersectsWith(rect)))
            {
                placedRectangles.Add(rect);
                pointGenerator.Reset();
                return rect;
            }
        } while(spiralEnumerator.MoveNext());
        
        return Rectangle.Empty;  // Never happens because squareFibonacciSpiral infinite
    }
}