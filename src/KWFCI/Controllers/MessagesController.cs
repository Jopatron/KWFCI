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
        private IKWTaskRepository taskRepo;

        public MessagesController(IMessageRepository repo, IBrokerRepository bRepo, IStaffProfileRepository sRepo, IKWTaskRepository repo2)
        {
            messageRepo = repo;
            brokerRepo = bRepo;
            staffRepo = sRepo;
            taskRepo = repo2;
        }



        public ViewResult AllMessages()
        {
            /*Call the GetToday method of the helper class to get and set today's date for use with datepicker validation*/
            Helper.GetToday();

            var criticalVB = taskRepo.GetAllTasksByType("Alert").Where(t => t.Priority == 5).ToList();

            foreach (KWTask task in taskRepo.GetAllTasksByType("Task").Where(t => t.Priority == 5).ToList())
            {
                criticalVB.Add(task);
            }

            ViewBag.Critical = criticalVB;
            var vm = new MessageVM();
            var allMessages = messageRepo.GetAllMessages().ToList();

            vm.Messages = allMessages;
            vm.NewMessage = new Message();
            return View(vm);
        }
        [Route("Add")]
        [HttpPost]
        public IActionResult SendMessage(Message m, string returnURL, bool checkAll = false, bool checkAllBrokers = false, bool checkStaff = false,
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
                brokers = brokerRepo.GetAllBrokers(false, true);
                staff = staffRepo.GetAllStaffProfiles(true);

            }
            else
            {
                if (checkAllBrokers == true)

                    brokers = brokerRepo.GetAllBrokers(false, true);
                else
                {

                    if (checkNewBrokers == true)
                    {
                        if (brokers != null)
                        {
                            var newBrokers = brokerRepo.GetBrokersByType("New Broker", true);
                            foreach (var b in newBrokers)
                                brokers.Append(b);
                        }
                        else
                            brokers = brokerRepo.GetBrokersByType("New Broker", true);
                    }

                    if (checkBrokersInTransition == true)
                    {
                        if (brokers != null)
                        {
                            var transitionBrokers = brokerRepo.GetBrokersByType("In Transition", true);
                            foreach (var b in transitionBrokers)
                                brokers.Append(b);
                        }
                        else
                            brokers = brokerRepo.GetBrokersByType("In Transition", true);
                    }
                    if (checkTransferBrokers == true)
                    {
                        if (brokers != null)
                        {
                            var transferBrokers = brokerRepo.GetBrokersByType("Transfer", true);
                            foreach (var b in transferBrokers)
                                brokers.Append(b);
                        }
                        else
                            brokers = brokerRepo.GetBrokersByType("Transfer", true);
                    }
                }
                if (checkStaff == true)
                    staff = staffRepo.GetAllStaffProfiles(true);


            }
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("KWFCI", "do-not-reply@kw.com"));
            email.Subject = message.Subject;
            email.Body = new TextPart("plain")
            {
                Text = message.Body
            };
            if (brokers != null)
            {

                foreach (var b in brokers)
                {

                    email.Bcc.Add(new MailboxAddress(b.FirstName + " " + b.LastName, b.Email));

                }
            }

            if (staff != null)
            {
                foreach (var st in staff)
                    email.Bcc.Add(new MailboxAddress(st.FirstName + " " + st.LastName, st.Email));

            }
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.gmail.com", 587, false);

                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("kwfamilycheckin", "Fancy123!");

                client.Send(email);
                client.Disconnect(true);
            }




            return Redirect(returnURL);
        }
    }
}
