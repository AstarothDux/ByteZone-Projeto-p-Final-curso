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
            var conexao = new MySqlConnection(Variaveis.strConn);
            conexao.Open();
            
        }

        private void usuáriosAdminsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usuáriosAdminsToolStripMenuItem.Pressed)
            {
                CadastroUsuario cadastroUsuario = new CadastroUsuario();
                cadastroUsuario.Show();
            }
        }

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
