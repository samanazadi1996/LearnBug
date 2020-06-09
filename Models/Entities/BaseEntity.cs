using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public interface IEntity
    {
    }

    public abstract class BaseEntity<TKey> : IEntity
    {
        protected BaseEntity()
        {
            IsActive = true;
            IsDeleted = false;
            InsertDateTime = DateTime.Now;
        }
        [Key]
        [ScaffoldColumn(false)]
        public TKey Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertDateTime { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public string Description { get; set; }

    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
