using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using LyrickPick.Processors;
using System.Diagnostics;

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
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            string userInput = activity.Text;

            string botOutput = String.Empty;
            string question = String.Empty;
            bool userMessage = true;

            while (userMessage)
            {
                Quiz qz = context.ConversationData.GetValue<Quiz>(ContextConstants.quiz);
                if (true)
                {
                    if (!context.ConversationData.TryGetValue(ContextConstants.question, out question))
                    {
                        question = qz.Question();
                        context.ConversationData.SetValue(ContextConstants.question, question);
                        botOutput = question;
                        userMessage = false;
                    }
                    else if ((String.Equals("hint", userInput, StringComparison.OrdinalIgnoreCase)))
                    {
                        string hint = String.Empty;
                        Console.WriteLine(hint);
                        if (!context.ConversationData.TryGetValue(ContextConstants.hint, out hint))
                        {
                            hint = qz.processHint();
                            context.ConversationData.SetValue(ContextConstants.hint, hint);
                        }
                        else
                        {
                            hint = "Sorry but you have already asked for hint";
                        }
                        botOutput = hint;
                        userMessage = false;
                    }
                    else
                    {
                        string answer = String.Empty;
                        if ((String.Equals("pass", userInput, StringComparison.OrdinalIgnoreCase)))
                        {
                            answer = answer + "\nSong:- " + qz.GetCurrentContext().GetCurrentSongTitle();
                            answer = answer + "\nArtist:- " + qz.GetCurrentContext().GetCurrentSongArtist();

                            context.ConversationData.RemoveValue(ContextConstants.question);
                            context.ConversationData.RemoveValue(ContextConstants.hint);
                            userMessage = true;
                        }
                        else
                        {
                            ResultsProcessor rp = new ResultsProcessor();
                            //if (String.Equals(qz.GetCurrentContext().GetCurrentSongTitle(), userInput, StringComparison.OrdinalIgnoreCase))
                            if (rp.checkSongGuess(userInput, qz.GetCurrentContext().GetCurrentSong()))
                            {
                                answer = "Correct!";
                                context.ConversationData.RemoveValue(ContextConstants.question);
                                context.ConversationData.RemoveValue(ContextConstants.hint);
                                userMessage = true;
                            }
                            else
                            {
                                answer = "Wrong!";
                                userMessage = false;
                            }
                        }
                        botOutput = answer;
                    }
                }

                context.ConversationData.SetValue(ContextConstants.quiz, qz);
                // return our reply to the user
                await context.PostAsync(botOutput);
            }
            context.Wait(MessageReceivedAsync);
        }
    }
}