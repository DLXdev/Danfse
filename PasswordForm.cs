using System;
using System.Drawing;
using System.Windows.Forms;

namespace GeradorDanfse
{
    public partial class PasswordForm : Form
    {
        private TextBox txtPassword;
        private Button btnOK;
        private Button btnCancel;
        private Label lblInstruction;

        public string Password { get; private set; }

        public PasswordForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Senha do Certificado";
            this.Size = new Size(300, 150);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Label
            lblInstruction = new Label();
            lblInstruction.Text = "Digite a senha do certificado digital:";
            lblInstruction.Location = new Point(15, 20);
            lblInstruction.Size = new Size(250, 20);

            // Password textbox
            txtPassword = new TextBox();
            txtPassword.Location = new Point(15, 45);
            txtPassword.Size = new Size(250, 20);
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // OK button
            btnOK = new Button();
            btnOK.Text = "OK";
            btnOK.Location = new Point(110, 80);
            btnOK.Size = new Size(75, 25);
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Click += BtnOK_Click;

            // Cancel button
            btnCancel = new Button();
            btnCancel.Text = "Cancelar";
            btnCancel.Location = new Point(190, 80);
            btnCancel.Size = new Size(75, 25);
            btnCancel.DialogResult = DialogResult.Cancel;

            // Add controls to form
            this.Controls.AddRange(new Control[] { lblInstruction, txtPassword, btnOK, btnCancel });
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Password = txtPassword.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}