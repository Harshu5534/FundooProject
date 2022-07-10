﻿using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface IUserRl
    {
        public UserEntity Registration(UserRegistration user);
        public string Login(UserLogin userLogin);
        public string ForgetPassword(string email);
        public bool ResetPassword(string email, string password, string confirmpassword);
    }
}
