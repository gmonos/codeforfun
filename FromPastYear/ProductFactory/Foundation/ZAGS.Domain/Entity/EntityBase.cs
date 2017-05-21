using System;

namespace Zags.Domain.Entity
{
    public class EntityBase : IEntity
    {

        public EntityBase()
        {

        }

        public int Id { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsNew {
            get {
                return Id <= 0;
            }
        }

        //public byte[] RowVersion { get; set; }

        //public string CreatedBy { get; set; }


        //public string ModifiedBy { get; set; }


        //public DateTime CreatedOn { get; set; }


        //public DateTime? ModifiedOn { get; set; }

    }
}
