using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DesafioGlobaltec.Domain.Services;
using DesafioGlobaltec.Domain.Models;

namespace DesafioGlobaltec.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class Pessoas : ControllerBase {
        private SPessoa _service;

        public Pessoas(SPessoa service) {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Pessoa> Get() {
            return _service.ListarTodos();
        }

        [HttpGet("{codigoPessoa}")]
        public ActionResult<Pessoa> Get(string codigoPessoa) {
            var Pessoa = _service.Obter(codigoPessoa);
            if (Pessoa != null) {
                return Pessoa;
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        public Resultado Post([FromBody]Pessoa pessoa) {
            return _service.Incluir(pessoa);
        }

        [HttpPut]
        public Resultado Put([FromBody]Pessoa pessoa) {
            return _service.Atualizar(pessoa);
        }

        [HttpDelete("{codigoPessoa}")]
        public Resultado Delete(string codigoPessoa) {
            return _service.Excluir(codigoPessoa);
        }
    }
}