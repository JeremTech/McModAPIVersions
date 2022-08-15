using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace McModAPIVersions.Forge
{
    /// <summary>
    /// Representing maven-metadata.json file.
    /// Retrieve Minecraft Forge's version only for Minecraft 1.13 and above.
    /// </summary>
    public class ForgeVersionsJson
    {
        #region Minecraft 1.19 - Wild Update
        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.19.2
        /// </summary>
        [JsonProperty("1.19.2")]
        public List<string> v1192 = null;

        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.19.1
        /// </summary>
        [JsonProperty("1.19.1")]
        public List<string> v1191 = null;

        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.19
        /// </summary>
        [JsonProperty("1.19")]
        public List<string> v119 = null;
        #endregion

        #region Minecraft 1.18 - Cave Update - Part Two
        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.18.2
        /// </summary>
        [JsonProperty("1.18.2")]
        public List<string> v1182 = null;

        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.18.1
        /// </summary>
        [JsonProperty("1.18.1")]
        public List<string> v1181 = null;

        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.18
        /// </summary>
        [JsonProperty("1.18")]
        public List<string> v118 = null;
        #endregion

        #region Minecraft 1.17 - Cave Update - Part One
        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.17.1
        /// </summary>
        [JsonProperty("1.17.1")]
        public List<string> v1171 = null;
        #endregion

        #region Minecraft 1.16 - Nether Update
        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.16.5
        /// </summary>
        [JsonProperty("1.16.5")]
        public List<string> v1165 = null;

        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.16.4
        /// </summary>
        [JsonProperty("1.16.4")]
        public List<string> v1164 = null;

        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.16.3
        /// </summary>
        [JsonProperty("1.16.3")]
        public List<string> v1163 = null;

        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.16.2
        /// </summary>
        [JsonProperty("1.16.2")]
        public List<string> v1162 = null;

        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.16.1
        /// </summary>
        [JsonProperty("1.16.1")]
        public List<string> v1161 = null;
        #endregion

        #region Minecraft 1.15 - Buzzy Bees Update
        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.15.2
        /// </summary>
        [JsonProperty("1.15.2")]
        public List<string> v1152 = null;

        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.15.1
        /// </summary>
        [JsonProperty("1.15.1")]
        public List<string> v1151 = null;

        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.15
        /// </summary>
        [JsonProperty("1.15")]
        public List<string> v115 = null;
        #endregion

        #region Minecraft 1.14 - Village and Pillage Update
        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.14.4
        /// </summary>
        [JsonProperty("1.14.4")]
        public List<string> v1144 = null;

        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.14.3
        /// </summary>
        [JsonProperty("1.14.3")]
        public List<string> v1143 = null;

        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.14.2
        /// </summary>
        [JsonProperty("1.14.2")]
        public List<string> v1142 = null;
        #endregion

        #region Minecraft 1.13 - Aquatic Update
        /// <summary>
        /// List of Minecraft Forge's versions for Minecraft 1.13.2
        /// </summary>
        [JsonProperty("1.13.2")]
        public List<string> v1132 = null;
        #endregion

        /// <summary>
        /// Get all available Minecraft Forge versions for Minecraft <paramref name="mcVersion"/>
        /// </summary>
        /// <param name="mcVersion">Minecraft version</param>
        /// <returns>All available Minecraft Forge versions for Minecraft <paramref name="mcVersion"/></returns>
        /// <exception cref="VersionNotFoundException">Thrown when no versions have been found</exception>
        public List<string> GetForgeVersions(string mcVersion)
        {
            // Convert mcVersion parameter to variable name for reflection
            string VariableName = "v" + mcVersion.Replace(".", "");
            List<string> ForgeVersions = null;

            try 
            {
                ForgeVersions = (List<string>)this.GetType().GetField(VariableName).GetValue(this);
            }
            catch(NullReferenceException)
            {
                throw new VersionNotFoundException(String.Format("No Forge versions for Minecraft {0} found.", mcVersion));
            }

            return ForgeVersions;
        }
    }
}
