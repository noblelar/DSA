using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ExcelDataReader;
using static System.Collections.Specialized.BitVector32;

namespace ConsoleApp1
{
    public static class GetData
    {
        public static void GetSectionData()
        {
            Console.WriteLine("This is the begining ");
            // Register encoding provider required by ExcelDataReader
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            // Path to the Excel file
            string filePath = @"C:\Users\larte\source\repos\ConsoleApp1\istt.xlsx";

            // List to hold the sections
            // List<Section> sections = new List<Section>();

            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        // Assuming the first row is the header and data starts from the second row
                        bool firstRow = true;

                        while (reader.Read()) // Each row of the file
                        {
                            if (firstRow)
                            {
                                firstRow = false; // Skip the header row
                                continue;
                            }

                            if(reader.GetString(2) != null)
                            Console.WriteLine($"From {reader.GetDouble(4)} km");

                        }
                    }
                }

                // Optional: Print out the sections to verify
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

    }
}


