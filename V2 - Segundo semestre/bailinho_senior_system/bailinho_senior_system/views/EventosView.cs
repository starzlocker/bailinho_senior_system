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
    public partial class EventosView : Form
    {
        private enum ViewState { Listing, Editing, Creating }

        private List<Evento> eventos = new List<Evento>();
        private int currentIndex = 0;
        private ViewState state;
        private Evento editItem = null;
        private Endereco editEndereco = null;

        private EventoRepository eventoRepository = new EventoRepository();
        private EnderecoRepository enderecoRepository = new EnderecoRepository();

        public EventosView()
        {
            InitializeComponent();
            this.Load += EventosView_Load;
            this.tabControl.Selecting += tabControl_Selecting;
            this.listTable.CellClick += listTable_CellClick;
        }

        private void EventosView_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
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


        private void ConfigurarDataGridView()
        {
            // Configurações visuais e de comportamento
            listTable.AutoGenerateColumns = false;
            listTable.ReadOnly = true;
            listTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            listTable.AllowUserToAddRows = false;
            listTable.MultiSelect = false;

            // Estilos visuais
            listTable.EnableHeadersVisualStyles = false;
            listTable.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            listTable.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            listTable.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(173, 216, 230);
            listTable.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            listTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            listTable.ColumnHeadersHeight = 30;
            listTable.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            // Adição e Mapeamento das Colunas
            listTable.Columns.Clear();

            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ID",
                DataPropertyName = "Id",
                Name = "Id",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Evento",
                DataPropertyName = "Nome",
                Name = "colNomeEvento",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
            });

            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Data",
                DataPropertyName = "Data",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Hora",
                DataPropertyName = "Hora",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = new DataGridViewCellStyle { Format = @"hh\:mm" }
            });

            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Entrada",
                DataPropertyName = "ValorEntrada",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            listTable.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Endereço",
                DataPropertyName = "EnderecoCompleto",
                Name = "colEnderecoCompleto",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            listTable.Columns["colNomeEvento"].FillWeight = 50;
            listTable.Columns["colEnderecoCompleto"].FillWeight = 50;
        }

        private void ReadEventos()
        {
            try
            {
                // Carrega todos os eventos
                this.eventos = eventoRepository.GetEventos();
                listTable.DataSource = null;
                listTable.DataSource = this.eventos;
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
            editEndereco = endereco;

            // Preenche campos do Evento
            idBox.Text = evento.Id.ToString();
            nomeBox.Text = evento.Nome ?? "";
            descricaoBox.Text = evento.Descricao ?? "";
            dateBox.Value = evento.Data;
            timeBox.Text = evento.Hora.ToString(@"hh\:mm");
            valorEntradaBox.Value = evento.ValorEntrada;

            // Preenche campos do Endereço
            cepBox.Text = endereco.Cep ?? "";
            logradouroBox.Text = endereco.Logradouro ?? "";
            bairroBox.Text = endereco.Bairro ?? "";
            cidadeBox.Text = endereco.Cidade ?? "";
            numeroBox.Text = endereco.Numero ?? "";
            estadoBox.Text = endereco.Estado ?? "";
            complementoBox.Text = endereco.Complemento ?? "";
        }

        private void CleanupFields()
        {
            // Limpa todos os campos de evento
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
            // Gerencia a visibilidade e habilitação dos botões e campos de entrada
            state = newState;

            bool creatingOrEditing = state == ViewState.Creating || state == ViewState.Editing;
            bool listing = state == ViewState.Listing;
            int count = eventos.Count;

            // Navegação
            firstBtn.Enabled = listing && currentIndex > 0;
            previousBtn.Enabled = listing && currentIndex > 0;
            nextBtn.Enabled = listing && currentIndex < count - 1;
            lastBtn.Enabled = listing && currentIndex < count - 1;

            // CRUD
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
            else if (listing && count > 0 && currentIndex >= 0)
            {
                PopulateFields(eventos[currentIndex]);
                UpdateDataGridViewSelection();
            }
        }

        private void SwitchToTabByName(string tabName)
        {
            // Muda para a aba especificada no TabControl
            var page = tabControl.TabPages.Cast<TabPage>().FirstOrDefault(t => t.Name == tabName);
            if (page != null) tabControl.SelectedTab = page;
        }

        private void UpdateDataGridViewSelection()
        {
            // Seleciona o item atual no DataGridView e rola para visualização
            if (listTable == null || eventos.Count == 0 || currentIndex < 0 || currentIndex >= eventos.Count)
            {
                return;
            }

            // Tenta encontrar o item pelo índice na lista atual do DGV
            if (listTable.DataSource is List<Evento> listaExibida)
            {
                Evento eventoAtual = eventos[currentIndex];
                int indexNaListaExibida = listaExibida.FindIndex(e => e.Id == eventoAtual.Id);

                if (indexNaListaExibida != -1)
                {
                    // Limpa seleções anteriores e seleciona a nova linha
                    listTable.ClearSelection();
                    listTable.Rows[indexNaListaExibida].Selected = true;

                    // Rola para a linha selecionada
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
            currentIndex = 0;
            ReadEventos();
            SetState(ViewState.Listing);
        }

        private void previousBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex--;
            ReadEventos();
            SetState(ViewState.Listing);
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < eventos.Count - 1) currentIndex++;
            ReadEventos();
            SetState(ViewState.Listing);
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            currentIndex = eventos.Count - 1;
            ReadEventos();
            SetState(ViewState.Listing);
        }

        private void listTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Garante que o clique foi em uma linha de dados válida
            if (e.RowIndex < 0 || e.RowIndex >= listTable.Rows.Count) return;

            if (state != ViewState.Listing) return;

            // Obtém o objeto Evento vinculado à linha clicada
            Evento eventoSelecionado = listTable.Rows[e.RowIndex].DataBoundItem as Evento;

            if (eventoSelecionado != null)
            {
                int novoIndex = eventos.FindIndex(c => c.Id == eventoSelecionado.Id);
                if (novoIndex != -1)
                {
                    currentIndex = novoIndex;
                }

                // Atualiza o estado para refletir a nova seleção
                SetState(ViewState.Listing);
            }
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            editItem = new Evento();
            editEndereco = new Endereco();
            SetState(ViewState.Creating);
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (eventos.Count == 0) return;
            editItem = eventos[currentIndex];
            SetState(ViewState.Editing);
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
                List<string> errors = ValidateForm();
                if (errors.Count > 0)
                {
                    MessageBox.Show(string.Join("\n", errors), "Erros de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                editEndereco.Cep = cepBox.Text.Trim();
                editEndereco.Logradouro = logradouroBox.Text.Trim();
                editEndereco.Bairro = bairroBox.Text.Trim();
                editEndereco.Cidade = cidadeBox.Text.Trim();
                editEndereco.Numero = numeroBox.Text.Trim();
                editEndereco.Estado = estadoBox.Text.Trim();
                editEndereco.Complemento = complementoBox.Text.Trim();

                if (editEndereco.Id == 0)
                {
                    enderecoRepository.CreateEndereco(editEndereco);
                }
                else
                {
                    enderecoRepository.UpdateEndereco(editEndereco);
                }

                editItem.Nome = nomeBox.Text.Trim();
                editItem.Descricao = descricaoBox.Text.Trim();
                editItem.Data = dateBox.Value.Date;
                editItem.Hora = TimeSpan.Parse(timeBox.Text.Trim());
                editItem.ValorEntrada = valorEntradaBox.Value;
                editItem.IdEndereco = editEndereco.Id;

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

                editItem = null;
                editEndereco = null;
                SetState(ViewState.Listing);
                MessageBox.Show("Evento salvo com sucesso!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show("Erro de Validação: " + aex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar o evento: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                eventoRepository.DeleteEvento(eventoIdParaExcluir);
                enderecoRepository.DeleteEndereco(enderecoIdParaExcluir);

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

        private void button1_Click(object sender, EventArgs e)
        {
            // Lógica de busca/filtro
            string searchTerm = searchBox.Text.Trim().ToLower();
            List<Evento> filteredEvents;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Busca por ID (se for numérico) OU por Nome/Descrição
                filteredEvents = eventos.Where(evento =>
                    (int.TryParse(searchTerm, out int id) && evento.Id == id) ||
                    (evento.Nome.ToLower().Contains(searchTerm)) ||
                    (evento.Descricao != null && evento.Descricao.ToLower().Contains(searchTerm))
                ).ToList();
            }
            else
            {
                // Se a busca estiver vazia, retorna a lista completa
                filteredEvents = eventos;
            }

            listTable.DataSource = null;
            listTable.DataSource = filteredEvents;

            // Atualiza o currentIndex e o estado com o primeiro item encontrado
            if (filteredEvents.Count > 0)
            {
                currentIndex = eventos.FindIndex(evento => evento.Id == filteredEvents[0].Id);
                PopulateFields(filteredEvents[0]);
            }
            else
            {
                currentIndex = -1;
                CleanupFields();
                MessageBox.Show("Nenhum evento encontrado.", "Busca", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            SetState(ViewState.Listing);
        }
    }
}