using Microsoft.EntityFrameworkCore;
using NewZWalkAPI.Data;
using NewZWalkAPI.Models.Domain;

namespace NewZWalkAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        // C'est le role de RegionRepository d'accéder au BDD
        private readonly NZWalkDbContext dbContext;

        // Maintenant que nous avons une définition IRegionRepository
        // créons une implémentation qui mettra en œuvre cette interface.
        // On va injecter la connexion à la BDD

        public SQLRegionRepository(NZWalkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            //C'est le role de RegionRepository d'accéder au dbContext
            return await dbContext.Regions.ToListAsync(); // ce code se trouvait dans la controller auparavant
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            // Verifier si une region existe avec cette id
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            // Après avoir changer, on va enregistrer les nouvels elements
            await dbContext.SaveChangesAsync();
            return existingRegion;

        }
    }

}
