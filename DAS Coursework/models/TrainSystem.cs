
namespace DAS_Coursework.models
{
    public class TrainSystem
    {
        public Verticex[] vertices;
        public Edge[] edges;

        public TrainSystem()
        {
            vertices = new Verticex[0];
            edges = new Edge[0];
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


        public void AddEdge(string line, string fromVertexName, string toVertexName, double weight)
        {
            Verticex fromVertex = FindVertexByName(fromVertexName);
            Verticex toVertex = FindVertexByName(toVertexName);

            if (fromVertex == null || toVertex == null)
            {
                return;
            }

            Array.Resize(ref edges, edges.Length + 1);
            edges[edges.Length - 1] = new Edge(line, fromVertex, toVertex, weight);
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

        public  string[] VerticesConnectedToLine(string lineName)
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

        public bool AddDelayToEdge(string startVertexName, string lineName, double delay)
        {
            var delayingEdge = GetEdgeWithStartVertexAndLine(startVertexName, lineName);
            if (delayingEdge == null)
            {
                return false;
            }
            else
            {
                delayingEdge.AddDelay(delay);

            }

            return false;
        }
    }
}
