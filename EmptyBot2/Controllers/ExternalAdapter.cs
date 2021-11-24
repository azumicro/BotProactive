using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Schema;
using System.Collections.Concurrent;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EmptyBot2.Controllers
{
    [Route("api/notify")]
    [ApiController]
    public class ExternalAdapter : ControllerBase
    {
        private IBotFrameworkHttpAdapter _externAdapter;
        public ExternalAdapter(IBotFrameworkHttpAdapter adapter)
        {
            _externAdapter = adapter;
        }

        public async Task<IActionResult> Get()
        {

            var _userReference = TestProactive.TestProactive.ConversationReference;
            //read and send value from conversation reference

            foreach (var conversationReference in _userReference.Values)
            {
                await ((BotAdapter)_externAdapter).ContinueConversationAsync(string.Empty, conversationReference,
                    ExternalCallback, default(CancellationToken));
            }


            var result = new ContentResult();
            result.StatusCode = (int)HttpStatusCode.OK;
            result.ContentType = "text/html";
            result.Content = "<html> Hey I sent the message to the users </html>";

            return result;

        }

        private async Task ExternalCallback(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            await turnContext.SendActivityAsync(MessageFactory.Text("Hey am external source"), cancellationToken);
        }
    }
}
