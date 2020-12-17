using System;
using System.Text;

namespace Models {
    public class PointClass {
        private DateTime epoch = new DateTime(1970, 1, 1);

        public string pointName { get; set; }

        public float latitude { get; set; }
        
        public float longitude { get; set; }

        public float elevation { get; set; }

        public int timestamp { get; set; }

        public string ToKml() {
            var xx = epoch.AddSeconds(timestamp);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<Placemark>");
            sb.AppendLine($"    <name>{timestamp}</name>");
            sb.AppendLine($"    <timestamp>");
            sb.AppendLine($"        <when>{xx.ToString("o")}</when>");
            sb.AppendLine($"    </timestamp>");
            sb.AppendLine("    <Point>");
            sb.AppendLine($"        <coordinates>{longitude},{latitude},{elevation}</coordinates>");
            sb.AppendLine("    </Point>");
            sb.AppendLine("</Placemark>");

            return sb.ToString();
        }

        public string ToCSV() {
            var xx = epoch.AddSeconds(timestamp);
            
            // return $"\"{xx.ToString("o")}\",\"{longitude}\",\"{latitude}\"";
            return $"\"{pointName}\",\"{timestamp}\",\"{longitude}\",\"{latitude}\"";
        }
    }
}