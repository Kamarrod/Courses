//using Courses.DAL.Interfaces;
//using Courses.Domain.Entity;
//using Courses.Domain.Enum;
//using Courses.Domain.Helpers;
//using Courses.Domain.Response;
//using Courses.Domain.ViewModules.Account;
//using Courses.Service.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Security;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace Courses.Service.Implementations
//{
//    public class AccountService : IAccountService
//    {
//        //private readonly IBaseRepository<Profile> _proFileRepository;
//        private readonly IBaseRepository<User> _userRepository;

//        public AccountService(IBaseRepository<User> userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
//        {
//            try
//            {
//                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
//                if (user == null)
//                {
//                    return new BaseResponse<ClaimsIdentity>()
//                    {
//                        Description = "Пользователь с таким логином не существует"
//                    };
//                }

//                if (user.Password != HashPasswordHelper.HashPassword(model.Password))
//                {
//                    return new BaseResponse<ClaimsIdentity>()
//                    {
//                        Description = "Неверный пароль или логин"
//                    };
//                }

//                var result = Authenticate(user);
//                return new BaseResponse<ClaimsIdentity>()
//                {
//                    Data = result,
//                    StatusCode = StatusCode.OK
//                };
//            }
//            catch (Exception ex)
//            {
//                return new BaseResponse<ClaimsIdentity>()
//                {
//                    Description = $"[LoginUser] : {ex.Message}",
//                    StatusCode = Domain.Enum.StatusCode.InternalStatusError
//                };
//            }
//        }

//        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
//        {
//            try
//            {
//                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
//                if (user != null)
//                {
//                    return new BaseResponse<ClaimsIdentity>()
//                    {
//                        Description = "Пользователь с таким логином уже существует"
//                    };
//                }

//                user = new User()
//                {
//                    Name = model.Name,
//                    Role = Domain.Enum.Role.User,
//                    Password = HashPasswordHelper.HashPassword(model.Password),
//                };

//                await _userRepository.Create(user);
//                var result = Authenticate(user);

//                return new BaseResponse<ClaimsIdentity>()
//                {
//                    Data = result,
//                    Description = "Пользователь добавлен",
//                    StatusCode = Domain.Enum.StatusCode.OK
//                };

//            }
//            catch (Exception ex)
//            {
//                return new BaseResponse<ClaimsIdentity>()
//                {
//                    Description = $"[RegisterUser] : {ex.Message}",
//                    StatusCode = Domain.Enum.StatusCode.InternalStatusError
//                };
//            }
//        }

//        private ClaimsIdentity Authenticate(User user)
//        {
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
//                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
//            };
//            return new ClaimsIdentity(claims, "ApplicationCookie",
//                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
//        }
//    }
//}
