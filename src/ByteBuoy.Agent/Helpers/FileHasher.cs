using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ByteBuoy.Agent.Helpers
{
	internal class FileHasher
	{
		internal static string GetFileSHA256Hash(string filePath)
		{
			if (!File.Exists(filePath))
				throw new FileNotFoundException("File not found.", filePath);

			using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
			byte[] fileBytes = File.ReadAllBytes(filePath);
			byte[] hashBytes = SHA256.HashData(fileBytes);

			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < hashBytes.Length; i++)
			{
				builder.Append(hashBytes[i].ToString("x2"));
			}
			return builder.ToString();
		}
	}
}
