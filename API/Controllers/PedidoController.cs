using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacao;
using Aplicacao.DTO;
using Dominio;
using Dominio.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Produces("application/json")]
  [Route("api/Pedido")]
  public class PedidoController : Controller
  {
    LojaStoneFacade facade;

    public PedidoController(LojaStoneFacade facade)
    {
      this.facade = facade;
    }

    [HttpGet]
    public async Task<IActionResult> ListarPedidos()
    {
      var ret = await facade.BuscarPedidosAsync();

      if (ret.Sucesso)
        return new ObjectResult(ret.Pedidos);
      else
        return BadRequest(ret.Mensagem);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPedidoPorId(Guid id)
    {
      var ret = await facade.BuscarPedidoAsync(id);

      if (ret.Sucesso)
      {
        if (ret.Pedido == null)
          return NotFound();
        else
          return new ObjectResult(ret.Pedido);
      }
      else
      {
        return BadRequest(ret.Mensagem);
      }
    }

    [HttpPost]
    public async Task<IActionResult> CriarPedido([FromBody]CriacaoPedidoDTO pedido)
    {
      var ret = await facade.CriarPedidoAsync(pedido);

      if (ret.Sucesso)
        return Ok(ret.Id);
      else
        return BadRequest(ret.Mensagem);
    }

    [HttpPost("Cancelar/{id}")]
    public async Task<IActionResult> CancelarPedido(Guid id)
    {
      var ret = await facade.CancelarPedidoAsync(id);

      if (ret.Sucesso)
        return Ok(ret.Id);
      else
        return BadRequest(ret.Mensagem);
    }
  }
}
