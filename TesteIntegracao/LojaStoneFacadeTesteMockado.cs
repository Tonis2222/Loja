using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio.Repositorio;
using Moq;
using Aplicacao;
using Dominio;
using Aplicacao.DTO;
using AutoMapper;
using Aplicacao.ServicosDeAplicacao;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace TesteIntegracao
{
  [TestClass]
  public class LojaStoneFacadeTesteMockado
  {
    private Mock<IRepositorioCliente> mockRepositorioCliente;
    private Mock<IRepositorioItem> mockRepositorioItem;
    private Mock<IRepositorioPedido> mockRepositorioPedido;
    private Mock<IServicoMensageria> mockServicoMensageria;
    private Mock<ILogger<LojaStoneFacade>> mockLogger;

    [TestInitialize]
    public void Inicializar()
    {
      Mapper.Reset();
      MapperInit.MapearDTOs();
      mockRepositorioCliente = new Mock<IRepositorioCliente>();
      mockRepositorioItem = new Mock<IRepositorioItem>();
      mockRepositorioPedido = new Mock<IRepositorioPedido>();
      mockServicoMensageria = new Mock<IServicoMensageria>();
      mockLogger = new Mock<ILogger<LojaStoneFacade>>();
    }

    [TestMethod]
    public void CancelarPedido()
    {
      string idPedido = "cc23a3e5-3044-4972-a114-a7a1453168cb";

      var facade = new LojaStoneFacade(mockRepositorioCliente.Object, mockRepositorioItem.Object, mockRepositorioPedido.Object, mockServicoMensageria.Object, mockLogger.Object);

      mockRepositorioPedido
        .Setup(a => a.BuscarPedidoAsync(It.Is<Guid>(b => b.ToString() == idPedido)))
        .ReturnsAsync(() => { return new Pedido() { Estado = EstadoPedido.Ativo }; });

      var retorno1 = facade.CancelarPedidoAsync(new Guid(idPedido));
      retorno1.Wait();

      mockRepositorioPedido.Verify(a => a.AtualizarPedidoAsync(It.IsAny<Pedido>()), Times.Once);
    }

    [TestMethod]
    public void CriarPedidoDeveCriarEGuardarCopiaNaMensageria()
    {
      IdVersaoDTO cliente = new IdVersaoDTO() { Id = 1, Versao = new byte[0] };
      List<IdVersaoDTO> itens = new List<IdVersaoDTO>()
      {
        new IdVersaoDTO(){ Id = 1, Versao = new byte[0] },
        new IdVersaoDTO(){ Id = 2, Versao = new byte[0] },
      };

      var facade = new LojaStoneFacade(mockRepositorioCliente.Object, mockRepositorioItem.Object, mockRepositorioPedido.Object, mockServicoMensageria.Object, mockLogger.Object);

      mockRepositorioCliente
        .Setup(a => a.BuscarClienteAsync(It.Is<int>(b => b == cliente.Id), It.IsAny<byte[]>()))
        .ReturnsAsync(() => { return new Cliente(); });

      mockRepositorioItem
        .Setup(a => a.BuscarItemAsync(It.Is<int>(b => b == itens[0].Id), It.IsAny<byte[]>()))
        .ReturnsAsync(() => { return new Item(); });

      mockRepositorioItem
        .Setup(a => a.BuscarItemAsync(It.Is<int>(b => b == itens[1].Id), It.IsAny<byte[]>()))
        .ReturnsAsync(() => { return new Item(); });

      var retorno1 = facade.CriarPedidoAsync(new CriacaoPedidoDTO() { Cliente = cliente, Itens = itens });
      retorno1.Wait();

      mockServicoMensageria.Verify(a => a.GuardarCopia(It.IsAny<Pedido>()), Times.Once);
      mockRepositorioPedido.Verify(a => a.CriarPedidoAsync(It.IsAny<Pedido>()), Times.Once);
      mockRepositorioCliente.Verify(a => a.BuscarClienteAsync(It.IsAny<int>(), It.IsAny<byte[]>()), Times.Once);
      mockRepositorioItem.Verify(a => a.BuscarItemAsync(It.IsAny<int>(), It.IsAny<byte[]>()), Times.Exactly(2));
    }

    [TestMethod]
    public void NaoPodeCriarPedidoSemCliente()
    {
      IdVersaoDTO cliente = new IdVersaoDTO() { Id = 1, Versao = new byte[0] };
      List<IdVersaoDTO> itens = new List<IdVersaoDTO>()
      {
        new IdVersaoDTO(){ Id = 1, Versao = new byte[0] },
        new IdVersaoDTO(){ Id = 2, Versao = new byte[0] },
      };

      var facade = new LojaStoneFacade(mockRepositorioCliente.Object, mockRepositorioItem.Object, mockRepositorioPedido.Object, mockServicoMensageria.Object, mockLogger.Object);

      mockRepositorioCliente
        .Setup(a => a.BuscarClienteAsync(It.Is<int>(b => b == cliente.Id), It.IsAny<byte[]>()))
        .ReturnsAsync(() => { return null; });

      mockRepositorioItem
        .Setup(a => a.BuscarItemAsync(It.Is<int>(b => b == itens[0].Id), It.IsAny<byte[]>()))
        .ReturnsAsync(() => { return new Item(); });

      mockRepositorioItem
        .Setup(a => a.BuscarItemAsync(It.Is<int>(b => b == itens[1].Id), It.IsAny<byte[]>()))
        .ReturnsAsync(() => { return new Item(); });

      var retorno1 = facade.CriarPedidoAsync(new CriacaoPedidoDTO() { Cliente = cliente, Itens = itens });
      retorno1.Wait();

      mockServicoMensageria.Verify(a => a.GuardarCopia(It.IsAny<Pedido>()), Times.Never);
      mockRepositorioPedido.Verify(a => a.CriarPedidoAsync(It.IsAny<Pedido>()), Times.Never);
      mockRepositorioCliente.Verify(a => a.BuscarClienteAsync(It.IsAny<int>(), It.IsAny<byte[]>()), Times.Once);
    }

    [TestMethod]
    public void NaoPodeCriarPedidoSemUmDosItens()
    {
      IdVersaoDTO cliente = new IdVersaoDTO() { Id = 1, Versao = new byte[0] };
      List<IdVersaoDTO> itens = new List<IdVersaoDTO>()
      {
        new IdVersaoDTO(){ Id = 1, Versao = new byte[0] },
        new IdVersaoDTO(){ Id = 2, Versao = new byte[0] },
      };

      var facade = new LojaStoneFacade(mockRepositorioCliente.Object, mockRepositorioItem.Object, mockRepositorioPedido.Object, mockServicoMensageria.Object, mockLogger.Object);

      mockRepositorioCliente
        .Setup(a => a.BuscarClienteAsync(It.Is<int>(b => b == cliente.Id), It.IsAny<byte[]>()))
        .ReturnsAsync(() => { return new Cliente(); });

      mockRepositorioItem
        .Setup(a => a.BuscarItemAsync(It.Is<int>(b => b == itens[0].Id), It.IsAny<byte[]>()))
        .ReturnsAsync(() => { return new Item(); });

      mockRepositorioItem
        .Setup(a => a.BuscarItemAsync(It.Is<int>(b => b == itens[1].Id), It.IsAny<byte[]>()))
        .ReturnsAsync(() => { return null; });

      var retorno1 = facade.CriarPedidoAsync(new CriacaoPedidoDTO() { Cliente = cliente, Itens = itens });
      retorno1.Wait();

      mockServicoMensageria.Verify(a => a.GuardarCopia(It.IsAny<Pedido>()), Times.Never);
      mockRepositorioPedido.Verify(a => a.CriarPedidoAsync(It.IsAny<Pedido>()), Times.Never);
      mockRepositorioCliente.Verify(a => a.BuscarClienteAsync(It.IsAny<int>(), It.IsAny<byte[]>()), Times.Once);
      mockRepositorioItem.Verify(a => a.BuscarItemAsync(It.IsAny<int>(), It.IsAny<byte[]>()), Times.Exactly(2));
    }

    [TestMethod]
    public void CadastrarCliente()
    {
      var facade = new LojaStoneFacade(mockRepositorioCliente.Object, mockRepositorioItem.Object, mockRepositorioPedido.Object, mockServicoMensageria.Object, mockLogger.Object);

      var clienteDTO = new ClienteDTO()
      {
        Nome = "Cliente Teste Mockado 1",
        CPF = 12345678910,
        Endereco = new EnderecoDTO()
        {
          Rua = "Rua Teste 1",
          Numero = 3,
          Complemento = "apartamento 101"
        }
      };

      var retorno1 = facade.CadastrarClienteAsync(clienteDTO);
      retorno1.Wait();

      mockRepositorioCliente.Verify(a => a.CadastrarClienteAsync(It.IsAny<Cliente>()), Times.Once);
    }

    [TestMethod]
    public void AtualizarCliente()
    {
      var facade = new LojaStoneFacade(mockRepositorioCliente.Object, mockRepositorioItem.Object, mockRepositorioPedido.Object, mockServicoMensageria.Object, mockLogger.Object);

      var clienteDTO = new ClienteDTO()
      {
        Id = 1,
        Nome = "Cliente Teste Mockado 1",
        CPF = 12345678910,
        Endereco = new EnderecoDTO()
        {
          Rua = "Rua Teste 1",
          Numero = 3,
          Complemento = "apartamento 101"
        }
      };

      var retorno1 = facade.AtualizarClienteAsync(clienteDTO);
      retorno1.Wait();

      mockRepositorioCliente.Verify(a => a.AtualizarClienteAsync(It.IsAny<Cliente>()), Times.Once);
    }

    [TestMethod]
    public void BuscarClientePorId()
    {
      var facade = new LojaStoneFacade(mockRepositorioCliente.Object, mockRepositorioItem.Object, mockRepositorioPedido.Object, mockServicoMensageria.Object, mockLogger.Object);

      var retorno1 = facade.BuscarClienteAsync(1);
      retorno1.Wait();

      mockRepositorioCliente.Verify(a => a.BuscarClienteAsync(It.IsAny<int>(), null), Times.Once);
    }


    [TestMethod]
    public void BuscarClientes()
    {
      var facade = new LojaStoneFacade(mockRepositorioCliente.Object, mockRepositorioItem.Object, mockRepositorioPedido.Object, mockServicoMensageria.Object, mockLogger.Object);

      var retorno1 = facade.BuscarClientesAsync();
      retorno1.Wait();

      mockRepositorioCliente.Verify(a => a.BuscarClientesAsync(), Times.Once);
    }

    [TestMethod]
    public void CadastrarItem()
    {
      var facade = new LojaStoneFacade(mockRepositorioCliente.Object, mockRepositorioItem.Object, mockRepositorioPedido.Object, mockServicoMensageria.Object, mockLogger.Object);

      var itemDTO = new ItemDTO()
      {
        Descricao = "Item Teste Mockado 1",
        Valor = 1.99M
      };

      var retorno1 = facade.CadastrarItemAsync(itemDTO);
      retorno1.Wait();

      mockRepositorioItem.Verify(a => a.CadastrarItemAsync(It.IsAny<Item>()), Times.Once);
    }

    [TestMethod]
    public void AtualizarItem()
    {
      var facade = new LojaStoneFacade(mockRepositorioCliente.Object, mockRepositorioItem.Object, mockRepositorioPedido.Object, mockServicoMensageria.Object, mockLogger.Object);

      var itemDTO = new ItemDTO()
      {
        Id = 1,
        Descricao = "Item Teste Mockado 1",
        Valor = 1.99M
      };

      var retorno1 = facade.AtualizarItemAsync(itemDTO);
      retorno1.Wait();

      mockRepositorioItem.Verify(a => a.AtualizarItemAsync(It.IsAny<Item>()), Times.Once);
    }

    [TestMethod]
    public void BuscarItemPorId()
    {
      var facade = new LojaStoneFacade(mockRepositorioCliente.Object, mockRepositorioItem.Object, mockRepositorioPedido.Object, mockServicoMensageria.Object, mockLogger.Object);

      var retorno1 = facade.BuscarItemAsync(1);
      retorno1.Wait();

      mockRepositorioItem.Verify(a => a.BuscarItemAsync(It.IsAny<int>(), null), Times.Once);
    }


    [TestMethod]
    public void BuscarItems()
    {
      var facade = new LojaStoneFacade(mockRepositorioCliente.Object, mockRepositorioItem.Object, mockRepositorioPedido.Object, mockServicoMensageria.Object, mockLogger.Object);

      var retorno1 = facade.BuscarItensAsync();
      retorno1.Wait();

      mockRepositorioItem.Verify(a => a.BuscarItensAsync(), Times.Once);
    }

    [TestMethod]
    public void BuscarPedidoPorId()
    {
      var facade = new LojaStoneFacade(mockRepositorioCliente.Object, mockRepositorioItem.Object, mockRepositorioPedido.Object, mockServicoMensageria.Object, mockLogger.Object);

      var retorno1 = facade.BuscarPedidoAsync(new Guid());
      retorno1.Wait();

      mockRepositorioPedido.Verify(a => a.BuscarPedidoAsync(It.IsAny<Guid>()), Times.Once);
    }

    [TestMethod]
    public void BuscarPedidos()
    {
      var facade = new LojaStoneFacade(mockRepositorioCliente.Object, mockRepositorioItem.Object, mockRepositorioPedido.Object, mockServicoMensageria.Object, mockLogger.Object);

      var retorno1 = facade.BuscarPedidosAsync();
      retorno1.Wait();

      mockRepositorioPedido.Verify(a => a.BuscarPedidosAsync(), Times.Once);
    }    
  }
}
