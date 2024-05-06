using System;
namespace DAS_Coursework.utils
{
	public class TrainSystem
	{
        private string[] vertices;
        private double[,] weights;

        public TrainSystem()
		{
            this.vertices = new string[0];
            this.weights = new double[0, 0];
        }

        public void AddVertex(string vertex)
        {
            // Resize vertices array
            Array.Resize(ref this.vertices, this.vertices.Length + 1);
            this.vertices[this.vertices.Length - 1] = vertex;

            // Resize weights array
            double[,] newWeights = new double[this.vertices.Length, this.vertices.Length];
            for (int i = 0; i < this.vertices.Length - 1; i++)
            {
                for (int j = 0; j < this.vertices.Length - 1; j++)
                {
                    newWeights[i, j] = this.weights[i, j];
                }
            }

            this.weights = newWeights;
        }

        public void AddEdge(string fromVertex, string toVertex, double weight)
        {
            int fromIndex = Array.IndexOf(this.vertices, fromVertex);
            int toIndex = Array.IndexOf(this.vertices, toVertex);
            if (fromIndex == -1 || toIndex == -1)
            {
                throw new Exception("Vertex not found");
            }
            this.weights[fromIndex, toIndex] = weight;
        }

        public string[] Vertices
        {
            get { return this.vertices; }
        }

        public double[,] Weights
        {
            get { return this.weights; }
        }
    }
}

