using System;
using System.Collections.Generic;
using System.Xml;

namespace SvgChart
{
    public class SvgLineChart : ILineChart
    {
        private XmlDocument _svg;

        public SvgLineChart()
        {
            _svg = new XmlDocument();
            //
        }

        public string CreateLineChart(List<Tuple<float, float>> points)
        {
            throw new NotImplementedException();
        }

        public string CreateLineChart(List<float> points)
        {
            throw new NotImplementedException();
        }
    }
}
