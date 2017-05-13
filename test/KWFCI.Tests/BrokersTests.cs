using KWFCI.Controllers;
using KWFCI.Models;
using KWFCI.Tests.FakeRepositories;
using Microsoft.AspNetCore.Mvc;
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

        //public BrokersTests()
        //{
        //    repo = new FakeBrokerRepository();
        //    controller = new BrokersController(repo,null);
        //    brokers = repo.GetAllBrokers().ToList();
        //}

        //[Fact]
        //public void TestAddBrokers()
        //{
        //    Broker bob = new Broker { FirstName = "Bob", LastName = "Helmet", Email = "bhelmet@kw.com", EmailNotifications = true, Type = "New Broker" };

        //    repo.AddBroker(bob);

        //    Assert.Contains(bob, repo.GetAllBrokers());
        //    Assert.Equal(repo.GetAllBrokers().Count(), 4);
        //}
        //[Fact]
        //public void TestDeleteBrokers()
        //{
        //    Broker bob = repo.GetAllBrokers().Where(b => b.BrokerID == 1) as Broker;

        //    repo.DeleteBroker(bob);

        //    Assert.DoesNotContain(bob, repo.GetAllBrokers());
        //}
        //[Fact]
        //public void TestUpdateBrokers()
        //{
        //    Broker b1 = repo.GetAllBrokers().First();

        //    Assert.Equal(b1.FirstName, "Lonny");
        //    Assert.Equal(b1.LastName, "Jenkins");

        //    b1.FirstName = "George";
        //    b1.LastName = "Harvey";
        //    repo.UpdateBroker(b1);

        //    Broker b2 = repo.GetAllBrokers().First(); //Demonstrating they go back into the same place in the list

        //    Assert.Equal(b2.FirstName, b1.FirstName);
        //    Assert.Equal(b2.LastName, b1.LastName);
        //}
        //[Fact]
        //public void TestControllerGetAllBrokers()
        //{
        //    //Get the base object
        //    var result = controller.AllBrokers(); 

        //    //Make sure it is returning as a ViewResult
        //    var viewResult = Assert.IsType<ViewResult>(result); 

        //    //Make sure the model is a List of brokers
        //    var model = Assert.IsType<List<Broker>>(viewResult.ViewData.Model); 

        //    //Make sure my returned model and the repos GetAllBrokers() method return same number
        //    Assert.Equal(model.Count(), repo.GetAllBrokers().Count());
        //}
        //[Fact]
        //public void TestControllerDelete()
        //{
        //    Assert.Equal(repo.GetAllBrokers().Count(), 3);
        //    //Get the base object
        //    var brokerID = repo.GetAllBrokers().First().BrokerID;
        //    var result = controller.Archive(brokerID);

        //    //Make sure it is returning as a RedirectToActionResult
        //    var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

        //    Assert.Equal("Index", redirectToActionResult.ActionName);
        //    Assert.Equal(repo.GetAllBrokers().Count(), 2);
        //}

        //[Fact]
        //public void TestControllerAddBroker()
        //{
        //    //Ensure there are only 3 brokers to start
        //    Assert.Equal(repo.GetAllBrokers().Count(), 3);
        //    //Get a broker
        //    var broker = repo.GetAllBrokers().First();
        //    //Get the base object
        //    var result = controller.AddBroker(broker, "");

        //    //Make sure it is returning as a IActionResult
        //    var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

        //    Assert.Equal("Home", redirectToActionResult.ControllerName);
        //    Assert.Equal("Index", redirectToActionResult.ActionName);
        //    //Ensure the broker was actually added
        //    Assert.Equal(repo.GetAllBrokers().Count(), 4);
        //}

        //[Fact]
        //public void TestControllerGETEditBroker()
        //{
        //    //Make sure I have a real broker and I know who it is
        //    var broker = repo.GetAllBrokers().First();
        //    Assert.Equal("Lonny", broker.FirstName);
        //    Assert.Equal("Jenkins", broker.LastName);

        //    //If we have a valid ID, we should get a ViewResult returned
        //    var result1 = controller.Edit(broker.BrokerID);
        //    var viewResult = Assert.IsType<ViewResult>(result1);

        //    //If we have an invalid ID, we should get a RedirectToAction returned
        //    var result2 = controller.Edit(999);
        //    var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result2);

        //    //Lastly, make sure Index is where the Redirect is going
        //    Assert.Equal("Index", redirectToActionResult.ActionName);
        //}

        //[Fact]
        //public void TestControllerPOSTEditBroker()
        //{
        //    //Make sure I have a real broker and I know who it is
        //    var broker = repo.GetAllBrokers().First();
        //    Assert.Equal("Lonny", broker.FirstName);
        //    Assert.Equal("Jenkins", broker.LastName);

        //    broker.FirstName = "Bob";
        //    broker.LastName = "Drescher";

        //    //If we have a valid ID, we should get a RedirectToActionResult returned
        //    var result1 = controller.Edit(broker);
        //    var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result1);

        //    Assert.Equal(repo.GetAllBrokers().First().FirstName, "Bob");
        //    Assert.Equal(repo.GetAllBrokers().First().LastName, "Drescher");
        //    Assert.Equal("Index", redirectToActionResult.ActionName);

        //    //But if we don't have a real broker, we should get an error
        //    var result2 = controller.Edit(null);

        //    Assert.IsType<ViewResult>(result2);
        //}
    }
}
