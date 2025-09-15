using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvents
    {
        public IntegrationBaseEvents()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
        public IntegrationBaseEvents(Guid id,DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }
        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }
    }
}
