using System.Drawing;

namespace TagsCloudVisualization.SpiralLayouter;

public class SpiralCloudLayouter : ICloudLayouter
{
    private const int STEP = 1;
    private List<Rectangle> placedRectangles;
    private SquareFibonacciSpiral squareFibonacciSpiral;
    
    public Point Center { get; private set; }

    public SpiralCloudLayouter(Point center)
    {
        Center = center;
        placedRectangles = [];
        squareFibonacciSpiral = new SquareFibonacciSpiral(center, STEP);
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        using var spiralEnumerator = squareFibonacciSpiral.GetEnumerator();
        do
        {
            var rectangleCenter = spiralEnumerator.Current;
            var rectangleUpperLeft = rectangleCenter - rectangleSize / 2;
            var rect = new Rectangle(rectangleUpperLeft, rectangleSize);

            if (!placedRectangles.Any(r => r.IntersectsWith(rect)))
            {
                placedRectangles.Add(rect);
                squareFibonacciSpiral.Reset();
                return rect;
            }
        } while(spiralEnumerator.MoveNext());
        
        return Rectangle.Empty;  // Never happens because squareFibonacciSpiral infinite
    }
}