using System.Threading.Tasks;
using Entities.Models;

namespace Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IPortraitRepository _portrait;
        private IUserRepository _user;

        public IPortraitRepository Portrait {
            get {
                if(_portrait == null)
                {
                    _portrait = new PortraitRepository(_repoContext);
                }

                return _portrait;
            }
        }

        public IUserRepository User {
            get {
                if(_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }

                return _user;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}