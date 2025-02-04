﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class AkademisyenManager : IAkademisyenService
    {
        IAkademisyenDal _akademisyenDal;

        public AkademisyenManager(IAkademisyenDal akademisyenDal)
        {
            _akademisyenDal = akademisyenDal;
        }

        public IResult Add(AkademisyenForRegisterDto akademisyenForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(akademisyenForRegisterDto.Sifre , out passwordHash, out passwordSalt);

            var akademisyen = new Akademisyen
            {
                Isim = akademisyenForRegisterDto.Isim,
                Soyad = akademisyenForRegisterDto.Soyad,
                EMail = akademisyenForRegisterDto.EMail,
                Adres = akademisyenForRegisterDto.Adres,
                KayitTarihi = akademisyenForRegisterDto.KayitTarihi,
                TelefonNumarasi = akademisyenForRegisterDto.TelefonNumarasi,
                SaltSifre = passwordSalt,
                HashSifre = passwordHash,
                UnvanId = akademisyenForRegisterDto.UnvanId,
                BolumId = akademisyenForRegisterDto.BolumId,
                SicilNo = akademisyenForRegisterDto.SicilNo
            };

            var result = BusinessRules.Run(SicilNoKontrol(akademisyenForRegisterDto.SicilNo), EmailKontrol(akademisyenForRegisterDto.EMail),
                    TelefeonNoKontrol(akademisyenForRegisterDto.TelefonNumarasi));

            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

            _akademisyenDal.Add(akademisyen);
            return new Result(true, Messages.AkademisyenAdded);
        }

        public IResult Delete(int sicilNo)
        {
            var akademisyen = _akademisyenDal.Get(a => a.SicilNo == sicilNo);
            _akademisyenDal.Delete(akademisyen);
            return new Result(true, Messages.AkademisyenDeleted);
        }

        [ValidationAspect(typeof(AkademisyenValidator))]
        public IResult Update(AkademisyenForRegisterDto akademisyenForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(akademisyenForRegisterDto.Sifre, out passwordHash, out passwordSalt);

            var akademisyen = new Akademisyen
            {
                Isim = akademisyenForRegisterDto.Isim,
                Soyad = akademisyenForRegisterDto.Soyad,
                EMail = akademisyenForRegisterDto.EMail,
                Adres = akademisyenForRegisterDto.Adres,
                KayitTarihi = akademisyenForRegisterDto.KayitTarihi,
                TelefonNumarasi = akademisyenForRegisterDto.TelefonNumarasi,
                SaltSifre = passwordSalt,
                HashSifre = passwordHash,
                UnvanId = akademisyenForRegisterDto.UnvanId,
                BolumId = akademisyenForRegisterDto.BolumId,
                SicilNo = akademisyenForRegisterDto.SicilNo
            };

            var result = BusinessRules.Run(SicilNoKontrol(akademisyenForRegisterDto.SicilNo), EmailKontrol(akademisyenForRegisterDto.EMail),
                    TelefeonNoKontrol(akademisyenForRegisterDto.TelefonNumarasi));

            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }
            _akademisyenDal.Update(akademisyen);
            return new Result(true, Messages.AkademisyenUpdated);
        }

        public IDataResult<Akademisyen> Login(LoginDto LoginDto)
        {
            var akademisyenKontrol = _akademisyenDal.Get(a=>a.SicilNo==LoginDto.LoginNo);
            if (akademisyenKontrol == null)
            {
                return new ErrorDataResult<Akademisyen>("Kullanıcı bulunamadı");
            }
            if (!HashingHelper.VerifyPasswordHash(LoginDto.Password, akademisyenKontrol.HashSifre, akademisyenKontrol.SaltSifre))
            {
                return new ErrorDataResult<Akademisyen>("Parola hatası");
            }
            return new SuccessDataResult<Akademisyen>(akademisyenKontrol, "Başarılı giriş");
        }

        public IDataResult<List<Akademisyen>> GetAll()
        {
            return new SuccessDataResult<List<Akademisyen>>(_akademisyenDal.GetAll(), Messages.AkademisyenListed);
        }

        public IDataResult<List<AkademisyenDetayDto>> GetByBolumId(int Id)
        {
            return new SuccessDataResult<List<AkademisyenDetayDto>>(_akademisyenDal.GetAkademisyenDetaylari(a => a.BolumId == Id), Messages.AkademisyenGeted);
        }

        public IDataResult<List<AkademisyenDetayDto>> GetBySicilNo(int sicilNo)
        {
            return new SuccessDataResult<List<AkademisyenDetayDto>>(_akademisyenDal.GetAkademisyenDetaylari(a => a.SicilNo == sicilNo), Messages.AkademisyenGeted);

        }

        public IDataResult<List<AkademisyenDetayDto>> GetByEMail(string email)
        {
            return new SuccessDataResult<List<AkademisyenDetayDto>>(_akademisyenDal.GetAkademisyenDetaylari(a => a.EMail == email), Messages.AkademisyenGeted);
        }

        public IDataResult<List<AkademisyenDetayDto>> GetByUnvanId(int Id)
        {
            return new SuccessDataResult<List<AkademisyenDetayDto>>(_akademisyenDal.GetAkademisyenDetaylari(a => a.UnvanId == Id), Messages.AkademisyenGeted);
        }
        public IDataResult<List<AkademisyenDetayDto>> GetById(int Id)
        {
            return new SuccessDataResult<List<AkademisyenDetayDto>>(_akademisyenDal.GetAkademisyenDetaylari(a => a.Id == Id), Messages.AkademisyenGeted);
        }

        public IDataResult<List<AkademisyenDetayDto>> GetAllByAkademisyenDto()
        {
            return new SuccessDataResult<List<AkademisyenDetayDto>>(_akademisyenDal.GetAkademisyenDetaylari(), Messages.AkademisyenGeted);
        }

        private IResult SicilNoKontrol(int sicilNo)
        {
            var result = _akademisyenDal.GetAll(a => a.SicilNo == sicilNo).Count();
            if (result == 0)
            {
                return new SuccessResult();
            }

            return new ErrorResult("Bu sicil numarasına ait akademisyen var");
        }

        private IResult EmailKontrol(string email)
        {
            var result = _akademisyenDal.GetAll(a => a.EMail == email).Count();
            if (result == 0)
            {
                return new SuccessResult();
            }

            return new ErrorResult("Bu sicil emaile ait akademisyen var");
        }

        private IResult TelefeonNoKontrol(string telefonNumarasi)
        {
            var result = _akademisyenDal.GetAll(a => a.TelefonNumarasi == telefonNumarasi).Count();
            if (result == 0)
            {
                return new SuccessResult();
            }

            return new ErrorResult("Bu telefon numarasına ait akademisyen var");
        }
    }

}

