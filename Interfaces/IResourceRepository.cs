using System.Threading.Tasks;
using System;

namespace MicroService.Interfaces
{
  public interface IResourceRepository<T>
  {
    ValueTask<T> FindOrGet(string id);
    void ClearStore();

  }
}