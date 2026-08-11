using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;
using NLog;

namespace teladelogin.DAL
{
    /// <summary>
    /// Classe responsável por acessar dados das vendas no banco de dados
    /// DAL = Data Access Layer (Camada de Acesso a Dados)
    /// </summary>
    public class VendasDAL
    {
        // String de conexão pega do arquivo App.config
        private readonly string conexaoString = ConfigurationManager.ConnectionStrings["ConexaoLojinha"].ConnectionString;

        // Logger para registrar ações e erros
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #region Método para buscar imagem por ID da venda

        /// <summary>
        /// Busca a foto de uma venda específica no banco de dados
        /// </summary>
        /// <param name="vendaID">ID da venda</param>
        /// <returns>Array de bytes da imagem ou null se não encontrar</returns>
        public byte[] BuscarImagemPorVendaID(int vendaID)
        {
            // Variável que vai guardar os bytes da imagem
            byte[] imagemBytes = null;

            using (SqlConnection con = new SqlConnection(conexaoString))
            {
                // Query para buscar apenas a foto da venda específica
                string sql = "SELECT Foto FROM Vendas WHERE VendaID = @VendaID";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    // Parâmetro seguro contra SQL Injection
                    cmd.Parameters.AddWithValue("@VendaID", vendaID);

                    con.Open();

                    // ExecuteScalar retorna apenas um valor (a foto)
                    object result = cmd.ExecuteScalar();

                    // Se encontrou algo e não é nulo
                    if (result != DBNull.Value && result != null)
                    {
                        // Converte o resultado para array de bytes
                        byte[] bytes = (byte[])result;

                        try
                        {
                            // Cria um MemoryStream para trabalhar com a imagem
                            using (MemoryStream ms = new MemoryStream(bytes))
                            {
                                // Cria uma imagem a partir dos bytes
                                using (Image imagem = Image.FromStream(ms))
                                {
                                    // Cria novo stream para salvar no formato PNG
                                    using (MemoryStream msNovo = new MemoryStream())
                                    {
                                        // Converte para PNG (formato padrão)
                                        imagem.Save(msNovo, System.Drawing.Imaging.ImageFormat.Png);

                                        // Retorna os bytes da imagem convertida
                                        imagemBytes = msNovo.ToArray();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Se deu erro ao converter imagem, registra e lança exceção
                            logger.Error(ex, $"Erro ao converter imagem da venda {vendaID}");
                            throw new Exception("Erro ao converter imagem: " + ex.Message);
                        }
                    }
                }
            }

            return imagemBytes; // Retorna bytes da imagem ou null
        }

        #endregion

        #region Método para salvar venda

        /// <summary>
        /// Salva uma nova venda no banco de dados
        /// </summary>
        /// <param name="descricao">Descrição do produto</param>
        /// <param name="quantidade">Quantidade vendida</param>
        /// <param name="precoUnitario">Preço por unidade</param>
        /// <param name="precoTotal">Preço total calculado</param>
        /// <param name="formaPagamento">Como foi pago (cartão, dinheiro, etc.)</param>
        /// <param name="imagem">Foto do produto em bytes</param>
        /// <returns>ID da venda criada ou 0 se deu erro</returns>
        public int SalvarVenda(string descricao, int quantidade, decimal precoUnitario, decimal precoTotal, string formaPagamento, byte[] imagem)
        {
            // Variável para guardar o ID da venda criada
            int vendaID = 0;

            // Query SQL com SCOPE_IDENTITY() para pegar o ID gerado
            string query = @"
                INSERT INTO Vendas (ProdutoDescricao, Quantidade, PrecoUnitario, PrecoTotal, FormaPagamento, Foto)
                VALUES (@Descricao, @Quantidade, @PrecoUnitario, @PrecoTotal, @FormaPagamento, @Foto);

                SELECT SCOPE_IDENTITY();"; // Retorna o ID da linha inserida

            using (SqlConnection con = new SqlConnection(conexaoString))
            {
                try
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Parâmetros seguros contra SQL Injection
                        cmd.Parameters.AddWithValue("@Descricao", descricao);
                        cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                        cmd.Parameters.AddWithValue("@PrecoUnitario", precoUnitario);
                        cmd.Parameters.AddWithValue("@PrecoTotal", precoTotal);
                        cmd.Parameters.AddWithValue("@FormaPagamento", formaPagamento);

                        // Para imagem: se for null, salva DBNull.Value
                        cmd.Parameters.Add("@Foto", SqlDbType.VarBinary).Value = imagem ?? (object)DBNull.Value;

                        // ExecuteScalar retorna o ID gerado pelo SCOPE_IDENTITY()
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            vendaID = Convert.ToInt32(result);
                            logger.Info($"Venda salva com sucesso. ID: {vendaID}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Erro ao salvar venda");
                    throw new Exception("Erro ao salvar venda: " + ex.Message, ex);
                }
            }

            return vendaID; // Retorna o ID da venda criada
        }

        #endregion

        #region Método para atualizar venda

        /// <summary>
        /// Atualiza uma venda existente no banco de dados
        /// </summary>
        /// <param name="vendaID">ID da venda a ser atualizada</param>
        /// <param name="descricao">Nova descrição</param>
        /// <param name="quantidade">Nova quantidade</param>
        /// <param name="precoUnitario">Novo preço unitário</param>
        /// <param name="precoTotal">Novo preço total</param>
        /// <param name="formaPagamento">Nova forma de pagamento</param>
        /// <returns>True se atualizou, False se não conseguiu</returns>
        public bool AtualizarVenda(int vendaID, string descricao, int quantidade, decimal precoUnitario, decimal precoTotal, string formaPagamento)
        {
            using (SqlConnection con = new SqlConnection(conexaoString))
            {
                try
                {
                    con.Open();

                    // Query UPDATE para modificar dados existentes
                    string query = @"
                        UPDATE Vendas 
                        SET ProdutoDescricao = @Descricao, 
                            Quantidade = @Quantidade, 
                            PrecoUnitario = @PrecoUnitario, 
                            PrecoTotal = @PrecoTotal, 
                            FormaPagamento = @FormaPagamento
                        WHERE VendaID = @VendaID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Parâmetros seguros contra SQL Injection
                        cmd.Parameters.AddWithValue("@Descricao", descricao);
                        cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                        cmd.Parameters.AddWithValue("@PrecoUnitario", precoUnitario);
                        cmd.Parameters.AddWithValue("@PrecoTotal", precoTotal);
                        cmd.Parameters.AddWithValue("@FormaPagamento", formaPagamento);
                        cmd.Parameters.AddWithValue("@VendaID", vendaID);

                        // ExecuteNonQuery retorna quantas linhas foram afetadas
                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            logger.Info($"Venda {vendaID} atualizada com sucesso");
                            return true;
                        }
                        else
                        {
                            logger.Warn($"Nenhuma venda encontrada com ID {vendaID}");
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"Erro ao atualizar venda {vendaID}");
                    throw new Exception("Erro ao atualizar a venda: " + ex.Message, ex);
                }
            }
        }

        #endregion

        #region Método para excluir venda

        /// <summary>
        /// Exclui uma venda do banco de dados
        /// </summary>
        /// <param name="vendaID">ID da venda a ser excluída</param>
        /// <returns>True se excluiu, False se não conseguiu</returns>
        public bool ExcluirVenda(int vendaID)
        {
            using (SqlConnection conexao = new SqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();

                    // Query DELETE para remover a venda
                    string query = "DELETE FROM Vendas WHERE VendaID = @VendaID";

                    using (SqlCommand cmd = new SqlCommand(query, conexao))
                    {
                        // Parâmetro seguro
                        cmd.Parameters.AddWithValue("@VendaID", vendaID);

                        // ExecuteNonQuery retorna quantas linhas foram excluídas
                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            logger.Info($"Venda {vendaID} excluída com sucesso");
                            return true;
                        }
                        else
                        {
                            logger.Warn($"Nenhuma venda encontrada com ID {vendaID}");
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"Erro ao excluir venda {vendaID}");
                    throw new Exception("Erro ao excluir venda: " + ex.Message, ex);
                }
            }
        }

        #endregion

        #region Método para listar todas as vendas

        /// <summary>
        /// Lista todas as vendas do banco para preencher um DataGridView
        /// </summary>
        /// <returns>DataTable com todas as vendas</returns>
        public DataTable ListarVendas()
        {
            // DataTable para guardar os dados das vendas
            DataTable tabela = new DataTable();

            // Query para buscar todas as colunas das vendas
            string query = "SELECT VendaID, ProdutoDescricao, Quantidade, PrecoUnitario, PrecoTotal, FormaPagamento, Foto FROM Vendas";

            using (SqlConnection conexao = new SqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();

                    // SqlDataAdapter preenche o DataTable automaticamente
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexao);
                    adaptador.Fill(tabela); // Preenche o DataTable com dados do banco

                    logger.Info($"Listadas {tabela.Rows.Count} vendas");
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Erro ao listar vendas");
                    throw new Exception("Erro ao listar as vendas: " + ex.Message);
                }
            }

            return tabela; // Retorna a tabela com todas as vendas
        }

        #endregion

        #region Método auxiliar para converter imagem

        /// <summary>
        /// Converte uma imagem (Image) para array de bytes
        /// Método auxiliar usado internamente
        /// </summary>
        /// <param name="imagem">Imagem a ser convertida</param>
        /// <returns>Array de bytes da imagem ou null</returns>
        private byte[] ConverterImagemParaBytes(Image imagem)
        {
            // Se imagem for null, retorna null
            if (imagem == null) return null;

            // Usa MemoryStream para converter imagem em bytes
            using (MemoryStream ms = new MemoryStream())
            {
                // Salva a imagem no stream como PNG
                imagem.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                // Retorna os bytes da imagem
                return ms.ToArray();
            }
        }

        #endregion
    }
}