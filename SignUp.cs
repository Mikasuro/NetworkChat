using NetworkChat.Model;
using NetworkChat.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkChat
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            if (tbLogin.Text != string.Empty && tbPassword.Text != string.Empty)
            {
                client.Start("4", JsonConvert.SerializeObject(new User(tbLogin.Text, tbPassword.Text)), "2");
            }
            else MessageBox.Show("Некоректные данные");
            MessageBox.Show("Регистрация прошла успешно");
            Close();
        }
    }
}
