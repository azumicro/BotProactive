using Microsoft.Bot.Builder;
using System.Threading;
using System.Threading.Tasks;

namespace EmptyBot2
{
    public class TestMiddleware : IMiddleware
    {
        public async Task OnTurnAsync(ITurnContext turnContext, NextDelegate next, CancellationToken cancellationToken = default)
        {
            if (turnContext.Activity.Text == "sample Text")
            {
               await turnContext.SendActivityAsync("em", cancellationToken: cancellationToken);
                return;
                
            }


            await next(cancellationToken);
        }
        
    }
}
