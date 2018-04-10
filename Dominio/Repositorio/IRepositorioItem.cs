using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorio
{
  public interface IRepositorioItem
  {
    Task CadastrarItemAsync(Item item);
    Task AtualizarItemAsync(Item item);
    Task<List<Item>> BuscarItensAsync();
    Task<Item> BuscarItemAsync(int id);
  }
}
