using System;
using System.Drawing;
using System.Windows.Forms;
using StudentManagementSystem.DataAccess;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            TestDatabaseConnection();
        }

        private void InitializeComponent()
        {
            this.Text = "Student Management System - Main Page";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Welcome Label
            Label lblWelcome = new Label();
            lblWelcome.Text = "Welcome to the Student Management System";
            lblWelcome.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblWelcome.Location = new Point(200, 50);
            lblWelcome.Size = new Size(400, 40);
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblWelcome);

            // Buttons Panel
            Panel buttonPanel = new Panel();
            buttonPanel.Location = new Point(150, 150);
            buttonPanel.Size = new Size(500, 350);
            buttonPanel.BackColor = Color.White;
            this.Controls.Add(buttonPanel);

            // Manage Students Button
            Button btnStudents = CreateMenuButton("Manage Students", 50);
            btnStudents.Click += BtnStudents_Click;
            buttonPanel.Controls.Add(btnStudents);

            // Manage Courses Button
            Button btnCourses = CreateMenuButton("Manage Courses", 120);
            btnCourses.Click += BtnCourses_Click;
            buttonPanel.Controls.Add(btnCourses);

            // Manage Departments Button
            Button btnDepartments = CreateMenuButton("Manage Departments", 190);
            btnDepartments.Click += BtnDepartments_Click;
            buttonPanel.Controls.Add(btnDepartments);


            // Enrollments Button
            Button btnEnrollments = CreateMenuButton("Enrollments", 190);
            btnEnrollments.Click += BtnEnrollments_Click;
            buttonPanel.Controls.Add(btnEnrollments);

            // Exit Button
            Button btnExit = CreateMenuButton("Exit", 260);
            btnExit.BackColor = Color.FromArgb(220, 53, 69);
            btnExit.Click += BtnExit_Click;
            buttonPanel.Controls.Add(btnExit);

            // Database Status Label
            Label lblStatus = new Label();
            lblStatus.Name = "lblStatus";
            lblStatus.Text = "Checking database connection...";
            lblStatus.Font = new Font("Segoe UI", 10);
            lblStatus.Location = new Point(250, 520);
            lblStatus.Size = new Size(300, 25);
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblStatus);
        }

        private Button CreateMenuButton(string text, int yPosition)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btn.Size = new Size(400, 50);
            btn.Location = new Point(50, yPosition);
            btn.BackColor = Color.FromArgb(0, 123, 255);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        private void TestDatabaseConnection()
        {
            Label lblStatus = this.Controls["lblStatus"] as Label;
            if (DatabaseConnection.TestConnection())
            {
                lblStatus.Text = "✓ Database connection successful";
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                lblStatus.Text = "✗ Database connection failed";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show(
                    "Please make sure SQL Server is running and the connection string is correct.",
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BtnStudents_Click(object sender, EventArgs e)
        {
            StudentForm studentForm = new StudentForm();
            studentForm.ShowDialog();
        }

        private void BtnCourses_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Course management screen is under development.",
                "Coming Soon",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void BtnDepartments_Click(object sender, EventArgs e)
        {
            DepartmentForm departmentForm = new DepartmentForm();
            departmentForm.ShowDialog();
        }


        private void BtnEnrollments_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Enrollment screen is under development.",
                "Coming Soon",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to exit?",
                "Exit Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
