﻿using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Service
{
    public interface IUserService : IBaseService<User>
    {
        ServiceResult Login(string userName, string password);
        List<User> GetAllShipper();

    }
}
