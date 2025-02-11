namespace FootballersCatalog.Persistence.Entities
{
    public class TeamEntity
    {
        public Guid Id { get; set; }

        public string TeamTitle { get; set; }

        public virtual List<FootballerEntity>? Footballers { get; set; } = [];
    }
}
