using System;
using System.Security.Cryptography;
using System.Text;
using NLog;

namespace teladelogin.BLL
{
    /// <summary>
    /// Classe responsável pela segurança e criptografia de senhas
    /// BLL = Business Logic Layer (Camada de Lógica de Negócio)
    /// Classe static = não precisa criar objeto, usa direto SegurancaBLL.GerarHash()
    /// </summary>
    public static class SegurancaBLL
    {
        // PBKDF2 é um algoritmo propositalmente LENTO. Isso é uma vantagem:
        // dificulta ataque de força bruta, porque o atacante precisa repetir
        // as 100.000 iterações para cada tentativa de senha.
        private const int ITERACOES = 100_000;

        // Tamanho do salt e do hash gerado, em bytes
        private const int TAMANHO_SALT = 16;   // 128 bits
        private const int TAMANHO_HASH = 32;   // 256 bits

        // Identificador do formato, para permitir trocar o algoritmo no futuro
        private const string PREFIXO = "PBKDF2";

        // Logger para registrar ações de segurança
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #region Método para gerar o hash da senha

        /// <summary>
        /// Gera o hash de uma senha usando PBKDF2-SHA256 com salt aleatório.
        ///
        /// Cada usuário recebe um salt diferente, sorteado na hora do cadastro.
        /// Por isso dois usuários com a senha "123456" ficam com hashes distintos,
        /// e uma tabela pré-calculada (rainbow table) não serve para nada.
        ///
        /// O salt não é segredo: ele é guardado junto do hash, no mesmo campo.
        /// </summary>
        /// <param name="senha">Senha em texto puro</param>
        /// <returns>String no formato PBKDF2$iteracoes$salt$hash</returns>
        public static string GerarHash(string senha)
        {
            if (string.IsNullOrEmpty(senha))
                throw new ArgumentException("Senha não pode ser vazia.", nameof(senha));

            try
            {
                logger.Info("Gerando hash de senha para usuário");

                // Sorteia um salt novo usando gerador criptográfico
                // (Random comum é previsível e não serve para segurança)
                byte[] salt = new byte[TAMANHO_SALT];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(salt);
                }

                byte[] hash = DerivarChave(senha, salt, ITERACOES);

                // Guarda tudo em um campo só: quem valida depois consegue
                // reconstruir os parâmetros a partir da própria string
                string resultado = string.Join("$",
                    PREFIXO,
                    ITERACOES.ToString(),
                    Convert.ToBase64String(salt),
                    Convert.ToBase64String(hash));

                logger.Info("Hash de senha gerado com sucesso");
                return resultado;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Erro ao gerar hash de senha");
                throw new Exception("Erro ao gerar hash de senha: " + ex.Message, ex);
            }
        }

        #endregion

        #region Método para verificar a senha

        /// <summary>
        /// Confere se a senha digitada corresponde ao hash guardado no banco.
        ///
        /// Não existe "descriptografar": o que se faz é aplicar o mesmo salt e
        /// as mesmas iterações na senha digitada e comparar os dois resultados.
        /// </summary>
        /// <param name="senha">Senha em texto puro digitada pelo usuário</param>
        /// <param name="hashArmazenado">Valor gravado no banco</param>
        /// <returns>True se a senha confere</returns>
        public static bool VerificarSenha(string senha, string hashArmazenado)
        {
            if (string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(hashArmazenado))
                return false;

            try
            {
                string[] partes = hashArmazenado.Split('$');

                if (partes.Length != 4 || partes[0] != PREFIXO)
                {
                    logger.Warn("Hash armazenado em formato desconhecido");
                    return false;
                }

                int iteracoes = int.Parse(partes[1]);
                byte[] salt = Convert.FromBase64String(partes[2]);
                byte[] hashEsperado = Convert.FromBase64String(partes[3]);

                byte[] hashCalculado = DerivarChave(senha, salt, iteracoes);

                return ComparacaoSegura(hashEsperado, hashCalculado);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Erro ao verificar senha");
                return false;
            }
        }

        #endregion

        #region Métodos auxiliares de criptografia

        /// <summary>
        /// Aplica o PBKDF2 com SHA256 sobre a senha e o salt.
        /// </summary>
        private static byte[] DerivarChave(string senha, byte[] salt, int iteracoes)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, iteracoes, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(TAMANHO_HASH);
            }
        }

        /// <summary>
        /// Compara dois hashes em tempo constante.
        ///
        /// Uma comparação normal para no primeiro byte diferente, e o tempo que
        /// ela leva denuncia quantos bytes acertaram. Aqui todos os bytes são
        /// sempre percorridos, então o tempo não vaza informação.
        /// </summary>
        private static bool ComparacaoSegura(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            int diferenca = 0;
            for (int i = 0; i < a.Length; i++)
            {
                diferenca |= a[i] ^ b[i];
            }

            return diferenca == 0;
        }

        #endregion

        #region Método para validar força da senha

        /// <summary>
        /// Verifica se uma senha é forte o suficiente
        /// Regras: mínimo 6 caracteres, pelo menos 1 número e 1 letra
        /// </summary>
        /// <param name="senha">Senha para validar</param>
        /// <returns>True se senha forte, False se fraca</returns>
        public static bool ValidarForcaSenha(string senha)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(senha))
                {
                    logger.Warn("Tentativa de validar senha vazia");
                    return false;
                }

                if (senha.Length < 6)
                {
                    logger.Info("Senha rejeitada: menos de 6 caracteres");
                    return false;
                }

                bool temNumero = false;
                bool temLetra = false;

                foreach (char c in senha)
                {
                    if (char.IsDigit(c)) temNumero = true;
                    if (char.IsLetter(c)) temLetra = true;
                }

                if (!temNumero)
                {
                    logger.Info("Senha rejeitada: sem número");
                    return false;
                }

                if (!temLetra)
                {
                    logger.Info("Senha rejeitada: sem letra");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Erro ao validar força da senha");
                return false;
            }
        }

        #endregion
    }
}
