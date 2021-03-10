﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace ontoUcmd.Models
{
    public class CsvPeople
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string LinkedElements { get; set; }

        public static List<CsvPeople> FromFile(string path)
        {
            
            List<CsvPeople> csvPeoples = new List<CsvPeople>();
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(path)), true))  
            {
                csvTable.Load(csvReader);  
            } 
            for (int i = 0; i < csvTable.Rows.Count; i++)  
            {  
                csvPeoples.Add(new CsvPeople { Name = csvTable.Rows[i][0].ToString(), 
                    Label= csvTable.Rows[i][1].ToString(), Icon = csvTable.Rows[i][2].ToString(),
                    Description= csvTable.Rows[i][3].ToString(), Link = csvTable.Rows[i][4].ToString(),
                    LinkedElements = csvTable.Rows[i][5].ToString()
                });  
            }

            return csvPeoples;
        }
        
    }
}