using System.IO;

namespace TestNinja.Mocking
{
	public interface IFileReader
	{
		string ReadText(string name);
	}

	public sealed class FileReader : IFileReader
	{
		public string ReadText(string name)
		{
			return File.ReadAllText(name);
		}
	}
}