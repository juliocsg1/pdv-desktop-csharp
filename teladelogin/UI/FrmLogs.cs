using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace teladelogin.UI
{
    public partial class FrmLogs : Form
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private Timer timerAtualizacao;
        private string caminhoLogs;
        private long ultimoTamanhoArquivo = 0;

        public FrmLogs()
        {
            InitializeComponent();
            ConfigurarForm();
            ConfigurarTimer();
        }

        private string LerArquivoCompartilhado(string caminhoArquivo)
        {
            int tentativas = 0;
            const int maxTentativas = 5;

            while (tentativas < maxTentativas)
            {
                try
                {
                    // Abre o arquivo com compartilhamento de leitura e escrita
                    using (FileStream fs = new FileStream(caminhoArquivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
                catch (IOException ex) when (tentativas < maxTentativas - 1)
                {
                    tentativas++;
                    // Aguarda um pouco antes de tentar novamente
                    System.Threading.Thread.Sleep(100);
                }
            }

            throw new IOException($"Não foi possível acessar o arquivo após {maxTentativas} tentativas");
        }

        private void ConfigurarForm()
        {
            // Configurar o caminho dos logs
            caminhoLogs = Path.Combine(Application.StartupPath, "logs");

            // Log de abertura da tela
            logger.Info("Tela de logs aberta pelo usuário");

            // Carregar logs iniciais
            CarregarLogs();
        }

        private void ConfigurarTimer()
        {
            // Timer para atualizar logs automaticamente a cada 2 segundos
            timerAtualizacao = new Timer();
            timerAtualizacao.Interval = 2000; // 2 segundos
            timerAtualizacao.Tick += TimerAtualizacao_Tick;
            timerAtualizacao.Start();
        }

        private void TimerAtualizacao_Tick(object sender, EventArgs e)
        {
            if (chkAtualizacaoAutomatica.Checked)
            {
                VerificarNovasEntradas();
            }
        }

        private void CarregarLogs()
        {
            try
            {
                string filtroArquivo = ObterFiltroArquivo();
                string caminhoArquivo = Path.Combine(caminhoLogs, filtroArquivo);

                if (File.Exists(caminhoArquivo))
                {
                    string conteudo = LerArquivoCompartilhado(caminhoArquivo);
                    txtLogs.Text = conteudo;

                    // Rolar para o final
                    txtLogs.SelectionStart = txtLogs.Text.Length;
                    txtLogs.ScrollToCaret();

                    // Atualizar informações
                    ultimoTamanhoArquivo = new FileInfo(caminhoArquivo).Length;
                    AtualizarInformacoes(caminhoArquivo);
                }
                else
                {
                    txtLogs.Text = $"Arquivo de log não encontrado: {caminhoArquivo}\r\n\r\n" +
                                  "Dicas:\r\n" +
                                  "- Execute algumas ações no sistema para gerar logs\r\n" +
                                  "- Verifique se o NLog.config está configurado corretamente\r\n" +
                                  "- A pasta de logs será criada automaticamente quando houver o primeiro log";
                }
            }
            catch (Exception ex)
            {
                txtLogs.Text = $"Erro ao carregar logs: {ex.Message}";
                logger.Error(ex, "Erro ao carregar logs na tela de visualização");
            }
        }

        private void VerificarNovasEntradas()
        {
            try
            {
                string filtroArquivo = ObterFiltroArquivo();
                string caminhoArquivo = Path.Combine(caminhoLogs, filtroArquivo);

                if (File.Exists(caminhoArquivo))
                {
                    var fileInfo = new FileInfo(caminhoArquivo);
                    long tamanhoAtual = fileInfo.Length;

                    if (tamanhoAtual > ultimoTamanhoArquivo)
                    {
                        // Arquivo cresceu, carregar novamente
                        CarregarLogs();
                    }
                }
            }
            catch (IOException)
            {
                // Arquivo pode estar sendo usado, ignorar esta tentativa
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Erro ao verificar atualizações do log");
            }
        }

        private string ObterFiltroArquivo()
        {
            DateTime data = dtpFiltroData.Value.Date;

            if (rbTodosLogs.Checked)
                return $"app-{data:yyyy-MM-dd}.log";
            else if (rbApenasErros.Checked)
                return $"errors-{data:yyyy-MM-dd}.log";

            return $"app-{data:yyyy-MM-dd}.log";
        }

        private void AtualizarInformacoes(string caminhoArquivo)
        {
            try
            {
                var fileInfo = new FileInfo(caminhoArquivo);
                int totalLinhas = txtLogs.Lines.Length;

                lblInformacoes.Text = $"Arquivo: {Path.GetFileName(caminhoArquivo)} | " +
                                     $"Tamanho: {fileInfo.Length / 1024:N0} KB | " +
                                     $"Linhas: {totalLinhas:N0} | " +
                                     $"Modificado: {fileInfo.LastWriteTime:dd/MM/yyyy HH:mm:ss}";
            }
            catch (IOException)
            {
                lblInformacoes.Text = "Arquivo sendo atualizado pelo sistema...";
            }
            catch (Exception ex)
            {
                lblInformacoes.Text = "Erro ao obter informações do arquivo";
                logger.Error(ex, "Erro ao obter informações do arquivo de log");
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarLogs();
            logger.Info("Logs atualizados manualmente pelo usuário");
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtLogs.Clear();
        }

        private void btnSalvarComo_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*";
                sfd.FileName = $"logs-{DateTime.Now:yyyy-MM-dd-HHmm}.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, txtLogs.Text, Encoding.UTF8);
                    MessageBox.Show("Logs salvos com sucesso!", "Sucesso",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    logger.Info($"Logs exportados para: {sfd.FileName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(ex, "Erro ao exportar logs");
            }
        }

        private void btnAbrirPasta_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(caminhoLogs))
                {
                    System.Diagnostics.Process.Start("explorer.exe", caminhoLogs);
                    logger.Info("Pasta de logs aberta no Explorer");
                }
                else
                {
                    MessageBox.Show("Pasta de logs ainda não existe.\nExecute algumas ações no sistema para gerar logs.",
                                  "Pasta não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir pasta: {ex.Message}", "Erro",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(ex, "Erro ao abrir pasta de logs");
            }
        }

        private void dtpFiltroData_ValueChanged(object sender, EventArgs e)
        {
            CarregarLogs();
        }

        private void rbTodosLogs_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTodosLogs.Checked)
                CarregarLogs();
        }

        private void rbApenasErros_CheckedChanged(object sender, EventArgs e)
        {
            if (rbApenasErros.Checked)
                CarregarLogs();
        }

        private void txtPesquisar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPesquisar.Text))
            {
                PesquisarTexto(txtPesquisar.Text);
            }
        }

        private void PesquisarTexto(string termo)
        {
            if (string.IsNullOrEmpty(termo))
                return;

            int index = txtLogs.Text.IndexOf(termo, StringComparison.OrdinalIgnoreCase);
            if (index >= 0)
            {
                txtLogs.Select(index, termo.Length);
                txtLogs.ScrollToCaret();
                txtLogs.Focus();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPesquisar.Text))
            {
                PesquisarTexto(txtPesquisar.Text);
            }
        }

        private void FrmLogs_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timerAtualizacao != null)
            {
                timerAtualizacao.Stop();
                timerAtualizacao.Dispose();
            }
            logger.Info("Tela de logs fechada pelo usuário");
        }

        private void btnGerarLogTeste_Click(object sender, EventArgs e)
        {
            // Gerar alguns logs de teste
            logger.Info("=== LOG DE TESTE GERADO ===");
            logger.Debug("Este é um log de debug para teste");
            logger.Warn("Este é um log de warning para teste");
            logger.Error("Este é um log de erro para teste");
            logger.Info($"Logs de teste gerados às {DateTime.Now:dd/MM/yyyy HH:mm:ss}");

            MessageBox.Show("Logs de teste gerados com sucesso!", "Teste",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Atualizar a visualização
            System.Threading.Thread.Sleep(500); // Aguardar um pouco
            CarregarLogs();
        }
    }
}