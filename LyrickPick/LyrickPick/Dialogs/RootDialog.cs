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

            await context.PostAsync($"Welcome to the Lyric Pick bot! Let's Start...");
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            string userInput = activity.Text;
            string botOutput = String.Empty;

            Quiz qz;
            if (context.ConversationData.TryGetValue(ContextConstants.quiz, out qz))
            {
                string question = String.Empty;

                if (!context.ConversationData.TryGetValue(ContextConstants.question, out question))
                {
                    question = qz.Question();
                    context.ConversationData.SetValue(ContextConstants.question, question);
                    botOutput = question;
                }
                else if ((String.Equals("hint", userInput, StringComparison.OrdinalIgnoreCase)))
                {
                    string hint = "You have already asked for hint: " + qz.GetFoo();
                    Console.WriteLine(hint);
                    if (!context.ConversationData.TryGetValue(ContextConstants.hint, out hint))
                    {
                        hint = qz.processHint();
                        context.ConversationData.SetValue(ContextConstants.hint, hint);
                    }
                    botOutput = hint;
                }
                else {
                    string answer = String.Empty;
                    if (String.Equals(qz.GetCurrentContext().GetCurrentSongTitle(), userInput, StringComparison.OrdinalIgnoreCase))
                    {
                        answer = "Correct!";
                    }
                    else
                    {
                        answer = "Wrong!";
                    }
                    answer = answer + "\nSong:- " + qz.GetCurrentContext().GetCurrentSongTitle();
                    answer = answer + "\nArtist:- " + qz.GetCurrentContext().GetCurrentSongArtist();

                    botOutput = answer;
                    context.ConversationData.RemoveValue(ContextConstants.question);
                    context.ConversationData.RemoveValue(ContextConstants.hint);
                }
                context.ConversationData.SetValue(ContextConstants.quiz, qz);
            }

            // return our reply to the user
            await context.PostAsync(botOutput);
            context.Wait(MessageReceivedAsync);
        }
    }
}