
namespace teladelogin.UI
{
    partial class FrmBKeRestore
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtCaminho = new System.Windows.Forms.TextBox();
            this.btnBK = new System.Windows.Forms.Button();
            this.sfdSalvar = new System.Windows.Forms.SaveFileDialog();
            this.ofdAbrir = new System.Windows.Forms.OpenFileDialog();
            this.btnSelecionarRes = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Caminho:";
            // 
            // txtCaminho
            // 
            this.txtCaminho.Enabled = false;
            this.txtCaminho.Location = new System.Drawing.Point(186, 86);
            this.txtCaminho.Name = "txtCaminho";
            this.txtCaminho.Size = new System.Drawing.Size(571, 26);
            this.txtCaminho.TabIndex = 1;
            // 
            // btnBK
            // 
            this.btnBK.Location = new System.Drawing.Point(91, 248);
            this.btnBK.Name = "btnBK";
            this.btnBK.Size = new System.Drawing.Size(195, 94);
            this.btnBK.TabIndex = 2;
            this.btnBK.Text = "&Backup";
            this.btnBK.UseVisualStyleBackColor = true;
            this.btnBK.Click += new System.EventHandler(this.btnBK_Click);
            // 
            // ofdAbrir
            // 
            this.ofdAbrir.FileName = "openFileDialog1";
            // 
            // btnSelecionarRes
            // 
            this.btnSelecionarRes.Location = new System.Drawing.Point(353, 248);
            this.btnSelecionarRes.Name = "btnSelecionarRes";
            this.btnSelecionarRes.Size = new System.Drawing.Size(136, 97);
            this.btnSelecionarRes.TabIndex = 3;
            this.btnSelecionarRes.Text = "&Selecione o Restore";
            this.btnSelecionarRes.UseVisualStyleBackColor = true;
            this.btnSelecionarRes.Click += new System.EventHandler(this.btnSelecionarRes_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(601, 248);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(121, 97);
            this.btnRestore.TabIndex = 4;
            this.btnRestore.Text = "&Restaurar";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // FrmBKeRestore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnSelecionarRes);
            this.Controls.Add(this.btnBK);
            this.Controls.Add(this.txtCaminho);
            this.Controls.Add(this.label1);
            this.Name = "FrmBKeRestore";
            this.Text = "FrmBKeRestore";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCaminho;
        private System.Windows.Forms.Button btnBK;
        private System.Windows.Forms.SaveFileDialog sfdSalvar;
        private System.Windows.Forms.OpenFileDialog ofdAbrir;
        private System.Windows.Forms.Button btnSelecionarRes;
        private System.Windows.Forms.Button btnRestore;
    }
}