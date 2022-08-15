using McModAPIVersions;
using McModAPIVersions.Fabric;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace McModAPIVersions.Test
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
            Assert.IsTrue(McFabric.GetAllSupportedVersions().Contains("1.18"));
        }

        [TestMethod]
        public void VersionInfosTest()
        {
            Assert.IsNotNull(McFabric.GetLastStableLoaderInfos("1.19.2"));
        }

        [TestMethod]
        public void VersionInfosErrorTest()
        {
            Assert.ThrowsException<VersionNotFoundException>(() => McFabric.GetLastStableLoaderInfos("22w08b"));
        }
    }
}
