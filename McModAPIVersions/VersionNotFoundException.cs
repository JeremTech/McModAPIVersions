using System;
using System.Collections.Generic;
using System.Text;

namespace McModAPIVersions
{
    [Serializable]
    public class VersionNotFoundException : Exception
    {
        public VersionNotFoundException() : base() { }
        public VersionNotFoundException(string message) : base(message) { }
        public VersionNotFoundException(string message, Exception inner) : base(message, inner) { }

        // Constructor for serialization
        protected VersionNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
