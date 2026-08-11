using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using teladelogin.DAL;

namespace teladelogin.UI
{
    public partial class FrmBKeRestore : Form
    {
        private readonly string pastaBackup = @"C:\BK_Lojinha\";
        private BackupRestoreDAL backupDAL = new BackupRestoreDAL(); 
        public FrmBKeRestore()
        {
            InitializeComponent();
        }

        private void btnBK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(pastaBackup))
                    Directory.CreateDirectory(pastaBackup);

                string dataHora = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string nomeBase = $"Lojinha_Backup_{dataHora}";
                string caminhoCompleto = Path.Combine(pastaBackup, nomeBase + ".bak");

                // Impede path injection
                caminhoCompleto = Path.GetFullPath(caminhoCompleto);
                if (!caminhoCompleto.StartsWith(Path.GetFullPath(pastaBackup)))
                    throw new UnauthorizedAccessException("O caminho do backup não é permitido.");

                // Verifica duplicados
                int contador = 1;
                while (File.Exists(caminhoCompleto))
                {
                    string nomeIncrementado = $"{nomeBase}_{contador}.bak";
                    caminhoCompleto = Path.Combine(pastaBackup, nomeIncrementado);
                    contador++;
                }

                backupDAL.FazerBackup(caminhoCompleto);

                MessageBox.Show($"Backup realizado com sucesso em:\n{caminhoCompleto}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogHelper.RegistrarErro(ex);
                MessageBox.Show("Erro ao realizar backup:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void btnSelecionarRes_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Arquivos de backup (*.bak)|*.bak",
                InitialDirectory = pastaBackup,
                Title = "Selecione o arquivo de backup"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtCaminho.Text = dialog.FileName;
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCaminho.Text) || !File.Exists(txtCaminho.Text))
            {
                MessageBox.Show("Por favor, selecione um arquivo de backup válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                backupDAL.RestaurarBackup(txtCaminho.Text);

                MessageBox.Show("Restaurado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao restaurar o banco de dados:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static class LogHelper
        {
            public static void RegistrarErro(Exception ex)
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "erros_log.txt");
                File.AppendAllText(logPath, $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - {ex.Message}{Environment.NewLine}");
            }
        }
    }
}
