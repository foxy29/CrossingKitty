﻿using CrossingKitty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossingKitty.DataAccess
{
    public interface IUserRepository
    {
        public Task<User> Login(string username, string password);
        public Task Register(string username, string password);
    }
}
