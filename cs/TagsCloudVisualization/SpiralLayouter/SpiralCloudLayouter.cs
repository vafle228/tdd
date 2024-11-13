using System.Drawing;
using TagsCloudVisualization.SpiralLayouter.PointGenerators;

namespace TagsCloudVisualization.SpiralLayouter;

public class SpiralCloudLayouter : ICloudLayouter
{
    private const int STEP = 5;
    private List<Rectangle> placedRectangles;
    private SquareArchimedesSpiral squareArchimedesSpiral;
    
    public Point Center { get; private set; }

    public SpiralCloudLayouter(Point center)
    {
        Center = center;
        placedRectangles = [];
        squareArchimedesSpiral = new SquareArchimedesSpiral(center, STEP);
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        using var spiralEnumerator = squareArchimedesSpiral.GetEnumerator();
        do
        {
            var rectangleCenter = spiralEnumerator.Current;
            var rectangleUpperLeft = rectangleCenter - rectangleSize / 2;
            var rect = new Rectangle(rectangleUpperLeft, rectangleSize);

            if (!placedRectangles.Any(r => r.IntersectsWith(rect)))
            {
                placedRectangles.Add(rect);
                squareArchimedesSpiral.Reset();
                return rect;
            }
        } while(spiralEnumerator.MoveNext());
        
        return Rectangle.Empty;  // Never happens because squareFibonacciSpiral infinite
    }
}