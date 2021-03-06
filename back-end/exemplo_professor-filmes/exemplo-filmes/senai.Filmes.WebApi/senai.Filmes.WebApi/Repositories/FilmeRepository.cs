using senai.Filmes.WebApi.Domains;
using senai.Filmes.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Filmes.WebApi.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private string stringConexao = "Data Source=DESKTOP-GCOFA7F\\SQLEXPRESS; initial catalog=Filmes_tarde; user Id=sa; pwd=sa@132";
        public void atualizarIdCorpo(FilmeDomain filme)
        {

            using (SqlConnection con = new SqlConnection (stringConexao))
            {
                string queryUpdate = "UPDATE Filmes SET Nome = @Nome WHERE IdFilme = @ID";

                using (SqlCommand cmd = new SqlCommand (queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@ID", filme.IdFilme);
                    cmd.Parameters.AddWithValue("@Nome", filme.Titulo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void atualizarIdUrl(int id, FilmeDomain filme)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE Filmes SET Nome = @Nome WHERE IdFilme = @ID";

                using (SqlCommand cmd = new SqlCommand (queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Nome", filme.Titulo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public FilmeDomain buscarPorId(int id)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string querySelectById = "SELECT IdFilme, Nome FROM Filmes WHERE IdFilme = @ID";
                
                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader fazer a leitura no banco de dados
                SqlDataReader rdr;

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Executa a query
                    rdr = cmd.ExecuteReader();

                    // Caso a o resultado da query possua registro
                    if (rdr.Read())
                    {
                        // Cria um objeto genero
                        FilmeDomain filme = new FilmeDomain
                        {
                            // Atribui à propriedade IdGenero o valor da coluna "IdGenero" da tabela do banco
                            IdFilme = Convert.ToInt32(rdr["IdFilme"])

                            // Atribui à propriedade Nome o valor da coluna "Nome" da tabela do banco
                            ,Titulo = rdr["Titulo"].ToString()
                        };

                        // Retorna o genero com os dados obtidos
                        return filme;
                    }

                    // Caso o resultado da query não possua registros, retorna null
                    return null;
                }
            }
        }

        public void Cadastrar(FilmeDomain filme)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Filmes(Titulo) VALUES (@Titulo)";


                SqlCommand cmd = new SqlCommand(queryInsert, con);

                cmd.Parameters.AddWithValue("@Titulo", filme.Nome);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM Filmes WHERE IdFilme = @ID";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@ID",id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

            public List<GeneroDomain> Listar()
        {
            List<FilmeDomain> filmes = new List<FilmeDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT IdFilme, Nome from Filmes";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        FilmeDomain filme = new FilmeDomain
                        {
                            IdFilme = Convert.ToInt32(rdr[0]),


                            Titulo = rdr["Titulo"].ToString()
                        };

                        generos.Add(filme);
                    }
                }
            }
            return filmes;
        }
    }
}


