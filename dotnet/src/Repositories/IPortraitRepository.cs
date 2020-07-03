using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Repositories
{
    public interface IPortraitRepository : IRepositoryBase<Portrait>
    {
        /** Async methods **/
        Task<IEnumerable<Portrait>> GetAllPortraitsAsync();
        Task<Portrait> GetPortraitByIdAsync( Guid portraitId );
        /** Non-Async methods **/
        IEnumerable<Portrait> GetAllPortraits();
        Portrait GetPortraitById(Guid portraitId);
        /** CRUD - Async non-necessary **/
        void CreatePortrait(Portrait portrait);
        void UpdatePortrait(Portrait portrait);
        void DeletePortrait(Portrait portrait);
    }
}