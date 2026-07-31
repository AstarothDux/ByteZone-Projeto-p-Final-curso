using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class CadastroUsuario : Form
    {
        // campos auxiliares para máscaras e botões de visibilidade
        private bool _formattingCpf = false;
        private Button btnToggleSenhaSite;
        private Button btnToggleConfirmSenhaSite;
        private Button btnToggleSenhaApp;
        private Button btnToggleConfirmSenhaApp;

        public CadastroUsuario()
        {
            InitializeComponent();
            // evitar corte de conteúdo em resoluções menores: permitir scroll e escalonamento por fonte
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoScroll = true;
            this.btnCadUsuario.Click += BtnCadUsuario_Click;
            this.btnCadUserAPP.Click += BtnCadUserAPP_Click;
            // configurar mascaramento padrão das senhas
            try
            {
                txtSenha.PasswordChar = '*';
                txtConfirmSenha.PasswordChar = '*';
                txtSenhaAPP.PasswordChar = '*';
                txtConfirmSenhaAPP.PasswordChar = '*';
                txtSenha.UseSystemPasswordChar = true;
                txtConfirmSenha.UseSystemPasswordChar = true;
                txtSenhaAPP.UseSystemPasswordChar = true;
                txtConfirmSenhaAPP.UseSystemPasswordChar = true;
            }
            catch { }

            // botões para alternar visibilidade das senhas (criados em tempo de execução ao lado dos TextBox existentes)
            CreateToggleButton(ref btnToggleSenhaSite, txtSenha, "Ver", ToggleSenhaSite_Click);
            CreateToggleButton(ref btnToggleConfirmSenhaSite, txtConfirmSenha, "Ver", ToggleConfirmSenhaSite_Click);
            CreateToggleButton(ref btnToggleSenhaApp, txtSenhaAPP, "Ver", ToggleSenhaApp_Click);
            CreateToggleButton(ref btnToggleConfirmSenhaApp, txtConfirmSenhaAPP, "Ver", ToggleConfirmSenhaApp_Click);

            // máscara/formatador para CPF
            txtCadCPF.TextChanged += TxtCadCPF_TextChanged;

            // posicionamento dinâmico dos botões de visibilidade
            UpdateToggleButtonsPositions();
            this.Resize += (s, e) => UpdateToggleButtonsPositions();
            this.Move += (s, e) => UpdateToggleButtonsPositions();
            // também reagir quando os TextBox mudarem de tamanho/posição
            if (txtSenha != null) { txtSenha.SizeChanged += (s, e) => UpdateToggleButtonsPositions(); txtSenha.LocationChanged += (s,e)=> UpdateToggleButtonsPositions(); }
            if (txtConfirmSenha != null) { txtConfirmSenha.SizeChanged += (s, e) => UpdateToggleButtonsPositions(); txtConfirmSenha.LocationChanged += (s,e)=> UpdateToggleButtonsPositions(); }
            if (txtSenhaAPP != null) { txtSenhaAPP.SizeChanged += (s, e) => UpdateToggleButtonsPositions(); txtSenhaAPP.LocationChanged += (s,e)=> UpdateToggleButtonsPositions(); }
            if (txtConfirmSenhaAPP != null) { txtConfirmSenhaAPP.SizeChanged += (s, e) => UpdateToggleButtonsPositions(); txtConfirmSenhaAPP.LocationChanged += (s,e)=> UpdateToggleButtonsPositions(); }
        }

        // Validação de email simples (verifica formato e domínio pós-@)
        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            const string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        // Validação de telefone brasileiro: aceita 10 ou 11 dígitos (DDD + número)
        private static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            var digits = Regex.Replace(phone, "[^0-9]", "");
            if (digits.Length != 10 && digits.Length != 11) return false;
            // checar DDD válido (não começar com 0)
            if (digits.Length >= 2)
            {
                var ddd = digits.Substring(0, 2);
                int dddNum;
                if (!int.TryParse(ddd, out dddNum)) return false;
                if (dddNum < 11 || dddNum > 99) return false;
            }
            return true;
        }

        // Validação de CPF (algoritmo padrão)
        private static bool IsValidCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;
            var digits = Regex.Replace(cpf, "[^0-9]", "");
            if (digits.Length != 11) return false;
            // rejeitar sequências iguais
            var first = digits[0];
            if (digits.All(c => c == first)) return false;

            int[] numbers = digits.Select(c => c - '0').ToArray();

            // primeiro dígito verificador
            int sum = 0;
            for (int i = 0; i < 9; i++) sum += numbers[i] * (10 - i);
            int rem = sum % 11;
            int dv1 = rem < 2 ? 0 : 11 - rem;
            if (numbers[9] != dv1) return false;

            // segundo dígito verificador
            sum = 0;
            for (int i = 0; i < 10; i++) sum += numbers[i] * (11 - i);
            rem = sum % 11;
            int dv2 = rem < 2 ? 0 : 11 - rem;
            if (numbers[10] != dv2) return false;

            return true;
        }

        // Validação de senha: >=8, pelo menos 1 maiúscula, 1 minúscula e 1 caractere especial
        private static bool ValidatePassword(string password, out string error)
        {
            error = null;
            if (string.IsNullOrEmpty(password)) { error = "Senha vazia"; return false; }
            if (password.Length < 8) { error = "deve ter pelo menos 8 caracteres"; return false; }
            if (!Regex.IsMatch(password, "[A-Z]")) { error = "deve conter pelo menos uma letra maiúscula"; return false; }
            if (!Regex.IsMatch(password, "[a-z]")) { error = "deve conter pelo menos uma letra minúscula"; return false; }
            if (!Regex.IsMatch(password, "[^a-zA-Z0-9]")) { error = "deve conter pelo menos um caractere especial"; return false; }
            return true;
        }

        // Validação de nome pessoal: sem números, permite letras (incluindo acentuadas), espaços, hífen e apóstrofo
        private static bool IsValidPersonName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            var trimmed = name.Trim();
            if (trimmed.Length < 2) return false;
            const string pattern = @"^[A-Za-zÀ-ÖØ-öø-ÿ' .-]+$";
            return Regex.IsMatch(trimmed, pattern);
        }

        // Validação de nome de usuário do aplicativo: 3-20 chars, alfanuméricos e _ . - permitidos
        private static bool IsValidAppUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            var trimmed = username.Trim();
            if (trimmed.Length < 3 || trimmed.Length > 20) return false;
            const string pattern = "^[A-Za-z0-9_.-]+$";
            return Regex.IsMatch(trimmed, pattern);
        }

        // Cria um botão pequeno ao lado de um TextBox para alternar visibilidade da senha
        private void CreateToggleButton(ref Button btn, TextBox target, string text, EventHandler onClick)
        {
            if (target == null) return;
            btn = new Button();
            btn.Text = text;
            btn.Width = 40;
            btn.Height = target.Height + 2;
            btn.Font = new Font(btn.Font.FontFamily, 8);
            btn.Location = new Point(target.Right + 4, target.Top - 1);
            btn.Click += onClick;
            btn.TabStop = false;
            this.Controls.Add(btn);
        }

        private void ToggleSenhaSite_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility(txtSenha, btnToggleSenhaSite);
        }

        private void ToggleConfirmSenhaSite_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility(txtConfirmSenha, btnToggleConfirmSenhaSite);
        }

        private void ToggleSenhaApp_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility(txtSenhaAPP, btnToggleSenhaApp);
        }

        private void ToggleConfirmSenhaApp_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility(txtConfirmSenhaAPP, btnToggleConfirmSenhaApp);
        }

        private void TogglePasswordVisibility(TextBox tb, Button btn)
        {
            if (tb == null || btn == null) return;
            // alterna UseSystemPasswordChar
            tb.UseSystemPasswordChar = !tb.UseSystemPasswordChar;
            btn.Text = tb.UseSystemPasswordChar ? "Ver" : "Ocultar";
        }

        // Reposiciona os botões de alternância para ficar ao lado dos TextBox correspondentes
        private void UpdateToggleButtonsPositions()
        {
            try
            {
                PositionButtonNextToTextBox(btnToggleSenhaSite, txtSenha);
                PositionButtonNextToTextBox(btnToggleConfirmSenhaSite, txtConfirmSenha);
                PositionButtonNextToTextBox(btnToggleSenhaApp, txtSenhaAPP);
                PositionButtonNextToTextBox(btnToggleConfirmSenhaApp, txtConfirmSenhaAPP);
            }
            catch { }
        }

        private void PositionButtonNextToTextBox(Button btn, TextBox tb)
        {
            if (btn == null || tb == null) return;
            // calcular posição padrão à direita
            int x = tb.Right + 4;
            int y = tb.Top - 1;

            // se extrapolar a área cliente, posicionar dentro do TextBox à direita
            if (x + btn.Width > this.ClientSize.Width)
            {
                x = Math.Max(tb.Left, tb.Right - btn.Width - 4);
            }
            btn.Location = new Point(x, y);
            btn.Height = tb.Height + 2;
        }

        // Formata CPF enquanto o usuário digita: 000.000.000-00
        private void TxtCadCPF_TextChanged(object sender, EventArgs e)
        {
            if (_formattingCpf) return;
            try
            {
                _formattingCpf = true;
                var tb = sender as TextBox;
                if (tb == null) return;
                int sel = tb.SelectionStart;
                // conta quantos dígitos haviam antes do cursor para reposicionar
                int digitsBefore = 0;
                for (int i = 0; i < Math.Min(sel, tb.Text.Length); i++) if (char.IsDigit(tb.Text[i])) digitsBefore++;

                var digits = Regex.Replace(tb.Text, "[^0-9]", "");
                if (digits.Length > 11) digits = digits.Substring(0, 11);

                // monta formatação progressiva: 000.000.000-00
                var sb = new StringBuilder();
                for (int i = 0; i < digits.Length; i++)
                {
                    sb.Append(digits[i]);
                    if (i == 2 && digits.Length > 3) sb.Append('.');
                    else if (i == 5 && digits.Length > 6) sb.Append('.');
                    else if (i == 8 && digits.Length > 9) sb.Append('-');
                }
                string formatted = sb.ToString();
                tb.Text = formatted;

                // reposiciona cursor na posição correspondente aos dígitosBefore
                int newPos = 0;
                int counted = 0;
                for (int i = 0; i < formatted.Length; i++)
                {
                    if (char.IsDigit(formatted[i])) counted++;
                    if (counted >= digitsBefore)
                    {
                        newPos = i + 1;
                        break;
                    }
                }
                if (digitsBefore == 0) newPos = 0;
                if (counted < digitsBefore) newPos = formatted.Length;
                tb.SelectionStart = Math.Min(formatted.Length, newPos);
            }
            finally
            {
                _formattingCpf = false;
            }
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

            // Validação de nome e sobrenome (sem números ou caracteres inválidos)
            if (!IsValidPersonName(nome) || !IsValidPersonName(sobrenome))
            {
                MessageBox.Show("Nome ou sobrenome inválido. Use apenas letras, espaços, hífen ou apóstrofo, mínimo 2 caracteres.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string pwdErrorApp;
            if (!ValidatePassword(senha, out pwdErrorApp))
            {
                MessageBox.Show("Senha inválida: " + pwdErrorApp, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validações adicionais: email, telefone, CPF e requisitos de senha
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Email inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidPhone(telefone))
            {
                MessageBox.Show("Telefone inválido. Informe um número com DDD (10 ou 11 dígitos).", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidCpf(cpf))
            {
                MessageBox.Show("CPF inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string pwdError;
            if (!ValidatePassword(senha, out pwdError))
            {
                MessageBox.Show("Senha inválida: " + pwdError, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                    string sql = @"INSERT INTO tbl_clientes (Nome_Cliente, Sobr_Cliente,Email_Cliente, Tel_Cliente, CPF_Cliente,  SenhaHash)
                                   VALUES (@nome, @sobrenome,@email, @telefone,@cpf, @senha)";
                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@sobrenome", sobrenome);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@telefone", telefone);
                        cmd.Parameters.AddWithValue("@cpf", cpf);
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

            // Validação do nome de usuário do app (antes da validação da senha)
            if (!IsValidAppUsername(nomeUsuario))
            {
                MessageBox.Show("Nome de usuário inválido. Use 3-20 caracteres alfanuméricos, underscore, ponto ou hífen.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string hashedSenha = ComputeSha512Hex(senha);

                using (var conexao = new MySqlConnection(Variaveis.strConn))
                {
                    conexao.Open();

                    string checkSql = "SELECT COUNT(1) FROM tbl_usuarios WHERE NomeUsuario = @nome";
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

                    string sql = "INSERT INTO tbl_usuarios (NomeUsuario, SenhaHash) VALUES (@nome, @senha)";
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