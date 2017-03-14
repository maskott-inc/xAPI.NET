using FluentAssertions;
using JsonDiffPatchDotNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using xAPI.Client.Resources;

namespace xAPI.Client.Tests.Tests
{
    public class SerializationTests : BaseTest
    {
        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Error };
        private static readonly JsonDiffPatch _differ = new JsonDiffPatch();

        [Test]
        public void can_deserialize_about_without_missing_members()
        {
            // Arrange
            string json = this.ReadDataFile("about/get.json");

            // Act
            About resource = JsonConvert.DeserializeObject<About>(json, _serializerSettings);

            // Assert
            resource.Should().NotBeNull();
        }

        [Test]
        public void can_deserialize_person_without_missing_members()
        {
            // Arrange
            string json = this.ReadDataFile("agents/get.json");

            // Act
            Person resource = JsonConvert.DeserializeObject<Person>(json, _serializerSettings);

            // Assert
            resource.Should().NotBeNull();
        }

        [Test]
        public void can_deserialize_activity_without_missing_members()
        {
            // Arrange
            string json = this.ReadDataFile("activities/get.json");

            // Act
            Activity resource = JsonConvert.DeserializeObject<Activity>(json, _serializerSettings);

            // Assert
            resource.Should().NotBeNull();
        }

        [Test]
        public void can_deserialize_statement_without_missing_members()
        {
            // Arrange
            string json = this.ReadDataFile("statements/get.json");

            // Act
            Statement resource = JsonConvert.DeserializeObject<Statement>(json, _serializerSettings);

            // Assert
            resource.Should().NotBeNull();
        }

        [Test]
        public void can_serialize_statement_properly()
        {
            // Arrange
            string originalJson = this.ReadDataFile("statements/get.json");
            JToken originalData = JToken.Parse(originalJson);
            Statement resource = JsonConvert.DeserializeObject<Statement>(originalJson);
            string targetJson = JsonConvert.SerializeObject(resource, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
            JToken targetData = JToken.Parse(targetJson);

            // Act
            JToken diff = _differ.Diff(originalData, targetData);

            // Assert
            diff.Should().BeNull();
        }
    }
}
