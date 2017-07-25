using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LyrickPick.Processors
{
	public class Context
	{
		private Song currentSong;
		private string[] currentSongLines;
		private List<String> selectedLines;
		private bool isHintUsed = false;
		private bool isArtistUsed = false;

		public Context()
		{
			selectedLines = new List<String>();
		}

		public void SetCurrentSong(Song currentSong)
		{
			this.currentSong = currentSong;
		}

		public void SetCurrentSongLines(string[] currentSongLines)
		{
			this.currentSongLines = currentSongLines;
		}

		public void SetIsHintUsed(bool isHintUsed)
		{
			this.isHintUsed  = isHintUsed;
		}

		public void SetIsArtistUsed(bool isArtistUsed)
		{
			this.isArtistUsed = isArtistUsed;
		}

		public void AddtoSelectedLines(string line)
		{
			selectedLines.Add(line);
		}

		public string GetCurrentSongTitle()
		{
			return currentSong.getTitle();
		}

		public string GetCurrentSongArtist()
		{
			return currentSong.getArtist();
		}

		public string[] GetCurrentSongLines()
		{
			return currentSongLines;
		}

		public List<String> GetSelectedLinesList()
		{
			return selectedLines;
		}

		public bool GetIsHintUsed()
		{
			return isHintUsed;
		}

		public bool GetIsArtistUsed()
		{
			return isArtistUsed;
		}
	}
}