using System.Collections.Generic;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// A language map is a dictionary where the key is a RFC 5646 Language Tag, and
    /// the value is a string in the language specified in the tag. This map SHOULD
    /// be populated as fully as possible based on the knowledge of the string in
    /// question in different languages.
    /// The shortest valid language code for each language string is generally preferred.
    /// The ISO 639 language code plus an ISO 3166-1 country code allows for the
    /// designation of basic languages (e.g., es for Spanish) and regions(e.g., es-MX,
    /// the dialect of Spanish spoken in Mexico). If only the ISO 639 language code is
    /// known for certain, do not guess at the possible ISO 3166-1 country code.
    /// For example, if only the primary language is known (e.g., English) then use the
    /// top level language tag en, rather than en-US.If the specific regional variation
    /// is known, then use the full language code.
    /// </summary>
    public class LanguageMap : Dictionary<string, string>
    {
    }
}
