using ExcelDataReader;
using DAS_Coursework.models;

namespace DAS_Coursework.data
{
    //public static class GetData
    //{
    //    public static string[] GetStationA()
    //    {
    //        // Register encoding provider required by ExcelDataReader
    //        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

    //        // Path to the Excel file
    //        string filePath = @"/Users/otcheredev/Projects/DAS Coursework/DAS Coursework/data/istt.xlsx";

    //        try
    //        {
    //            string[] stations = new string[0];
    //            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
    //            {
    //                using (var reader = ExcelReaderFactory.CreateReader(stream))
    //                {
    //                    // Assuming the first row is the header and data starts from the second row
    //                    bool firstRow = true;

    //                    while (reader.Read()) // Each row of the file
    //                    {
    //                        if (firstRow)
    //                        {
    //                            firstRow = false; // Skip the header row
    //                            continue;
    //                        }

    //                        if(reader.GetString(2) != null)
    //                        {
    //                        var station =reader.GetString(2);
    //                        Array.Resize(ref stations, stations.Length + 1);
    //                        stations[stations.Length - 1] = station;
    //                        }



    //                    }
    //                }
    //            }

    //            // Optional: Print out the sections to verify

    //            return stations;

    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine("An error occurred: " + ex.Message);

    //            return new string[0];
    //        }
    //    }

    //    public static string[] GetStationB()
    //    {
    //        // Register encoding provider required by ExcelDataReader
    //        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

    //        // Path to the Excel file
    //        string filePath = @"/Users/otcheredev/Projects/DAS Coursework/DAS Coursework/data/istt.xlsx";

    //        try
    //        {
    //            string[] stations = new string[0];
    //            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
    //            {
    //                using (var reader = ExcelReaderFactory.CreateReader(stream))
    //                {
    //                    // Assuming the first row is the header and data starts from the second row
    //                    bool firstRow = true;

    //                    while (reader.Read()) // Each row of the file
    //                    {
    //                        if (firstRow)
    //                        {
    //                            firstRow = false; // Skip the header row
    //                            continue;
    //                        }

    //                        if (reader.GetString(3) != null)
    //                        {
    //                        var station = reader.GetString(3);
    //                        Array.Resize(ref stations, stations.Length + 1);
    //                        stations[stations.Length - 1] = station;

    //                        }



    //                    }
    //                }
    //            }

    //            // Optional: Print out the sections to verify

    //            return stations;

    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine("An error occurred: " + ex.Message);

    //            return new string[0];
    //        }
    //    }


    //    public static string[] GetLines()
    //    {
    //        // Register encoding provider required by ExcelDataReader
    //        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

    //        // Path to the Excel file
    //        string filePath = @"/Users/otcheredev/Projects/DAS Coursework/DAS Coursework/data/istt.xlsx";

    //        try
    //        {
    //            string[] stations = new string[0];
    //            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
    //            {
    //                using (var reader = ExcelReaderFactory.CreateReader(stream))
    //                {
    //                    // Assuming the first row is the header and data starts from the second row
    //                    bool firstRow = true;

    //                    while (reader.Read()) // Each row of the file
    //                    {
    //                        if (firstRow)
    //                        {
    //                            firstRow = false; // Skip the header row
    //                            continue;
    //                        }

    //                        if (reader.GetString(0) != null)
    //                        {

    //                        var station = reader.GetString(0);
    //                        Array.Resize(ref stations, stations.Length + 1);
    //                        stations[stations.Length - 1] = station;
    //                        }



    //                    }
    //                }
    //            }

    //            // Optional: Print out the sections to verify

    //            return stations;

    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine("An error occurred: " + ex.Message);

    //            return new string[0];
    //        }
    //    }


    //    public static double[] GetEdgeTime()
    //    {
    //        // Register encoding provider required by ExcelDataReader
    //        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

    //        // Path to the Excel file
    //        string filePath = @"/Users/otcheredev/Projects/DAS Coursework/DAS Coursework/data/istt.xlsx";

    //        try
    //        {
    //            double[] travelTimes = new double[0];
    //            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
    //            {
    //                using (var reader = ExcelReaderFactory.CreateReader(stream))
    //                {
    //                    // Assuming the first row is the header and data starts from the second row
    //                    bool firstRow = true;

    //                    while (reader.Read()) // Each row of the file
    //                    {
    //                        if (firstRow)
    //                        {
    //                            firstRow = false; // Skip the header row
    //                            continue;
    //                        }

    //                        if (reader.GetString(3) != null)
    //                        {
    //                        var times = reader.GetDouble(5);
    //                        Array.Resize(ref travelTimes, travelTimes.Length + 1);
    //                        travelTimes[travelTimes.Length - 1] = times;

    //                        }


    //                    }
    //                }
    //            }

    //            // Optional: Print out the sections to verify

    //            return travelTimes;

    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine("An error occurred: " + ex.Message);

    //            return new double[0];
    //        }
    //    }

    //}

    public static class GetData
    {
        //private static string FilePath = @"/Users/otcheredev/Projects/DAS Coursework/DAS Coursework/data/istt.xlsx";
        private static string FilePath = @"C:\Users\larte\source\repos\Data-structure-Coursework\DAS Coursework\data\istt.xlsx";
        private static IExcelDataReader GetExcelReader()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var stream = File.Open(FilePath, FileMode.Open, FileAccess.Read);
            return ExcelReaderFactory.CreateReader(stream);
        }

        private static string[] GetColumnData(int columnIndex)
        {
            string[] data = new string[0];
            using (var reader = GetExcelReader())
            {
                bool firstRow = true;
                int count = 0;
                while (reader.Read()) // Each row of the file
                {
                    if (firstRow)
                    {
                        firstRow = false; // Skip the header row
                        continue;
                    }
                    var value = reader.GetString(columnIndex);
                    if (!string.IsNullOrEmpty(value))
                    {
                        Array.Resize(ref data, count + 1);
                        data[count++] = value;
                    }
                }
            }
            return data;
        }

        public static string[] GetStationA() {
            var result = GetColumnData(2);


            return result;
        
        }
        

        public static string[] GetStationB() => GetColumnData(3);

        public static string[] GetLines() => GetColumnData(0);

        public static double[] GetEdgeTime()
        {
            double[] data = new double[0];
            using (var reader = GetExcelReader())
            {
                bool firstRow = true;
                int count = 0;
                while (reader.Read()) // Each row of the file
                {
                    if (firstRow)
                    {
                        firstRow = false; // Skip the header row
                        continue;
                    }
                  
                     Array.Resize(ref data, count + 1);
                    if (!reader.IsDBNull(5) && reader.GetDouble(5) != null)
                    {
                        data[count++] = reader.GetDouble(5);
                    }
                    else
                    {
                        data[count++] = 0;
                    }
                }
            }
            return data;
        }
    }

}


