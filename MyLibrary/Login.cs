using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MyLibrary
{
    public partial class Login : Form
    {
        public event EventHandler UserLoggedIn;
        public Login()
        {
            InitializeComponent();
        }

        private bool IsUserValid()
        {
            XDocument korisnici = XDocument.Load("korisnici.xml");

            var users = from user in korisnici.Descendants("korisnik")
                        select new
                        {
                            username = user.Descendants("korisnickoIme").First().Value,
                            password = user.Descendants("lozinka").First().Value
                        };

            foreach (var user in users)
            {
                if (string.Compare(user.username, textBoxUsername.Text, true) == 0 && user.password == textBoxPassword.Text)
                    return true;
            }
            return false;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (IsUserValid())
            {
                if (UserLoggedIn != null)
                    UserLoggedIn(this, EventArgs.Empty);

                Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password!", "User Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
