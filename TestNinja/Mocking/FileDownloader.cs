using System.Net;

namespace TestNinja.Mocking
{
	public interface IFileDownloader
	{
		void DownloadFile(string address, string path);
	}

	public class FileDownloader : IFileDownloader
	{
		public void DownloadFile(string address, string path)
		{
			using (var client = new WebClient())
			{
				client.DownloadFile(address, path);
			}
		}
	}
}