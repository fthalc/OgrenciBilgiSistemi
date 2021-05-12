﻿using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IDevamsizlikService
    {
        IDataResult<Devamsizlik> GetById(int Id);
        IDataResult<List<Devamsizlik>> GetAll();
        IResult Add(Devamsizlik devamsizlik);
        IResult Update(Devamsizlik devamsizlik);
        IResult Delete(Devamsizlik devamsizlik);
    }
}
