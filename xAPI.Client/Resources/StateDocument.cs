using System;

namespace xAPI.Client.Resources
{
    public class StateDocument : StateDocument<dynamic>
    {
    }

    public class StateDocument<T> : BaseDocument<T>
    {
        public Uri ActivityId { get; set; }

        public Agent Agent { get; set; }

        public Guid? Registration { get; set; }
    }
}
