using System;

namespace LyrickPick.Processors
{
	public class FetchSongs
	{
		private static string musixmatchAPIkey = @"8b7654870c8395335a30eb19039218f6";
		private string songsData = string.Empty;

		public string getSongsData()
		{
			return songsData;
		}

		public FetchSongs()
		{
			GetSongs();
		}

		public void GetSongs()
		{
			string url = @"http://api.musixmatch.com/ws/1.1/chart.tracks.get?page=1&page_size=25&country=us&f_has_lyrics=1&apikey=" + musixmatchAPIkey;
			songsData = FetchData.FetchDataFromURL(url);
		}
		public string GetSongs(int pageNum)
		{
			string url = @"http://api.musixmatch.com/ws/1.1/chart.tracks.get?page=" + pageNum + @"&page_size=25&country=us&f_has_lyrics=1&apikey=" + musixmatchAPIkey;
			songsData = FetchData.FetchDataFromURL(url);
			return songsData;
		}

		//assumes already looked up the musixmatch artist_id
		public string GetSongsByArtist(int artistID, int pageNum)
		{
			string url = @"http://api.musixmatch.com/ws/1.1/track.search?f_artist_id=" + artistID + @"&page_size=25&f_has_lyrics=1&page=" + pageNum + @"&s_track_rating=desc&apikey=" + musixmatchAPIkey;
			songsData = FetchData.FetchDataFromURL(url);
			return songsData;
		}

		//assumes year is YYYY
		public string GetSongsByYear(int year, int pageNum)
		{
			var min = year;
			var max = year + 1;
			string url = @"http://api.musixmatch.com/ws/1.1/track.search?f_track_release_group_first_release_date_min=" + min + @"&f_track_release_group_first_release_date_max=" + max + @"&country=us&page_size=10&page=1&s_track_rating=desc&apikey=8b7654870c8395335a30eb19039218f6";
			songsData = FetchData.FetchDataFromURL(url);
			return songsData;
		}
			  
		//assumes already looked up the musixmatch music_genre_id
		public string GetSongsByGenre(int genreID, int pageNum)
		{
			string url = @"http://api.musixmatch.com/ws/1.1/track.search?f_music_genre_id=" + genreID + @"&page_size=25&f_has_lyrics=1&page=" + pageNum + @"&s_track_rating=desc&apikey=" + musixmatchAPIkey;
			songsData = FetchData.FetchDataFromURL(url);
			return songsData;
		}

		//ideally should not use getsongsbyartist (string)
		public string GetSongsByArtist(string artist, int pageNum)
		{
			string url = @"http://api.musixmatch.com/ws/1.1/track.search?q_artist=" + Uri.EscapeDataString(artist) + @"&page_size=10&f_has_lyrics=1&page=" + pageNum + @"&s_track_rating=desc&apikey=" + musixmatchAPIkey;
			songsData = FetchData.FetchDataFromURL(url);
			return songsData;
		}

		public string GetSongsByArtist(int artistID)
		{
			return GetSongsByArtist(artistID, 1);
		}
	}
}