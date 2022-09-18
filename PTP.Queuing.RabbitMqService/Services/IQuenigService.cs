using KoalaKit.Messaging.Queuing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Queuing.RabbitMqService.Services
{
    public interface IQuenigService<in TMessage>
              where TMessage : IQueuingMessage, new()
    {
        Task PublishToMQ(TMessage message);
    }
}
