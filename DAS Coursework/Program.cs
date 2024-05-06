namespace DAS_Coursework;
using utils;
using controller;
using data;

class Program
{
    static void Main(string[] args)
    {
        ////MainController.GetMainMain();
        //data.GetData.GetSectionData();
        //Console.ReadKey();

        var graph = new TrainSystem();

        // Add vertices to the graph
        string[] allVertices = {
            "STANMORE", "CANONS PARK", "QUEENSBURY", "KINGSBURY", "WEMBLEY PARK", "NEASDEN",
            "DOLLIS HILL", "WILLESDEN GREEN", "KILBURN", "WEST HAMPSTEAD", "FINCHLEY ROAD",
            "SWISS COTTAGE", "ST JOHNS WOOD", "BAKER STREET", "BOND STREET", "GREEN PARK",
            "WESTMINSTER", "WATERLOO", "SOUTHWARK", "LONDON BRIDGE", "BERMONDSEY",
            "CANADA WATER", "CANARY WHARF", "NORTH GREENWICH", "CANNING TOWN", "WEST HAM",
            "STRATFORD", "AMERSHAM", "CHESHAM", "CHALFONT & LATIMER", "CHORLEYWOOD",
            "RICKMANSWORTH", "WATFORD", "CROXLEY", "MOOR PARK", "NORTHWOOD", "NORTHWOOD HILLS",
            "PINNER", "NORTH HARROW", "UXBRIDGE", "HILLINGDON", "ICKENHAM", "RUISLIP",
            "RUISLIP MANOR", "EASTCOTE", "RAYNERS LANE", "WEST HARROW", "HARROW-ON-THE-HILL",
            "NORTHWICK PARK", "PRESTON ROAD", "GREAT PORTLAND STREET", "EUSTON SQUARE",
            "KINGS CROSS ST PANCRAS", "FARRINGDON", "BARBICAN", "MOORGATE", "LIVERPOOL STREET",
            "ALDGATE"
        };

        foreach (var vertex in allVertices)
        {
            graph.AddVertex(vertex);
        }

        // Add edges to the graph
        graph.AddEdge("AMERSHAM", "CHALFONT & LATIMER", 3.65);
        graph.AddEdge("CHESHAM", "CHALFONT & LATIMER", 9.63);
        graph.AddEdge("CHALFONT & LATIMER", "CHORLEYWOOD", 3.68);
        graph.AddEdge("CHORLEYWOOD", "RICKMANSWORTH", 4.07);
        graph.AddEdge("RICKMANSWORTH", "MOOR PARK", 3.78);
        graph.AddEdge("WATFORD", "CROXLEY", 3.22);
        graph.AddEdge("CROXLEY", "MOOR PARK", 4.42);
        graph.AddEdge("MOOR PARK", "NORTHWOOD", 2.87);
        graph.AddEdge("NORTHWOOD", "NORTHWOOD HILLS", 2.2);
        graph.AddEdge("NORTHWOOD HILLS", "PINNER", 2.48);
        graph.AddEdge("PINNER", "NORTH HARROW", 2.08);
        graph.AddEdge("NORTH HARROW", "HARROW-ON-THE-HILL", 3.08);
        graph.AddEdge("MOOR PARK", "HARROW-ON-THE-HILL", 9.62);
        graph.AddEdge("UXBRIDGE", "HILLINGDON", 3.0);
        graph.AddEdge("HILLINGDON", "ICKENHAM", 1.73);
        graph.AddEdge("ICKENHAM", "RUISLIP", 2.45);
        graph.AddEdge("RUISLIP", "RUISLIP MANOR", 1.45);
        graph.AddEdge("RUISLIP MANOR", "EASTCOTE", 1.82);
        graph.AddEdge("EASTCOTE", "RAYNERS LANE", 2.33);
        graph.AddEdge("RAYNERS LANE", "WEST HARROW", 2.08);
        graph.AddEdge("WEST HARROW", "HARROW-ON-THE-HILL", 2.15);
        graph.AddEdge("HARROW-ON-THE-HILL", "FINCHLEY ROAD", 10.15);
        graph.AddEdge("HARROW-ON-THE-HILL", "NORTHWICK PARK", 1.9);
        graph.AddEdge("NORTHWICK PARK", "PRESTON ROAD", 2.05);
        graph.AddEdge("PRESTON ROAD", "WEMBLEY PARK", 1.98);
        graph.AddEdge("WEMBLEY PARK", "FINCHLEY ROAD", 7.0);
        graph.AddEdge("FINCHLEY ROAD", "BAKER STREET", 6.13);
        graph.AddEdge("BAKER STREET", "GREAT PORTLAND STREET", 1.98);
        graph.AddEdge("GREAT PORTLAND STREET", "EUSTON SQUARE", 1.25);
        graph.AddEdge("EUSTON SQUARE", "KINGS CROSS ST PANCRAS", 1.75);
        graph.AddEdge("KINGS CROSS ST PANCRAS", "FARRINGDON", 2.98);
        graph.AddEdge("FARRINGDON", "BARBICAN", 1.22);
        graph.AddEdge("BARBICAN", "MOORGATE", 1.32);
        graph.AddEdge("MOORGATE", "LIVERPOOL STREET", 1.18);
        graph.AddEdge("LIVERPOOL STREET", "ALDGATE", 2.18);
        graph.AddEdge("CHALFONT & LATIMER", "AMERSHAM", 4.07);
        graph.AddEdge("CHALFONT & LATIMER", "CHESHAM", 9.48);
        graph.AddEdge("CHORLEYWOOD", "CHALFONT & LATIMER", 3.95);
        graph.AddEdge("RICKMANSWORTH", "CHORLEYWOOD", 4.03);
        graph.AddEdge("MOOR PARK", "RICKMANSWORTH", 4.13);
        graph.AddEdge("CROXLEY", "WATFORD", 3.6);
        graph.AddEdge("MOOR PARK", "CROXLEY", 4.33);
        graph.AddEdge("HARROW-ON-THE-HILL", "MOOR PARK", 9.03);
        graph.AddEdge("NORTHWOOD", "MOOR PARK", 2.55);
        graph.AddEdge("NORTHWOOD HILLS", "NORTHWOOD", 2.23);
        graph.AddEdge("PINNER", "NORTHWOOD HILLS", 2.6);
        graph.AddEdge("NORTH HARROW", "PINNER", 2.0);
        graph.AddEdge("HARROW-ON-THE-HILL", "NORTH HARROW", 3.18);
        graph.AddEdge("HILLINGDON", "UXBRIDGE", 4.08);
        graph.AddEdge("ICKENHAM", "HILLINGDON", 1.75);
        graph.AddEdge("RUISLIP", "ICKENHAM", 2.57);
        graph.AddEdge("RUISLIP MANOR", "RUISLIP", 1.35);
        graph.AddEdge("EASTCOTE", "RUISLIP MANOR", 1.82);
        graph.AddEdge("RAYNERS LANE", "EASTCOTE", 2.23);
        graph.AddEdge("WEST HARROW", "RAYNERS LANE", 2.05);
        graph.AddEdge("HARROW-ON-THE-HILL", "WEST HARROW", 2.32);
        graph.AddEdge("FINCHLEY ROAD", "HARROW-ON-THE-HILL", 11.02);
        graph.AddEdge("NORTHWICK PARK", "HARROW-ON-THE-HILL", 2.32);
        graph.AddEdge("PRESTON ROAD", "NORTHWICK PARK", 2.3);
        graph.AddEdge("WEMBLEY PARK", "PRESTON ROAD", 2.12);
        graph.AddEdge("FINCHLEY ROAD", "WEMBLEY PARK", 7.05);
        graph.AddEdge("BAKER STREET", "FINCHLEY ROAD", 5.55);
        graph.AddEdge("GREAT PORTLAND STREET", "BAKER STREET", 2.23);
        graph.AddEdge("EUSTON SQUARE", "GREAT PORTLAND STREET", 1.3);
        graph.AddEdge("KINGS CROSS ST PANCRAS", "EUSTON SQUARE", 1.65);
        graph.AddEdge("FARRINGDON", "KINGS CROSS ST PANCRAS", 3.12);
        graph.AddEdge("BARBICAN", "FARRINGDON", 1.2);
        graph.AddEdge("MOORGATE", "BARBICAN", 1.38);
        graph.AddEdge("LIVERPOOL STREET", "MOORGATE", 1.32);
        graph.AddEdge("ALDGATE", "LIVERPOOL STREET", 1.75);
        graph.AddEdge("STANMORE", "CANONS PARK", 1.95);
        graph.AddEdge("CANONS PARK", "QUEENSBURY", 1.93);
        graph.AddEdge("QUEENSBURY", "KINGSBURY", 1.72);
        graph.AddEdge("KINGSBURY", "WEMBLEY PARK", 3.47);
        graph.AddEdge("WEMBLEY PARK", "NEASDEN", 2.6);
        graph.AddEdge("NEASDEN", "DOLLIS HILL", 1.43);
        graph.AddEdge("DOLLIS HILL", "WILLESDEN GREEN", 1.8);
        graph.AddEdge("WILLESDEN GREEN", "KILBURN", 1.68);
        graph.AddEdge("KILBURN", "WEST HAMPSTEAD", 1.63);
        graph.AddEdge("WEST HAMPSTEAD", "FINCHLEY ROAD", 1.25);
        graph.AddEdge("FINCHLEY ROAD", "SWISS COTTAGE", 1.18);
        graph.AddEdge("SWISS COTTAGE", "ST JOHNS WOOD", 1.52);
        graph.AddEdge("ST JOHNS WOOD", "BAKER STREET", 2.77);
        graph.AddEdge("BAKER STREET", "BOND STREET", 2.1);
        graph.AddEdge("BOND STREET", "GREEN PARK", 1.78);
        graph.AddEdge("GREEN PARK", "WESTMINSTER", 1.87);
        graph.AddEdge("WESTMINSTER", "WATERLOO", 1.38);
        graph.AddEdge("WATERLOO", "SOUTHWARK", 1.02);
        graph.AddEdge("SOUTHWARK", "LONDON BRIDGE", 1.65);
        graph.AddEdge("LONDON BRIDGE", "BERMONDSEY", 2.25);
        graph.AddEdge("BERMONDSEY", "CANADA WATER", 1.48);
        graph.AddEdge("CANADA WATER", "CANARY WHARF", 2.5);
        graph.AddEdge("CANARY WHARF", "NORTH GREENWICH", 2.23);
        graph.AddEdge("NORTH GREENWICH", "CANNING TOWN", 2.15);
        graph.AddEdge("CANNING TOWN", "WEST HAM", 2.15);
        graph.AddEdge("WEST HAM", "STRATFORD", 3.15);
        graph.AddEdge("STRATFORD", "WEST HAM", 2.42);
        graph.AddEdge("WEST HAM", "CANNING TOWN", 2.13);
        graph.AddEdge("CANNING TOWN", "NORTH GREENWICH", 2.17);
        graph.AddEdge("NORTH GREENWICH", "CANARY WHARF", 1.98);
        graph.AddEdge("CANARY WHARF", "CANADA WATER", 2.63);
        graph.AddEdge("CANADA WATER", "BERMONDSEY", 1.52);
        graph.AddEdge("BERMONDSEY", "LONDON BRIDGE", 2.17);
        graph.AddEdge("LONDON BRIDGE", "SOUTHWARK", 1.77);
        graph.AddEdge("SOUTHWARK", "WATERLOO", 0.97);
        graph.AddEdge("WATERLOO", "WESTMINSTER", 1.43);
        graph.AddEdge("WESTMINSTER", "GREEN PARK", 1.82);
        graph.AddEdge("GREEN PARK", "BOND STREET", 1.82);
        graph.AddEdge("BOND STREET", "BAKER STREET", 2.28);
        graph.AddEdge("BAKER STREET", "ST JOHNS WOOD", 2.85);
        graph.AddEdge("ST JOHNS WOOD", "SWISS COTTAGE", 1.52);
        graph.AddEdge("SWISS COTTAGE", "FINCHLEY ROAD", 1.18);
        graph.AddEdge("FINCHLEY ROAD", "WEST HAMPSTEAD", 1.2);
        graph.AddEdge("WEST HAMPSTEAD", "KILBURN", 1.55);
        graph.AddEdge("KILBURN", "WILLESDEN GREEN", 2.07);
        graph.AddEdge("WILLESDEN GREEN", "DOLLIS HILL", 1.67);
        graph.AddEdge("DOLLIS HILL", "NEASDEN", 1.38);
        graph.AddEdge("NEASDEN", "WEMBLEY PARK", 2.65);
        graph.AddEdge("WEMBLEY PARK", "KINGSBURY", 3.47);
        graph.AddEdge("KINGSBURY", "QUEENSBURY", 1.85);
        graph.AddEdge("QUEENSBURY", "CANONS PARK", 2.23);
        graph.AddEdge("CANONS PARK", "STANMORE", 2.87);

        string source = "WEMBLEY PARK";
        string destination = "LONDON BRIDGE";
        Dijkstra.ShortestPath(graph, source, destination);

        Console.WriteLine();
        Console.WriteLine("+++++++++++++++++++++++++returning++++++++++++++++++++++++++++++++++++++++++++++++");
        Console.WriteLine();

        string source1 = "LONDON BRIDGE";
        string destination1 = "WEMBLEY PARK";
        Dijkstra.ShortestPath(graph, source1, destination1);

        Console.ReadKey();
    }

}

