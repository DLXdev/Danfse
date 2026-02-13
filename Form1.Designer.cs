namespace GeradorDanfse
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnSelecionarOrigem = new System.Windows.Forms.Button();
            this.btnSelecionarDestino = new System.Windows.Forms.Button();
            this.btnSelecionarCertificado = new System.Windows.Forms.Button();
            this.btn05GerarDanfse = new System.Windows.Forms.Button();
            this.btnTestarChave = new System.Windows.Forms.Button();
            this.txtPastaOrigem = new System.Windows.Forms.TextBox();
            this.txtPastaDestino = new System.Windows.Forms.TextBox();
            this.txtCaminhoCertificado = new System.Windows.Forms.TextBox();
            this.txtChaveTeste = new System.Windows.Forms.TextBox();
            this.lblOrigem = new System.Windows.Forms.Label();
            this.lblDestino = new System.Windows.Forms.Label();
            this.lblCertificado = new System.Windows.Forms.Label();
            this.lblChaveTeste = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelecionarOrigem
            // 
            this.btnSelecionarOrigem.Location = new System.Drawing.Point(9, 23);
            this.btnSelecionarOrigem.Name = "btnSelecionarOrigem";
            this.btnSelecionarOrigem.Size = new System.Drawing.Size(120, 30);
            this.btnSelecionarOrigem.TabIndex = 0;
            this.btnSelecionarOrigem.Text = "Selecionar Origem";
            this.btnSelecionarOrigem.UseVisualStyleBackColor = true;
            this.btnSelecionarOrigem.Click += new System.EventHandler(this.btnSelecionarOrigem_Click);
            // 
            // btnSelecionarDestino
            // 
            this.btnSelecionarDestino.Location = new System.Drawing.Point(15, 25);
            this.btnSelecionarDestino.Name = "btnSelecionarDestino";
            this.btnSelecionarDestino.Size = new System.Drawing.Size(120, 30);
            this.btnSelecionarDestino.TabIndex = 3;
            this.btnSelecionarDestino.Text = "Selecionar Destino";
            this.btnSelecionarDestino.UseVisualStyleBackColor = true;
            this.btnSelecionarDestino.Click += new System.EventHandler(this.btnSelecionarDestino_Click);
            // 
            // btnSelecionarCertificado
            // 
            this.btnSelecionarCertificado.Location = new System.Drawing.Point(15, 25);
            this.btnSelecionarCertificado.Name = "btnSelecionarCertificado";
            this.btnSelecionarCertificado.Size = new System.Drawing.Size(120, 30);
            this.btnSelecionarCertificado.TabIndex = 6;
            this.btnSelecionarCertificado.Text = "Selecionar Certificado";
            this.btnSelecionarCertificado.UseVisualStyleBackColor = true;
            this.btnSelecionarCertificado.Click += new System.EventHandler(this.btnSelecionarCertificado_Click);
            // 
            // btn05GerarDanfse
            // 
            this.btn05GerarDanfse.BackColor = System.Drawing.Color.SteelBlue;
            this.btn05GerarDanfse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn05GerarDanfse.ForeColor = System.Drawing.Color.White;
            this.btn05GerarDanfse.Location = new System.Drawing.Point(206, 317);
            this.btn05GerarDanfse.Name = "btn05GerarDanfse";
            this.btn05GerarDanfse.Size = new System.Drawing.Size(150, 40);
            this.btn05GerarDanfse.TabIndex = 6;
            this.btn05GerarDanfse.Text = "Gerar DANFSE";
            this.btn05GerarDanfse.UseVisualStyleBackColor = false;
            this.btn05GerarDanfse.Click += new System.EventHandler(this.btn05GerarDanfse_Click);
            // 
            // btnTestarChave
            // 
            this.btnTestarChave.Location = new System.Drawing.Point(0, 0);
            this.btnTestarChave.Name = "btnTestarChave";
            this.btnTestarChave.Size = new System.Drawing.Size(75, 23);
            this.btnTestarChave.TabIndex = 0;
            // 
            // txtPastaOrigem
            // 
            this.txtPastaOrigem.Location = new System.Drawing.Point(150, 29);
            this.txtPastaOrigem.Name = "txtPastaOrigem";
            this.txtPastaOrigem.ReadOnly = true;
            this.txtPastaOrigem.Size = new System.Drawing.Size(400, 20);
            this.txtPastaOrigem.TabIndex = 1;
            // 
            // txtPastaDestino
            // 
            this.txtPastaDestino.Location = new System.Drawing.Point(150, 29);
            this.txtPastaDestino.Name = "txtPastaDestino";
            this.txtPastaDestino.ReadOnly = true;
            this.txtPastaDestino.Size = new System.Drawing.Size(400, 20);
            this.txtPastaDestino.TabIndex = 4;
            // 
            // txtCaminhoCertificado
            // 
            this.txtCaminhoCertificado.Location = new System.Drawing.Point(150, 29);
            this.txtCaminhoCertificado.Name = "txtCaminhoCertificado";
            this.txtCaminhoCertificado.ReadOnly = true;
            this.txtCaminhoCertificado.Size = new System.Drawing.Size(400, 20);
            this.txtCaminhoCertificado.TabIndex = 7;
            // 
            // txtChaveTeste
            // 
            this.txtChaveTeste.Location = new System.Drawing.Point(0, 0);
            this.txtChaveTeste.Name = "txtChaveTeste";
            this.txtChaveTeste.Size = new System.Drawing.Size(100, 20);
            this.txtChaveTeste.TabIndex = 0;
            // 
            // lblOrigem
            // 
            this.lblOrigem.AutoSize = true;
            this.lblOrigem.Location = new System.Drawing.Point(12, 9);
            this.lblOrigem.Name = "lblOrigem";
            this.lblOrigem.Size = new System.Drawing.Size(165, 13);
            this.lblOrigem.TabIndex = 2;
            this.lblOrigem.Text = "Pasta com arquivos XML (NFSE):";
            // 
            // lblDestino
            // 
            this.lblDestino.AutoSize = true;
            this.lblDestino.Location = new System.Drawing.Point(12, 9);
            this.lblDestino.Name = "lblDestino";
            this.lblDestino.Size = new System.Drawing.Size(165, 13);
            this.lblDestino.TabIndex = 5;
            this.lblDestino.Text = "Pasta destino para arquivos PDF:";
            // 
            // lblCertificado
            // 
            this.lblCertificado.AutoSize = true;
            this.lblCertificado.Location = new System.Drawing.Point(12, 9);
            this.lblCertificado.Name = "lblCertificado";
            this.lblCertificado.Size = new System.Drawing.Size(118, 13);
            this.lblCertificado.TabIndex = 8;
            this.lblCertificado.Text = "Certificado Digital (.pfx):";
            // 
            // lblChaveTeste
            // 
            this.lblChaveTeste.Location = new System.Drawing.Point(0, 0);
            this.lblChaveTeste.Name = "lblChaveTeste";
            this.lblChaveTeste.Size = new System.Drawing.Size(100, 23);
            this.lblChaveTeste.TabIndex = 0;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 278);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(570, 23);
            this.progressBar.TabIndex = 7;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(9, 262);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(74, 13);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status: Pronto";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelecionarOrigem);
            this.groupBox1.Controls.Add(this.txtPastaOrigem);
            this.groupBox1.Location = new System.Drawing.Point(12, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 66);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSelecionarDestino);
            this.groupBox2.Controls.Add(this.txtPastaDestino);
            this.groupBox2.Location = new System.Drawing.Point(12, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(570, 64);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSelecionarCertificado);
            this.groupBox3.Controls.Add(this.txtCaminhoCertificado);
            this.groupBox3.Location = new System.Drawing.Point(12, 176);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(570, 67);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 100);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 361);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn05GerarDanfse);
            this.Controls.Add(this.lblCertificado);
            this.Controls.Add(this.lblDestino);
            this.Controls.Add(this.lblOrigem);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerador de DANFSE - NFSE para PDF";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Button btnSelecionarOrigem;
        private System.Windows.Forms.Button btnSelecionarDestino;
        private System.Windows.Forms.Button btnSelecionarCertificado;
        private System.Windows.Forms.Button btn05GerarDanfse;
        private System.Windows.Forms.TextBox txtPastaOrigem;
        private System.Windows.Forms.TextBox txtPastaDestino;
        private System.Windows.Forms.TextBox txtCaminhoCertificado;
        private System.Windows.Forms.Label lblOrigem;
        private System.Windows.Forms.Label lblDestino;
        private System.Windows.Forms.Label lblCertificado;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblChaveTeste;
        private System.Windows.Forms.TextBox txtChaveTeste;
        private System.Windows.Forms.Button btnTestarChave;
    }
}