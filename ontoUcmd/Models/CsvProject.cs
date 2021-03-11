using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ConstellationEditor.Models;
using LumenWorks.Framework.IO.Csv;
using ontoUcmd.Services;

namespace ontoUcmd.Models
{
    public class CsvProject
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string LinkedElements { get; set; }

        public static List<CsvProject> FromFile(string path)
        {
            
            List<CsvProject> csvProjects = new List<CsvProject>();
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(path)), true))  
            {
                csvTable.Load(csvReader);  
            } 
            for (int i = 0; i < csvTable.Rows.Count; i++)  
            {  
                csvProjects.Add(new CsvProject { Name = csvTable.Rows[i][0].ToString(), 
                    Label= csvTable.Rows[i][1].ToString(), Link = csvTable.Rows[i][2].ToString(),
                    Description= csvTable.Rows[i][3].ToString(), LinkedElements = csvTable.Rows[i][4].ToString()
                });  
            }

            return csvProjects;
        }
        
        public static NodeProject ToNodeElement(CsvProject item)
        {
            NodeProject nodeItem = new NodeProject()
            {
                name = item.Name, label = item.Label, type = "element", meta = new NodeProjectMeta()
                {
                    description = item.Description, link = item.Link
                }
            };
            return nodeItem;
        }
        
    }
}