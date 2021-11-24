using AdaptiveExpressions.Properties;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace TestProactive
{
    //TestProactive
    public class TestProactive : Dialog
    {
        public TestProactive([CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) : base()
        {
            RegisterSourceLocation(sourceFilePath, sourceLineNumber);
        }

        [JsonProperty("$Kind")]
        public const string Kind = "TestProactive";

        [JsonProperty("resultProperty")]
        public StringExpression ResultProperty { get; set; }

        public static readonly ConcurrentDictionary<string, ConversationReference> ConversationReference =
            new ConcurrentDictionary<string, ConversationReference>();
     
        public override Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var result = AddConversationReference(dc.Context.Activity);
            //find activity object pass to method and store in dictionary




            if (ResultProperty != null)
            {
                dc.State.SetValue(this.ResultProperty.GetValue(dc.State), result);
                //store in result property and send value to composer.
            }

            return dc.EndDialogAsync(result: result, cancellationToken: cancellationToken);

           
        }
        private string AddConversationReference(Activity activity)
        {
            var conversationReference = activity.GetConversationReference();

            var userInfo = ConversationReference.AddOrUpdate(conversationReference.User.Id, conversationReference, (key, NewValue) => conversationReference);
            if (userInfo != null)
                return userInfo.User.Id;
            // is stored return user id.


            return "User information is missing";
        }
    }
}
