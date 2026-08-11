
namespace teladelogin
{
    partial class FrmCadastroUsuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancelar = new Guna.UI2.WinForms.Guna2Button();
            this.btnCadastrar = new Guna.UI2.WinForms.Guna2Button();
            this.Senha = new System.Windows.Forms.Label();
            this.txtCriaSenha = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtCriaLogin = new Guna.UI2.WinForms.Guna2TextBox();
            this.Login = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            this.btnCancelar.Location = new System.Drawing.Point(80, 326);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.btnCancelar.Size = new System.Drawing.Size(270, 69);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCadastrar
            // 
            this.btnCadastrar.BackColor = System.Drawing.Color.Transparent;
            this.btnCadastrar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.btnCadastrar.BorderRadius = 20;
            this.btnCadastrar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCadastrar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCadastrar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCadastrar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCadastrar.FillColor = System.Drawing.Color.White;
            this.btnCadastrar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCadastrar.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.btnCadastrar.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.btnCadastrar.Location = new System.Drawing.Point(425, 326);
            this.btnCadastrar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCadastrar.Name = "btnCadastrar";
            this.btnCadastrar.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.btnCadastrar.Size = new System.Drawing.Size(270, 69);
            this.btnCadastrar.TabIndex = 7;
            this.btnCadastrar.Text = "&Cadastrar Conta";
            this.btnCadastrar.Click += new System.EventHandler(this.btnCadastrar_Click);
            // 
            // Senha
            // 
            this.Senha.AutoSize = true;
            this.Senha.BackColor = System.Drawing.Color.Transparent;
            this.Senha.Font = new System.Drawing.Font("Palatino Linotype", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Senha.ForeColor = System.Drawing.Color.White;
            this.Senha.Location = new System.Drawing.Point(180, 152);
            this.Senha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Senha.Name = "Senha";
            this.Senha.Size = new System.Drawing.Size(256, 49);
            this.Senha.TabIndex = 12;
            this.Senha.Text = "Crie sua senha";
            // 
            // txtCriaSenha
            // 
            this.txtCriaSenha.Animated = true;
            this.txtCriaSenha.AutoRoundedCorners = true;
            this.txtCriaSenha.BackColor = System.Drawing.Color.Transparent;
            this.txtCriaSenha.BorderColor = System.Drawing.Color.Transparent;
            this.txtCriaSenha.BorderRadius = 48;
            this.txtCriaSenha.BorderThickness = 0;
            this.txtCriaSenha.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtCriaSenha.DefaultText = "";
            this.txtCriaSenha.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCriaSenha.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCriaSenha.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCriaSenha.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCriaSenha.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCriaSenha.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtCriaSenha.ForeColor = System.Drawing.Color.DarkGray;
            this.txtCriaSenha.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCriaSenha.IconLeft = global::teladelogin.Properties.Resources.sing;
            this.txtCriaSenha.IconLeftSize = new System.Drawing.Size(60, 50);
            this.txtCriaSenha.IconRight = global::teladelogin.Properties.Resources.todah_removebg_preview;
            this.txtCriaSenha.Location = new System.Drawing.Point(167, 203);
            this.txtCriaSenha.Margin = new System.Windows.Forms.Padding(8);
            this.txtCriaSenha.Name = "txtCriaSenha";
            this.txtCriaSenha.PlaceholderForeColor = System.Drawing.Color.SlateGray;
            this.txtCriaSenha.PlaceholderText = "Insira seu login";
            this.txtCriaSenha.SelectedText = "";
            this.txtCriaSenha.Size = new System.Drawing.Size(468, 98);
            this.txtCriaSenha.TabIndex = 11;
            // 
            // txtCriaLogin
            // 
            this.txtCriaLogin.Animated = true;
            this.txtCriaLogin.AutoRoundedCorners = true;
            this.txtCriaLogin.BackColor = System.Drawing.Color.Transparent;
            this.txtCriaLogin.BorderColor = System.Drawing.Color.Transparent;
            this.txtCriaLogin.BorderRadius = 48;
            this.txtCriaLogin.BorderThickness = 0;
            this.txtCriaLogin.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtCriaLogin.DefaultText = "";
            this.txtCriaLogin.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCriaLogin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCriaLogin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCriaLogin.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCriaLogin.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCriaLogin.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtCriaLogin.ForeColor = System.Drawing.Color.DarkGray;
            this.txtCriaLogin.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCriaLogin.IconLeft = global::teladelogin.Properties.Resources.cantando;
            this.txtCriaLogin.IconLeftSize = new System.Drawing.Size(60, 50);
            this.txtCriaLogin.IconRight = global::teladelogin.Properties.Resources.todah_removebg_preview;
            this.txtCriaLogin.Location = new System.Drawing.Point(167, 46);
            this.txtCriaLogin.Margin = new System.Windows.Forms.Padding(8);
            this.txtCriaLogin.Name = "txtCriaLogin";
            this.txtCriaLogin.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtCriaLogin.PlaceholderText = "Insira seu login";
            this.txtCriaLogin.SelectedText = "";
            this.txtCriaLogin.Size = new System.Drawing.Size(468, 98);
            this.txtCriaLogin.TabIndex = 10;
            // 
            // Login
            // 
            this.Login.AutoSize = true;
            this.Login.BackColor = System.Drawing.Color.Transparent;
            this.Login.Font = new System.Drawing.Font("Palatino Linotype", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login.ForeColor = System.Drawing.Color.White;
            this.Login.Location = new System.Drawing.Point(180, -6);
            this.Login.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(354, 49);
            this.Login.TabIndex = 9;
            this.Login.Text = "Digite aqui seu login";
            // 
            // FrmCadastroUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Senha);
            this.Controls.Add(this.txtCriaSenha);
            this.Controls.Add(this.txtCriaLogin);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnCadastrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCadastroUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCadastroUsuario";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnCancelar;
        private Guna.UI2.WinForms.Guna2Button btnCadastrar;
        private System.Windows.Forms.Label Senha;
        private Guna.UI2.WinForms.Guna2TextBox txtCriaSenha;
        private Guna.UI2.WinForms.Guna2TextBox txtCriaLogin;
        private System.Windows.Forms.Label Login;
    }
}