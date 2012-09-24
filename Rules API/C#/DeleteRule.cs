using System;
using System.Net;
using System.IO;
using System.Text;

namespace BasicOps
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			MainClass op = new MainClass();
			op.deleteRule();
		}

		public HttpWebRequest makeRequest()
		{	
//			Ensure that your stream format matches the rule format you intend to use (e.g. '.xml' or '.json')
//			See below to edit the rule format used when adding and deleting rules (xml or json)
			
//			Expected Enterprise Data Collector URL formats:
//				JSON:	https://<host>.gnip.com/data_collectors/<data_collector_id>/rules.json
//				XML:	https://<host>.gnip.com/data_collectors/<data_collector_id>/rules.xml

//			Expected Premium Stream URL Format:
//				https://api.gnip.com:443/accounts/<account>/publishers/<publisher>/streams/<stream>/<label>/rules.json
	
			string urlString = "ENTER_RULES_API_URL_HERE"; 	
			string username = "ENTER_USERNAME_HERE";
			string password = "ENTER_PASSWORD_HERE";

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlString);

		    	NetworkCredential nc = new NetworkCredential(username, password);
		    	request.Credentials = nc;
			request.PreAuthenticate = true;

			request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			request.Headers.Add("Accept-Encoding", "gzip");

			return request;			
		}

		public void deleteRule()
		{
			HttpWebRequest request = makeRequest();
			string postData = "";
			string rule = "C# Rule";

			request.Method = "DELETE";

//			Edit below to use the rule format that matches the Rules API URL you entered above

//			Use this line for JSON formatted rules
			postData = "{\"rules\":[{\"value\":\"" + rule + "\"}]}";

//			Use this line for XML formatted rules
			//postData = "<rules><rule><value>" + rule + "</value></rule></rules>";
   
			byte[] byteArray = Encoding.UTF8.GetBytes (postData);
            		request.ContentType = "application/x-www-form-urlencoded";
            		request.ContentLength = byteArray.Length;
           
            Stream dataStream = request.GetRequestStream ();			
	    dataStream.Write (byteArray, 0, byteArray.Length);
	    dataStream.Close ();

            WebResponse response = request.GetResponse ();
            Console.WriteLine (((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream ();
            StreamReader reader = new StreamReader (dataStream);
            string responseFromServer = reader.ReadToEnd ();
            Console.WriteLine (responseFromServer);
			Console.WriteLine();
            reader.Close ();
            dataStream.Close ();
            response.Close ();
		}		
	}
}
