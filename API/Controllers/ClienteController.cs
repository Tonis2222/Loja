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
    public async Task<IActionResult> ListarClientes()
    {
      var ret = await facade.BuscarClientesAsync();

      if (ret.Sucesso)
        return new ObjectResult(ret.Clientes);
      else
        return BadRequest(ret.Mensagem);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarClientePorId(int id)
    {
      var ret = await facade.BuscarClienteAsync(id);

      if (ret.Sucesso)
      {
        if (ret.Cliente == null)
          return NotFound();
        else
          return new ObjectResult(ret.Cliente);
      }
      else
      {
        return BadRequest(ret.Mensagem);
      }
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
