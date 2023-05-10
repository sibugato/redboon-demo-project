using System.Collections.Generic;
using UnityEngine;
using PathFinderStructs;

interface IPathFinder
{
    IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges);
}


