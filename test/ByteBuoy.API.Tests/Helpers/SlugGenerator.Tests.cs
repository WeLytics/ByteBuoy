using ByteBuoy.Application.Helpers;
using FluentAssertions;

namespace ByteBuoy.API.Tests.Helpers
{
	public class SlugGeneratorTests
	{
		[Theory]
		[InlineData("This Is My Title", "this-is-my-title")]
		[InlineData("This-Is- It","this-is-it")]
		[InlineData("%&@123: should be removed", "123-should-be-removed")]
		[InlineData("%&@", "")]
		public void SlugGenerator_Titles(string title, string expectedValue)
		{
			// Arrange
			
			// Act
			var result = SlugGenerator.GenerateSlug(title);

			// Assert
			result.Should().Be(expectedValue);
		}
	}
}
