using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ConstellationEditor.Models;
using LumenWorks.Framework.IO.Csv;

namespace ontoUcmd.Models
{
    public class CsvPublication
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Keywords { get; set; }
        public string LinkedElements { get; set; }

        public static List<CsvPublication> FromFile(string path)
        {
            
            List<CsvPublication> csvPublications = new List<CsvPublication>();
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(path)), true))  
            {
                csvTable.Load(csvReader);  
            } 
            for (int i = 0; i < csvTable.Rows.Count; i++)  
            {  
                csvPublications.Add(new CsvPublication { Name = csvTable.Rows[i][0].ToString(), 
                    Label= csvTable.Rows[i][1].ToString(), Language = csvTable.Rows[i][2].ToString(),
                    Description= csvTable.Rows[i][3].ToString(), Link = csvTable.Rows[i][4].ToString(),
                    Keywords= csvTable.Rows[i][5].ToString(), LinkedElements = csvTable.Rows[i][6].ToString()
                });  
            }

            return csvPublications;
        }
        
        public static NodePublication ToNodeElement(CsvPublication item)
        {
            NodePublication nodeItem = new NodePublication()
            {
                name = item.Name, label = item.Label, type = "concept", meta = new NodePublicationMeta()
                {
                    description = item.Description, keywords = item.Keywords.Split(','), language = item.Language, link = item.Link, paper = ""
                }
            };
            return nodeItem;
        }
        
    }
}