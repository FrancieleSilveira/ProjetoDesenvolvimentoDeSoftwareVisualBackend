using Microsoft.AspNetCore.Mvc;
using API.Models;
using System.Linq;
using System;
using API.Data;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/paciente")]
    public class PacienteController : ControllerBase
    {
        private readonly DataContext _context;

        public PacienteController(DataContext context)
        {
            _context = context;
        }

        //POST: api/paciente/create
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Paciente paciente)
        {
            Convenio convenio = _context.Convenios.FirstOrDefault(
                c => c.Id == paciente.ConvenioId
            );

            paciente.Convenio = convenio;

            _context.Pacientes.Add(paciente);
            _context.SaveChanges();
            return Created("", paciente);
        }

        //GET: api/paciente/list
        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            return Ok(_context.Pacientes.Include(p => p.Convenio).ToList());
        }

        //GET: api/paciente/getbyid/1
        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            Paciente paciente = _context.Pacientes.Find(id);
            if (paciente == null) return NotFound();

            return Ok(paciente);
        }

        //GET: api/paciente/getbyname/Roger
        [HttpGet]
        [Route("getbyname/{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
           Paciente paciente = _context.Pacientes.FirstOrDefault(
                paciente => paciente.Nome == name
            );
            if (paciente == null)
            {
                return NotFound();
            }
            return Ok(paciente);
        }

        //DELETE: api/paciente/delete/2
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            Paciente paciente = _context.Pacientes.FirstOrDefault(
                paciente => paciente.Id == id
            );

            _context.Pacientes.Remove(paciente);
            _context.SaveChanges();
            return Ok(paciente);
        }

        //PUT: api/paciente/update
        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
            _context.SaveChanges();
            return Ok(paciente);
        }
    }
}