
namespace teladelogin
{
    partial class FrmLogin
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Login = new System.Windows.Forms.Label();
            this.txtSenha = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2ImageButton1 = new Guna.UI2.WinForms.Guna2ImageButton();
            this.txtLogin = new Guna.UI2.WinForms.Guna2TextBox();
            this.Senha = new System.Windows.Forms.Label();
            this.btnLogar = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancelar = new Guna.UI2.WinForms.Guna2Button();
            this.likEsqueciSenha = new System.Windows.Forms.LinkLabel();
            this.lnkNovoCadastro = new System.Windows.Forms.LinkLabel();
            this.btnBackUpERestore = new System.Windows.Forms.Button();
            this.btnLogs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Login
            // 
            this.Login.AutoSize = true;
            this.Login.BackColor = System.Drawing.Color.Transparent;
            this.Login.Font = new System.Drawing.Font("Palatino Linotype", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login.ForeColor = System.Drawing.Color.White;
            this.Login.Location = new System.Drawing.Point(357, 285);
            this.Login.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(115, 49);
            this.Login.TabIndex = 1;
            this.Login.Text = "Login";
            // 
            // txtSenha
            // 
            this.txtSenha.Animated = true;
            this.txtSenha.AutoRoundedCorners = true;
            this.txtSenha.BackColor = System.Drawing.Color.Transparent;
            this.txtSenha.BorderColor = System.Drawing.Color.Transparent;
            this.txtSenha.BorderRadius = 48;
            this.txtSenha.BorderThickness = 0;
            this.txtSenha.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtSenha.DefaultText = "12345";
            this.txtSenha.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSenha.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSenha.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSenha.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSenha.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSenha.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtSenha.ForeColor = System.Drawing.Color.DarkGray;
            this.txtSenha.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSenha.IconLeft = global::teladelogin.Properties.Resources.sing;
            this.txtSenha.IconLeftSize = new System.Drawing.Size(60, 50);
            this.txtSenha.IconRight = global::teladelogin.Properties.Resources.todah_removebg_preview;
            this.txtSenha.Location = new System.Drawing.Point(344, 494);
            this.txtSenha.Margin = new System.Windows.Forms.Padding(8);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PlaceholderForeColor = System.Drawing.Color.SlateGray;
            this.txtSenha.PlaceholderText = "Insira seu login";
            this.txtSenha.SelectedText = "";
            this.txtSenha.Size = new System.Drawing.Size(468, 98);
            this.txtSenha.TabIndex = 3;
            this.txtSenha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSenha_KeyDown);
            // 
            // guna2ImageButton1
            // 
            this.guna2ImageButton1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ImageButton1.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.HoverState.ImageSize = new System.Drawing.Size(200, 200);
            this.guna2ImageButton1.Image = global::teladelogin.Properties.Resources.OIP;
            this.guna2ImageButton1.ImageOffset = new System.Drawing.Point(0, 0);
            this.guna2ImageButton1.ImageRotate = 0F;
            this.guna2ImageButton1.ImageSize = new System.Drawing.Size(200, 200);
            this.guna2ImageButton1.Location = new System.Drawing.Point(459, 22);
            this.guna2ImageButton1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2ImageButton1.Name = "guna2ImageButton1";
            this.guna2ImageButton1.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.Size = new System.Drawing.Size(262, 280);
            this.guna2ImageButton1.TabIndex = 0;
            this.guna2ImageButton1.UseTransparentBackground = true;
            // 
            // txtLogin
            // 
            this.txtLogin.Animated = true;
            this.txtLogin.AutoRoundedCorners = true;
            this.txtLogin.BackColor = System.Drawing.Color.Transparent;
            this.txtLogin.BorderColor = System.Drawing.Color.Transparent;
            this.txtLogin.BorderRadius = 48;
            this.txtLogin.BorderThickness = 0;
            this.txtLogin.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtLogin.DefaultText = "admin";
            this.txtLogin.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtLogin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtLogin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLogin.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLogin.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLogin.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtLogin.ForeColor = System.Drawing.Color.DarkGray;
            this.txtLogin.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLogin.IconLeft = global::teladelogin.Properties.Resources.cantando;
            this.txtLogin.IconLeftSize = new System.Drawing.Size(60, 50);
            this.txtLogin.IconRight = global::teladelogin.Properties.Resources.todah_removebg_preview;
            this.txtLogin.Location = new System.Drawing.Point(344, 337);
            this.txtLogin.Margin = new System.Windows.Forms.Padding(8);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtLogin.PlaceholderText = "Insira seu login";
            this.txtLogin.SelectedText = "";
            this.txtLogin.Size = new System.Drawing.Size(468, 98);
            this.txtLogin.TabIndex = 2;
            this.txtLogin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLogin_KeyDown);
            // 
            // Senha
            // 
            this.Senha.AutoSize = true;
            this.Senha.BackColor = System.Drawing.Color.Transparent;
            this.Senha.Font = new System.Drawing.Font("Palatino Linotype", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Senha.ForeColor = System.Drawing.Color.White;
            this.Senha.Location = new System.Drawing.Point(357, 443);
            this.Senha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Senha.Name = "Senha";
            this.Senha.Size = new System.Drawing.Size(118, 49);
            this.Senha.TabIndex = 4;
            this.Senha.Text = "Senha";
            // 
            // btnLogar
            // 
            this.btnLogar.BackColor = System.Drawing.Color.Transparent;
            this.btnLogar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.btnLogar.BorderRadius = 20;
            this.btnLogar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLogar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLogar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLogar.FillColor = System.Drawing.Color.White;
            this.btnLogar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLogar.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.btnLogar.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.btnLogar.Location = new System.Drawing.Point(603, 643);
            this.btnLogar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLogar.Name = "btnLogar";
            this.btnLogar.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.btnLogar.Size = new System.Drawing.Size(270, 69);
            this.btnLogar.TabIndex = 5;
            this.btnLogar.Text = "Logar";
            this.btnLogar.Click += new System.EventHandler(this.btnLogar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.btnCancelar.BorderRadius = 20;
            this.btnCancelar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCancelar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCancelar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCancelar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCancelar.FillColor = System.Drawing.Color.White;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.btnCancelar.HoverState.FillColor = System.Drawing.Color.DarkRed;
            this.btnCancelar.Location = new System.Drawing.Point(258, 643);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.btnCancelar.Size = new System.Drawing.Size(270, 69);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // likEsqueciSenha
            // 
            this.likEsqueciSenha.ActiveLinkColor = System.Drawing.Color.DarkRed;
            this.likEsqueciSenha.AutoSize = true;
            this.likEsqueciSenha.BackColor = System.Drawing.Color.Transparent;
            this.likEsqueciSenha.LinkColor = System.Drawing.SystemColors.Window;
            this.likEsqueciSenha.Location = new System.Drawing.Point(651, 600);
            this.likEsqueciSenha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.likEsqueciSenha.Name = "likEsqueciSenha";
            this.likEsqueciSenha.Size = new System.Drawing.Size(161, 20);
            this.likEsqueciSenha.TabIndex = 7;
            this.likEsqueciSenha.TabStop = true;
            this.likEsqueciSenha.Text = "Esqueci minha senha";
            this.likEsqueciSenha.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.likEsqueciSenha_LinkClicked);
            // 
            // lnkNovoCadastro
            // 
            this.lnkNovoCadastro.ActiveLinkColor = System.Drawing.Color.DarkRed;
            this.lnkNovoCadastro.AutoSize = true;
            this.lnkNovoCadastro.BackColor = System.Drawing.Color.Transparent;
            this.lnkNovoCadastro.LinkColor = System.Drawing.SystemColors.Window;
            this.lnkNovoCadastro.Location = new System.Drawing.Point(362, 600);
            this.lnkNovoCadastro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkNovoCadastro.Name = "lnkNovoCadastro";
            this.lnkNovoCadastro.Size = new System.Drawing.Size(111, 20);
            this.lnkNovoCadastro.TabIndex = 8;
            this.lnkNovoCadastro.TabStop = true;
            this.lnkNovoCadastro.Text = "Crie sua conta";
            this.lnkNovoCadastro.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNovoCadastro_LinkClicked);
            // 
            // btnBackUpERestore
            // 
            this.btnBackUpERestore.Location = new System.Drawing.Point(983, 412);
            this.btnBackUpERestore.Name = "btnBackUpERestore";
            this.btnBackUpERestore.Size = new System.Drawing.Size(171, 41);
            this.btnBackUpERestore.TabIndex = 9;
            this.btnBackUpERestore.Text = "&Backup e Restore";
            this.btnBackUpERestore.UseVisualStyleBackColor = true;
            this.btnBackUpERestore.Click += new System.EventHandler(this.btnBackUpERestore_Click);
            // 
            // btnLogs
            // 
            this.btnLogs.Location = new System.Drawing.Point(983, 469);
            this.btnLogs.Name = "btnLogs";
            this.btnLogs.Size = new System.Drawing.Size(171, 42);
            this.btnLogs.TabIndex = 10;
            this.btnLogs.Text = "&Verificar Logs";
            this.btnLogs.UseVisualStyleBackColor = true;
            this.btnLogs.Click += new System.EventHandler(this.btnLogs_Click);
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::teladelogin.Properties.Resources.bk;
            this.ClientSize = new System.Drawing.Size(1200, 746);
            this.Controls.Add(this.btnLogs);
            this.Controls.Add(this.btnBackUpERestore);
            this.Controls.Add(this.lnkNovoCadastro);
            this.Controls.Add(this.likEsqueciSenha);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnLogar);
            this.Controls.Add(this.Senha);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.guna2ImageButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Login;
        private Guna.UI2.WinForms.Guna2TextBox txtSenha;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton1;
        private Guna.UI2.WinForms.Guna2TextBox txtLogin;
        private System.Windows.Forms.Label Senha;
        private Guna.UI2.WinForms.Guna2Button btnLogar;
        private Guna.UI2.WinForms.Guna2Button btnCancelar;
        private System.Windows.Forms.LinkLabel likEsqueciSenha;
        private System.Windows.Forms.LinkLabel lnkNovoCadastro;
        private System.Windows.Forms.Button btnBackUpERestore;
        private System.Windows.Forms.Button btnLogs;
    }
}

