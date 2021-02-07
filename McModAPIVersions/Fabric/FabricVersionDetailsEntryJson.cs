using System;
using System.Collections.Generic;
using System.Text;

namespace McModAPIVersions.Fabric
{
    public class FabricVersionDetailsEntryJson
    {
        public LoaderEntry loader { get; set; }
        public MappingsEntry mappings { get; set; }

        public class LoaderEntry
        {
            public string separator { get; set; }
            public int build { get; set; }
            public string maven { get; set; }
            public string version { get; set; }
            public bool stable { get; set; }
        }

        public class MappingsEntry
        {
            public string gameVersion { get; set; }
            public string separator { get; set; }
            public int build { get; set; }
            public string maven { get; set; }
            public string version { get; set; }
            public bool stable { get; set; }
        }
    }
}
