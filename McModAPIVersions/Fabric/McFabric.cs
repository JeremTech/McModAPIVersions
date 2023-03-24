using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace McModAPIVersions.Fabric
{
    /// <summary>
    /// Get Minecraft Fabric versions
    /// </summary>
    public class McFabric
    {
        /// <summary>
        /// URL to Fabric's supported Minecraft versions JSON
        /// </summary>
        private static string Json_game_version_url = "https://meta.fabricmc.net/v2/versions/game";

        /// <summary>
        /// Base URL to loader infos JSON
        /// </summary>
        private static string JsonGameLoaderInfoBaseUrl = "https://meta.fabricmc.net/v2/versions/loader/";

        /// <summary>
        /// Fabric's supported Minecraft versions JSON object
        /// </summary>
        private static List<FabricGameVersionsEntryJson> FabricGameVersionsJsonObject = null;

        /// <summary>
        /// Fabric's supported Minecraft versions JSON object
        /// </summary>
        private static List<FabricVersionDetailsEntryJson> FabricLoaderInfoObject = null;

        /// <summary>
        /// Get all Minecraft versions supported by Fabric
        /// </summary>
        /// <returns>All Minecraft versions supported by Fabric</returns>
        public static List<string> GetAllSupportedVersions()
        {
            // If FabricGameVersionsJsonObject is null, we retrieve remote json 
            if (FabricGameVersionsJsonObject == null)
            {
                // If FabricGameVersionsJsonObject is null after that
                if (!RetrieveRemoteSupportedVersionsJson())
                {
                    throw new Exception("Unable to retreive remote informations.");
                }
            }

            List<string> supportedVersions = new List<string>();

            // Constructing list
            foreach (FabricGameVersionsEntryJson item in FabricGameVersionsJsonObject)
            {
                supportedVersions.Add(item.version);
            }

            return supportedVersions;
        }

        /// <summary>
        /// Get all stable Minecraft versions supported by Fabric
        /// </summary>
        /// <returns>All stable Minecraft versions supported by Fabric</returns>
        public static List<string> GetAllSupportedStableVersions()
        {
            // If FabricGameVersionsJsonObject is null, we retrieve remote json 
            if (FabricGameVersionsJsonObject == null)
            {
                // If FabricGameVersionsJsonObject is null after that
                if (!RetrieveRemoteSupportedVersionsJson())
                {
                    throw new Exception("Unable to retreive remote informations.");
                }
            }

            List<string> supportedStableVersions = new List<string>();

            // Constructing list
            foreach (FabricGameVersionsEntryJson item in FabricGameVersionsJsonObject)
            {
                if (item.stable)
                {
                    supportedStableVersions.Add(item.version);
                }
            }

            return supportedStableVersions;
        }

        /// <summary>
        /// Get last stable Fabric loader infos
        /// </summary>
        /// <param name="mcVersion">Minecraft version</param>
        /// <returns>Object with loader and mappings infos</returns>
        /// <exception cref="VersionNotFoundException">Thown when Fabric has no version for Minecraft <paramref name="mcVersion"/></exception>
        public static FabricVersionDetailsEntryJson GetLastStableLoaderInfos(string mcVersion)
        {
            // Checking if minecraft version is supported by Fabric
            if(!GetAllSupportedVersions().Contains(mcVersion))
            {
                throw new VersionNotFoundException(String.Format("Minecraft {0} is not supported by Fabric.", mcVersion));
            }

            // If FabricLoaderInfoObject is null, we retrieve remote json 
            if (FabricLoaderInfoObject == null)
            {
                // If FabricLoaderInfoObject is null after that
                if (!RetrieveRemoteLoaderInfoJson(mcVersion))
                {
                    throw new Exception("Unable to retreive remote informations.");
                }
            }

            return FabricLoaderInfoObject[0];
        }

        /// <summary>
        /// Retrieve remote JSON with infos about Fabric loader for a specific Minecraft version
        /// </summary>
        private static bool RetrieveRemoteLoaderInfoJson(string mcVersion)
        {
            // Task to retrieve remote JSON
            Task t = Task.Factory.StartNew(() => RetrieveRemoteLoaderInfoJsonFunction(mcVersion));

            // Waiting for task end
            t.Wait();

            // Return true if json has been successfully retrieved, false else 
            return FabricLoaderInfoObject != null;
        }

        /// <summary>
        /// Function thread retrieve remote JSON with infos about Fabric loader for a specific Minecraft version
        /// </summary>
        private static void RetrieveRemoteLoaderInfoJsonFunction(string mcVersion)
        {
            using (WebClient client = new WebClient())
            {
                // Convert remote json to FabricVersionDetailsEntryJson object
                FabricLoaderInfoObject = JsonConvert.DeserializeObject<List<FabricVersionDetailsEntryJson>>(client.DownloadString(JsonGameLoaderInfoBaseUrl + mcVersion));
            }
        }

        /// <summary>
        /// Retrieve remote JSON with supported Minecraft versions by Fabric
        /// </summary>
        private static bool RetrieveRemoteSupportedVersionsJson()
        {
            // Task to retrieve remote JSON
            Task t = Task.Factory.StartNew(() => RetrieveRemoteSupportedVersionsJsonFunction());

            // Waiting for task end
            t.Wait();

            // Return true if json has been successfully retrieved, false else 
            return FabricGameVersionsJsonObject != null;
        }

        /// <summary>
        /// Function thread retrieve remote JSON with supported Minecraft versions by Fabric
        /// </summary>
        private static void RetrieveRemoteSupportedVersionsJsonFunction()
        {
            using (WebClient client = new WebClient())
            {
                // Convert remote json to FabricGameVersionsEntryJson object
                FabricGameVersionsJsonObject = JsonConvert.DeserializeObject<List<FabricGameVersionsEntryJson>>(client.DownloadString(Json_game_version_url));
            }
        }
    }
}
