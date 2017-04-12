﻿using KWFCI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Repositories
{
    public interface IBrokerRepository
    {
        IQueryable<Broker> GetAllBrokers();
        //int return value represents whether or not operation completed: 1 for True, 0 for False
        int DeleteBroker(Broker broker);
        int AddBroker(Broker broker);
        int UpdateBroker(Broker broker);
        IQueryable<Broker> GetBrokersByType(string type);
        Broker GetBrokerByID(int id);
    }
}