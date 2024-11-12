using System.Drawing;

namespace TagsCloudVisualization;

public class CircularCloudLayouter
{
    public Point Center { get; private set; }
    
    public CircularCloudLayouter(Point center)
    {
        Center = center;
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        return Rectangle.Empty;
    }
}