using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ByteBuoy.Core.Helpers
{
	public partial class IOHelper
	{
		public static string GetVersion()
		{
			return typeof(IOHelper).Assembly.GetName().Version?.ToString() ?? "";
		}

		public static async Task CopyFileAsync(string sourcePath, string destinationPath)
		{
			using var sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
			using var destinationStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
			await sourceStream.CopyToAsync(destinationStream);
		}

		public static async Task MoveFileAsync(string sourcePath, string destinationPath)
		{
			await CopyFileAsync(sourcePath, destinationPath);
			File.Delete(sourcePath);
		}

		public static bool IsFileIgnored(string filePath, IEnumerable<string> ignorePatterns)
		{
			if (ignorePatterns == null || !ignorePatterns.Any())
				return false;

			var fileName = Path.GetFileName(filePath);
			var comparison = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;

			return ignorePatterns.Any(pattern => fileName.Equals(pattern, comparison));
		}

		public static string ResolvePathWithDynamicPlaceholders(string path)
		{
			var placeholderRegex = PlaceholderGroupPattern();
			var matches = placeholderRegex.Matches(path);

			foreach (var match in matches.Cast<Match>())
			{
				var placeholder = match.Groups[1].Value; 
				var replacement = ResolvePathPlaceholder(placeholder);
				if (!string.IsNullOrEmpty(replacement))
				{
					path = path.Replace($"{{{placeholder}}}", replacement);
				}
			}

			return path;
		}

		private static string ResolvePathPlaceholder(string placeholder)
		{
			var currentDate = DateTime.Now;

			placeholder = placeholder
							.Replace("yyyy", currentDate.ToString("yyyy"))
							.Replace("yy", currentDate.ToString("yy"))
							.Replace("MM", currentDate.ToString("MM"))
							.Replace("dd", currentDate.ToString("dd"));

			return placeholder;
		}

		[GeneratedRegex(@"\{([^}]+)\}")]
		private static partial Regex PlaceholderGroupPattern();
	}
}
