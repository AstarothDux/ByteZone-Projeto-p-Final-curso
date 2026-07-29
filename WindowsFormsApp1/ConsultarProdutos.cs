using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class ConsultarProdutos
    {
        MySqlConnection conn = new MySqlConnection(Variaveis.strConn);
        public void consultarProdutos(string consulta)
        {
            try
            {
                conn.Open();
                //Criar comando de consulta
                MySqlCommand comando = new MySqlCommand(consulta, conn);
                //Criar dataReader
                MySqlDataReader drDados = null;
                //Fazer a consulta
                drDados = comando.ExecuteReader();
                while (drDados.Read())
                {
                    //Obter resultados das colunas
                    string NomeProduto = drDados["Nome_Produto"].ToString();

                    //Preencher variáveis com dados
                    Variaveis.CaixaTxtNomeProd = NomeProduto;
                }
                drDados.Close();
            }
            catch (MySqlException s)
            {
                MessageBox.Show(s.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}
