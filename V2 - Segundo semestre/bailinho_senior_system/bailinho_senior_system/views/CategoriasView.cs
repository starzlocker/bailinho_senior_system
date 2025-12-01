using bailinho_senior_system.models;
using bailinho_senior_system.repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace bailinho_senior_system.views
{
    public partial class CategoriasView : Form
    {
        private enum ViewState { Listing, Editing, Creating }

        private List<Categoria> categorias = new List<Categoria>();
        private int currentIndex = 0;
        private ViewState state;
        private Categoria editItem = null;

        private CategoriaRepository categoriaRepository = new CategoriaRepository();

        public CategoriasView()
        {
            InitializeComponent();
        }

        private void CategoriasView_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView(listTable);
            ReadCategorias();

            if (categorias.Count > 0)
                PopulateCategoria(categorias[currentIndex]);

            SetState(ViewState.Listing);
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            // Evita sair do cadastro durante edição/criação
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
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.EnableHeadersVisualStyles = false;
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(173, 216, 230);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 30;
            dgv.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            dgv.Columns.Clear();

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Id",
                DataPropertyName = "Id",
                Name = "Id",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Nome",
                DataPropertyName = "Nome",
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Descrição",
                DataPropertyName = "Descricao",
            });
        }

        private void ReadCategorias()
        {
            try
            {
                categorias = categoriaRepository.GetCategorias();

                listTable.DataSource = null;
                listTable.DataSource = categorias;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar categorias: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateCategoria(Categoria categoria)
        {
            idBox.Text = categoria.Id.ToString();
            nomeBox.Text = categoria.Nome ?? "";
            descricaoBox.Text = categoria.Descricao ?? "";
        }

        private void CleanupFields()
        {
            idBox.Text = "";
            nomeBox.Text = "";
            descricaoBox.Text = "";
        }

        private List<string> ValidateForm()
        {
            List<string> errors = new List<string>();

            if (nomeBox.Text.Length == 0) errors.Add("Nome não pode estar vazio!");
            else if (nomeBox.Text.Length > 150) errors.Add("Nome deve ter até 150 caracteres.");

            if (descricaoBox.Text.Length == 0) errors.Add("Descrição não pode estar vazia!");
            else if (descricaoBox.Text.Length > 150) errors.Add("Descrição deve ter até 150 caracteres.");

            return errors;
        }

        private void SetState(ViewState newState)
        {
            state = newState;

            bool creating = state == ViewState.Creating;
            bool editing = state == ViewState.Editing;
            bool listing = state == ViewState.Listing;

            bool creatingOrEditing = creating || editing;
            int count = categorias.Count;

            // Quando criando/editando, troca automaticamente para a aba de cadastro
            if (creatingOrEditing)
                SwitchToTabByName("tabPageCadastro");

            deleteBtn.Enabled = count > 0 && listing;
            editBtn.Enabled = count > 0 && listing;
            newBtn.Enabled = listing;
            searchBtn.Enabled = listing;

            saveBtn.Enabled = creatingOrEditing;
            cancelBtn.Enabled = creatingOrEditing;

            nextBtn.Enabled = count > 0 && listing && currentIndex < count - 1;
            lastBtn.Enabled = count > 0 && listing && currentIndex < count - 1;
            firstBtn.Enabled = count > 0 && listing && currentIndex > 0;
            previousBtn.Enabled = count > 0 && listing && currentIndex > 0;

            nomeBox.ReadOnly = listing;
            descricaoBox.ReadOnly = listing;

            if (count == 0 || creating)
                CleanupFields();
            else if (listing && currentIndex >= 0)
            {
                PopulateCategoria(categorias[currentIndex]);
                UpdateDataGridViewSelection();
            }
        }

        private void SwitchToTabByName(string tabName)
        {
            // Localiza a aba pelo nome — útil para alternar programaticamente
            var page = tabControl.TabPages.Cast<TabPage>().FirstOrDefault(t => t.Name == tabName);
            if (page != null) tabControl.SelectedTab = page;
        }

        private void UpdateDataGridViewSelection()
        {
            // Seleciona a categoria atual no listTable e rola para visualização
            if (listTable == null || categorias.Count == 0 || currentIndex < 0 || currentIndex >= categorias.Count)
            {
                listTable.ClearSelection();
                return;
            }

            listTable.ClearSelection();

            // Sincroniza a seleção visual
            if (listTable.DataSource is List<Categoria> listaExibida)
            {
                Categoria itemAtual = categorias[currentIndex];

                // Procura o índice correto na lista exibida (caso esteja filtrada)
                int indexNaListaExibida = listaExibida.FindIndex(c => c.Id == itemAtual.Id);

                if (indexNaListaExibida != -1)
                {
                    listTable.Rows[indexNaListaExibida].Selected = true;
                    listTable.FirstDisplayedScrollingRowIndex = indexNaListaExibida;
                }
            }
        }

        private void firstBtn_Click(object sender, EventArgs e)
        {
            currentIndex = 0;
            ReadCategorias();
            SetState(ViewState.Listing);
        }

        private void previousBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex--;
            ReadCategorias();
            SetState(ViewState.Listing);
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < categorias.Count - 1) currentIndex++;
            ReadCategorias();
            SetState(ViewState.Listing);
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            currentIndex = categorias.Count - 1;
            ReadCategorias();
            SetState(ViewState.Listing);
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            editItem = new Categoria();
            SetState(ViewState.Creating);
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            editItem = null;
            SetState(ViewState.Listing);
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (categorias.Count == 0) return;
            editItem = categorias[currentIndex];
            SetState(ViewState.Editing);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (editItem == null) return;

            List<string> errors = ValidateForm();
            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors),
                    "Erros de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                editItem.Nome = nomeBox.Text.Trim();
                editItem.Descricao = descricaoBox.Text.Trim();

                if (state == ViewState.Creating)
                {
                    categoriaRepository.CreateCategoria(editItem);
                    ReadCategorias();
                    currentIndex = categorias.Count - 1;
                }
                else if (state == ViewState.Editing)
                {
                    categoriaRepository.UpdateCategoria(editItem);
                    ReadCategorias();
                }

                SetState(ViewState.Listing);

                MessageBox.Show("Categoria salva com sucesso!",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar categoria: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (categorias.Count == 0) return;

            var result = MessageBox.Show(
                $"Tem certeza que deseja excluir a categoria '{categorias[currentIndex].Nome}'?",
                "Confirmar Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.No) return;

            try
            {
                categoriaRepository.DeleteCategoria(categorias[currentIndex].Id);

                ReadCategorias();

                if (categorias.Count > 0)
                {
                    currentIndex = Math.Max(0, currentIndex - 1);
                }
                else
                {
                    currentIndex = 0;
                }

                editItem = null;
                SetState(ViewState.Listing);

                MessageBox.Show("Categoria excluída com sucesso!",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException mysex) when (mysex.Number == 1451)
            {
                MessageBox.Show("Não foi possível excluir a Categoria, pois ela possui produtos vinculados.",
                    "Erro de Chave Estrangeira", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir categoria: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            Close();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            ReadCategorias();
            SwitchToTabByName("tabPageLista");
            searchBox.Focus();
        }

        private void makeSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = searchBox.Text.Trim().ToLower();
            List<Categoria> filteredCategorias;

            // Busca dinâmica: aceita ID, Nome ou Descrição
            if (!string.IsNullOrEmpty(searchTerm))
            {
                filteredCategorias = categorias.Where(c =>
                    (int.TryParse(searchTerm, out int id) && c.Id == id) ||
                    (c.Nome != null && c.Nome.ToLower().Contains(searchTerm)) ||
                    (c.Descricao != null && c.Descricao.ToLower().Contains(searchTerm))
                ).ToList();
            }
            else
            {
                // Se a busca estiver vazia, retorna a lista completa
                filteredCategorias = categorias;
            }

            listTable.DataSource = null;
            listTable.DataSource = filteredCategorias;

            if (filteredCategorias.Count > 0)
            {
                // Sincroniza o currentIndex com o primeiro item encontrado na lista original
                currentIndex = categorias.FindIndex(c => c.Id == filteredCategorias[0].Id);
                PopulateCategoria(filteredCategorias[0]);
            }
            else
            {
                currentIndex = -1;
                CleanupFields();
                MessageBox.Show("Nenhuma categoria encontrada.",
                    "Busca", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            SetState(ViewState.Listing);
        }

        private void listTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= listTable.Rows.Count) return;
            if (state != ViewState.Listing) return;

            Categoria categoriaSelecionada = listTable.Rows[e.RowIndex].DataBoundItem as Categoria;

            if (categoriaSelecionada != null)
            {
                // Reencontra o índice na lista original — importante quando filtrado
                int novoIndex = categorias.FindIndex(c => c.Id == categoriaSelecionada.Id);

                if (novoIndex != -1)
                {
                    currentIndex = novoIndex;
                }
                SetState(ViewState.Listing);
            }
        }
    }
}
