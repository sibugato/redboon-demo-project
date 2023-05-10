using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PathFinderStructs;

public static class PathFinderInputDataValidation
{
    // ��������� ������� ������
    public static bool ValidateInputData(Vector2 A, Vector2 C, IEnumerable<Edge> edges)
    {
        Rectangle firstRectangle;
        Rectangle secondRectangle = new Rectangle();
        int count = 0;

        if (edges.Count() == 0)
        {
            PathFinderVisualizer.PathVisualizer.ShowError("������� ������ �������� ������ ���������");
            return false;
        }

        foreach (Edge edge in edges)
        {
            if (count == 0 && !IsPointInRectangleCheck(A, edge.First))
            {
                PathFinderVisualizer.PathVisualizer.ShowError("����� ������ ���� ����� �� ��������� ������� ��������������");
                return false;
            }

            if (count == edges.Count() - 1 && !IsPointInRectangleCheck(C, edge.Second))
            {
                PathFinderVisualizer.PathVisualizer.ShowError("����� ��������� ���� ����� �� ��������� ���������� ��������������");
                return false;
            }

            if (IsRectangleIsLineCheck(edge.First))
            {
                PathFinderVisualizer.PathVisualizer.ShowError("������������� #1 �������� ������ (������ �����: " + count + ")");
                return false;
            }
            else if (IsRectangleIsLineCheck(edge.Second))
            {
                PathFinderVisualizer.PathVisualizer.ShowError("������������� #2 �������� ������ (������ �����: " + count + ")");
                return false;
            }

            if (!IsRectanglesAdjacentCheck(edge.First, edge.Second))
            {
                PathFinderVisualizer.PathVisualizer.ShowError("�������������� �� ��������� ��� ������������ (������ �����: " + count + ")");
                return false;
            }

            if ((edge.Start.x != edge.End.x && edge.Start.y != edge.End.y)
                || (edge.Start.x == edge.End.x && edge.Start.y == edge.End.y))
            {
                PathFinderVisualizer.PathVisualizer.ShowError("����������� ������� ����� ����������� (������ �����: " + count + ")");
                return false;
            }

            if (!IsEdgeLineCorrespondToActualEdge(edge))
            {
                PathFinderVisualizer.PathVisualizer.ShowError("��������� ����� ����������� �� ������������� ��������� ��������������� (������ �����: " + count + ")");
                return false;
            }

            if (count != 0)
            {
                firstRectangle = edge.First;
                if (!firstRectangle.Equals(secondRectangle))
                {
                    PathFinderVisualizer.PathVisualizer.ShowError("������ ������������� �� ������������� ������� �� ������� ����� (������ �����: " + count + ")");
                    return false;
                }
            }
            secondRectangle = edge.Second;
            count++;
        }

        return true;
    }

    // ��������� ��������� � ��������� ����� � �������� ����� �����������
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

    // ��������� ��� �������������� ���������, � �� ������������
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

    // ��������� ��� ����� ������ ��������������, ��� �������� ������ � ����� ����
    private static bool IsPointInRectangleCheck(Vector2 point, Rectangle rectangle)
    {
        if ((point.x >= rectangle.Min.x && point.x <= rectangle.Max.x)
            && (point.y >= rectangle.Min.y && point.y <= rectangle.Max.y))
        {
            return true;
        }
        return false;
    }

    // �������� ����������� ������
    private static bool IsEdgesAdjacentCheck(float min1, float max1, float min2, float max2)
    {
        if ((min1 < min2 && max1 <= min2) || (min1 >= max2 && max1 > max2))
        {
            return false;
        }
        return true;
    }

    // �������� ����� ��������������
    private static bool IsRectangleIsLineCheck(Rectangle rectangle)
    {
        if (rectangle.Min.x == rectangle.Max.x || rectangle.Min.y == rectangle.Max.y)
        {
            return true;
        }
        return false;
    }
}
