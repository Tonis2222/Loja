using Dominio;
using Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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


    private string connectionString;
    public RepositorioClienteSQLServer(string connectionString)
    {
      this.connectionString = connectionString;

    }
    public void CadastrarCliente(Cliente cliente)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();

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
          command.ExecuteNonQuery();
          cliente.Id = Convert.ToInt32(paramId.Value);
        }
      }
    }

    public void AtualizarCliente(Cliente cliente)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();

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

          var numeroLinhasAfetadas = command.ExecuteNonQuery();
        }
      }
    }

    public List<Cliente> BuscarClientes()
    {
      List<Cliente> listaRetorno = new List<Cliente>();

      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();

        using (SqlCommand command = new SqlCommand(queryBuscaClientes, connection))
        {
          var rd = command.ExecuteReader();

          while (rd.Read())
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

  }
}
