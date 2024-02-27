using System.Runtime.InteropServices;

namespace ByteBuoy.Agent.Helpers
{
	internal class IOHelper
	{

		internal static async Task CopyFileAsync(string sourcePath, string destinationPath)
		{
			using var sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
			using var destinationStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
			await sourceStream.CopyToAsync(destinationStream);
		}

		internal static async Task MoveFileAsync(string sourcePath, string destinationPath)
		{
			await CopyFileAsync(sourcePath, destinationPath);
			File.Delete(sourcePath);
		}

		internal static bool IsFileIgnored(string filePath, IEnumerable<string> ignorePatterns)
		{
			if (ignorePatterns == null || !ignorePatterns.Any())
				return false;

			var fileName = Path.GetFileName(filePath);	
			var comparison  = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;

			return ignorePatterns.Any(pattern => fileName.Equals(pattern, comparison));
		}	
	}
}
