using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacao;
using Dominio;
using Dominio.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Produces("application/json")]
  [Route("api/Cliente")]
  public class ClienteController : Controller
  {

    IRepositorioCliente repositorioCliente;
    public ClienteController(IRepositorioCliente repositorioCliente)
    {
      this.repositorioCliente = repositorioCliente;
    }

    // GET: api/Cliente
    [HttpGet]
    public async Task<IEnumerable<Cliente>> Get()
    {
      return await repositorioCliente.BuscarClientesAsync();
    }

    // GET: api/Cliente/5
    [HttpGet("{id}", Name = "Get")]
    public async Task<IActionResult> Get(int id)
    {
      var cliente = await repositorioCliente.BuscarClienteAsync(id);

      if (cliente == null)
        return await Task.FromResult(NotFound());

      return new ObjectResult(cliente);
    }

    // POST: api/Cliente
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Cliente cliente)
    {
      if (cliente == null)
        return BadRequest(cliente);
      
      string mensagemValidacao;
      if (!cliente.EValidoParaCadastrar(out mensagemValidacao))
      {
        return BadRequest(mensagemValidacao);
      }

      await repositorioCliente.CadastrarClienteAsync(cliente);

      return await Task.FromResult(Ok(cliente));
    }

    // PUT: api/Cliente/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody]Cliente cliente)
    {
      if (cliente == null)
        return BadRequest(cliente);

      string mensagemValidacao;
      if (!cliente.EValidoParaAtualizar(out mensagemValidacao))
      {
        return BadRequest(mensagemValidacao);
      }

      await repositorioCliente.AtualizarClienteAsync(cliente);

      return await Task.FromResult(Ok(cliente));
      
    }
  }
}
