using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using teladelogin.DAL;
using teladelogin.BLL;
using System.Data.SqlClient;
using System.IO;
namespace teladelogin
{
    public partial class FrmPDV : Form
    {
        public FrmPDV()
        {
            InitializeComponent();
        }
        //criando o objeto para chamar o método de salvar dados
        VendasDAL dal = new VendasDAL();

        #region Métodos Keypress
        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            //verifica se foi pressionada número ou backspace
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                {
                    //se não for dígito (número) ele cancela a ação
                    e.Handled = true;
                }
            }

            if (txtQuantidade.Text.Length >= 4 && !char.IsControl(e.KeyChar))
            {
                {
                    //se não for dígito (número) ele cancela a ação
                    e.Handled = true;
                }
            }
        }

        private void txtPrecoUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite números, ponto, vírgula e tecla de backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Bloqueia outras teclas
            }

            // Impede mais de um ponto ou vírgula
            if ((e.KeyChar == ',' || e.KeyChar == '.') && (txtPrecoUnit.Text.Contains(",") || txtPrecoUnit.Text.Contains(".")))
            {
                e.Handled = true;
            }

            //CORREÇÃO 2 - faz com que o ponto seja substiuido pela vírgula
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

        }
        #endregion

        #region Métodos Click
        private void btnImagem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Arquivos de imagem|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Selecione uma foto do produto"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Abre diretamente a imagem como Bitmap para evitar problema de stream fechado
                pcbImagemProduto.Image = new Bitmap(dialog.FileName);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DeletarDados();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            SalvarDados();
            LimpaCampos();
            CarregaDadosNoGrid();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FrmLogin voltarLogin = new FrmLogin();
            this.Hide();
            voltarLogin.Show();
        }
        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDescProduto.Text) || 
                    string.IsNullOrEmpty(txtPrecoUnit.Text) || 
                    string.IsNullOrEmpty(txtQuantidade.Text) || 
                    (cmbFormaPagamento.SelectedItem == null))
                {
                    MessageBox.Show("Por favor, preencha todos os campos!");
                    return;//obrigando o código a parar de executar
                }

                string descricao = txtDescProduto.Text;
                int quantidade = int.Parse(txtQuantidade.Text);
                decimal precoUnitario = decimal.Parse(txtPrecoUnit.Text, new CultureInfo("pt-BR"));
                decimal precoTotal = decimal.Parse(txtPrecoTot.Text, new CultureInfo("pt-BR"));
                string formaPagamento = cmbFormaPagamento.SelectedItem.ToString();
                int vendaID = Convert.ToInt32(dgvProduto.SelectedRows[0].Cells["VendaID"].Value);

                VendasDAL dal = new VendasDAL();
                bool sucesso = dal.AtualizarVenda(vendaID, descricao, quantidade, 
                    precoUnitario, precoTotal, formaPagamento);

                if (sucesso)
                {
                    MessageBox.Show("Dados atualizados!");
                    CarregaDadosNoGrid();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao atualizar os dados: " + ex.Message);
            }
        }
        #endregion

        #region Métodos Closing
        private void FrmPDV_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Verifica se a janela está sendo fechada (por exemplo, clicando no "X")
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Exibe uma mensagem de confirmação
                DialogResult result = MessageBox.Show("Você tem certeza que deseja sair do sistema?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    // Cancela o fechamento do formulário
                    e.Cancel = true;
                }
                else
                {
                    // Fecha a aplicação normalmente
                    Application.Exit();
                }
            }
        }
        #endregion

        #region Métodos TextChanged
        private void txtQuantidade_TextChanged(object sender, EventArgs e)
        {
            AtualizarPrecoTotal();
        }
        private void txtPrecoUnit_TextChanged(object sender, EventArgs e)
        {
            AtualizarPrecoTotal();
        }
        #endregion

        #region Métodos dos Programador

        private void ConfigurarImagemDataGrid()
        {
            // Configura o comportamento da coluna Foto
            dgvProduto.Columns["Foto"].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProduto.Columns["Foto"].CellTemplate.Style.WrapMode = DataGridViewTriState.True;

            // Ajuste o tamanho da coluna conforme necessário
            dgvProduto.Columns["Foto"].Width = 40; // Ajuste da largura da coluna
            dgvProduto.AutoResizeColumns(); // Ajuste automático das colunas

            // Definir como a imagem será exibida
            dgvProduto.Columns["Foto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProduto.Columns["Foto"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }


        private void AtualizarPrecoTotal()
        {
            try
            {
                decimal preco = 0;
                int quantidade = 0;

                if (decimal.TryParse(txtPrecoUnit.Text.Replace(".", ","), out preco) &&
                    int.TryParse(txtQuantidade.Text, out quantidade))
                {
                    decimal total = VendaBLL.CalcularTotal(preco, quantidade);
                    txtPrecoTot.Text = total.ToString("F2");
                }
                else
                {
                    txtPrecoTot.Text = "0";
                }
            }
            catch
            {
                txtPrecoTot.Text = "0";
            }
        }
        
        
        public void PreencheCamposAoSlecionarGrid()
        {
            if (dgvProduto.SelectedRows.Count == 0) return;

            var row = dgvProduto.SelectedRows[0];

            txtDescProduto.Text = row.Cells[1].Value?.ToString();
            txtQuantidade.Text = row.Cells[2].Value?.ToString();
            txtPrecoUnit.Text = row.Cells[3].Value?.ToString();
            txtPrecoTot.Text = row.Cells[4].Value?.ToString();
            cmbFormaPagamento.Text = row.Cells[5].Value?.ToString();

            // Buscar imagem no BD
            int vendaID = Convert.ToInt32(row.Cells["VendaID"].Value);
            byte[] imagemBytes = dal.BuscarImagemPorVendaID(vendaID);

            if (imagemBytes != null)
            {
                using (MemoryStream ms = new MemoryStream(imagemBytes))
                {
                    pcbImagemProduto.Image = Image.FromStream(ms);
                }
            }
            else
            {
                pcbImagemProduto.Image = null;
            }
        }
        void LimpaCampos()
        {
            txtDescProduto.Clear();
            txtPrecoTot.Clear();
            txtPrecoUnit.Clear();
            txtQuantidade.Clear();
            cmbFormaPagamento.SelectedItem = -1;
            pcbImagemProduto.Image = null;

            txtDescProduto.Focus();//volta o ponto de inserção na descrição
        }

        public void SalvarDados()
        {
            // Verifica se todos os campos foram preenchidos
            if (string.IsNullOrEmpty(txtDescProduto.Text) ||
                string.IsNullOrEmpty(txtQuantidade.Text) ||
                string.IsNullOrEmpty(txtPrecoUnit.Text) ||
                string.IsNullOrEmpty(txtPrecoTot.Text) ||
                cmbFormaPagamento.SelectedItem == null)
            {
                MessageBox.Show("Para salvar, necessário que todos os campos estejam preenchidos", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Converte a imagem exibida no PictureBox para byte[]
            byte[] imagemBytes = ConverterImagemParaBytes(pcbImagemProduto.Image);

            // Verifica se a imagem foi convertida corretamente
            if (imagemBytes == null || imagemBytes.Length == 0)
            {
                MessageBox.Show("Nenhuma imagem foi carregada ou houve erro ao converter a imagem.", "Erro de Imagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chama o método DAL para salvar os dados no banco
            int vendaID = dal.SalvarVenda(
                txtDescProduto.Text,
                int.Parse(txtQuantidade.Text),
                decimal.Parse(txtPrecoUnit.Text),
                decimal.Parse(txtPrecoTot.Text),
                cmbFormaPagamento.SelectedItem.ToString(),
                imagemBytes
            );

            // Verifica se salvou com sucesso
            if (vendaID > 0)
            {
                MessageBox.Show("O Produto: " + txtDescProduto.Text + ", foi cadastrado com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao cadastrar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void AtualizaBtnExcluir()
        {
            if (dgvProduto.SelectedRows.Count > 0)
            {
                btnExcluir.Enabled = true;
                btnAtualizar.Enabled = true;
                btnSalvar.Enabled = false;
            }
            else
            {
                btnExcluir.Enabled = false;
                btnAtualizar.Enabled = false;
                btnSalvar.Enabled = true;
                LimpaCampos();
            }
        }

        public void DeletarDados()
        {
            // Verifica se há alguma linha selecionata no DataGridView
            if (dgvProduto.SelectedRows.Count > 0)
            {
                // Pergunta se o usuário tem certeza que deseja excluir a linha
                DialogResult result = MessageBox.Show("Tem certeza de que deseja excluir este item?", "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //exclui a linha selecionada 
                    foreach (DataGridViewRow row in dgvProduto.SelectedRows)
                    {
                        //verifica se a linha não está sendo editada ou se não é uma nova linha ( a nova linha é aquela que tem a bo
                        if (!row.IsNewRow)
                        {
                            //passando o valor da coluna ID para a var vendaID
                            int vendaID = Convert.ToInt32(row.Cells["VendaID"].Value);
                            bool sucesso = dal.ExcluirVenda(vendaID);

                            if (sucesso)
                            {
                                dgvProduto.Rows.Remove(row);
                                MessageBox.Show("Item excluído com sucesso!");
                            }
                            else
                            {
                                MessageBox.Show("Erro ao excluir o item.", "Erro", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selecione uma linha para excluir.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private byte[] ConverterImagemParaBytes(Image imagem)
        {
            if (imagem == null) return null;

            using (MemoryStream ms = new MemoryStream())
            {
                imagem.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // Salva a imagem em memória no formato PNG
                return ms.ToArray(); // Converte para vetor de bytes
            }
        }

        #endregion

        #region SelectionChanged
        private void dgvProduto_SelectionChanged(object sender, EventArgs e)
        {
            AtualizaBtnExcluir();
            PreencheCamposAoSlecionarGrid();
        }
        #endregion

        #region Método para Carregar os dados no DataGrid
        public void CarregaDadosNoGrid()
        {
            try
            {
                dgvProduto.Rows.Clear();  // Limpa as linhas do DataGrid

                DataTable vendas = dal.ListarVendas();  // Obtém os dados das vendas

                foreach (DataRow row in vendas.Rows)
                {
                    byte[] imagemBytes = (byte[])row["Foto"];
                    Image imagem = null;

                    if (imagemBytes != null && imagemBytes.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(imagemBytes))
                        {
                            imagem = Image.FromStream(ms);
                        }

                        // Redimensionar a imagem para 20x20 pixels
                        imagem = new Bitmap(imagem, new Size(20, 20));
                    }

                    // Preenche a imagem na coluna Foto
                    dgvProduto.Rows.Add(
                        row["VendaID"],
                        row["ProdutoDescricao"],
                        row["Quantidade"],
                        Convert.ToDecimal(row["PrecoUnitario"]).ToString("F2"),
                        Convert.ToDecimal(row["PrecoTotal"]).ToString("F2"),
                        row["FormaPagamento"],
                        imagem  // Preenche a coluna Foto com a imagem convertida e redimensionada
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar o Datagrid: " + ex.Message);
            }
        }


        #endregion

        #region Métodos do Load
        private void FrmPDV_Load(object sender, EventArgs e)
        {
            CarregaDadosNoGrid();
        }


        #endregion

      
    }
}
