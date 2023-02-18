using System.Diagnostics;
using System.Windows.Forms.Design;

namespace DesligaComputador
{
    public partial class FormDesligar : Form
    {
        private string senha = "teste";
        int tentativas = 3;
        string mensagemUser = String.Empty;
        int hora = DateTime.Now.Hour;
           
      
        public FormDesligar()
        {
            if (Environment.UserName.ToLower() == "teste" && (hora >= 20 || hora < 3) )
            {
              
                InitializeComponent();
                //Mata o servi�o do explorer pra ficar apenas a janela que quero.
                Process.Start("cmd.exe", "/C taskkill /im explorer.exe /f");

                mensagemUser = $"Boa noite voc� tem {tentativas} tentativa(s) antes do PC se auto-desligar";
                lblMsgUser.Text = mensagemUser;
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {

            if (txtSenha.Text.ToLower() == senha)
            {
                //Caso acerte a senha volta o servi�o do explorer.
                Process.Start("cmd.exe", "/C start explorer.exe");

                Environment.Exit(0);
            }
            else if (tentativas >= 2)
            {
                --tentativas;
                lblMsgUser.Text = $"Boa noite voc� tem {tentativas} tentativa(s) antes do PC se auto-desligar";
            }
            else
            {
                // /P desligar repentinamente
                // /F for�ar desligar
                // /Y aceitar todas as op�oes casa esteja algo aberto.

                string desligar = "/C shutdown /p /f /y";
                Process.Start("cmd.exe", desligar);

                MessageBox.Show("Desligando");
            }
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {   
            // Caso aperte alt+f4 n�o vai fechar a aplica��o.

            e.Cancel = true;
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = false;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = true;
        }
    }
}