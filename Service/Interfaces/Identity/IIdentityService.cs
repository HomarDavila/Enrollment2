using Common;
using Common.HttpHelpers;
using Common.Logging;
using Domain.Custom_Models;
using Domain.Entity_Models;
using Domain.Entity_Models.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IIdentityService
    {
        ApplicationUserManager _userManager { get; set; }
        bool ExistUser(string user);
        EResponseBase<Role> GetRolesByUserId(int userId, string applicationCode);
        EResponseBase<OptionRol> GetOptionsByUserId(int userId, string applicationCode);
        bool HavePermissions(string optionCode, int rolId, string applicationCode);
        Task<EResponseBase<SimpleEntity>> Register(User user);
        Task<EResponseBase<User>> ResetPassword(string userName, string newPassword);
        Task<EResponseBase<User>> ChangePassword(int userId, string oldPassword, string newPassword);
        Task<EResponseBase<User>> ChangePasswordExternal(string userName, string mpi, string newPassword);
        EResponseBase<SimpleEntity> UnlockUser(int userId);
        EResponseBase<User> UpdatePhone(int userId, string phoneNumber);
        EResponseBase<User> UpdateEmail(int userId, string email);
        EResponseBase<User> UpdatePersonalData(int userId, string email, string phoneNumber, string name, string firstLastName, string secondLastName, string ssnLast4, bool isAdminitrador);
        Task<User> Find(string userName, string password);
        Task<User> Login(string userName, string password);
        Task<ClaimsIdentity> GenerateUserIdentityAsync(User user, string authenticationType);
        ICustomLog Logger { get; set; }
        ITransaction Transaction { get; set; }
    }
}
