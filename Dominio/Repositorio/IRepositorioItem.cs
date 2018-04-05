using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Repositorio
{
  public interface IRepositorioItem
  {
    void CadastrarItem(Item cliente);
    void AtualizarItem(Item cliente);
    List<Item> BuscarItens();
  }
}
