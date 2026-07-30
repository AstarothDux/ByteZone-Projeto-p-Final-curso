using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class AlterarProduto : Form
    {
        private string originalSKU = null;

        public AlterarProduto()
        {
            InitializeComponent();
            btnBuscar.Click += BtnBuscar_Click;
            btnSalvar.Click += BtnSalvar_Click;
            btnCancelar.Click += (s, e) => this.Close();

            cmbStatusProduto.Items.Clear();
            cmbStatusProduto.Items.Add("Ativo");
            cmbStatusProduto.Items.Add("Inativo");
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
                    string sql = "SELECT id, Nome_Prod, Nome_Produto, SKU FROM tbl_produtos ORDER BY Nome_Prod";
                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var nome = dr["Nome_Prod"]?.ToString();
                            if (string.IsNullOrEmpty(nome)) nome = dr["Nome_Produto"]?.ToString();
                            var sku = dr["SKU"]?.ToString() ?? string.Empty;
                            var id = dr["id"] != null && dr["id"] != DBNull.Value ? Convert.ToInt32(dr["id"]) : 0;
                            cmbProdutos.Items.Add(new ProductListItem { Id = id, Display = (nome ?? string.Empty) + " - " + sku });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar lista de produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string sql = "SELECT Nome_Prod, Nome_Produto, SKU, ID_Marca, ID_Categoria, Valor_Preco, ValorPromocional, QtdEstoque, PesoKG, Slug, StatusProduto FROM tbl_produtos WHERE id = @id LIMIT 1";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtNomeProd.Text = dr["Nome_Prod"]?.ToString() ?? dr["Nome_Produto"]?.ToString() ?? string.Empty;
                                txtSKU.Text = dr["SKU"]?.ToString() ?? string.Empty;
                                txtIDMarca.Text = dr["ID_Marca"]?.ToString() ?? string.Empty;
                                txtIDCategoria.Text = dr["ID_Categoria"]?.ToString() ?? string.Empty;
                                txtValorPreco.Text = dr["Valor_Preco"]?.ToString() ?? string.Empty;
                                txtValorPromocional.Text = dr["ValorPromocional"]?.ToString() ?? string.Empty;
                                txtQtdEstoque.Text = dr["QtdEstoque"]?.ToString() ?? string.Empty;
                                txtPesoKG.Text = dr["PesoKG"]?.ToString() ?? string.Empty;
                                txtSlug.Text = dr["Slug"]?.ToString() ?? string.Empty;
                                var status = dr["StatusProduto"]?.ToString();
                                cmbStatusProduto.SelectedItem = status == "Inativo" ? "Inativo" : "Ativo";
                                originalSKU = txtSKU.Text;
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

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            var sku = txtSKUSearch.Text.Trim();
            if (string.IsNullOrEmpty(sku))
            {
                MessageBox.Show("Informe o SKU para buscar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();
                    string sql = "SELECT Nome_Prod, SKU, ID_Marca, ID_Categoria, Valor_Preco, ValorPromocional, QtdEstoque, PesoKG, Slug, StatusProduto FROM tbl_produtos WHERE SKU = @sku LIMIT 1";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@sku", sku);
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtNomeProd.Text = dr["Nome_Prod"]?.ToString();
                                txtSKU.Text = dr["SKU"]?.ToString();
                                txtIDMarca.Text = dr["ID_Marca"]?.ToString();
                                txtIDCategoria.Text = dr["ID_Categoria"]?.ToString();
                                txtValorPreco.Text = dr["Valor_Preco"]?.ToString();
                                txtValorPromocional.Text = dr["ValorPromocional"]?.ToString();
                                txtQtdEstoque.Text = dr["QtdEstoque"]?.ToString();
                                txtPesoKG.Text = dr["PesoKG"]?.ToString();
                                txtSlug.Text = dr["Slug"]?.ToString();
                                var status = dr["StatusProduto"]?.ToString();
                                cmbStatusProduto.SelectedItem = status == "Inativo" ? "Inativo" : "Ativo";
                                originalSKU = txtSKU.Text;
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

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(originalSKU))
            {
                MessageBox.Show("Busque um produto antes de salvar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // leitura e validação simples
            var nome = txtNomeProd.Text.Trim();
            var sku = txtSKU.Text.Trim();
            var idMarca = txtIDMarca.Text.Trim();
            var idCategoria = txtIDCategoria.Text.Trim();
            var slug = txtSlug.Text.Trim();
            var status = cmbStatusProduto.SelectedItem?.ToString() ?? "Ativo";

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(sku))
            {
                MessageBox.Show("Nome e SKU são obrigatórios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            SKU = @SKU,
                            ID_Marca = @ID_Marca,
                            ID_Categoria = @ID_Categoria,
                            Valor_Preco = @Valor_Preco,
                            ValorPromocional = @ValorPromocional,
                            QtdEstoque = @QtdEstoque,
                            PesoKG = @PesoKG,
                            Slug = @Slug,
                            StatusProduto = @StatusProduto
                        WHERE SKU = @OriginalSKU
                        LIMIT 1";
                    using (var cmd = new MySqlCommand(updateSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome_Prod", nome);
                        cmd.Parameters.AddWithValue("@SKU", sku);
                        cmd.Parameters.AddWithValue("@ID_Marca", idMarca);
                        cmd.Parameters.AddWithValue("@ID_Categoria", idCategoria);
                        cmd.Parameters.AddWithValue("@Valor_Preco", valorPreco);
                        cmd.Parameters.AddWithValue("@ValorPromocional", valorPromo);
                        cmd.Parameters.AddWithValue("@QtdEstoque", qtdEstoque);
                        cmd.Parameters.AddWithValue("@PesoKG", pesoKG);
                        cmd.Parameters.AddWithValue("@Slug", slug);
                        cmd.Parameters.AddWithValue("@StatusProduto", status);
                        cmd.Parameters.AddWithValue("@OriginalSKU", originalSKU);

                        var rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Produto atualizado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // atualizar originalSKU se SKU mudou
                            originalSKU = sku;
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
