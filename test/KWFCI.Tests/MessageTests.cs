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
    public class MessageTests
    {
        FakeMessageRepository repo;
        MessagesController controller;
        List<Message> messages;

        //public MessageTests()
        //{
        //    repo = new FakeMessageRepository();
        //    controller = new MessagesController(repo);
        //    messages = repo.GetAllMessages().ToList();
        //}

        //[Fact]
        //public void TestAddMessage()
        //{
        //    Message message = new Message { Subject = "The Subject", Body = "Body of a message" };
        //    repo.AddMessage(message);

        //    Assert.Contains(message, repo.GetAllMessages());
        //    Assert.Equal(repo.GetAllMessages().Count(), 3);
        //}

        //[Fact]
        //public void TestControllerAddMessage()
        //{

        //    Assert.Equal(repo.GetAllMessages().Count(), 2);
        //    var message = repo.GetAllMessages().First();
        //    var result = controller.SendMessage(message);

        //    //Make sure it is returning as a IActionResult
        //    var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

        //    Assert.Equal("Messages", redirectToActionResult.ControllerName);
        //    Assert.Equal("AllMessages", redirectToActionResult.ActionName);
  
        //    Assert.Equal(repo.GetAllMessages().Count(), 3);
        //}

        //[Fact]
        //public void TestGetAllMessages()
        //{
        //    Assert.Equal(2, repo.GetAllMessages().Count());
        //}


        
        



 

    }
}
