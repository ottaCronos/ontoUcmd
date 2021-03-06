using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using ontoUcmd.Models;
using ontoUcmd.Services;

namespace ontoUcmd
{
    class Program
    {
        
        static string iniPath = string.Format("{0}\\config.ini", Environment.CurrentDirectory);

        static void Main(string[] args)
        {
            Console.WriteLine("OntoU CMD");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.WriteLine("Commands: set-files, get-files, set-path, build");
            Console.WriteLine();
            while (true)
            {
                Console.Write("Enter a command: ");
                ResolveCmd(Console.ReadLine());
            }
        }

        static void ResolveCmd(string cmd)
        {
            switch (cmd)
            {
                case "set-files":
                    SetFiles();
                    break;
                case "get-files":
                    GetFiles();
                    break;
                case "set-path":
                    SetPath();
                    break;
                case "build":
                    Build();
                    break;
                default:
                    Console.WriteLine("Invalid commad. Please retry.");
                    break;
            }
        }

        static async void Build()
        {

            bool locationBuild = false;
            
            Console.WriteLine();
            Console.WriteLine("Build JSON from CSV.");
            
            IniFile ini = new IniFile();
            if (!File.Exists(iniPath))
                File.WriteAllText(iniPath, "");
            ini.Load(iniPath);

            Console.WriteLine("Infos :");
            string path = ini.GetKeyValue("Config", "OutputPath");
            string lpath = ini.GetKeyValue("Config", "OutputLocationPath");
            Console.WriteLine($"     Output path : {path}");
            Console.WriteLine($"     Location output path : {lpath}");
            if(File.Exists(path))
                Console.WriteLine($"     The file {path} already exists and will be updated.");
            if(File.Exists(lpath))
                Console.WriteLine($"     The file {lpath} already exists and will be updated.");
            Console.WriteLine();

            Console.WriteLine("Converting Excel files to Csv...");

            
            List<CsvElement> csvElements = new List<CsvElement>();
            List<CsvConcept> csvConcepts = new List<CsvConcept>();
            List<CsvCountry> csvCountries = new List<CsvCountry>();
            List<CsvRegion> csvRegions = new List<CsvRegion>();
            List<CsvPeople> csvPeoples = new List<CsvPeople>();
            List<CsvProject> csvProjects = new List<CsvProject>();
            List<CsvPublication> csvPublications = new List<CsvPublication>();
            
            Console.WriteLine("Reading data from csv documents...");
            string str = "";
            string name = "";
            Dictionary<string, string> dic = new Dictionary<string, string>();

            // Converts all excel files found in config.ini to csv files, in a temporary dir.
            name = "Elements";
            str = ini.GetKeyValue("ExcelFiles", name);
            if (!File.Exists(str))
                Console.WriteLine($"error: {name} csv file could not be found.");
            else
            {
                ExcelService.SaveAsCsv(str,
                    Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + $@"\{name}.csv");
                dic.Add(name, Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + $@"\{name}.csv");
            }
            name = "Concepts";
            str = ini.GetKeyValue("ExcelFiles", name);
            if (!File.Exists(str))
                Console.WriteLine($"error: {name} csv file could not be found.");
            else
            {
                ExcelService.SaveAsCsv(str,
                    Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + $@"\{name}.csv");
                dic.Add(name, Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + $@"\{name}.csv");
            }
            name = "Countries";
            str = ini.GetKeyValue("ExcelFiles", name);
            if (!File.Exists(str))
                Console.WriteLine($"error: {name} csv file could not be found.");
            else
            {
                ExcelService.SaveAsCsv(str,
                    Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + $@"\{name}.csv");
                dic.Add(name, Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + $@"\{name}.csv");
            }
            name = "Regions";
            str = ini.GetKeyValue("ExcelFiles", name);
            if (!File.Exists(str))
                Console.WriteLine($"error: {name} csv file could not be found.");
            else
            {
                ExcelService.SaveAsCsv(str,
                    Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + $@"\{name}.csv");
                dic.Add(name, Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + $@"\{name}.csv");
            }
            name = "Projects";
            str = ini.GetKeyValue("ExcelFiles", name);
            if (!File.Exists(str))
                Console.WriteLine($"error: {name} csv file could not be found.");
            else
            {
                ExcelService.SaveAsCsv(str,
                    Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + $@"\{name}.csv");
                dic.Add(name, Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + $@"\{name}.csv");
            }
            name = "Publications";
            str = ini.GetKeyValue("ExcelFiles", name);
            if (!File.Exists(str))
                Console.WriteLine($"error: {name} csv file could not be found.");
            else
            {
                ExcelService.SaveAsCsv(str,
                    Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + $@"\{name}.csv");
                dic.Add(name, Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + $@"\{name}.csv");
            }
            name = "People";
            str = ini.GetKeyValue("ExcelFiles", name);
            if (!File.Exists(str))
                Console.WriteLine($"error: {name} csv file could not be found.");
            else
            {
                ExcelService.SaveAsCsv(str,
                    Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + $@"\{name}.csv");
                dic.Add(name, Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + $@"\{name}.csv");
            }
            
            str = dic["Elements"];
            if (!File.Exists(str))
                Console.WriteLine("error: Elements csv file could not be found.");
            else
                csvElements = CsvElement.FromFile(str);
            
            str = dic["Concepts"];
            if (!File.Exists(str))
                Console.WriteLine("error: Concepts csv file could not be found.");
            else
                csvConcepts = CsvConcept.FromFile(str);
            
            str = dic["Countries"];
            if (!File.Exists(str))
                Console.WriteLine("error: Countries csv file could not be found.");
            else
                csvCountries = CsvCountry.FromFile(str);
            
            str = dic["Regions"];
            if (!File.Exists(str))
                Console.WriteLine("error: Regions csv file could not be found.");
            else
                csvRegions = CsvRegion.FromFile(str);
            
            str = dic["People"];
            if (!File.Exists(str))
                Console.WriteLine("error: People csv file could not be found.");
            else
                csvPeoples = CsvPeople.FromFile(str);
            
            str = dic["Projects"];
            if (!File.Exists(str))
                Console.WriteLine("error: Projects csv file could not be found.");
            else
                csvProjects = CsvProject.FromFile(str);
            
            str = dic["Publications"];
            if (!File.Exists(str))
                Console.WriteLine("error: Publications csv file could not be found.");
            else
                csvPublications = CsvPublication.FromFile(str);
            
            Console.WriteLine("CSV files has been read. Building graph_xx.json...");
            Console.WriteLine("Converting...");
            var graphData = await SerializationService.ConvertToGraphData(csvConcepts, csvElements, csvCountries, csvPeoples,
                csvProjects, csvPublications, csvRegions);
            Console.WriteLine("Building...");
            SerializationService.BuildGraphData(path, graphData);
            Console.WriteLine("Graph file has been build.");
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[Info] Do you want to build locations files (txt/json) ? Y/N : ");
            string resp = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            if (!string.IsNullOrEmpty(resp) && resp.ToLower() == "y")
            {
                locationBuild = true;
                Console.WriteLine("Building location text file...");
                SerializationService.BuildLocationFile(lpath, graphData, csvConcepts);
                Console.WriteLine("Location text file has been build.");
                Console.WriteLine("Building location json file...");
                ConstellationLocationGenerator.Build(lpath, $"{Path.GetDirectoryName(lpath)}\\constellation-node-locations_.json");
                Console.WriteLine("Location json file has been build.");
                Console.WriteLine($"     Location txt output path : {lpath}");
                Console.WriteLine($"     Location txt output path : {Path.GetDirectoryName(lpath)}\\constellation-node-locations_.json");
            }
            Console.WriteLine($"     Output path : {path}");
            
            
            
            Console.WriteLine("");
            
        }

        static void SetPath()
        {
            Console.WriteLine();
            Console.WriteLine("Set output json path. Data will be saved in config.ini.");
            
            IniFile ini = new IniFile();
            if (!File.Exists(iniPath))
                File.WriteAllText(iniPath, "");
            ini.Load(iniPath);
            
            Console.Write("     Enter output json file location: ");
            string path = Console.ReadLine();
            if(File.Exists(path))
                Console.WriteLine("     [Info] A file already exists at this location. Data inside will be ereased if you build.");
            ini.SetKeyValue("Config", "OutputPath", path);
            
            Console.Write("     Enter location text output location: ");
            string lpath = Console.ReadLine();
            if(File.Exists(path))
                Console.WriteLine("     [Info] A file already exists at this location. Data inside will be ereased if you build.");
            ini.SetKeyValue("Config", "OutputLocationPath", lpath);
            
            ini.Save(iniPath);
            Console.WriteLine("Output path as been updated.");
        }
        
        static void GetFiles()
        {
            Console.WriteLine();
            Console.WriteLine("Get files path. Data will be read from config.ini.");
            
            IniFile ini = new IniFile();
            if (!File.Exists(iniPath))
                File.WriteAllText(iniPath, "");
            ini.Load(iniPath);

            string str = "";
            string name = "";

            name = "Elements"; str = ini.GetKeyValue("ExcelFiles", name);
            if(string.IsNullOrEmpty(str))
                Console.WriteLine($"     {name}: this field is empty, fix this by doing 'set-files'.");
            else
                Console.WriteLine($"     {name}: {str}");
            
            name = "Concepts"; str = ini.GetKeyValue("ExcelFiles", name);
            if(string.IsNullOrEmpty(str))
                Console.WriteLine($"     {name}: this field is empty, fix this by doing 'set-files'.");
            else
                Console.WriteLine($"     {name}: {str}");
            
            name = "People"; str = ini.GetKeyValue("ExcelFiles", name);
            if(string.IsNullOrEmpty(str))
                Console.WriteLine($"     {name}: this field is empty, fix this by doing 'set-files'.");
            else
                Console.WriteLine($"     {name}: {str}");
            
            name = "Publications"; str = ini.GetKeyValue("ExcelFiles", name);
            if(string.IsNullOrEmpty(str))
                Console.WriteLine($"     {name}: this field is empty, fix this by doing 'set-files'.");
            else
                Console.WriteLine($"     {name}: {str}");
            
            name = "Countries"; str = ini.GetKeyValue("ExcelFiles", name);
            if(string.IsNullOrEmpty(str))
                Console.WriteLine($"     {name}: this field is empty, fix this by doing 'set-files'.");
            else
                Console.WriteLine($"     {name}: {str}");
            
            name = "Regions"; str = ini.GetKeyValue("ExcelFiles", name);
            if(string.IsNullOrEmpty(str))
                Console.WriteLine($"     {name}: this field is empty, fix this by doing 'set-files'.");
            else
                Console.WriteLine($"     {name}: {str}");
            
            name = "Projects"; str = ini.GetKeyValue("ExcelFiles", name);
            if(string.IsNullOrEmpty(str))
                Console.WriteLine($"     {name}: this field is empty, fix this by doing 'set-files'.");
            else
                Console.WriteLine($"     {name}: {str}");
            
        }
        
        static void SetFiles()
        {
            Console.WriteLine();
            Console.WriteLine("Set files path. Data will be saved in config.ini.");
            
            IniFile ini = new IniFile();
            if (!File.Exists(iniPath))
                File.WriteAllText(iniPath, "");
            ini.Load(iniPath);
            
            Console.Write("     Elements csv file: ");
            string elements = Console.ReadLine();
            if(!File.Exists(elements))
                Console.WriteLine($"[Alert] The file '{elements}' does not exists.");
            ini.SetKeyValue("ExcelFiles", "Elements", elements);
            
            Console.Write("     Concepts csv file: ");
            string concepts = Console.ReadLine();
            if(!File.Exists(concepts))
                Console.WriteLine($"[Alert] The file '{concepts}' does not exists.");
            ini.SetKeyValue("ExcelFiles", "Concepts", concepts);
            
            Console.Write("     People csv file: ");
            string peoples = Console.ReadLine();
            if(!File.Exists(peoples))
                Console.WriteLine($"[Alert] The file '{peoples}' does not exists.");
            ini.SetKeyValue("ExcelFiles", "People", peoples);
            
            Console.Write("     Projects csv file: ");
            string projects = Console.ReadLine();
            if(!File.Exists(projects))
                Console.WriteLine($"[Alert] The file '{projects}' does not exists.");
            ini.SetKeyValue("ExcelFiles", "Projects", projects);
            
            Console.Write("     Publications csv file: ");
            string publications = Console.ReadLine();
            if(!File.Exists(publications))
                Console.WriteLine($"[Alert] The file '{publications}' does not exists.");
            ini.SetKeyValue("ExcelFiles", "Publications", publications);
            
            Console.Write("     Countries csv file: ");
            string countries = Console.ReadLine();
            if(!File.Exists(countries))
                Console.WriteLine($"[Alert] The file '{countries}' does not exists.");
            ini.SetKeyValue("ExcelFiles", "Countries", countries);
            
            Console.Write("     Regions csv file: ");
            string regions = Console.ReadLine();
            if(!File.Exists(regions))
                Console.WriteLine($"[Alert] The file '{regions}' does not exists.");
            ini.SetKeyValue("ExcelFiles", "Regions", regions);
            
            Console.WriteLine();
            Console.Write("Configuration completed. Do you want to save theses files to the config.ini (this will erease the current file data) ? Y/N : ");
            string resp = Console.ReadLine();
            if (resp.ToLower().Contains("y"))
            {
                Console.WriteLine("Writing data to config.ini...");
                ini.Save(iniPath);
                Console.WriteLine("Done.");
            }
        }
        
    }
}