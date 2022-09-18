using KoalaKit.Messaging;
using Microsoft.Extensions.DependencyInjection;
using PTP.Queing.Meassage;

namespace PTP.Queing.Handlers.CreateUserHandlers
{
    public static class UserHandlersExtinsions
    {
        public static void ConfigreRabitHandlersServices(this IServiceCollection services)
        {
            services.AddTransient<IMessagingHandler<CreateUserMessage>, CreateUserHandlers>();

        }
    }
}
