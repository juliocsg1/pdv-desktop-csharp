using System;
using System.Data.SqlClient;
using System.Configuration;
using NLog;

namespace teladelogin.DAL
{
    /// <summary>
    /// Classe responsável por fazer backup e restore do banco de dados
    /// DAL = Data Access Layer (Camada de Acesso a Dados)
    /// </summary>
    public class BackupRestoreDAL
    {
        // String de conexão para o banco MASTER (necessário para backup/restore)
        private readonly string conexaoMaster = ConfigurationManager.ConnectionStrings["ConexaoMaster"].ConnectionString;

        // Nome do banco de dados da aplicação
        private readonly string nomeBanco = "Lojinha";

        // Logger para registrar ações e erros
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #region Método para fazer backup

        /// <summary>
        /// Cria um backup completo do banco de dados
        /// </summary>
        /// <param name="caminhoCompleto">Caminho onde salvar o arquivo de backup (ex: C:\backups\backup.bak)</param>
        public void FazerBackup(string caminhoCompleto)
        {
            try
            {
                logger.Info($"Iniciando backup do banco {nomeBanco} para {caminhoCompleto}");

                // Comando SQL para fazer backup
                // INIT sobrescreve arquivo existente
                string comandoSql = $@"BACKUP DATABASE [{nomeBanco}] TO DISK = '{caminhoCompleto}' WITH INIT;";

                // Conecta no banco MASTER (não no banco da aplicação)
                // Isso é necessário para operações de backup
                using (SqlConnection conexao = new SqlConnection(conexaoMaster))
                {
                    using (SqlCommand cmd = new SqlCommand(comandoSql, conexao))
                    {
                        // Abre conexão
                        conexao.Open();

                        // Executa o backup (pode demorar dependendo do tamanho do banco)
                        cmd.ExecuteNonQuery();

                        logger.Info($"Backup concluído com sucesso: {caminhoCompleto}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Erro ao fazer backup para {caminhoCompleto}");
                throw new Exception("Erro ao criar backup: " + ex.Message, ex);
            }
        }

        #endregion

        #region Método para restaurar backup

        /// <summary>
        /// Restaura o banco de dados a partir de um arquivo de backup
        /// ATENÇÃO: Isso substitui completamente o banco atual!
        /// </summary>
        /// <param name="caminhoBackup">Caminho do arquivo de backup para restaurar</param>
        public void RestaurarBackup(string caminhoBackup)
        {
            try
            {
                logger.Info($"Iniciando restauração do banco {nomeBanco} a partir de {caminhoBackup}");

                // Comando SQL complexo para restaurar backup
                string comandoSql = $@"
                    USE master;
                    -- Muda para modo single user (apenas uma conexão)
                    ALTER DATABASE [{nomeBanco}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    
                    -- Restaura o backup, substituindo o banco atual
                    RESTORE DATABASE [{nomeBanco}] FROM DISK = '{caminhoBackup}' WITH REPLACE;
                    
                    -- Volta para modo multi user (várias conexões)
                    ALTER DATABASE [{nomeBanco}] SET MULTI_USER;
                ";

                // Conecta no banco MASTER para fazer a restauração
                using (SqlConnection conexao = new SqlConnection(conexaoMaster))
                {
                    using (SqlCommand cmd = new SqlCommand(comandoSql, conexao))
                    {
                        // Abre conexão
                        conexao.Open();

                        // Executa a restauração (pode demorar)
                        cmd.ExecuteNonQuery();

                        logger.Info($"Restauração concluída com sucesso a partir de {caminhoBackup}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Erro ao restaurar backup de {caminhoBackup}");
                throw new Exception("Erro ao restaurar backup: " + ex.Message, ex);
            }
        }

        #endregion

        #region Método para validar arquivo de backup

        /// <summary>
        /// Verifica se um arquivo de backup é válido
        /// </summary>
        /// <param name="caminhoBackup">Caminho do arquivo para validar</param>
        /// <returns>True se válido, False se inválido</returns>
        public bool ValidarArquivoBackup(string caminhoBackup)
        {
            try
            {
                logger.Info($"Validando arquivo de backup: {caminhoBackup}");

                // Comando SQL para verificar se o backup está íntegro
                string comandoSql = $@"RESTORE VERIFYONLY FROM DISK = '{caminhoBackup}'";

                using (SqlConnection conexao = new SqlConnection(conexaoMaster))
                {
                    using (SqlCommand cmd = new SqlCommand(comandoSql, conexao))
                    {
                        conexao.Open();

                        // Se não der erro, o backup é válido
                        cmd.ExecuteNonQuery();

                        logger.Info($"Arquivo de backup válido: {caminhoBackup}");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Arquivo de backup inválido: {caminhoBackup}");
                return false;
            }
        }

        #endregion

        #region Método para obter informações do backup

        /// <summary>
        /// Obtém informações sobre um arquivo de backup
        /// </summary>
        /// <param name="caminhoBackup">Caminho do arquivo de backup</param>
        /// <returns>String com informações do backup</returns>
        public string ObterInformacoesBackup(string caminhoBackup)
        {
            try
            {
                logger.Info($"Obtendo informações do backup: {caminhoBackup}");

                // Comando para ler informações do cabeçalho do backup
                string comandoSql = $@"RESTORE HEADERONLY FROM DISK = '{caminhoBackup}'";

                using (SqlConnection conexao = new SqlConnection(conexaoMaster))
                {
                    using (SqlCommand cmd = new SqlCommand(comandoSql, conexao))
                    {
                        conexao.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Pega algumas informações importantes do backup
                                string nomeBancoBackup = reader["DatabaseName"].ToString();
                                DateTime dataBackup = Convert.ToDateTime(reader["BackupStartDate"]);
                                string tipoBackup = reader["BackupType"].ToString();

                                string info = $"Banco: {nomeBancoBackup}\n" +
                                            $"Data: {dataBackup:dd/MM/yyyy HH:mm:ss}\n" +
                                            $"Tipo: {(tipoBackup == "1" ? "Full" : "Incremental")}";

                                logger.Info($"Informações do backup obtidas: {nomeBancoBackup}");
                                return info;
                            }
                            else
                            {
                                return "Não foi possível ler informações do backup";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Erro ao obter informações do backup: {caminhoBackup}");
                return "Erro ao ler arquivo de backup: " + ex.Message;
            }
        }

        #endregion
    }
}