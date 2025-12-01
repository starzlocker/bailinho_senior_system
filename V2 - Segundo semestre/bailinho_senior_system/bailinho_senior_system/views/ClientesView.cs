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

        private List<Cliente> clientes = new List<Cliente>();
        private int currentIndex = 0;
        private ViewState currentState;
        private Cliente editCliente = null;

        private ClienteRepository clienteRepository = new ClienteRepository();

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
            // Bloqueia a mudança de aba se estiver criando ou editando até confirmação do usuário
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

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;

            dgv.EnableHeadersVisualStyles = false;
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(173, 216, 230);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 30;
            dgv.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.Columns.Clear();

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
            // Atualiza a lista de clientes
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

            radioHomem.Checked = cliente.Genero == 'M';
            radioMulher.Checked = cliente.Genero == 'F';
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
            var page = tabControl.TabPages.Cast<TabPage>().FirstOrDefault(t => t.Name == tabName);
            if (page != null) tabControl.SelectedTab = page;
        }

        private void UpdateDataGridViewSelection()
        {
            if (listTable == null || clientes.Count == 0 || currentIndex < 0 || currentIndex >= clientes.Count) return;

            CurrencyManager cm = (CurrencyManager)listTable.BindingContext[listTable.DataSource];
            if (cm.Count > 0)
            {
                Cliente clienteAtual = clientes[currentIndex];
                List<Cliente> listaExibida = listTable.DataSource as List<Cliente>;
                int indexNaListaExibida = listaExibida?.FindIndex(c => c.Id == clienteAtual.Id) ?? -1;

                if (indexNaListaExibida != -1)
                {
                    cm.Position = indexNaListaExibida; // Atualiza seleção no DGV
                    listTable.FirstDisplayedScrollingRowIndex = indexNaListaExibida;
                }
                else
                {
                    listTable.ClearSelection();
                }
            }
        }

        private void firstBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex = 0;
            ReadClientes();
            SetState(ViewState.Listing);
        }

        private void previousBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex--;
            ReadClientes();
            SetState(ViewState.Listing);
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < clientes.Count - 1) currentIndex++;
            ReadClientes();
            SetState(ViewState.Listing);
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < clientes.Count - 1) currentIndex = clientes.Count - 1;
            ReadClientes();
            SetState(ViewState.Listing);
        }

        private void listTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= listTable.Rows.Count) return;

            Cliente clienteSelecionado = listTable.Rows[e.RowIndex].DataBoundItem as Cliente;
            if (clienteSelecionado != null)
            {
                int novoIndex = clientes.FindIndex(c => c.Id == clienteSelecionado.Id);
                if (novoIndex != -1) 
                    currentIndex = novoIndex;

                SetState(ViewState.Listing);
            }
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            editCliente = new Cliente();
            SetState(ViewState.Creating);
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (clientes.Count == 0) 
                return;

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
                // Atualiza o objeto cliente com os valores do formulário
                editCliente.Nome = nomeBox.Text.Trim();
                editCliente.Telefone = telefoneBox.Text.Trim();
                editCliente.Cpf = cpfBox.Text.Trim();
                editCliente.DtNascimento = dataDeNascimento.Value.Date;

                if (radioHomem.Checked) editCliente.Genero = 'M';
                else if (radioMulher.Checked) editCliente.Genero = 'F';
                else throw new ArgumentException("Selecione um gênero (Masculino ou Feminino).");

                // Decide se é criação ou atualização
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

                editCliente = null;
                SetState(ViewState.Listing);
                MessageBox.Show("Cliente salvo com sucesso!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show("Erro de Validação: " + aex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar o cliente: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (clientes.Count > 0 && currentIndex >= clientes.Count)
                    currentIndex = clientes.Count - 1;
                else if (clientes.Count == 0)
                    currentIndex = 0;

                editCliente = null;
                SetState(ViewState.Listing);

                MessageBox.Show("Cliente excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException mysex) when (mysex.Number == 1451)
            {
                MessageBox.Show(
                    "Não foi possível excluir o Cliente, pois ele possui vendas ou outras referências associadas.",
                    "Erro de Chave Estrangeira",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao deletar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
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

            if (filteredClientes.Count > 0)
            {
                currentIndex = clientes.FindIndex(cliente => cliente.Id == filteredClientes[0].Id);
                PopulateFields(filteredClientes[0]);

                listTable.ClearSelection();
                listTable.Rows[0].Selected = true;
                listTable.FirstDisplayedScrollingRowIndex = 0;
            }
            else
            {
                currentIndex = -1;
                CleanupFields();
                MessageBox.Show("Nenhum cliente encontrado.", "Busca", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (result == DialogResult.Cancel) e.Cancel = true;
            }
        }
    }
}
