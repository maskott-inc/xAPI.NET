using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// In some cases an Attachment is logically an important part of a Learning Record.
    /// It could be an essay, a video, etc. Another example of such an Attachment is (the
    /// image of) a certificate that was granted as a result of an experience. It is useful
    /// to have a way to store these Attachments in and retrieve them from an LRS.
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Identifies the usage of this Attachment. For example: one expected use case
        /// for Attachments is to include a "completion certificate". An IRI corresponding
        /// to this usage MUST be coined, and used with completion certificate attachments.
        /// </summary>
        [JsonProperty("usageType", Required = Required.Always)]
        [Required]
        public Uri UsageType { get; set; }

        /// <summary>
        /// Display name (title) of this Attachment.
        /// </summary>
        [JsonProperty("display", Required = Required.Always)]
        [Required, ValidateProperty]
        public LanguageMap Display { get; set; }

        /// <summary>
        /// A description of the Attachment.
        /// </summary>
        [JsonProperty("description")]
        [ValidateProperty]
        public LanguageMap Description { get; set; }

        /// <summary>
        /// The content type of the Attachment.
        /// </summary>
        [JsonProperty("contentType", Required = Required.Always)]
        [Required]
        public string ContentType { get; set; }

        /// <summary>
        /// The length of the Attachment data in octets.
        /// </summary>
        [JsonProperty("length", Required = Required.Always)]
        public uint Length { get; set; }

        /// <summary>
        /// The SHA-2 hash of the Attachment data.
        /// This property is always required, even if fileURL is also specified.
        /// </summary>
        [JsonProperty("sha2", Required = Required.Always)]
        [Required]
        public string SHA2 { get; set; }

        /// <summary>
        /// An IRL at which the Attachment data can be retrieved, or from which it used
        /// to be retrievable.
        /// </summary>
        [JsonProperty("fileUrl")]
        public Uri FileUrl { get; set; }
    }
}
