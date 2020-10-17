using Moq;
using NUnit.Framework;
using System.Net;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	class InstallerHelperTests
	{
		private InstallerHelper helper;
		private Mock<IFileDownloader> downloader;

		[SetUp]
		public void Setup()
		{
			downloader = new Mock<IFileDownloader>();
			helper = new InstallerHelper(downloader.Object);
		}

		[Test]
		public void DownloadInstaller_DownloadFileSuccess_ReturnsTrue()
		{
			var result = helper.DownloadInstaller(string.Empty, string.Empty);

			Assert.That(result, Is.True);
		}

		[Test]
		public void DownloadInstaller_DownloadFileUnsuccess_ReturnsFalse()
		{
			downloader.Setup(d => d.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

			var result = helper.DownloadInstaller(string.Empty, string.Empty);

			Assert.That(result, Is.False);
		}
	}
}