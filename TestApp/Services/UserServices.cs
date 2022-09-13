using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestApp.BO;
using TestApp.DataContext;
using TestApp.DTO;
using TestApp.ServiceContracts;

namespace TestApp.Services
{
    public class UserServices : IUserServices
    {

        private UserDbContext _context;
        public UserServices(UserDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserDetails> GetAllUser()
        {
            try
            {
                IQueryable<UserDetails> userDetails = _context.userDetails.OrderBy(p => p.UserId);
                return userDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddUser(UserDetailsDTO user)
        {
            try
            {
                var userBO = new UserDetails { UserName = user.UserName };
                _context.userDetails.Add(userBO);
                _context.SaveChanges();
                return userBO.UserId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateUser(int Id, UserDetailsDTO user)
        {
            try
            {
                var userData = GetUserById(Id);
                if (userData != null)
                {
                    userData.UserName = user.UserName;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DeleteUser(int Id)
        {
            try
            {
                var user = GetUserById(Id);
                if (user != null)
                {
                    _context.Entry(user).State = EntityState.Deleted;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetUserByName(string username)
        {
            try
            {
                var user = _context.userDetails.SingleOrDefault(p => p.UserName == username);
                if (user != null)
                    return user.UserName;
                else
                    return String.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private UserDetails GetUserById(int Id)
        {
            try
            {
                return _context.userDetails.SingleOrDefault(p => p.UserId == Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
