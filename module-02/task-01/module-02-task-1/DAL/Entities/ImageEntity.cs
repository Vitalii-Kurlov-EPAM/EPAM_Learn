using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module_02.Task_01.CartingService.WebApi.DAL.Entities
{
    [Table("images")]
    public class ImageEntity
    {
        [Key]
        public int ImageId { get; set; }
        
        public string Url { get; set; }

        public string Alt { get; set; }
    }
}