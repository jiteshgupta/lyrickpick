using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using LyrickPick.Processors;

namespace LyrickPick.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            Quiz qz;

            if (!context.ConversationData.TryGetValue(ContextConstants.quiz, out qz))
            {
                qz = new Quiz();
                context.ConversationData.SetValue(ContextConstants.quiz, qz);
            }

            await context.PostAsync($"Welcome to the Lyric Pick bot");
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            string userInput = activity.Text;

            string question = String.Empty;
            Quiz qz;
            if (context.ConversationData.TryGetValue(ContextConstants.quiz, out qz))
            {
                question = qz.Question();
            }
            // return our reply to the user
            await context.PostAsync(question);

            context.Wait(MessageReceivedAsync);
        }
    }
}