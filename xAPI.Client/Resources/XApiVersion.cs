using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using xAPI.Client.Json;

namespace xAPI.Client.Resources
{
    [JsonConverter(typeof(XApiVersionConverter))]
    public class XApiVersion
    {
        internal static readonly string[] SUPPORTED_VERSIONS = new string[]
        {
            "1.0.*"
        };
        private readonly string _version;

        private XApiVersion(string version)
        {
            this._version = version;
        }

        public bool IsSupported()
        {
            return SUPPORTED_VERSIONS.Any(x => MatchVersion(this._version, x));
        }

        public override string ToString()
        {
            return this._version;
        }

        public static XApiVersion Parse(string version)
        {
            if (!ValidateFormat(version))
            {
                throw new ArgumentException("Invalid version string", nameof(version));
            }

            return new XApiVersion(version);
        }

        private static bool ValidateFormat(string version)
        {
            return Regex.IsMatch(version, @"^[0-9]+\.[0-9]+\.[0-9]+$");
        }

        private static bool MatchVersion(string version, string pattern)
        {
            return Regex.IsMatch(version, WildCardToRegular(pattern));
        }

        private static string WildCardToRegular(string value)
        {
            return "^" + Regex.Escape(value).Replace("\\*", ".*") + "$";
        }
    }
}
