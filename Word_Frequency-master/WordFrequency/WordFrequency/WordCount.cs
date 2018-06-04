using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace WordFrequency
{
    public partial class WordCount : Form
    {
        public WordCount()
        {
            InitializeComponent();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = "";
            StreamReader srText;
            StringBuilder cleanText = new StringBuilder();
            string[] wordArray;
            DataTable dtIndex = new DataTable();

            try
            {
                // Add columns to the table. Set the count column to INT and 
                // set the primary key.
                dtIndex.Columns.Add("Word");
                dtIndex.Columns.Add("Count");
                dtIndex.Columns["Count"].DataType = typeof(int);
                dtIndex.PrimaryKey = new DataColumn[] { dtIndex.Columns["Word"] };

                // Get the file and read it into the text box.
                if (ofdFileOpen.ShowDialog() == DialogResult.OK)
                {
                    fileName = ofdFileOpen.FileName;
                    srText = new StreamReader(fileName);
                    txtContent.Text = srText.ReadToEnd();

                    // Clean the punctuation and numbers from the text.
                    foreach (char thisChar in txtContent.Text)
                    {
                        if (char.IsLetter(thisChar) || char.IsWhiteSpace(thisChar))
                            cleanText.Append(thisChar);
                    }

                    txtContent.Text = cleanText.ToString();

                    // Break the text down into words.
                    wordArray = txtContent.Text.Split(' ');

                    // Add the words and their counts to the datatable.
                    foreach (string word in wordArray)
                    {
                        if (dtIndex.Rows.Contains(word))
                        {
                            DataRow drFind = dtIndex.Rows.Find(word);
                            drFind["Count"] = int.Parse(drFind["Count"].ToString()) + 1;
                        }
                        else
                        {
                            dtIndex.Rows.Add(word, 1);
                        }
                    }

                    // Set the datatable as the source for the datagrid.
                    dgIndex.DataSource = dtIndex;
                    dgIndex.Columns["Word"].SortMode = DataGridViewColumnSortMode.Automatic;
                    dgIndex.Columns["Count"].SortMode = DataGridViewColumnSortMode.Automatic;

                    // Fill in stats
                    lblTotalChars.Text = txtContent.Text.Length.ToString();
                    lblTotalUnique.Text = dtIndex.Rows.Count.ToString();
                    lblTotalWords.Text = wordArray.GetLength(0).ToString();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }



  
            }

        private void WordCount_Load(object sender, EventArgs e)
        {

        }
    }
    }
