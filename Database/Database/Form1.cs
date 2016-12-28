using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Database
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
                String baglanti = "datasource=localhost;port=3306;database=asd;username=root;password=";
                String sorgu = "SELECT * FROM mesajlar";
                try
                {
                    MySqlConnection connection = new MySqlConnection(baglanti); //mysql bağlantısı
                    MySqlCommand cmd = new MySqlCommand(sorgu, connection); //sorguyu çalıştırmak için
                    connection.Open();  //bağlantıyı aç
                    MySqlDataReader okuyucu = cmd.ExecuteReader();    //verileri okuması için data reader
                   // MessageBox.Show("Veriler çekildi");

                    if (okuyucu.FieldCount > 0)     //eğer gelen kayıt sayısı 0 dan fazlaysa
                    {
                        for (int i = 0; i < okuyucu.FieldCount; i++)
                        {
                            if (i == 0)
                            {
                                listView1.Columns.Add(okuyucu.GetName(0), 0, HorizontalAlignment.Left);
                            }
                            else
                            {
                                listView1.Columns.Add(okuyucu.GetName(i).ToString().Replace("_", " "), 80, HorizontalAlignment.Left);
                            }
                        }
                        ListViewItem liste = new ListViewItem();
                        while (okuyucu.Read())    //veri okundukça, kayıt sonuna gelinceye kadar
                        {
                            liste = listView1.Items.Add(okuyucu[okuyucu.GetName(0)].ToString().Replace('_', ' '));
                            for (int h = 1; h < okuyucu.FieldCount; h++)
                            {

                                liste.SubItems.Add(okuyucu[okuyucu.GetName(h)].ToString());
                            }
                        }
                        for (int i = 1; i < listView1.Columns.Count; i++)
                            listView1.Columns[i].Width = -2;
                        //okuyucu kapanır
                        okuyucu.Close();
                        //bağlantı/ connection kapanır
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } 
            
        }


    }
}
