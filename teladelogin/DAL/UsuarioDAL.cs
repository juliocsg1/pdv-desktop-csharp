using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using teladelogin.BLL;
using NLog;

namespace teladelogin.DAL
{
    /// <summary>
    /// Classe responsável por acessar dados dos usuários no banco de dados
    /// DAL = Data Access Layer (Camada de Acesso a Dados)
    /// </summary>
    public class UsuarioDAL
    {
        // String de conexão pega do arquivo App.config
        private readonly string conexaoString = ConfigurationManager.ConnectionStrings["ConexaoLojinha"].ConnectionString;

        // Logger para registrar ações e erros
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #region Método para buscar o hash da senha

        /// <summary>
        /// Busca o hash de senha guardado para um usuário.
        ///
        /// A comparação de senha NÃO é feita em SQL. Como cada usuário tem um
        /// salt aleatório, o banco não tem como recalcular o hash — quem faz
        /// isso é a BLL, com SegurancaBLL.VerificarSenha.
        /// </summary>
        /// <param name="usuario">Nome do usuário</param>
        /// <returns>Hash armazenado, ou null se o usuário não existir</returns>
        public string ObterHashSenha(string usuario)
        {
            logger.Info($"Buscando hash de senha do usuário: {usuario}");

            // Query SQL usando parâmetros para evitar SQL Injection
            string query = "SELECT Senha FROM Usuarios WHERE Usuario = @Usuario";

            // Using garante que conexão seja fechada automaticamente
            using (SqlConnection conexao = new SqlConnection(conexaoString))
            using (SqlCommand comando = new SqlCommand(query, conexao))
            {
                comando.Parameters.AddWithValue("@Usuario", usuario);

                try
                {
                    conexao.Open();

                    // ExecuteScalar devolve null quando não há linha
                    object resultado = comando.ExecuteScalar();

                    if (resultado == null || resultado == DBNull.Value)
                    {
                        logger.Warn($"Usuário não encontrado: {usuario}");
                        return null;
                    }

                    return resultado.ToString();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"Erro ao buscar hash do usuário {usuario}");
                    throw new Exception("Erro ao buscar dados do usuário: " + ex.Message, ex);
                }
                // Conexão fecha automaticamente aqui (using)
            }
        }

        #endregion

        #region Método para alterar a senha

        /// <summary>
        /// Grava um novo hash de senha para um usuário existente
        /// </summary>
        /// <param name="usuario">Nome do usuário</param>
        /// <param name="novoHash">Novo hash já gerado pela SegurancaBLL</param>
        /// <returns>True se alterou com sucesso</returns>
        public bool AlterarSenha(string usuario, string novoHash)
        {
            string query = "UPDATE Usuarios SET Senha = @Senha WHERE Usuario = @Usuario";

            using (SqlConnection conexao = new SqlConnection(conexaoString))
            using (SqlCommand comando = new SqlCommand(query, conexao))
            {
                comando.Parameters.AddWithValue("@Usuario", usuario);
                comando.Parameters.AddWithValue("@Senha", novoHash);

                try
                {
                    conexao.Open();
                    int linhasAfetadas = comando.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"Erro ao alterar senha do usuário {usuario}");
                    throw new Exception("Erro ao alterar senha: " + ex.Message, ex);
                }
            }
        }

        #endregion

        #region Método para cadastrar usuário

        /// <summary>
        /// Cadastra um novo usuário no banco de dados
        /// </summary>
        /// <param name="usuario">Nome do usuário</param>
        /// <param name="senhaCriptografada">Senha já criptografada</param>
        /// <returns>True se cadastrou com sucesso, False se deu erro</returns>
        public bool CadastrarUsuario(string usuario, string senhaCriptografada)
        {
            // Using garante que conexão seja fechada automaticamente
            using (SqlConnection conexao = new SqlConnection(conexaoString))
            {
                try
                {
                    // Abre conexão
                    conexao.Open();

                    // Query para inserir novo usuário
                    string query = "INSERT INTO Usuarios (Usuario, Senha) VALUES (@Usuario, @Senha)";

                    // Executa o comando
                    using (SqlCommand cmd = new SqlCommand(query, conexao))
                    {
                        // Parâmetros seguros contra SQL Injection
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@Senha", senhaCriptografada);

                        // ExecuteNonQuery retorna quantas linhas foram afetadas
                        // Se > 0, significa que inseriu com sucesso
                        int linhasAfetadas = cmd.ExecuteNonQuery();
                        return linhasAfetadas > 0;
                    }
                }
                catch (Exception ex)
                {
                    // Se deu erro, lança exceção com mensagem clara
                    throw new Exception("Erro ao cadastrar usuário: " + ex.Message, ex);
                }
                // Conexão fecha automaticamente aqui (using)
            }
        }

        #endregion

        #region Método para verificar se usuário já existe

        /// <summary>
        /// Verifica se um usuário já existe no banco de dados
        /// </summary>
        /// <param name="usuario">Nome do usuário para verificar</param>
        /// <returns>True se usuário existe, False se não existe</returns>
        public bool UsuarioExiste(string usuario)
        {
            // Query para contar quantos usuários com esse nome existem
            string query = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @Usuario";

            // Using garante fechamento automático da conexão
            using (SqlConnection conexao = new SqlConnection(conexaoString))
            using (SqlCommand comando = new SqlCommand(query, conexao))
            {
                // Parâmetro seguro contra SQL Injection
                comando.Parameters.AddWithValue("@Usuario", usuario);

                try
                {
                    // Abre conexão
                    conexao.Open();

                    // Conta quantos usuários existem com esse nome
                    int quantidade = (int)comando.ExecuteScalar();

                    // Se quantidade > 0, significa que usuário existe
                    return quantidade > 0;
                }
                catch (Exception ex)
                {
                    // Se deu erro, lança exceção
                    throw new Exception("Erro ao verificar se usuário existe: " + ex.Message, ex);
                }
                // Conexão fecha automaticamente (using)
            }
        }

        #endregion
    }
}