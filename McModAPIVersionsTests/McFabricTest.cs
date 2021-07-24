using McModAPIVersions;
using McModAPIVersions.Fabric;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace McModAPIVersionsTests
{
    [TestClass]
    public class McFabricTest
    {
        [TestMethod]
        public void GetAllSupportedVersionsTest()
        {
            Assert.IsNotNull(McFabric.GetAllSupportedVersions());
        }

        [TestMethod]
        public void GetAllStableSupportedVersionsTest()
        {
            Assert.IsNotNull(McFabric.GetAllSupportedStableVersions());
        }

        [TestMethod]
        public void VersionExistTest()
        {
            Assert.IsTrue(McFabric.GetAllSupportedVersions().Contains("1.17.1"));
        }

        [TestMethod]
        public void VersionInfosTest()
        {
            Assert.IsNotNull(McFabric.GetLastStableLoaderInfos("1.17.1"));
        }

        [TestMethod]
        public void VersionInfosErrorTest()
        {
            Assert.ThrowsException<VersionNotFoundException>(() => McFabric.GetLastStableLoaderInfos("22w08b"));
        }
    }
}
