using Aplicacao;
using Aplicacao.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
  [Produces("application/json")]
  [Route("api/Item")]
  public class ItemController : Controller
  {
    LojaStoneFacade facade;
          
    public ItemController(LojaStoneFacade facade)
    {
      this.facade = facade;
    }

    [HttpGet]
    public async Task<IActionResult> ListarItens()
    {
      var ret = await facade.BuscarItensAsync();

      if (ret.Sucesso)
        return new ObjectResult(ret.Itens);
      else
        return BadRequest(ret.Mensagem);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarItemPorId(int id)
    {
      var ret = await facade.BuscarItemAsync(id);

      if (ret.Sucesso)
      {
        if (ret.Item == null)
          return NotFound();
        else
          return new ObjectResult(ret.Item);
      }
      else
      {
        return BadRequest(ret.Mensagem);
      }
    }

    [HttpPost]
    public async Task<IActionResult> CriarItem([FromBody]ItemDTO item)
    {
      var ret = await facade.CadastrarItemAsync(item);

      if (ret.Sucesso)
        return Ok(ret.Id);
      else
        return BadRequest(ret.Mensagem);
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarItem([FromBody]ItemDTO item)
    {
      var ret = await facade.AtualizarItemAsync(item);

      if (ret.Sucesso)
        return Ok(ret.Id);
      else
        return BadRequest(ret.Mensagem);

    }
  }
}
