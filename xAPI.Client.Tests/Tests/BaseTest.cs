using NUnit.Framework;
using System.IO;
using xAPI.Client.Tests.FluentExtensions;

namespace xAPI.Client.Tests
{
    public abstract class BaseTest
    {
        [SetUp]
        public void SetUpFluentAssertionsExtensions()
        {
            FluentAssertions.Formatting.Formatter.AddFormatter(new JTokenFormatter());
        }

        protected string ReadDataFile(string file)
        {
            return File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", file));
        }
    }
}
