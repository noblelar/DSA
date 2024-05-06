using System;

namespace DAS_Coursework.utils
{
    public class Dijkstra
    {
        public static void ShortestPath(TrainSystem graph, string source, string destination)
        {
            string[] vertices = graph.Vertices;
            double[,] weights = graph.Weights;

            double[] distances = new double[vertices.Length];
            string[] predecessors = new string[vertices.Length];
            PriorityQueue pq = new PriorityQueue();

            // Initialize distances and predecessors
            for (int i = 0; i < vertices.Length; i++)
            {
                distances[i] = vertices[i] == source ? 0 : double.PositiveInfinity;
                predecessors[i] = null;
                pq.Enqueue(vertices[i], distances[i]);
            }

            // Dijkstra's algorithm
            while (!pq.IsEmpty())
            {
                var u = pq.Dequeue();

                if (u == destination) break; // Stop when destination is reached

                int uIndex = Array.IndexOf(vertices, u);
                for (int i = 0; i < vertices.Length; i++)
                {
                    if (weights[uIndex, i] > 0)
                    {
                        var alt = distances[uIndex] + weights[uIndex, i];
                        if (alt < distances[i])
                        {
                            distances[i] = alt;
                            predecessors[i] = u;
                            pq.Enqueue(vertices[i], alt);
                        }
                    }
                }
            }

            // Reconstruct shortest path
            string[] shortestPath = new string[0];
            string currentVertex = destination;
            while (currentVertex != source)
            {
                Array.Resize(ref shortestPath, shortestPath.Length + 1);
                shortestPath[shortestPath.Length - 1] = currentVertex;
                int currentIndex = Array.IndexOf(vertices, currentVertex);
                currentVertex = predecessors[currentIndex];
                if (currentVertex == null)
                {
                    Console.WriteLine($"There is no path from {source} to {destination}");
                    return;
                }
            }
            Array.Resize(ref shortestPath, shortestPath.Length + 1);
            shortestPath[shortestPath.Length - 1] = source;

            Array.Reverse(shortestPath);
            Console.WriteLine($"Shortest path from {source} to {destination}: {string.Join(" -> ", shortestPath)}, Distance: {distances[Array.IndexOf(vertices, destination)]}");
        }
    }

}

