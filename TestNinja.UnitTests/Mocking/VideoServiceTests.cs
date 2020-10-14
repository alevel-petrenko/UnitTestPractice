using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	class VideoServiceTests
	{
		private Mock<IFileReader> fileReader;
		private VideoService videoService;
		private Mock<IVideoRepository> repository;

		[SetUp]
		public void SetUp()
		{
			fileReader = new Mock<IFileReader>();
			repository = new Mock<IVideoRepository>();
			videoService = new VideoService(fileReader.Object, repository.Object);
		}

		[Test]
		public void ReadVideoTitle_EmptyFile_ReturnsError()
		{
			fileReader.Setup(reader => reader.ReadText("video.txt")).Returns(string.Empty);

			var result = videoService.ReadVideoTitle();

			Assert.That(result, Does.Contain("error").IgnoreCase);
		}

		[Test]
		public void GetUnprocessedVideosAsCsv_AllProcessedVideos_ReturnsEmptyString()
		{
			repository.Setup(repo => repo.GetUnprocessedVideos()).Returns(new List<Video>());

			var result = videoService.GetUnprocessedVideosAsCsv();

			Assert.That(result, Is.EqualTo(string.Empty));
		}

		[Test]
		public void GetUnprocessedVideosAsCsv_UnprocessedVideosExist_ReturnsStringWithIds()
		{
			var video1 = new Video { Id = 1 };
			var video2 = new Video { Id = 2 };
			var video3 = new Video { Id = 3 };
			repository.Setup(repo => repo.GetUnprocessedVideos()).Returns(new List<Video>() { video1, video2, video3 });

			var result = videoService.GetUnprocessedVideosAsCsv();

			Assert.That(result, Is.EqualTo("1,2"));
		}
	}
}