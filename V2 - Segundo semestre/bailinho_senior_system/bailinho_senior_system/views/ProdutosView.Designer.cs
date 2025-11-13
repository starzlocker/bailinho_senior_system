namespace bailinho_senior_system.views
{
    partial class ProdutosView
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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProdutosView));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCadastro = new System.Windows.Forms.TabPage();
            this.fornecedoresBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.categoriaLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.qtdEstoqueBox = new System.Windows.Forms.NumericUpDown();
            this.categoriaBox = new System.Windows.Forms.ComboBox();
            this.precoBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.descricaoBox = new System.Windows.Forms.TextBox();
            this.nomeBox = new System.Windows.Forms.TextBox();
            this.idBox = new System.Windows.Forms.TextBox();
            this.tabPageLista = new System.Windows.Forms.TabPage();
            this.listTable = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.tsBotoes = new System.Windows.Forms.ToolStrip();
            this.firstBtn = new System.Windows.Forms.ToolStripButton();
            this.previousBtn = new System.Windows.Forms.ToolStripButton();
            this.nextBtn = new System.Windows.Forms.ToolStripButton();
            this.lastBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.searchBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.newBtn = new System.Windows.Forms.ToolStripButton();
            this.cancelBtn = new System.Windows.Forms.ToolStripButton();
            this.editBtn = new System.Windows.Forms.ToolStripButton();
            this.saveBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitBtn = new System.Windows.Forms.ToolStripButton();
            this.produtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl.SuspendLayout();
            this.tabPageCadastro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qtdEstoqueBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.precoBox)).BeginInit();
            this.tabPageLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listTable)).BeginInit();
            this.tsBotoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.produtoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageCadastro);
            this.tabControl.Controls.Add(this.tabPageLista);
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.ItemSize = new System.Drawing.Size(110, 30);
            this.tabControl.Location = new System.Drawing.Point(11, 78);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(749, 306);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 14;
            this.tabControl.Tag = "";
            // 
            // tabPageCadastro
            // 
            this.tabPageCadastro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(210)))), ((int)(((byte)(221)))));
            this.tabPageCadastro.Controls.Add(this.fornecedoresBox);
            this.tabPageCadastro.Controls.Add(this.label6);
            this.tabPageCadastro.Controls.Add(this.label4);
            this.tabPageCadastro.Controls.Add(this.categoriaLabel);
            this.tabPageCadastro.Controls.Add(this.label5);
            this.tabPageCadastro.Controls.Add(this.qtdEstoqueBox);
            this.tabPageCadastro.Controls.Add(this.categoriaBox);
            this.tabPageCadastro.Controls.Add(this.precoBox);
            this.tabPageCadastro.Controls.Add(this.label3);
            this.tabPageCadastro.Controls.Add(this.label2);
            this.tabPageCadastro.Controls.Add(this.label1);
            this.tabPageCadastro.Controls.Add(this.descricaoBox);
            this.tabPageCadastro.Controls.Add(this.nomeBox);
            this.tabPageCadastro.Controls.Add(this.idBox);
            this.tabPageCadastro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageCadastro.Location = new System.Drawing.Point(4, 34);
            this.tabPageCadastro.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageCadastro.Name = "tabPageCadastro";
            this.tabPageCadastro.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageCadastro.Size = new System.Drawing.Size(741, 268);
            this.tabPageCadastro.TabIndex = 0;
            this.tabPageCadastro.Text = "Cadastro";
            // 
            // fornecedoresBox
            // 
            this.fornecedoresBox.FormattingEnabled = true;
            this.fornecedoresBox.Location = new System.Drawing.Point(407, 112);
            this.fornecedoresBox.Margin = new System.Windows.Forms.Padding(4);
            this.fornecedoresBox.Name = "fornecedoresBox";
            this.fornecedoresBox.Size = new System.Drawing.Size(305, 28);
            this.fornecedoresBox.TabIndex = 30;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(291, 118);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 18);
            this.label6.TabIndex = 32;
            this.label6.Text = "Fornecedor";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 70);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 18);
            this.label4.TabIndex = 26;
            this.label4.Text = "Quantidade";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // categoriaLabel
            // 
            this.categoriaLabel.AutoSize = true;
            this.categoriaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoriaLabel.Location = new System.Drawing.Point(291, 70);
            this.categoriaLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.categoriaLabel.Name = "categoriaLabel";
            this.categoriaLabel.Size = new System.Drawing.Size(81, 18);
            this.categoriaLabel.TabIndex = 31;
            this.categoriaLabel.Text = "Categoria";
            this.categoriaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 113);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 18);
            this.label5.TabIndex = 27;
            this.label5.Text = "Preço";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // qtdEstoqueBox
            // 
            this.qtdEstoqueBox.Location = new System.Drawing.Point(144, 66);
            this.qtdEstoqueBox.Margin = new System.Windows.Forms.Padding(4);
            this.qtdEstoqueBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.qtdEstoqueBox.Name = "qtdEstoqueBox";
            this.qtdEstoqueBox.Size = new System.Drawing.Size(133, 26);
            this.qtdEstoqueBox.TabIndex = 25;
            this.qtdEstoqueBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // categoriaBox
            // 
            this.categoriaBox.FormattingEnabled = true;
            this.categoriaBox.Location = new System.Drawing.Point(407, 66);
            this.categoriaBox.Margin = new System.Windows.Forms.Padding(4);
            this.categoriaBox.Name = "categoriaBox";
            this.categoriaBox.Size = new System.Drawing.Size(305, 28);
            this.categoriaBox.TabIndex = 29;
            // 
            // precoBox
            // 
            this.precoBox.DecimalPlaces = 2;
            this.precoBox.Location = new System.Drawing.Point(144, 110);
            this.precoBox.Margin = new System.Windows.Forms.Padding(4);
            this.precoBox.Maximum = new decimal(new int[] {
            32000,
            0,
            0,
            0});
            this.precoBox.Name = "precoBox";
            this.precoBox.Size = new System.Drawing.Size(133, 26);
            this.precoBox.TabIndex = 28;
            this.precoBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 151);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 18);
            this.label3.TabIndex = 24;
            this.label3.Text = "Descrição";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(291, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 18);
            this.label2.TabIndex = 23;
            this.label2.Text = "Nome";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 18);
            this.label1.TabIndex = 22;
            this.label1.Text = "ID";
            // 
            // descricaoBox
            // 
            this.descricaoBox.Location = new System.Drawing.Point(21, 173);
            this.descricaoBox.Margin = new System.Windows.Forms.Padding(4);
            this.descricaoBox.Multiline = true;
            this.descricaoBox.Name = "descricaoBox";
            this.descricaoBox.Size = new System.Drawing.Size(691, 70);
            this.descricaoBox.TabIndex = 21;
            // 
            // nomeBox
            // 
            this.nomeBox.Location = new System.Drawing.Point(407, 22);
            this.nomeBox.Margin = new System.Windows.Forms.Padding(4);
            this.nomeBox.Name = "nomeBox";
            this.nomeBox.Size = new System.Drawing.Size(305, 26);
            this.nomeBox.TabIndex = 20;
            // 
            // idBox
            // 
            this.idBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.idBox.Location = new System.Drawing.Point(144, 22);
            this.idBox.Margin = new System.Windows.Forms.Padding(4);
            this.idBox.Name = "idBox";
            this.idBox.ReadOnly = true;
            this.idBox.Size = new System.Drawing.Size(133, 26);
            this.idBox.TabIndex = 19;
            // 
            // tabPageLista
            // 
            this.tabPageLista.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageLista.Controls.Add(this.listTable);
            this.tabPageLista.Controls.Add(this.button1);
            this.tabPageLista.Controls.Add(this.label8);
            this.tabPageLista.Controls.Add(this.searchBox);
            this.tabPageLista.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPageLista.Location = new System.Drawing.Point(4, 34);
            this.tabPageLista.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageLista.Name = "tabPageLista";
            this.tabPageLista.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageLista.Size = new System.Drawing.Size(741, 268);
            this.tabPageLista.TabIndex = 1;
            this.tabPageLista.Text = "Lista";
            // 
            // listTable
            // 
            this.listTable.AllowUserToAddRows = false;
            this.listTable.AllowUserToDeleteRows = false;
            this.listTable.AllowUserToResizeColumns = false;
            this.listTable.AllowUserToResizeRows = false;
            this.listTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.listTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listTable.Location = new System.Drawing.Point(10, 59);
            this.listTable.MultiSelect = false;
            this.listTable.Name = "listTable";
            this.listTable.ReadOnly = true;
            this.listTable.RowHeadersVisible = false;
            this.listTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.listTable.Size = new System.Drawing.Size(838, 336);
            this.listTable.TabIndex = 18;
            this.listTable.SelectionChanged += new System.EventHandler(this.listTable_SelectionChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(601, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 26);
            this.button1.TabIndex = 17;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(153, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "Buscar (nome ou Id)";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(10, 27);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(585, 26);
            this.searchBox.TabIndex = 16;
            // 
            // tsBotoes
            // 
            this.tsBotoes.BackColor = System.Drawing.Color.White;
            this.tsBotoes.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsBotoes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.firstBtn,
            this.previousBtn,
            this.nextBtn,
            this.lastBtn,
            this.toolStripSeparator1,
            this.searchBtn,
            this.toolStripSeparator2,
            this.newBtn,
            this.deleteBtn,
            this.editBtn,
            this.saveBtn,
            this.toolStripSeparator3,
            this.cancelBtn,
            this.toolStripSeparator4,
            this.exitBtn});
            this.tsBotoes.Location = new System.Drawing.Point(0, 0);
            this.tsBotoes.Name = "tsBotoes";
            this.tsBotoes.Size = new System.Drawing.Size(773, 65);
            this.tsBotoes.TabIndex = 15;
            this.tsBotoes.Text = "toolStrip1";
            // 
            // firstBtn
            // 
            this.firstBtn.AutoSize = false;
            this.firstBtn.Image = ((System.Drawing.Image)(resources.GetObject("firstBtn.Image")));
            this.firstBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.firstBtn.Name = "firstBtn";
            this.firstBtn.Size = new System.Drawing.Size(67, 62);
            this.firstBtn.Text = "I&nício";
            this.firstBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.firstBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.firstBtn.ToolTipText = "Vai ao primeiro registro";
            this.firstBtn.Click += new System.EventHandler(this.firstBtn_Click);
            // 
            // previousBtn
            // 
            this.previousBtn.AutoSize = false;
            this.previousBtn.Image = ((System.Drawing.Image)(resources.GetObject("previousBtn.Image")));
            this.previousBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.previousBtn.Name = "previousBtn";
            this.previousBtn.Size = new System.Drawing.Size(67, 62);
            this.previousBtn.Text = "Anterior";
            this.previousBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.previousBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.previousBtn.ToolTipText = "Vai ao registro anterior";
            this.previousBtn.Click += new System.EventHandler(this.previousBtn_Click);
            // 
            // nextBtn
            // 
            this.nextBtn.AutoSize = false;
            this.nextBtn.Image = global::bailinho_senior_system.Properties.Resources.chevron_right;
            this.nextBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(67, 62);
            this.nextBtn.Text = "Próximo";
            this.nextBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.nextBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.nextBtn.ToolTipText = "Vai ao registro seguinte";
            this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);
            // 
            // lastBtn
            // 
            this.lastBtn.AutoSize = false;
            this.lastBtn.Image = global::bailinho_senior_system.Properties.Resources.chevron_double_right_30;
            this.lastBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lastBtn.Name = "lastBtn";
            this.lastBtn.Size = new System.Drawing.Size(67, 62);
            this.lastBtn.Text = "Ú&ltimo";
            this.lastBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lastBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.lastBtn.ToolTipText = "Vai ao último registro";
            this.lastBtn.Click += new System.EventHandler(this.lastBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 65);
            // 
            // searchBtn
            // 
            this.searchBtn.AutoSize = false;
            this.searchBtn.Image = global::bailinho_senior_system.Properties.Resources.search_30;
            this.searchBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(67, 62);
            this.searchBtn.Text = "&Buscar";
            this.searchBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.searchBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.searchBtn.ToolTipText = "Busca registro pelo código";
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 65);
            // 
            // newBtn
            // 
            this.newBtn.AutoSize = false;
            this.newBtn.Image = global::bailinho_senior_system.Properties.Resources.plus_30;
            this.newBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newBtn.Name = "newBtn";
            this.newBtn.Size = new System.Drawing.Size(67, 62);
            this.newBtn.Text = "&Novo";
            this.newBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.newBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.newBtn.ToolTipText = "Iniciar a inclusão de novo registro";
            this.newBtn.Click += new System.EventHandler(this.newBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.AutoSize = false;
            this.cancelBtn.Image = global::bailinho_senior_system.Properties.Resources.close_30;
            this.cancelBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(67, 62);
            this.cancelBtn.Text = "Cancelar";
            this.cancelBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cancelBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cancelBtn.ToolTipText = "Cancela a operação atual";
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // editBtn
            // 
            this.editBtn.AutoSize = false;
            this.editBtn.Image = global::bailinho_senior_system.Properties.Resources.edit_30;
            this.editBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(67, 62);
            this.editBtn.Text = "Editar";
            this.editBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.editBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.AutoSize = false;
            this.saveBtn.Enabled = false;
            this.saveBtn.Image = global::bailinho_senior_system.Properties.Resources.save_30;
            this.saveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(67, 62);
            this.saveBtn.Text = "&Salvar";
            this.saveBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.saveBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.saveBtn.ToolTipText = "Salva o registro incluído ou modicado";
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 65);
            // 
            // deleteBtn
            // 
            this.deleteBtn.AutoSize = false;
            this.deleteBtn.Image = global::bailinho_senior_system.Properties.Resources.trash_30;
            this.deleteBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(67, 62);
            this.deleteBtn.Text = "&Excluir";
            this.deleteBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.deleteBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.deleteBtn.ToolTipText = "Exclui o registro apresentado na tela";
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 65);
            // 
            // exitBtn
            // 
            this.exitBtn.AutoSize = false;
            this.exitBtn.Image = global::bailinho_senior_system.Properties.Resources.exit_30;
            this.exitBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(67, 62);
            this.exitBtn.Text = "Sai&r";
            this.exitBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.exitBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.exitBtn.ToolTipText = "Termina a execução do programa e salva no disco todos os dados";
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // produtoBindingSource
            // 
            this.produtoBindingSource.DataSource = typeof(bailinho_senior_system.models.Produto);
            // 
            // ProdutosView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(210)))), ((int)(((byte)(221)))));
            this.ClientSize = new System.Drawing.Size(773, 391);
            this.Controls.Add(this.tsBotoes);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(789, 430);
            this.Name = "ProdutosView";
            this.Text = "Produtos";
            this.Load += new System.EventHandler(this.ProdutosView_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageCadastro.ResumeLayout(false);
            this.tabPageCadastro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qtdEstoqueBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.precoBox)).EndInit();
            this.tabPageLista.ResumeLayout(false);
            this.tabPageLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listTable)).EndInit();
            this.tsBotoes.ResumeLayout(false);
            this.tsBotoes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.produtoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageCadastro;
        private System.Windows.Forms.TabPage tabPageLista;
        private System.Windows.Forms.BindingSource produtoBindingSource;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.DataGridView listTable;
        private System.Windows.Forms.ToolStrip tsBotoes;
        private System.Windows.Forms.ToolStripButton firstBtn;
        private System.Windows.Forms.ToolStripButton previousBtn;
        private System.Windows.Forms.ToolStripButton nextBtn;
        private System.Windows.Forms.ToolStripButton lastBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton searchBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton newBtn;
        private System.Windows.Forms.ToolStripButton cancelBtn;
        private System.Windows.Forms.ToolStripButton editBtn;
        private System.Windows.Forms.ToolStripButton saveBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton deleteBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton exitBtn;
        private System.Windows.Forms.ComboBox fornecedoresBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label categoriaLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown qtdEstoqueBox;
        private System.Windows.Forms.ComboBox categoriaBox;
        private System.Windows.Forms.NumericUpDown precoBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox descricaoBox;
        private System.Windows.Forms.TextBox nomeBox;
        private System.Windows.Forms.TextBox idBox;
    }
}