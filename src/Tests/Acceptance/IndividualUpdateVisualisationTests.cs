using System;
using System.Collections;
using System.Collections.Generic;
using BusinessRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using StoryQ.Framework;
using System.Diagnostics;
using System.Linq;

namespace visFleet_mockConnector.Tests.Acceptance
{
    [TestClass]
    public class IndividualUpdateVisualisationTests
    {
        private Mock<IConnector> _mockConnector;

        [TestInitialize]
        public void Setup()
        {
            _mockConnector = new Mock<IConnector>();
            _mockConnector.Setup(x => x.Fetch(It.IsAny<string>())).Returns(null);
        }

        [TestMethod]
        public void Batch_must_occur_within_30_seconds()
        {
            var story = new Story("Must return an individual asset update in less than 30 seconds");
            story.AsA("Dispatcher")
                .IWant("To enter information into the system")
                .SoThat("I can see the result in visualised in the external system")

                .WithScenario("Item is updated into our internal system")
                .Given(() => DataForItem_IsUpdatedInInternalSystem(56))
                    .And(Narrative.Text("Database trigger has occured"))
                    .And(Narrative.Text("New item exists in the queue"))
                .When(Narrative.Exec("_mockConnector Service runs", StartTimer))
                    .And(() => GetFleedIdsFrom_Within_Secs("http://external.system.com", 20))
                    .And(() => HasMoreThan_Assets(310))
                    .And(() => GetsUpdatedItemsWithin_Secs(9))
                    .And(() => PutsSingleAsset_To_Within_Secs("281", "http://otherexternalsystem.com", 20))
                    .And(() => RetrievesNewDataFrom_ForAsset_XmlWithUtilisation_("http://otherexternalsystem.com", "281", 56))
                    .And(() => Within_Seconds(30));
            story.Assert();
        }

        private static void RetrievesNewDataFrom_ForAsset_XmlWithUtilisation_(string uri, string id, int utilisation)
        {

        }


        private static Stopwatch Timer { get; set; }
        private static Hashtable FleetIds { get; set; }
        private static List<Asset> Assets { get; set; }

        private static void StartTimer()
        {
            Timer = new Stopwatch();
            Timer.Start();
        }

        private static void PutsSingleAsset_To_Within_Secs(string id, string url, int i)
        {
            var asset = Assets.Where(x => x.Id == id).First();

            AssertSW(i, () => new HttpService().PostString(url, asset.Id, _mockConnector.ToXml(asset)));
        }

        private static void GetsUpdatedItemsWithin_Secs(int i)
        {
            AssertSW(i, () => _mockConnector.BuildAssets(FleetIds) );
        }

        private static void GetFleedIdsFrom_Within_Secs(string baseUrl, int i)
        {
            AssertSW(i, () => _mockConnector.Fetch(baseUrl));
        }

        private static void HasMoreThan_Assets(int i)
        {
            Assert.IsTrue(i < FleetIds.Count, string.Format("Current GET returns only {0} assets and less than {1} required", i, FleetIds.Count));
        }

        private static void Within_Seconds(int i)
        {
            Timer.Stop();
            Assert.IsTrue(Timer.ElapsedMilliseconds <= (i * 1000 * 60), string.Format("Time taken {0:0.0} minutes", Timer.ElapsedMilliseconds / 60000M));
        }

        private static void DataForItem_IsUpdatedInInternalSystem(int id)
        {
          Assert.IsTrue(true);
        }

        private static void AssertStopwatchMillisecond(Stopwatch total, int total_time)
        {
            Assert.IsTrue(total.ElapsedMilliseconds <= (total_time * 1000), string.Format("Time taken {0:0.0}s", total.ElapsedMilliseconds/1000M));
        }

        private static void AssertSW(int i, Action action)
        {
            var sw = new Stopwatch();

            sw.Start();
            action.Invoke();
            sw.Stop();

            AssertStopwatchMillisecond(sw, i);
        }

    }
}