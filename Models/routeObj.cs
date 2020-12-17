using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace Models {
    public class RouteObj {
        public List<PointClass> _points { get; private set; }

        public RouteObj(string pointName, string coordinates)
        {
            _points = new List<PointClass>();
            Regex regex = new Regex(@"(\d+\.\d+)\,(\d+\.\d+)\,(\d+)");
            MatchCollection myMatches = regex.Matches(coordinates);

            foreach(Match myMatch in myMatches) {
                _points.Add(new PointClass{
                    pointName = pointName,
                    longitude = float.Parse(myMatch.Groups[1].Value),
                    latitude = float.Parse(myMatch.Groups[2].Value),
                    elevation = float.Parse(myMatch.Groups[3].Value),
                    timestamp = 0
                });
            }
        }

        public void SetTimes(DateTime tgt) {
            TimeSpan t = tgt - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            TimeSpan offset = new TimeSpan(0, 0, 30);
            _points.Reverse();
            _points.ForEach(p => {
                p.timestamp = secondsSinceEpoch;
                secondsSinceEpoch -= 30;
            });
            _points.Reverse();
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(_points);
        }

        public string ToKml() {
            StringBuilder sb = new StringBuilder();
            _points.ForEach(o => sb.Append(o.ToKml()));

            return sb.ToString();
        }

        public string ToCSV() {
            StringBuilder sb = new StringBuilder();
            _points.ForEach(o => sb.AppendLine(o.ToCSV()));

            return sb.ToString();
        }
    }
}