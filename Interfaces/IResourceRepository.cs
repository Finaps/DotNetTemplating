using system;

namespace communication.Interfaces
{
  public interface IResourceRepository<T>
  {
    T FindOrGet(string id);
    void ClearStore();

  }
}