using DAS_Coursework.models;

namespace DAS_Coursework.utils
{
    public class Dijkstra
    {
        public static void ShortestPath(TrainSystem graph, Verticex source, Verticex destination)
        {
            PriorityQueue pq = new PriorityQueue();
            double[] distances = new double[graph.vertices.Length];
            Verticex[] predecessors = new Verticex[graph.vertices.Length];

            // Initialize distances
            for (int i = 0; i < graph.vertices.Length; i++)
            {
                distances[i] = (graph.vertices[i] == source) ? 0 : double.PositiveInfinity;
                pq.Enqueue(graph.vertices[i], distances[i]);
            }

            // Dijkstra's algorithm
            while (!pq.IsEmpty())
            {
                Verticex u = pq.Dequeue();
                if (u == destination) break;

                foreach (Edge edge in graph.edges)
                {
                    if (edge.fromVerticex == u)
                    {
                        double alt = distances[Array.IndexOf(graph.vertices, u)] + edge.weight;
                        if (alt < distances[Array.IndexOf(graph.vertices, edge.toVerticex)])
                        {
                            distances[Array.IndexOf(graph.vertices, edge.toVerticex)] = alt;
                            predecessors[Array.IndexOf(graph.vertices, edge.toVerticex)] = u;
                            pq.Enqueue(edge.toVerticex, alt);
                        }
                    }
                }
            }

            // Reconstruct shortest path
            Console.Write("Shortest path from " + source.Name + " to " + destination.Name + ": ");
            Verticex[] path = new Verticex[graph.vertices.Length];
            int pathLength = 0;
            Verticex currentVertex = destination;
            while (currentVertex != source)
            {
                path[pathLength++] = currentVertex;
                currentVertex = predecessors[Array.IndexOf(graph.vertices, currentVertex)];
                if (currentVertex == null)
                {
                    Console.WriteLine("There is no path from " + source.Name + " to " + destination.Name);
                    return;
                }
            }
            path[pathLength++] = source;

            // Print the path
            for (int i = pathLength - 1; i > 0; i--)
            {
                Console.Write(path[i].Name + " -> ");
            }
            Console.WriteLine(path[0].Name + ", Time(mins): " + distances[Array.IndexOf(graph.vertices, destination)]);
        }
    }

}

