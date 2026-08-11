using System;
using teladelogin.DAL;
using NLog;

namespace teladelogin.BLL
{
    /// <summary>
    /// Classe responsável pelas regras de negócio dos usuários
    /// BLL = Business Logic Layer (Camada de Lógica de Negócio)
    /// Aqui ficam as validações e regras antes de salvar no banco
    /// </summary>
    public class UsuarioBLL
    {
        // Objeto para acessar dados no banco (DAL)
        private UsuarioDAL dal = new UsuarioDAL();

        // Logger para registrar ações
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #region Método para fazer login

        /// <summary>
        /// Realiza login do usuário com validações
        /// </summary>
        /// <param name="usuario">Nome do usuário</param>
        /// <param name="senha">Senha em texto puro</param>
        /// <returns>True se login válido, False se inválido</returns>
        public bool FazerLogin(string usuario, string senha)
        {
            try
            {
                logger.Info($"Iniciando processo de login para usuário: {usuario}");

                // VALIDAÇÃO 1: Verifica se campos não estão vazios
                if (string.IsNullOrWhiteSpace(usuario))
                {
                    logger.Warn("Login rejeitado: nome de usuário vazio");
                    throw new ArgumentException("Nome de usuário é obrigatório!");
                }

                if (string.IsNullOrWhiteSpace(senha))
                {
                    logger.Warn("Login rejeitado: senha vazia");
                    throw new ArgumentException("Senha é obrigatória!");
                }

                // VALIDAÇÃO 2: Busca o hash guardado para esse usuário
                string hashArmazenado = dal.ObterHashSenha(usuario);

                // VALIDAÇÃO 3: Confere a senha digitada contra o hash.
                // Mesmo quando o usuário não existe, VerificarSenha é chamada com
                // um hash vazio e devolve false — assim o tempo de resposta não
                // denuncia se o nome de usuário existe ou não.
                bool loginValido = SegurancaBLL.VerificarSenha(senha, hashArmazenado);

                if (loginValido)
                    logger.Info($"Login realizado com sucesso para usuário: {usuario}");
                else
                    logger.Warn($"Login falhou para usuário: {usuario}");

                return loginValido;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Erro ao fazer login do usuário: {usuario}");
                throw; // Re-lança a exceção para quem chamou tratar
            }
        }

        #endregion

        #region Método para cadastrar usuário

        /// <summary>
        /// Cadastra um novo usuário com todas as validações necessárias
        /// </summary>
        /// <param name="usuario">Nome do usuário</param>
        /// <param name="senha">Senha em texto puro</param>
        /// <returns>True se cadastrou com sucesso</returns>
        public bool CadastrarUsuario(string usuario, string senha)
        {
            try
            {
                logger.Info($"Iniciando cadastro do usuário: {usuario}");

                // VALIDAÇÃO 1: Campos obrigatórios
                if (string.IsNullOrWhiteSpace(usuario))
                {
                    logger.Warn("Cadastro rejeitado: nome de usuário vazio");
                    throw new ArgumentException("Nome de usuário é obrigatório!");
                }

                if (string.IsNullOrWhiteSpace(senha))
                {
                    logger.Warn("Cadastro rejeitado: senha vazia");
                    throw new ArgumentException("Senha é obrigatória!");
                }

                // VALIDAÇÃO 2: Tamanho mínimo do nome de usuário
                if (usuario.Length < 3)
                {
                    logger.Warn($"Cadastro rejeitado: usuário muito curto ({usuario.Length} caracteres)");
                    throw new ArgumentException("Nome de usuário deve ter pelo menos 3 caracteres!");
                }

                // VALIDAÇÃO 3: Verifica força da senha
                if (!SegurancaBLL.ValidarForcaSenha(senha))
                {
                    logger.Warn("Cadastro rejeitado: senha não atende critérios de força");
                    throw new ArgumentException("Senha deve ter pelo menos 6 caracteres, incluindo números e letras!");
                }

                // VALIDAÇÃO 4: Verifica se usuário já existe
                if (dal.UsuarioExiste(usuario))
                {
                    logger.Warn($"Cadastro rejeitado: usuário já existe - {usuario}");
                    throw new ArgumentException("Este nome de usuário já existe! Escolha outro.");
                }

                // VALIDAÇÃO 5: Gera o hash da senha (com salt aleatório próprio)
                string hashSenha = SegurancaBLL.GerarHash(senha);

                // CADASTRO: Chama o DAL para salvar no banco
                bool sucesso = dal.CadastrarUsuario(usuario, hashSenha);

                if (sucesso)
                    logger.Info($"Usuário cadastrado com sucesso: {usuario}");
                else
                    logger.Error($"Falha ao cadastrar usuário: {usuario}");

                return sucesso;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Erro ao cadastrar usuário: {usuario}");
                throw; // Re-lança a exceção para o Form tratar
            }
        }

        #endregion

        #region Método para alterar senha

        /// <summary>
        /// Altera a senha de um usuário existente
        /// </summary>
        /// <param name="usuario">Nome do usuário</param>
        /// <param name="senhaAtual">Senha atual (para confirmação)</param>
        /// <param name="novaSenha">Nova senha</param>
        /// <returns>True se alterou com sucesso</returns>
        public bool AlterarSenha(string usuario, string senhaAtual, string novaSenha)
        {
            try
            {
                logger.Info($"Iniciando alteração de senha para usuário: {usuario}");

                // VALIDAÇÃO 1: Campos obrigatórios
                if (string.IsNullOrWhiteSpace(usuario) ||
                    string.IsNullOrWhiteSpace(senhaAtual) ||
                    string.IsNullOrWhiteSpace(novaSenha))
                {
                    logger.Warn("Alteração de senha rejeitada: campos vazios");
                    throw new ArgumentException("Todos os campos são obrigatórios!");
                }

                // VALIDAÇÃO 2: Verifica se senha atual está correta
                string hashArmazenado = dal.ObterHashSenha(usuario);
                if (!SegurancaBLL.VerificarSenha(senhaAtual, hashArmazenado))
                {
                    logger.Warn($"Alteração de senha rejeitada: senha atual incorreta - {usuario}");
                    throw new ArgumentException("Senha atual está incorreta!");
                }

                // VALIDAÇÃO 3: Verifica força da nova senha
                if (!SegurancaBLL.ValidarForcaSenha(novaSenha))
                {
                    logger.Warn("Alteração de senha rejeitada: nova senha fraca");
                    throw new ArgumentException("Nova senha deve ter pelo menos 6 caracteres, incluindo números e letras!");
                }

                // VALIDAÇÃO 4: Nova senha não pode ser igual à atual
                if (senhaAtual == novaSenha)
                {
                    logger.Warn("Alteração de senha rejeitada: nova senha igual à atual");
                    throw new ArgumentException("Nova senha deve ser diferente da atual!");
                }

                // ALTERAÇÃO: gera um hash novo (com salt novo) e grava no banco
                string novoHash = SegurancaBLL.GerarHash(novaSenha);
                bool sucesso = dal.AlterarSenha(usuario, novoHash);

                if (sucesso)
                    logger.Info($"Senha alterada com sucesso para usuário: {usuario}");
                else
                    logger.Error($"Falha ao alterar senha do usuário: {usuario}");

                return sucesso;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Erro ao alterar senha do usuário: {usuario}");
                throw;
            }
        }

        #endregion

        #region Método para validar usuário

        /// <summary>
        /// Valida se um nome de usuário é aceitável (sem caracteres especiais perigosos)
        /// </summary>
        /// <param name="usuario">Nome do usuário para validar</param>
        /// <returns>True se válido, False se inválido</returns>
        public bool ValidarNomeUsuario(string usuario)
        {
            try
            {
                // VALIDAÇÃO 1: Não pode ser vazio
                if (string.IsNullOrWhiteSpace(usuario))
                {
                    return false;
                }

                // VALIDAÇÃO 2: Tamanho entre 3 e 20 caracteres
                if (usuario.Length < 3 || usuario.Length > 20)
                {
                    logger.Info($"Nome de usuário rejeitado: tamanho inválido ({usuario.Length})");
                    return false;
                }

                // VALIDAÇÃO 3: Apenas letras, números e underscore
                foreach (char c in usuario)
                {
                    if (!char.IsLetterOrDigit(c) && c != '_')
                    {
                        logger.Info($"Nome de usuário rejeitado: caractere inválido '{c}'");
                        return false;
                    }
                }

                // VALIDAÇÃO 4: Deve começar com letra
                if (!char.IsLetter(usuario[0]))
                {
                    logger.Info("Nome de usuário rejeitado: deve começar com letra");
                    return false;
                }

                logger.Info($"Nome de usuário validado com sucesso: {usuario}");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Erro ao validar nome de usuário");
                return false;
            }
        }

        #endregion

        #region Método para verificar se usuário existe

        /// <summary>
        /// Verifica se um usuário já existe no sistema
        /// </summary>
        /// <param name="usuario">Nome do usuário</param>
        /// <returns>True se existe, False se não existe</returns>
        public bool UsuarioExiste(string usuario)
        {
            try
            {
                // Valida primeiro se nome é válido
                if (!ValidarNomeUsuario(usuario))
                {
                    return false;
                }

                // Chama DAL para verificar no banco
                bool existe = dal.UsuarioExiste(usuario);

                logger.Info($"Verificação de existência - usuário {usuario}: {(existe ? "EXISTE" : "NÃO EXISTE")}");
                return existe;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Erro ao verificar se usuário existe: {usuario}");
                throw;
            }
        }

        #endregion
    }
}