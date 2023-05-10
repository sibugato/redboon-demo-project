using System.Collections.Generic;
using UnityEngine;
using PathFinderStructs;

public class PathFinderService : MonoBehaviour, IPathFinder
{
    private float _rectangleSpaceFromMiddleStartingMultiplier = 0.1f;
    private float _rectangleSpaceFromMiddleMultiplierStep = 0.1f;

    private List<Rectangle> _rectanglesList = new List<Rectangle>();
    private List<Vector2> _pathPointsList = new List<Vector2>();
    private List<Line> _intersectionLines = new List<Line>();

    public static PathFinderService PathService;

    private void Awake()
    {      
        // Singleton
        if (PathService == null)
        {
            PathService = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges)
    {
        int passedEdges = 0;

        // �������������� ����������� �������������� ������ ������� ������
        edges = PathFinderInputDataCorrection.TryToFixCoordinates(edges);

        // ������� ������ �������� ������ � ���������� � ������
        PreparingShapeLists(edges);

        // ��������� ������� ������ � ����� ���� ��� ������
        if (PathFinderInputDataValidation.ValidateInputData(A, C, edges))
        {
            _pathPointsList.Add(A);

            while (!_pathPointsList.Contains(C))
            {
                // �������� ���� �� ������� ����� ����� � ������
                if (CheckGroupEdgesCrossing(C, passedEdges, _intersectionLines.Count - 1))
                {
                    _pathPointsList.Add(C);
                    break;
                }

                /*
                ���� �� ����� ��������� ������� - �������� ������ ����� ����� � �������������� ����� �������,
                � ��������� ������ �� �� � ����� �� ��� ��������� ������� ������������ ��� ���������� �����.

                ���� ��� - ��������� �� �� ����� � �������������� ��� ���������������.
                ���� �� ������ ��������� ������� ������ - ��������� ����� ����� ��������� ��������� �����.
                */
                int edgeCountController = 0;
                int initialPassedEdges = passedEdges;

                for (int i = _rectanglesList.Count - 1; i > passedEdges; i--)
                {
                    foreach (Vector2 rectanglePoint in GetPointsInRectangle(_rectanglesList[i]))
                    {
                        if (CheckGroupEdgesCrossing(rectanglePoint, passedEdges, _intersectionLines.Count - edgeCountController - 1))
                        {
                            _pathPointsList.Add(rectanglePoint);
                            passedEdges += _intersectionLines.Count - edgeCountController - passedEdges;
                            break;
                        }
                    }
                    if (initialPassedEdges != passedEdges)
                    {
                        break;
                    }
                    edgeCountController++;
                }

                // ���� ������ �� ������ ��������� ������� ��� ��������� - ���������� ��������� ������ ���� �������� ��������� �����
                if (initialPassedEdges == passedEdges)
                {
                    Vector2 middleOfClosestEdge = _intersectionLines[passedEdges].Start + (_intersectionLines[passedEdges].End - _intersectionLines[passedEdges].Start) / 2;
                    _pathPointsList.Add(middleOfClosestEdge);
                }

                // ���� ��� ����� ���������� - ������ � ������� ����� �������������� ����� ������� ������ ��� ���������
                if (passedEdges == _intersectionLines.Count)
                {
                    _pathPointsList.Add(C);
                }
                
            }
            PathFinderVisualizer.PathVisualizer.ShowMessage("���� ������� ��������");
        }

        PathFinderVisualizer.PathVisualizer.ChangeDrawingSubjects(_rectanglesList, _pathPointsList, _intersectionLines);
        return _pathPointsList;
    }

    // ��������� ���������� �� ������������� ���� ��������� ������ ������
    private bool CheckGroupEdgesCrossing(Vector2 pathEnd, int startingEdgeLineIndex, int finalEdgeLineIndex)
    {
        for (int i = startingEdgeLineIndex; i <= finalEdgeLineIndex; i++)
        {
            if (!CheckEdgeCrossing(_pathPointsList[_pathPointsList.Count - 1], pathEnd, _intersectionLines[i]))
            {
                break;
            }
            if (i == finalEdgeLineIndex)
            {
                return true;
            }
        }
        return false;
    }

    // ��������� ���������� �� ������������� ���� ��������� �����
    private bool CheckEdgeCrossing(Vector2 pathStart, Vector2 pathEnd, Line edgeLine)
    {
        if (edgeLine.LineOrientation == Orientation.vertical)
        {
            float pathY = CustomShapeUtils.GetLineY(pathStart, pathEnd, edgeLine.Start.x);
            if (pathY < edgeLine.Start.y || pathY > edgeLine.End.y)
            {
                return false;
            }
        }
        else if (edgeLine.LineOrientation == Orientation.horizontal)
        {
            float pathX = CustomShapeUtils.GetLineX(pathStart, pathEnd, edgeLine.Start.y);
            if (pathX < edgeLine.Start.x || pathX > edgeLine.End.x)
            {
                return false;
            }
        }
        return true;
    }

    // ������ ������ ����� ������ �������������� ��� �������� ����������� ����. �� �������� � ������ �� �������� ���������� ��������
    private List<Vector2> GetPointsInRectangle(Rectangle rectangle)
    {
        Vector2 middle = rectangle.Min + (rectangle.Max - rectangle.Min) / 2;
        Vector2 size = new Vector2(rectangle.Max.x - rectangle.Min.x, rectangle.Max.y - rectangle.Min.y);
        List<Vector2> rectanglePoints = new List<Vector2> { middle };
        for (float i = _rectangleSpaceFromMiddleStartingMultiplier; i < 0.5f; i += _rectangleSpaceFromMiddleMultiplierStep)
        {
            rectanglePoints.Add(new Vector2(middle.x - size.x * _rectangleSpaceFromMiddleStartingMultiplier, middle.y - size.y * _rectangleSpaceFromMiddleStartingMultiplier));
            rectanglePoints.Add(new Vector2(middle.x, middle.y - size.y * _rectangleSpaceFromMiddleStartingMultiplier));
            rectanglePoints.Add(new Vector2(middle.x + size.x * _rectangleSpaceFromMiddleStartingMultiplier, middle.y - size.y * _rectangleSpaceFromMiddleStartingMultiplier));
            rectanglePoints.Add(new Vector2(middle.x - size.x * _rectangleSpaceFromMiddleStartingMultiplier, middle.y));
            rectanglePoints.Add(new Vector2(middle.x + size.x * _rectangleSpaceFromMiddleStartingMultiplier, middle.y));
            rectanglePoints.Add(new Vector2(middle.x - size.x * _rectangleSpaceFromMiddleStartingMultiplier, middle.y + size.y * _rectangleSpaceFromMiddleStartingMultiplier));
            rectanglePoints.Add(new Vector2(middle.x, middle.y + size.y * _rectangleSpaceFromMiddleStartingMultiplier));
            rectanglePoints.Add(new Vector2(middle.x + size.x * _rectangleSpaceFromMiddleStartingMultiplier, middle.y + size.y * _rectangleSpaceFromMiddleStartingMultiplier));
        }
        return rectanglePoints;
    }

    // ��������������� ���������
    private Line GetIntersectionLineInEdge(Edge edge)
    {
        Orientation lineOrientation = edge.Start.x == edge.End.x ? Orientation.vertical : Orientation.horizontal;
        return new Line(edge.Start, edge.End, lineOrientation);
    }

    // ���������� � ������ ������, ������� ������ ������ � ��������� ���� ����������� �����
    private void PreparingShapeLists(IEnumerable<Edge> edges)
    {
        int count = 0;
        _pathPointsList.Clear();
        _rectanglesList.Clear();
        _intersectionLines.Clear();

        foreach (Edge edge in edges)
        {
            if (count == 0)
            {
                _rectanglesList.Add(edge.First);
            }
            _rectanglesList.Add(edge.Second);
            _intersectionLines.Add(GetIntersectionLineInEdge(edge));
            count++;
        }
    }
}
