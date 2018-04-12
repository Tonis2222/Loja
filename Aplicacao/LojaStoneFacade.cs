using Aplicacao.DTO;
using Aplicacao.Retorno;
using Aplicacao.ServicosDeAplicacao;
using AutoMapper;
using Dominio;
using Dominio.Repositorio;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao
{
  public class LojaStoneFacade
  {
    private IRepositorioCliente repositorioCliente;
    private IRepositorioItem repositorioItem;
    private IRepositorioPedido repositorioPedido;
    private IServicoMensageria servicoMensageria;
    private ILogger<LojaStoneFacade> logger;

    private const string MENSAGEM_ERRO_GERNERICO = "Ocorreu um erro ao processar a execução, tente novamente. Caso o problema persista contate o administrador do sistema.";

    public LojaStoneFacade(IRepositorioCliente repositorioCliente, IRepositorioItem repositorioItem, IRepositorioPedido repositorioPedido, IServicoMensageria servicoMensageria, ILogger<LojaStoneFacade> logger)
    {
      this.repositorioCliente = repositorioCliente;
      this.repositorioItem = repositorioItem;
      this.repositorioPedido = repositorioPedido;
      this.servicoMensageria = servicoMensageria;
      this.logger = logger;
    }

    #region Cliente

    public async Task<RetornoBuscarClientes> BuscarClientesAsync()
    {
      try
      {
        var clientes = await repositorioCliente.BuscarClientesAsync();

        var listaRetorno = new List<ClienteDTO>();

        clientes.ForEach(cliente =>
        {
          listaRetorno.Add(Mapper.Map<ClienteDTO>(cliente));
        });

        return new RetornoBuscarClientes() { Sucesso = true, Clientes = listaRetorno };
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Erro.");
        return new RetornoBuscarClientes() { Sucesso = false, Mensagem = MENSAGEM_ERRO_GERNERICO };
      }
    }

    public async Task<RetornoBuscarCliente> BuscarClienteAsync(int id)
    {
      try
      {
        var cliente = await repositorioCliente.BuscarClienteAsync(id);

        return new RetornoBuscarCliente() { Sucesso = true, Cliente = Mapper.Map<ClienteDTO>(cliente) };
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Erro.", id);
        return new RetornoBuscarCliente() { Sucesso = false, Mensagem = MENSAGEM_ERRO_GERNERICO };
      }
    }

    public async Task<RetornoCadastrarCliente> CadastrarClienteAsync(ClienteDTO clienteDTO)
    {
      try
      {
        if (clienteDTO == null)
          return new RetornoCadastrarCliente() { Sucesso = false, Mensagem = "ClienteDTO nulo." };

        var cliente = Mapper.Map<Cliente>(clienteDTO);

        string mensagemValidacao;
        if (!cliente.EValidoParaCadastrar(out mensagemValidacao))
          return new RetornoCadastrarCliente() { Sucesso = false, Mensagem = mensagemValidacao };

        await repositorioCliente.CadastrarClienteAsync(cliente);

        return new RetornoCadastrarCliente() { Sucesso = true, Id = cliente.Id };
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Erro.", clienteDTO);
        return new RetornoCadastrarCliente() { Sucesso = false, Mensagem = MENSAGEM_ERRO_GERNERICO };
      }
    }

    public async Task<RetornoAtualizarCliente> AtualizarClienteAsync(ClienteDTO clienteDTO)
    {
      try
      {
        if (clienteDTO == null)
          return new RetornoAtualizarCliente() { Sucesso = false, Mensagem = "ClienteDTO nulo." };

        var cliente = Mapper.Map<Cliente>(clienteDTO);

        string mensagemValidacao;
        if (!cliente.EValidoParaAtualizar(out mensagemValidacao))
          return new RetornoAtualizarCliente() { Sucesso = false, Mensagem = mensagemValidacao };

        var atualizou = await repositorioCliente.AtualizarClienteAsync(cliente);

        if (!atualizou)
          return new RetornoAtualizarCliente() { Sucesso = false, Mensagem = "O Cliente não pode ser atualizado, versão inválida." };

        return new RetornoAtualizarCliente() { Sucesso = true, Id = cliente.Id };
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Erro.", clienteDTO);
        return new RetornoAtualizarCliente() { Sucesso = false, Mensagem = MENSAGEM_ERRO_GERNERICO };
      }
    }

    #endregion

    #region Item
    public async Task<RetornoBuscarItens> BuscarItensAsync()
    {
      try
      {
        var itens = await repositorioItem.BuscarItensAsync();

        var listaRetorno = new List<ItemDTO>();

        itens.ForEach(item =>
        {
          listaRetorno.Add(Mapper.Map<ItemDTO>(item));
        });

        return new RetornoBuscarItens() { Sucesso = true, Itens = listaRetorno };
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Erro.");
        return new RetornoBuscarItens() { Sucesso = false, Mensagem = MENSAGEM_ERRO_GERNERICO };
      }
    }

    public async Task<RetornoBuscarItem> BuscarItemAsync(int id)
    {
      try
      {
        var item = await repositorioItem.BuscarItemAsync(id);

        return new RetornoBuscarItem() { Sucesso = true, Item = Mapper.Map<ItemDTO>(item) };
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Erro.", id);
        return new RetornoBuscarItem() { Sucesso = false, Mensagem = MENSAGEM_ERRO_GERNERICO };
      }
    }

    public async Task<RetornoCadastrarItem> CadastrarItemAsync(ItemDTO itemDTO)
    {
      try
      {
        if (itemDTO == null)
          return new RetornoCadastrarItem() { Sucesso = false, Mensagem = "ItemDTO nulo." };

        var item = Mapper.Map<Item>(itemDTO);

        string mensagemValidacao;
        if (!item.EValidoParaCadastrar(out mensagemValidacao))
          return new RetornoCadastrarItem() { Sucesso = false, Mensagem = mensagemValidacao };

        await repositorioItem.CadastrarItemAsync(item);

        return new RetornoCadastrarItem() { Sucesso = true, Id = item.Id };
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Erro.", itemDTO);
        return new RetornoCadastrarItem() { Sucesso = false, Mensagem = MENSAGEM_ERRO_GERNERICO };
      }
    }

    public async Task<RetornoAtualizarItem> AtualizarItemAsync(ItemDTO itemDTO)
    {
      try
      {
        if (itemDTO == null)
          return new RetornoAtualizarItem() { Sucesso = false, Mensagem = "ItemDTO nulo." };

        var item = Mapper.Map<Item>(itemDTO);

        string mensagemValidacao;
        if (!item.EValidoParaAtualizar(out mensagemValidacao))
          return new RetornoAtualizarItem() { Sucesso = false, Mensagem = mensagemValidacao };

        var atualizou = await repositorioItem.AtualizarItemAsync(item);

        if (!atualizou)
          return new RetornoAtualizarItem() { Sucesso = false, Mensagem = "O item não pode ser atualizado, versão inválida." };

        return new RetornoAtualizarItem() { Sucesso = true, Id = item.Id };
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Erro.", itemDTO);
        return new RetornoAtualizarItem() { Sucesso = false, Mensagem = MENSAGEM_ERRO_GERNERICO };
      }
    }

    #endregion

    #region Pedido

    public async Task<RetornoBuscarPedidos> BuscarPedidosAsync()
    {
      try
      {
        var pedidos = await repositorioPedido.BuscarPedidosAsync();

        var listaRetorno = new List<PedidoDTO>();

        pedidos.ForEach(pedido =>
        {
          listaRetorno.Add(Mapper.Map<PedidoDTO>(pedido));
        });

        return new RetornoBuscarPedidos() { Sucesso = true, Pedidos = listaRetorno };
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Erro.");
        return new RetornoBuscarPedidos() { Sucesso = false, Mensagem = MENSAGEM_ERRO_GERNERICO };
      }
    }

    public async Task<RetornoBuscarPedido> BuscarPedidoAsync(Guid id)
    {
      try
      {
        var pedido = await repositorioPedido.BuscarPedidoAsync(id);

        return new RetornoBuscarPedido() { Sucesso = true, Pedido = Mapper.Map<PedidoDTO>(pedido) };
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Erro.", id);
        return new RetornoBuscarPedido() { Sucesso = false, Mensagem = MENSAGEM_ERRO_GERNERICO };
      }
    }

    public async Task<RetornoCriarPedido> CriarPedidoAsync(CriacaoPedidoDTO criacaoPedidoDTO)
    {
      try
      {
        if (criacaoPedidoDTO == null)
          return new RetornoCriarPedido() { Sucesso = false, Mensagem = "criacaoPedidoDTO nulo." };

        if (criacaoPedidoDTO.Cliente == null)
          return new RetornoCriarPedido() { Sucesso = false, Mensagem = "criacaoPedidoDTO.Cliente nulo." };

        if (criacaoPedidoDTO.Itens == null || !criacaoPedidoDTO.Itens.Any())
          return new RetornoCriarPedido() { Sucesso = false, Mensagem = "criacaoPedidoDTO.Itens nulo ou vazio." };

        var cliente = await repositorioCliente.BuscarClienteAsync(criacaoPedidoDTO.Cliente.Id, criacaoPedidoDTO.Cliente.Versao);

        List<Item> itens = new List<Item>();

        foreach (var itemDTO in criacaoPedidoDTO.Itens)
        {
          var item = await repositorioItem.BuscarItemAsync(itemDTO.Id, itemDTO.Versao);

          if (item == null)
            return new RetornoCriarPedido() { Sucesso = false, Mensagem = $"Item inválido ou não encontrado. Id {itemDTO.Id}" };

          itens.Add(item);
        }

        var pedido = FabricaPedido.CriarPedido(cliente, itens);

        string mensagemValidacao;
        if (!pedido.EValidoParaCriar(out mensagemValidacao))
          return new RetornoCriarPedido() { Sucesso = false, Mensagem = mensagemValidacao };
        
        await repositorioPedido.CriarPedidoAsync(pedido);

        servicoMensageria.GuardarCopia(pedido);

        return new RetornoCriarPedido() { Sucesso = true, Id = pedido.Id };
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Erro.", criacaoPedidoDTO);
        return new RetornoCriarPedido() { Sucesso = false, Mensagem = MENSAGEM_ERRO_GERNERICO };
      }
    }

    public async Task<RetornoCancelarPedido> CancelarPedidoAsync(Guid id)
    {
      try
      {
        if (id == null || id == Guid.Empty)
          return new RetornoCancelarPedido() { Sucesso = false, Mensagem = "id Nulo." };

        var pedido = await repositorioPedido.BuscarPedidoAsync(id);

        if (pedido == null)
          return new RetornoCancelarPedido() { Sucesso = false, Mensagem = "Pedido não encontrado." };

        string mensagemValidacao;
        if (!pedido.Cancelar(out mensagemValidacao))
          return new RetornoCancelarPedido() { Sucesso = false, Mensagem = mensagemValidacao };

        await repositorioPedido.AtualizarPedidoAsync(pedido);

        return new RetornoCancelarPedido() { Sucesso = true, Id = pedido.Id };
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Erro.", id);
        return new RetornoCancelarPedido() { Sucesso = false, Mensagem = MENSAGEM_ERRO_GERNERICO };
      }
    }

    #endregion

  }
}
