using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace McModAPIVersions.Fabric
{
    /// <summary>
    /// Representing one Minecraft versions supported by fabric in JSON
    /// </summary>
    public class FabricGameVersionsEntryJson
    {
        public string version { get; set; }
        public bool stable { get; set; }
    }
}
