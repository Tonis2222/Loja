using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
  public class Item
  {
    private const int TAMANHO_MAXIMO_DESCRICAO = 200;

    public int Id { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public byte[] Versao { get; set; }

    public bool EValidoParaAtualizar(out string mensagemValidacao)
    {
      if (Id < 1)
      {
        mensagemValidacao = "Id não informado.";
        return false;
      }

      return EValidoParaCadastrar(out mensagemValidacao);
    }

    public bool EValidoParaCadastrar(out string mensagemValidacao)
    {

      if (string.IsNullOrWhiteSpace(Descricao))
      {
        mensagemValidacao = "Descrição não informada.";
        return false;
      }

      if (Descricao.Length > TAMANHO_MAXIMO_DESCRICAO)
      {
        mensagemValidacao = $"Descrição deve ter no máximo {TAMANHO_MAXIMO_DESCRICAO} caracteres.";
        return false;
      }

      if (Valor <= 0)
      {
        mensagemValidacao = "Valor não informado.";
        return false;
      }

      mensagemValidacao = string.Empty;
      return true;
    }
  }
}
