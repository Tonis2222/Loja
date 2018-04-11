using Dominio;
using InfraSQLServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TesteIntegracao
{
  [TestClass]
  public class RepositorioClienteSQLServerTeste
  {
    private string connectionString = @"Server=localhost\SQLEXPRESS;Database=teste;Trusted_Connection=True;";

    [TestMethod]
    public void Cadastrarcliente()
    {
      var repositorio = new RepositorioClienteSQLServer(connectionString);
      Cliente cliente = new Cliente()
      {
        CPF = new Random().Next(0, int.MaxValue),
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };
      var ret = repositorio.CadastrarClienteAsync(cliente);
      ret.Wait();

      Assert.IsTrue(cliente.Id > 0);
    }

    [TestMethod]
    public void Buscarcliente()
    {
      var repositorio = new RepositorioClienteSQLServer(connectionString);
      Cliente cliente = new Cliente()
      {
        CPF = new Random().Next(0, int.MaxValue),
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };
      var ret = repositorio.CadastrarClienteAsync(cliente);
      ret.Wait();
      Assert.IsTrue(cliente.Id > 0);

      var ret1 = repositorio.BuscarClientesAsync();
      ret1.Wait();
      Assert.IsNotNull(repositorio);
      Assert.IsTrue(ret1.Result.Any(a => a.Id == cliente.Id));
    }

    [TestMethod]
    public void BuscarclientePorId()
    {
      var repositorio = new RepositorioClienteSQLServer(connectionString);
      Cliente cliente = new Cliente()
      {
        CPF = new Random().Next(0, int.MaxValue),
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };
      var ret = repositorio.CadastrarClienteAsync(cliente);
      ret.Wait();
      Assert.IsTrue(cliente.Id > 0);

      var ret1 = repositorio.BuscarClienteAsync(cliente.Id, null);
      ret1.Wait();
      Assert.IsNotNull(ret1.Result);
      Assert.IsTrue(ret1.Result.Id == cliente.Id);
    }

    [TestMethod]
    public void Atualizarcliente()
    {
      var nomeEsperado = "Cliente Teste Atualizado";
      var repositorio = new RepositorioClienteSQLServer(connectionString);
      Cliente cliente = new Cliente()
      {
        CPF = new Random().Next(0, int.MaxValue),
        Nome = "Cliente Teste",
        Endereco = new Endereco()
        {
          Rua = "Rua Teste",
          Numero = 3,
          Complemento = "101"
        }
      };
      var ret = repositorio.CadastrarClienteAsync(cliente);
      ret.Wait();
      Assert.IsTrue(cliente.Id > 0);

      var ret1 = repositorio.BuscarClientesAsync();
      ret1.Wait();

      Assert.IsNotNull(repositorio);
      Assert.IsTrue(ret1.Result.Any(a => a.Id == cliente.Id));

      var clienteSalvo = ret1.Result.First(a => a.Id == cliente.Id);
      clienteSalvo.Nome = nomeEsperado;

      var ret2 = repositorio.AtualizarClienteAsync(clienteSalvo);
      ret2.Wait();

      var ret3 = repositorio.BuscarClientesAsync();
      ret3.Wait();
      Assert.IsNotNull(repositorio);
      Assert.IsTrue(ret3.Result.Any(a => a.Id == cliente.Id));
      Assert.AreEqual(nomeEsperado, ret3.Result.First(a => a.Id == cliente.Id).Nome);
    }

  }
}
