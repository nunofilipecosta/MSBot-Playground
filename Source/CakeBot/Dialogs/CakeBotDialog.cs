using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace CakeBot.Dialogs
{
    public class CakeBotDialog
    {
        public static readonly IDialog<string> dialog = Chain.PostToChain()
            .Select(msg => msg.Text)
            .Switch(new RegexCase<IDialog<string>>(new Regex("", RegexOptions.IgnoreCase), (context, text) =>
            {
                return Chain.ContinueWith(new WelcomeBotDialog(), AfterWelcomeContinuation);
            }
            ));

        //new RegexCase<IDialog<string>>(new Regex("^hi", RegexOptions.IgnoreCase), (context, text) => 
        //        { return Chain.ContinueWith(new WelcomeBotDialog(), AfterWelcomeContinuation); }
        //        )

        private async static Task<IDialog<string>> AfterWelcomeContinuation(IBotContext context, IAwaitable<IMessageActivity> item)
{ }

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as IMessageActivity;

            // TODO: Put logic for handling user message here

            context.Wait(MessageReceivedAsync);
        }
    }
}