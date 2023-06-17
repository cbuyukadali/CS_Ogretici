using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS_Ogretici.Models
{
    public class Kullanicilar
    {
        [Key]
        public int KullaniciId { get; set; }
        [Column(TypeName ="Varchar(20)")]
        public string KullaniciAd { get; set; }

        [Column(TypeName = "Varchar(20)")]
        public string KullaniciSoyad { get; set; }
        public string KullaniciEposta { get; set; }
        public string KullaniciSifre { get; set; }

        public string KullaniciTur { get; set; } = "Normal"; 
        //Admin yada Normal, baslangicta tum kullanicilar Normal ve konuları degistirme linklerine erisimleri yok
        //sadece Admin kullanicilar, baska kulllanicilari Admin olarak donusturebilir veya silebilir,
        //ama baska Admini Normal yapamaz
        //Buna sadece SuperAdminin hakki var?

    }
}
