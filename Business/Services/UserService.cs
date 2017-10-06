using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using DAL.Interfaces;

namespace Business.Services
{
    public class UserService : IUserService
    {
        ISOContext db;

        public UserService() { }
        
        public UserService(ISOContext service)
        {
            db = service;
        }

        public void addUser(User user)
        {
            //db.Users.Add(user);
        }
    }
}
