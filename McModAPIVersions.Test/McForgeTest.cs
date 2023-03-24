using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace McModAPIVersions.Test
{
    [TestClass]
    public class McForgeTest
    {
        [TestMethod]
        public void NoRecommendedVersionFoundTest()
        {
            // Forge for Minecraft 1.13 has no recommended version
            Assert.ThrowsException<VersionNotFoundException>(() => McForge.GetRecommendedVersion("1.13.2"));
        }

        [TestMethod]
        public void NoLatestVersionFoundTest()
        {
            // Forge for Minecraft 1.35 has no latest version
            Assert.ThrowsException<VersionNotFoundException>(() => McForge.GetLatestVersion("1.35"));
        }

        [TestMethod]
        public void RecommendedVersionFoundTest()
        {
            // Forge for Minecraft 1.18.2 has recommended version
            Assert.IsFalse(string.IsNullOrEmpty(McForge.GetRecommendedVersion("1.18.2")));
        }

        [TestMethod]
        public void LatestVersionFoundTest()
        {
            // Forge for Minecraft 1.19 has latest version
            Assert.IsNotNull(McForge.GetLatestVersion("1.19.4"));
        }

        [TestMethod]
        public void NoVersionsFoundTest()
        {
            // Forge for Minecraft 1.14.1 has no versions
            Assert.ThrowsException<VersionNotFoundException>(() => McForge.GetAvailableVersions("1.14.1"));
        }

        [TestMethod]
        public void VersionsFoundTest()
        {
            // Forge for Minecraft 1.15 has 5 versions
            Assert.AreEqual<int>(5, McForge.GetAvailableVersions("1.15").Count);
        }

        [TestMethod]
        public void MDKLinkNotFound()
        {
            // 1.18-31.2.5 version not exist
            Assert.ThrowsException<VersionNotFoundException>(() => McForge.GetMDKDownloadLink("1.18", "31.2.5"));
        }

        [TestMethod]
        public void MDKLinkFound()
        {
            // 35.1.37 version exist
            Assert.IsNotNull(McForge.GetMDKDownloadLink("1.16.4", "35.1.37"));
        }

        [TestMethod]
        public void InstallerLinkNotFound()
        {
            // 1.18-31.2.5 version not exist
            Assert.ThrowsException<VersionNotFoundException>(() => McForge.GetInstallerDownloadLink("1.18", "31.2.5"));
        }

        [TestMethod]
        public void InstallerLinkFound()
        {
            // 35.1.37 version exist
            Assert.IsNotNull(McForge.GetInstallerDownloadLink("1.16.4", "35.1.37"));
        }
    }
}
