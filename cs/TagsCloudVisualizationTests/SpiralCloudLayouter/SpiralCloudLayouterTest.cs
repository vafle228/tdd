using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualizationTests.SpiralCloudLayouter;

[TestFixture]
public class SpiralCloudLayouterTest
{
    public static IEnumerable<TestCaseData> InitCenterAtGivenPointTestCases
    {
        get
        {
            yield return new TestCaseData(new Point(0, 0));
            yield return new TestCaseData(new Point(-10, 10));
            yield return new TestCaseData(new Point(2000, 10000));
        }
    }
    
    [TestCaseSource(nameof(InitCenterAtGivenPointTestCases))]
    public void SpiralCloudLayouter_InitCenterAtGivenPoint(Point center)
    {
        var layouter = new TagsCloudVisualization.SpiralCloudLayouter.SpiralCloudLayouter(center);
        
        layouter.Center.Should().BeEquivalentTo(center);
    }
}