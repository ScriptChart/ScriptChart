using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public interface IDbReader
    {
        Task<float[][]> ReadChartAsync(string chartId);
    }
}