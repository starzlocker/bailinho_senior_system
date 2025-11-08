using bailinho_senior_system.models;
using bailinho_senior_system.repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bailinho_senior_system.views
{
    public partial class CategoriasView : Form
    {
        public CategoriasView()
        {
            InitializeComponent();
        }

        List<Categoria> categorias = new List<Categoria>();
        int currentIndex = 0;

        private enum ViewState { Listing, Editing, Creating }
        private ViewState state;
        private Categoria editItem = null;

        private void CategoriasView_Load(object sender, EventArgs e)
        {
            tabControl.Selecting += tabControl_Selecting;

            currentIndex = 0;
            ReadCategorias();

            if (categorias.Count > 0)
                PopulateCategoria(categorias[currentIndex]);


            SetState(ViewState.Listing);
        }

        private List<string> ValidateForm()
        {
            List<string> errors = new List<string>();

            if (nomeBox.Text.Length == 0) errors.Add("Nome não pode estar vazio!");
            else if (nomeBox.Text.Length > 150) errors.Add("Nome deve ter até 150 caracteres.");

            if (descricaoBox.Text.Length == 0) errors.Add("Descrição não pode estar vazio!");
            else if (descricaoBox.Text.Length > 150) errors.Add("Descricao deve ter até 150 caracteres.");

            return errors;
        }

        private void SetState(ViewState newState)
        {
            state = newState;

            var creating = state == ViewState.Creating;
            var editing = state == ViewState.Editing;
            var listing = state == ViewState.Listing;


            string curTab = tabControl.SelectedTab?.Name ?? "";

            if (creating || editing)
            {
                SwitchToTabByName("tabPageCadastro");
            }


            deleteBtn.Enabled = categorias.Count > 0 && listing;
            editBtn.Enabled = categorias.Count > 0 && listing;
            newBtn.Enabled = listing;
            searchBtn.Enabled = categorias.Count > 0 && listing;

            saveBtn.Enabled = editing || creating;
            cancelBtn.Enabled = editing || creating;

            nextBtn.Enabled = categorias.Count > 0 && listing && currentIndex < categorias.Count - 1;
            lastBtn.Enabled = categorias.Count > 0 && listing && currentIndex < categorias.Count - 1;
            firstBtn.Enabled = categorias.Count > 0 && listing && currentIndex > 0;
            previousBtn.Enabled = categorias.Count > 0 && listing && currentIndex > 0;

            nomeBox.ReadOnly = listing;
            descricaoBox.ReadOnly = listing;

            if (categorias.Count == 0 || creating)
            {
                CleanupFields();
            }
            else if (listing)
            {
                PopulateCategoria(categorias[currentIndex]);
            }

        }

        private void ReadCategorias()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Nome");
            dataTable.Columns.Add("Descrição");

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            this.categorias = categoriaRepository.GetCategorias();
            foreach (Categoria c in categorias)
            {
                var row = dataTable.NewRow();

                row["Id"] = c.Id;
                row["Nome"] = c.Nome;
                row["Descrição"] = c.Descricao;

                dataTable.Rows.Add(row);
            }

            listTable.DataSource = dataTable;
        }


        private void previousBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex--;
            SetState(ViewState.Listing);
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < categorias.Count - 1) currentIndex++;
            SetState(ViewState.Listing);
        }

        private void firstBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex = 0;
            SetState(ViewState.Listing);
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            if (currentIndex < categorias.Count - 1) currentIndex = categorias.Count - 1;
            SetState(ViewState.Listing);
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
            editItem = categorias[currentIndex];
            SetState(ViewState.Editing);
        }


        private void saveBtn_Click(object sender, EventArgs e)
        {
            List<string> errors = ValidateForm();
            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), "Erros", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CategoriaRepository categoriaRepository = new CategoriaRepository();

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
            MessageBox.Show("Categoria salvo com sucesso!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (categorias.Count == 0)
            {
                return;
            }

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            categoriaRepository.DeleteCategoria(categorias[currentIndex].Id);

            ReadCategorias();

            if (categorias.Count > 0)
            {
                if (currentIndex > categorias.Count - 1) currentIndex--;
            }
            else currentIndex = 0;

            editItem = null;
            SetState(ViewState.Listing);
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

                if (result == DialogResult.Cancel)
                    return; // usuário cancelou — volta para o formulário
            }
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            SwitchToTabByName("tabPageLista");
            searchBox.Focus();
        }

        private void SwitchToTabByName(string tabName)
        {
            if (string.IsNullOrEmpty(tabName)) return;
            var page = tabControl.TabPages.Cast<TabPage>().FirstOrDefault(t => t.Name == tabName);
            if (page != null) tabControl.SelectedTab = page;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Categoria> categoriasEncontrados = categorias;
            if (searchBox.Text.Trim().Length > 0)
            {
                string searchStr = searchBox.Text.Trim().ToLower();

                if (int.TryParse(searchStr, out int id))
                {
                    categoriasEncontrados = categorias.FindAll(p => p.Id == id);
                }
                else
                {
                    categoriasEncontrados = categorias.FindAll(p => p.Nome.ToLower().Contains(searchStr));
                }
            }
            if (categoriasEncontrados.Count > 0)
                currentIndex = categorias.FindIndex(p => p.Id == categoriasEncontrados[0].Id);
            listTable.DataSource = categoriasEncontrados;
            SetState(ViewState.Listing);
        }

        private void listTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var cur = this.listTable.CurrentRow;
            if (cur != null)
                currentIndex = cur.Index;

            SetState(ViewState.Listing);
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (state != ViewState.Listing)
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
}