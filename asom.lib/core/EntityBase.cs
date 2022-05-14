using System;

namespace itrex.businessObjects.model.core
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            DateCreated = DateTime.UtcNow;
            IsActive = true;
        }
        object id = Guid.NewGuid().ToString();
        public object Id
        {
            get
            {
                return id ??  Guid.NewGuid().ToString();
            }
            set
            {
                id=  value;
            }
        }
        public string Key
        {
            get
            {
                return Id.ToString();
            }
            set
            {
                Id = value;
            }
        }
        public DateTime? DateCreated { get; set; }
        public bool? IsActive { get; set; }
    }
}