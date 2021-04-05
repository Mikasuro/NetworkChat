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
            string result = string.Empty;
            if (tbLogin.Text != string.Empty && tbPassword.Text != string.Empty)
            {
                Thread thread = new Thread(new ThreadStart(() => 
                {
                    result = client.Start("4", JsonConvert.SerializeObject(new User(tbLogin.Text, tbPassword.Text)), "1");
                    if (result != "false")
                    {
                        IsAuthenticated = true;
                        Close();
                    }
                    else MessageBox.Show("Неверный логин или пароль");
                }));
                thread.Start();
            }
            else MessageBox.Show("Некоректные данные");
            
            
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
        }
    }
}
