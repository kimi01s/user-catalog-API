using UserCatalog.Model;

namespace UserCatalog.Data.repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UsersModel>> GetAllUsers();
        Task <UsersModel> FindUserById(int id);
        Task<bool> InsertUser(UsersModel user);
        Task<bool> UpdateUser(UsersModel user);
        Task<bool> DeleteUser(int id);
    }
}
