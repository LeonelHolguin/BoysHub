using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Island.Core.Domain.Entities
{
    public class ContainerType
    {
        public int Id { get; set; }
        public required string Description { get; set; }

        public ICollection<Container>? Containers { get; set; }
    }
}
