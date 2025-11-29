using bailinho_senior_system.models;
using bailinho_senior_system.repositories;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;

namespace bailinho_senior_system.views
{

    public partial class ClientesView : Form
    {
        private enum ViewState { Listing, Editing, Creating }

        // --- Variáveis de Estado da View ---
        private List<Cliente> clientes = new List<Cliente>();
        private int currentIndex = 0;
        private ViewState currentState;
        private Cliente editCliente = null;

        // --- Repositórios ---
        private ClienteRepository clienteRepository = new ClienteRepository();

        // --- Construtor e Eventos de Inicialização ---

        public ClientesView()
        {
            InitializeComponent();
            this.Load += ClientesView_Load;
            this.tabControl.Selecting += tabControl_Selecting;
            this.listTable.CellClick += listTable_CellClick;
        }

        private void ClientesView_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView(listTable);
            ReadClientes();

            if (clientes.Count > 0)
                PopulateFields(clientes[currentIndex]);

            SetState(ViewState.Listing);
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (currentState == ViewState.Editing || currentState == ViewState.Creating)
            {

                if (e.TabPage.Name != "tabPageCadastro")
                {
                    var result = MessageBox.Show(
                        "Se você sair, suas alterações serão perdidas. Deseja continuar?",
                        "Confirmar",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }

                    editCliente = null;
                    SetState(ViewState.Listing);
                }
            }
        }

        // --- Configuração e Leitura de Dados ---

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            // Configurações visuais e de comportamento
            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;

            // Configurações de Aparência
            dgv.EnableHeadersVisualStyles = false;
            dgv.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(173, 216, 230);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 30;
            dgv.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Limpa as colunas definidas no designer para redefini-las programaticamente
            dgv.Columns.Clear();

            // Adição e Mapeamento das Colunas (DataPropertyName deve corresponder à propriedade da classe Cliente)
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Id",
                DataPropertyName = "Id",
                Name = "Id",
                Visible = false,
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Nome",
                DataPropertyName = "Nome",
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Data",
                DataPropertyName = "DtNascimento",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Genero",
                DataPropertyName = "Genero",
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "CPF",
                DataPropertyName = "Cpf",
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Telefone",
                DataPropertyName = "Telefone",
                Resizable = DataGridViewTriState.False
            });
        }

        private void ReadClientes()
        {
            // Carrega todos os clientes do repositório e vincula a lista completa ao DataGridView
            this.clientes = clienteRepository.GetClientes();

            listTable.DataSource = null;
            listTable.DataSource = this.clientes;
        }

        private void PopulateFields(Cliente cliente)
        {
            // Preenche os campos do formulário de cadastro com os dados do cliente
            idBox.Text = cliente.Id.ToString();
            nomeBox.Text = cliente.Nome ?? "";
            telefoneBox.Text = cliente.Telefone ?? "";
            cpfBox.Text = cliente.Cpf;
            dataDeNascimento.Value = cliente.DtNascimento;

            if (cliente.Genero == 'M')
            {
                radioHomem.Checked = true;
            }
            else
            {
                radioMulher.Checked = true;
            }
        }

        private void CleanupFields()
        {
            // Limpa todos os campos de entrada no formulário de cadastro
            idBox.Text = "";
            nomeBox.Text = "";
            telefoneBox.Text = "";
            cpfBox.Text = "";
            dataDeNascimento.Value = DateTime.Today;
            radioHomem.Checked = false;
            radioMulher.Checked = false;
        }

        // --- Navegação e Estado ---

        private void SetState(ViewState newState)
        {
            // Gerencia a visibilidade e habilitação dos botões e campos de entrada
            currentState = newState;

            bool creatingOrEditing = currentState == ViewState.Creating || currentState == ViewState.Editing;
            bool listing = currentState == ViewState.Listing;
            int count = clientes.Count;

            // Navegação
            firstBtn.Enabled = listing && currentIndex > 0;
            previousBtn.Enabled = listing && currentIndex > 0;
            nextBtn.Enabled = listing && currentIndex < count - 1;
            lastBtn.Enabled = listing && currentIndex < count - 1;

            // Ações CRUD
            newBtn.Enabled = listing;
            editBtn.Enabled = listing && count > 0;
            deleteBtn.Enabled = listing && count > 0;
            searchBtn.Enabled = listing;

            // Salvamento/Cancelamento
            saveBtn.Enabled = creatingOrEditing;
            cancelBtn.Enabled = creatingOrEditing;

            // Habilitação/Desabilitação de Campos
            nomeBox.ReadOnly = listing;
            telefoneBox.ReadOnly = listing;
            cpfBox.ReadOnly = listing;
            dataDeNascimento.Enabled = !listing;
            radioHomem.Enabled = !listing;
            radioMulher.Enabled = !listing;

            if (creatingOrEditing)
            {
                SwitchToTabByName("tabPageCadastro");
            }

            if (currentState == ViewState.Creating || count == 0)
            {
                CleanupFields();
            }
            else if (listing && count > 0 && currentIndex >= 0)
            {
                PopulateFields(clientes[currentIndex]);
                UpdateDataGridViewSelection();
            }
        }

        private void SwitchToTabByName(string tabName)
        {
            // Muda para a aba especificada no TabControl
            if (string.IsNullOrEmpty(tabName)) return;
            var page = tabControl.TabPages.Cast<TabPage>().FirstOrDefault(t => t.Name == tabName);
            if (page != null) tabControl.SelectedTab = page;
        }

        private void UpdateDataGridViewSelection()
        {
            // Seleciona o cliente atual no DataGridView e rola para visualização
            if (listTable == null || clientes.Count == 0 || currentIndex < 0 || currentIndex >= clientes.Count)
            {
                return;
            }

            // A forma mais robusta em Data Bound é sincronizar o CurrencyManager
            CurrencyManager cm = (CurrencyManager)listTable.BindingContext[listTable.DataSource];
            if (cm.Count > 0)
            {
                Cliente clienteAtual = clientes[currentIndex];
                List<Cliente> listaExibida = listTable.DataSource as List<Cliente>;

                // Encontra o índice do cliente atual na lista exibida
                int indexNaListaExibida = listaExibida?.FindIndex(c => c.Id == clienteAtual.Id) ?? -1;

                if (indexNaListaExibida != -1)
                {
                    // Define a posição do CurrencyManager, que atualiza a seleção do DGV
                    cm.Position = indexNaListaExibida;

                    // Rola para a linha selecionada
                    listTable.FirstDisplayedScrollingRowIndex = indexNaListaExibida;
                }
                else
                {
                    listTable.ClearSelection();
                }
            }
        }

        // --- MANIPULADORES DE EVENTOS (BOTÕES DE NAVEGAÇÃO) ---

        private void firstBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex = 0;
            ReadClientes(); // Garante que a lista completa esteja vinculada (limpando filtros)
            SetState(ViewState.Listing);
        }

        private void previousBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex--;
            ReadClientes(); // Garante que a lista completa esteja vinculada (limpando filtros)
            SetState(ViewState.Listing);
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < clientes.Count - 1) currentIndex++;
            ReadClientes(); // Garante que a lista completa esteja vinculada (limpando filtros)
            SetState(ViewState.Listing);
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < clientes.Count - 1) currentIndex = clientes.Count - 1;
            ReadClientes(); // Garante que a lista completa esteja vinculada (limpando filtros)
            SetState(ViewState.Listing);
        }

        // --- MANIPULADOR DE EVENTOS (SELEÇÃO DE LINHA NO DGV) ---

        private void listTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Garante que o clique foi em uma linha de dados válida
            if (e.RowIndex < 0 || e.RowIndex >= listTable.Rows.Count) return;

            // Obtém o objeto Cliente vinculado à linha clicada
            Cliente clienteSelecionado = listTable.Rows[e.RowIndex].DataBoundItem as Cliente;

            if (clienteSelecionado != null)
            {
                // 1. Encontra o índice na lista principal (clientes)
                int novoIndex = clientes.FindIndex(c => c.Id == clienteSelecionado.Id);

                // 2. Atualiza o índice atual da view
                if (novoIndex != -1)
                {
                    currentIndex = novoIndex;
                }

                // 3. Atualiza o estado para refletir a nova seleção nos campos de cadastro e no DGV
                SetState(ViewState.Listing);
            }
        }

        // --- MANIPULADORES de EVENTOS (BOTÕES CRUD) ---

        private void newBtn_Click(object sender, EventArgs e)
        {
            editCliente = new Cliente();
            SetState(ViewState.Creating);
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (clientes.Count == 0) return;
            editCliente = clientes[currentIndex];
            SetState(ViewState.Editing);
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            editCliente = null;
            SetState(ViewState.Listing);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (editCliente == null) return;

            try
            {
                // Bloco de validação de dados usando as Propriedades da classe Cliente
                try
                {
                    editCliente.Nome = nomeBox.Text.Trim();
                    editCliente.Telefone = telefoneBox.Text.Trim();
                    editCliente.Cpf = cpfBox.Text.Trim();
                    editCliente.DtNascimento = dataDeNascimento.Value.Date;

                    if (radioHomem.Checked)
                    {
                        editCliente.Genero = 'M';
                    }
                    else if (radioMulher.Checked)
                    {
                        editCliente.Genero = 'F';
                    }
                    else
                    {
                        throw new ArgumentException("Selecione um gênero (Masculino ou Feminino).");
                    }
                }
                catch (ArgumentException error)
                {
                    MessageBox.Show(
                        error.Message,
                        "Erros de Validação",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Criação ou Atualização no repositório
                if (currentState == ViewState.Creating)
                {
                    clienteRepository.CreateCliente(editCliente);
                    ReadClientes();
                    currentIndex = clientes.Count - 1;
                }
                else if (currentState == ViewState.Editing)
                {
                    clienteRepository.UpdateCliente(editCliente);
                    ReadClientes();
                }

                // Finalização
                editCliente = null;
                SetState(ViewState.Listing);
                MessageBox.Show(
                    "Cliente salvo com sucesso!",
                    "OK",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show(
                    "Erro de Validação: " + aex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao salvar o cliente: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (clientes.Count == 0) return;

            try
            {
                var result = MessageBox.Show(
                    $"Tem certeza que deseja excluir o cliente '{clientes[currentIndex].Nome}'?",
                    "Confirmar Exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No) return;

                int clienteIdParaExcluir = clientes[currentIndex].Id;

                clienteRepository.DeleteCliente(clienteIdParaExcluir);

                ReadClientes();

                if (clientes.Count > 0)
                {
                    if (currentIndex >= clientes.Count) currentIndex = clientes.Count - 1;
                }
                else
                {
                    currentIndex = 0;
                }

                editCliente = null;
                SetState(ViewState.Listing);

                MessageBox.Show(
                    "Cliente excluído com sucesso!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (MySqlException mysex) when (mysex.Number == 1451)
            {
                MessageBox.Show(
                    "Não foi possível excluir o Cliente, pois ele possui vendas ou outras referências associadas. Exclua as vendas primeiro.",
                    "Erro de Chave Estrangeira",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao deletar: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // --- MANIPULADORES DE EVENTOS (BUSCA E SAÍDA) ---

        private void searchBtn_Click(object sender, EventArgs e)
        {
            // Garante que a lista completa esteja vinculada ao entrar na aba de busca
            ReadClientes();
            SwitchToTabByName("tabPageLista");
            searchBox.Focus();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            if (currentState == ViewState.Editing || currentState == ViewState.Creating)
            {
                var result = MessageBox.Show(
                    "Se você sair, suas alterações serão perdidas. Deseja continuar?",
                    "Confirmar",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Cancel) return;
            }
            this.Close();
        }

        private void makeSearch_Click(object sender, EventArgs e)
        {
            // Implementa a lógica de busca/filtro
            string searchTerm = searchBox.Text.Trim().ToLower();
            List<Cliente> filteredClientes;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Busca por ID (se for numérico) ou por Nome/CPF/Telefone
                filteredClientes = clientes.Where(
                    cliente => (int.TryParse(searchTerm, out int id) && cliente.Id == id) ||
                               (cliente.Nome != null && cliente.Nome.ToLower().Contains(searchTerm)) ||
                               (cliente.Cpf != null && cliente.Cpf.Contains(searchTerm)) ||
                               (cliente.Telefone != null && cliente.Telefone.Contains(searchTerm))
                ).ToList();
            }
            else
            {
                // Se a busca estiver vazia, retorna a lista completa
                filteredClientes = clientes;
            }

            // Atribui a lista filtrada como DataSource
            listTable.DataSource = null;
            listTable.DataSource = filteredClientes;

            // Atualiza o currentIndex e o estado com o primeiro item encontrado
            if (filteredClientes.Count > 0)
            {
                // Encontra o índice na lista original que corresponde ao primeiro resultado filtrado
                currentIndex = clientes.FindIndex(cliente => cliente.Id == filteredClientes[0].Id);

                // Popula os campos e seleciona no DataGridView
                PopulateFields(filteredClientes[0]);

                // Seleciona o primeiro item na lista filtrada (posição 0 na lista exibida)
                listTable.ClearSelection();
                listTable.Rows[0].Selected = true;
                listTable.FirstDisplayedScrollingRowIndex = 0;
            }
            else
            {
                // Se nada for encontrado, limpa a tela e o índice
                currentIndex = -1;
                CleanupFields();
                MessageBox.Show(
                    "Nenhum cliente encontrado.",
                    "Busca",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            SetState(ViewState.Listing);
        }

        private void ClientesView_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (currentState == ViewState.Editing || currentState == ViewState.Creating)
            {
                var result = MessageBox.Show(
                    "Se você sair, suas alterações serão perdidas. Deseja continuar?",
                    "Confirmar",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}