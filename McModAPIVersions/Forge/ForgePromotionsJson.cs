using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace McModAPIVersions
{
    /// <summary>
    /// Representing Forge versions JSON file
    /// </summary>
    public class ForgePromotionsJson
    {
        // Variables according to the forge versions json content
        public string Homepage { get; set; }
        public JObject Promos { get; set; }

        /// <summary>
        /// Get the latest version of forge for a specific Minecraft version
        /// </summary>
        /// <param name="mcVersion">Minecraft version</param>
        /// <returns>Latest Forge version for <paramref name="mcVersion"/> Minecraft version</returns>
        /// <exception cref="VersionNotFoundException">Thrown when no latest version has been found</exception>
        public string GetLatestVersion(string mcVersion)
        {
            // Check if promos is not null
            if (this.Promos != null && !string.IsNullOrWhiteSpace(mcVersion))
            {
                // Check if there is mcVersion-latest entry
                if(this.Promos.ContainsKey(mcVersion + "-latest"))
                {
                    return this.Promos[mcVersion + "-latest"].ToString();
                }
            }

            // Throw exception
            throw new VersionNotFoundException(String.Format("No latest Forge version for Minecraft {0} found.", mcVersion));
        }

        /// <summary>
        /// Get the recommended version of forge for a specific Minecraft version
        /// </summary>
        /// <param name="mcVersion">Minecraft version</param>
        /// <returns>Recommended Forge version for <paramref name="mcVersion"/> Minecraft version</returns>
        /// <exception cref="VersionNotFoundException">Thrown when no recommended version has been found</exception>
        public string GetRecommendedVersion(string mcVersion)
        {
            // Check if promos is not null
            if (this.Promos != null && !string.IsNullOrWhiteSpace(mcVersion))
            {
                // Check if a recommended version is available
                if (this.Promos.ContainsKey(mcVersion + "-recommended"))
                {
                    return this.Promos[mcVersion + "-recommended"].ToString();
                }
            }

            // Throw exception
            throw new VersionNotFoundException(String.Format("No recommended Forge version for Minecraft {0} found.", mcVersion));
        }
    }
}
