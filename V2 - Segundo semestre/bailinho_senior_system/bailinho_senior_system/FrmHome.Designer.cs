using System;

namespace bailinho_senior_system
{
    partial class FrmHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHome));
            this.btnProdutos = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.imgProduto = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imgLogo = new System.Windows.Forms.PictureBox();
            this.btnEventos = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnParticipantes = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnVendas = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnCategorias = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btnFornecedores = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.btnRelatorios = new System.Windows.Forms.Panel();
            this.lbDescricaoRelatorio = new System.Windows.Forms.Label();
            this.lbTituloRelatorio = new System.Windows.Forms.Label();
            this.pbRelatorio = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnProdutos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgProduto)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            this.btnEventos.SuspendLayout();
            this.btnParticipantes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.btnVendas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.btnCategorias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.btnFornecedores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.btnRelatorios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRelatorio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnProdutos
            // 
            this.btnProdutos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnProdutos.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnProdutos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btnProdutos.Controls.Add(this.label2);
            this.btnProdutos.Controls.Add(this.label1);
            this.btnProdutos.Controls.Add(this.imgProduto);
            this.btnProdutos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProdutos.Location = new System.Drawing.Point(142, 466);
            this.btnProdutos.Name = "btnProdutos";
            this.btnProdutos.Size = new System.Drawing.Size(200, 110);
            this.btnProdutos.TabIndex = 7;
            this.btnProdutos.Click += new System.EventHandler(this.btnProdutos_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Gerenciamento de produtos";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(50, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Produto";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // imgProduto
            // 
            this.imgProduto.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.imgProduto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgProduto.BackgroundImage")));
            this.imgProduto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgProduto.Location = new System.Drawing.Point(74, 13);
            this.imgProduto.Name = "imgProduto";
            this.imgProduto.Size = new System.Drawing.Size(35, 35);
            this.imgProduto.TabIndex = 0;
            this.imgProduto.TabStop = false;
            this.imgProduto.Click += new System.EventHandler(this.imgProduto_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.imgLogo);
            this.panel2.Location = new System.Drawing.Point(323, 14);
            this.panel2.Margin = new System.Windows.Forms.Padding(30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(333, 280);
            this.panel2.TabIndex = 8;
            // 
            // imgLogo
            // 
            this.imgLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgLogo.BackgroundImage")));
            this.imgLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgLogo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imgLogo.Location = new System.Drawing.Point(12, 14);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(300, 250);
            this.imgLogo.TabIndex = 6;
            this.imgLogo.TabStop = false;
            // 
            // btnEventos
            // 
            this.btnEventos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEventos.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnEventos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btnEventos.Controls.Add(this.label3);
            this.btnEventos.Controls.Add(this.label4);
            this.btnEventos.Controls.Add(this.pictureBox1);
            this.btnEventos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEventos.Location = new System.Drawing.Point(384, 466);
            this.btnEventos.Name = "btnEventos";
            this.btnEventos.Size = new System.Drawing.Size(200, 110);
            this.btnEventos.TabIndex = 8;
            this.btnEventos.Click += new System.EventHandler(this.btnEventos_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Gerenciamento de eventos";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MediumPurple;
            this.label4.Location = new System.Drawing.Point(57, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 25);
            this.label4.TabIndex = 1;
            this.label4.Text = "Evento";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // btnParticipantes
            // 
            this.btnParticipantes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnParticipantes.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnParticipantes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btnParticipantes.Controls.Add(this.label5);
            this.btnParticipantes.Controls.Add(this.label6);
            this.btnParticipantes.Controls.Add(this.pictureBox2);
            this.btnParticipantes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnParticipantes.Location = new System.Drawing.Point(621, 466);
            this.btnParticipantes.Name = "btnParticipantes";
            this.btnParticipantes.Size = new System.Drawing.Size(200, 110);
            this.btnParticipantes.TabIndex = 9;
            this.btnParticipantes.Click += new System.EventHandler(this.btnParticipantes_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(34, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Gerenciamento de clientes";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.SeaGreen;
            this.label6.Location = new System.Drawing.Point(59, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 25);
            this.label6.TabIndex = 1;
            this.label6.Text = "Cliente";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(78, 13);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(35, 35);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // btnVendas
            // 
            this.btnVendas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnVendas.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnVendas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btnVendas.Controls.Add(this.label7);
            this.btnVendas.Controls.Add(this.label8);
            this.btnVendas.Controls.Add(this.pictureBox3);
            this.btnVendas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVendas.Location = new System.Drawing.Point(621, 598);
            this.btnVendas.Name = "btnVendas";
            this.btnVendas.Size = new System.Drawing.Size(200, 110);
            this.btnVendas.TabIndex = 10;
            this.btnVendas.Click += new System.EventHandler(this.btnVendas_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(35, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Gerenciamento de vendas";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Tomato;
            this.label8.Location = new System.Drawing.Point(64, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 25);
            this.label8.TabIndex = 1;
            this.label8.Text = "Venda";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(78, 13);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(35, 35);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // btnCategorias
            // 
            this.btnCategorias.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCategorias.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCategorias.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btnCategorias.Controls.Add(this.label9);
            this.btnCategorias.Controls.Add(this.label10);
            this.btnCategorias.Controls.Add(this.pictureBox4);
            this.btnCategorias.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCategorias.Location = new System.Drawing.Point(142, 598);
            this.btnCategorias.Name = "btnCategorias";
            this.btnCategorias.Size = new System.Drawing.Size(200, 110);
            this.btnCategorias.TabIndex = 9;
            this.btnCategorias.Click += new System.EventHandler(this.btnCategorias_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(25, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(146, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Gerenciamento de categorias";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Crimson;
            this.label10.Location = new System.Drawing.Point(45, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 25);
            this.label10.TabIndex = 1;
            this.label10.Text = "Categoria";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pictureBox4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.BackgroundImage")));
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(78, 13);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(35, 35);
            this.pictureBox4.TabIndex = 0;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // btnFornecedores
            // 
            this.btnFornecedores.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFornecedores.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnFornecedores.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btnFornecedores.Controls.Add(this.label11);
            this.btnFornecedores.Controls.Add(this.label12);
            this.btnFornecedores.Controls.Add(this.pictureBox5);
            this.btnFornecedores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFornecedores.Location = new System.Drawing.Point(384, 598);
            this.btnFornecedores.Name = "btnFornecedores";
            this.btnFornecedores.Size = new System.Drawing.Size(200, 110);
            this.btnFornecedores.TabIndex = 10;
            this.btnFornecedores.Click += new System.EventHandler(this.btnFornecedores_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(22, 76);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(153, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Gerenciamento de forncedores\r\n";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label12.Location = new System.Drawing.Point(43, 51);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(122, 25);
            this.label12.TabIndex = 1;
            this.label12.Text = "Fornecedor";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pictureBox5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox5.BackgroundImage")));
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox5.Location = new System.Drawing.Point(78, 13);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(35, 35);
            this.pictureBox5.TabIndex = 0;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.pictureBox5_Click);
            // 
            // btnRelatorios
            // 
            this.btnRelatorios.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRelatorios.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnRelatorios.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btnRelatorios.Controls.Add(this.lbDescricaoRelatorio);
            this.btnRelatorios.Controls.Add(this.lbTituloRelatorio);
            this.btnRelatorios.Controls.Add(this.pbRelatorio);
            this.btnRelatorios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRelatorios.Location = new System.Drawing.Point(384, 327);
            this.btnRelatorios.Name = "btnRelatorios";
            this.btnRelatorios.Size = new System.Drawing.Size(200, 110);
            this.btnRelatorios.TabIndex = 9;
            this.btnRelatorios.Click += new System.EventHandler(this.btnRelatorios_Click);
            // 
            // lbDescricaoRelatorio
            // 
            this.lbDescricaoRelatorio.AutoSize = true;
            this.lbDescricaoRelatorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescricaoRelatorio.Location = new System.Drawing.Point(25, 76);
            this.lbDescricaoRelatorio.Name = "lbDescricaoRelatorio";
            this.lbDescricaoRelatorio.Size = new System.Drawing.Size(139, 13);
            this.lbDescricaoRelatorio.TabIndex = 2;
            this.lbDescricaoRelatorio.Text = "Gerenciamento de relatórios";
            this.lbDescricaoRelatorio.Click += new System.EventHandler(this.lbDescricaoRelatorio_Click);
            // 
            // lbTituloRelatorio
            // 
            this.lbTituloRelatorio.AutoSize = true;
            this.lbTituloRelatorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTituloRelatorio.ForeColor = System.Drawing.Color.Black;
            this.lbTituloRelatorio.Location = new System.Drawing.Point(43, 51);
            this.lbTituloRelatorio.Name = "lbTituloRelatorio";
            this.lbTituloRelatorio.Size = new System.Drawing.Size(97, 25);
            this.lbTituloRelatorio.TabIndex = 1;
            this.lbTituloRelatorio.Text = "Relatório";
            this.lbTituloRelatorio.Click += new System.EventHandler(this.lbTituloRelatorio_Click);
            // 
            // pbRelatorio
            // 
            this.pbRelatorio.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pbRelatorio.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbRelatorio.BackgroundImage")));
            this.pbRelatorio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbRelatorio.Location = new System.Drawing.Point(78, 13);
            this.pbRelatorio.Name = "pbRelatorio";
            this.pbRelatorio.Size = new System.Drawing.Size(35, 35);
            this.pbRelatorio.TabIndex = 0;
            this.pbRelatorio.TabStop = false;
            this.pbRelatorio.Click += new System.EventHandler(this.pbRelatorio_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(78, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 35);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(210)))), ((int)(((byte)(221)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1004, 736);
            this.Controls.Add(this.btnRelatorios);
            this.Controls.Add(this.btnFornecedores);
            this.Controls.Add(this.btnCategorias);
            this.Controls.Add(this.btnVendas);
            this.Controls.Add(this.btnParticipantes);
            this.Controls.Add(this.btnEventos);
            this.Controls.Add(this.btnProdutos);
            this.Controls.Add(this.panel2);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "FrmHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bailinho Senior System";
            this.btnProdutos.ResumeLayout(false);
            this.btnProdutos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgProduto)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            this.btnEventos.ResumeLayout(false);
            this.btnEventos.PerformLayout();
            this.btnParticipantes.ResumeLayout(false);
            this.btnParticipantes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.btnVendas.ResumeLayout(false);
            this.btnVendas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.btnCategorias.ResumeLayout(false);
            this.btnCategorias.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.btnFornecedores.ResumeLayout(false);
            this.btnFornecedores.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.btnRelatorios.ResumeLayout(false);
            this.btnRelatorios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRelatorio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox imgLogo;
        private System.Windows.Forms.Panel btnProdutos;
        private System.Windows.Forms.PictureBox imgProduto;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel btnEventos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel btnParticipantes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel btnVendas;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel btnCategorias;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Panel btnFornecedores;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Panel btnRelatorios;
        private System.Windows.Forms.Label lbDescricaoRelatorio;
        private System.Windows.Forms.Label lbTituloRelatorio;
        private System.Windows.Forms.PictureBox pbRelatorio;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

