using DAS_Coursework.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS_Coursework.models
{
    internal class Station
    {
        private Guid id;
        private string name;
        private List<Line> linesPassingThrough;
        private Status status;

        public Station(string name, Status initialStatus)
        {
            id = Guid.NewGuid();
            this.name = name;
            linesPassingThrough = new List<Line>(); 
            status = initialStatus;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public List<Line> LinesPassingThrough => linesPassingThrough;
        public Status Status
        {
            get { return status; }
            set { status = value; }
        }

        // Methods to manage lines passing through the station
        public void AddLine(Line line)
        {
            if (line != null && !linesPassingThrough.Contains(line))
                linesPassingThrough.Add(line);
        }

        public void RemoveLine(Line line)
        {
            if (line != null)
                linesPassingThrough.Remove(line);
        }

    }
}
