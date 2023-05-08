using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uyg04.ViewModel
{
    public class KayitModel
    {
        public string kayitId { get; set; }
        public string kayitSoruId { get; set; }
        public string kayitDersId { get; set; }
        public string kayitOgrId { get; set; }
        public string kayitCevapId { get; set; }
        public OgrenciModel ogrBilgi { get; set; }
        public DersModel dersBilgi { get; set; }
        public SoruModel soruBilgi { get; set; }
        public CevapModel cevapBilgi { get; set; }
    }
}