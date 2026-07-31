using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class AlterarProduto : Form
    {
        public AlterarProduto()
        {
            InitializeComponent();
            btnSalvar.Click += BtnSalvar_Click;
            btnCancelar.Click += (s, e) => this.Close();

            cmbStatusProduto.Items.Clear();
            cmbStatusProduto.Items.Add("Disponível");
            cmbStatusProduto.Items.Add("Esgotado");
            cmbStatusProduto.DropDownStyle = ComboBoxStyle.DropDownList;
            // registrar combobox de produtos e carregar lista
            cmbProdutos.SelectedIndexChanged += CmbProdutos_SelectedIndexChanged;
            LoadProductsList();
        }

        // Construtor adicional para abrir o formulário já carregado pelo ID do produto
        public AlterarProduto(int id) : this()
        {
            LoadProductById(id);
        }

        private class ProductListItem
        {
            public int Id { get; set; }
            public string Display { get; set; }
            public override string ToString() => Display;
        }

        private void LoadProductsList()
        {
            try
            {
                cmbProdutos.Items.Clear();

                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();

                    string sql = "SELECT ID_Produto, Nome_Prod FROM tbl_produtos ORDER BY ID_Produto, Nome_Prod";

                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string nome = dr["Nome_Prod"]?.ToString() ?? "";
                            int id = dr["ID_Produto"] != DBNull.Value
                                ? Convert.ToInt32(dr["ID_Produto"])
                                : 0;

                            cmbProdutos.Items.Add(new ProductListItem
                            {
                                Id = id,
                                Display = nome
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao carregar lista de produtos: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CmbProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProdutos.SelectedItem is ProductListItem it && it.Id > 0)
            {
                LoadProductById(it.Id);
            }
        }

        private void LoadProductById(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();
                    string sql = "SELECT Nome_Prod,ID_Marca, ID_Categoria, Valor_Preco, ValorPromocional, QtdEstoque, PesoKG, StatusProduto FROM tbl_produtos WHERE ID_Produto = @ID_Produto LIMIT 1";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_Produto", id);
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtNomeProd.Text = dr["Nome_Prod"]?.ToString() ?? dr["Nome_Produto"]?.ToString() ?? string.Empty;
                                txtIDMarca.Text = dr["ID_Marca"]?.ToString() ?? string.Empty;
                                txtIDCategoria.Text = dr["ID_Categoria"]?.ToString() ?? string.Empty;
                                txtValorPreco.Text = dr["Valor_Preco"]?.ToString() ?? string.Empty;
                                txtValorPromocional.Text = dr["ValorPromocional"]?.ToString() ?? string.Empty;
                                txtQtdEstoque.Text = dr["QtdEstoque"]?.ToString() ?? string.Empty;
                                txtPesoKG.Text = dr["PesoKG"]?.ToString() ?? string.Empty;
                                var status = dr["StatusProduto"]?.ToString();
                                cmbStatusProduto.SelectedItem = status == "Inativo" ? "Inativo" : "Ativo";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

 /*       private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();
                    string sql = "SELECT Nome_Prod, ID_Marca, ID_Categoria, Valor_Preco, ValorPromocional, QtdEstoque, PesoKG, Slug, StatusProduto";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtNomeProd.Text = dr["Nome_Prod"]?.ToString();
                                txtIDMarca.Text = dr["ID_Marca"]?.ToString();
                                txtIDCategoria.Text = dr["ID_Categoria"]?.ToString();
                                txtValorPreco.Text = dr["Valor_Preco"]?.ToString();
                                txtValorPromocional.Text = dr["ValorPromocional"]?.ToString();
                                txtQtdEstoque.Text = dr["QtdEstoque"]?.ToString();
                                txtPesoKG.Text = dr["PesoKG"]?.ToString();
                                txtSlug.Text = dr["Slug"]?.ToString();
                                var status = dr["StatusProduto"]?.ToString();
                                cmbStatusProduto.SelectedItem = status == "Inativo" ? "Inativo" : "Ativo";
                            }
                            else
                            {
                                MessageBox.Show("Produto não encontrado.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
 */

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            // leitura e validação simples
            var nome = txtNomeProd.Text.Trim();
            var idMarca = txtIDMarca.Text.Trim();
            var idCategoria = txtIDCategoria.Text.Trim();
            var status = cmbStatusProduto.SelectedItem?.ToString() ?? "Ativo";

            if (string.IsNullOrEmpty(nome))
            {
                MessageBox.Show("Nome é Obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtValorPreco.Text.Trim(), out decimal valorPreco))
            {
                MessageBox.Show("Valor_Preco inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal.TryParse(txtValorPromocional.Text.Trim(), out decimal valorPromo);
            int.TryParse(txtQtdEstoque.Text.Trim(), out int qtdEstoque);
            decimal.TryParse(txtPesoKG.Text.Trim(), out decimal pesoKG);

            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();
                    string updateSql = @"
                        UPDATE tbl_produtos
                        SET Nome_Prod = @Nome_Prod,
                            ID_Marca = @ID_Marca,
                            ID_Categoria = @ID_Categoria,
                            Valor_Preco = @Valor_Preco,
                            ValorPromocional = @ValorPromocional,
                            QtdEstoque = @QtdEstoque,
                            PesoKG = @PesoKG,
                            StatusProduto = @StatusProduto";
                    using (var cmd = new MySqlCommand(updateSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome_Prod", nome);
                        cmd.Parameters.AddWithValue("@ID_Marca", idMarca);
                        cmd.Parameters.AddWithValue("@ID_Categoria", idCategoria);
                        cmd.Parameters.AddWithValue("@Valor_Preco", valorPreco);
                        cmd.Parameters.AddWithValue("@ValorPromocional", valorPromo);
                        cmd.Parameters.AddWithValue("@QtdEstoque", qtdEstoque);
                        cmd.Parameters.AddWithValue("@PesoKG", pesoKG);
                        cmd.Parameters.AddWithValue("@StatusProduto", status);
                        var rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Produto atualizado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Nenhuma alteração aplicada.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
