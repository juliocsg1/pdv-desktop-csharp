using System;
using NLog;

namespace teladelogin.BLL
{
    /// <summary>
    /// Classe para calcular e validar vendas
    /// Versão SIMPLIFICADA para alunos iniciantes
    /// </summary>
    public static class VendaBLL
    {
        // Logger para registrar erros (opcional)
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #region Calcular Total da Venda

        /// <summary>
        /// Multiplica preço x quantidade = total
        /// </summary>
        /// <param name="preco">Preço unitário</param>
        /// <param name="quantidade">Quantidade</param>
        /// <returns>Preço total</returns>
        public static decimal CalcularTotal(decimal preco, int quantidade)
        {
            // Validação simples: não pode ser negativo ou zero
            if (preco <= 0)
                throw new Exception("Preço deve ser maior que zero!");

            if (quantidade <= 0)
                throw new Exception("Quantidade deve ser maior que zero!");

            // Faz a multiplicação
            decimal total = preco * quantidade;

            // Registra no log (opcional)
            logger.Info($"Calculado: {quantidade} x {preco:C} = {total:C}");

            return total;
        }

        #endregion

        #region Validar Campos da Venda

        /// <summary>
        /// Verifica se os campos estão preenchidos corretamente
        /// </summary>
        /// <param name="descricao">Descrição do produto</param>
        /// <param name="quantidade">Quantidade vendida</param>
        /// <param name="preco">Preço unitário</param>
        /// <returns>True se tudo OK</returns>
        public static bool ValidarCampos(string descricao, int quantidade, decimal preco)
        {
            // Verifica se descrição não está vazia
            if (string.IsNullOrEmpty(descricao))
            {
                throw new Exception("Descrição é obrigatória!");
            }

            // Verifica se descrição não é muito curta
            if (descricao.Length < 3)
            {
                throw new Exception("Descrição deve ter pelo menos 3 caracteres!");
            }

            // Verifica quantidade
            if (quantidade <= 0)
            {
                throw new Exception("Quantidade deve ser maior que zero!");
            }

            // Verifica preço
            if (preco <= 0)
            {
                throw new Exception("Preço deve ser maior que zero!");
            }

            // Se chegou até aqui, está tudo OK
            logger.Info("Validação da venda: OK");
            return true;
        }

        #endregion
    }
}