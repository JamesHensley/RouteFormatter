using System;
using System.Collections.Generic;
using System.IO;
using Models;
using System.Text;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Linq;
using System.Text.Json;

namespace Services {
    public class Reader {
        private List<RouteObj> routes;

        public Reader(string kmlFile)
        {
            this.routes = new List<RouteObj>();

            using (var fileStream = File.Open(kmlFile, FileMode.Open))
            {
                XPathDocument xPath = new XPathDocument(fileStream);
                var navigator = xPath.CreateNavigator();

                int routeNum = 1;
                var coordinates = navigator.SelectDescendants("coordinates", "http://www.opengis.net/kml/2.2", false);
                while(coordinates.MoveNext()) {
                    string coordStr = coordinates.Current.Value;
                    this.routes.Add(new RouteObj($"Route{routeNum}", coordinates.Current.Value));
                    routeNum++;
                }
            }
        }
    
        public void UpdateTimeStamps(DateTime target) {
            foreach(RouteObj ro in routes) {
                ro.SetTimes(target);
            }
        }

        public string GetKmlDoc() {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<kml xmlns=\"http://www.opengis.net/kml/2.2\" xmlns:gx=\"http://www.google.com/kml/ext/2.2\" xmlns:kml=\"http://www.opengis.net/kml/2.2\" xmlns:atom=\"http://www.w3.org/2005/Atom\">");
            sb.AppendLine("<Document>");

            routes.ForEach(o => sb.Append(o.ToKml()));

            sb.AppendLine("</Document>");
            sb.AppendLine("</kml>");

            return sb.ToString();
        }

        public string GetCsvDoc() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\"pointName\",\"timestamp\",\"longitude\",\"latitude\"");
            routes.ForEach(o => sb.Append(o.ToCSV()));
            return sb.ToString();
        }
    }
}
