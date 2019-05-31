using System.Collections.Generic;
using System.Linq;

namespace MicroService.Common.Query
{
  public class PaginatedResponse<T>
  {
    public long Total { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
    public IList<T> Data { get; set; }
    public static PaginatedResponse<T> Create(IList<T> data, int limit, int offset, long total)
    {
      return new PaginatedResponse<T>()
      {
        Limit = limit,
        Offset = offset,
        Total = total,
        Data = data
      };
    }
  }
}
