using System;
using System.Collections.Generic;

using System.Linq.Expressions;

namespace communication.Interfaces
{
  public interface IDatabase<T>
  {
    T RetrieveItem(string id);
    T RetrieveItem(Expression<Func<T, bool>> pred);
    List<T> RetrieveItems(Expression<Func<T, bool>> pred);
    List<T> RetrieveItems();
    T InsertItem(T obj);
    T UpdateItem(T obj, string id);
    void RemoveItem(string id);
  }
}
