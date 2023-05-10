using System.Collections.Generic;
using UnityEngine;
using PathFinderStructs;

public class PathFinderTestData : MonoBehaviour
{
    // Демонстрационные данные. Каждый Тест относится к соответствующей кнопке в сцене
    public void Test1()
    {
        List<Edge> edges = new List<Edge>
        {
            new Edge(
                new Rectangle(new Vector2(0, 0), new Vector2(20, 10)),
                new Rectangle(new Vector2(15, 10), new Vector2(35, 20)),
                new Vector2(15, 10),
                new Vector2(20, 10)),

            new Edge(
                new Rectangle(new Vector2(15, 10), new Vector2(35, 20)),
                new Rectangle(new Vector2(35, 5), new Vector2(45, 15)),
                new Vector2(35, 10),
                new Vector2(35, 15)),

            new Edge(
                new Rectangle(new Vector2(35, 5), new Vector2(45, 15)),
                new Rectangle(new Vector2(40, 15), new Vector2(55, 35)),
                new Vector2(40, 15),
                new Vector2(45, 15)),

            new Edge(
                new Rectangle(new Vector2(40, 15), new Vector2(55, 35)),
                new Rectangle(new Vector2(30, 25), new Vector2(40, 45)),
                new Vector2(40, 25),
                new Vector2(40, 35)),

            new Edge(
                new Rectangle(new Vector2(30, 25), new Vector2(40, 45)),
                new Rectangle(new Vector2(10, 32), new Vector2(30, 42)),
                new Vector2(30, 32),
                new Vector2(30, 42)),

            new Edge(
                new Rectangle(new Vector2(10, 32), new Vector2(30, 42)),
                new Rectangle(new Vector2(0, 20), new Vector2(10, 37)),
                new Vector2(10, 32),
                new Vector2(10, 37)),

            new Edge(
                new Rectangle(new Vector2(0, 20), new Vector2(10, 37)),
                new Rectangle(new Vector2(-5, 22), new Vector2(0, 28)),
                new Vector2(0, 22),
                new Vector2(0, 28)),

            new Edge(
                new Rectangle(new Vector2(-5, 22), new Vector2(0, 28)),
                new Rectangle(new Vector2(-15, 0), new Vector2(-5, 33)),
                new Vector2(-5, 22),
                new Vector2(-5, 28)),

            new Edge(
                new Rectangle(new Vector2(-15, 0), new Vector2(-5, 33)),
                new Rectangle(new Vector2(-5, 12), new Vector2(10, 17)),
                new Vector2(-5, 12),
                new Vector2(-5, 17)),

        };
        PathFinderService.PathService.GetPath(new Vector2(0, 3), new Vector2(10, 15), edges);
    }

    public void Test2()
    {
        List<Edge> edges = new List<Edge>
        {
            new Edge(
                new Rectangle(new Vector2(-15, 0), new Vector2(2, 10)),
                new Rectangle(new Vector2(-3, 10), new Vector2(17, 20)),
                new Vector2(-3, 10),
                new Vector2(2, 10)),

            new Edge(
                new Rectangle(new Vector2(-3, 10), new Vector2(17, 20)),
                new Rectangle(new Vector2(17, 5), new Vector2(37, 15)),
                new Vector2(17, 10),
                new Vector2(17, 15)),

             new Edge(
                new Rectangle(new Vector2(40, 15), new Vector2(55, 35)),
                new Rectangle(new Vector2(30, 25), new Vector2(40, 45)),
                new Vector2(40, 25),
                new Vector2(40, 35)),

            new Edge(
                new Rectangle(new Vector2(30, 25), new Vector2(40, 45)),
                new Rectangle(new Vector2(10, 32), new Vector2(30, 42)),
                new Vector2(30, 32),
                new Vector2(30, 42)),

            new Edge(
                new Rectangle(new Vector2(10, 32), new Vector2(30, 42)),
                new Rectangle(new Vector2(5, 25), new Vector2(10, 38)),
                new Vector2(10, 32),
                new Vector2(10, 38)),
        };
        PathFinderService.PathService.GetPath(new Vector2(0, 3), new Vector2(5, 37), edges);
    }

    public void Test3()
    {
        List<Edge> edges = new List<Edge>
        {
            new Edge(
                new Rectangle(new Vector2(-15, 15), new Vector2(2, 25)),
                new Rectangle(new Vector2(-3, 25), new Vector2(17, 35)),
                new Vector2(-3, 25),
                new Vector2(2, 25)),

            new Edge(
                new Rectangle(new Vector2(-3, 25), new Vector2(17, 35)),
                new Rectangle(new Vector2(17, 20), new Vector2(37, 30)),
                new Vector2(17, 25),
                new Vector2(17, 30)),
        };
        PathFinderService.PathService.GetPath(new Vector2(-6.5f, 15), new Vector2(37, 25), edges);
    }

    public void Test4()
    {
        List<Edge> edges = new List<Edge>
        {
            new Edge(
                new Rectangle(new Vector2(0,0), new Vector2(20,20)),
                new Rectangle(new Vector2(10,20), new Vector2(20, 45)),
                new Vector2(10,20),
                new Vector2(20,20)),

            new Edge(
                new Rectangle(new Vector2(10,20), new Vector2(20, 45)),
                new Rectangle(new Vector2(15,30), new Vector2(40, 40)),
                new Vector2(20,30),
                new Vector2(20,40)),
        };
        PathFinderService.PathService.GetPath(new Vector2(0, 0), new Vector2(20, 40), edges);
    }

    public void Test5()
    {
        List<Edge> edges = new List<Edge>
        {
            new Edge(
                new Rectangle(new Vector2(40, 15), new Vector2(55, 35)),
                new Rectangle(new Vector2(30, 25), new Vector2(40, 45)),
                new Vector2(40, 25),
                new Vector2(40, 35)),

            new Edge(
                new Rectangle(new Vector2(30, 25), new Vector2(40, 45)),
                new Rectangle(new Vector2(10, 32), new Vector2(30, 42)),
                new Vector2(30, 32),
                new Vector2(30, 42)),

            new Edge(
                new Rectangle(new Vector2(10, 32), new Vector2(30, 42)),
                new Rectangle(new Vector2(0, 20), new Vector2(10, 37)),
                new Vector2(10, 10),
                new Vector2(20, 20)),

            new Edge(
                new Rectangle(new Vector2(0, 20), new Vector2(10, 37)),
                new Rectangle(new Vector2(-5, 27), new Vector2(0, 30)),
                new Vector2(0, 27),
                new Vector2(0, 30)),

            new Edge(
                new Rectangle(new Vector2(-5, 27), new Vector2(0, 30)),
                new Rectangle(new Vector2(-15, 0), new Vector2(-5, 33)),
                new Vector2(-5, 27),
                new Vector2(-5, 30)),

            new Edge(
                new Rectangle(new Vector2(-15, 0), new Vector2(-5, 33)),
                new Rectangle(new Vector2(-5, 12), new Vector2(10, 17)),
                new Vector2(-5, 12),
                new Vector2(-5, 17)),
        };
        PathFinderService.PathService.GetPath(new Vector2(40, 15), new Vector2(-5, 17), edges);
    }

    public void Test6()
    {
        List<Edge> edges = new List<Edge>
        {
            new Edge(
                new Rectangle(new Vector2(-10,0), new Vector2(0,10)),
                new Rectangle(new Vector2(-10,10), new Vector2(0, 20)),
                new Vector2(-10,10),
                new Vector2(0,10)),

            new Edge(
                new Rectangle(new Vector2(-10,10), new Vector2(0, 20)),
                new Rectangle(new Vector2(-10,20), new Vector2(0, 30)),
                new Vector2(-10,20),
                new Vector2(0,20)),

            new Edge(
                new Rectangle(new Vector2(-10,20), new Vector2(0, 30)),
                new Rectangle(new Vector2(-10,30), new Vector2(0, 40)),
                new Vector2(-10,30),
                new Vector2(0,30)),

            new Edge(
                new Rectangle(new Vector2(-10,30), new Vector2(0, 40)),
                new Rectangle(new Vector2(0,30), new Vector2(10, 40)),
                new Vector2(0,30),
                new Vector2(0,40)),

            new Edge(
                new Rectangle(new Vector2(0,30), new Vector2(10, 40)),
                new Rectangle(new Vector2(10,30), new Vector2(20, 40)),
                new Vector2(10,30),
                new Vector2(10,40)),

            new Edge(
                new Rectangle(new Vector2(10,30), new Vector2(20, 40)),
                new Rectangle(new Vector2(20,30), new Vector2(30, 40)),
                new Vector2(20,30),
                new Vector2(20,40)),

            new Edge(
                new Rectangle(new Vector2(20,30), new Vector2(30, 40)),
                new Rectangle(new Vector2(30,30), new Vector2(40, 40)),
                new Vector2(30,30),
                new Vector2(30,40)),

            new Edge(
                new Rectangle(new Vector2(30,30), new Vector2(40, 40)),
                new Rectangle(new Vector2(30,30), new Vector2(40, 20)),
                new Vector2(30,30),
                new Vector2(40,30)),

            new Edge(
                new Rectangle(new Vector2(30,30), new Vector2(40, 20)),
                new Rectangle(new Vector2(30,20), new Vector2(40, 10)),
                new Vector2(30,20),
                new Vector2(40,20)),

            new Edge(
                new Rectangle(new Vector2(30,20), new Vector2(40, 10)),
                new Rectangle(new Vector2(30,10), new Vector2(40, 0)),
                new Vector2(30,10),
                new Vector2(40,10)),

            new Edge(
                new Rectangle(new Vector2(30,10), new Vector2(40, 0)),
                new Rectangle(new Vector2(20,0), new Vector2(30, 10)),
                new Vector2(30,0),
                new Vector2(30,10)),

            new Edge(
                new Rectangle(new Vector2(20,0), new Vector2(30, 10)),
                new Rectangle(new Vector2(10,0), new Vector2(20, 10)),
                new Vector2(20,0),
                new Vector2(20,10)),

            new Edge(
                new Rectangle(new Vector2(10,0), new Vector2(20, 10)),
                new Rectangle(new Vector2(10,10), new Vector2(20, 20)),
                new Vector2(10,10),
                new Vector2(20,10)),
        };
        PathFinderService.PathService.GetPath(new Vector2(-5, 0), new Vector2(15, 20), edges);
    }
}
