using Dominio;
using Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

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

    private const string queryUpdateCliente = @"update Cliente set nome = @nome, cpf = @cpf where id = @id and versao = @versao";
    private const string queryUpdateEndereco = @"update Endereco set rua = @rua, numero = @numero, complemento = @complemento where idcliente = @id";

    private const string queryBuscaClientes = @"
select id, nome, cpf, rua, numero, complemento, versao from Cliente c join Endereco e on c.id = e.idCliente";

    private const string queryBuscaClientesPorId = queryBuscaClientes + @" where c.id = @id and versao = isnull(convert(binary(8),@versao), versao)";

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

    public async Task<bool> AtualizarClienteAsync(Cliente cliente)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        await connection.OpenAsync();
        var transaction = connection.BeginTransaction();

        using (SqlCommand command = new SqlCommand(queryUpdateCliente, connection, transaction))
        {
          command.Parameters.Add(new SqlParameter("@nome", cliente.Nome));
          command.Parameters.Add(new SqlParameter("@cpf", cliente.CPF));
          command.Parameters.Add(new SqlParameter("@versao", cliente.Versao));
          command.Parameters.Add(new SqlParameter("@id", cliente.Id));

          var numeroLinhasAfetadas = await command.ExecuteNonQueryAsync();
          if (numeroLinhasAfetadas <= 0)
          {
            transaction.Rollback();
            return false;
          }
        }

        using (SqlCommand command = new SqlCommand(queryUpdateEndereco, connection, transaction))
        {

          command.Parameters.Add(new SqlParameter("@rua", cliente.Endereco.Rua));
          command.Parameters.Add(new SqlParameter("@numero", cliente.Endereco.Numero));

          if (string.IsNullOrEmpty(cliente.Endereco.Complemento))
            command.Parameters.Add(new SqlParameter("@complemento", DBNull.Value));
          else
            command.Parameters.Add(new SqlParameter("@complemento", cliente.Endereco.Complemento));

          command.Parameters.Add(new SqlParameter("@id", cliente.Id));

          await command.ExecuteNonQueryAsync();
        }
        transaction.Commit();
      }

      return true;

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
            listaRetorno.Add(LerCliente(rd));
          }
        }
      }
      return listaRetorno;
    }

    private static Cliente LerCliente(SqlDataReader rd)
    {
      return new Cliente()
      {
        Id = Convert.ToInt32(rd["id"]),
        Nome = Convert.ToString(rd["nome"]),
        CPF = Convert.ToInt64(rd["cpf"]),
        Versao = (byte[])rd["versao"],
        Endereco = new Endereco()
        {
          Rua = Convert.ToString(rd["rua"]),
          Numero = Convert.ToInt32(rd["numero"]),
          Complemento = rd["complemento"] == DBNull.Value ? null : Convert.ToString(rd["complemento"])
        }
      };
    }

    public async Task<Cliente> BuscarClienteAsync(int id, byte[] versao)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        await connection.OpenAsync();

        using (SqlCommand command = new SqlCommand(queryBuscaClientesPorId, connection))
        {
          command.Parameters.Add(new SqlParameter("@id", id));

          if (versao != null)
            command.Parameters.Add(new SqlParameter("@versao", versao) { SqlDbType = System.Data.SqlDbType.VarBinary });
          else
            command.Parameters.Add(new SqlParameter("@versao", DBNull.Value));

          var rd = await command.ExecuteReaderAsync();

          if (await rd.ReadAsync())
          {
            return LerCliente(rd);
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
