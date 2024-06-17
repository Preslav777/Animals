using System;
using System.Data;
using System.Windows.Forms;
using AnimalsProject.Controllers;

namespace AnimalsProject
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Animals;Integrated Security=true";
        private int selectedAnimalId = -1;
        private AnimalsLogic animalsLogic;
        private BreedsLogic breedsLogic;

        public Form1()
        {
            InitializeComponent();
            itemsListBox.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            animalsLogic = new AnimalsLogic(connectionString);
            breedsLogic = new BreedsLogic(connectionString);

            this.Load += Form1_Load;
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemsListBox.SelectedIndex != -1 && itemsListBox.SelectedIndex != 0)
            {
                string selectedItem = itemsListBox.SelectedItem.ToString();
                string[] parts = selectedItem.Split('/');
                string idPart = parts[0].Trim();

                if (int.TryParse(idPart, out int id))
                {
                    selectedAnimalId = id;
                }
                else
                {
                    selectedAnimalId = -1;
                }
            }
            else
            {
                selectedAnimalId = -1;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable animalsTable = animalsLogic.GetAllAnimals();
                itemsListBox.Items.Clear();

                itemsListBox.Items.Add("Id / Име / Описание / Възраст / Порода");

                foreach (DataRow row in animalsTable.Rows)
                {
                    int breedId = (int)row["BreedId"];
                    string breedName = breedsLogic.GetBreedNameById(breedId);

                    string item = $"{row["Id"]} / {row["Name"]} / {row["Description"]} / {row["Age"]} / {breedName}";
                    itemsListBox.Items.Add(item);
                }

                DataTable breedsTable = breedsLogic.GetAllBreeds();
                breedComboBox.DisplayMember = "Name";
                breedComboBox.ValueMember = "Id";
                breedComboBox.DataSource = breedsTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проблем с базата данни: " + ex.Message);
            }
        }

        private void addButton_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || string.IsNullOrWhiteSpace(descriptionTextBox.Text) || string.IsNullOrWhiteSpace(ageTextBox.Text))
            {
                MessageBox.Show("Всички полета трябва да са запълнени");
                return;
            }

            if (!float.TryParse(ageTextBox.Text, out float animalAge))
            {
                MessageBox.Show("Възрастта трябва да е число");
                return;
            }

            try
            {
                string animalName = nameTextBox.Text;
                string animalDescription = descriptionTextBox.Text;
                int selectedBreedId = (int)breedComboBox.SelectedValue;
                animalsLogic.AddAnimal(animalName, animalDescription, animalAge, selectedBreedId);

                nameTextBox.Clear();
                descriptionTextBox.Clear();
                ageTextBox.Clear();
                breedComboBox.SelectedIndex = 0;

                Form1_Load(sender, e);
                MessageBox.Show("Животното е добавено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проблем с базата данни: " + ex.Message);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (selectedAnimalId == -1)
            {
                MessageBox.Show("Избери животно за изтриване");
                return;
            }

            try
            {
                animalsLogic.DeleteAnimal(selectedAnimalId);
                Form1_Load(sender, e);
                selectedAnimalId = -1;
                MessageBox.Show("Животното е изтрито!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проблем с базата данни: " + ex.Message);
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (selectedAnimalId == -1 || itemsListBox.SelectedIndex == -1 || itemsListBox.SelectedIndex == 0)
            {
                MessageBox.Show("Избери животно за обновяване");
                return;
            }

            try
            {
                int selectedIndex = itemsListBox.SelectedIndex == 0 ? 1 : itemsListBox.SelectedIndex;
                string selectedItem = itemsListBox.Items[selectedIndex].ToString();
                string[] parts = selectedItem.Split('/');

                string oldName = parts[1].Trim();
                string oldDescription = parts[2].Trim();
                float oldAge = float.Parse(parts[3].Trim());

                string animalName = string.IsNullOrWhiteSpace(nameTextBox.Text) ? oldName : nameTextBox.Text;
                string animalDescription = string.IsNullOrWhiteSpace(descriptionTextBox.Text) ? oldDescription : descriptionTextBox.Text;

                float animalAgeInput = oldAge;
                if (!string.IsNullOrWhiteSpace(ageTextBox.Text))
                {
                    if (!float.TryParse(ageTextBox.Text, out animalAgeInput))
                    {
                        MessageBox.Show("Възрастта трябва да е число");
                        return;
                    }
                }
                int selectedBreedId = (int)breedComboBox.SelectedValue;

                animalsLogic.UpdateAnimal(selectedAnimalId, animalName, animalDescription, animalAgeInput, selectedBreedId);

                nameTextBox.Clear();
                descriptionTextBox.Clear();
                ageTextBox.Clear();
                selectedAnimalId = -1;

                Form1_Load(sender, e);
                MessageBox.Show("Животното е обновено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проблем с базата данни: " + ex.Message);
            }
        }
    }
}
