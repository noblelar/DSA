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
            Edge[] edgePredecessors = new Edge[graph.vertices.Length]; // Store the edge predecessors for line changes

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

                foreach (Edge edge in graph.GetOpenEdges())
                {
                    if (edge.fromVerticex == u)
                    {
                        double alt = distances[Array.IndexOf(graph.vertices, u)] + edge.weight;
                        // Consider the additional cost for changing lines
                        if (edgePredecessors[Array.IndexOf(graph.vertices, u)] != null && edgePredecessors[Array.IndexOf(graph.vertices, u)].line != edge.line)
                        {
                            alt += 2; // Additional cost for changing lines
                        }
                        if (alt < distances[Array.IndexOf(graph.vertices, edge.toVerticex)])
                        {
                            distances[Array.IndexOf(graph.vertices, edge.toVerticex)] = alt;
                            predecessors[Array.IndexOf(graph.vertices, edge.toVerticex)] = u;
                            edgePredecessors[Array.IndexOf(graph.vertices, edge.toVerticex)] = edge; // Store the edge as predecessor
                            pq.Enqueue(edge.toVerticex, alt);
                        }
                    }
                }
            }

            string endLine = "";
            string endDir = "";
            string startLine = "";
            string startDir = "";

            // Reconstruct shortest path
            Console.WriteLine("\nShortest path from " + source.Name + " to " + destination.Name + ": \n");
            Verticex currentVertex = destination;
            string[] path = new string[graph.vertices.Length * 2]; // Array to store the path
            int pathIndex = 0;
            while (currentVertex != source)
            {
                Verticex prevVertex = predecessors[Array.IndexOf(graph.vertices, currentVertex)];
                Edge connectingEdge = edgePredecessors[Array.IndexOf(graph.vertices, currentVertex)];
                if(pathIndex == 0)
                {
                   
                    endLine = connectingEdge.line;
                    endDir = connectingEdge.direction;
                }


                path[pathIndex++] = $"{connectingEdge.line} ({connectingEdge.direction}): {connectingEdge.fromVerticex.Name} to {connectingEdge.toVerticex.Name} {connectingEdge.weight}min";

                // Check for line change
                if (edgePredecessors[Array.IndexOf(graph.vertices, currentVertex)] != null)
                {
                    Edge prevEdge = edgePredecessors[Array.IndexOf(graph.vertices, prevVertex)];
                    if (prevEdge != null && connectingEdge!= null && prevEdge.line != connectingEdge.line)
                    {
                        path[pathIndex++] = $"Change: {prevEdge.line} to {connectingEdge.line} 2.00min";
                    }
                }
               
                currentVertex = prevVertex;
                if (currentVertex == null)
                {
                    Console.WriteLine($"There is no path from {source.Name} to {destination.Name}");
                    return;
                }

                startDir = connectingEdge.direction;
                startLine = connectingEdge.line;
            }
            path[pathIndex++] = $"Start: {source.Name}, {startLine} ({startDir})";

            // Print the path
            for (int i = pathIndex - 1; i >= 0; i--)
            {
                Console.WriteLine($"\n({pathIndex - i}) {path[i]}");
            }
            Console.WriteLine($"\n({pathIndex+1}) End: {destination.Name}, {endLine} ({endDir})");
            Console.WriteLine($"\nTotal Time: {distances[Array.IndexOf(graph.vertices, destination)]} minutes");
        }

    }

}

