using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.DTO
{
  public class ClienteDTO
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public long CPF { get; set; }
    public EnderecoDTO Endereco { get; set; }

  }
}
