using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ConstellationEditor.Models;
using LumenWorks.Framework.IO.Csv;

namespace ontoUcmd.Models
{
    public class CsvConcept
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string LinkedElements { get; set; }

        public static List<CsvConcept> FromFile(string path)
        {
            
            List<CsvConcept> csvConcepts = new List<CsvConcept>();
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(path)), true, '|'))  
            {
                csvTable.Load(csvReader);  
            } 
            for (int i = 0; i < csvTable.Rows.Count; i++)  
            {  
                csvConcepts.Add(new CsvConcept { Name = csvTable.Rows[i][0].ToString(), 
                    Label= csvTable.Rows[i][1].ToString(), LinkedElements = csvTable.Rows[i][2].ToString()
                });  
            }

            return csvConcepts;
        }
        
        public static NodeVt ToNodeElement(CsvConcept item)
        {
            NodeVt nodeItem = new NodeVt()
            {
                name = item.Name, label = item.Label, type = "concept", group = "unesco"
            };
            return nodeItem;
        }
        
    }
}