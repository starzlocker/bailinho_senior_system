using bailinho_senior_system.models;
using bailinho_senior_system.repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace bailinho_senior_system.views
{
    public enum ViewState { Listing, Editing, Creating }

    public partial class EventosView : Form
    {
        // --- Variáveis de Estado da View ---
        private List<Evento> eventos = new List<Evento>();
        private int currentIndex = 0;
        private ViewState state;
        private Evento editItem = null;
        private Endereco editEndereco = null; // Objeto Endereço sendo editado/criado

        // --- Repositórios ---
        private EventoRepository eventoRepository = new EventoRepository();
        private EnderecoRepository enderecoRepository = new EnderecoRepository();


        // --- Construtor e Eventos de Inicialização ---

        public EventosView()
        {
            InitializeComponent();
            this.Load += EventosView_Load;
            this.tabControl.Selecting += tabControl_Selecting;
            // Adicione a vinculação dos eventos de lista (se tiver)
            // this.listTable.SelectionChanged += listTable_SelectionChanged; 
        }

        private void EventosView_Load(object sender, EventArgs e)
        {
            ReadEventos();

            if (eventos.Count > 0)
                PopulateFields(eventos[currentIndex]);

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

                    editItem = null;
                    editEndereco = null;
                    SetState(ViewState.Listing);
                }
            }
        }

        // --- Configuração e Leitura de Dados ---

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
                HeaderText = "ID",
                DataPropertyName = "Id",
                Name = "Id",
                Width = 45,
            });

            // 1. Nome do Evento
            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Evento",
                DataPropertyName = "Nome",
                Width = 150,
                Resizable = DataGridViewTriState.False
            });

            // 2. Data
            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Data",
                DataPropertyName = "Data",
                Width = 100,
                Resizable = DataGridViewTriState.False,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            // 3. Hora
            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Hora",
                DataPropertyName = "Hora",
                Width = 60,
                Resizable = DataGridViewTriState.False,
                DefaultCellStyle = new DataGridViewCellStyle { Format = @"hh\:mm" }
            });

            // 4. Valor da Entrada (Moeda)
            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Entrada",
                DataPropertyName = "ValorEntrada",
                Width = 90,
                Resizable = DataGridViewTriState.False,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C", // Formato de moeda local
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // 5. Endereço COMPLETO (Usa o Fill para ocupar o restante do espaço)
            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Endereço",
                DataPropertyName = "EnderecoCompleto",
                Name = "colEnderecoCompleto" // Nome de referência para o Fill
            });

            // Garante que a coluna Endereço Completo ocupe o restante do espaço
            listTable.Columns["colEnderecoCompleto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void ReadEventos()
        {
            try
            {
                this.eventos = eventoRepository.GetEventos();

                ConfigurarDataGridView();

                listTable.DataSource = null; // Limpa a fonte de dados anterior
                listTable.DataSource = this.eventos; // Associa a nova lista

                if (eventos.Count > 0)
                {
                    // Garante que o item atual é mantido selecionado, se possível
                    if (currentIndex < eventos.Count)
                    {
                        listTable.Rows[currentIndex].Selected = true;
                    }
                    else
                    {
                        currentIndex = eventos.Count - 1;
                        listTable.Rows[currentIndex].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar eventos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateFields(Evento evento)
        {
            // Busca e popula o endereço associado
            Endereco endereco = enderecoRepository.GetEndereco(evento.IdEndereco);
            editEndereco = endereco; // Mantém o objeto Endereço em memória

            // Preenche campos do Evento
            idBox.Text = evento.Id.ToString();
            nomeBox.Text = evento.Nome ?? "";
            descricaoBox.Text = evento.Descricao ?? "";
            dateBox.Value = evento.Data;
            timeBox.Text = evento.Hora.ToString(@"hh\:mm");
            valorEntradaBox.Value = evento.ValorEntrada;

            // Preenche campos do Endereço (se encontrado)
            if (endereco != null)
            {
                cepBox.Text = endereco.Cep ?? "";
                logradouroBox.Text = endereco.Logradouro ?? "";
                bairroBox.Text = endereco.Bairro ?? "";
                cidadeBox.Text = endereco.Cidade ?? "";
                numeroBox.Text = endereco.Numero ?? "";
                estadoBox.Text = endereco.Estado ?? "";
                complementoBox.Text = endereco.Complemento ?? "";
            }
            else
            {
                CleanupFields();
            }
        }

        private void CleanupFields()
        {
            // Limpa campos do Evento
            idBox.Text = "";
            nomeBox.Text = "";
            descricaoBox.Text = "";
            dateBox.Value = DateTime.Today;
            timeBox.Text = "00:00";
            valorEntradaBox.Value = 0;

            // Limpa campos de Endereço
            cepBox.Text = "";
            logradouroBox.Text = "";
            bairroBox.Text = "";
            cidadeBox.Text = "";
            numeroBox.Text = "";
            estadoBox.Text = "";
            complementoBox.Text = "";
        }

        private List<string> ValidateForm()
        {
            List<string> errors = new List<string>();

            // Validações do Evento
            if (string.IsNullOrWhiteSpace(nomeBox.Text)) errors.Add("Nome do Evento não pode estar vazio.");
            if (valorEntradaBox.Value < 0) errors.Add("Valor da entrada não pode ser negativo.");
            if (!TimeSpan.TryParse(timeBox.Text, out _)) errors.Add("Formato de hora inválido (HH:mm).");

            // Validações do Endereço
            if (string.IsNullOrWhiteSpace(cepBox.Text)) errors.Add("CEP não pode estar vazio.");
            if (cepBox.Text.Length != 8) errors.Add("CEP deve ter 8 caracteres.");
            if (string.IsNullOrWhiteSpace(logradouroBox.Text)) errors.Add("Logradouro não pode estar vazio.");
            if (string.IsNullOrWhiteSpace(bairroBox.Text)) errors.Add("Bairro não pode estar vazio.");
            if (string.IsNullOrWhiteSpace(cidadeBox.Text)) errors.Add("Cidade não pode estar vazia.");
            if (string.IsNullOrWhiteSpace(numeroBox.Text)) errors.Add("Número não pode estar vazio.");
            if (estadoBox.Text.Length != 2) errors.Add("Estado deve ter 2 caracteres (UF).");

            return errors;
        }

        private void SetState(ViewState newState)
        {
            state = newState;

            bool creatingOrEditing = state == ViewState.Creating || state == ViewState.Editing;
            bool listing = state == ViewState.Listing;
            int count = eventos.Count;

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

            // Habilitação/Desabilitação de Campos (Evento)
            nomeBox.ReadOnly = listing;
            descricaoBox.ReadOnly = listing;
            dateBox.Enabled = !listing;
            timeBox.ReadOnly = listing;
            valorEntradaBox.Enabled = !listing;

            // Habilitação/Desabilitação de Campos (Endereço)
            cepBox.ReadOnly = listing;
            logradouroBox.ReadOnly = listing;
            bairroBox.ReadOnly = listing;
            cidadeBox.ReadOnly = listing;
            numeroBox.ReadOnly = listing;
            estadoBox.ReadOnly = listing;
            complementoBox.ReadOnly = listing;

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
                PopulateFields(eventos[currentIndex]);
            }
        }

        private void SwitchToTabByName(string tabName)
        {
            if (string.IsNullOrEmpty(tabName)) return;
            // Assumindo um TabControl nomeado 'tabControl'
            var page = tabControl.TabPages.Cast<TabPage>().FirstOrDefault(t => t.Name == tabName);
            if (page != null) tabControl.SelectedTab = page;
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
            if (currentIndex < eventos.Count - 1) currentIndex++;
            SetState(ViewState.Listing);
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < eventos.Count - 1) currentIndex = eventos.Count - 1;
            SetState(ViewState.Listing);
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            editItem = new Evento();
            editEndereco = new Endereco(); // Novo objeto Endereco
            SetState(ViewState.Creating);
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (eventos.Count == 0) return;
            editItem = eventos[currentIndex];
            SetState(ViewState.Editing);
            // O objeto editEndereco será carregado/mantido no PopulateFields.
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            editItem = null;
            editEndereco = null;
            SetState(ViewState.Listing);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (editItem == null || editEndereco == null) return;

            try
            {
                // 1. Validação
                List<string> errors = ValidateForm();
                if (errors.Count > 0)
                {
                    MessageBox.Show(string.Join("\n", errors), "Erros de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Coleta de Dados do Endereço
                editEndereco.Cep = cepBox.Text.Trim();
                editEndereco.Logradouro = logradouroBox.Text.Trim();
                editEndereco.Bairro = bairroBox.Text.Trim();
                editEndereco.Cidade = cidadeBox.Text.Trim();
                editEndereco.Numero = numeroBox.Text.Trim();
                editEndereco.Estado = estadoBox.Text.Trim();
                editEndereco.Complemento = complementoBox.Text.Trim();

                // 3. Persistência (Criação/Atualização) do Endereço
                if (editEndereco.Id == 0)
                {
                    enderecoRepository.CreateEndereco(editEndereco);
                }
                else
                {
                    enderecoRepository.UpdateEndereco(editEndereco);
                }

                // 4. Coleta de Dados e Persistência do Evento
                editItem.Nome = nomeBox.Text.Trim();
                editItem.Descricao = descricaoBox.Text.Trim();
                editItem.Data = dateBox.Value.Date;
                editItem.Hora = TimeSpan.Parse(timeBox.Text.Trim());
                editItem.ValorEntrada = valorEntradaBox.Value;
                editItem.IdEndereco = editEndereco.Id; // Vincula o ID do Endereço salvo/atualizado

                if (state == ViewState.Creating)
                {
                    eventoRepository.CreateEvento(editItem);
                    ReadEventos();
                    currentIndex = eventos.Count - 1;
                }
                else if (state == ViewState.Editing)
                {
                    eventoRepository.UpdateEvento(editItem);
                    ReadEventos();
                }

                // 5. Finalização
                editItem = null;
                editEndereco = null;
                SetState(ViewState.Listing);
                MessageBox.Show("Evento e Endereço salvos com sucesso!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show("Erro de Validação: " + aex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar o evento/endereço: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (eventos.Count == 0) return;

            try
            {
                var result = MessageBox.Show(
                    $"Tem certeza que deseja excluir o evento '{eventos[currentIndex].Nome}'?",
                    "Confirmar Exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No) return;

                int eventoIdParaExcluir = eventos[currentIndex].Id;
                int enderecoIdParaExcluir = eventos[currentIndex].IdEndereco;

                // 1. Exclui o Evento (primeiro, devido à FK Venda)
                eventoRepository.DeleteEvento(eventoIdParaExcluir);

                // 2. Exclui o Endereço
                enderecoRepository.DeleteEndereco(enderecoIdParaExcluir);

                // 3. Finaliza
                ReadEventos();

                if (eventos.Count > 0)
                {
                    if (currentIndex >= eventos.Count) currentIndex = eventos.Count - 1;
                }
                else
                {
                    currentIndex = 0;
                }

                editItem = null;
                editEndereco = null;
                SetState(ViewState.Listing);

                MessageBox.Show("Evento e Endereço excluídos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException mysex) when (mysex.Number == 1451)
            {
                MessageBox.Show("Não foi possível excluir o Evento, pois ele possui vendas ou outras referências associadas. Exclua as vendas primeiro.", "Erro de Chave Estrangeira", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao deletar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            SwitchToTabByName("tabPageLista");
            // Assumindo que a caixa de busca se chama 'searchBox'
            // searchBox.Focus();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            if (state == ViewState.Editing || state == ViewState.Creating)
            {
                var result = MessageBox.Show(
                    "Se você sair, suas alterações serão perdidas. Deseja continuar?",
                    "Confirmar",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Cancel) return;
            }
            this.Close();
        }

        // --- Eventos adicionais do Designer ---

        private void button1_Click(object sender, EventArgs e)
        {
            // Este método implementa a lógica de busca/filtro.

            // 1. Obtém o termo de busca (assumindo que o TextBox se chama searchBox)
            string searchTerm = searchBox.Text.Trim().ToLower();
            List<Evento> filteredEvents = eventos;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // 2. Tenta buscar por ID (se for numérico) ou por Nome
                if (int.TryParse(searchTerm, out int id))
                {
                    // Busca por ID
                    filteredEvents = eventos.Where(evento => evento.Id == id).ToList();
                }
                else
                {
                    // Busca por Nome (ou Descrição, se desejar)
                    filteredEvents = eventos.Where(evento =>
                        evento.Nome.ToLower().Contains(searchTerm) ||
                        (evento.Descricao != null && evento.Descricao.ToLower().Contains(searchTerm))
                    ).ToList();
                }
            }

            // 3. Atualiza o DataGridView com a lista filtrada/original
            listTable.DataSource = null;
            listTable.DataSource = filteredEvents;

            // 4. Atualiza o currentIndex e o estado com o primeiro item encontrado
            if (filteredEvents.Count > 0)
            {
                // Encontra o índice na lista original para manter a navegação consistente
                currentIndex = eventos.FindIndex(evento => evento.Id == filteredEvents[0].Id);
                listTable.Rows[0].Selected = true;
                PopulateFields(filteredEvents[0]);
            }
            else
            {
                // Se nada for encontrado, limpamos a tela
                currentIndex = -1;
                CleanupFields();
                MessageBox.Show("Nenhum evento encontrado.", "Busca", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            SetState(ViewState.Listing);
        }

        private void listTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o evento foi disparado pela atualização de dados ou se não há linha atual
            if (listTable.CurrentRow == null || listTable.CurrentRow.Index < 0) return;

            // Ignora se estivermos nos modos de Edição/Criação, onde o foco deve permanecer nos campos
            if (state != ViewState.Listing) return;

            try
            {
                // 1. Obtém a linha atualmente selecionada no DGV filtrado
                var currentRow = listTable.CurrentRow;

                // Verifica se a lista de eventos está vazia antes de prosseguir
                if (eventos.Count == 0) return;

                // 2. Extrai o ID do evento da célula oculta 'Id'
                // NOTA: É fundamental que a coluna 'Id' exista e seja oculta.
                int selectedId = (int)currentRow.Cells["Id"].Value;

                // 3. Encontra o índice correspondente na lista de eventos ORIGINAL (eventos)
                int newIndex = eventos.FindIndex(evento => evento.Id == selectedId);

                if (newIndex != -1)
                {
                    currentIndex = newIndex;
                    // A chamada a SetState fará a dupla função:
                    // a) Chamar PopulateFields(eventos[currentIndex]);
                    // b) Atualizar o estado dos botões.
                    SetState(ViewState.Listing);
                }
            }
            catch (Exception)
            {
                // Geralmente ocorre durante a inicialização/re-vinculação da fonte de dados.
                // É seguro ignorar neste contexto para não travar a UI.
            }
        }

        private void listTable_SelectionChanged(object sender, EventArgs e)
        {
            // Verifica se o evento foi disparado pela atualização de dados ou se não há linha atual
            if (listTable.CurrentRow == null || listTable.CurrentRow.Index < 0) return;

            // Ignora se estivermos nos modos de Edição/Criação, onde o foco deve permanecer nos campos
            if (state != ViewState.Listing) return;

            try
            {
                // 1. Obtém a linha atualmente selecionada no DGV filtrado
                var currentRow = listTable.CurrentRow;

                // Verifica se a lista de eventos está vazia antes de prosseguir
                if (eventos.Count == 0) return;

                // 2. Extrai o ID do evento da célula oculta 'Id'
                // NOTA: É fundamental que a coluna 'Id' exista e seja oculta.
                int selectedId = (int)currentRow.Cells["Id"].Value;

                // 3. Encontra o índice correspondente na lista de eventos ORIGINAL (eventos)
                int newIndex = eventos.FindIndex(evento => evento.Id == selectedId);

                if (newIndex != -1)
                {
                    currentIndex = newIndex;
                    // A chamada a SetState fará a dupla função:
                    // a) Chamar PopulateFields(eventos[currentIndex]);
                    // b) Atualizar o estado dos botões.
                    SetState(ViewState.Listing);
                }
            }
            catch (Exception)
            {
                // Geralmente ocorre durante a inicialização/re-vinculação da fonte de dados.
                // É seguro ignorar neste contexto para não travar a UI.
            }
        }
    }
}