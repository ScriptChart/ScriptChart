using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public interface IDbReader
    {
        float[][] ReadChart(string chartId);
    }
}