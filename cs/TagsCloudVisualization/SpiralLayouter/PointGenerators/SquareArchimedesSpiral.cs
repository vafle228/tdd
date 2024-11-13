using System.Collections;
using System.Drawing;

namespace TagsCloudVisualization.SpiralLayouter.PointGenerators;

public class SquareArchimedesSpiral : IEnumerable<Point>, IEnumerator<Point>
{
    private int neededPoints = 1;
    private int pointsToPlace = 1;
    private Direction direction = Direction.Up;
    
    public int Step { get; }
    public Point Center { get; }
    
    object? IEnumerator.Current => Current;
    public Point Current { get; private set; }

    public SquareArchimedesSpiral(Point center, int step)
    {
        if (step <= 0)
            throw new ArgumentException("Step should be positive number");
        
        Step = step;
        Center = center;
        Current = Center;
    }

    public IEnumerator<Point> GetEnumerator() => this;
    IEnumerator IEnumerable.GetEnumerator() => this;

    public bool MoveNext()
    {
        pointsToPlace--;
        Current += GetOffsetSize();
        
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
        Current = Center;
        pointsToPlace = neededPoints;
    }

    private Size GetOffsetSize() => direction switch
    {
        Direction.Up => new Size(0, Step),
        Direction.Right => new Size(Step, 0),
        Direction.Down => new Size(0, -Step),
        Direction.Left => new Size(-Step, 0),
        _ => throw new ArgumentOutOfRangeException()
    };
    
    # region Dispose code
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing) {}
    # endregion
}