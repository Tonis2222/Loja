using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Repositorio
{
  public interface IRepositorioItem
  {
    void CadastrarItem(Item item);
    void AtualizarItem(Item item);
    List<Item> BuscarItens();
  }
}
