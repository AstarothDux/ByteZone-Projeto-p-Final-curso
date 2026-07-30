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
            this.btnCadUserAPP.Click += BtnCadUserAPP_Click;
        }

        // Cadastro para o site -> usa a tabela tbl_clientes
        private void BtnCadUsuario_Click(object sender, EventArgs e)
        {
            var email = txtCadEmail.Text.Trim();
            var cpf = txtCadCPF.Text.Trim();
            var nome = txtCadNomeUsuario.Text.Trim();
            var sobrenome = txtSobrenome.Text.Trim();
            var telefone = txtTelefone.Text.Trim();
            var senha = txtSenha.Text;
            var confirm = txtConfirmSenha.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(nome) ||
                string.IsNullOrEmpty(sobrenome) || string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(confirm))
            {
                MessageBox.Show("Preencha todos os campos do cadastro do site.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (senha != confirm)
            {
                MessageBox.Show("Senha e confirmação não conferem.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string hashedSenha = ComputeSha512Hex(senha);

                using (var conexao = new MySqlConnection(Variaveis.strConn))
                {
                    conexao.Open();

                    string checkSql = "SELECT COUNT(1) FROM tbl_clientes WHERE Email_Cliente = @email OR CPF_Cliente = @cpf";
                    using (var checkCmd = new MySqlCommand(checkSql, conexao))
                    {
                        checkCmd.Parameters.AddWithValue("@email", email);
                        checkCmd.Parameters.AddWithValue("@cpf", cpf);
                        var exists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (exists > 0)
                        {
                            MessageBox.Show("Email ou CPF já cadastrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string sql = @"INSERT INTO tbl_clientes (Nome_Cliente, Sobr_Cliente, CPF_Cliente, Email_Cliente, Tel_Cliente, SenhaHash)
                                   VALUES (@nome, @sobrenome, @email,@telefone,@cpf, @senha)";
                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@sobrenome", sobrenome);
                        cmd.Parameters.AddWithValue("@cpf", cpf);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@telefone", telefone);
                        cmd.Parameters.AddWithValue("@senha", hashedSenha);

                        var rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Cliente cadastrado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Falha ao cadastrar cliente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar cliente: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cadastro para o aplicativo -> usa a tabela tbl_usuario
        private void BtnCadUserAPP_Click(object sender, EventArgs e)
        {
            var nomeUsuario = txtNomeUsuarioAPP.Text.Trim();
            var senha = txtSenhaAPP.Text;
            var confirm = txtConfirmSenhaAPP.Text;

            if (string.IsNullOrEmpty(nomeUsuario) || string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(confirm))
            {
                MessageBox.Show("Preencha todos os campos do cadastro do app.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (senha != confirm)
            {
                MessageBox.Show("Senha e confirmação não conferem.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string hashedSenha = ComputeSha512Hex(senha);

                using (var conexao = new MySqlConnection(Variaveis.strConn))
                {
                    conexao.Open();

                    string checkSql = "SELECT COUNT(1) FROM tbl_usuario WHERE NomeUsuario = @nome";
                    using (var checkCmd = new MySqlCommand(checkSql, conexao))
                    {
                        checkCmd.Parameters.AddWithValue("@nome", nomeUsuario);
                        var exists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (exists > 0)
                        {
                            MessageBox.Show("Nome de usuário já cadastrado no aplicativo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string sql = "INSERT INTO tbl_usuario (NomeUsuario, SenhaHash) VALUES (@nome, @senha)";
                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@nome", nomeUsuario);
                        cmd.Parameters.AddWithValue("@senha", hashedSenha);

                        var rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Usuário do app cadastrado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtNomeUsuarioAPP.Clear();
                            txtSenhaAPP.Clear();
                            txtConfirmSenhaAPP.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Falha ao cadastrar usuário do app.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar usuário do app: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper: SHA-512 hex lowercase
        private static string ComputeSha512Hex(string input)
        {
            using (var sha = SHA512.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder();
                foreach (var b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        private void lblCadEmail_Click(object sender, EventArgs e)
        {
        }
    }
}