using System.Collections;
using System.Drawing;

namespace TagsCloudVisualization.SpiralCloudLayouter;

public class SquareFibonacciSpiral : IEnumerable<Point>, IEnumerator<Point>
{
    public int Step { get; private set; }
    public Point Center { get; private set; }

    public SquareFibonacciSpiral(Point center, int step)
    {
        if (step <= 0)
            throw new ArgumentException("Step should be positive number");
        
        Step = step;
        Center = center;
    }

    public IEnumerator<Point> GetEnumerator() => this;

    IEnumerator IEnumerable.GetEnumerator() => this;

    public bool MoveNext()
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public Point Current { get; }

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}