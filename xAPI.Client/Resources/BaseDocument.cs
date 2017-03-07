using System;

namespace xAPI.Client.Resources
{
    public abstract class BaseDocument<T>
    {
        public string ETag { get; set; }

        public DateTimeOffset? LastModified { get; set; }

        public T Content { get; set; }
    }
}
