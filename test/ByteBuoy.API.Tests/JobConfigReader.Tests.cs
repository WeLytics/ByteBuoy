using System.Reflection;
using System.Text;
using ByteBuoy.Infrastructure.Config;
using FluentAssertions;
using Microsoft.Extensions.FileProviders;

namespace ByteBuoy.API.Tests
{
	public class JobConfigReaderTests
	{
        [Fact]
        public void YamlConfigReader_ReadV1Template_ShouldBeValid()
        {
			// Arrange
			var fixturePath = "Fixtures\\ConfigV1JobTemplate_Valid.yml";
			var yamlContent = ReadJobTemplateFixture(fixturePath);
			var configReader = new AgentConfigReader();

			// Act
			var jobConfig = configReader.ReadConfigText(yamlContent);

			// Assert
			configReader.Should().NotBeNull();
			configReader.HasErrors.Should().BeFalse();
			jobConfig.Should().NotBeNull();
			jobConfig!.Version.Should().Be(1);
			jobConfig.IsValid().Should().BeTrue();
			jobConfig.Tasks.Should().NotBeNull();
			jobConfig.Tasks.Should().HaveCount(5);
        }

		[Fact]
		public void YamlConfigReader_ReadV1Template_ShouldBeInvalid()
		{
			// Arrange
			var fixturePath = "Fixtures\\ConfigV1JobTemplate_Invalid.yml";
			var yamlContent = ReadJobTemplateFixture(fixturePath);
			var configReader = new AgentConfigReader();

			// Act
			var jobConfig = configReader.ReadConfigText(yamlContent);

			// Assert
			configReader.Should().NotBeNull();
			configReader.HasErrors.Should().BeTrue();
			configReader.ValidationErrors.Should().HaveCount(1);
			jobConfig.Should().BeNull();
		}

		private static string ReadJobTemplateFixture(string fixturePath)
		{
			var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
			using var reader = embeddedProvider.GetFileInfo(fixturePath).CreateReadStream();
			using var stream = new StreamReader(reader, Encoding.UTF8);

			return stream.ReadToEnd();
		}
	}
}
