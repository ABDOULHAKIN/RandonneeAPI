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
        public async Task<List<Region>> GetAllAsync()
        {
            //C'est le role de RegionRepository d'accéder au dbContext
            return await dbContext.Regions.ToListAsync(); // ce code se trouvait dans la controller auparavant
        }
    }
}
