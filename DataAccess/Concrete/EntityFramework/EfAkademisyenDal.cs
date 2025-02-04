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
    public class EfAkademisyenDal : EfEntityRepositoryBase<Akademisyen, OBSContext>, IAkademisyenDal
    {
        public List<AkademisyenDetayDto> GetAkademisyenDetaylari(Expression<Func<Akademisyen, bool>> filter = null)
        {
            using (OBSContext context = new OBSContext())
            {
                var result = from a in filter == null ? context.Akademisyenler : context.Set<Akademisyen>().Where(filter)
                             join u in context.Unvanlar on a.UnvanId equals u.Id
                             join b in context.Bolumler on a.BolumId equals b.Id
                             join f in context.Fakulteler on b.FakulteId equals f.Id
                             join af in context.AkademisyenFotograflar on a.Id equals af.AkademisyenId
                             select new AkademisyenDetayDto
                             {
                                 Id = a.Id,
                                 BolumId=a.BolumId,
                                 UnvanId=a.UnvanId,
                                 HashSifre=a.HashSifre,
                                 SaltSifre=a.SaltSifre,
                                 Isim = a.Isim,
                                 Soyad = a.Soyad,
                                 EMail = a.EMail,
                                 Adres = a.Adres,
                                 KayitTarihi = a.KayitTarihi,
                                 TelefonNumarasi = a.TelefonNumarasi,
                                 UnvanAdi = u.UnvanAdi,
                                 FakulteAdi = f.FakulteAdi,
                                 SicilNo = a.SicilNo,
                                 BolumAdi = b.BolumAdi,
                                 fotografYolu = af.FotografYolu
                             };
                return result.ToList();
            }
        }
    }
}
