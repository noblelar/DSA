using System;
using DAS_Coursework.models;

namespace DAS_Coursework.data
{
    public class Stations
    {
        public Stations()
        {
            List<Section> sections = new List<Section>();

            // Add sections for the Bakerloo and Central lines
            sections.Add(new Section("HARROW & WEALDSTONE", "KENTON", 1.74f, 2.23f, 2.5f, 2.5f));
            sections.Add(new Section("KENTON", "SOUTH KENTON", 1.4f, 1.88f, 2.0f, 2.0f));
            sections.Add(new Section("SOUTH KENTON", "NORTH WEMBLEY", 0.9f, 1.5f, 1.5f, 1.5f));
            sections.Add(new Section("NORTH WEMBLEY", "WEMBLEY CENTRAL", 1.27f, 1.92f, 2.06f, 2.06f));
            sections.Add(new Section("WEMBLEY CENTRAL", "STONEBRIDGE PARK", 1.71f, 2.23f, 3.13f, 3.13f));
            sections.Add(new Section("STONEBRIDGE PARK", "HARLESDEN", 1.63f, 1.83f, 2.25f, 2.25f));
            sections.Add(new Section("HARLESDEN", "WILLESDEN JUNCTION", 1.21f, 1.67f, 2.0f, 2.0f));
            sections.Add(new Section("WILLESDEN JUNCTION", "KENSAL GREEN", 1.3f, 1.75f, 2.08f, 2.08f));
            sections.Add(new Section("KENSAL GREEN", "QUEEN'S PARK", 1.24f, 1.67f, 2.0f, 2.0f));
            sections.Add(new Section("QUEEN'S PARK", "KILBURN PARK", 1.21f, 1.67f, 2.0f, 2.0f));
        }
    }
}

