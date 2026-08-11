using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using teladelogin.BLL;
using teladelogin.DAL;


namespace teladelogin
{
    public partial class FrmCadastroUsuario : Form
    {
        public FrmCadastroUsuario()
        {
            InitializeComponent();
        }

        #region Método Click
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            FrmLogin abreLogin = new FrmLogin();
            abreLogin.Show();
            this.Close();

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Salvar();
        }
        #endregion


        #region Salvar
        public void Salvar()
        {
            string usuario = txtCriaLogin.Text.Trim();
            string senhaACriptografar = txtCriaSenha.Text.Trim();


            UsuarioBLL bll = new UsuarioBLL();

            try
            {
                bool usuarioCadastrado = bll.CadastrarUsuario(usuario, senhaACriptografar);

                if (usuarioCadastrado)
                {
                    MessageBox.Show("Usuário cadastrado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCriaLogin.Clear();
                    txtCriaSenha.Clear();
                    btnCancelar.Focus();
                }
            }
            catch (ArgumentException ex)
            {
                // Erro de validação: usuário já existe ou campos vazios
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex) // Erros do banco de dados
            {
                MessageBox.Show("Erro no banco de dados: " + ex.Message, "Erro de Banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Erro geral: problema no banco, conexão ou outros imprevistos
                MessageBox.Show("Erro ao cadastrar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }





        #endregion

    }
}
