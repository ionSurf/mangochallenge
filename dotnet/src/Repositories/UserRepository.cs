using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Models;


namespace Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<User> GetAllUsers()
        {
            return FindAll()
                .OrderBy(user => user.UserName)
                .ToList();
        }
        public User GetUserById(Guid userId)
        {
            return FindByCondition(user => user.Id.Equals(userId))
            .FirstOrDefault();
        }
        public User GetUserByUserName(String UserName)
        {
            return FindByCondition(user => user.UserName.Equals(UserName))
            .FirstOrDefault();
        }
        public void CreateUser(User user)
        {
            // Cannot have duplicates
            Create(user);
        }
        public void UpdateUser(User user)
        {   
            // Cannot have duplicates
            Update(user);
        }
        public void DeleteUser(User user)
        {
            Delete(user);
        }
    }
}