namespace bailinho_senior_system
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.vendasBtn = new System.Windows.Forms.Button();
            this.produtosBtn = new System.Windows.Forms.Button();
            this.eventosBtn = new System.Windows.Forms.Button();
            this.categoriasBtn = new System.Windows.Forms.Button();
            this.clientesBtn = new System.Windows.Forms.Button();
            this.fornecedoresBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // vendasBtn
            // 
            this.vendasBtn.BackColor = System.Drawing.Color.Transparent;
            this.vendasBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.vendasBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.vendasBtn.Location = new System.Drawing.Point(42, 52);
            this.vendasBtn.Name = "vendasBtn";
            this.vendasBtn.Padding = new System.Windows.Forms.Padding(3);
            this.vendasBtn.Size = new System.Drawing.Size(200, 30);
            this.vendasBtn.TabIndex = 0;
            this.vendasBtn.Text = "Vendas";
            this.vendasBtn.UseVisualStyleBackColor = false;
            this.vendasBtn.Click += new System.EventHandler(this.vendasBtn_Click);
            // 
            // produtosBtn
            // 
            this.produtosBtn.BackColor = System.Drawing.Color.DimGray;
            this.produtosBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.produtosBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.produtosBtn.Location = new System.Drawing.Point(42, 96);
            this.produtosBtn.Name = "produtosBtn";
            this.produtosBtn.Padding = new System.Windows.Forms.Padding(3);
            this.produtosBtn.Size = new System.Drawing.Size(200, 30);
            this.produtosBtn.TabIndex = 1;
            this.produtosBtn.Text = "Produtos";
            this.produtosBtn.UseVisualStyleBackColor = false;
            this.produtosBtn.Click += new System.EventHandler(this.produtosBtn_Click);
            // 
            // eventosBtn
            // 
            this.eventosBtn.BackColor = System.Drawing.Color.DimGray;
            this.eventosBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.eventosBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.eventosBtn.Location = new System.Drawing.Point(42, 140);
            this.eventosBtn.Name = "eventosBtn";
            this.eventosBtn.Padding = new System.Windows.Forms.Padding(3);
            this.eventosBtn.Size = new System.Drawing.Size(200, 30);
            this.eventosBtn.TabIndex = 2;
            this.eventosBtn.Text = "Eventos";
            this.eventosBtn.UseVisualStyleBackColor = false;
            this.eventosBtn.Click += new System.EventHandler(this.eventosBtn_Click);
            // 
            // categoriasBtn
            // 
            this.categoriasBtn.BackColor = System.Drawing.Color.DimGray;
            this.categoriasBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.categoriasBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.categoriasBtn.Location = new System.Drawing.Point(42, 272);
            this.categoriasBtn.Name = "categoriasBtn";
            this.categoriasBtn.Padding = new System.Windows.Forms.Padding(3);
            this.categoriasBtn.Size = new System.Drawing.Size(200, 30);
            this.categoriasBtn.TabIndex = 5;
            this.categoriasBtn.Text = "Categorias";
            this.categoriasBtn.UseVisualStyleBackColor = false;
            this.categoriasBtn.Click += new System.EventHandler(this.categoriasBtn_Click);
            // 
            // clientesBtn
            // 
            this.clientesBtn.BackColor = System.Drawing.Color.DimGray;
            this.clientesBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.clientesBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.clientesBtn.Location = new System.Drawing.Point(42, 228);
            this.clientesBtn.Name = "clientesBtn";
            this.clientesBtn.Padding = new System.Windows.Forms.Padding(3);
            this.clientesBtn.Size = new System.Drawing.Size(200, 30);
            this.clientesBtn.TabIndex = 4;
            this.clientesBtn.Text = "Clientes";
            this.clientesBtn.UseVisualStyleBackColor = false;
            this.clientesBtn.Click += new System.EventHandler(this.clientesBtn_Click);
            // 
            // fornecedoresBtn
            // 
            this.fornecedoresBtn.BackColor = System.Drawing.Color.DimGray;
            this.fornecedoresBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.fornecedoresBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.fornecedoresBtn.Location = new System.Drawing.Point(42, 184);
            this.fornecedoresBtn.Name = "fornecedoresBtn";
            this.fornecedoresBtn.Padding = new System.Windows.Forms.Padding(3);
            this.fornecedoresBtn.Size = new System.Drawing.Size(200, 30);
            this.fornecedoresBtn.TabIndex = 3;
            this.fornecedoresBtn.Text = "Fornecedores";
            this.fornecedoresBtn.UseVisualStyleBackColor = false;
            this.fornecedoresBtn.Click += new System.EventHandler(this.fornecedoresBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 591);
            this.Controls.Add(this.categoriasBtn);
            this.Controls.Add(this.clientesBtn);
            this.Controls.Add(this.fornecedoresBtn);
            this.Controls.Add(this.eventosBtn);
            this.Controls.Add(this.produtosBtn);
            this.Controls.Add(this.vendasBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button vendasBtn;
        private System.Windows.Forms.Button produtosBtn;
        private System.Windows.Forms.Button eventosBtn;
        private System.Windows.Forms.Button categoriasBtn;
        private System.Windows.Forms.Button clientesBtn;
        private System.Windows.Forms.Button fornecedoresBtn;
    }
}

