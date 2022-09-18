using KoalaKit.Messaging;
using PTP.Queing.Meassage;
using System.Threading.Tasks;

namespace PTP.Queing.Handlers.CreateUserHandlers
{

    //TODO: CHANGE the location of this handlers and message to seprate library and folder 
    //for each handles
    public class CreateUserHandlers : IMessagingHandler<CreateUserMessage>
    {
        public Task<bool> HandleAsync(CreateUserMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}
