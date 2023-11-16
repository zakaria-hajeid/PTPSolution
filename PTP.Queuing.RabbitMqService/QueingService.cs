using PTP.Queuing.RabbitMqService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace PTP.Queuing.RabbitMqService
{
   /* public class QueingService<TMessage> : IQuenigService<TMessage>
           where TMessage : IQueuingMessage, new()
    {
        private readonly IMessageQueuingPublisher<TMessage> MessageQueuingPublisher;
        public QueingService(IMessageQueuingPublisher<TMessage> MessageQueuingPublisher)
        {
                this.MessageQueuingPublisher = MessageQueuingPublisher;
    
        }
        public async  Task PublishToMQ(TMessage message)
        {
          await Task.Run(() => MessageQueuingPublisher.Publish(message)); 


        }
    }*/
}
