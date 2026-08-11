using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using teladelogin.DAL;
using teladelogin.BLL;
using teladelogin.UI;
using NLog;

namespace teladelogin
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #region Método LinkClicked
        private void lnkNovoCadastro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmCadastroUsuario abreCadastro = new FrmCadastroUsuario();
            abreCadastro.ShowDialog();
        }

        private void likEsqueciSenha_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Funcionalidade 'Esqueci minha senha' ainda não implementada.");
        }
        #endregion

        #region Método Click

        private void btnLogs_Click(object sender, EventArgs e)
        {
            FrmLogs frmLogs = new FrmLogs();
            frmLogs.Show();
        }

        private void btnBackUpERestore_Click(object sender, EventArgs e)
        {
            // Cria uma nova instância da tela PDV
            FrmBKeRestore abrirRes = new FrmBKeRestore();
            abrirRes.Show();
        }
        private void btnLogar_Click(object sender, EventArgs e)
        {
            string usuario = txtLogin.Text;
            string senha = txtSenha.Text;

            logger.Info($"Botão de login clicado para usuário: {usuario}");

            try
            {
                if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(senha))
                {
                    logger.Warn("Campos de login vazios");
                    MessageBox.Show("Preencha todos os campos.");
                    return;
                }

                // A tela conversa apenas com a BLL. Toda a regra de autenticação
                // (hash, salt e comparação) fica na camada de negócio.
                UsuarioBLL bll = new UsuarioBLL();
                bool loginValido = bll.FazerLogin(usuario, senha);

                if (loginValido)
                {
                    logger.Info($"Login bem-sucedido para usuário: {usuario}");
                    FrmPDV abrirPDV = new FrmPDV();
                    this.Hide();
                    abrirPDV.Show();
                }
                else
                {
                    logger.Warn($"Login falhou para usuário: {usuario}");
                    MessageBox.Show("Usuário ou senha incorretos");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Erro ao processar login");
                MessageBox.Show("Erro ao tentar logar.");
            }


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Exibe a mensagem perguntando se o usuário quer sair ou continuar
            var resultado = MessageBox.Show("Deseja sair?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Verifica a resposta do usuário
            if (resultado == DialogResult.Yes)
            {
                //Fechar o sistema (fechar o formulário de login ou a aplicação)
                Application.Exit();
            }
            else
            {
                //Limpar os campos do texto
                txtLogin.Clear();
                txtSenha.Clear();

                //Retornar o foco ao campo login
                txtLogin.Focus();
            }
        }
        #endregion

        #region Método TextChanged
        private void txtLogin_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSenha.Focus(); // Mover o foco para o campo de senha
                e.SuppressKeyPress = true;
            }
        }
        #endregion

        #region Método KeyDown
        private void txtLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSenha.Focus(); // Mover o foco para o campo de senha
                e.SuppressKeyPress = true; // Impede a ação
            }
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true; // Impede a ação
            }

            if (e.KeyCode == Keys.Enter)
            {
                btnLogar.PerformClick();    // Simula o clique no botão logar
                e.SuppressKeyPress = true; // Impede a ação
            }
        }
        #endregion

       

    }

}