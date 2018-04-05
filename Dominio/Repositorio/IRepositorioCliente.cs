using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Repositorio
{
  public interface IRepositorioCliente
  {
    void CadastrarCliente(Cliente cliente);
    void AtualizarCliente(Cliente cliente);
    List<Cliente> BuscarClientes();
  }
}
