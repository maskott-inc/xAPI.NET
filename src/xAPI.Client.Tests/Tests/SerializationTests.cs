using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using xAPI.Client.Resources;
using xAPI.Client.Tests.Data;

namespace xAPI.Client.Tests.Tests
{
    public class SerializationTests : BaseTest
    {
        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Error };

        [Test]
        public void can_deserialize_about_without_missing_members()
        {
            // Arrange
            string json = this.ReadDataFile(Constants.ABOUT);

            // Act
            About resource = JsonConvert.DeserializeObject<About>(json, _serializerSettings);

            // Assert
            resource.Should().NotBeNull();
        }

        [Test]
        public void can_deserialize_person_without_missing_members()
        {
            // Arrange
            string json = this.ReadDataFile(Constants.AGENT);

            // Act
            Person resource = JsonConvert.DeserializeObject<Person>(json, _serializerSettings);

            // Assert
            resource.Should().NotBeNull();
        }

        [Test]
        public void can_deserialize_activity_without_missing_members()
        {
            // Arrange
            string json = this.ReadDataFile(Constants.ACTIVITY);

            // Act
            Activity resource = JsonConvert.DeserializeObject<Activity>(json, _serializerSettings);

            // Assert
            resource.Should().NotBeNull();
        }

        [Test]
        public void can_deserialize_statement_without_missing_members()
        {
            // Arrange
            string json = this.ReadDataFile(Constants.STATEMENT_FULL);

            // Act
            Statement resource = JsonConvert.DeserializeObject<Statement>(json, _serializerSettings);

            // Assert
            resource.Should().NotBeNull();
        }

        [Test]
        public void can_serialize_statement_properly()
        {
            // Arrange
            string originalJson = this.ReadDataFile(Constants.STATEMENT_FULL);
            var originalData = JToken.Parse(originalJson);
            Statement resource = JsonConvert.DeserializeObject<Statement>(originalJson);
            string targetJson = JsonConvert.SerializeObject(resource, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
            var targetData = JToken.Parse(targetJson);

            // Act
            bool deepEqual = JToken.DeepEquals(originalData, targetData);

            // Assert
            deepEqual.Should().BeTrue(
                "Original JSON {0} differs from target JSON {1}",
                JsonConvert.SerializeObject(originalData, Formatting.Indented),
                JsonConvert.SerializeObject(targetData, Formatting.Indented)
            );
        }
    }
}
