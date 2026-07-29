using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public class AlterarProduto : Form
    {
        private int produtoId;
        private TextBox txtNome;
        private TextBox txtValor;
        private Button btnSalvar;
        private Button btnCancelar;

        public AlterarProduto(int id)
        {
            produtoId = id;
            InitializeComponents();
            Load += AlterarProduto_Load;
        }

        private void InitializeComponents()
        {
            this.Text = "Alterar Produto";
            this.Width = 400;
            this.Height = 220;

            var lblNome = new Label() { Left = 10, Top = 20, Text = "Nome:", Width = 80 };
            txtNome = new TextBox() { Left = 100, Top = 18, Width = 260 };

            var lblValor = new Label() { Left = 10, Top = 60, Text = "Valor:", Width = 80 };
            txtValor = new TextBox() { Left = 100, Top = 58, Width = 260 };

            btnSalvar = new Button() { Left = 100, Top = 100, Width = 120, Text = "Salvar" };
            btnCancelar = new Button() { Left = 240, Top = 100, Width = 120, Text = "Cancelar" };

            btnSalvar.Click += BtnSalvar_Click;
            btnCancelar.Click += (s, e) => this.Close();

            this.Controls.Add(lblNome);
            this.Controls.Add(txtNome);
            this.Controls.Add(lblValor);
            this.Controls.Add(txtValor);
            this.Controls.Add(btnSalvar);
            this.Controls.Add(btnCancelar);
        }

        private void AlterarProduto_Load(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();
                    string sql = "SELECT Nome_Produto, Valor FROM tbl_produtos WHERE id = @id";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", produtoId);
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtNome.Text = dr["Nome_Produto"].ToString();
                                txtValor.Text = dr["Valor"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Produto não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            var nome = txtNome.Text.Trim();
            var valor = txtValor.Text.Trim();

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(valor))
            {
                MessageBox.Show("Preencha nome e valor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();
                    string sql = "UPDATE tbl_produtos SET Nome_Produto = @nome, Valor = @valor WHERE id = @id";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@valor", valor);
                        cmd.Parameters.AddWithValue("@id", produtoId);

                        var rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Produto atualizado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Nenhuma alteração aplicada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
