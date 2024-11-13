using System.Drawing;

namespace TagsCloudVisualization;

public static class BitmapGenerator
{
    public static Bitmap GenerateWindowsBitmap(ICloudLayouter cloudLayouter, Size bitmapSize, in Size[] rectSizes)
    {
        var bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
        using var graphics = Graphics.FromImage(bitmap);
        
        var rects = rectSizes
            .Select(cloudLayouter.PutNextRectangle)
            .ToArray();
        graphics.DrawRectangles(Pens.MediumPurple, rects);

        return bitmap;
    }
}