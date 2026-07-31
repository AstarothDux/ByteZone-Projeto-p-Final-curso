namespace WindowsFormsApp1
{
    partial class AlterarProduto
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblProdutos;
        private System.Windows.Forms.ComboBox cmbProdutos;
        private System.Windows.Forms.Label lblNomeProd;
        private System.Windows.Forms.TextBox txtNomeProd;
        private System.Windows.Forms.Label lblIDMarca;
        private System.Windows.Forms.TextBox txtIDMarca;
        private System.Windows.Forms.Label lblIDCategoria;
        private System.Windows.Forms.TextBox txtIDCategoria;
        private System.Windows.Forms.Label lblValorPreco;
        private System.Windows.Forms.TextBox txtValorPreco;
        private System.Windows.Forms.Label lblValorPromocional;
        private System.Windows.Forms.TextBox txtValorPromocional;
        private System.Windows.Forms.Label lblQtdEstoque;
        private System.Windows.Forms.TextBox txtQtdEstoque;
        private System.Windows.Forms.Label lblPesoKG;
        private System.Windows.Forms.TextBox txtPesoKG;
        private System.Windows.Forms.Label lblStatusProduto;
        private System.Windows.Forms.ComboBox cmbStatusProduto;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;

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
            this.lblProdutos = new System.Windows.Forms.Label();
            this.cmbProdutos = new System.Windows.Forms.ComboBox();
            this.lblNomeProd = new System.Windows.Forms.Label();
            this.txtNomeProd = new System.Windows.Forms.TextBox();
            this.lblIDMarca = new System.Windows.Forms.Label();
            this.txtIDMarca = new System.Windows.Forms.TextBox();
            this.lblIDCategoria = new System.Windows.Forms.Label();
            this.txtIDCategoria = new System.Windows.Forms.TextBox();
            this.lblValorPreco = new System.Windows.Forms.Label();
            this.txtValorPreco = new System.Windows.Forms.TextBox();
            this.lblValorPromocional = new System.Windows.Forms.Label();
            this.txtValorPromocional = new System.Windows.Forms.TextBox();
            this.lblQtdEstoque = new System.Windows.Forms.Label();
            this.txtQtdEstoque = new System.Windows.Forms.TextBox();
            this.lblPesoKG = new System.Windows.Forms.Label();
            this.txtPesoKG = new System.Windows.Forms.TextBox();
            this.lblStatusProduto = new System.Windows.Forms.Label();
            this.cmbStatusProduto = new System.Windows.Forms.ComboBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblProdutos
            // 
            this.lblProdutos.AutoSize = true;
            this.lblProdutos.Location = new System.Drawing.Point(12, 5);
            this.lblProdutos.Name = "lblProdutos";
            this.lblProdutos.Size = new System.Drawing.Size(49, 13);
            this.lblProdutos.TabIndex = 25;
            this.lblProdutos.Text = "Produtos";
            // 
            // cmbProdutos
            // 
            this.cmbProdutos.FormattingEnabled = true;
            this.cmbProdutos.Location = new System.Drawing.Point(15, 21);
            this.cmbProdutos.Name = "cmbProdutos";
            this.cmbProdutos.Size = new System.Drawing.Size(281, 21);
            this.cmbProdutos.TabIndex = 26;
            // 
            // lblNomeProd
            // 
            this.lblNomeProd.AutoSize = true;
            this.lblNomeProd.Location = new System.Drawing.Point(12, 95);
            this.lblNomeProd.Name = "lblNomeProd";
            this.lblNomeProd.Size = new System.Drawing.Size(90, 13);
            this.lblNomeProd.TabIndex = 3;
            this.lblNomeProd.Text = "Nome do Produto";
            // 
            // txtNomeProd
            // 
            this.txtNomeProd.Location = new System.Drawing.Point(15, 111);
            this.txtNomeProd.Name = "txtNomeProd";
            this.txtNomeProd.Size = new System.Drawing.Size(281, 20);
            this.txtNomeProd.TabIndex = 4;
            // 
            // lblIDMarca
            // 
            this.lblIDMarca.AutoSize = true;
            this.lblIDMarca.Location = new System.Drawing.Point(12, 185);
            this.lblIDMarca.Name = "lblIDMarca";
            this.lblIDMarca.Size = new System.Drawing.Size(54, 13);
            this.lblIDMarca.TabIndex = 7;
            this.lblIDMarca.Text = "ID_Marca";
            // 
            // txtIDMarca
            // 
            this.txtIDMarca.Location = new System.Drawing.Point(15, 201);
            this.txtIDMarca.Name = "txtIDMarca";
            this.txtIDMarca.Size = new System.Drawing.Size(100, 20);
            this.txtIDMarca.TabIndex = 8;
            // 
            // lblIDCategoria
            // 
            this.lblIDCategoria.AutoSize = true;
            this.lblIDCategoria.Location = new System.Drawing.Point(131, 185);
            this.lblIDCategoria.Name = "lblIDCategoria";
            this.lblIDCategoria.Size = new System.Drawing.Size(69, 13);
            this.lblIDCategoria.TabIndex = 9;
            this.lblIDCategoria.Text = "ID_Categoria";
            // 
            // txtIDCategoria
            // 
            this.txtIDCategoria.Location = new System.Drawing.Point(134, 201);
            this.txtIDCategoria.Name = "txtIDCategoria";
            this.txtIDCategoria.Size = new System.Drawing.Size(100, 20);
            this.txtIDCategoria.TabIndex = 10;
            // 
            // lblValorPreco
            // 
            this.lblValorPreco.AutoSize = true;
            this.lblValorPreco.Location = new System.Drawing.Point(12, 230);
            this.lblValorPreco.Name = "lblValorPreco";
            this.lblValorPreco.Size = new System.Drawing.Size(65, 13);
            this.lblValorPreco.TabIndex = 11;
            this.lblValorPreco.Text = "Valor_Preco";
            // 
            // txtValorPreco
            // 
            this.txtValorPreco.Location = new System.Drawing.Point(15, 246);
            this.txtValorPreco.Name = "txtValorPreco";
            this.txtValorPreco.Size = new System.Drawing.Size(100, 20);
            this.txtValorPreco.TabIndex = 12;
            // 
            // lblValorPromocional
            // 
            this.lblValorPromocional.AutoSize = true;
            this.lblValorPromocional.Location = new System.Drawing.Point(131, 230);
            this.lblValorPromocional.Name = "lblValorPromocional";
            this.lblValorPromocional.Size = new System.Drawing.Size(89, 13);
            this.lblValorPromocional.TabIndex = 13;
            this.lblValorPromocional.Text = "ValorPromocional";
            // 
            // txtValorPromocional
            // 
            this.txtValorPromocional.Location = new System.Drawing.Point(134, 246);
            this.txtValorPromocional.Name = "txtValorPromocional";
            this.txtValorPromocional.Size = new System.Drawing.Size(100, 20);
            this.txtValorPromocional.TabIndex = 14;
            // 
            // lblQtdEstoque
            // 
            this.lblQtdEstoque.AutoSize = true;
            this.lblQtdEstoque.Location = new System.Drawing.Point(12, 275);
            this.lblQtdEstoque.Name = "lblQtdEstoque";
            this.lblQtdEstoque.Size = new System.Drawing.Size(63, 13);
            this.lblQtdEstoque.TabIndex = 15;
            this.lblQtdEstoque.Text = "QtdEstoque";
            // 
            // txtQtdEstoque
            // 
            this.txtQtdEstoque.Location = new System.Drawing.Point(15, 291);
            this.txtQtdEstoque.Name = "txtQtdEstoque";
            this.txtQtdEstoque.Size = new System.Drawing.Size(100, 20);
            this.txtQtdEstoque.TabIndex = 16;
            // 
            // lblPesoKG
            // 
            this.lblPesoKG.AutoSize = true;
            this.lblPesoKG.Location = new System.Drawing.Point(131, 275);
            this.lblPesoKG.Name = "lblPesoKG";
            this.lblPesoKG.Size = new System.Drawing.Size(46, 13);
            this.lblPesoKG.TabIndex = 17;
            this.lblPesoKG.Text = "PesoKG";
            // 
            // txtPesoKG
            // 
            this.txtPesoKG.Location = new System.Drawing.Point(134, 291);
            this.txtPesoKG.Name = "txtPesoKG";
            this.txtPesoKG.Size = new System.Drawing.Size(100, 20);
            this.txtPesoKG.TabIndex = 18;
            // 
            // lblStatusProduto
            // 
            this.lblStatusProduto.AutoSize = true;
            this.lblStatusProduto.Location = new System.Drawing.Point(12, 365);
            this.lblStatusProduto.Name = "lblStatusProduto";
            this.lblStatusProduto.Size = new System.Drawing.Size(74, 13);
            this.lblStatusProduto.TabIndex = 21;
            this.lblStatusProduto.Text = "StatusProduto";
            // 
            // cmbStatusProduto
            // 
            this.cmbStatusProduto.FormattingEnabled = true;
            this.cmbStatusProduto.Location = new System.Drawing.Point(15, 381);
            this.cmbStatusProduto.Name = "cmbStatusProduto";
            this.cmbStatusProduto.Size = new System.Drawing.Size(121, 21);
            this.cmbStatusProduto.TabIndex = 22;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(15, 420);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(100, 30);
            this.btnSalvar.TabIndex = 23;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(196, 420);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.TabIndex = 24;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // AlterarProduto
            // 
            this.ClientSize = new System.Drawing.Size(314, 466);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.cmbStatusProduto);
            this.Controls.Add(this.lblStatusProduto);
            this.Controls.Add(this.txtPesoKG);
            this.Controls.Add(this.lblPesoKG);
            this.Controls.Add(this.txtQtdEstoque);
            this.Controls.Add(this.lblQtdEstoque);
            this.Controls.Add(this.txtValorPromocional);
            this.Controls.Add(this.lblValorPromocional);
            this.Controls.Add(this.txtValorPreco);
            this.Controls.Add(this.lblValorPreco);
            this.Controls.Add(this.txtIDCategoria);
            this.Controls.Add(this.lblIDCategoria);
            this.Controls.Add(this.txtIDMarca);
            this.Controls.Add(this.lblIDMarca);
            this.Controls.Add(this.txtNomeProd);
            this.Controls.Add(this.lblNomeProd);
            this.Controls.Add(this.cmbProdutos);
            this.Controls.Add(this.lblProdutos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AlterarProduto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Alterar Produto";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
