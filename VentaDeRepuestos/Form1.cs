using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VentaDeRepuestos.Helpers;

namespace VentaDeRepuestos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Image image = Image.FromFile("repuestoslogo.jpg");
            pbLogo.Image = image;
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            var pass = Encriptar.encriptarPassword(txtPassword.Text.Trim());
            var result = await Consultas.loginAsync(txtCorreo.Text.Trim(),pass);


            if (result.Read())
            {
                this.Hide();
                new Menu().Show();


            }
            else
            {
                MessageBox.Show("error correo o contraseña incorrectos");
            }
        }
    }
}
