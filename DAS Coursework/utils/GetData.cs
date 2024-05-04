using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ExcelDataReader;
using DAS_Coursework.models;

namespace DAS_Coursework.utils
{
    public static class GetData
    {
        public static void GetSectionData()
        {
            Console.WriteLine("This is the begining ");
            // Register encoding provider required by ExcelDataReader
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            // Path to the Excel file
            string filePath = @"C:\Users\larte\Desktop\Inter Station Train Times.xlsx";

            // List to hold the sections
            List<Section> sections = new List<Section>();

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

                            // Create a Section object from the row data
                            var section = new Section(
                                reader.GetString(2), // Starting Station
                                reader.GetString(3), // Ending Station
                                (float)reader.GetDouble(4), // Distance
                                (float)reader.GetDouble(5), // Un-impeded Running Time
                                (float)reader.GetDouble(6), // AM peak Running Time
                                (float)reader.GetDouble(7)  // Inter peak Running Time
                            );

                            sections.Add(section);
                        }
                    }
                }

                // Optional: Print out the sections to verify
                foreach (var section in sections)
                {
                   Console.WriteLine($"From {section.GetDelay()} km");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

    }
}


