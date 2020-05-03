using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RSClient
{

    partial class RSClient : Form
    {
        int i;

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        string commandString = "SELECT * FROM `tbl_mac_adress`;";
        string commandStringip1 = "SELECT * FROM `tbl_mac_adress` WHERE `ROUTER_IP` LIKE ";
        

        string tcount = ("");
        public RSClient()

        {
            InitializeComponent();
            select();
        }
        void select()
        {
            string connectionString = "Database=routerscan;Data Source=127.0.20.1;User Id=rsuser;Password=123456";
            MySqlConnection connection = new MySqlConnection(connectionString);
            statuslabel.Text = ("Соединено!");

            MySqlCommand command = new MySqlCommand();
            
            command.CommandText = commandString;
            command.Connection = connection;
            MySqlDataReader reader;
            try
            {
                command.Connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["ID"].ToString(), reader["ROUTER_IP"].ToString(), reader["MAC_ADRESS"].ToString(), reader["ROUTER_PORT"].ToString(), reader["CREATE_DATE"].ToString());
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                statuslabel.Text = ("Нет соединения с базой данных!");
                MessageBox.Show("Ошибка подключения к базе данных! " +
                    "Error: \r\n{0}", ex.ToString());

            }
            finally
            {
                command.Connection.Close();
                reader = null;
            }

            dataGridView1.MouseClick += new MouseEventHandler(dataGridView1_MouseClick);
        }

        void selectclr()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }

        void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Button == MouseButtons.Left)
                { 
                    MessageBox.Show("Left");
                }
            }
            else
            {
                MessageBox.Show("Right");
            }
        }

        void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        void RSClient_Load(object sender, EventArgs e)
        {
           /* i = 45;
            this.Text = i.ToString();
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Start();
            this.timer1_Tick*/
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           /* this.Text = (--i).ToString();
            if (i < 0)
                timer1.Stop();*/
        }

        void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string compare = textBox1.Text;
            System.Threading.Thread.Sleep(1500);
            if (compare != textBox1.Text)
            { }
            else
            { 
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Null");
            }
            else {
                    for (int i = 0; i < dataGridView1.RowCount; i++)

                    {
                        dataGridView1.Rows[i].Selected = false;
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            if (dataGridView1.Rows[i].Cells[j].Value != null)
                                if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                                {
                                    dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[j];//перейти к ячейке.
                                    MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                                    break;
                                }
                    }
                }
            }
        }

        void TextBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            MessageBox.Show("Click!");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            telnet settingsForm = new telnet();
            settingsForm.Show();
        }
    }
}