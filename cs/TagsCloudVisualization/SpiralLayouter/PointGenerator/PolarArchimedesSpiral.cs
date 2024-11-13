using System.Collections;
using System.Drawing;

namespace TagsCloudVisualization.SpiralLayouter.PointGenerator;

public class PolarArchimedesSpiral : IPointGenerator<Point>
{
    private double currentAngle;
    
    public Point Center { get; }
    public double Radius { get; }
    public double AngleOffset { get; }

    object? IEnumerator.Current => Current;
    public Point Current { get; private set; }

    public PolarArchimedesSpiral(Point center, double radius, double angleOffset)
    {
        if (radius <= 0 || angleOffset <= 0)
        {
            var argName = radius <= 0 ? nameof(radius) : nameof(angleOffset);
            throw new ArgumentException("Spiral params should be positive.", argName);
        }
        
        Center = center;
        Current = center;
        
        Radius = radius;
        AngleOffset = angleOffset * Math.PI / 180;
    }
    
    public IEnumerator<Point> GetEnumerator() => this;
    IEnumerator IEnumerable.GetEnumerator() => this;

    public bool MoveNext()
    {
        currentAngle += AngleOffset;
        var polarCoordinate = Radius / (2 * Math.PI) * currentAngle;
        
        var xOffset = (int)Math.Round(polarCoordinate * Math.Cos(currentAngle));
        var yOffset = (int)Math.Round(polarCoordinate * Math.Sin(currentAngle));
        
        Current = Center + new Size(xOffset, yOffset);
        
        return true;
    }

    public void Reset()
    {
        Current = Center;
        currentAngle = 0;
    }
    

    # region Dispose code
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing) {}
    # endregion
}