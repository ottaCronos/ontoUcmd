using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ConstellationEditor.Models;
using LumenWorks.Framework.IO.Csv;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ontoUcmd.Models;

namespace ontoUcmd.Services
{
    public class SerializationService
    {

        public static List<NodeImage> ToNodeImages(string source)
        {
            List<NodeImage> nodeImages = new List<NodeImage>();
            if (string.IsNullOrEmpty(source))
                return null;
            else
            {
                if(source.Contains(";"))
                    foreach (var s in source.Split(';'))
                    {
                        nodeImages.Add(new NodeImage() { url = s });
                    }
                else
                    nodeImages.Add(new NodeImage() { url = source });
            }

            return nodeImages;
        }
        
        public static List<NodeVideo> ToNodeVideos(string source)
        {
            List<NodeVideo> nodeVideos = new List<NodeVideo>();
            if (string.IsNullOrEmpty(source))
                return null;
            else
            {
                if(source.Contains(";"))
                    foreach (var s in source.Split(';'))
                    {
                        nodeVideos.Add(new NodeVideo() { url = s });
                    }
                else
                    nodeVideos.Add(new NodeVideo() { url = source });
            }

            return nodeVideos;
        }
        
        public static async Task<GraphData> ConvertToGraphData(List<CsvConcept> concepts, List<CsvElement> elements, List<CsvCountry> countries,
            List<CsvPeople> peoples, List<CsvProject> projects, List<CsvPublication> publications, List<CsvRegion> regions)
        {

            List<NodeElement> nodeElements = new List<NodeElement>();
            foreach (var item in elements)
            {
                nodeElements.Add(CsvElement.ToNodeElement(item));
            }
            
            GraphData graphData = new GraphData()
            {
                Meta = new Meta() { generated = "Generated with ontoUcmd", language = "en"},
                NodeElements = nodeElements,
            }
        }

        public static void BuildGraphData(string buildPath, GraphData graphData)
        {
            JObject o = new JObject();
            JObject nodeElementArray = new JObject();
            
            foreach (var node in graphData.NodeElements)
            {
                nodeElementArray.Add(new JProperty(node.name, JObject.Parse(JsonConvert.SerializeObject(node))));
            } foreach (var node in graphData.NodeProjects) {
                // nodeElementArray.Add(new JProperty(node.name, JObject.Parse(JsonConvert.SerializeObject(node))));
            }foreach (var node in graphData.NodeCaseStudies) {
                // nodeElementArray.Add(new JProperty(node.name, JObject.Parse(JsonConvert.SerializeObject(node))));
            }foreach (var node in graphData.NodePublications) {
                // nodeElementArray.Add(new JProperty(node.name, JObject.Parse(JsonConvert.SerializeObject(node))));
            }foreach (var node in graphData.NodeCountries) {
                // nodeElementArray.Add(new JProperty(node.name, JObject.Parse(JsonConvert.SerializeObject(node))));
            }foreach (var node in graphData.NodeVts) {
                nodeElementArray.Add(new JProperty(node.name, JObject.Parse(JsonConvert.SerializeObject(node))));
            }
            
            o["meta"] = JObject.Parse(JsonConvert.SerializeObject(graphData.Meta));
            o["nodes"] = nodeElementArray;
            o["edges"] = JArray.Parse(JsonConvert.SerializeObject(graphData.EdgeItems));
            
            File.WriteAllText($@"C:\Users\vnoum\Desktop\Projets Rider\Unesco\Unesco\wwwroot\dive\data\graph_xx.json", o.ToString());
            Process.Start($@"""C:\Users\vnoum\AppData\Local\Programs\Microsoft VS Code\Code.exe"" ""C:\Users\vnoum\Desktop\Projets Rider\Unesco\Unesco\wwwroot\dive\data\graph_xx.json""");
        }
        
    }
}