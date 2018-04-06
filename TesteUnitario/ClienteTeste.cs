using Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteUnitario
{

  [TestClass]
  public class ClienteTeste
  {
    [TestMethod]
    public void NaoPodeCadastrarClienteSemNome()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Nome não informado.";

      Cliente cliente = new Cliente()
      {
        CPF = 12345678910,
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCadastrarClienteSemCPF()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "CPF não informado.";

      Cliente cliente = new Cliente()
      {
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCadastrarClienteSemEndereco()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Endereço não informado.";

      Cliente cliente = new Cliente()
      {
        CPF = 12345678910,
        Nome = "Cliente Teste",
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCadastrarClienteSemRua()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Rua não informada.";

      Cliente cliente = new Cliente()
      {
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCadastrarClienteSemNumero()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Número não informado.";

      Cliente cliente = new Cliente()
      {
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void DeveCadastrarClienteComTodosOsDados()
    {
      bool resultadoEsperado = true;

      Cliente cliente = new Cliente()
      {
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 100,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);

    }

    [TestMethod]
    public void DeveCadastrarClienteSemComplemento()
    {
      bool resultadoEsperado = true;

      Cliente cliente = new Cliente()
      {
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 100
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);

    }

    [TestMethod]
    public void NaoPodeAtualizarClienteSemID()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Id não informado.";

      Cliente cliente = new Cliente()
      {
        CPF = 12345678910,
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeAtualizarClienteSemNome()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Nome não informado.";

      Cliente cliente = new Cliente()
      {
        Id = 1,
        CPF = 12345678910,
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeAtualizarClienteSemCPF()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "CPF não informado.";

      Cliente cliente = new Cliente()
      {
        Id = 1,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeAtualizarClienteSemEndereco()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Endereço não informado.";

      Cliente cliente = new Cliente()
      {
        Id = 1,
        CPF = 12345678910,
        Nome = "Cliente Teste",
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeAtualizarClienteSemRua()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Rua não informada.";

      Cliente cliente = new Cliente()
      {
        Id = 1,
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeAtualizarClienteSemNumero()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Número não informado.";

      Cliente cliente = new Cliente()
      {
        Id = 1,
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void DeveAtualizarClienteComTodosOsDados()
    {
      bool resultadoEsperado = true;

      Cliente cliente = new Cliente()
      {
        Id = 1,
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 100,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);

    }

    [TestMethod]
    public void DeveAtualizarClienteSemComplemento()
    {
      bool resultadoEsperado = true;

      Cliente cliente = new Cliente()
      {
        Id = 1,
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 100
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaAtualizar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);

    }

    [TestMethod]
    public void NaoPodeCadastrarClienteComNomeMaiorQue200Caracteres()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Nome deve ter no máximo 200 caracteres.";

      Cliente cliente = new Cliente()
      {
        Nome = "abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghija",
        CPF = 12345678910,
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCadastrarClienteComCPFInvalido()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "CPF inválido.";

      Cliente cliente = new Cliente()
      {
        Nome = "Cliente Teste",
        CPF = 123456789101,
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCadastrarClienteComRuaMaiorQue200Caracteres()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Rua deve ter no máximo 200 caracteres.";

      Cliente cliente = new Cliente()
      {
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghija",
          Numero = 3,
          Complemento = "101"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }

    [TestMethod]
    public void NaoPodeCadastrarClienteComComplementoMaiorQue200Caracteres()
    {
      bool resultadoEsperado = false;
      string mensagemEsperada = "Complemento deve ter no máximo 200 caracteres.";

      Cliente cliente = new Cliente()
      {
        CPF = 12345678910,
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste 1",
          Numero = 3,
          Complemento = "abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghija"
        }
      };

      string mensagem = string.Empty;
      var resultado = cliente.EValidoParaCadastrar(out mensagem);

      Assert.AreEqual(resultadoEsperado, resultado);
      Assert.AreEqual(mensagemEsperada, mensagem);
    }


  }
}
