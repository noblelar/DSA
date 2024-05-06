using System;
namespace DAS_Coursework.models
{
	public class Edge
	{
        public Guid id;
        public string line;
        public Verticex fromVerticex;
        public Verticex toVerticex;
        public double weight;

        public Edge(string line, Verticex fromVerticex, Verticex toVerticex, double weight)
		{
            id = Guid.NewGuid();
            this.line = line;
            this.fromVerticex = fromVerticex;
            this.toVerticex = toVerticex;
            this.weight = weight;
        }
	}
}

