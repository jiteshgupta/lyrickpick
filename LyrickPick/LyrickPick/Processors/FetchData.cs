using System.IO;
using System.Net;

namespace LyrickPick.Processors
{
    public class FetchData
	{
		public static string FetchDataFromURL(string url)
		{
			string responseData = string.Empty;
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.AutomaticDecompression = DecompressionMethods.GZip;

			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			using (Stream stream = response.GetResponseStream())
			using (StreamReader reader = new StreamReader(stream))
			{
				responseData = reader.ReadToEnd();
			}

			return responseData;
		}
        public static string FetchDataBSC(string url)
        {
            string responseData = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Ocp-Apim-Subscription-Key: 25798a1f9552475e83078317d412ec95");
            
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                responseData = reader.ReadToEnd();
            }

            return responseData;
        }
    }
}