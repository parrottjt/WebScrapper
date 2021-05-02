using NUnit.Framework;
using WebScrapper.DND.Data;

namespace WebScrapper.Test
{
    public class DndFeatureTests
    {
        Feature feature;
        [SetUp]
        public void Setup()
        {
            feature = new Feature();
        }

        [Test]
        public void Create_EmptyFeature()
        {
            Assert.NotNull(feature);
        }

        [Test]
        public void Add_FeatureLevel()
        {
            feature = new Feature(level: 1, "");
            Assert.AreEqual(1, feature.Level);
        }

        [Test]
        public void Add_FeatureInfo()
        {
            feature = new Feature(0, "This is a feature");
            Assert.IsNotEmpty(feature.Description);
        }

        [Test]
        public void Add_FeatureWithFullData()
        {
            feature = new Feature(1, "This is a feature");
            Assert.AreEqual(1,feature.Level);
            Assert.IsNotEmpty(feature.Description);
        }

        [Test]
        public void FeatureLevel_LessThan1_SetsLevelTo1()
        {
            feature = new Feature(0, "");
            Assert.AreNotEqual(0, feature.Level);
        }

        [Test]
        public void FeatureLevel_GreaterThan20_SetsLevelTo20()
        {
            feature = new Feature(21, "");
            Assert.AreEqual(20, feature.Level);
        }
    }
}