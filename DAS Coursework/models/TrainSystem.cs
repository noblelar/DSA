
using DAS_Coursework.controller;

namespace DAS_Coursework.models
{
    public class TrainSystem
    {
        public Verticex[] vertices;
        public Edge[] edges;
        public Delay[] delays;

        public TrainSystem()
        {
            vertices = new Verticex[0];
            edges = new Edge[0];
            delays = new Delay[0];
        }

        public void AddVertex(string vertexName)
        {
            // Check if the vertex with the same name already exists
            if (FindVertexByName(vertexName) != null)
            {
                return;
            }

            // Resize the vertices array and add the new vertex
            Array.Resize(ref vertices, vertices.Length + 1);
            vertices[vertices.Length - 1] = new Verticex(vertexName);
        }

        public string[] GetAllVertexNames()
        {
            string[] vertexNames = new string[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                vertexNames[i] = vertices[i].Name;
            }
            return vertexNames;
        }

        public Edge[] GetOpenEdges()
        {
            // Count the number of open edges
            int openEdgeCount = 0;
            foreach (Edge edge in edges)
            {
                if (!edge.isClosed)
                {
                    openEdgeCount++;
                }
            }

            // Create an array to store the open edges
            Edge[] openEdges = new Edge[openEdgeCount];
            int index = 0;

            // Populate the array with open edges
            foreach (Edge edge in edges)
            {
                if (!edge.isClosed)
                {
                    openEdges[index++] = edge;
                }
            }

            return openEdges;
        }


        public void AddEdge(string line, string fromVertexName, string toVertexName, double weight, string direction)
        {
            Verticex fromVertex = FindVertexByName(fromVertexName);
            Verticex toVertex = FindVertexByName(toVertexName);

            if (fromVertex == null || toVertex == null)
            {
                return;
            }

            Array.Resize(ref edges, edges.Length + 1);
            edges[edges.Length - 1] = new Edge(line, fromVertex, toVertex, weight, direction);
        }


        public void GetGraph()
        {
            Console.WriteLine("Vertices:");
            foreach (var vertex in vertices)
            {
                Console.WriteLine(vertex.Name);
            }
        }

        public void GetEdges()
        {
            Console.WriteLine("Edges:");
            foreach (var edge in edges)
            {
                Console.WriteLine($"{edge.fromVerticex.Name} -> {edge.toVerticex.Name}, Weight: {edge.weight}");
            }
        }

        public Verticex FindVertexByName(string vertexName)
        {
            foreach (var vertex in vertices)
            {
                if (vertex.Name == vertexName)
                {
                    return vertex;
                }
            }
            return null;
        }

        public string[] VerticesConnectedToLine(string lineName)
        {
            // Temporary array to store connected vertices' names
            string[] connectedVertices = new string[vertices.Length];
            int count = 0; // Counter for the number of connected vertices

            // Iterate through all edges
            foreach (Edge edge in edges)
            {
                // Check if the edge belongs to the specified line
                if (edge.line == lineName)
                {
                    // Add the 'from' vertex if not already added
                    if (!IsVertexNameInArray(connectedVertices, count, edge.fromVerticex.Name))
                    {
                        connectedVertices[count++] = edge.fromVerticex.Name;
                    }

                    // Add the 'to' vertex if not already added
                    if (!IsVertexNameInArray(connectedVertices, count, edge.toVerticex.Name))
                    {
                        connectedVertices[count++] = edge.toVerticex.Name;
                    }
                }
            }

            // Trim the array to remove any unused elements
            Array.Resize(ref connectedVertices, count);

            return connectedVertices;

        }

        public string[] FindLineDirections(string lineName)
        {
            // Temporary array to store connected vertices' names
            string[] directions = new string[vertices.Length];
            int count = 0; // Counter for the number of connected vertices

            // Iterate through all edges
            foreach (Edge edge in edges)
            {
                // Check if the edge belongs to the specified line
                if (edge.line == lineName && !IsVertexNameInArray(directions, count, edge.direction))
                {
                    directions[count++] = edge.direction;
                }
            }

            // Trim the array to remove any unused elements
            Array.Resize(ref directions, count);

            return directions;

        }

        // Helper function to check if a vertex is already present in the array
        private static bool IsVertexNameInArray(string[] vertices, int count, string vertexName)
        {
            for (int i = 0; i < count; i++)
            {
                if (vertices[i] == vertexName)
                {
                    return true;
                }
            }
            return false;
        }

        public Edge GetEdgeWithStartVertexAndLine(string startVertexName, string lineName)
        {
            // Iterate through all edges
            foreach (Edge edge in edges)
            {
                // Check if the edge belongs to the specified line and has the start vertex
                if (edge.line == lineName && edge.fromVerticex.Name == startVertexName)
                {
                    // Return the edge if found
                    return edge;
                }
            }

            // Return null if no such edge is found
            return null;
        }

        public Edge GetEdge(string startVertexName, string lineName)
        {
            // Iterate through all edges
            foreach (Edge edge in edges)
            {
                // Check if the edge belongs to the specified line and has the start vertex
                if (edge.line == lineName && edge.fromVerticex.Name == startVertexName)
                {
                    // Return the edge if found
                    return edge;
                }
            }

            // Return null if no such edge is found
            return null;
        }

        public bool AddDelayToEdge(string startVertexName, string lineName, double delay, string direction, string endVertexName)
        {
            var delayingEdge = FindVertexByName(startVertexName);
            var ending = FindVertexByName(endVertexName);

            if (delayingEdge == null || ending == null)
            {
                return false;
            }
            else
            {

                if (direction != "BOTH")
                {
                    var path = FindPathBetweenStations(startVertexName, endVertexName, lineName, direction, false);
                    if (path.Length == 0)
                    {
                        Console.WriteLine($"There is no path from {startVertexName} to {endVertexName} on the {lineName} line");
                        return false;
                    }

                    var newDelay = new Delay(delay, FindVertexByName(startVertexName), FindVertexByName(endVertexName), direction, lineName);
                    path[0].AddDelay(delay);
                    Array.Resize(ref delays, delays.Length + 1);
                    delays[delays.Length - 1] = newDelay;

                }
                else
                {
                    string[] directionOption = FindLineDirections(lineName);

                    Edge[] start;
                    Edge[] end;

                    start = FindPathBetweenStations(startVertexName, endVertexName, lineName, directionOption[0], false);

                    if (start.Length == 0)
                    {
                        start = FindPathBetweenStations(endVertexName, startVertexName, lineName, directionOption[0], false);
                    }

                    end = FindPathBetweenStations(startVertexName, endVertexName, lineName, directionOption[1], false);

                    if (end.Length == 0)
                    {
                        end = FindPathBetweenStations(endVertexName, startVertexName, lineName, directionOption[1], false);
                    }

                    if (end.Length != 0 && start.Length != 0)
                    {
                        start[0].AddDelay(delay);
                        end[0].AddDelay(delay);

                        var newDelay = new Delay(delay, FindVertexByName(startVertexName), FindVertexByName(endVertexName), start[0].direction, lineName);
                        Array.Resize(ref delays, delays.Length + 1);
                        delays[delays.Length - 1] = newDelay;

                        newDelay = new Delay(delay, FindVertexByName(endVertexName), FindVertexByName(startVertexName), end[0].direction, lineName);
                        Array.Resize(ref delays, delays.Length + 1);
                        delays[delays.Length - 1] = newDelay;
                    }
                    else
                    {
                        Console.WriteLine($"There is no path from {startVertexName} to {endVertexName} on the {lineName} line");
                        return false;
                    }
                }


            }

            Console.WriteLine($"delays are  {delays.Length}");

            return true;
        } 

        public Delay? FindDelay(string startStationName, string endStationName, string lineName, string direction)
        {
            // Filter delays based on start station name, end station name, and line
            var delay = delays.FirstOrDefault(delay =>
                delay.StartStation.Name == startStationName &&
                delay.EndStation.Name == endStationName &&
                delay.Line == lineName && delay.Direction == direction) ;

            return delay;
        }

        public bool RemoveDelay(Delay delayToRemove)
        {
            delays = delays.Where(delay => delay.ID != delayToRemove.ID).ToArray();
            return true;  
        }

        public Edge[] FindPathBetweenStations(string startVertexName, string endVertexName, string lineName, string direction, Boolean status)
        {
            // Find the starting vertex
            Verticex startVertex = FindVertexByName(startVertexName);
            Verticex endVertex = FindVertexByName(endVertexName);
            Edge[] path = new Edge[vertices.Length]; // Maximum possible path length is the number of vertices
            bool[] visited = new bool[vertices.Length];

            // Start DFS from the starting vertex
            if (DFS(startVertex, endVertex, lineName, visited, path, 0, direction, status))
            {
                // Trim the path array to remove null entries
                int pathLength = 0;
                for (int i = 0; i < path.Length; i++)
                {
                    if (path[i] != null)
                    {
                        pathLength++;
                    }
                    else
                    {
                        break;
                    }
                }
                Array.Resize(ref path, pathLength);
                return path;
            }
            else
            {
                return new Edge[0]; // No path found, return an empty array
            }
        }

        private bool DFS(Verticex currentVertex, Verticex endVertex, string lineName, bool[] visited, Edge[] path, int pathIndex, string direction, bool status)
        {
            // Mark the current vertex as visited
            visited[Array.IndexOf(vertices, currentVertex)] = true;

            // Base case: if the current vertex is the end vertex, return true
            if (currentVertex == endVertex)
                return true;

            // Iterate through the edges connected to the current vertex
            foreach (Edge edge in edges)
            {
                // Check if the edge belongs to the specified line and has the current vertex as the starting point
                if (edge.line == lineName && edge.isClosed == status && edge.direction == direction && edge.fromVerticex == currentVertex && !visited[Array.IndexOf(vertices, edge.toVerticex)])
                {
                    // Recursively explore the neighboring vertices
                    if (DFS(edge.toVerticex, endVertex, lineName, visited, path, pathIndex + 1, direction, status))
                    {
                        // If the path is found, add the current edge to the path and return true
                        path[pathIndex] = edge;
                        return true;
                    }
                }
            }

            // If no path is found, return false
            return false;
        }

        public bool CloseEdges(Edge[] path)
        {
            foreach(var edge in path)
            {
                edge.Close();
            }
            return true;
        }

        public bool OpenEdges(Edge[] path)
        {
            foreach (var edge in path)
            {
                edge.Open();
            }
            return true;
        }

        public Edge[] FindTracks(string lineName, string direction, Boolean isClose)
        {
            Edge[] tracks = new Edge[edges.Length]; // Initialize array to store closed tracks
            int count = 0; // Counter to keep track of the number of closed tracks found

            // Iterate through all edges belonging to the specified line
            foreach (Edge edge in edges)
            {
                if (edge.line == lineName && edge.direction == direction && isClose == edge.isClosed)
                {
                    tracks[count++] = edge; // Add the edge to the array and increment the counter
                }
            }

            // Trim the array to remove any unused elements
            Array.Resize(ref tracks, count);

            return tracks;
        }
    }
}
