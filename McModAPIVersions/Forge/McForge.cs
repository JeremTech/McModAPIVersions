using McModAPIVersions.Forge;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace McModAPIVersions
{
    /// <summary>
    /// Get Minecraft Forge versions
    /// </summary>
    public class McForge
    {
        /// <summary>
        /// URL to Forge's promoted versions JSON
        /// </summary>
        private static readonly string Promotions_url = "https://files.minecraftforge.net/maven/net/minecraftforge/forge/promotions_slim.json";

        /// <summary>
        /// URL to Forge's versions JSON
        /// </summary>
        private static readonly string Versions_url = "https://files.minecraftforge.net/maven/net/minecraftforge/forge/maven-metadata.json";

        /// <summary>
        /// Forge's promoted versions JSON object
        /// </summary>
        private static ForgePromotionsJson ForgePromotionsJsonObject = null;

        /// <summary>
        /// Forge's versions JSON object
        /// </summary>
        private static ForgeVersionsJson ForgeVersionsJsonObject = null;

        /// <summary>
        /// Get the latest version of forge for a specific Minecraft version
        /// </summary>
        /// <param name="mcVersion">Minecraft version</param>
        /// <returns>Latest Forge version for <paramref name="mcVersion"/> Minecraft version</returns>
        /// <exception cref="Exception">Thrown when remote informations can't be gathered</exception>
        /// <exception cref="VersionNotFoundException">Thrown when no latest version has been found</exception>
        public static string GetLatestVersion(string mcVersion)
        {
            // If ForgeVersionJsonObject is null, we retrieve remote json 
            if(ForgePromotionsJsonObject == null)
            {
                // If ForgeVersionJsonObject is null after that
                if (!RetrieveRemotePromotionsJson())
                {
                    throw new Exception("Unable to retreive remote informations.");
                }
            }

            // Get the latest forge version for Minecraft <mcVersion>
            try
            {
                return ForgePromotionsJsonObject.GetLatestVersion(mcVersion);
            }
            catch(VersionNotFoundException e)
            {
                throw new VersionNotFoundException(e.Message);
            }
        }

        /// <summary>
        /// Get the recommended version of forge for a specific Minecraft version
        /// </summary>
        /// <param name="mcVersion">Minecraft version</param>
        /// <returns>Recommended Forge version for <paramref name="mcVersion"/> Minecraft version</returns>
        /// <exception cref="Exception">Thrown when remote informations can't be gathered</exception>
        /// <exception cref="VersionNotFoundException">Thrown when no recommended version has been found</exception>
        public static string GetRecommendedVersion(string mcVersion)
        {
            // If ForgeVersionJsonObject is null, we retrieve remote json 
            if (ForgePromotionsJsonObject == null)
            {
                // If ForgeVersionJsonObject is null after that
                if (!RetrieveRemotePromotionsJson())
                {
                    throw new Exception("Unable to retreive remote informations.");
                }
            }

            // Get the recommended forge version for Minecraft <mcVersion>
            try
            {
                return ForgePromotionsJsonObject.GetRecommendedVersion(mcVersion);
            }
            catch (VersionNotFoundException e)
            {
                throw new VersionNotFoundException(e.Message);
            }
        }

        /// <summary>
        /// Get all available versions of forge for a specific Minecraft version
        /// </summary>
        /// <param name="mcVersion">Minecraft version</param>
        /// <returns>All available Forge versions for <paramref name="mcVersion"/> Minecraft version</returns>
        /// <exception cref="Exception">Thrown when remote informations can't be gathered</exception>
        /// <exception cref="VersionNotFoundException">Thrown when no available versions have been found</exception>
        public static List<String> GetAvailableVersions(string mcVersion)
        {
            // If ForgeVersionJsonObject is null, we retrieve remote json 
            if (ForgeVersionsJsonObject == null)
            {
                // If ForgeVersionJsonObject is null after that
                if (!RetrieveRemoteVersionsJson())
                {
                    throw new Exception("Unable to retreive remote informations.");
                }
            }

            // Get the recommended forge version for Minecraft <mcVersion>
            try
            {
                return ForgeVersionsJsonObject.GetForgeVersions(mcVersion);
            }
            catch (VersionNotFoundException e)
            {
                throw new VersionNotFoundException(e.Message);
            }
        }

        /// <summary>
        /// Return download link to the targetted Minecraft Forge MDK 
        /// </summary>
        /// <param name="mcVersion">Minecraft version</param>
        /// <param name="forgeVersion">Minecraft Forge version</param>
        /// <example>To download Minecraft Forge MDK version 35.1.37 for Minecraft 1.16.4, calling the function like this : <code>GetMDKDownloadLink("1.16.4", "35.1.37");</code></example>
        /// <returns>Download link to the targetted Minecraft Forge MDK</returns>
        /// <exception cref="VersionNotFoundException">Thrown when no download link has been found</exception>
        public static string GetMDKDownloadLink(string mcVersion, string forgeVersion)
        {
            // Build the mdk download link 
            string link = string.Format("https://files.minecraftforge.net/maven/net/minecraftforge/forge/" + mcVersion + "-" + forgeVersion + "/forge-" + mcVersion + "-" + forgeVersion + "-mdk.zip");

            // Check if url is a correct download link
            WebRequest request = WebRequest.Create(new Uri(link));
            WebResponse response;
            request.Timeout = 7000;

            try
            {
                response = request.GetResponse();
            }
            catch (Exception)
            {
                throw new VersionNotFoundException(string.Format("MDK Download link for forge {0} unreachable.", forgeVersion));
            }

            return link;
        }

        /// <summary>
        /// Retrieve remote JSON with promoted Forge versions
        /// </summary>
        private static bool RetrieveRemotePromotionsJson()
        {
            // Task to retrieve remote JSON
            Task t = Task.Factory.StartNew(() => RetrieveRemotePromotionsJsonFunction());

            // Waiting for task end
            t.Wait();

            // Return true if json has been successfully retrieved, false else 
            return ForgePromotionsJsonObject != null;
        }

        /// <summary>
        /// Function thread retrieve remote JSON with promoted Forge versions
        /// </summary>
        private static void RetrieveRemotePromotionsJsonFunction()
        {
            using (WebClient client = new WebClient())
            {
                // Convert remote json to ForgePromotionsJson object
                ForgePromotionsJsonObject = JsonConvert.DeserializeObject<ForgePromotionsJson>(client.DownloadString(Promotions_url));
            }
        }

        /// <summary>
        /// Retrieve remote JSON with Forge versions
        /// </summary>
        private static bool RetrieveRemoteVersionsJson()
        {
            // Task to retrieve remote JSON
            Task t = Task.Factory.StartNew(() => RetrieveRemoteVersionsJsonFunction());

            // Waiting for task end
            t.Wait();

            // Return true if json has been successfully retrieved, false else 
            return ForgeVersionsJsonObject != null;
        }

        /// <summary>
        /// Function thread retrieve remote JSON with promoted Forge versions
        /// </summary>
        private static void RetrieveRemoteVersionsJsonFunction()
        {
            using (WebClient client = new WebClient())
            {
                // Convert remote json to ForgeVersionsJson object
                ForgeVersionsJsonObject = JsonConvert.DeserializeObject<ForgeVersionsJson>(client.DownloadString(Versions_url));
            }
        }
    }
}
