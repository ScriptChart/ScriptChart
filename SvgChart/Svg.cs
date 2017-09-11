using System;
using System.Collections.Generic;
using System.Xml;

namespace SvgChart
{
    public class Svg
    {
        private XmlDocument _xml;

        public Svg(float MinX, float MinY, float MaxX, float MaxY)
        {
            _xml = new XmlDocument();
            XmlElement svg = _xml.CreateElement("svg");
            _xml.AppendChild(svg);

            svg.SetAttribute("version", "1.2");
            svg.SetAttribute("xmlns", "http://www.w3.org/2000/svg");
            svg.SetAttribute("xmlns:xlink", "http://www.w3.org/1999/xlink");

            float Width = MaxX - MinX;
            float Height = MaxY - MinY;
            svg.SetAttribute("width",  Width.ToString() );
            svg.SetAttribute("height", Height.ToString() );
            svg.SetAttribute("viewBox", MinX.ToString() + " " + MaxX.ToString() + " " + MinY.ToString() + " " + MaxY.ToString() );
        }

        public void polyline(string XPath, List<Tuple<float, float>> Points)
        {
            XmlElement line = _xml.CreateElement("polyline");
            XmlNode parent = _xml.SelectSingleNode(XPath);
            parent.AppendChild(line);

            string temp = "";
            foreach (Tuple<float, float> tuple in Points)
            {
                temp += tuple.Item1 + "," + tuple.Item2 + " ";
            }
            line.SetAttribute("points", temp);
        }

        public override string ToString()
        {
            return _xml.OuterXml;
        }
    }
}
