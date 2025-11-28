using bailinho_senior_system.models;
using bailinho_senior_system.repositories;
using MySql.Data.MySqlClient;
using Mysqlx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

namespace bailinho_senior_system.views
{

    public partial class ClientesView : Form
    {
        private enum ViewState { Listing, Editing, Creating }

        // --- Variáveis de Estado da View ---
        private List<Cliente> clientes = new List<Cliente>();
        private int currentIndex = 0;
        private ViewState state;
        private Cliente editCliente = null;

        // --- Repositórios ---
        private ClienteRepository clienteRepository = new ClienteRepository();

        // --- Construtor e Eventos de Inicialização ---
        public ClientesView()
        {
            InitializeComponent();
            this.Load += ClientesView_Load;
            this.tabControl.Selecting += tabControl_Selecting;
        }

        private void ClientesView_Load(object sender, EventArgs e)
        {
            ReadClientes();

            if (clientes.Count > 0)
                PopulateFields(clientes[currentIndex]);

            SetState(ViewState.Listing);
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (state == ViewState.Editing || state == ViewState.Creating)
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

        private void ConfigurarDataGridView()
        {
            listTable.Columns.Clear();
            listTable.AutoGenerateColumns = false;
            listTable.ReadOnly = true;
            listTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            listTable.AllowUserToAddRows = false;
            listTable.MultiSelect = false;
            listTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // --- Configurações de Aparência (Cores e Estilo) ---
            listTable.EnableHeadersVisualStyles = false;
            listTable.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            listTable.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            listTable.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(173, 216, 230); // Azul claro
            listTable.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            listTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            listTable.ColumnHeadersHeight = 30;

            // Estilo do corpo
            listTable.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            // --- Adição e Formatação das Colunas ---

            // 0. ID
            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Id",
                DataPropertyName = "Id",
                Name = "Id",
                Width = 45,
            });

            // 1. Nome do Cliente
            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Nome",
                DataPropertyName = "Nome",
                Width = 150,
                Resizable = DataGridViewTriState.False
            });

            // 2. Data de Nascimento
            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Data",
                DataPropertyName = "Data",
                Width = 100,
                Resizable = DataGridViewTriState.False,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            // 3. genero
            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Genero",
                DataPropertyName = "Genero",
                Width = 75,
                Resizable = DataGridViewTriState.False
            });

            // 4. CPF
            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "CPF",
                DataPropertyName = "CPF",
                Width = 150,
                Resizable = DataGridViewTriState.False
            });

            // 5. Telefone
            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Telefone",
                DataPropertyName = "Telefone",
                Width = 150,
                Resizable = DataGridViewTriState.False
            });
        }

        private void ReadClientes()
        {
            this.clientes = clienteRepository.GetClientes();

            ConfigurarDataGridView();

            listTable.Rows.Clear();
            foreach (var cliente in clientes)
            {
                listTable.Rows.Add(
                    cliente.getId().ToString(),
                    cliente.getNome(),
                    cliente.getDt_nascimento().ToString("dd/MM/yyyy"),
                    cliente.getGenero(),
                    cliente.getCpf(),
                    cliente.getTelefone()
                );
            }
        }

        private void PopulateFields(Cliente cliente)
        {
            // Preenche campos do Cliente
            idBox.Text = cliente.getId().ToString();
            nomeBox.Text = cliente.getNome() ?? "";
            telefoneBox.Text = cliente.getTelefone() ?? "";
            cpfBox.Text = cliente.getCpf();
            dataDeNascimento.Value = cliente.getDt_nascimento();

            if (cliente.getGenero() == 'M')
            {
                radioHomem.Checked = true;
            } else
            {
                radioMulher.Checked = true;
            }
        }

        private void SetState(ViewState newState)
        {
            state = newState;

            bool creatingOrEditing = state == ViewState.Creating || state == ViewState.Editing;
            bool listing = state == ViewState.Listing;
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

            if (state == ViewState.Creating || count == 0)
            {
                CleanupFields();
            }
            else if (listing && count > 0)
            {
                PopulateFields(clientes[currentIndex]);
            }
        }

        private void SwitchToTabByName(string tabName)
        {
            if (string.IsNullOrEmpty(tabName)) return;
            var page = tabControl.TabPages.Cast<TabPage>().FirstOrDefault(t => t.Name == tabName);
            if (page != null) tabControl.SelectedTab = page;
        }

        private void CleanupFields()
        {
            idBox.Text = "";
            nomeBox.Text = "";
            telefoneBox.Text = "";
            cpfBox.Text = "";
            dataDeNascimento.Value = DateTime.Today;
            radioHomem.Checked = false;
            radioMulher.Checked = false;
        }

        // --- MANIPULADORES DE EVENTOS (BOTÕES) ---

        private void firstBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex = 0;
            SetState(ViewState.Listing);
        }

        private void previousBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex--;
            SetState(ViewState.Listing);
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < clientes.Count - 1) currentIndex++;
            SetState(ViewState.Listing);
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < clientes.Count - 1) currentIndex = clientes.Count - 1;
            SetState(ViewState.Listing);
        }

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
                try
                {
                    editCliente.setNome(nomeBox.Text.Trim());
                    editCliente.setTelefone(telefoneBox.Text.Trim());
                    editCliente.setCPF(cpfBox.Text.Trim());
                    editCliente.setDt_nascimento(dataDeNascimento.Value.Date);
                    if (radioHomem.Checked)
                    {
                        editCliente.setGenero('M');
                    }
                    else if (radioMulher.Checked)
                    {
                        editCliente.setGenero('F');
                    } else
                    {
                        throw new ArgumentException("Selecione um gênero");
                    }
                } catch (ArgumentException error)
                {
                    MessageBox.Show(
                        error.Message,
                        "Erros de Validação",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                if (state == ViewState.Creating)
                {
                    clienteRepository.CreateCliente(editCliente);
                    ReadClientes();
                    currentIndex = clientes.Count - 1;
                }
                else if (state == ViewState.Editing)
                {
                    clienteRepository.UpdateCliente(editCliente);
                    ReadClientes();
                }

                // 5. Finalização
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
                    $"Tem certeza que deseja excluir o cliente '{clientes[currentIndex].getNome()}'?",
                    "Confirmar Exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No) return;

                int clienteIdParaExcluir = clientes[currentIndex].getId();

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

        private void searchBtn_Click(object sender, EventArgs e)
        {
            SwitchToTabByName("tabPageLista");
            searchBox.Focus();
        }
        private void exitBtn_Click(object sender, EventArgs e)
        {
            if (state == ViewState.Editing || state == ViewState.Creating)
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
            // Este método implementa a lógica de busca/filtro.

            // 1. Obtém o termo de busca (assumindo que o TextBox se chama searchBox)
            string searchTerm = searchBox.Text.Trim().ToLower();
            List<Cliente> filteredClientes = clientes;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // 2. Tenta buscar por ID (se for numérico) ou por Nome
                if (int.TryParse(searchTerm, out int id))
                {
                    // Busca por ID
                    filteredClientes = clientes.Where(
                        cliente => cliente.getId() == id
                    ).ToList();
                }
                else
                {
                    // Busca por Nome
                    filteredClientes = clientes.Where(
                        cliente => cliente.getNome().ToLower().Contains(searchTerm)
                    ).ToList();
                }
            }

            // 3. Atualiza o DataGridView com a lista filtrada/original
            listTable.Rows.Clear();
            foreach (var cliente in filteredClientes)
            {
                listTable.Rows.Add(
                    cliente.getId().ToString(),
                    cliente.getNome(),
                    cliente.getDt_nascimento().ToString("dd/MM/yyyy"),
                    cliente.getGenero(),
                    cliente.getCpf(),
                    cliente.getTelefone()
                );
            }

            // 4. Atualiza o currentIndex e o estado com o primeiro item encontrado
            if (filteredClientes.Count > 0)
            {
                // Encontra o índice na lista original para manter a navegação consistente
                currentIndex = clientes.FindIndex(cliente => cliente.getId() == filteredClientes[0].getId());
                listTable.Rows[0].Selected = true;
                PopulateFields(filteredClientes[0]);
            }
            else
            {
                // Se nada for encontrado, limpamos a tela
                if (filteredClientes.Count > 0) currentIndex = -1;
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
    }
}
