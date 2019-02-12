using System.Collections.Generic;

namespace MicroService.Common.Query
{
    public class PaginatedResponse<T>
    {
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public IList<T> Data { get; set; }
        public static PaginatedResponse<T> Create(IList<T> data, int offset, int total)
        {
            return new PaginatedResponse<T>()
            {
                Limit = data.Count,
                Offset = offset,
                Total = total,
                Data = data
            };
        }
    }
}