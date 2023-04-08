using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBookShopAPI.Data.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime UploadedInfo { get; set; }
        [Column(TypeName = "DateTime")]
        public DateTime UpdatedInfo { get; set; }
    }
}
