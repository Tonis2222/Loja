﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorio
{
  public interface IRepositorioCliente
  {
    Task CadastrarClienteAsync(Cliente cliente);
    Task<bool> AtualizarClienteAsync(Cliente cliente);
    Task<List<Cliente>> BuscarClientesAsync();
    Task<Cliente> BuscarClienteAsync(int id, byte[] versao = null);
  }
}
