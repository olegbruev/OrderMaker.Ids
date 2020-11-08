using System;


namespace Mtd.OrderMaker.Ids.Entity
{
    public class MtdGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }

    }
}
