using TestApp.BO;
using TestApp.DTO;

namespace TestApp.ServiceContracts
{
    public interface IUserServices
    {
        public IEnumerable<UserDetails> GetAllUser();
        public int AddUser(UserDetailsDTO user);
        public bool UpdateUser(int Id, UserDetailsDTO user);
        public bool DeleteUser(int Id);
        string GetUserByName(string username);

    }
}
