using System;
using System.Collections.Generic;
using System.Xml;

namespace SvgChart
{
    public class SvgLineChart : ILineChart
    {
        private XmlDocument _xml;
        private XmlElement svg;
        private List<Tuple<float, float>> _points;

        public List<Tuple<float, float>> Points { get => _points; set => _points = value; }

        public SvgLineChart()
        {
            _xml = new XmlDocument();
            svg = _xml.CreateElement("svg");
            svg.SetAttribute("version", "1.2");
            svg.SetAttribute("xmlns", "http://www.w3.org/2000/svg");
            svg.SetAttribute("xmlns:xlink", "http://www.w3.org/1999/xlink");
            svg.SetAttribute("width", "400");
            svg.SetAttribute("height", "400");
            svg.SetAttribute("viewBox", "-200 -200 400 400");
            _xml.AppendChild(svg);
        }

        private void paintLineChart()
        {
            XmlElement line = _xml.CreateElement("polyline");
            svg.AppendChild(line);

            string temp = "";
            foreach (Tuple<float, float> tuple in Points)
            {
                temp += tuple.Item1 + "," + tuple.Item2 + " ";
            }
            line.SetAttribute("points", temp);
        }

        public XmlDocument GetChart()
        {
            paintLineChart();
            return _xml;
        }

        public string CreateLineChart(List<Tuple<float, float>> points)
        {
            Points = points;

            return GetChart().ToString();
        }

        public string CreateLineChart(List<float> points)
        {
            throw new NotImplementedException();
        }
    }
}
