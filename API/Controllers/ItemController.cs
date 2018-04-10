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
    public async Task<IEnumerable<ItemDTO>> ListarItens()
    {
      return await facade.BuscarItensAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarItemPorId(int id)
    {
      var item = await facade.BuscarItemAsync(id);

      if (item == null)
        return await Task.FromResult(NotFound());

      return new ObjectResult(item);
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
