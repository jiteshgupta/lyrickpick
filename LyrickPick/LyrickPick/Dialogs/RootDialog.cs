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
        public Task StartAsync(IDialogContext context)
        {
            Quiz qz;

            if (!context.ConversationData.TryGetValue(ContextConstants.quiz, out qz))
            {
                qz = new Quiz();
                context.ConversationData.SetValue(ContextConstants.quiz, qz);
            }

            //await context.PostAsync($"Welcome to the Lyric Pick bot! Let's Start...");
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            string userInput = activity.Text;
            string botOutput = string.Empty;
            string question = string.Empty;

            Quiz qz;
            if (context.ConversationData.TryGetValue(ContextConstants.quiz, out qz))
            {
                if (!context.ConversationData.TryGetValue(ContextConstants.question, out question))
                {
                    question = qz.Question();
                    context.ConversationData.SetValue(ContextConstants.question, question);
                    botOutput = question;
                }
                else if ((string.Equals("hint", userInput, StringComparison.OrdinalIgnoreCase)))
                {
                    string hint = string.Empty;
                    Console.WriteLine(hint);
                    if (!context.ConversationData.TryGetValue(ContextConstants.hint, out hint))
                    {
                        hint = qz.ProcessHint();
                        context.ConversationData.SetValue(ContextConstants.hint, hint);
                    }
                    else
                    {
                        hint = "Sorry but you have already asked for hint";
                    }
                    botOutput = hint;
                }
                else {
                    string answer = string.Empty;
                    if ((string.Equals("pass", userInput, StringComparison.OrdinalIgnoreCase)))
                    {
                        answer = answer + "\nSong:- " + qz.GetCurrentContext().GetCurrentSongTitle();
                        answer = answer + "\nArtist:- " + qz.GetCurrentContext().GetCurrentSongArtist();

                        context.ConversationData.RemoveValue(ContextConstants.question);
                        context.ConversationData.RemoveValue(ContextConstants.hint);
                    }
                    else
                    {
                        ResultsProcessor rp = new ResultsProcessor();
                        //if (string.Equals(qz.GetCurrentContext().GetCurrentSongTitle(), userInput, StringComparison.OrdinalIgnoreCase))
                        if(rp.checkSongGuess(userInput, qz.GetCurrentContext().GetCurrentSong()))
                        {
                            answer = "Correct!";
                            context.ConversationData.RemoveValue(ContextConstants.question);
                            context.ConversationData.RemoveValue(ContextConstants.hint);
                        }
                        else
                        {
                            answer = "Wrong!";
                        }
                    }

                    botOutput = answer;
                }
                context.ConversationData.SetValue(ContextConstants.quiz, qz);
            }

            // return our reply to the user
            await context.PostAsync(botOutput);
            context.Wait(MessageReceivedAsync);
        }
    }
}