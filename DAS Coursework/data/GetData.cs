using ExcelDataReader;
using DAS_Coursework.models;
using System.Reflection.PortableExecutable;

namespace DAS_Coursework.data
{
    public static class GetData
    {
        // private static string FilePath = @"/Users/otcheredev/Projects/DAS Coursework/DAS Coursework/data/isst.xlsx";
        private static string FilePath = @"C:\Users\larte\source\repos\Data-structure-Coursework\DAS Coursework\data\isst.xlsx";

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
                bool secondRow = true;
                int count = 0;
                while (reader.Read()) // Each row of the file
                {
                    if (firstRow)
                    {
                        firstRow = false;
                        continue; // Continue to the next iteration of the loop

                    }

                    if (secondRow)
                    {
                        secondRow = false;
                        continue; // Continue to the next iteration of the loop

                    }
                    var value = reader.GetString(columnIndex);
                    if (!string.IsNullOrEmpty(value))
                    {
                        Array.Resize(ref data, count + 1);
                        data[count++] = value;
                        //Console.WriteLine(value);
                    }
                }
            }
            return data;
        }

        public static string[] GetStationA()
        {
            var result = GetColumnData(2);


            return result;

        }


        public static string[] GetStationB() => GetColumnData(3);

        public static string[] GetLines() => GetColumnData(0);

        public static string[] GetUniqueLines()
        {
            var allLines = GetLines();

            string[] lines = new string[0];
            int count = 0;

            foreach (var line in allLines)
            {
                if (!lines.Contains(line))
                {
                    Array.Resize(ref lines, count + 1);
                    lines[count++] = line;

                }
            }

            return lines;
        }

        public static double[] GetEdgeTime()
        {
            double[] data = new double[0];
            using (var reader = GetExcelReader())
            {
                bool firstRow = true;
                bool secondRow = true;
                int count = 0;
                while (reader.Read()) // Each row of the file
                {

                    if (firstRow)
                    {
                        firstRow = false;
                        continue; // Continue to the next iteration of the loop
                    }

                    if (secondRow)
                    {
                        secondRow = false;

                        continue; // Continue to the next iteration of the loop

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


        public static TrainData[] GetAllTrainData()
        {
            TrainData[] data = new TrainData[0];
            using (var reader = GetExcelReader())
            {
                bool firstRow = true;
                bool secondRow = true;
                int count = 0;
                while (reader.Read()) // Each row of the file
                {

                    if (firstRow)
                    {
                        firstRow = false;
                        continue; // Continue to the next iteration of the loop
                    }

                    if (secondRow)
                    {
                        secondRow = false;

                        continue; // Continue to the next iteration of the loop

                    }

                    Array.Resize(ref data, count + 1);
                    string line = GetExcelStringData(reader, 0);
                    string direction = GetExcelStringData(reader, 1);
                    string stationA = GetExcelStringData(reader, 2);
                    string stationB = GetExcelStringData(reader, 3);
                    double amPeakTime = GetExcelDoubleData(reader, 6);
                    double interPeakTime = GetExcelDoubleData(reader, 7);
                    double? umimpededTime = GetExcelDoubleData(reader, 5);

                    var newEntry = new TrainData(line, direction, stationA, stationB, amPeakTime, interPeakTime, umimpededTime);

                    data[count++] = newEntry;

                }
            }
            return data;
        }

        private static string GetExcelStringData(IExcelDataReader reader, int columnIndex)
        {
            var value = reader.GetString(columnIndex);
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }
            else
            {
                return "";
            }
        }

        private static double GetExcelDoubleData(IExcelDataReader reader, int columnIndex)
        {
            if (!reader.IsDBNull(5) && reader.GetDouble(columnIndex) != null)
            {
                return reader.GetDouble(columnIndex);
            }
            else
            {
                return 0;
            }
        }
    }

}


