using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BL.Student;
using EL.Student;

namespace PL.Student
{
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
            radioGender1.Checked = true;
            txtId.Enabled = false;
        }

        /// <summary>
        /// Save a student Details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveStudentDetails_Click(object sender, EventArgs e)
        {
            StudentEntity student = new StudentEntity();
            if (txtName.Text != string.Empty)
            {
                student.Name = txtName.Text;
                if (txtAge.Text != string.Empty)
                {
                    student.Age = Convert.ToInt32(txtAge.Text);
                    student.Gender = radioGender1.Checked ? 'M' : 'F';
                    CheckStudentValidations studentValidations = new CheckStudentValidations();
                    if (studentValidations.SaveStudent(student) > 0)
                    {
                        MessageBox.Show("Student Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        txtId.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Error Occured while save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtId.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Fill Age!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtId.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Fill Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtId.Enabled = false;
            }
        }
        
        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            if (txtAge.Text != string.Empty)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtAge.Text, @"^[0-9]+$"))
                {
                    MessageBox.Show("This textbox accepts only numeric characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAge.Text = string.Empty;
                }
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text != string.Empty)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtName.Text, @"^[a-zA-Z_ ]+$"))
                {
                    MessageBox.Show("This textbox accepts only alphabetical characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtName.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Closes the current application window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// To search a student record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            StudentEntity student = new StudentEntity();
            if (txtId.Text != string.Empty)
            {
                student.Id = Convert.ToInt32(txtId.Text);
                CheckStudentValidations studentValidations = new CheckStudentValidations();
                StudentEntity studentResult = new StudentEntity();
                studentResult = studentValidations.SearchStudentById(student);
                if (studentResult != null)
                {
                    txtName.Text = studentResult.Name;
                    txtAge.Text = studentResult.Age.ToString();
                    if(studentResult.Gender == 'M' || studentResult.Gender == 'm')
                    {
                        radioGender1.Checked = true;
                    }
                    else
                    {
                        radioGender2.Checked = true;
                    }
                    txtId.Enabled = false;
                }
                else
                {
                    MessageBox.Show("No student with this id exists", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    txtId.Enabled = false;
                }
            }

            else if(txtName.Text != string.Empty)
            {
                student.Name = txtName.Text;
                CheckStudentValidations studentValidations = new CheckStudentValidations();
                StudentEntity studentResult = new StudentEntity();
                studentResult = studentValidations.SearchStudentByName(student);
                if (studentResult != null)
                {
                    if (studentResult.IsMultipleRecordExist)
                    {
                        txtId.Text = studentResult.Id.ToString();
                        txtName.Text = studentResult.Name;
                        txtAge.Text = studentResult.Age.ToString();
                        if (studentResult.Gender == 'M' || studentResult.Gender == 'm')
                        {
                            radioGender1.Checked = true;
                        }
                        else
                        {
                            radioGender2.Checked = true;
                        }
                        MessageBox.Show("Multiple Student Exists with name : " + studentResult.Name+ ". If this is not the record you're looking for kindly search by student ID.", "Multiple Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtId.Enabled = true;
                    }
                    else
                    {
                        txtId.Text = studentResult.Id.ToString();
                        txtName.Text = studentResult.Name;
                        txtAge.Text = studentResult.Age.ToString();
                        if (studentResult.Gender == 'M' || studentResult.Gender == 'm')
                        {
                            radioGender1.Checked = true;
                        }
                        else
                        {
                            radioGender2.Checked = true;
                        }
                        txtId.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("No student with this Name exists", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    txtId.Enabled = false;
                }
            }

            else
            {
                MessageBox.Show("Fill Name to search!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Updates a student record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            StudentEntity student = new StudentEntity();
            if(txtId.Text != string.Empty)
            {
                student.Id = Convert.ToInt32(txtId.Text);
                if (txtName.Text != string.Empty)
                {
                    student.Name = txtName.Text;
                    if (txtAge.Text != string.Empty)
                    {
                        student.Age = Convert.ToInt32(txtAge.Text);
                        student.Gender = radioGender1.Checked ? 'M' : 'F';
                        CheckStudentValidations studentValidations = new CheckStudentValidations();
                        if (studentValidations.UpdateStudent(student) != 0)
                        {
                            MessageBox.Show("Student Data updated Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            ClearFields();
                            txtId.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Error Occured while save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Fill Age!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtId.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Fill Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtId.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please first search the student you want to update", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtId.Enabled = false;
            }
        }

        /// <summary>
        /// Deletes a student record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            StudentEntity student = new StudentEntity();
            if (txtId.Text != string.Empty)
            {
                student.Id = Convert.ToInt32(txtId.Text);
                CheckStudentValidations studentValidations = new CheckStudentValidations();
                if (studentValidations.DeleteStudent(student) != 0)
                {
                    MessageBox.Show("Student Data deleted Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ClearFields();
                    txtId.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Error Occured while save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please search the student you want to delete", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtId.Enabled = false;
            }
        }

        /// <summary>
        /// Clear All Fields.
        /// </summary>
        public void ClearFields()
        {
            txtName.Clear();
            txtId.Clear();
            txtAge.Clear();
            radioGender1.Checked = true;
        }

        /// <summary>
        /// Clear all fields.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            if (txtId.Text != string.Empty)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtId.Text, @"^[0-9]+$"))
                {
                    MessageBox.Show("This textbox accepts only numeric characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearFields();
                }
            }
        }
    }
}
