using System.Collections;
using System.Drawing;

namespace TagsCloudVisualization.SpiralLayouter.PointGenerator;

public class SquareArchimedesSpiral : IPointGenerator
{
    private int neededPoints = 1;
    private int pointsToPlace = 1;
    private Point currentPoint = Point.Empty;
    private Direction direction = Direction.Up;

    public int Step { get; }

    public SquareArchimedesSpiral(int step)
    {
        if (step <= 0)
            throw new ArgumentException("Step should be positive number");
        
        Step = step;
    }
    
    public IEnumerable<Point> StartFrom(Point startPoint)
    {
        SetStartState(startPoint);
        while (true)
        {
            pointsToPlace--;
            currentPoint += GetOffsetSize();
            
            if (pointsToPlace == 0)
            {
                direction = direction.AntiClockwiseRotate();
                if (direction is Direction.Up or Direction.Down) neededPoints++;
                pointsToPlace = neededPoints;
            }
            yield return currentPoint;
        }
    }

    private void SetStartState(Point startPoint)
    {
        neededPoints = 1;
        direction = Direction.Up;
        currentPoint = startPoint;
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
}