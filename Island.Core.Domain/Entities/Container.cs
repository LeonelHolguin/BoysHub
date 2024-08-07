using Island.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Island.Core.Domain.Entities
{
    public class Container : AuditableBaseEntity
    {
        public required string Name { get; set; }
        public int ContainerTypeId { get; set; }
        public required string Location { get; set; }
        public int ContainerStatusId { get; set; }

        #region Navigation properties
        public ContainerType? ContainerType { get; set; }
        public ContainerStatus? ContainerStatus { get; set; }
        #endregion
    }
}
