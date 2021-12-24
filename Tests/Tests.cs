using NUnit.Framework;
using PushShiftSharp;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void StandartTest()
        {
            PushShiftSearch search = new PushShiftSearch("meme");
            var list = search.Search();
            Assert.IsNotNull(list);
        }

        [Test]
        public void TitleTest()
        {
            PushShiftSearch search = new PushShiftSearch("woweconomy");
            var list = search
                .Title("Weekly")
                .Search();
            Assert.IsNotNull(list);
        }

        [Test]
        public void LimitTest()
        {
            PushShiftSearch search = new PushShiftSearch("woweconomy");
            var list = search
                .Limit(5)
                .Search();
            Assert.IsTrue(list.Count == 5);
        }

        [Test]
        public void ContainsVideoTest()
        {
            PushShiftSearch search = new PushShiftSearch("videos");
            var list = search
                .AvoidDeleted(true)
                .Search();
            Assert.IsTrue(list.Any(i => i.post_hint.Contains("video")));
        }

        [Test]
        public void AvoidVideoTest()
        {
            PushShiftSearch search = new PushShiftSearch("videos");
            var list = search
                .AvoidDeleted(true)
                .AvoidVideos(true)
                .Search();
            Assert.IsTrue(list.Count == 0);
        }

        [Test]
        public void GeneralTest()
        {
            PushShiftSearch search = new PushShiftSearch("woweconomy");
            var list = search
                .AvoidURL(true)
                .AvoidDeleted(true)
                .Search();
            Assert.IsNotNull(list);
        }
    }
}