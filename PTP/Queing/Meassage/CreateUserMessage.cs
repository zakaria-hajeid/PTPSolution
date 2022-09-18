using KoalaKit.Messaging.Queuing;
using System;

namespace PTP.Queing.Meassage
{
    [Serializable]
    public class CreateUserMessage: IQueuingMessage
    {
      public  string QueueName { get; } = "Users";
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

    }
}
