using System.ComponentModel.DataAnnotations;

namespace MicroService.Common.Query
{
  public class Pagination
  {
    [Range(0, int.MaxValue)]
    public int Offset { get; set; }
    [Range(0, 100)]
    public int Limit { get; set; } = 10;
  }

}
