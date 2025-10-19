using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace WildlifeTracker
{
    public partial class MainForm : Form
    {
        private List<Species> speciesList = new List<Species>();

        public MainForm()
        {
            InitializeComponent();
            LoadSpeciesData();
            logSightingButton.Click += LogSightingButton_Click;
        }

        private void LoadSpeciesData()
        {
            try
            {
                string json = File.ReadAllText("species.json");
                speciesList = JsonSerializer.Deserialize<List<Species>>(json);

                if (speciesList != null)
                {
                    speciesListBox.DataSource = speciesList;
                    speciesListBox.DisplayMember = "Name";
                }
                else
                {
                    MessageBox.Show("No species data found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading species data: " + ex.Message);
            }
        }

        private void LogSightingButton_Click(object sender, EventArgs e)
        {
            if (speciesListBox.SelectedItem is Species selected)
            {
                string location = locationTextBox.Text.Trim();
                if (string.IsNullOrEmpty(location))
                {
                    MessageBox.Show("Please enter a location.");
                    return;
                }

                string log = $"{DateTime.Now}: {selected.Name} sighted at {location}";
                File.AppendAllText("sightings.txt", log + Environment.NewLine);
                MessageBox.Show("Sighting logged successfully!");
                locationTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please select a species.");
            }
        }
    }
}
