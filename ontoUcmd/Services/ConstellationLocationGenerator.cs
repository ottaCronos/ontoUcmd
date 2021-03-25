using System;
using System.Collections.Generic;
using System.IO;

namespace ontoUcmd.Services
{

    public class ConstellationLocationGenerator
    {

        public string crlf = "\n";
        public string locationFile = "D:\\xampp\\htdocs\\ontoU\\dive\\data\\locations1.txt";
        public string locationJsonFile = "D:\\xampp\\htdocs\\ontoU\\dive\\data\\constellation-node-locations_.json";
        public string[] allType = {"Concept", "Element", "Country", "Region", "Project", "People", "Publication"};

        public class PlacedItem
        {

            public string name = "";
            public int dim = 0;
            public int x = 0;
            public int y = 0;

            public double dist(PlacedItem pi)
            {
                return Math.Sqrt((((x - pi.x)
                                   * (x - pi.x))
                                  + ((y - pi.y)
                                     * (y - pi.y))));
            }

            public string ToString(int com, int degre)
            {
                return "{\"id\":\"" + name + "\",\"community\":" + com + ",\"degree\":" + degre + ",\"x_fixed\":" + x +
                       ",\"y_fixed\":" + y + "}";
            }

        }

        private static List<PlacedItem> allConcept = new List<PlacedItem>();
        private static List<PlacedItem> allElement = new List<PlacedItem>();
        private static List<PlacedItem> allCountry = new List<PlacedItem>();
        private static List<PlacedItem> allRegion = new List<PlacedItem>();
        private static List<PlacedItem> allProject = new List<PlacedItem>();
        private static List<PlacedItem> allPeople = new List<PlacedItem>();
        private static List<PlacedItem> allPublication = new List<PlacedItem>();

        private List<PlacedItem>[] allTypeRead =
            {allConcept, allElement, allCountry, allRegion, allProject, allPeople, allPublication};

        public void FillAllItem()
        {
            try
            {
                StreamReader br = new StreamReader(locationFile);
                int iLine = 0;
                string line;
                while ((line = br.ReadLine()) != null)
                {
                    if (line.Contains(":") && line.Contains("."))
                    {
                        string concept = line.Split(':')[0].Split('.')[1];
                        string dimS = line.Split(':')[1].Replace(";", "");
                        int dim = 0;
                        Int32.TryParse(dimS, out dim);
                        PlacedItem c = new PlacedItem();
                        c.name = concept;
                        c.dim = 100;
                        AffectPlace(c);
                        allConcept.Add(c);
                    }
                    else if (line.Contains(".") && !line.Contains(":"))
                    {
                        string lu = line.Split('.')[0];
                        string name = line.Split('.')[1].Replace(";", "");
                        Console.WriteLine($"item  {lu} {name}");
                        for (int i = 1; i < allTypeRead.Length; i++)
                        {
                            if (lu.Equals(allType[i], StringComparison.CurrentCultureIgnoreCase))
                            {
                                PlacedItem pi = new PlacedItem();
                                pi.name = name;
                                AffectPlace(pi);
                                allTypeRead[i].Add(pi);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Error] {ex.ToString()}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public void AffectPlace(PlacedItem pi)
        {
            bool found = false;
            do
            {
                pi.x = new Random().Next(-900, 900);
                pi.y = new Random().Next(-900, 900);
                found = RoomOk(pi);
            } while (!found);

        }

        public bool RoomOk(PlacedItem pi)
        {
            bool ok = true;
            for (int i = 0; (i < allTypeRead.Length); i++)
            {
                for (int j = 0; (j < allTypeRead[i].Count); j++)
                {
                    PlacedItem pj = allTypeRead[i][j];
                    if ((pi.dist(pj) < 100))
                    {
                        return false;
                    }
                }
            }

            return ok;
        }

        public void WritePlace()
        {
            try
            {
                StreamWriter fo = new StreamWriter(locationJsonFile);
                fo.WriteLine("[" + crlf);

                for (int i = 0; i < allTypeRead.Length; i++)
                {
                    for (int j = 0; j < allTypeRead[i].Count; j++)
                    {
                        PlacedItem pj = allTypeRead[i][j];
                        if (i == allTypeRead.Length - 1 && j == allTypeRead[i].Count - 1)
                            fo.WriteLine(pj.ToString(i + 1, 150) + crlf);
                        else
                            fo.WriteLine(pj.ToString(i + 1, 150) + "," + crlf);
                    }
                }

                fo.WriteLine("]" + crlf);
                fo.Close();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Error] {ex.ToString()}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

        }

        public void ShowRead()
        {
            Console.WriteLine(("Concept " + allConcept.Count));
            Console.WriteLine(("Element " + allElement.Count));
            Console.WriteLine(("Country " + allCountry.Count));
            Console.WriteLine(("Region  " + allRegion.Count));
            Console.WriteLine(("Project " + allProject.Count));
            Console.WriteLine(("People  " + allPeople.Count));
            Console.WriteLine(("Pub     " + allPublication.Count));
        }

        public static void Build(string locationPath, string locationJsonPath)
        {
            ConstellationLocationGenerator clg = new ConstellationLocationGenerator();
            clg.locationFile = locationPath;
            clg.locationJsonFile = locationJsonPath;
            clg.FillAllItem();
            clg.ShowRead();
            clg.WritePlace();
        }

    }
}