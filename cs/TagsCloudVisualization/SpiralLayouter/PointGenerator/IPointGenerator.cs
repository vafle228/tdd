namespace TagsCloudVisualization.SpiralLayouter.PointGenerator;

public interface IPointGenerator<T> : IEnumerable<T>, IEnumerator<T>
{
    /*
     * Marking interface for infinite enumerators
     * That can be used as point generator
     */
}