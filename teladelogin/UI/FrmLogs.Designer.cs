using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace teladelogin.UI
{
    partial class FrmLogs
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtLogs;
        private Button btnAtualizar;
        private Button btnLimpar;
        private Button btnSalvarComo;
        private Button btnAbrirPasta;
        private DateTimePicker dtpFiltroData;
        private RadioButton rbTodosLogs;
        private RadioButton rbApenasErros;
        private CheckBox chkAtualizacaoAutomatica;
        private TextBox txtPesquisar;
        private Button btnPesquisar;
        private Label lblInformacoes;
        private Button btnGerarLogTeste;
        private GroupBox gbFiltros;
        private GroupBox gbAcoes;
        private Label lblData;
        private Label lblPesquisar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtLogs = new TextBox();
            this.btnAtualizar = new Button();
            this.btnLimpar = new Button();
            this.btnSalvarComo = new Button();
            this.btnAbrirPasta = new Button();
            this.dtpFiltroData = new DateTimePicker();
            this.rbTodosLogs = new RadioButton();
            this.rbApenasErros = new RadioButton();
            this.chkAtualizacaoAutomatica = new CheckBox();
            this.txtPesquisar = new TextBox();
            this.btnPesquisar = new Button();
            this.lblInformacoes = new Label();
            this.btnGerarLogTeste = new Button();
            this.gbFiltros = new GroupBox();
            this.gbAcoes = new GroupBox();
            this.lblData = new Label();
            this.lblPesquisar = new Label();
            this.gbFiltros.SuspendLayout();
            this.gbAcoes.SuspendLayout();
            this.SuspendLayout();

            // 
            // Form
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1000, 600);
            this.Text = "Visualizador de Logs - Sistema Lojinha";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(800, 500);

            // 
            // txtLogs
            // 
            this.txtLogs.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            this.txtLogs.BackColor = Color.Black;
            this.txtLogs.ForeColor = Color.Lime;
            this.txtLogs.Font = new Font("Consolas", 9F, FontStyle.Regular);
            this.txtLogs.Location = new Point(12, 120);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ReadOnly = true;
            this.txtLogs.ScrollBars = ScrollBars.Both;
            this.txtLogs.Size = new Size(976, 420);
            this.txtLogs.TabIndex = 0;
            this.txtLogs.WordWrap = false;

            // 
            // gbFiltros
            // 
            this.gbFiltros.Controls.Add(this.lblData);
            this.gbFiltros.Controls.Add(this.dtpFiltroData);
            this.gbFiltros.Controls.Add(this.rbTodosLogs);
            this.gbFiltros.Controls.Add(this.rbApenasErros);
            this.gbFiltros.Controls.Add(this.chkAtualizacaoAutomatica);
            this.gbFiltros.Location = new Point(12, 12);
            this.gbFiltros.Name = "gbFiltros";
            this.gbFiltros.Size = new Size(480, 100);
            this.gbFiltros.TabIndex = 1;
            this.gbFiltros.TabStop = false;
            this.gbFiltros.Text = "Filtros e Configurações";

            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new Point(15, 25);
            this.lblData.Name = "lblData";
            this.lblData.Size = new Size(34, 15);
            this.lblData.TabIndex = 0;
            this.lblData.Text = "Data:";

            // 
            // dtpFiltroData
            // 
            this.dtpFiltroData.Format = DateTimePickerFormat.Short;
            this.dtpFiltroData.Location = new Point(55, 22);
            this.dtpFiltroData.Name = "dtpFiltroData";
            this.dtpFiltroData.Size = new Size(120, 23);
            this.dtpFiltroData.TabIndex = 1;
            this.dtpFiltroData.Value = DateTime.Today;
            this.dtpFiltroData.ValueChanged += new EventHandler(this.dtpFiltroData_ValueChanged);

            // 
            // rbTodosLogs
            // 
            this.rbTodosLogs.AutoSize = true;
            this.rbTodosLogs.Checked = true;
            this.rbTodosLogs.Location = new Point(200, 24);
            this.rbTodosLogs.Name = "rbTodosLogs";
            this.rbTodosLogs.Size = new Size(82, 19);
            this.rbTodosLogs.TabIndex = 2;
            this.rbTodosLogs.TabStop = true;
            this.rbTodosLogs.Text = "Todos Logs";
            this.rbTodosLogs.UseVisualStyleBackColor = true;
            this.rbTodosLogs.CheckedChanged += new EventHandler(this.rbTodosLogs_CheckedChanged);

            // 
            // rbApenasErros
            // 
            this.rbApenasErros.AutoSize = true;
            this.rbApenasErros.Location = new Point(290, 24);
            this.rbApenasErros.Name = "rbApenasErros";
            this.rbApenasErros.Size = new Size(96, 19);
            this.rbApenasErros.TabIndex = 3;
            this.rbApenasErros.Text = "Apenas Erros";
            this.rbApenasErros.UseVisualStyleBackColor = true;
            this.rbApenasErros.CheckedChanged += new EventHandler(this.rbApenasErros_CheckedChanged);

            // 
            // chkAtualizacaoAutomatica
            // 
            this.chkAtualizacaoAutomatica.AutoSize = true;
            this.chkAtualizacaoAutomatica.Checked = true;
            this.chkAtualizacaoAutomatica.CheckState = CheckState.Checked;
            this.chkAtualizacaoAutomatica.Location = new Point(15, 55);
            this.chkAtualizacaoAutomatica.Name = "chkAtualizacaoAutomatica";
            this.chkAtualizacaoAutomatica.Size = new Size(160, 19);
            this.chkAtualizacaoAutomatica.TabIndex = 4;
            this.chkAtualizacaoAutomatica.Text = "Atualização Automática";
            this.chkAtualizacaoAutomatica.UseVisualStyleBackColor = true;

            // 
            // gbAcoes
            // 
            this.gbAcoes.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.gbAcoes.Controls.Add(this.lblPesquisar);
            this.gbAcoes.Controls.Add(this.txtPesquisar);
            this.gbAcoes.Controls.Add(this.btnPesquisar);
            this.gbAcoes.Controls.Add(this.btnAtualizar);
            this.gbAcoes.Controls.Add(this.btnLimpar);
            this.gbAcoes.Controls.Add(this.btnSalvarComo);
            this.gbAcoes.Controls.Add(this.btnAbrirPasta);
            this.gbAcoes.Controls.Add(this.btnGerarLogTeste);
            this.gbAcoes.Location = new Point(508, 12);
            this.gbAcoes.Name = "gbAcoes";
            this.gbAcoes.Size = new Size(480, 100);
            this.gbAcoes.TabIndex = 2;
            this.gbAcoes.TabStop = false;
            this.gbAcoes.Text = "Ações e Pesquisa";

            // 
            // lblPesquisar
            // 
            this.lblPesquisar.AutoSize = true;
            this.lblPesquisar.Location = new Point(15, 25);
            this.lblPesquisar.Name = "lblPesquisar";
            this.lblPesquisar.Size = new Size(62, 15);
            this.lblPesquisar.TabIndex = 0;
            this.lblPesquisar.Text = "Pesquisar:";

            // 
            // txtPesquisar
            // 
            this.txtPesquisar.Location = new Point(83, 22);
            this.txtPesquisar.Name = "txtPesquisar";
            this.txtPesquisar.Size = new Size(200, 23);
            this.txtPesquisar.TabIndex = 1;
            this.txtPesquisar.TextChanged += new EventHandler(this.txtPesquisar_TextChanged);

            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Location = new Point(289, 21);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new Size(75, 25);
            this.btnPesquisar.TabIndex = 2;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.UseVisualStyleBackColor = true;
            this.btnPesquisar.Click += new EventHandler(this.btnPesquisar_Click);

            // 
            // btnAtualizar
            // 
            this.btnAtualizar.BackColor = Color.FromArgb(0, 120, 215);
            this.btnAtualizar.ForeColor = Color.White;
            this.btnAtualizar.Location = new Point(15, 55);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new Size(80, 30);
            this.btnAtualizar.TabIndex = 3;
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = false;
            this.btnAtualizar.Click += new EventHandler(this.btnAtualizar_Click);

            // 
            // btnLimpar
            // 
            this.btnLimpar.BackColor = Color.FromArgb(255, 140, 0);
            this.btnLimpar.ForeColor = Color.White;
            this.btnLimpar.Location = new Point(105, 55);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new Size(80, 30);
            this.btnLimpar.TabIndex = 4;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Click += new EventHandler(this.btnLimpar_Click);

            // 
            // btnSalvarComo
            // 
            this.btnSalvarComo.BackColor = Color.FromArgb(34, 139, 34);
            this.btnSalvarComo.ForeColor = Color.White;
            this.btnSalvarComo.Location = new Point(195, 55);
            this.btnSalvarComo.Name = "btnSalvarComo";
            this.btnSalvarComo.Size = new Size(90, 30);
            this.btnSalvarComo.TabIndex = 5;
            this.btnSalvarComo.Text = "Salvar Como";
            this.btnSalvarComo.UseVisualStyleBackColor = false;
            this.btnSalvarComo.Click += new EventHandler(this.btnSalvarComo_Click);

            // 
            // btnAbrirPasta
            // 
            this.btnAbrirPasta.BackColor = Color.FromArgb(128, 0, 128);
            this.btnAbrirPasta.ForeColor = Color.White;
            this.btnAbrirPasta.Location = new Point(295, 55);
            this.btnAbrirPasta.Name = "btnAbrirPasta";
            this.btnAbrirPasta.Size = new Size(85, 30);
            this.btnAbrirPasta.TabIndex = 6;
            this.btnAbrirPasta.Text = "Abrir Pasta";
            this.btnAbrirPasta.UseVisualStyleBackColor = false;
            this.btnAbrirPasta.Click += new EventHandler(this.btnAbrirPasta_Click);

            // 
            // btnGerarLogTeste
            // 
            this.btnGerarLogTeste.BackColor = Color.FromArgb(220, 20, 60);
            this.btnGerarLogTeste.ForeColor = Color.White;
            this.btnGerarLogTeste.Location = new Point(390, 55);
            this.btnGerarLogTeste.Name = "btnGerarLogTeste";
            this.btnGerarLogTeste.Size = new Size(80, 30);
            this.btnGerarLogTeste.TabIndex = 7;
            this.btnGerarLogTeste.Text = "Gerar Teste";
            this.btnGerarLogTeste.UseVisualStyleBackColor = false;
            this.btnGerarLogTeste.Click += new EventHandler(this.btnGerarLogTeste_Click);

            // 
            // lblInformacoes
            // 
            this.lblInformacoes.Anchor = ((AnchorStyles)(((AnchorStyles.Bottom | AnchorStyles.Left)
            | AnchorStyles.Right)));
            this.lblInformacoes.BackColor = Color.FromArgb(240, 240, 240);
            this.lblInformacoes.BorderStyle = BorderStyle.FixedSingle;
            this.lblInformacoes.Location = new Point(12, 550);
            this.lblInformacoes.Name = "lblInformacoes";
            this.lblInformacoes.Padding = new Padding(5);
            this.lblInformacoes.Size = new Size(976, 30);
            this.lblInformacoes.TabIndex = 3;
            this.lblInformacoes.Text = "Carregando informações...";
            this.lblInformacoes.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // Form Controls
            // 
            this.Controls.Add(this.txtLogs);
            this.Controls.Add(this.gbFiltros);
            this.Controls.Add(this.gbAcoes);
            this.Controls.Add(this.lblInformacoes);
            this.gbFiltros.ResumeLayout(false);
            this.gbFiltros.PerformLayout();
            this.gbAcoes.ResumeLayout(false);
            this.gbAcoes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}