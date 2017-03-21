using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using xAPI.Client.Json;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// Version information in Statements helps Learning Record Consumers get their bearings.
    /// Since the Statement data model is guaranteed consistent through all 1.0.x versions,
    /// in order to support data flow among such LRSs, the LRS is given some flexibility on
    /// Statement versions that are accepted.
    /// </summary>
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

        /// <summary>
        /// Returns true if the xAPI version is supported by version of the client.
        /// </summary>
        /// <returns></returns>
        public bool IsSupported()
        {
            return SUPPORTED_VERSIONS.Any(x => MatchVersion(this._version, x));
        }

        /// <summary>
        /// Returns the version as a SemVer string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this._version;
        }

        /// <summary>
        /// Reads and validates a version string.
        /// </summary>
        /// <param name="version">The version string ("x.y.z").</param>
        /// <returns></returns>
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
