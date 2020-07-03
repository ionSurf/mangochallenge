using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class PortraitRepository : RepositoryBase<Portrait>, IPortraitRepository
    {
        public PortraitRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Portrait>> GetAllPortraitsAsync() {
            return await FindAll()
                .OrderBy(portrait => portrait.Title)
                .ToListAsync();
        }
        public async Task<Portrait> GetPortraitByIdAsync( Guid portraitId ) {
            return await FindByCondition(portrait => portrait.Id.Equals(portraitId))
            .FirstOrDefaultAsync();
        }
        public IEnumerable<Portrait> GetAllPortraits()
        {
            return FindAll()
                .OrderBy(portrait => portrait.Title)
                .ToList();
        }
        public Portrait GetPortraitById(Guid portraitId)
        {
            return FindByCondition(portrait => portrait.Id.Equals(portraitId))
            .FirstOrDefault();
        }
        public void CreatePortrait(Portrait portrait)
        {
            Create(portrait);
        }
        public void UpdatePortrait(Portrait portrait)
        {
            Update(portrait);
        }
        public void DeletePortrait(Portrait portrait)
        {
            Delete(portrait);
        }
    }
}