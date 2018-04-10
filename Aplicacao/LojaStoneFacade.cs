using Aplicacao.DTO;
using Aplicacao.Retorno;
using Aplicacao.ServicosDeAplicacao;
using AutoMapper;
using Dominio;
using Dominio.Repositorio;
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

    public LojaStoneFacade(IRepositorioCliente repositorioCliente, IRepositorioItem repositorioItem, IRepositorioPedido repositorioPedido, IServicoMensageria servicoMensageria)
    {
      this.repositorioCliente = repositorioCliente;
      this.repositorioItem = repositorioItem;
      this.repositorioPedido = repositorioPedido;
      this.servicoMensageria = servicoMensageria;
    }

    #region Cliente
    
    public async Task<List<ClienteDTO>> BuscarClientesAsync()
    {
      var clientes = await repositorioCliente.BuscarClientesAsync();

      var listaRetorno = new List<ClienteDTO>();

      clientes.ForEach(cliente =>
      {
        listaRetorno.Add(Mapper.Map<ClienteDTO>(cliente));
      });

      return listaRetorno;
    }

    public async Task<ClienteDTO> BuscarClienteAsync(int id)
    {
      var cliente = await repositorioCliente.BuscarClienteAsync(id);

      return Mapper.Map<ClienteDTO>(cliente);
    }

    public async Task<RetornoCadastrarCliente> CadastrarClienteAsync(ClienteDTO clienteDTO)
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
    
    public async Task<RetornoAtualizarCliente> AtualizarClienteAsync(ClienteDTO clienteDTO)
    {
      if (clienteDTO == null)
        return new RetornoAtualizarCliente() { Sucesso = false, Mensagem = "ClienteDTO nulo." };

      var cliente = Mapper.Map<Cliente>(clienteDTO);

      string mensagemValidacao;
      if (!cliente.EValidoParaAtualizar(out mensagemValidacao))
        return new RetornoAtualizarCliente() { Sucesso = false, Mensagem = mensagemValidacao };

      await repositorioCliente.AtualizarClienteAsync(cliente);

      return new RetornoAtualizarCliente() { Sucesso = true, Id = cliente.Id };
    }

    #endregion

    #region Item
    public async Task<List<ItemDTO>> BuscarItensAsync()
    {
      var itens = await repositorioItem.BuscarItensAsync();

      var listaRetorno = new List<ItemDTO>();

      itens.ForEach(item =>
      {
        listaRetorno.Add(Mapper.Map<ItemDTO>(item));
      });

      return listaRetorno;
    }

    public async Task<ItemDTO> BuscarItemAsync(int id)
    {
      var item = await repositorioItem.BuscarItemAsync(id);

      return Mapper.Map<ItemDTO>(item);
    }

    public async Task<RetornoCadastrarItem> CadastrarItemAsync(ItemDTO itemDTO)
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

    public async Task<RetornoAtualizarItem> AtualizarItemAsync(ItemDTO itemDTO)
    {
      if (itemDTO == null)
        return new RetornoAtualizarItem() { Sucesso = false, Mensagem = "ItemDTO nulo." };

      var item = Mapper.Map<Item>(itemDTO);

      string mensagemValidacao;
      if (!item.EValidoParaAtualizar(out mensagemValidacao))
        return new RetornoAtualizarItem() { Sucesso = false, Mensagem = mensagemValidacao };

      await repositorioItem.AtualizarItemAsync(item);

      return new RetornoAtualizarItem() { Sucesso = true, Id = item.Id };
    }

    #endregion

    #region Pedido

    public async Task<List<PedidoDTO>> BuscarPedidosAsync()
    {
      var pedidos = await repositorioPedido.BuscarPedidosAsync();

      var listaRetorno = new List<PedidoDTO>();

      pedidos.ForEach(pedido =>
      {
        listaRetorno.Add(Mapper.Map<PedidoDTO>(pedido));
      });

      return listaRetorno;
    }

    public async Task<PedidoDTO> BuscarPedidoAsync(Guid id)
    {
      var pedido = await repositorioPedido.BuscarPedidoAsync(id);

      return Mapper.Map<PedidoDTO>(pedido);
    }
    
    public async Task<RetornoCriarPedido> CriarPedidoAsync(CriacaoPedidoDTO criacaoPedidoDTO)
    {
      if (criacaoPedidoDTO == null)
        return new RetornoCriarPedido() { Sucesso = false, Mensagem = "criacaoPedidoDTO nulo." };

      if (criacaoPedidoDTO.Cliente == null)
        return new RetornoCriarPedido() { Sucesso = false, Mensagem = "criacaoPedidoDTO.Cliente nulo." };

      if (criacaoPedidoDTO.Itens == null || !criacaoPedidoDTO.Itens.Any())
        return new RetornoCriarPedido() { Sucesso = false, Mensagem = "criacaoPedidoDTO.Itens nulo ou vazio." };

      var cliente = Mapper.Map<Cliente>(criacaoPedidoDTO.Cliente);
      var itens = criacaoPedidoDTO.Itens.Select(itemDTO => Mapper.Map<Item>(itemDTO)).ToList();

      var pedido = FabricaPedido.CriarPedido(cliente, itens);

      await repositorioPedido.CriarPedidoAsync(pedido);

      servicoMensageria.GuardarCopia(pedido);      

      return new RetornoCriarPedido() { Sucesso = true, Id = pedido.Id };
    }

    public async Task<RetornoCancelarPedido> CancelarPedidoAsync(Guid id)
    {
      if (id == null || id == Guid.Empty)
        return new RetornoCancelarPedido() { Sucesso = false, Mensagem = "id Nulo." };

      var pedido = await repositorioPedido.BuscarPedidoAsync(id);

      if (pedido == null)
        return new RetornoCancelarPedido() { Sucesso = false, Mensagem = "Pedido não encontrado." };

      string mensagemValidacao;
      if (!pedido.EValidoParaCancelar(out mensagemValidacao))
        return new RetornoCancelarPedido() { Sucesso = false, Mensagem = mensagemValidacao };

      if (!pedido.Cancelar(out mensagemValidacao))
        return new RetornoCancelarPedido() { Sucesso = false, Mensagem = mensagemValidacao };

      await repositorioPedido.AtualizarPedidoAsync(pedido);

      return new RetornoCancelarPedido() { Sucesso = true, Id = pedido.Id };
    }

    #endregion

  }
}
