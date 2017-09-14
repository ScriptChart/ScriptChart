using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public interface IGraphicGetter
    {
        Task<dynamic> GetResultAsync(Dictionary<string, StringValues> headers, string body);
    }
}
