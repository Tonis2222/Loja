using System;

namespace Dominio
{
  public class Cliente
  {
    private const int TAMANHO_MAXIMO_NOME = 200;

    public int Id { get; set; }
    public string Nome { get; set; }
    public long CPF { get; set; }
    public Endereco Endereco { get; set; }

    public bool EValidoParaCadastrar(out string mensagemValidacao)
    {
      if (string.IsNullOrWhiteSpace(Nome))
      {
        mensagemValidacao = "Nome não informado.";
        return false;
      }

      if (Nome.Length > TAMANHO_MAXIMO_NOME)
      {
        mensagemValidacao = $"Nome deve ter no máximo {TAMANHO_MAXIMO_NOME} caracteres.";
        return false;
      }

      if (CPF < 1)
      {
        mensagemValidacao = "CPF não informado.";
        return false;
      }

      if (CPF > 99999999999)
      {
        mensagemValidacao = "CPF inválido.";
        return false;
      }

      if (Endereco == null)
      {
        mensagemValidacao = "Endereço não informado.";
        return false;
      }
      
      return Endereco.EValidoParaCadastrar(out mensagemValidacao);
    }

    public bool EValidoParaAtualizar(out string mensagemValidacao)
    {
      if (Id < 1 )
      {
        mensagemValidacao = "Id não informado.";
        return false;
      }

      return EValidoParaCadastrar(out mensagemValidacao);
    }
  }
}
