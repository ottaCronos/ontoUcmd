using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ConstellationEditor.Models;
using LumenWorks.Framework.IO.Csv;

namespace ontoUcmd.Models
{
    public class CsvRegion
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string LinkedElements { get; set; }

        public static List<CsvRegion> FromFile(string path)
        {
            
            List<CsvRegion> csvRegions = new List<CsvRegion>();
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(path)), true, '|'))  
            {
                csvTable.Load(csvReader);  
            } 
            for (int i = 0; i < csvTable.Rows.Count; i++)  
            {  
                csvRegions.Add(new CsvRegion { Name = csvTable.Rows[i][0].ToString(), 
                    Label= csvTable.Rows[i][1].ToString(), LinkedElements = csvTable.Rows[i][2].ToString()
                });  
            }

            return csvRegions;
        }
        
        public static NodeCountry ToNodeElement(CsvRegion item)
        {
            NodeCountry nodeItem = new NodeCountry()
            {
                name = item.Name, label = item.Label, type = "region"
            };
            return nodeItem;
        }
        
    }
}