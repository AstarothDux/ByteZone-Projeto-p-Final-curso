using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class CadastroUsuario : Form
    {
        public CadastroUsuario()
        {
            InitializeComponent();
            this.btnCadUsuario.Click += BtnCadUsuario_Click;
        }

        private void BtnCadUsuario_Click(object sender, EventArgs e)
        {
            var email = txtCadEmail.Text.Trim();
            var cpf = txtCadCPF.Text.Trim();
            var nome = txtCadNomeUsuario.Text.Trim();
            var senha = txtSenha.Text; // campo de senha
            var tipo = cmbTipoUsuario.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(tipo))
            {
                MessageBox.Show("Preencha todos os campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // calcular hash SHA512
                string hashedSenha;
                using (var sha = SHA512.Create())
                {
                    var hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(senha));
                    var sb = new StringBuilder();
                    foreach (var b in hashBytes)
                        sb.Append(b.ToString("x2"));
                    hashedSenha = sb.ToString();
                }

                using (var conexao = new MySqlConnection(Variaveis.strConn))
                {
                    conexao.Open();

                    // verificar duplicidade de usuário ou email
                    string checkSql = "SELECT COUNT(1) FROM tbl_usuarios WHERE NomeUsuario = @nome OR Email = @email";
                    using (var checkCmd = new MySqlCommand(checkSql, conexao))
                    {
                        checkCmd.Parameters.AddWithValue("@nome", nome);
                        checkCmd.Parameters.AddWithValue("@email", email);
                        var exists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (exists > 0)
                        {
                            MessageBox.Show("Usuário ou email já cadastrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string sql = "INSERT INTO tbl_usuarios (NomeUsuario, Email, CPF, TipoUsuario, SenhaHash) VALUES (@nome, @email, @cpf, @tipo, @senha)";
                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@cpf", cpf);
                        cmd.Parameters.AddWithValue("@tipo", tipo);
                        cmd.Parameters.AddWithValue("@senha", hashedSenha);

                        var rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Usuário cadastrado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Falha ao cadastrar usuário.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblCadEmail_Click(object sender, EventArgs e)
        {

        }
    }
}
