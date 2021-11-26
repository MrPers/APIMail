using Mail.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Mail.DB.Models
{
    public class BaseEntity<T> : IBaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
