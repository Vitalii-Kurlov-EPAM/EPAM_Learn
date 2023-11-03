using System.ComponentModel.DataAnnotations;

namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Entities
{
    public class ImageEntity
    {
        [Key]
        public int ImageId { get; set; }

        public string Url { get; set; }

        public string Alt { get; set; }
    }
}