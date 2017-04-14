using KWFCI.Controllers;
using KWFCI.Models;
using KWFCI.Tests.FakeRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace KWFCI.Tests
{
    public class BrokersTests
    {
        FakeBrokerRepository repo;
        BrokersController controller;
        List<Broker> brokers;

        public BrokersTests()
        {
            repo = new FakeBrokerRepository();
            controller = new BrokersController(repo);
            brokers = repo.GetAllBrokers().ToList();
        }

        [Fact]
        public void TestAddBrokers()
        {
            Broker bob = new Broker { FirstName = "Bob", LastName = "Helmet", Email = "bhelmet@kw.com", EmailNotifications = true, Type = "New Broker" };

            repo.AddBroker(bob);

            Assert.Contains(bob, repo.GetAllBrokers());
            Assert.Equal(repo.GetAllBrokers().Count(), 4);
        }
        [Fact]
        public void TestDeleteBrokers()
        {
            Broker bob = repo.GetAllBrokers().Where(b => b.BrokerID == 1) as Broker;

            repo.DeleteBroker(bob);

            Assert.DoesNotContain(bob, repo.GetAllBrokers());
        }
        [Fact]
        public void TestUpdateBrokers()
        {
            Broker b1 = repo.GetAllBrokers().First();

            Assert.Equal(b1.FirstName, "Lonny");
            Assert.Equal(b1.LastName, "Jenkins");

            b1.FirstName = "George";
            b1.LastName = "Harvey";
            repo.UpdateBroker(b1);

            Broker b2 = repo.GetAllBrokers().First(); //Demonstrating they go back into the same place in the list

            Assert.Equal(b2.FirstName, b1.FirstName);
            Assert.Equal(b2.LastName, b1.LastName);
        }
    }
}
