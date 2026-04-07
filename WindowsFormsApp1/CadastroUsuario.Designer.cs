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
            this.txtCadSobrUsuario = new System.Windows.Forms.TextBox();
            this.lblCadSobrUsuario = new System.Windows.Forms.Label();
            this.lblCadTipoUsuario = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblTipoUsuarioA_U = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCadNomeUsuario
            // 
            this.txtCadNomeUsuario.Location = new System.Drawing.Point(15, 105);
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
            // txtCadSobrUsuario
            // 
            this.txtCadSobrUsuario.Location = new System.Drawing.Point(15, 144);
            this.txtCadSobrUsuario.Name = "txtCadSobrUsuario";
            this.txtCadSobrUsuario.Size = new System.Drawing.Size(199, 20);
            this.txtCadSobrUsuario.TabIndex = 19;
            // 
            // lblCadSobrUsuario
            // 
            this.lblCadSobrUsuario.AutoSize = true;
            this.lblCadSobrUsuario.Location = new System.Drawing.Point(12, 128);
            this.lblCadSobrUsuario.Name = "lblCadSobrUsuario";
            this.lblCadSobrUsuario.Size = new System.Drawing.Size(152, 13);
            this.lblCadSobrUsuario.TabIndex = 18;
            this.lblCadSobrUsuario.Text = "Digite o sobrenome do Usuário";
            // 
            // lblCadTipoUsuario
            // 
            this.lblCadTipoUsuario.AutoSize = true;
            this.lblCadTipoUsuario.Location = new System.Drawing.Point(642, 9);
            this.lblCadTipoUsuario.Name = "lblCadTipoUsuario";
            this.lblCadTipoUsuario.Size = new System.Drawing.Size(82, 13);
            this.lblCadTipoUsuario.TabIndex = 20;
            this.lblCadTipoUsuario.Text = "Tipo de Usuário";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(645, 45);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(79, 21);
            this.comboBox1.TabIndex = 21;
            // 
            // lblTipoUsuarioA_U
            // 
            this.lblTipoUsuarioA_U.AutoSize = true;
            this.lblTipoUsuarioA_U.Location = new System.Drawing.Point(642, 29);
            this.lblTipoUsuarioA_U.Name = "lblTipoUsuarioA_U";
            this.lblTipoUsuarioA_U.Size = new System.Drawing.Size(146, 13);
            this.lblTipoUsuarioA_U.TabIndex = 22;
            this.lblTipoUsuarioA_U.Text = "A = Admnistrador U = Usuário";
            // 
            // CadastroUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblTipoUsuarioA_U);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblCadTipoUsuario);
            this.Controls.Add(this.txtCadSobrUsuario);
            this.Controls.Add(this.lblCadSobrUsuario);
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
        private System.Windows.Forms.TextBox txtCadSobrUsuario;
        private System.Windows.Forms.Label lblCadSobrUsuario;
        private System.Windows.Forms.Label lblCadTipoUsuario;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblTipoUsuarioA_U;
    }
}