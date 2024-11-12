using System.Collections;
using System.Drawing;

namespace TagsCloudVisualization.SpiralCloudLayouter;

public class SquareFibonacciSpiral : IEnumerable<Point>, IEnumerator<Point>
{
    private Point currentPoint;
    private int neededPoints = 1;
    private int pointsToPlace = 1;
    private Direction direction = Direction.Up;

    public int Step { get; private set; }
    public Point Center { get; private set; }

    public SquareFibonacciSpiral(Point center, int step)
    {
        if (step <= 0)
            throw new ArgumentException("Step should be positive number");
        
        Step = step;
        Center = center;
        currentPoint = Center;
    }

    public IEnumerator<Point> GetEnumerator() => this;

    IEnumerator IEnumerable.GetEnumerator() => this;

    public bool MoveNext()
    {
        pointsToPlace--;
        currentPoint += direction switch
        {
            Direction.Up => new Size(0, Step),
            Direction.Right => new Size(Step, 0),
            Direction.Down => new Size(0, -Step),
            Direction.Left => new Size(-Step, 0),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        if (pointsToPlace == 0)
        {
            direction = direction.AntiClockwiseRotate();
            if (direction is Direction.Up or Direction.Down) neededPoints++;
            pointsToPlace = neededPoints;
        }
        return true;
    }

    public void Reset()
    {
        neededPoints = 1;
        currentPoint = Center;
        pointsToPlace = neededPoints;
    }

    public Point Current => currentPoint;

    object? IEnumerator.Current => Current;
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {}
}