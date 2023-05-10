using UnityEngine;
using PathFinderStructs;

public static class CustomShapeUtils
{
    public static bool RectOverlapsCheck(RectTransform rectTransform1, RectTransform rectTransform2)
    {
        Rect rect1 = CreateRectFromRectTransform(rectTransform1);
        Rect rect2 = CreateRectFromRectTransform(rectTransform2);
        return rect1.Overlaps(rect2);
    }

    public static Rect CreateRectFromRectTransform(RectTransform rectTransform)
    {
        Vector2 pivot = rectTransform.pivot;
        Vector2 position = rectTransform.position;
        Vector2 lossyScale = rectTransform.lossyScale;
        return new Rect
            (position.x - (rectTransform.rect.width * lossyScale.x * pivot.x), 
            position.y - (rectTransform.rect.height * lossyScale.y * pivot.y),
            rectTransform.rect.width * lossyScale.x, 
            rectTransform.rect.height * lossyScale.y);
    }

    public static Rect CreateRectFromRectangle(Rectangle rectangle)
    {
        float width = rectangle.Max.x - rectangle.Min.x;
        float height = rectangle.Max.y - rectangle.Min.y;
        return new Rect(rectangle.Min.x, rectangle.Min.y, width, height);
    }

    // группа методов для получения координат точки на линии
    public static float GetLineX(Vector2 lineStart, Vector2 lineEnd, float y)
    {
        float ratioXY = (lineEnd.x - lineStart.x) / (lineEnd.y - lineStart.y); // X to Y ratio
        return ((y - lineStart.y) * ratioXY) + lineStart.x;
    }

    public static float GetLineX(Line line, float y)
    {
        float ratioXY = (line.End.x - line.Start.x) / (line.End.y - line.Start.y); // X to Y ratio
        return ((y - line.Start.y) * ratioXY) + line.Start.x;
    }

    public static float GetLineY(Vector2 lineStart, Vector2 lineEnd, float x)
    {
        float ratioXY = (lineEnd.x - lineStart.x) / (lineEnd.y - lineStart.y); // X to Y ratio
        return ((x - lineStart.x) / ratioXY) + lineStart.y;
    }

    public static float GetLineY(Line line, float x)
    {
        float ratioXY = (line.End.x - line.Start.x) / (line.End.y - line.Start.y); // X to Y ratio
        return ((x - line.Start.x) / ratioXY) + line.Start.y;
    }
}
