using DAS_Coursework.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS_Coursework.models
{
    internal class Line
    {
        private Guid id;
        private string name;
        private List<Track> tracks;
        private List<Station> stations;
        private Status status;


        // Constructor
        // Constructor
        public Line(string name, Status initialStatus)
        {
            id = Guid.NewGuid(); // Automatically assign a new unique identifier
            this.name = name;
            tracks = new List<Track>(); // Initialize the list of tracks
            stations = new List<Station>(); // Initialize the list of stations
            status = initialStatus;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public List<Track> Tracks => tracks;
        public List<Station> Stations => stations;
        public Status Status
        {
            get { return status; }
            set { status = value; }
        }

        // Methods for managing tracks
        public void AddTrack(Track track)
        {
            if (track != null && !tracks.Contains(track))
                tracks.Add(track);
        }

        public void RemoveTrack(Track track)
        {
            if (track != null)
                tracks.Remove(track);
        }

        // Methods for managing stations
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
    }

}
