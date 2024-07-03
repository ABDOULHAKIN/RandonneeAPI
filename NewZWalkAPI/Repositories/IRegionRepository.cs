using NewZWalkAPI.Models.Domain;

namespace NewZWalkAPI.Repositories
{
    public interface IRegionRepository
    {
        // Vu que le RegionController avait 5 méthodes pour faire les opérations de CRUD
        // Par définition, nous avons donc besoin de cinq définitions de méthodes
        // que nous mettrons plus tard en œuvre dans une classe concrète.

        // On va créer les differentes méthodes un par un

        // D'abord on définit le nom de la méthode
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid id, Region region);
        Task<Region?> DeleteAsync(Guid id);


        // Après avoir definit les nom des méthodes, on implemente ses methodes grace au fichier SQLRegionRepository
    }
}
