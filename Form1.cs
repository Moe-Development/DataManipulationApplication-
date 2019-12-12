using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Lab5b
{
 /**************************
 * Author: Mohamad Albazeai
 */

    public partial class Form1 : Form
    {
        //setting the Database connection
        public SqlConnection Connection { get; }
        SqlCommand command;
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=DATABASEMANIPULATION;Integrated Security=True";

        // setting the main lisets
        List<Doctor> doctors;
        List<Companion> companions;
        List<Episode> episodes;
        public Form1()
        {
            InitializeComponent();
            try
            {
                Connection = new SqlConnection();
                Connection.ConnectionString = connectionString;
                Connection.Open();

            }
            catch (Exception ex)
            {
                statusLabel.Text = "Database Connection failed - check Connection String : " + ex.Message;
            }
            listBox.Items.Clear();
            episodes = new List<Episode>();
            doctors = new List<Doctor>();
            companions = new List<Companion>();

           
            //reading the Doctor database
            try
            {
                
                command = new SqlCommand("SELECT * FROM Doctor", Connection);

                // Create new SqlDataReader object and read data from the command.
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    doctors.Add(new Doctor((int)reader["doctorid"], (string)reader["actor"], (int)reader["series"],
                        (int)reader["age"], (string)reader["debut"], (byte[])reader["picture"]));

                }
                reader.Close();
                foreach (Doctor d in doctors)
                {
                    DoctorComboBox.Items.Add(d.DoctorId);
                }
           
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Database operation failed : " + ex.Message;
            }

            //reading the Companion database

            try
            {
                
                command = new SqlCommand("SELECT * FROM Companion", Connection);

                // Create new SqlDataReader object and read data from the command.
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    companions.Add(new Companion((string)reader["name"], (string)reader["actor"], (int)reader["doctorid"], (string)reader["storyid"]));

                }
                reader.Close();

            }
            catch (Exception ex)
            {
                statusLabel.Text = "Database operation failed : " + ex.Message;
            }

            //reading the Episode database:
            try
            {
                
                command = new SqlCommand("SELECT * FROM Episode", Connection);

                // Create new SqlDataReader object and read data from the command.
                SqlDataReader reader = command.ExecuteReader();
                // while there is another record present
                while (reader.Read())
                {
                    episodes.Add(new Episode((string)reader["storyid"], (int)reader["season"], (int)reader["seasonyear"], (string)reader["title"]));

                }
                reader.Close();
       
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Database operation failed : " + ex.Message;
            }
            SqlRadioButton.Checked = true;
            DoctorComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// the Doctor combo box method is reading and spliting the data from the lists and replace the approprate text in its specefide text box.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">eventArgs</param>
        private void DoctorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = DoctorComboBox.SelectedIndex;
            PlayedByTextBox.Text = doctors[index].Actor;
            AgeAtStartTextBox.Text = doctors[index].Age.ToString();
            SeriesTextBox.Text = doctors[index].Series.ToString();
            MemoryStream stream = new MemoryStream(doctors[index].Picture);
            doctorPictureBox.Image = Image.FromStream(stream);
            listBox.Items.Clear();

            // creating query making use of SQL: 
            if (SqlRadioButton.Checked)
            {
                SqlCommand command = new SqlCommand("SELECT seasonyear, title FROM EPISODE WHERE STORYID = @ParamYear", Connection);
                SqlParameter yearSeason = new SqlParameter("@ParamYear", SqlDbType.VarChar);
                yearSeason.Value = doctors[index].Debut;
                command.Parameters.Add(yearSeason);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    YearTextBox.Text = reader["seasonyear"].ToString();
                    FirstFullEpisodeTextBox.Text = reader["title"].ToString();
                }
                reader.Close();


                command = new SqlCommand("SELECT name, actor, seasonyear, title FROM COMPANION c join EPISODE e ON c.STORYID = e.STORYID  WHERE DOCTORID = @paramid", Connection);
                SqlParameter selectedDoctor = new SqlParameter("@paramid", SqlDbType.Int, 4);
                selectedDoctor.Value = doctors[index].DoctorId;
                command.Parameters.Add(selectedDoctor);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listBox.Items.Add(reader["name"] + " (" + reader["actor"].ToString() + ") ");
                    listBox.Items.Add(reader["title"].ToString() + " (" + reader["seasonyear"].ToString() + ")");
                    listBox.Items.Add("");
                }
                reader.Close();
            }

            // creating query making use of Linq: 
            else if (LinqRadioButton.Checked)
            {
                var linqQuery1 = from companion in companions
                             join episode in episodes on companion.StoryID equals episode.StoryID
                             where companion.DoctorID == doctors[index].DoctorId
                             orderby episode.SeasonYear
                             select new { companion.Name, companion.Actor, episode.Title, episode.SeasonYear };
                foreach( var l in linqQuery1)
                {
                    listBox.Items.Add(l.Name + " (" + l.Actor + ")");
                    listBox.Items.Add(l.Title + " (" + l.SeasonYear + ")");
                    listBox.Items.Add("");
                }

                var linqQuery2 = from episode in episodes
                                 where episode.StoryID == doctors[index].Debut
                                 select new { episode.SeasonYear, episode.Title };
                foreach( var l in linqQuery2)
                {
                    YearTextBox.Text = l.SeasonYear.ToString();
                    FirstFullEpisodeTextBox.Text = l.Title;
                }

            }
        }

       
        /// <summary>
        /// this is the Exit button that was replaced in the menu, and its job is to closing the program. 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">eventArgs</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();            //Exit
        }
    }
}
