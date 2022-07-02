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
        public string JwtMethod(string email, long id);
    }
}
