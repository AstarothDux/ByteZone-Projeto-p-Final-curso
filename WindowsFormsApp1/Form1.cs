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

        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (produtosToolStripMenuItem.Pressed)
            {
                CadastrarProduto cadastrarProduto = new CadastrarProduto();
                cadastrarProduto.Show();
            }
        }

        private void usuáriosAdminsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usuáriosAdminsToolStripMenuItem.Pressed)
            {
                CadastroUsuario cadastroUsuario = new CadastroUsuario();
                cadastroUsuario.Show();
            }
        }
    }
}
