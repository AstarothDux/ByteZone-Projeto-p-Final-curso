using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public class EditarUsuarios : Form
    {
        private CheckBox chkModoApp;
        private TextBox txtSearch;
        private Button btnLoad;
        private Panel panelSite;
        private Panel panelApp;
        private Button btnSave;
        private Button btnCancel;

        // site fields
        private TextBox txtNome;
        private TextBox txtSobrenome;
        private TextBox txtEmail;
        private TextBox txtCPF;
        private TextBox txtTelefone;
        private TextBox txtSenhaSite;

        // app fields
        private TextBox txtNomeUsuario;
        private TextBox txtSenhaApp;

        private int currentClientId = 0;
        private int currentUserId = 0;

        public EditarUsuarios()
        {
            InitializeComponents();
            UpdateMode();
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(620, 380);
            this.Text = "Editar Usuários";
        }

        // P/Invoke para definir cue banner (watermark) em TextBox no .NET Framework
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        private void SetCueBanner(TextBox tb, string cue)
        {
            if (tb == null) return;
            try
            {
                SendMessage(tb.Handle, EM_SETCUEBANNER, (IntPtr)0, cue);
            }
            catch { }
        }

        private void InitializeComponents()
        {
            chkModoApp = new CheckBox { Text = "Modo App", Location = new Point(12, 12), AutoSize = true };
            chkModoApp.CheckedChanged += (s, e) => UpdateMode();

            txtSearch = new TextBox { Location = new Point(12, 40), Width = 380 };
            btnLoad = new Button { Text = "Carregar", Location = new Point(400, 38) };
            btnLoad.Click += BtnLoad_Click;

            // painel site
            panelSite = new Panel { Location = new Point(12, 80), Size = new Size(580, 220) };
            var lblNome = new Label { Text = "Nome", Location = new Point(0, 0) };
            txtNome = new TextBox { Location = new Point(0, 18), Width = 260 };
            var lblSob = new Label { Text = "Sobrenome", Location = new Point(280, 0) };
            txtSobrenome = new TextBox { Location = new Point(280, 18), Width = 260 };

            var lblEmail = new Label { Text = "Email", Location = new Point(0, 50) };
            txtEmail = new TextBox { Location = new Point(0, 68), Width = 260 };
            var lblCPF = new Label { Text = "CPF", Location = new Point(280, 50) };
            txtCPF = new TextBox { Location = new Point(280, 68), Width = 260 };

            var lblTel = new Label { Text = "Telefone", Location = new Point(0, 100) };
            txtTelefone = new TextBox { Location = new Point(0, 118), Width = 260 };
            var lblSenhaSite = new Label { Text = "Senha (apenas para reset)", Location = new Point(280, 100) };
            txtSenhaSite = new TextBox { Location = new Point(280, 118), Width = 260, UseSystemPasswordChar = true };

            panelSite.Controls.AddRange(new Control[] { lblNome, txtNome, lblSob, txtSobrenome, lblEmail, txtEmail, lblCPF, txtCPF, lblTel, txtTelefone, lblSenhaSite, txtSenhaSite });

            // painel app
            panelApp = new Panel { Location = new Point(12, 80), Size = new Size(580, 120) };
            var lblNomeUser = new Label { Text = "Nome de Usuário", Location = new Point(0, 0) };
            txtNomeUsuario = new TextBox { Location = new Point(0, 18), Width = 260 };
            var lblSenhaApp = new Label { Text = "Senha (apenas para reset)", Location = new Point(280, 0) };
            txtSenhaApp = new TextBox { Location = new Point(280, 18), Width = 260, UseSystemPasswordChar = true };

            panelApp.Controls.AddRange(new Control[] { lblNomeUser, txtNomeUsuario, lblSenhaApp, txtSenhaApp });

            btnSave = new Button { Text = "Salvar", Location = new Point(420, 320), Width = 80 };
            btnCancel = new Button { Text = "Cancelar", Location = new Point(520, 320), Width = 80 };
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { chkModoApp, txtSearch, btnLoad, panelSite, panelApp, btnSave, btnCancel });
        }

        private void UpdateMode()
        {
            bool app = chkModoApp.Checked;
            panelApp.Visible = app;
            panelSite.Visible = !app;
            // PlaceholderText não existe no .NET Framework 4.7.2; usar cue banner via WinAPI
            SetCueBanner(txtSearch, app ? "Digite nome de usuário" : "Digite email ou CPF");
            ClearFields();
        }

        private void ClearFields()
        {
            currentClientId = 0;
            currentUserId = 0;
            txtNome.Text = txtSobrenome.Text = txtEmail.Text = txtCPF.Text = txtTelefone.Text = txtSenhaSite.Text = string.Empty;
            txtNomeUsuario.Text = txtSenhaApp.Text = string.Empty;
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            if (chkModoApp.Checked)
            {
                LoadAppUser(txtSearch.Text.Trim());
            }
            else
            {
                LoadSiteClient(txtSearch.Text.Trim());
            }
        }

        private void LoadSiteClient(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) { MessageBox.Show("Informe email ou CPF para busca."); return; }
            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();
                    string sql = "SELECT ID_Cliente, Nome_Cliente, Sobr_Cliente, Email_Cliente, CPF_Cliente, Tel_Cliente FROM tbl_clientes WHERE Email_Cliente = @k OR CPF_Cliente = @k LIMIT 1";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@k", key);
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                currentClientId = dr["ID_Cliente"] != DBNull.Value ? Convert.ToInt32(dr["ID_Cliente"]) : 0;
                                txtNome.Text = dr["Nome_Cliente"]?.ToString() ?? string.Empty;
                                txtSobrenome.Text = dr["Sobr_Cliente"]?.ToString() ?? string.Empty;
                                txtEmail.Text = dr["Email_Cliente"]?.ToString() ?? string.Empty;
                                txtCPF.Text = dr["CPF_Cliente"]?.ToString() ?? string.Empty;
                                txtTelefone.Text = dr["Tel_Cliente"]?.ToString() ?? string.Empty;
                                MessageBox.Show("Cliente carregado.");
                            }
                            else MessageBox.Show("Cliente não encontrado.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void LoadAppUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) { MessageBox.Show("Informe nome de usuário para busca."); return; }
            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();
                    string sql = "SELECT ID_Usuario, NomeUsuario FROM tbl_usuarios WHERE NomeUsuario = @u LIMIT 1";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", username);
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                currentUserId = dr["ID_Usuario"] != DBNull.Value ? Convert.ToInt32(dr["ID_Usuario"]) : 0;
                                txtNomeUsuario.Text = dr["NomeUsuario"]?.ToString() ?? string.Empty;
                                MessageBox.Show("Usuário do app carregado.");
                            }
                            else MessageBox.Show("Usuário não encontrado.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (chkModoApp.Checked)
            {
                SaveAppUser();
            }
            else
            {
                SaveSiteClient();
            }
        }

        private void SaveSiteClient()
        {
            if (currentClientId == 0) { MessageBox.Show("Nenhum cliente carregado para atualizar."); return; }
            var nome = txtNome.Text.Trim();
            var sobrenome = txtSobrenome.Text.Trim();
            var email = txtEmail.Text.Trim();
            var cpf = txtCPF.Text.Trim();
            var telefone = txtTelefone.Text.Trim();
            var senha = txtSenhaSite.Text;

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(sobrenome) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Preencha nome, sobrenome e email.");
                return;
            }

            if (!IsValidEmail(email)) { MessageBox.Show("Email inválido."); return; }
            if (!IsValidCpf(cpf)) { MessageBox.Show("CPF inválido."); return; }

            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();
                    string sql = "UPDATE tbl_clientes SET Nome_Cliente=@n, Sobr_Cliente=@s, Email_Cliente=@e, CPF_Cliente=@c, Tel_Cliente=@t";
                    if (!string.IsNullOrEmpty(senha))
                    {
                        // substituir senha apenas se informado
                        var hash = ComputeSha512Hex(senha);
                        sql += ", SenhaHash=@h";
                    }
                    sql += " WHERE ID_Cliente = @id";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@n", nome);
                        cmd.Parameters.AddWithValue("@s", sobrenome);
                        cmd.Parameters.AddWithValue("@e", email);
                        cmd.Parameters.AddWithValue("@c", cpf);
                        cmd.Parameters.AddWithValue("@t", telefone);
                        if (!string.IsNullOrEmpty(senha)) cmd.Parameters.AddWithValue("@h", ComputeSha512Hex(senha));
                        cmd.Parameters.AddWithValue("@id", currentClientId);
                        var rows = cmd.ExecuteNonQuery();
                        if (rows > 0) MessageBox.Show("Cliente atualizado com sucesso."); else MessageBox.Show("Nenhuma alteração aplicada.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar: " + ex.Message);
            }
        }

        private void SaveAppUser()
        {
            if (currentUserId == 0) { MessageBox.Show("Nenhum usuário do app carregado para atualizar."); return; }
            var nomeUsuario = txtNomeUsuario.Text.Trim();
            var senha = txtSenhaApp.Text;
            if (string.IsNullOrEmpty(nomeUsuario)) { MessageBox.Show("Nome de usuário obrigatório."); return; }
            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();
                    string sql = "UPDATE tbl_usuarios SET NomeUsuario=@n";
                    if (!string.IsNullOrEmpty(senha)) sql += ", SenhaHash=@h";
                    sql += " WHERE ID_Usuario = @id";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@n", nomeUsuario);
                        if (!string.IsNullOrEmpty(senha)) cmd.Parameters.AddWithValue("@h", ComputeSha512Hex(senha));
                        cmd.Parameters.AddWithValue("@id", currentUserId);
                        var rows = cmd.ExecuteNonQuery();
                        if (rows > 0) MessageBox.Show("Usuário do app atualizado com sucesso."); else MessageBox.Show("Nenhuma alteração aplicada.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar: " + ex.Message);
            }
        }

        // Utilitários de validação e hash (duplicados para permitir form funcionar isoladamente)
        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            const string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        private static bool IsValidCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;
            var digits = Regex.Replace(cpf, "[^0-9]", "");
            if (digits.Length != 11) return false;
            var first = digits[0];
            if (digits.All(c => c == first)) return false;
            int[] numbers = digits.Select(c => c - '0').ToArray();
            int sum = 0;
            for (int i = 0; i < 9; i++) sum += numbers[i] * (10 - i);
            int rem = sum % 11;
            int dv1 = rem < 2 ? 0 : 11 - rem;
            if (numbers[9] != dv1) return false;
            sum = 0;
            for (int i = 0; i < 10; i++) sum += numbers[i] * (11 - i);
            rem = sum % 11;
            int dv2 = rem < 2 ? 0 : 11 - rem;
            if (numbers[10] != dv2) return false;
            return true;
        }

        private static string ComputeSha512Hex(string input)
        {
            using (var sha = System.Security.Cryptography.SHA512.Create())
            {
                var bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
                var sb = new System.Text.StringBuilder();
                foreach (var b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
