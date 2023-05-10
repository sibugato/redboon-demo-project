using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PathFinderStructs;

// Класс для визуализации результата работы сервиса
public class PathFinderVisualizer : MonoBehaviour
{
    private List<Rectangle> _rectanglesList = new List<Rectangle>();
    private List<Vector2> _pathPointsList = new List<Vector2>();
    private List<Line> _intersectionLines = new List<Line>();

    [SerializeField] private TextMeshProUGUI _pathFinderState;

    public static PathFinderVisualizer PathVisualizer;

    private void Awake()
    {
        if (PathVisualizer == null)
        {
            PathVisualizer = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeDrawingSubjects(List<Rectangle> rectanglesList, List<Vector2> pathPointsList, List<Line> intersectionLines)
    {
        _rectanglesList = rectanglesList;
        _pathPointsList = pathPointsList;
        _intersectionLines = intersectionLines;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        foreach (Rectangle rectangle in _rectanglesList)
        {
            DrawRect(CustomShapeUtils.CreateRectFromRectangle(rectangle));
        }

        Gizmos.color = Color.yellow;
        foreach (Line intersectionLine in _intersectionLines)
        {
            DrawLine(intersectionLine.Start, intersectionLine.End);
        }

        Gizmos.color = Color.cyan;
        for (int i = 0; i < _pathPointsList.Count; i++)
        {
            if (i > 0)
            {
                DrawLine(_pathPointsList[i - 1], _pathPointsList[i]);
            }
        }
    }

    private void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector2(rect.center.x, rect.center.y), new Vector2(rect.size.x, rect.size.y));
    }

    private void DrawLine(Vector2 start, Vector2 end)
    {
        Gizmos.DrawLine(start, end);
    }

    public void ShowMessage(string message)
    {
        _pathFinderState.text = message;
    }

    public void ShowError(string error)
    {
        _pathFinderState.text = "<color=red>ОШИБКА</color>: " + error;
    }
}
