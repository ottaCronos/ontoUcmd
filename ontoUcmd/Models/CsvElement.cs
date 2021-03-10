using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ConstellationEditor.Models;
using ConstellationEditor.Services;
using LumenWorks.Framework.IO.Csv;
using ontoUcmd.Services;

namespace ontoUcmd.Models
{
    public class CsvElement
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string List { get; set; }
        public int Year { get; set; }
        public string Link { get; set; }
        public string Images { get; set; }
        public string Videos { get; set; }
        public string LinkedElements { get; set; }

        public static List<CsvElement> FromFile(string path)
        {
            
            List<CsvElement> csvElements = new List<CsvElement>();
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(path)), true))  
            {
                csvTable.Load(csvReader);  
            } 
            for (int i = 0; i < csvTable.Rows.Count; i++)  
            {  
                csvElements.Add(new CsvElement { Name = csvTable.Rows[i][0].ToString(), 
                    Label= csvTable.Rows[i][1].ToString(), Icon = csvTable.Rows[i][2].ToString(),
                    Description= csvTable.Rows[i][3].ToString(), List = csvTable.Rows[i][4].ToString(),
                    Year= Int32.Parse(csvTable.Rows[i][5].ToString()), Link = csvTable.Rows[i][6].ToString(),
                    Images= csvTable.Rows[i][7].ToString(), Videos = csvTable.Rows[i][8].ToString(), LinkedElements = csvTable.Rows[i][9].ToString()
                });  
            }

            return csvElements;
        }

        public static NodeElement ToNodeElement(CsvElement item)
        {
            NodeElement nodeElement = new NodeElement()
            {
                name = item.Name, label = item.Label, type = "element", meta = new NodeMeta()
                {
                    description = item.Description, icon = new NodeIcon() {large = item.Icon, small = item.Icon},
                    images = SerializationService.ToNodeImages(item.Images),
                    link = item.Link, list = item.List, multinational = true, sustainability = "", year = item.Year,
                    videos = SerializationService.ToNodeVideos(item.Videos)
                }
            };
            return nodeElement;
        }
        
    }
}