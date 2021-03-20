using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            List<NodeCountry> nodeCountries = new List<NodeCountry>();
            List<NodeProject> nodeProjects = new List<NodeProject>();
            List<NodeVt> nodeConcepts = new List<NodeVt>();
            List<NodePublication> nodePublications = new List<NodePublication>();
            List<NodeCaseStudy> nodePeople = new List<NodeCaseStudy>();
            foreach (var item in elements) { nodeElements.Add(CsvElement.ToNodeElement(item)); }
            foreach (var item in countries) { nodeCountries.Add(CsvCountry.ToNodeElement(item)); }
            foreach (var item in regions) { nodeCountries.Add(CsvRegion.ToNodeElement(item)); }
            foreach (var item in projects) { nodeProjects.Add(CsvProject.ToNodeElement(item)); }
            foreach (var item in concepts) { nodeConcepts.Add(CsvConcept.ToNodeElement(item)); }
            foreach (var item in publications) { nodePublications.Add(CsvPublication.ToNodeElement(item)); }
            foreach (var item in peoples) { nodePeople.Add(CsvPeople.ToNodeElement(item)); }

            List<EdgeItem> edgeItems = new List<EdgeItem>();
            foreach (var concept in concepts) {
                if (concept.LinkedElements.Contains(",")) {
                    string[] items = concept.LinkedElements.Split(',');
                    foreach (var s in items) { edgeItems.Add(new EdgeItem() { Object = concept.Name, predicate = "related", subject = s, weight = 2}); }
                } else {
                    if (!string.IsNullOrEmpty(concept.LinkedElements))
                    {
                        edgeItems.Add(new EdgeItem() { Object = concept.Name, predicate = "related", subject = concept.LinkedElements, weight = 2});
                    }
                }
            }
            foreach (var concept in elements) {
                if (concept.LinkedElements.Contains(",")) {
                    string[] items = concept.LinkedElements.Split(',');
                    foreach (var s in items) { edgeItems.Add(new EdgeItem() { Object = concept.Name, predicate = "related", subject = s, weight = 2}); }
                } else {
                    if (!string.IsNullOrEmpty(concept.LinkedElements))
                    {
                        edgeItems.Add(new EdgeItem() { Object = concept.Name, predicate = "related", subject = concept.LinkedElements, weight = 2});
                    }
                }
            }
            foreach (var concept in countries) {
                if (concept.LinkedElements.Contains(",")) {
                    string[] items = concept.LinkedElements.Split(',');
                    foreach (var s in items) { edgeItems.Add(new EdgeItem() { Object = concept.Name, predicate = "related", subject = s, weight = 2}); }
                } else {
                    if (!string.IsNullOrEmpty(concept.LinkedElements))
                    {
                        edgeItems.Add(new EdgeItem() { Object = concept.Name, predicate = "related", subject = concept.LinkedElements, weight = 2});
                    }
                }
            }
            foreach (var concept in regions) {
                if (concept.LinkedElements.Contains(",")) {
                    string[] items = concept.LinkedElements.Split(',');
                    foreach (var s in items) { edgeItems.Add(new EdgeItem() { Object = concept.Name, predicate = "related", subject = s, weight = 2}); }
                } else {
                    if (!string.IsNullOrEmpty(concept.LinkedElements))
                    {
                        edgeItems.Add(new EdgeItem() { Object = concept.Name, predicate = "related", subject = concept.LinkedElements, weight = 2});
                    }
                }
            }
            foreach (var concept in peoples) {
                if (concept.LinkedElements.Contains(",")) {
                    string[] items = concept.LinkedElements.Split(',');
                    foreach (var s in items) { edgeItems.Add(new EdgeItem() { Object = concept.Name, predicate = "related", subject = s, weight = 2}); }
                } else {
                    if (!string.IsNullOrEmpty(concept.LinkedElements))
                    {
                        edgeItems.Add(new EdgeItem() { Object = concept.Name, predicate = "related", subject = concept.LinkedElements, weight = 2});
                    }
                }
            }
            foreach (var concept in projects) {
                if (concept.LinkedElements.Contains(",")) {
                    string[] items = concept.LinkedElements.Split(',');
                    foreach (var s in items) { edgeItems.Add(new EdgeItem() { Object = concept.Name, predicate = "related", subject = s, weight = 2}); }
                } else {
                    if (!string.IsNullOrEmpty(concept.LinkedElements))
                    {
                        edgeItems.Add(new EdgeItem() { Object = concept.Name, predicate = "related", subject = concept.LinkedElements, weight = 2});
                    }
                }
            }
            foreach (var concept in publications) {
                if (concept.LinkedElements.Contains(",")) {
                    string[] items = concept.LinkedElements.Split(',');
                    foreach (var s in items) { edgeItems.Add(new EdgeItem() { Object = concept.Name, predicate = "related", subject = s, weight = 2}); }
                } else {
                    if (!string.IsNullOrEmpty(concept.LinkedElements))
                    {
                        edgeItems.Add(new EdgeItem() { Object = concept.Name, predicate = "related", subject = concept.LinkedElements, weight = 2});
                    }
                }
            }

            GraphData graphData = new GraphData()
            {
                Meta = new Meta() {generated = "Generated with ontoUcmd", language = "en"},
                NodeElements = nodeElements, NodeCountries = nodeCountries, NodeProjects = nodeProjects, NodePublications = nodePublications,
                NodeVts = nodeConcepts, NodeCaseStudies = nodePeople, EdgeItems = edgeItems
            };

            return graphData;
        }

        public static void BuildGraphData(string buildPath, GraphData graphData)
        {
            JObject o = new JObject();
            JObject nodeElementArray = new JObject();
            
            foreach (var node in graphData.NodeElements)
            {
                nodeElementArray.Add(new JProperty(node.name, JObject.Parse(JsonConvert.SerializeObject(node))));
            } foreach (var node in graphData.NodeProjects) {
                nodeElementArray.Add(new JProperty(node.name, JObject.Parse(JsonConvert.SerializeObject(node))));
            }foreach (var node in graphData.NodeCaseStudies) {
                nodeElementArray.Add(new JProperty(node.name, JObject.Parse(JsonConvert.SerializeObject(node))));
            }foreach (var node in graphData.NodePublications) {
                nodeElementArray.Add(new JProperty(node.name, JObject.Parse(JsonConvert.SerializeObject(node))));
            }foreach (var node in graphData.NodeCountries) {
                nodeElementArray.Add(new JProperty(node.name, JObject.Parse(JsonConvert.SerializeObject(node))));
            }foreach (var node in graphData.NodeVts) {
                nodeElementArray.Add(new JProperty(node.name, JObject.Parse(JsonConvert.SerializeObject(node))));
            }
            
            o["meta"] = JObject.Parse(JsonConvert.SerializeObject(graphData.Meta));
            o["nodes"] = nodeElementArray;
            o["edges"] = JArray.Parse(JsonConvert.SerializeObject(graphData.EdgeItems));
            
            File.WriteAllText(buildPath, o.ToString());
        }

        public static void BuildLocationFile(string buildPath, GraphData graphData, List<CsvConcept> concepts)
        {
            string outputText = "";
            foreach (var node in graphData.NodeElements)
            {
                outputText += $"element.{node.name};\n";
            } foreach (var node in graphData.NodeProjects) {
                outputText += $"project.{node.name};\n";
            }foreach (var node in graphData.NodeCaseStudies) {
                outputText += $"people.{node.name};\n";
            }foreach (var node in graphData.NodePublications) {
                outputText += $"publication.{node.name};\n";
            }foreach (var node in graphData.NodeCountries) {
                if (node.name.ToLower().Contains("country"))
                {
                    outputText += $"coutry.{node.name};\n";
                }
                else
                {
                    outputText += $"region.{node.name};\n";
                }
                
            }foreach (var concept in concepts)
            {
                List<string> items = new List<string>();
                if (concept.LinkedElements.Contains(","))
                    items.AddRange(concept.LinkedElements.Split(',').ToList());
                else
                    if (!string.IsNullOrEmpty(concept.LinkedElements))
                        items.Add(concept.LinkedElements);
                int linkedCount = 0;
                if (items.Count > 0)
                    linkedCount = items.Count;
                outputText += $"concept.{concept.Name}:{linkedCount};\n";
            }
            
            File.WriteAllText(buildPath, outputText);
        }
        
    }
}