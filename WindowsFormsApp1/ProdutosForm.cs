using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public class Product
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Valor { get; set; }
        public override string ToString() => Nome + " - " + Valor;
    }

    public class ProdutosForm : Form
    {
        private ListBox lstProdutos;
        private Button btnEditar;

        public ProdutosForm()
        {
            this.Text = "Produtos";
            this.Width = 500;
            this.Height = 400;

            lstProdutos = new ListBox() { Left = 10, Top = 10, Width = 460, Height = 300 };
            btnEditar = new Button() { Left = 10, Top = 320, Width = 120, Text = "Editar Selecionado" };

            btnEditar.Click += BtnEditar_Click;

            this.Controls.Add(lstProdutos);
            this.Controls.Add(btnEditar);

            Load += ProdutosForm_Load;
        }

        private void ProdutosForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            lstProdutos.Items.Clear();
            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();
                    string sql = "SELECT id, Nome_Produto, Valor FROM tbl_produtos";
                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var p = new Product();
                            p.Id = Convert.ToInt32(dr["id"]);
                            p.Nome = dr["Nome_Produto"].ToString();
                            p.Valor = dr["Valor"].ToString();
                            lstProdutos.Items.Add(p);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (lstProdutos.SelectedItem is Product p)
            {
                var form = new AlterarProduto(p.Id);
                form.ShowDialog();
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Selecione um produto para editar.");
            }
        }
    }
}
