
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
    }
}
