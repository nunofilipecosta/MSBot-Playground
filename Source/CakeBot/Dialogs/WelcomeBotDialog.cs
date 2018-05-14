using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace CakeBot.Dialogs
{
    [Serializable]
    public class WelcomeBotDialog : IDialog<string>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hello !");
            await Respond(context);

            context.Wait(MessageReceivedAsync);
        }

        private async Task Respond(IDialogContext context)
        {
            var username = string.Empty;
            context.UserData.TryGetValue("Name", out username);

            if(string.IsNullOrWhiteSpace(username))
            {
                await context.PostAsync("What is your Name?");
                context.UserData.SetValue("", true);
            }
            else
            {
                await context.PostAsync(string.Format("Hi {0}, how are you today? You may order ...", username));
                await context.PostAsync("Foo, bar, zed");
            }
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            string username = string.Empty;
            bool getName = false;

            context.UserData.TryGetValue("Name", out username);
            context.UserData.TryGetValue("", out getName);

            if(getName)
            {
                username = message.Text;
                context.UserData.SetValue("Name", username);
                context.UserData.SetValue("", false);
            }

            await Respond(context);
            context.Done(message);
        }
    }
}