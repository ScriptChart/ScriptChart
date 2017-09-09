using System;
using System.Collections.Generic;

namespace SvgChart
{
    public class SvgLineChart : ILineChart
    {
        public string CreateLineChart(List<Tuple<float, float>> Points)
        {
            float MinX = float.MaxValue;
            float MaxX = float.MinValue;
            float MinY = float.MaxValue;
            float MaxY = float.MinValue;
            foreach (Tuple<float, float> Point in Points)
            {
                if (Point.Item1 < MinX) MinX = Point.Item1;
                if (Point.Item1 > MaxX) MaxX = Point.Item1;
                if (Point.Item2 < MinY) MinX = Point.Item2;
                if (Point.Item2 > MaxY) MaxY = Point.Item2;
            }

            Svg Svg = new Svg(MinX, MaxX, MinY, MaxY);
            Svg.polyline("/svg", Points);
            return Svg.ToString();
        }

        public string CreateLineChart(List<float> Points)
        {
            List<Tuple<float, float>> Points2 = new List<Tuple<float, float>>();
            float x = 0;
            foreach (float y in Points)
            {
                Points2.Add(new Tuple<float, float>(x, y));
            }
            return CreateLineChart(Points2);
        }
    }
}
