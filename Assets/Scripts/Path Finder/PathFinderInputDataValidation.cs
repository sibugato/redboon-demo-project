using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PathFinderStructs;

public static class PathFinderInputDataValidation
{
    // валидаци€ входных данных
    public static bool ValidateInputData(Vector2 A, Vector2 C, IEnumerable<Edge> edges)
    {
        Rectangle firstRectangle;
        Rectangle secondRectangle = new Rectangle();
        int count = 0;

        if (edges.Count() == 0)
        {
            PathFinderVisualizer.PathVisualizer.ShowError("входные данные содержат пустую коллекцию");
            return false;
        }

        foreach (Edge edge in edges)
        {
            if (count == 0 && !IsPointInRectangleCheck(A, edge.First))
            {
                PathFinderVisualizer.PathVisualizer.ShowError("точка начала пути лежит за пределами первого пр€моугольника");
                return false;
            }

            if (count == edges.Count() - 1 && !IsPointInRectangleCheck(C, edge.Second))
            {
                PathFinderVisualizer.PathVisualizer.ShowError("точка окончани€ пути лежит за пределами последнего пр€моугольника");
                return false;
            }

            if (IsRectangleIsLineCheck(edge.First))
            {
                PathFinderVisualizer.PathVisualizer.ShowError("пр€моугольник #1 €вл€етс€ линией (индекс грани: " + count + ")");
                return false;
            }
            else if (IsRectangleIsLineCheck(edge.Second))
            {
                PathFinderVisualizer.PathVisualizer.ShowError("пр€моугольник #2 €вл€етс€ линией (индекс грани: " + count + ")");
                return false;
            }

            if (!IsRectanglesAdjacentCheck(edge.First, edge.Second))
            {
                PathFinderVisualizer.PathVisualizer.ShowError("пр€моугольники не примыкают или пересекаютс€ (индекс грани: " + count + ")");
                return false;
            }

            if ((edge.Start.x != edge.End.x && edge.Start.y != edge.End.y)
                || (edge.Start.x == edge.End.x && edge.Start.y == edge.End.y))
            {
                PathFinderVisualizer.PathVisualizer.ShowError("некорректно указана грань пересечени€ (индекс грани: " + count + ")");
                return false;
            }

            if (!IsEdgeLineCorrespondToActualEdge(edge))
            {
                PathFinderVisualizer.PathVisualizer.ShowError("указанна€ грань пересечени€ не соответствует положению пр€моугольников (индекс грани: " + count + ")");
                return false;
            }

            if (count != 0)
            {
                firstRectangle = edge.First;
                if (!firstRectangle.Equals(secondRectangle))
                {
                    PathFinderVisualizer.PathVisualizer.ShowError("первый пр€моугольник не соответствует второму из прошлой грани (индекс грани: " + count + ")");
                    return false;
                }
            }
            secondRectangle = edge.Second;
            count++;
        }

        return true;
    }

    // сравнение указанной в структуре грани и реальной грани пересечени€
    private static bool IsEdgeLineCorrespondToActualEdge(Edge edge)
    {
        if (edge.Start.x == edge.End.x)
        {
            if ((edge.Start.x == edge.First.Min.x && edge.Start.x == edge.Second.Max.x)
                ||
                (edge.Start.x == edge.First.Max.x || edge.Start.x == edge.Second.Min.x))
            {
                if ((edge.Start.y >= edge.First.Min.y && edge.Start.y >= edge.Second.Min.y)
                    &&
                    (edge.End.y <= edge.First.Max.y && edge.End.y <= edge.Second.Max.y))
                {
                    return true;
                }
            }
        }
        else
        {
            if ((edge.Start.y == edge.First.Min.y && edge.Start.y == edge.Second.Max.y)
                 ||
                  (edge.Start.y == edge.First.Max.y || edge.Start.y == edge.Second.Min.y))
            {
                if ((edge.Start.x >= edge.First.Min.x && edge.Start.x >= edge.Second.Min.x)
                    &&
                    (edge.End.x <= edge.First.Max.x && edge.End.x <= edge.Second.Max.x))
                {
                    return true;
                }
            }
        }
        return false;
    }

    // ѕровер€ет что пр€моугольники примыкают, и не пересекаютс€
    private static bool IsRectanglesAdjacentCheck(Rectangle first, Rectangle second)
    {
        if (IsEdgesAdjacentCheck(first.Min.x, first.Max.x, second.Min.x, second.Max.x))
        {
            if (first.Min.y == second.Max.y || first.Max.y == second.Min.y)
            {
                return true;
            }
        }
        else
        {
            if (first.Min.x == second.Max.x || first.Max.x == second.Min.x)
            {
                return true;
            }
        }
        return false;
    }

    // ѕровер€ет что точка внутри пр€моугольника, дл€ проверки начала и конца пути
    private static bool IsPointInRectangleCheck(Vector2 point, Rectangle rectangle)
    {
        if ((point.x >= rectangle.Min.x && point.x <= rectangle.Max.x)
            && (point.y >= rectangle.Min.y && point.y <= rectangle.Max.y))
        {
            return true;
        }
        return false;
    }

    // ѕроверка пересечени€ граней
    private static bool IsEdgesAdjacentCheck(float min1, float max1, float min2, float max2)
    {
        if ((min1 < min2 && max1 <= min2) || (min1 >= max2 && max1 > max2))
        {
            return false;
        }
        return true;
    }

    // ѕроверка формы пр€моугольника
    private static bool IsRectangleIsLineCheck(Rectangle rectangle)
    {
        if (rectangle.Min.x == rectangle.Max.x || rectangle.Min.y == rectangle.Max.y)
        {
            return true;
        }
        return false;
    }
}
