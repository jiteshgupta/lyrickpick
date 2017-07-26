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

        public string GetSongsByArtist(int artistID, int pageNum)
        {
            string url = @"http://api.musixmatch.com/ws/1.1/track.search?f_artist_id=" + artistID + @"&page_size=25&page=" + pageNum + @"&s_track_rating=desc&apikey=" + musixmatchAPIkey;
            songsData = FetchData.FetchDataFromURL(url);
            return songsData;
        }
        //ideally should not use getsongsbyartist (string)
        public string GetSongsByArtist(string artist, int pageNum)
        {
            string url = @"http://api.musixmatch.com/ws/1.1/track.search?q_artist=" + Uri.EscapeDataString(artist) + @"&page_size=10&page=" + pageNum + @"&s_track_rating=desc&apikey=" + musixmatchAPIkey;
            songsData = FetchData.FetchDataFromURL(url);
            return songsData;
        }

        
        public string GetSongsByGenre(int genreID, int pageNum)
        {
            string url = @"http://api.musixmatch.com/ws/1.1/track.search?f_music_genre_id=" + genreID + @"&page_size=25&page=" + pageNum + @"&s_track_rating=desc&apikey=" + musixmatchAPIkey;
            songsData = FetchData.FetchDataFromURL(url);
            return songsData;
        }

	}
}