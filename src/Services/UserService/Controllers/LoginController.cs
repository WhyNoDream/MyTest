//using CommonConfBus;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using UserServiceContracts;
//using UserServiceContracts.Dto;

//namespace UserService.Controllers
//{
  
//    public class LoginController : BaseController
//    {
//        private readonly IUserLogin _iUserLogin;

//        public LoginController(IUserLogin userLogin)
//        {
//            _iUserLogin = userLogin;
//        }

//        [HttpGet]
//        public async Task<LoginDto> Login(string account, string password)
//        {
//           return await _iUserLogin.Login(account, password);
//        }
//    }
//}
