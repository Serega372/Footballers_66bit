using System.ComponentModel.DataAnnotations.Schema;

namespace FootballersCatalog.Persistence.Entities
{
    public class FootballerEntity
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string? TeamTitle { get; set; }

        public string? Country { get; set; }

        public Guid? TeamId { get; set; }
        [ForeignKey("TeamId")]
        public TeamEntity? Team { get; set; }
    }
}
