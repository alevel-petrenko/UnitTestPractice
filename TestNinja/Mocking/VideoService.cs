using System;
using System.Collections.Generic;
using System.Data.Entity;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
	public class VideoService
	{
		private readonly IFileReader reader;
		private readonly IVideoRepository repository;

		public VideoService(IFileReader reader, IVideoRepository repository)
		{
			this.reader = reader;
			this.repository = repository;
		}

		public string ReadVideoTitle()
		{
			var str = reader.ReadText("video.txt");
			var video = JsonConvert.DeserializeObject<Video>(str);

			if (video == null)
				return "Error parsing the video.";

			return video.Title;
		}

		public string GetUnprocessedVideosAsCsv()
		{
			var videoIds = new List<int>();

			var videos = this.repository.GetUnprocessedVideos();

			foreach (Video video in videos)
				videoIds.Add(video.Id);

			return String.Join(",", videoIds);
		}
	}
}

public class Video
{
	public int Id { get; set; }
	public string Title { get; set; }
	public bool IsProcessed { get; set; }
}

public class VideoContext : DbContext
{
	public DbSet<Video> Videos { get; set; }
}