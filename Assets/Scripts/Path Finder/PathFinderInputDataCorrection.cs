using System.Collections.Generic;
using PathFinderStructs;

public static class PathFinderInputDataCorrection
{
    // попытка исправить простые ошибки в входных данных, вроде перепутанных координат или порядка прямоугольников
    public static IEnumerable<Edge> TryToFixCoordinates(IEnumerable<Edge> edges)
    {
        List<Edge> fixedEdges = new List<Edge>();

        foreach (Edge edge in edges)
        {
            // координаты
            fixedEdges.Add(TryToFixCoordinates(edge));
        }

        for (int i = 1; i < fixedEdges.Count; i++)
        {
            // порядок прямоугольников
            if (!fixedEdges[i - 1].Second.Equals(fixedEdges[i].First)
                && fixedEdges[i - 1].Second.Equals(fixedEdges[i].Second))
            {
                fixedEdges[i] = new Edge(fixedEdges[i].Second, fixedEdges[i].First, fixedEdges[i].Start, fixedEdges[i].End);
            }
        }

        return fixedEdges; 
    }

    private static Edge TryToFixCoordinates(Edge edge)
    {
        // каждого прямоугольника
        edge.First = TryToFixCoordinates(edge.First);
        edge.Second = TryToFixCoordinates(edge.Second);

        // указанной грани пересечения
        if (edge.Start.x == edge.End.x)
        {
            if (edge.Start.y > edge.End.y)
            {
                float tmp = edge.Start.y;
                edge.Start.y = edge.End.y;
                edge.End.y = tmp;
            }
        }
        else
        {
            if (edge.Start.x > edge.End.x)
            {
                float tmp = edge.Start.x;
                edge.Start.x = edge.End.x;
                edge.End.x = tmp;
            }
        }

        return edge;
    }

    private static Rectangle TryToFixCoordinates(Rectangle rectangle)
    {
        if (rectangle.Min.x > rectangle.Max.x)
        {
            float tmp = rectangle.Min.x;
            rectangle.Min.x = rectangle.Max.x;
            rectangle.Max.x = tmp;
        }
        if (rectangle.Min.y > rectangle.Max.y)
        {
            float tmp = rectangle.Min.y;
            rectangle.Min.y = rectangle.Max.y;
            rectangle.Max.y = tmp;
        }
        return rectangle;
    }
}
