using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public interface IDbWriter
    {
        Task WriteAsync(string json);
    }
}