using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NUEVA_IT201WM_ASSIGNMENT1_MIDTERM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Closes the application when the exit button is clicked.
        private void exit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Clears all text input fields across all group boxes and panels to reset the form.
        private void clear_btn_Click(object sender, EventArgs e)
        {
            foreach (GroupBox parentContainer in this.Controls.OfType<GroupBox>())
            {
                foreach (GroupBox childContainer in parentContainer.Controls.OfType<GroupBox>())
                {
                    foreach (TextBox textBox in childContainer.Controls.OfType<TextBox>())
                    {
                        textBox.Clear();
                    }
                    foreach (Panel panel in childContainer.Controls.OfType<Panel>())
                    {
                        foreach (TextBox textBox in panel.Controls.OfType<TextBox>())
                        {
                            textBox.Clear();
                        }
                    }
                }
            }
        }

        // Validates all inputs, computes the weighted grades for each period, and calculates the overall final grade.
        private void calculate_btn_Click(object sender, EventArgs e)
        {
            List<double> totalGradesList = new List<double>();

            foreach (GroupBox parentContainer in this.Controls.OfType<GroupBox>())
            {
                List<double> periodGradesList = new List<double>();

                foreach (GroupBox childContainer in parentContainer.Controls.OfType<GroupBox>())
                {
                    List<double> gradesList = new List<double>();

                    foreach (Panel panel in childContainer.Controls.OfType<Panel>())
                    {
                        TextBox[] textBox = (panel.Controls.OfType<TextBox>()).ToArray();

                        TextBox txtScore = textBox[2];
                        TextBox txtTotal = textBox[1];
                        TextBox txtGrade = textBox[0];

                        if (txtTotal.Text == "0")
                        {
                            MessageBox.Show("Total cannot be zero.");
                            return;
                        }

                        if (txtScore.Text == "" && txtTotal.Text == "")
                        {
                            continue;
                        }

                        if (txtScore.Text == "" || txtTotal.Text == "")
                        {
                            MessageBox.Show("Score and Total must be filled.");
                            return;
                        }

                        try
                        {
                            double score = double.Parse(txtScore.Text);
                            double total = double.Parse(txtTotal.Text);

                            if (score > total)
                            {
                                MessageBox.Show("Score cannot be greater than Total.");
                                return;
                            }
                            else if (score < 0 || total < 0)
                            {
                                MessageBox.Show("Values must not be negative.");
                                return;
                            }

                            double computedGrade = Math.Round((score / total) * 60 + 40, 2);
                            gradesList.Add(computedGrade);
                            txtGrade.Text = $"{computedGrade}%";
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Please enter valid numbers.");
                            return;
                        }
                        catch (DivideByZeroException)
                        {
                            MessageBox.Show("Total score cannot be zero.");
                            return;
                        }
                    }

                    if (gradesList.Count > 0)
                    {
                        double averageOfActivity = Math.Round(gradesList.Average(), 2);

                        TextBox txtTotalOfActivity = childContainer.Controls.OfType<TextBox>().ToArray()[0];
                        double.TryParse($"{txtTotalOfActivity.Tag}", out double formula);

                        double weightedCategoryGrade = Math.Round(averageOfActivity * formula, 2);
                        periodGradesList.Add(weightedCategoryGrade);
                        txtTotalOfActivity.Text = $"{weightedCategoryGrade}%";
                    }
                }

                double sumOfPeriodGrades = Math.Round(periodGradesList.Sum(), 2);
                TextBox txtTotalPeriodGrades = parentContainer.Controls.OfType<TextBox>().ToArray()[0];

                totalGradesList.Add(sumOfPeriodGrades);
                txtTotalPeriodGrades.Text = $"{sumOfPeriodGrades}%";
            }

            txtFinalGrade.Text = $"{Math.Round(totalGradesList.Average(), 2)}%";
        }

        // Populates the empty input fields with mock data for rapid testing purposes.
        private void generateMockData_btn_Click(object sender, EventArgs e)
        {
            foreach (GroupBox parentContainer in this.Controls.OfType<GroupBox>())
            {
                foreach (GroupBox childContainer in parentContainer.Controls.OfType<GroupBox>())
                {
                    foreach (TextBox textBox in childContainer.Controls.OfType<TextBox>())
                    {
                    }
                    foreach (Panel panel in childContainer.Controls.OfType<Panel>())
                    {
                        foreach (TextBox textBox in panel.Controls.OfType<TextBox>())
                        {
                            textBox.Text = "30";
                        }
                    }
                }
            }
        }
    }
}