using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KWFCI.Repositories;
using KWFCI.Models.ViewModels;
using KWFCI.Models;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

//TODO: Change all the repos to grab only staff and brokers who have true on email notification
// Possible solution - add a repo method to grab those objects
namespace KWFCI.Controllers
{
    [Authorize(Roles = "Staff")]
    [Route("Messages")]
    public class MessagesController : Controller
    {
        private IMessageRepository messageRepo;
        private IBrokerRepository brokerRepo;
        private IStaffProfileRepository staffRepo;
        private IEnumerable<Broker> brokers;
        private IQueryable<StaffProfile> staff;

        public MessagesController(IMessageRepository repo, IBrokerRepository bRepo, IStaffProfileRepository sRepo)
        {
            messageRepo = repo;
            brokerRepo = bRepo;
            staffRepo = sRepo;
        }

       

        public ViewResult AllMessages()
        {
            var vm = new MessageVM();
            var allMessages = messageRepo.GetAllMessages().ToList();

            vm.Messages = allMessages;
            vm.NewMessage = new Message();
            return View(vm);
        }
        [Route("Add")]
        [HttpPost]
        public IActionResult SendMessage(Message m, bool checkAll = false, bool checkAllBrokers = false, bool checkStaff = false,
            bool checkNewBrokers = false, bool checkBrokersInTransition = false, bool checkTransferBrokers = false)
        {

            var message = new Message
            {
                Subject = m.Subject,
                Body = m.Body,
                DateSent = DateTime.Now,
                
        };


        messageRepo.AddMessage(message);

            if (checkAll == true)
            {
                brokers = brokerRepo.GetAllBrokers();
                staff = staffRepo.GetAllStaffProfiles();

            }
            else
            {
                if (checkAllBrokers == true)

                    brokers = brokerRepo.GetAllBrokers();
                else
                {

                    if (checkNewBrokers == true)
                    {
                        if (brokers != null)
                        {
                            var newBrokers = brokerRepo.GetBrokersByType("New Broker");
                            foreach (var b in newBrokers)
                                brokers.Append(b);
                        }
                        else
                            brokers = brokerRepo.GetBrokersByType("New Broker");
                    }

                    if (checkBrokersInTransition == true)
                    {
                        if (brokers != null)
                        {
                            var transitionBrokers = brokerRepo.GetBrokersByType("In Transition");
                            foreach (var b in transitionBrokers)
                                brokers.Append(b);
                        }
                        else 
                            brokers = brokerRepo.GetBrokersByType("In Transition");
                    }
                    if (checkTransferBrokers == true)
                    {
                        if (brokers != null)
                        {
                            var transferBrokers = brokerRepo.GetBrokersByType("Transfer");
                            foreach (var b in transferBrokers)
                                brokers.Append(b);
                        }
                        else
                            brokers = brokerRepo.GetBrokersByType("Transfer");
                    }
                }
                    if (checkStaff == true)
                        staff = staffRepo.GetAllStaffProfiles();
                
                
            }
            if (brokers != null)
            {
                foreach (var b in brokers)
                {
                    var email = new MimeMessage();
                    email.From.Add(new MailboxAddress("KWFCI", "do-not-reply@kw.com"));
                    email.To.Add(new MailboxAddress(b.FirstName + " " + b.LastName, b.Email));
                    email.Subject = message.Subject;

                    email.Body = new TextPart("plain")
                    {
                        Text = message.Body
                    };

                    using (var client = new SmtpClient())
                    {
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                        client.Connect("smtp.gmail.com", 587, false);

                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        client.Authenticate("kwfamilycheckin", "Fancy123!");

                        client.Send(email);
                        client.Disconnect(true);
                    }
                }
            }

            if (staff != null)
            {

                foreach (var st in staff)
                {
                    var email = new MimeMessage();
                    email.From.Add(new MailboxAddress("KWFCI", "do-not-reply@kw.com"));
                    email.To.Add(new MailboxAddress(st.FirstName + " " + st.LastName, st.Email));
                    email.Subject = message.Subject;

                    email.Body = new TextPart("plain")
                    {
                        Text = message.Body
                    };

                    using (var client = new SmtpClient())
                    {
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                        client.Connect("smtp.gmail.com", 587, false);

                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        client.Authenticate("kwfamilycheckin", "Fancy123!");

                        client.Send(email);
                        client.Disconnect(true);
                    }
                }
            }
        

            return RedirectToAction("AllMessages", "Messages");
        }
    }
}
