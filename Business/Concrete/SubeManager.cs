﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class SubeManager : ISubeService
    {
        ISubeDal _subeDal;

        public SubeManager(ISubeDal subeDal)
        {
            _subeDal = subeDal;
        }

        [ValidationAspect(typeof(SubeValidator))]
        public IResult Add(Sube sube)
        {
            _subeDal.Add(sube);
            return new Result(true, Messages.SubeAdded);
        }

        public IResult Delete(int Id)
        {
            Sube sube = _subeDal.Get(s => s.Id == Id);
            _subeDal.Delete(sube);
            return new Result(true, Messages.SubeDeleted);
        }

        [ValidationAspect(typeof(SubeValidator))]
        public IResult Update(Sube sube)
        {
            _subeDal.Update(sube);
            return new Result(true, Messages.SubeUpdated);
        }
        public IDataResult<List<Sube>> GetAll()
        {
            return new SuccessDataResult<List<Sube>>(_subeDal.GetAll(), Messages.SubeListed);
        }

        public IDataResult<List<SubeDetayDto>> GetById(int Id)
        {
            return new SuccessDataResult<List<SubeDetayDto>>(_subeDal.GetSubeDetaylari(s => s.Id == Id), Messages.SubeGeted);
        }

        public IDataResult<List<SubeDetayDto>> GetByDersId(int dersid)
        {
            return new SuccessDataResult<List<SubeDetayDto>>(_subeDal.GetSubeDetaylari(d => d.DersId == dersid), Messages.SubeListed);
        }
        public IDataResult<List<SubeDetayDto>> GetByOgretmenId(int ogretmenid)
        {
            return new SuccessDataResult<List<SubeDetayDto>>(_subeDal.GetSubeDetaylari(m => m.OgretmenId == ogretmenid), Messages.SubeListed);
        }

        public IDataResult<List<SubeDetayDto>> GetAllBySubeDto()
        {
            return new SuccessDataResult<List<SubeDetayDto>>(_subeDal.GetSubeDetaylari(), Messages.SubeListed);
        }
    }
}