using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class LojaTech : Form
    {
        public LojaTech()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Conexão será aberta quando necessária nos formulários
            // Associar ações adicionais do menu
            this.produtosToolStripMenuItem.Click += produtosToolStripMenuItem_Click;
            this.alteraçãoToolStripMenuItem.Click += alteraçãoToolStripMenuItem_Click;
        }

        private void usuáriosAdminsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Abre o formulário de cadastro de usuários sempre que o menu for clicado
            CadastroUsuario cadastroUsuario = new CadastroUsuario();
            cadastroUsuario.ShowDialog();
        }

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ProdutosForm();
            form.ShowDialog();
        }

        private void alteraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Abrir listagem de produtos para seleção/alteração
            var form = new ProdutosForm();
            form.ShowDialog();
        }
    }
}
