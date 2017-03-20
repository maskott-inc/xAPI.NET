using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// Extensions are available as part of Activity Definitions, as part of
    /// a Statement's "context" property, or as part of a Statement's "result"
    /// property. In each case, extensions are intended to provide a natural
    /// way to extend those properties for some specialized use. The contents
    /// of these extensions might be something valuable to just one application,
    /// or it might be a convention used by an entire Community of Practice.
    /// </summary>
    public class Extensions : Dictionary<Uri, JToken>
    {
    }
}
