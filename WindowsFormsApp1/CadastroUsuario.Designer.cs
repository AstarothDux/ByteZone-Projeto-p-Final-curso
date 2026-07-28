namespace WindowsFormsApp1
{
    partial class CadastroUsuario
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
            this.txtCadNomeUsuario = new System.Windows.Forms.TextBox();
            this.lblNomeUsuario = new System.Windows.Forms.Label();
            this.txtCadCPF = new System.Windows.Forms.TextBox();
            this.lblCadCPF = new System.Windows.Forms.Label();
            this.txtCadEmail = new System.Windows.Forms.TextBox();
            this.lblCadEmail = new System.Windows.Forms.Label();
            this.lblCadTipoUsuario = new System.Windows.Forms.Label();
            this.cmbTipoUsuario = new System.Windows.Forms.ComboBox();
            this.lblTipoUsuarioA_U = new System.Windows.Forms.Label();
            this.btnCadUsuario = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblSenha = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCadNomeUsuario
            // 
            this.txtCadNomeUsuario.Location = new System.Drawing.Point(12, 105);
            this.txtCadNomeUsuario.Name = "txtCadNomeUsuario";
            this.txtCadNomeUsuario.Size = new System.Drawing.Size(199, 20);
            this.txtCadNomeUsuario.TabIndex = 17;
            // 
            // lblNomeUsuario
            // 
            this.lblNomeUsuario.AutoSize = true;
            this.lblNomeUsuario.Location = new System.Drawing.Point(12, 89);
            this.lblNomeUsuario.Name = "lblNomeUsuario";
            this.lblNomeUsuario.Size = new System.Drawing.Size(126, 13);
            this.lblNomeUsuario.TabIndex = 16;
            this.lblNomeUsuario.Text = "Digite o nome do Usuário";
            // 
            // txtCadCPF
            // 
            this.txtCadCPF.Location = new System.Drawing.Point(13, 66);
            this.txtCadCPF.Name = "txtCadCPF";
            this.txtCadCPF.Size = new System.Drawing.Size(201, 20);
            this.txtCadCPF.TabIndex = 15;
            // 
            // lblCadCPF
            // 
            this.lblCadCPF.AutoSize = true;
            this.lblCadCPF.Location = new System.Drawing.Point(12, 49);
            this.lblCadCPF.Name = "lblCadCPF";
            this.lblCadCPF.Size = new System.Drawing.Size(77, 13);
            this.lblCadCPF.TabIndex = 14;
            this.lblCadCPF.Text = "Digite seu CPF";
            // 
            // txtCadEmail
            // 
            this.txtCadEmail.Location = new System.Drawing.Point(12, 26);
            this.txtCadEmail.Name = "txtCadEmail";
            this.txtCadEmail.Size = new System.Drawing.Size(201, 20);
            this.txtCadEmail.TabIndex = 13;
            // 
            // lblCadEmail
            // 
            this.lblCadEmail.AutoSize = true;
            this.lblCadEmail.Location = new System.Drawing.Point(12, 9);
            this.lblCadEmail.Name = "lblCadEmail";
            this.lblCadEmail.Size = new System.Drawing.Size(82, 13);
            this.lblCadEmail.TabIndex = 12;
            this.lblCadEmail.Text = "Digite seu Email";
            // 
            // lblCadTipoUsuario
            // 
            this.lblCadTipoUsuario.AutoSize = true;
            this.lblCadTipoUsuario.Location = new System.Drawing.Point(527, 9);
            this.lblCadTipoUsuario.Name = "lblCadTipoUsuario";
            this.lblCadTipoUsuario.Size = new System.Drawing.Size(82, 13);
            this.lblCadTipoUsuario.TabIndex = 20;
            this.lblCadTipoUsuario.Text = "Tipo de Usuário";
            // 
            // cmbTipoUsuario
            // 
            this.cmbTipoUsuario.FormattingEnabled = true;
            this.cmbTipoUsuario.Location = new System.Drawing.Point(527, 49);
            this.cmbTipoUsuario.Name = "cmbTipoUsuario";
            this.cmbTipoUsuario.Size = new System.Drawing.Size(79, 21);
            this.cmbTipoUsuario.TabIndex = 21;
            // 
            // lblTipoUsuarioA_U
            // 
            this.lblTipoUsuarioA_U.AutoSize = true;
            this.lblTipoUsuarioA_U.Location = new System.Drawing.Point(527, 29);
            this.lblTipoUsuarioA_U.Name = "lblTipoUsuarioA_U";
            this.lblTipoUsuarioA_U.Size = new System.Drawing.Size(146, 13);
            this.lblTipoUsuarioA_U.TabIndex = 22;
            this.lblTipoUsuarioA_U.Text = "A = Admnistrador U = Usuário";
            // 
            // btnCadUsuario
            // 
            this.btnCadUsuario.Location = new System.Drawing.Point(12, 170);
            this.btnCadUsuario.Name = "btnCadUsuario";
            this.btnCadUsuario.Size = new System.Drawing.Size(199, 45);
            this.btnCadUsuario.TabIndex = 23;
            this.btnCadUsuario.Text = "Cadastrar Usuário";
            this.btnCadUsuario.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 144);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(199, 20);
            this.textBox1.TabIndex = 25;
            // 
            // lblSenha
            // 
            this.lblSenha.AutoSize = true;
            this.lblSenha.Location = new System.Drawing.Point(12, 128);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Size = new System.Drawing.Size(91, 13);
            this.lblSenha.TabIndex = 24;
            this.lblSenha.Text = "Digite uma Senha";
            // 
            // CadastroUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.hq720;
            this.ClientSize = new System.Drawing.Size(685, 385);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblSenha);
            this.Controls.Add(this.btnCadUsuario);
            this.Controls.Add(this.lblTipoUsuarioA_U);
            this.Controls.Add(this.cmbTipoUsuario);
            this.Controls.Add(this.lblCadTipoUsuario);
            this.Controls.Add(this.txtCadNomeUsuario);
            this.Controls.Add(this.lblNomeUsuario);
            this.Controls.Add(this.txtCadCPF);
            this.Controls.Add(this.lblCadCPF);
            this.Controls.Add(this.txtCadEmail);
            this.Controls.Add(this.lblCadEmail);
            this.Name = "CadastroUsuario";
            this.Text = "CadastroUsuario";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCadNomeUsuario;
        private System.Windows.Forms.Label lblNomeUsuario;
        private System.Windows.Forms.TextBox txtCadCPF;
        private System.Windows.Forms.Label lblCadCPF;
        private System.Windows.Forms.TextBox txtCadEmail;
        private System.Windows.Forms.Label lblCadEmail;
        private System.Windows.Forms.Label lblCadTipoUsuario;
        private System.Windows.Forms.ComboBox cmbTipoUsuario;
        private System.Windows.Forms.Label lblTipoUsuarioA_U;
        private System.Windows.Forms.Button btnCadUsuario;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblSenha;
    }
}