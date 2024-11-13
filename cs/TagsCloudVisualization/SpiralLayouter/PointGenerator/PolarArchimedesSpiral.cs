using System.Collections;
using System.Drawing;

namespace TagsCloudVisualization.SpiralLayouter.PointGenerator;

public class PolarArchimedesSpiral : IPointGenerator<Point>
{
    public Point Center { get; }
    public double Angle { get; }
    public double Radius { get; }

    public Point Current { get; }
    object? IEnumerator.Current => Current;

    public PolarArchimedesSpiral(Point center, double radius, double angle)
    {
        if (radius <= 0 || angle <= 0)
        {
            var argName = radius <= 0 ? nameof(radius) : nameof(angle);
            throw new ArgumentException("Spiral params should be positive.", argName);
        }
            
        
        Angle = angle;
        Radius = radius;
        
        Center = center;
        Current = center;
    }
    
    public IEnumerator<Point> GetEnumerator() => this;
    IEnumerator IEnumerable.GetEnumerator() => this;
    
    public double Step => Radius / (2 * Math.PI);

    public bool MoveNext()
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
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