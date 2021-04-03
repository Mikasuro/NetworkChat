using NetworkChat.Model;
using NetworkChat.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkChat
{
    public partial class SignIn : Form
    {
        public bool IsAuthenticated { get; private set; }
        public SignIn()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            IsAuthenticated = client.Start("1", JsonConvert.SerializeObject(new User(tbLogin.Text, tbPassword.Text)));
            if (IsAuthenticated == true)
            {
                Close();
            }
            else MessageBox.Show("Неверный логин или пароль");
            
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
        }
    }
}
