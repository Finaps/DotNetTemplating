using System.Threading.Tasks;
using System;

namespace MicroService.Common.Common
{
  public interface IResourceRepository<T>
  {
    ValueTask<T> FindOrGet(string id);
    void ClearStore();

  }
}
