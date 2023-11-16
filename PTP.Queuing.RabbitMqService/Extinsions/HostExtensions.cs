using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Queuing.RabbitMqService.Extinsions
{
    public static class HostExtensions
    {
        /*  public static IHost UseQueuingRabbitMqConsumers(this IHost app, params Type[] messageAssemblyMarkerTypes)
          {
              return RegisterQueuesConsumers(app, messageAssemblyMarkerTypes.Select((Type t) => t.GetTypeInfo().Assembly));

          }
          private static IHost RegisterQueuesConsumers(IHost host, IEnumerable<Assembly> assembliesToScan)
          {
              var messageTypes = assembliesToScan
                  .SelectMany(a => a.GetExportedTypes())
                  .Where(type => !type.IsAbstract && typeof(IQueuingMessage).IsAssignableFrom(type));

              foreach (var type in messageTypes)
              {
                  var consumer = host.Services.GetService(typeof(IMessageQueuingConsumer<>).MakeGenericType(type));
                  consumer?.GetType().GetMethod("Consume")?.Invoke(consumer,null);
              }
              return host;

          }
      }*/
    }
}
