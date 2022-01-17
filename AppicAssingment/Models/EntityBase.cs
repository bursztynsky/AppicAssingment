using System.ComponentModel.DataAnnotations;

namespace AppicAssingment.Models
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }

        //public virtual bool IsValid()
        //{
        //    return Id != Guid.Empty;
        //}
    }
}
