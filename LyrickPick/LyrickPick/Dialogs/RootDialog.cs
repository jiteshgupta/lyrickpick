﻿using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using LyrickPick.Processors;
using System.Diagnostics;
using System.Collections.Generic;

namespace LyrickPick.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            Quiz qz;
            if (!context.UserData.TryGetValue(ContextConstants.quiz, out qz))
            {
                qz = new Quiz();
                context.UserData.SetValue(ContextConstants.quiz, qz);
                context.UserData.SetValue(ContextConstants.artist, false);
            }
            await context.PostAsync(ContextConstants.startMessage);
            context.Wait(MessageReceivedAsync);
            return;
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
                Quiz qz;
                if (context.UserData.TryGetValue(ContextConstants.quiz, out qz))
                {
                    if (!context.UserData.TryGetValue(ContextConstants.question, out question))
                    {
                        bool isArtistSet;
                        if (context.UserData.TryGetValue(ContextConstants.artist, out isArtistSet) && isArtistSet)
                        {
                            qz = new Quiz();

                            ResultsProcessor rp = new ResultsProcessor();
                            MMSearch mm = new MMSearch();
                            string artistName = rp.fixGuess(userInput.Trim());

                            List<int> artists = mm.matchArtist(artistName);
                            if (artists.Count != 0)
                            {
                                int artistID = mm.matchArtist(artistName)[0];
                                Quiz.songs = DataParser.GetSongList(Quiz.fs.GetSongsByArtist(artistID, Quiz.pageNum));
                                botOutput = ContextConstants.artistFound + artistName;
                            }
                            else
                            {
                                Quiz.songs = DataParser.GetSongList(Quiz.fs.getSongsData());
                                botOutput = ContextConstants.noArtistFound;
                            }

                            context.UserData.SetValue(ContextConstants.artist, false);
                            context.UserData.RemoveValue(ContextConstants.question);
                            context.UserData.RemoveValue(ContextConstants.hint);
                            userMessage = true;
                        }
                        else
                        {
                            question = qz.Question();
                            context.UserData.SetValue(ContextConstants.question, question);
                            botOutput = question;
                            userMessage = false;
                        }
                    }
                    else if(String.Equals("select artist", userInput.Trim(), StringComparison.OrdinalIgnoreCase) || String.Equals("change artist", userInput.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        botOutput = ContextConstants.artistMessage;
                        context.UserData.SetValue(ContextConstants.artist, true);

                        context.UserData.RemoveValue(ContextConstants.question);
                        context.UserData.RemoveValue(ContextConstants.hint);
                        userMessage = false;
                    }
                    else if ((String.Equals("start", userInput.Trim(), StringComparison.OrdinalIgnoreCase)) || (String.Equals("restart", userInput.Trim(), StringComparison.OrdinalIgnoreCase)))
                    {
                        qz = new Quiz();
                        Quiz.songs = DataParser.GetSongList(Quiz.fs.getSongsData());
                        botOutput = ContextConstants.startMessage;

                        context.UserData.RemoveValue(ContextConstants.question);
                        context.UserData.RemoveValue(ContextConstants.hint);
                        userMessage = true;
                    }
                    else if ((String.Equals("hint", userInput.Trim(), StringComparison.OrdinalIgnoreCase)))
                    {
                        string hint = String.Empty;
                        Console.WriteLine(hint);
                        if (!context.UserData.TryGetValue(ContextConstants.hint, out hint))
                        {
                            hint = qz.ProcessHint();
                            context.UserData.SetValue(ContextConstants.hint, hint);
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
                        if ((String.Equals("pass", userInput.Trim(), StringComparison.OrdinalIgnoreCase)))
                        {
                            answer = answer + "\nSong:- " + qz.GetCurrentContext().GetCurrentSongTitle();
                            answer = answer + "\nArtist:- " + qz.GetCurrentContext().GetCurrentSongArtist();

                            context.UserData.RemoveValue(ContextConstants.question);
                            context.UserData.RemoveValue(ContextConstants.hint);
                            userMessage = true;
                        }
                        else
                        {
                            ResultsProcessor rp = new ResultsProcessor();
                            //if (String.Equals(qz.GetCurrentContext().GetCurrentSongTitle(), userInput, StringComparison.OrdinalIgnoreCase))
                            if (rp.checkSongGuess(userInput, qz.GetCurrentContext().GetCurrentSong()))
                            {
                                answer = "Correct!";

                                answer = answer + "\nSong:- " + qz.GetCurrentContext().GetCurrentSongTitle();
                                answer = answer + "\nArtist:- " + qz.GetCurrentContext().GetCurrentSongArtist();

                                context.UserData.RemoveValue(ContextConstants.question);
                                context.UserData.RemoveValue(ContextConstants.hint);
                                userMessage = true;
                            }
                            else
                            {
                                answer = "Wrong! Guess again, or \"pass\"";
                                userMessage = false;
                            }
                        }
                        botOutput = answer;
                    }
                }

                context.UserData.SetValue(ContextConstants.quiz, qz);
                // return our reply to the user
                await context.PostAsync(botOutput);
            }
            context.Wait(MessageReceivedAsync);
        }
    }
}