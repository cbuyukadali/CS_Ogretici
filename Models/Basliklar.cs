using System.ComponentModel.DataAnnotations;

namespace CS_Ogretici.Models
{
    public class Basliklar
    {
        [Key]
        public int BaslikId { get; set; }
        public string BaslikAd { get; set; } = "ilk";
        public string BaslikSayfa { get; set; } = " ";
        public int BaslikParentId { get; set; }= 0;
    }
}
