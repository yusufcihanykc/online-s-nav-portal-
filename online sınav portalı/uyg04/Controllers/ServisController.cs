using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using uyg04.Models;
using uyg04.ViewModel;

namespace uyg04.Controllers
{
    public class ServisController : ApiController
    {
        DB03Entities4 db = new DB03Entities4();
        SonucModel sonuc = new SonucModel();

        #region Soru

        [HttpGet]
        [Route("api/soruliste")]
        public List<SoruModel> SoruListe()
        {
            List<SoruModel> liste = db.Soru.Select(x => new SoruModel()
            {
                soruId = x.soruId,
                soruKatId = x.soruKatId,
                soru = x.soru
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/sorubyid/{soruId}")]
        public SoruModel SoruById(string soruId)
        {
            SoruModel kayit = db.Soru.Where(s => s.soruId == soruId).Select(x => new SoruModel()
            {
                soruId = x.soruId,
                soruKatId = x.soruKatId,
                soru = x.soru,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/soruekle")]
        public SonucModel SoruEkle(SoruModel model)
        {
            if (db.Soru.Count(c => c.soru == model.soru) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Soru Kayıtlıdır!";
                return sonuc;
            }

            Soru yeni = new Soru();
            yeni.soruId = Guid.NewGuid().ToString();
            yeni.soru = model.soru;
            yeni.soruKatId = model.soruKatId;
            db.Soru.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Soru Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/soruduzenle")]
        public SonucModel SoruDuzenle(SoruModel model)
        {
            Soru kayit = db.Soru.Where(s => s.soruId == model.soruId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            kayit.soru = model.soru;
            kayit.soruKatId = model.soruKatId;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Soru Düzenlendi";

            return sonuc;
        }


        [HttpDelete]
        [Route("api/sorusil/{soruId}")]
        public SonucModel SoruSil(string soruId)
        {
            Soru kayit = db.Soru.Where(s => s.soruId == soruId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            if (db.Kayit.Count(c => c.kayitSoruId == soruId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Cevaplanmis Soru Silinemez!";
                return sonuc;
            }

            db.Soru.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Soru Silindi";

            return sonuc;
        }
        #endregion

        #region Ders

        [HttpGet]
        [Route("api/derSid/{dersId}")]
        public DersModel DersById(string dersId)
        {
            DersModel kayit = db.Ders.Where(s => s.dersId == dersId).Select(x => new DersModel()
            {
                dersId = x.dersId,
                dersId = x.dersId,
                ders = x.ders,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpGet]
        [Route("api/dersliste")]
        public List<DersModel> DersListe()
        {
            List<DersModel> liste = db.Ders.Select(x => new DersModel()
            {
                dersId = x.dersId,
                dersKodu = x.dersKodu,
                ders = x.ders
            }).ToList();
            return liste;
        }

        [HttpPost]
        [Route("api/dersekle")]
        public SonucModel DersEkle(DersModel model)
        {
            if (db.Ders.Count(c => c.ders == model.ders) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Ders Kayıtlıdır!";
                return sonuc;
            }

            Ders yeni = new Ders();
            yeni.dersId = Guid.NewGuid().ToString();
            yeni.ders = model.ders;
            yeni.dersId = model.dersId;
            db.Ders.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ders Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/dersduzenle")]
        public SonucModel DersDuzenle(OgrenciModel model)
        {
            Ders kayit = db.Ders.Where(s => s.dersId == model.ersId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ders Bulunmadı!";
                return sonuc;
            }

            kayit.ders = model.ders;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ders Düzenlendi";
            return sonuc;
        }


        [HttpDelete]
        [Route("api/derssil/{dersId}")]
        public SonucModel DersSil(string dersId)
        {
            Ders kayit = db.Ders.Where(s => s.dersId == dersId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ders Bulunmadı!";
                return sonuc;
            }


            db.Ders.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ders Silindi";
            return sonuc;
        }

        [HttpGet]
        [Route("api/ogrbyid/{ogrId}")]
        public OgrenciModel OgrById(string ogrId)
        {
            OgrenciModel kayit = db.Ogrenci.Where(s => s.ogrId == ogrId).Select(x => new OgrenciModel()
            {
                ogrId = x.ogrId,
                ogrNo = x.ogrNo,
                ogrAdsoyad = x.ogrAdsoyad
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPut]
        [Route("api/ogrduzenle")]
        public SonucModel OgrDuzenle(OgrenciModel model)
        {
            Ogrenci kayit = db.Ogrenci.Where(s => s.ogrId == model.ogrId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunmadı!";
                return sonuc;
            }

            kayit.ogrAdsoyad = model.ogrAdsoyad;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Cevap Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/ogrencisil/{ogrId}")]
        public SonucModel OgrenciSil(string ogrId)
        {
            Ogrenci kayit = db.Ogrenci.Where(s => s.ogrId == ogrId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunmadı!";
                return sonuc;
            }

            if (db.Kayit.Count(c => c.kayitOgrId == ogrId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Derse Kayıtlı Öğrenci Silinemez!";
                return sonuc;
            }


            db.Ogrenci.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Silindi";
            return sonuc;
        }

        #endregion

        #region Cevap

        [HttpPost]
        [Route("api/cevapekle")]
        public SonucModel Cevapla(KayitModel model)
        {
            if (db.Kayit.Count(c => c.kayitSoruId == model.kayitSoruId & c.kayitCevapId == model.kayitCevapId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Soru daha once cevaplanmistir!";
                return sonuc;
            }

            Kayit yeni = new Kayit();
            yeni.kayitId = Guid.NewGuid().ToString();
            yeni.kayitCevapId = model.kayitCevapId;
            yeni.kayitSoruId = model.kayitSoruId;
            db.Kayit.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Soru Cevaplandi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/kayitsil/{kayitId}")]
        public SonucModel KayitSil(string kayitId)
        {
            Kayit kayit = db.Kayit.Where(s => s.kayitId == kayitId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }


            db.Kayit.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kayıt Silindi";

            return sonuc;
        }
        #endregion
    }
}
