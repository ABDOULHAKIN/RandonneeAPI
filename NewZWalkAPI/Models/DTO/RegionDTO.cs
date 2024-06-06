namespace NewZWalkAPI.Models.DTO
{
    public class RegionDTO
    {
        // C'est cette classe RegionDTO qui sera exposer en retour aux clients
        // Cette classe aura les propriétés de la classe Region from DM
        // Cet DTO sera un sous-ensemble de la classe Region DM

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
