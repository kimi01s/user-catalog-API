using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCatalog.Model;

namespace UserCatalog.Data.repositories
{
    public class UserRepository : IUserRepository

    {
        private readonly MySQLConfiguration _connectionString;

        public UserRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;    
        }

        protected MySqlConnection dbConnection() {
            return new MySqlConnection(_connectionString.ConnectionString);
        }
   
        public async Task<IEnumerable<UsersModel>> GetAllUsers()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM users";

            return await db.QueryAsync<UsersModel>(sql, new { });
        }
        public async Task<bool>InsertUser(UsersModel user)
        {
            var db = dbConnection();
            var sql = @"INSERT INTO users(username, password, createdat, updatedat)
                      VALUES(@Username, @Password, @CreatedAt, @UpdatedAt)";

            var result = await db.ExecuteAsync(sql, new { 
                user.Username, 
                user.Password, 
                user.CreatedAt, 
                user.UpdatedAt
            });

            return result > 0;
        }
        public async Task<UsersModel> FindUserById(int id) {

            var db = dbConnection();
            var sql = @"SELECT id, username, updatedat FROM users WHERE id = @Id";

            return await db.QueryFirstOrDefaultAsync<UsersModel>(sql, new { Id = id });
        }
        public async Task<bool> UpdateUser(UsersModel user)
        {
            DateTime date = DateTime.Now;
            user.UpdatedAt = date;
            var db = dbConnection();
            var sql = @"UPDATE users SET username = @Username, password = @Password, 
                      createdat = @CreatedAt, updatedat = @UpdatedAt WHERE id = @Id";

            var result = await db.ExecuteAsync(sql, new
            {
                user.Id,
                user.Username,
                user.Password,
                user.CreatedAt,
                user.UpdatedAt
            });

            return result > 0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM users WHERE id=@Id";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

    }
}
