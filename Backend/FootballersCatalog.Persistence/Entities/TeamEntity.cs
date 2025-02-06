using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballersCatalog.Persistence.Entities
{
    public class TeamEntity
    {
        public Guid Id { get; set; }

        public string TeamTitle { get; set; }

        public virtual List<FootballerEntity>? Footballers { get; set; } = [];
    }
}
