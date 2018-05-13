using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FxFFmpeg.Core;
using FxFFmpeg.Objects;

namespace FxFFmpeg.Services
{
	public class MediaFileService
	{
		public IEnumerable<MediaFile> GetMediaFiles(string path, bool includeSubFolder = false)
		{
			var mediaFiles = Enumerable.Empty<MediaFile>().ToList();

			try
			{
				mediaFiles = FFMpegConstants.SupportedMediaExtensions
					.SelectMany(ext => Directory.GetFiles(path, ext)
						.Select(fn => new FileInfo(fn))
						.Select(fi => new MediaFile(fi)))
					.ToList();
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (PathTooLongException)
			{
			}

			if (includeSubFolder)
			{
				try
				{
					var subFolders = Directory.GetDirectories(path);
					return mediaFiles
						.Union(subFolders.SelectMany(f => GetMediaFiles(f, true)))
						.ToList();
				}
				catch (PathTooLongException)
				{
				}
				catch (UnauthorizedAccessException)
				{
				}
			}

			return mediaFiles;
		}
	}
}