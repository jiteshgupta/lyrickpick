using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace LyrickPick.Processors
{
	public class FetchData
	{
		public static string FetchDataFromURL(string url)
		{
			string responseData = String.Empty;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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