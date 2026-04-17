using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class ConsultarProdutos
    {
        SqlConnection conn = new SqlConnection(Variaveis.strConn);
        public void consultarProdutos(string consulta)
        {
            try
            {
                conn.Open();
                //Criar comando de consulta
                SqlCommand comando = new SqlCommand(consulta, conn);
                //Criar dataReader
                SqlDataReader drDados = null;
                //Fazer a consulta
                drDados = comando.ExecuteReader();
                while (drDados.Read())
                {
                    //Obter resultados das colunas
                    string NomeProduto = (string)drDados["Nome_Produto"];
                    //Preencher combobox com dados
                    Variaveis.CaixaTxtNomeProd = NomeProduto;
                }
            }
            catch (SqlException s)
            {
                MessageBox.Show(s.Source.ToString());
            }
        }
    }
}
