﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOgrenciDal : EfEntityRepositoryBase<Ogrenci, OBSContext>, IOgrenciDal
    {
        public List<OgrenciDetayDTO> GetOgrenciDetaylari(Expression<Func<Ogrenci, bool>> filter = null)
        {
            using (OBSContext context = new OBSContext())
            {
                var result = from o in filter == null ? context.Ogrenciler : context.Set<Ogrenci>().Where(filter)
                             join u in context.Unvanlar on o.UnvanId equals u.Id
                             join kf in context.KullaniciFotograflar on o.FotografId equals kf.Id
                             join b in context.Bolumler on o.BolumId equals b.Id
                             join f in context.Fakulteler on b.FakulteId equals f.Id
                             join a in context.Akademisyenler on o.DanismanId equals a.Id
                             join au in context.Unvanlar on a.UnvanId equals au.Id
                             select new OgrenciDetayDTO
                             {
                                 Id=o.Id,
                                 Isim=o.Isim,
                                 Soyad=o.Soyad,
                                 EMail=o.EMail,
                                 Adres=o.Adres,
                                 KayitTarihi=o.KayitTarihi,
                                 TelefonNumarasi=o.TelefonNumarasi,
                                 UnvanId=o.UnvanId,
                                 UnvanAdi=u.UnvanAdi,
                                 FotografId=o.FotografId,
                                 FotografYolu=kf.FotografYolu,
                                 OgrenciNo=o.OgrenciNo,
                                 MufredatId=o.MufredatId,
                                 BolumId=o.BolumId,
                                 BolumAdi=b.BolumAdi,
                                 FakulteAdi=f.FakulteAdi,
                                 AileAdres=o.AileAdres,
                                 AileTelefon=o.AileTelefon,
                                 BankaAdi=o.BankaAdi,
                                 SubeAdi=o.SubeAdi,
                                 SubeKodu=o.SubeKodu,
                                 HesapNumarası=o.HesapNumarası,
                                 IBAN=o.IBAN,
                                 HesapSahibininAdiSoyadi=o.HesapSahibininAdiSoyadi,
                                 DanismanId=o.DanismanId,
                                 DanismanAdi=a.Isim,
                                 DanismanSoyadi=a.Soyad,
                                 DanismanEMail=a.EMail,
                                 DanismanTelefonNumarasi=a.TelefonNumarasi,
                                 DanismanUnvanAdi=au.UnvanAdi
                             };
                return result.ToList();
            }
        }
    }
}
