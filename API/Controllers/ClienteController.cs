using Aplicacao;
using Aplicacao.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
  [Produces("application/json")]
  [Route("api/Cliente")]
  public class ClienteController : Controller
  {
    LojaStoneFacade facade;

    public ClienteController(LojaStoneFacade facade)
    {
      this.facade = facade;
    }

    [HttpGet]
    public async Task<IEnumerable<ClienteDTO>> ListarClientes()
    {
      return await facade.BuscarClientesAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarClientePorId(int id)
    {
      var cliente = await facade.BuscarClienteAsync(id);

      if (cliente == null)
        return await Task.FromResult(NotFound());

      return new ObjectResult(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> CriarCliente([FromBody]ClienteDTO cliente)
    {
      var ret = await facade.CadastrarClienteAsync(cliente);

      if (ret.Sucesso)
        return Ok(ret.Id);
      else
        return BadRequest(ret.Mensagem);
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarCliente([FromBody]ClienteDTO cliente)
    {
      var ret = await facade.AtualizarClienteAsync(cliente);

      if (ret.Sucesso)
        return Ok(ret.Id);
      else
        return BadRequest(ret.Mensagem);

    }
  }
}
