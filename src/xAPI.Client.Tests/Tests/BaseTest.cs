using NUnit.Framework;
using System.IO;

namespace xAPI.Client.Tests
{
    public abstract class BaseTest
    {
        protected string ReadDataFile(string file)
        {
            return File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", file));
        }
    }
}
