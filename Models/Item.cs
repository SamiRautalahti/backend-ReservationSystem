using System.ComponentModel.DataAnnotations;

namespace ReservationSystem2022.Models
{
    public class Item
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String? Description { get; set; }
        public virtual User? Owner { get; set; }

        public virtual List<Image>? Images { get; set; }
        
        public long accessCount { get; set; }//Sisäinen kenttä - ei lähetetä verkon yli

    }

    public class ItemDTO
    {
        public long Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Name { get; set; }    

        public string? Description { get; set; }
        [Required]
        public String Owner { get; set; }

        public virtual List<ImageDTO>? Images { get; set; }

    }
}
