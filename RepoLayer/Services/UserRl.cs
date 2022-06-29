using CommonLayer.Model;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Services
{
    public class UserRl:IUserRl
    {
        FundooContext fundooContext;
        public UserRl(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public UserEntity Registration(UserRegistration user)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.Email = user.Email;
                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;
                userEntity.Password=user.Password;
                fundooContext.Users.Add(userEntity);
                int result=fundooContext.SaveChanges();
                if (result > 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
