using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBl:IUserBl
    {
        IUserRl userRl;
        public UserBl(IUserRl userRl)
        {
            this.userRl = userRl;
        }
        public UserEntity Registration(UserRegistration user)
        {
            try
            {
                return userRl.Registration(user);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
