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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientesView));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCadastro = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioMulher = new System.Windows.Forms.RadioButton();
            this.radioHomem = new System.Windows.Forms.RadioButton();
            this.dataDeNascimento = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.telefoneBox = new System.Windows.Forms.TextBox();
            this.cpfBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.idBox = new System.Windows.Forms.TextBox();
            this.nomeBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageLista = new System.Windows.Forms.TabPage();
            this.listTable = new System.Windows.Forms.DataGridView();
            this.makeSearch = new System.Windows.Forms.Button();
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
            this.editBtn = new System.Windows.Forms.ToolStripButton();
            this.saveBtn = new System.Windows.Forms.ToolStripButton();
            this.deleteBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitBtn = new System.Windows.Forms.ToolStripButton();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cpf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Genero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Telefone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl.SuspendLayout();
            this.tabPageCadastro.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listTable)).BeginInit();
            this.tsBotoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageCadastro);
            this.tabControl.Controls.Add(this.tabPageLista);
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.ItemSize = new System.Drawing.Size(110, 30);
            this.tabControl.Location = new System.Drawing.Point(54, 88);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1079, 472);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 26;
            this.tabControl.Tag = "";
            this.tabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl_Selecting);
            // 
            // tabPageCadastro
            // 
            this.tabPageCadastro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(210)))), ((int)(((byte)(221)))));
            this.tabPageCadastro.Controls.Add(this.groupBox1);
            this.tabPageCadastro.Controls.Add(this.dataDeNascimento);
            this.tabPageCadastro.Controls.Add(this.label4);
            this.tabPageCadastro.Controls.Add(this.telefoneBox);
            this.tabPageCadastro.Controls.Add(this.cpfBox);
            this.tabPageCadastro.Controls.Add(this.label7);
            this.tabPageCadastro.Controls.Add(this.label9);
            this.tabPageCadastro.Controls.Add(this.idBox);
            this.tabPageCadastro.Controls.Add(this.nomeBox);
            this.tabPageCadastro.Controls.Add(this.label2);
            this.tabPageCadastro.Controls.Add(this.label1);
            this.tabPageCadastro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageCadastro.Location = new System.Drawing.Point(4, 34);
            this.tabPageCadastro.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageCadastro.Name = "tabPageCadastro";
            this.tabPageCadastro.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageCadastro.Size = new System.Drawing.Size(1071, 434);
            this.tabPageCadastro.TabIndex = 0;
            this.tabPageCadastro.Text = "Cadastro";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.radioMulher);
            this.groupBox1.Controls.Add(this.radioHomem);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(288, 244);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(528, 68);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gênero";
            // 
            // radioMulher
            // 
            this.radioMulher.AutoSize = true;
            this.radioMulher.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioMulher.Location = new System.Drawing.Point(267, 25);
            this.radioMulher.Name = "radioMulher";
            this.radioMulher.Size = new System.Drawing.Size(75, 24);
            this.radioMulher.TabIndex = 34;
            this.radioMulher.TabStop = true;
            this.radioMulher.Text = "Mulher";
            this.radioMulher.UseVisualStyleBackColor = true;
            // 
            // radioHomem
            // 
            this.radioHomem.AutoSize = true;
            this.radioHomem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioHomem.Location = new System.Drawing.Point(178, 25);
            this.radioHomem.Name = "radioHomem";
            this.radioHomem.Size = new System.Drawing.Size(83, 24);
            this.radioHomem.TabIndex = 33;
            this.radioHomem.TabStop = true;
            this.radioHomem.Text = "Homem";
            this.radioHomem.UseVisualStyleBackColor = true;
            // 
            // dataDeNascimento
            // 
            this.dataDeNascimento.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dataDeNascimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataDeNascimento.Location = new System.Drawing.Point(449, 327);
            this.dataDeNascimento.Name = "dataDeNascimento";
            this.dataDeNascimento.Size = new System.Drawing.Size(367, 24);
            this.dataDeNascimento.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(284, 331);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 18);
            this.label4.TabIndex = 39;
            this.label4.Text = "Data de nascimento";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // telefoneBox
            // 
            this.telefoneBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.telefoneBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.telefoneBox.Location = new System.Drawing.Point(365, 160);
            this.telefoneBox.Margin = new System.Windows.Forms.Padding(4);
            this.telefoneBox.Name = "telefoneBox";
            this.telefoneBox.Size = new System.Drawing.Size(451, 24);
            this.telefoneBox.TabIndex = 38;
            // 
            // cpfBox
            // 
            this.cpfBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cpfBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpfBox.Location = new System.Drawing.Point(365, 198);
            this.cpfBox.Margin = new System.Windows.Forms.Padding(4);
            this.cpfBox.Name = "cpfBox";
            this.cpfBox.Size = new System.Drawing.Size(451, 24);
            this.cpfBox.TabIndex = 37;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(284, 202);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 18);
            this.label7.TabIndex = 36;
            this.label7.Text = "CPF";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(284, 164);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 18);
            this.label9.TabIndex = 35;
            this.label9.Text = "Telefone";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // idBox
            // 
            this.idBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.idBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.idBox.Enabled = false;
            this.idBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idBox.Location = new System.Drawing.Point(365, 78);
            this.idBox.Margin = new System.Windows.Forms.Padding(4);
            this.idBox.Name = "idBox";
            this.idBox.ReadOnly = true;
            this.idBox.Size = new System.Drawing.Size(451, 24);
            this.idBox.TabIndex = 28;
            this.idBox.Visible = false;
            // 
            // nomeBox
            // 
            this.nomeBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nomeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nomeBox.Location = new System.Drawing.Point(365, 118);
            this.nomeBox.Margin = new System.Windows.Forms.Padding(4);
            this.nomeBox.Name = "nomeBox";
            this.nomeBox.Size = new System.Drawing.Size(451, 24);
            this.nomeBox.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(284, 122);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 18);
            this.label2.TabIndex = 31;
            this.label2.Text = "Nome";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(284, 81);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 18);
            this.label1.TabIndex = 30;
            this.label1.Text = "ID";
            this.label1.Visible = false;
            // 
            // tabPageLista
            // 
            this.tabPageLista.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageLista.Controls.Add(this.listTable);
            this.tabPageLista.Controls.Add(this.makeSearch);
            this.tabPageLista.Controls.Add(this.searchBox);
            this.tabPageLista.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPageLista.Location = new System.Drawing.Point(4, 34);
            this.tabPageLista.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageLista.Name = "tabPageLista";
            this.tabPageLista.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageLista.Size = new System.Drawing.Size(1071, 434);
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
            this.listTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Cpf,
            this.Nome,
            this.Data,
            this.Genero,
            this.Telefone});
            this.listTable.Location = new System.Drawing.Point(7, 40);
            this.listTable.MultiSelect = false;
            this.listTable.Name = "listTable";
            this.listTable.ReadOnly = true;
            this.listTable.RowHeadersVisible = false;
            this.listTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.listTable.Size = new System.Drawing.Size(1057, 368);
            this.listTable.TabIndex = 18;
            this.listTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.listTable_CellClick);
            // 
            // makeSearch
            // 
            this.makeSearch.Location = new System.Drawing.Point(598, 8);
            this.makeSearch.Name = "makeSearch";
            this.makeSearch.Size = new System.Drawing.Size(67, 26);
            this.makeSearch.TabIndex = 17;
            this.makeSearch.Text = "Buscar";
            this.makeSearch.UseVisualStyleBackColor = true;
            this.makeSearch.Click += new System.EventHandler(this.makeSearch_Click);
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(7, 8);
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
            this.editBtn,
            this.saveBtn,
            this.deleteBtn,
            this.toolStripSeparator3,
            this.cancelBtn,
            this.toolStripSeparator4,
            this.exitBtn});
            this.tsBotoes.Location = new System.Drawing.Point(0, 0);
            this.tsBotoes.Name = "tsBotoes";
            this.tsBotoes.Size = new System.Drawing.Size(1166, 65);
            this.tsBotoes.TabIndex = 28;
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 65);
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
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // Cpf
            // 
            this.Cpf.HeaderText = "CPF";
            this.Cpf.Name = "Cpf";
            this.Cpf.ReadOnly = true;
            // 
            // Nome
            // 
            this.Nome.HeaderText = "Nome";
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            // 
            // Data
            // 
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            // 
            // Genero
            // 
            this.Genero.HeaderText = "Gênero";
            this.Genero.Name = "Genero";
            this.Genero.ReadOnly = true;
            // 
            // Telefone
            // 
            this.Telefone.HeaderText = "Telefone";
            this.Telefone.Name = "Telefone";
            this.Telefone.ReadOnly = true;
            // 
            // ClientesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(210)))), ((int)(((byte)(221)))));
            this.ClientSize = new System.Drawing.Size(1166, 586);
            this.Controls.Add(this.tsBotoes);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "ClientesView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clientes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientesView_FormClosing_1);
            this.Load += new System.EventHandler(this.ClientesView_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageCadastro.ResumeLayout(false);
            this.tabPageCadastro.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageLista.ResumeLayout(false);
            this.tabPageLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listTable)).EndInit();
            this.tsBotoes.ResumeLayout(false);
            this.tsBotoes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageCadastro;
        private System.Windows.Forms.TabPage tabPageLista;
        internal System.Windows.Forms.DataGridView listTable;
        private System.Windows.Forms.Button makeSearch;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.ToolStrip tsBotoes;
        private System.Windows.Forms.ToolStripButton firstBtn;
        private System.Windows.Forms.ToolStripButton previousBtn;
        private System.Windows.Forms.ToolStripButton nextBtn;
        private System.Windows.Forms.ToolStripButton lastBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton searchBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton newBtn;
        private System.Windows.Forms.ToolStripButton editBtn;
        private System.Windows.Forms.ToolStripButton saveBtn;
        private System.Windows.Forms.ToolStripButton deleteBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton cancelBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton exitBtn;
        private System.Windows.Forms.DateTimePicker dataDeNascimento;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox telefoneBox;
        private System.Windows.Forms.TextBox cpfBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton radioMulher;
        private System.Windows.Forms.RadioButton radioHomem;
        private System.Windows.Forms.TextBox idBox;
        private System.Windows.Forms.TextBox nomeBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cpf;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Genero;
        private System.Windows.Forms.DataGridViewTextBoxColumn Telefone;
    }
}