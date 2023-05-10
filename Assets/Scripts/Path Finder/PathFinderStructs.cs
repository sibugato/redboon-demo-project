using UnityEngine;

namespace PathFinderStructs
{
    public enum Orientation
    {
        vertical,
        horizontal,
    }

    public struct Line
    {
        public Vector2 Start { get; }
        public Vector2 End { get; }
        public Orientation LineOrientation { get; }

        public Line(Vector2 start, Vector2 end, Orientation lineOrientation)
        {
            Start = start;
            End = end;
            LineOrientation = lineOrientation;
        }
    }

    public struct Rectangle
    {
        public Vector2 Min;
        public Vector2 Max;

        public Rectangle(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }
    }

    public struct Edge
    {
        public Rectangle First;
        public Rectangle Second;
        public Vector2 Start;
        public Vector2 End;

        public Edge(Rectangle first, Rectangle second, Vector2 start, Vector2 end)
        {
            First = first;
            Second = second;
            Start = start;
            End = end;
        }
    }
}
