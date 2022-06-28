﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.Api.Controllers
{
    
    public class UsersController : BaseEntitiesController<User>
    {
        IUserService _baseService;
        public UsersController(IUserService baseService) : base(baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// api login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] User user)
        {
            try
            {
                var res = _baseService.Login(user.UserName, user.Password);
                if (res.MISACode == Core.Enums.MISAEnum.MISACode.Success)
                {
                    // lưu thông tin user vào session
                    string userInfo = JsonSerializer.Serialize(res.Data);
                    HttpContext.Session.SetString("user", userInfo);
                    // lưu quyền của user
                    User userTemp = JsonSerializer.Deserialize<User>(userInfo)!;
                    HttpContext.Session.SetString("permission", userTemp.lstPermission);

                    return Ok(HttpContext.Session.GetString("permission"));
                    //return Ok(Core.Enums.MISAEnum.MISACode.Success);
                }
                else
                {
                    return Ok(Core.Enums.MISAEnum.MISACode.NotValid);
                }
            }
            catch (Exception)
            {
                return Ok(Core.Enums.MISAEnum.MISACode.NotValid);
            }
            
        }

        /// <summary>
        /// api logout
        /// </summary>
        /// <returns></returns>
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            try
            {
                string userInfo = HttpContext.Session.GetString("user");
                if (!string.IsNullOrEmpty(userInfo))
                {
                    HttpContext.Session.Remove("user");
                    HttpContext.Session.Remove("permission");
                    return Ok(Core.Enums.MISAEnum.MISACode.Success);
                }
                else
                {
                    return Ok(Core.Enums.MISAEnum.MISACode.NotValid);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
       
        }

        [HttpGet("GetAllShipper")]
        public IActionResult GetAllShipper()
        {
            try
            {
                List<User> lstUser = _baseService.GetAllShipper();
                return Ok(lstUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
