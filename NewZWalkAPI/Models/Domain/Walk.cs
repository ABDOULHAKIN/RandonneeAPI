namespace NewZWalkAPI.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get;set; }

        // Les clés étrangeres avec les tables Difficulty and Region
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        // Pour accéder aux entités associés par des relations
        // On a la navigation property
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }

    }
}
