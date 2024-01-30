using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using ByteBuoy.Domain.Entities;

namespace ByteBuoy.Application.Helpers
{
	public static partial class SlugGenerator
	{
		[GeneratedRegex(@"[^a-z0-9\s-]")]
		private static partial Regex InvalidCharacters();
		[GeneratedRegex(@"\s+")]
		private static partial Regex MultipleSpaces();
		[GeneratedRegex(@"\-+")]
		private static partial Regex DuplicatedHyphens();

		public static string GenerateSlug(string title)
		{
			if (string.IsNullOrWhiteSpace(title))
			{
				return "";
			}

			var result = title.ToLowerInvariant();

			// Remove invalid characters
			result = InvalidCharacters().Replace(result, "");

			// Convert multiple spaces into one space
			result = MultipleSpaces().Replace(result, " ").Trim();

			// Replace spaces with hyphens
			result = result.Replace(' ', '-');

			// Remove duplicate hyphens
			result = DuplicatedHyphens().Replace(result, "-");
			return result;
		}
    }
}
