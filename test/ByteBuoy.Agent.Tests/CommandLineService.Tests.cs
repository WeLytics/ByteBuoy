using FluentAssertions;

namespace ByteBuoy.Agent.Tests
{
	public class CommandLineServiceTests
	{
		[Fact]
		public void CommandLineService_Constructor_ShouldMapArguments()
		{
			// Arrange
			var args = new string[] { "--dryrun", "--file", "test.yaml" };

			// Act
			var sut = new Services.CommandLineService(args);

			// Assert
			sut.FilePath.Should().Be("test.yaml");
			sut.DryRun.Should().BeTrue();
		}

		[Fact]
		public void CommandLineService_Constructor_ShouldMapShortFormatArguments()
		{
			// Arrange
			var args = new string[] { "-d", "-f", "test.yaml" };

			// Act
			var sut = new Services.CommandLineService(args);

			// Assert
			sut.FilePath.Should().Be("test.yaml");
			sut.DryRun.Should().BeTrue();
		}

		[Fact]
		public void CommandLineService_NoArguments_ShouldMapDefault()
		{
			// Arrange

			// Act
			var sut = new Services.CommandLineService(null);

			// Assert
			sut.FilePath.Should().Be("bytebuoy.yml");
			sut.DryRun.Should().BeFalse();
		}
	}
}
