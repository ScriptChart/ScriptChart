using System;
using System.Collections.Generic;
using System.Text;

namespace SvgChart
{
    public interface ILineChart
    {
        string CreateLineChart(List<Tuple<float, float>> points);

        string CreateLineChart(List<float> points);
    }
}
