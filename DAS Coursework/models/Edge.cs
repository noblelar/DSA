using System;
namespace DAS_Coursework.models
{
	public class Edge
	{
        public Guid id;
        public string line;
        public Verticex fromVerticex;
        public Verticex toVerticex;
        private double weightField;
        public double delay = 0;


        public double weight {
            get { return weightField + delay; }
            private set {}
        }

        public Edge(string line, Verticex fromVerticex, Verticex toVerticex, double weight)
		{
            id = Guid.NewGuid();
            this.line = line;
            this.fromVerticex = fromVerticex;
            this.toVerticex = toVerticex;
            this.weightField = weight;
        }

        public void AddDelay(double delayTime)
        {
            delay += delayTime;
        }

        public void RemoveDelay()
        {
            delay = 0;
        }
	}
}

