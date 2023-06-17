using System.ComponentModel.DataAnnotations;

namespace CS_Ogretici.Models
{
    public class Yorumlar
    {
        [Key]
        public int YorumId { get; set; }
        public int YorumParentId { get; set; } = 0; //Hangi başlık id sinde yorum açılmış
        public string YorumYazar { get; set; } = "Yazar";
        public string YorumSayfa { get; set; } = "Sayfa";
        public string YorumTime { get; set; }= DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToShortTimeString();
        
        public bool YorumAktif { get; set; }=false;


    }
}
