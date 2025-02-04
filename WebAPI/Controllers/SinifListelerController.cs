﻿using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinifListelerController : ControllerBase
    {
        ISinifListeService _sinifListeService;
        public SinifListelerController(ISinifListeService sinifListeService)
        {
            _sinifListeService = sinifListeService;
        }

        [HttpPost("add")]
        public IActionResult Add(SinifListe sinifListe)
        {
            var result = _sinifListeService.Add(sinifListe);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("delete")]
        public IActionResult Delete(int Id)
        {
            var result = _sinifListeService.Delete(Id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(SinifListe sinifListe)
        {
            var result = _sinifListeService.Update(sinifListe);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _sinifListeService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int Id)
        {
            var result = _sinifListeService.GetById(Id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("getbyogrenciid")]
        public IActionResult GetByOgrenciId(int ogrenciId)
        {
            var result = _sinifListeService.GetByOgrenciId(ogrenciId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbysubeiid")]
        public IActionResult GetBySubeiId(int subeId)
        {
            var result = _sinifListeService.GetBySubeId(subeId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getsiniflisteDetaylari")]
        public IActionResult GetAllBySinifListeDto()
        {
            var result = _sinifListeService.GetAllBySinifListeDto();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}