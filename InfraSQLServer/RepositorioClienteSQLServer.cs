using Dominio;
using Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InfraSQLServer
{
  public class RepositorioClienteSQLServer : IRepositorioCliente
  {
    private const string queryInsertCliente = @"
insert into Cliente (nome, cpf)
values(@nome, @cpf)
select @id = @@identity
insert into Endereco (idCliente, rua, numero, complemento)
values(@id, @rua, @numero, @complemento)";

    private const string queryUpdateCliente = @"
update Cliente set nome = @nome, cpf = @cpf where id = @id
update Endereco set rua = @rua, numero = @numero, complemento = @complemento where idcliente = @id";

    private const string queryBuscaClientes = @"
select id, nome, cpf, rua, numero, complemento from Cliente c join Endereco e on c.id = e.idCliente";

    private const string queryBuscaClientesPorId = queryBuscaClientes + @" where c.id = @id";

    private string connectionString;
    public RepositorioClienteSQLServer(string connectionString)
    {
      this.connectionString = connectionString;
    }
    public async Task CadastrarClienteAsync(Cliente cliente)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        await connection.OpenAsync();

        using (SqlCommand command = new SqlCommand(queryInsertCliente, connection))
        {
          command.Parameters.Add(new SqlParameter("@nome", cliente.Nome));
          command.Parameters.Add(new SqlParameter("@cpf", cliente.CPF));
          command.Parameters.Add(new SqlParameter("@rua", cliente.Endereco.Rua));
          command.Parameters.Add(new SqlParameter("@numero", cliente.Endereco.Numero));

          if (string.IsNullOrEmpty(cliente.Endereco.Complemento))
            command.Parameters.Add(new SqlParameter("@complemento", DBNull.Value));
          else
            command.Parameters.Add(new SqlParameter("@complemento", cliente.Endereco.Complemento));

          var paramId = new SqlParameter("@id", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };
          command.Parameters.Add(paramId);
          await command.ExecuteNonQueryAsync();
          cliente.Id = Convert.ToInt32(paramId.Value);
        }
      }
    }

    public async Task AtualizarClienteAsync(Cliente cliente)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        await connection.OpenAsync();

        using (SqlCommand command = new SqlCommand(queryUpdateCliente, connection))
        {
          command.Parameters.Add(new SqlParameter("@nome", cliente.Nome));
          command.Parameters.Add(new SqlParameter("@cpf", cliente.CPF));
          command.Parameters.Add(new SqlParameter("@rua", cliente.Endereco.Rua));
          command.Parameters.Add(new SqlParameter("@numero", cliente.Endereco.Numero));

          if (string.IsNullOrEmpty(cliente.Endereco.Complemento))
            command.Parameters.Add(new SqlParameter("@complemento", DBNull.Value));
          else
            command.Parameters.Add(new SqlParameter("@complemento", cliente.Endereco.Complemento));

          command.Parameters.Add(new SqlParameter("@id", cliente.Id));

          var numeroLinhasAfetadas = await command.ExecuteNonQueryAsync();
        }
      }
    }

    public async Task<List<Cliente>> BuscarClientesAsync()
    {
      List<Cliente> listaRetorno = new List<Cliente>();

      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        await connection.OpenAsync();

        using (SqlCommand command = new SqlCommand(queryBuscaClientes, connection))
        {
          var rd = await command.ExecuteReaderAsync();

          while (await rd.ReadAsync())
          {
            listaRetorno.Add(new Cliente()
            {
              Id = Convert.ToInt32(rd["id"]),
              Nome = Convert.ToString(rd["nome"]),
              CPF = Convert.ToInt64(rd["cpf"]),
              Endereco = new Endereco()
              {
                Rua = Convert.ToString(rd["rua"]),
                Numero = Convert.ToInt32(rd["numero"]),
                Complemento = rd["complemento"] == DBNull.Value ? null : Convert.ToString(rd["complemento"])
              }
            });
          }
        }
      }
      return listaRetorno;
    }

    public async Task<Cliente> BuscarClienteAsync(int id)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        await connection.OpenAsync();

        using (SqlCommand command = new SqlCommand(queryBuscaClientesPorId, connection))
        {
          command.Parameters.Add(new SqlParameter("@id", id));
          
          var rd = await command.ExecuteReaderAsync();

          if (await rd.ReadAsync())
          {
            return new Cliente()
            {
              Id = Convert.ToInt32(rd["id"]),
              Nome = Convert.ToString(rd["nome"]),
              CPF = Convert.ToInt64(rd["cpf"]),
              Endereco = new Endereco()
              {
                Rua = Convert.ToString(rd["rua"]),
                Numero = Convert.ToInt32(rd["numero"]),
                Complemento = rd["complemento"] == DBNull.Value ? null : Convert.ToString(rd["complemento"])
              }
            };
          }
          else
          {
            return null;
          }
        }
      }
    }
  }
}
