using System.Threading.Tasks;

namespace Repositories
{
    public interface IRepositoryWrapper
    {
        IPortraitRepository Portrait { get; }
        IUserRepository User { get; }
        void Save();
        Task SaveAsync();
    }
}