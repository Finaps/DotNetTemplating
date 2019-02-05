using System;
using System.Collections.Generic;

using System.Linq.Expressions;

namespace communication.Interfaces
{
    public interface IMessageQueue
    {
      void Publish<T>(string key, T message);

      void Publish(string key, string message);
    }
}