namespace bailinho_senior_system.views
{
    partial class ClientesView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nome = new System.Windows.Forms.TextBox();
            this.id = new System.Windows.Forms.TextBox();
            this.descricao = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fornecedores = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.categoriaLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.qtd_estoque = new System.Windows.Forms.NumericUpDown();
            this.id_categoria = new System.Windows.Forms.ComboBox();
            this.preco = new System.Windows.Forms.NumericUpDown();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.exitBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.editBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.newBtn = new System.Windows.Forms.Button();
            this.searchBtn = new System.Windows.Forms.Button();
            this.lastBtn = new System.Windows.Forms.Button();
            this.nextBtn = new System.Windows.Forms.Button();
            this.previousBtn = new System.Windows.Forms.Button();
            this.firstBtn = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qtd_estoque)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.preco)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(110, 30);
            this.tabControl1.Location = new System.Drawing.Point(16, 97);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1089, 519);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 14;
            this.tabControl1.Tag = "";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1081, 481);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cadastro";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nome);
            this.groupBox1.Controls.Add(this.id);
            this.groupBox1.Controls.Add(this.descricao);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1067, 137);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informações Gerais";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 83);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Descrição";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(434, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 18);
            this.label2.TabIndex = 9;
            this.label2.Text = "Nome";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "ID";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // nome
            // 
            this.nome.Location = new System.Drawing.Point(437, 53);
            this.nome.Margin = new System.Windows.Forms.Padding(4);
            this.nome.Name = "nome";
            this.nome.Size = new System.Drawing.Size(387, 26);
            this.nome.TabIndex = 2;
            // 
            // id
            // 
            this.id.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.id.Location = new System.Drawing.Point(10, 53);
            this.id.Margin = new System.Windows.Forms.Padding(4);
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Size = new System.Drawing.Size(386, 26);
            this.id.TabIndex = 1;
            // 
            // descricao
            // 
            this.descricao.Location = new System.Drawing.Point(10, 105);
            this.descricao.Margin = new System.Windows.Forms.Padding(4);
            this.descricao.Name = "descricao";
            this.descricao.Size = new System.Drawing.Size(386, 26);
            this.descricao.TabIndex = 3;
            this.descricao.TextChanged += new System.EventHandler(this.descricao_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fornecedores);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.categoriaLabel);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.qtd_estoque);
            this.groupBox2.Controls.Add(this.id_categoria);
            this.groupBox2.Controls.Add(this.preco);
            this.groupBox2.Location = new System.Drawing.Point(7, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1067, 249);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalhes";
            // 
            // fornecedores
            // 
            this.fornecedores.FormattingEnabled = true;
            this.fornecedores.Location = new System.Drawing.Point(437, 46);
            this.fornecedores.Margin = new System.Windows.Forms.Padding(4);
            this.fornecedores.Name = "fornecedores";
            this.fornecedores.Size = new System.Drawing.Size(387, 28);
            this.fornecedores.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(434, 25);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 18);
            this.label6.TabIndex = 18;
            this.label6.Text = "Fornecedores";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 26);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 18);
            this.label4.TabIndex = 12;
            this.label4.Text = "Quantidade em Estoque";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // categoriaLabel
            // 
            this.categoriaLabel.AutoSize = true;
            this.categoriaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoriaLabel.Location = new System.Drawing.Point(7, 136);
            this.categoriaLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.categoriaLabel.Name = "categoriaLabel";
            this.categoriaLabel.Size = new System.Drawing.Size(72, 18);
            this.categoriaLabel.TabIndex = 17;
            this.categoriaLabel.Text = "Categoria";
            this.categoriaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 80);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 18);
            this.label5.TabIndex = 13;
            this.label5.Text = "Preço";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // qtd_estoque
            // 
            this.qtd_estoque.Location = new System.Drawing.Point(10, 48);
            this.qtd_estoque.Margin = new System.Windows.Forms.Padding(4);
            this.qtd_estoque.Name = "qtd_estoque";
            this.qtd_estoque.Size = new System.Drawing.Size(386, 26);
            this.qtd_estoque.TabIndex = 11;
            this.qtd_estoque.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // id_categoria
            // 
            this.id_categoria.FormattingEnabled = true;
            this.id_categoria.Location = new System.Drawing.Point(10, 158);
            this.id_categoria.Margin = new System.Windows.Forms.Padding(4);
            this.id_categoria.Name = "id_categoria";
            this.id_categoria.Size = new System.Drawing.Size(386, 28);
            this.id_categoria.TabIndex = 15;
            // 
            // preco
            // 
            this.preco.DecimalPlaces = 2;
            this.preco.Location = new System.Drawing.Point(10, 102);
            this.preco.Margin = new System.Windows.Forms.Padding(4);
            this.preco.Name = "preco";
            this.preco.Size = new System.Drawing.Size(386, 26);
            this.preco.TabIndex = 14;
            this.preco.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.preco.ValueChanged += new System.EventHandler(this.preco_ValueChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightGray;
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1081, 481);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Lista";
            // 
            // exitBtn
            // 
            this.exitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitBtn.Image = global::bailinho_senior_system.Properties.Resources.exit_30;
            this.exitBtn.Location = new System.Drawing.Point(784, 13);
            this.exitBtn.Margin = new System.Windows.Forms.Padding(4);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(67, 62);
            this.exitBtn.TabIndex = 25;
            this.exitBtn.Text = "Sair";
            this.exitBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.exitBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.exitBtn.UseVisualStyleBackColor = true;
            // 
            // deleteBtn
            // 
            this.deleteBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteBtn.Image = global::bailinho_senior_system.Properties.Resources.trash_30;
            this.deleteBtn.Location = new System.Drawing.Point(697, 13);
            this.deleteBtn.Margin = new System.Windows.Forms.Padding(4);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(67, 62);
            this.deleteBtn.TabIndex = 24;
            this.deleteBtn.Text = "Excluir";
            this.deleteBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.deleteBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.deleteBtn.UseVisualStyleBackColor = true;
            // 
            // saveBtn
            // 
            this.saveBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveBtn.Image = global::bailinho_senior_system.Properties.Resources.save_30;
            this.saveBtn.Location = new System.Drawing.Point(622, 13);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(4);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(67, 62);
            this.saveBtn.TabIndex = 23;
            this.saveBtn.Text = "Salvar";
            this.saveBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.saveBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.saveBtn.UseVisualStyleBackColor = true;
            // 
            // editBtn
            // 
            this.editBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editBtn.Image = global::bailinho_senior_system.Properties.Resources.edit_30;
            this.editBtn.Location = new System.Drawing.Point(548, 13);
            this.editBtn.Margin = new System.Windows.Forms.Padding(4);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(67, 62);
            this.editBtn.TabIndex = 22;
            this.editBtn.Text = "Editar";
            this.editBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.editBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.editBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBtn.Image = global::bailinho_senior_system.Properties.Resources.close_30;
            this.cancelBtn.Location = new System.Drawing.Point(473, 13);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(4);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(67, 62);
            this.cancelBtn.TabIndex = 21;
            this.cancelBtn.Text = "Cancelar";
            this.cancelBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cancelBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // newBtn
            // 
            this.newBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newBtn.Image = global::bailinho_senior_system.Properties.Resources.plus_30;
            this.newBtn.Location = new System.Drawing.Point(398, 13);
            this.newBtn.Margin = new System.Windows.Forms.Padding(4);
            this.newBtn.Name = "newBtn";
            this.newBtn.Size = new System.Drawing.Size(67, 62);
            this.newBtn.TabIndex = 20;
            this.newBtn.Text = "Novo";
            this.newBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.newBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.newBtn.UseVisualStyleBackColor = true;
            // 
            // searchBtn
            // 
            this.searchBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBtn.Image = global::bailinho_senior_system.Properties.Resources.search_30;
            this.searchBtn.Location = new System.Drawing.Point(324, 13);
            this.searchBtn.Margin = new System.Windows.Forms.Padding(4);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(67, 62);
            this.searchBtn.TabIndex = 19;
            this.searchBtn.Text = "Buscar";
            this.searchBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.searchBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.searchBtn.UseVisualStyleBackColor = true;
            // 
            // lastBtn
            // 
            this.lastBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastBtn.Image = global::bailinho_senior_system.Properties.Resources.chevron_double_right_30;
            this.lastBtn.Location = new System.Drawing.Point(240, 13);
            this.lastBtn.Margin = new System.Windows.Forms.Padding(4);
            this.lastBtn.Name = "lastBtn";
            this.lastBtn.Size = new System.Drawing.Size(67, 62);
            this.lastBtn.TabIndex = 18;
            this.lastBtn.Text = "Último";
            this.lastBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lastBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.lastBtn.UseVisualStyleBackColor = true;
            // 
            // nextBtn
            // 
            this.nextBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextBtn.Image = global::bailinho_senior_system.Properties.Resources.chevron_right_30;
            this.nextBtn.Location = new System.Drawing.Point(165, 13);
            this.nextBtn.Margin = new System.Windows.Forms.Padding(4);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(67, 62);
            this.nextBtn.TabIndex = 17;
            this.nextBtn.Text = "Próximo";
            this.nextBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.nextBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.nextBtn.UseVisualStyleBackColor = true;
            // 
            // previousBtn
            // 
            this.previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previousBtn.Image = global::bailinho_senior_system.Properties.Resources.chevron_left_30;
            this.previousBtn.Location = new System.Drawing.Point(91, 13);
            this.previousBtn.Margin = new System.Windows.Forms.Padding(4);
            this.previousBtn.Name = "previousBtn";
            this.previousBtn.Size = new System.Drawing.Size(67, 62);
            this.previousBtn.TabIndex = 16;
            this.previousBtn.Text = "Anterior";
            this.previousBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.previousBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.previousBtn.UseVisualStyleBackColor = true;
            // 
            // firstBtn
            // 
            this.firstBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.firstBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstBtn.Image = global::bailinho_senior_system.Properties.Resources.chevron_double_left_30;
            this.firstBtn.Location = new System.Drawing.Point(16, 13);
            this.firstBtn.Margin = new System.Windows.Forms.Padding(4);
            this.firstBtn.Name = "firstBtn";
            this.firstBtn.Size = new System.Drawing.Size(67, 62);
            this.firstBtn.TabIndex = 15;
            this.firstBtn.Text = "Inicio";
            this.firstBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.firstBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.firstBtn.UseVisualStyleBackColor = true;
            // 
            // ClientesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 624);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.newBtn);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.lastBtn);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.previousBtn);
            this.Controls.Add(this.firstBtn);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ClientesView";
            this.Text = "Clientes";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qtd_estoque)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.preco)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox descricao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown preco;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown qtd_estoque;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label categoriaLabel;
        private System.Windows.Forms.ComboBox fornecedores;
        private System.Windows.Forms.ComboBox id_categoria;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nome;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button newBtn;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.Button lastBtn;
        private System.Windows.Forms.Button nextBtn;
        private System.Windows.Forms.Button previousBtn;
        private System.Windows.Forms.Button firstBtn;
    }
}