namespace DAS_Coursework;
//using utils;
using controller;
using DAS_Coursework.utils;
using data;
using models;

class Program
{
    static void Main(string[] args)
    {
        var graph = new TrainSystem();

        ////MainController.GetMainMain();
        var stationA = data.GetData.GetStationA();
        var stationB = data.GetData.GetStationB();
        var travelTimes = data.GetData.GetEdgeTime();
        var lines = data.GetData.GetLines();

        for (var i = 0; i < stationA.Length; i++)
        {
            graph.AddVertex(stationA[i]);
        }

        for (var i = 0; i < stationA.Length; i++)
        {
            graph.AddEdge(lines[i], stationA[i], stationB[i], travelTimes[i]);
        }

        string source = "WEMBLEY PARK";
        string destination = "PADDINGTON (H&C)";

        Dijkstra.ShortestPath(graph, graph.FindVertexByName(source), graph.FindVertexByName(destination));

        //Console.WriteLine();
        //Console.WriteLine("+++++++++++++++++++++++++returning++++++++++++++++++++++++++++++++++++++++++++++++");
        //Console.WriteLine();

        //string source1 = "LONDON BRIDGE";
        //string destination1 = "WEMBLEY PARK";
        //Dijkstra.ShortestPath(graph, graph.FindVertexByName(source1), graph.FindVertexByName(destination1));

        Console.ReadKey();
    }

}

