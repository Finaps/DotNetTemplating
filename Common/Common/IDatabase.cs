using System;
using System.Collections.Generic;

using System.Linq.Expressions;
using System.Threading;

namespace MicroService.Common.Common
{
  public interface IDatabase<T>
  {
    T InsertItem(T obj, CancellationToken cancellationToken = default(CancellationToken));
    T RetrieveItem(string id, CancellationToken cancellationToken = default(CancellationToken));
    T RetrieveItem(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    List<T> RetrieveItems(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
    List<T> RetrieveItems(Expression<Func<T, bool>> predicate, int limit, int offset, CancellationToken cancellationToken = default(CancellationToken));
    List<T> RetrieveItems();
    List<T> RetrieveItems(int limit, int offset);
    T UpdateItem(T obj, string id, CancellationToken cancellationToken = default(CancellationToken));
    T UpdateItem(T obj, CancellationToken cancellationToken = default(CancellationToken));
    long Count(CancellationToken cancellationToken = default(CancellationToken));
    long Count(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
    void RemoveItem(string id, CancellationToken cancellationToken = default(CancellationToken));
    void RemoveItem(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    void RemoveItem(T obj, CancellationToken cancellationToken = default(CancellationToken));
  }
}
