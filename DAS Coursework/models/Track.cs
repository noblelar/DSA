using DAS_Coursework.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS_Coursework.models
{
    internal class Track
    {
        private Guid id;
        private string name;
        private string direction;
        private List<Station> stations;
        private List<Section> stops;
        private Status status;

        public Track(string name, string direction, Status initialStatus = Status.Opened)
        {
            id = Guid.NewGuid(); 
            this.name = name;
            this.direction = direction;
            stations = new List<Station>(); 
            stops = new List<Section>(); 
            status = initialStatus;
        }
       
        
        public Status Status
        {
            get { return status; }
            set { status = value; }
        }

        public void AddStation(Station station)
        {
            if (station != null && !stations.Contains(station))
                stations.Add(station);
        }

        public void RemoveStation(Station station)
        {
            if (station != null)
                stations.Remove(station);
        }

        public void AddSection(Section section)
        {
            if (section != null && !stops.Contains(section))
                stops.Add(section);
        }

        public void RemoveSection(Section section)
        {
            if (section != null)
                stops.Remove(section);
        } 

    }

}