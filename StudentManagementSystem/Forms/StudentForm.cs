using StudentManagementSystem.DataAccess;
using StudentManagementSystem.Models;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace StudentManagementSystem.Forms
{
    public partial class StudentForm : Form
    {
        private StudentDAO studentDAO;
        private DepartmentDAO departmentDAO;
        private int? selectedStudentID = null;

        // Controls
        private DataGridView dgvStudents;
        private TextBox txtFirstName, txtLastName, txtEmail, txtPhone, txtAddress, txtSearch;
        private DateTimePicker dtpDateOfBirth;
        private ComboBox cmbDepartment;
        private Button btnAdd, btnUpdate, btnDelete, btnClear, btnSearch;

        public StudentForm()
        {
            studentDAO = new StudentDAO();
            departmentDAO = new DepartmentDAO();
            InitializeComponent();
            LoadStudents();
            LoadDepartments();
        }

        private void InitializeComponent()
        {
            this.Text = "Student Management";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Left Panel - Form
            Panel leftPanel = new Panel();
            leftPanel.Location = new Point(20, 20);
            leftPanel.Size = new Size(350, 620);
            leftPanel.BackColor = Color.White;
            leftPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(leftPanel);

            int yPos = 20;

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "Student Information";
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitle.Location = new Point(80, yPos);
            lblTitle.Size = new Size(200, 30);
            leftPanel.Controls.Add(lblTitle);
            yPos += 50;

            // First Name
            leftPanel.Controls.Add(CreateLabel("First Name:", 20, yPos));
            txtFirstName = CreateTextBox(20, yPos + 25);
            leftPanel.Controls.Add(txtFirstName);
            yPos += 70;

            // Last Name
            leftPanel.Controls.Add(CreateLabel("Last Name:", 20, yPos));
            txtLastName = CreateTextBox(20, yPos + 25);
            leftPanel.Controls.Add(txtLastName);
            yPos += 70;

            // Date of Birth
            leftPanel.Controls.Add(CreateLabel("Date of Birth:", 20, yPos));
            dtpDateOfBirth = new DateTimePicker();
            dtpDateOfBirth.Location = new Point(20, yPos + 25);
            dtpDateOfBirth.Size = new Size(310, 25);
            dtpDateOfBirth.Format = DateTimePickerFormat.Short;
            dtpDateOfBirth.Value = new DateTime(2000, 1, 1);
            leftPanel.Controls.Add(dtpDateOfBirth);
            yPos += 70;

            // Email
            leftPanel.Controls.Add(CreateLabel("Email:", 20, yPos));
            txtEmail = CreateTextBox(20, yPos + 25);
            leftPanel.Controls.Add(txtEmail);
            yPos += 70;

            // Phone
            leftPanel.Controls.Add(CreateLabel("Phone:", 20, yPos));
            txtPhone = CreateTextBox(20, yPos + 25);
            leftPanel.Controls.Add(txtPhone);
            yPos += 70;

            // Address
            leftPanel.Controls.Add(CreateLabel("Address:", 20, yPos));
            txtAddress = CreateTextBox(20, yPos + 25);
            txtAddress.Multiline = true;
            txtAddress.Height = 50;
            leftPanel.Controls.Add(txtAddress);
            yPos += 85;

            // Department
            leftPanel.Controls.Add(CreateLabel("Department:", 20, yPos));
            cmbDepartment = new ComboBox();
            cmbDepartment.Location = new Point(20, yPos + 25);
            cmbDepartment.Size = new Size(310, 25);
            cmbDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            leftPanel.Controls.Add(cmbDepartment);
            yPos += 70;

            // Buttons
            btnAdd = CreateButton("Add", 20, yPos, Color.FromArgb(40, 167, 69));
            btnAdd.Click += BtnAdd_Click;
            leftPanel.Controls.Add(btnAdd);

            btnUpdate = CreateButton("Update", 95, yPos, Color.FromArgb(0, 123, 255));
            btnUpdate.Click += BtnUpdate_Click;
            leftPanel.Controls.Add(btnUpdate);

            btnDelete = CreateButton("Delete", 170, yPos, Color.FromArgb(220, 53, 69));
            btnDelete.Click += BtnDelete_Click;
            leftPanel.Controls.Add(btnDelete);

            btnClear = CreateButton("Clear", 245, yPos, Color.FromArgb(108, 117, 125));
            btnClear.Click += BtnClear_Click;
            leftPanel.Controls.Add(btnClear);

            // Right Panel - Table
            Panel rightPanel = new Panel();
            rightPanel.Location = new Point(390, 20);
            rightPanel.Size = new Size(780, 620);
            rightPanel.BackColor = Color.White;
            rightPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(rightPanel);

            // Search Bar
            Label lblSearch = new Label();
            lblSearch.Text = "Search:";
            lblSearch.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblSearch.Location = new Point(650, 15);
            lblSearch.Size = new Size(60, 25);
            rightPanel.Controls.Add(lblSearch);

            txtSearch = new TextBox();
            txtSearch.Location = new Point(340, 15);
            txtSearch.Size = new Size(300, 25);
            txtSearch.Font = new Font("Segoe UI", 10);
            rightPanel.Controls.Add(txtSearch);

            btnSearch = CreateButton("Search", 250, 13, Color.FromArgb(23, 162, 184));
            btnSearch.Width = 80;
            btnSearch.Click += BtnSearch_Click;
            rightPanel.Controls.Add(btnSearch);

            // DataGridView
            dgvStudents = new DataGridView();
            dgvStudents.Location = new Point(10, 50);
            dgvStudents.Size = new Size(760, 555);
            dgvStudents.BackgroundColor = Color.White;
            dgvStudents.BorderStyle = BorderStyle.None;
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.ReadOnly = true;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.MultiSelect = false;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.RowHeadersVisible = false;
            dgvStudents.CellClick += DgvStudents_CellClick;
            rightPanel.Controls.Add(dgvStudents);
        }

        private Label CreateLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(x, y),
                Size = new Size(150, 20)
            };
        }

        private TextBox CreateTextBox(int x, int y)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(310, 25),
                Font = new Font("Segoe UI", 10)
            };
        }

        private Button CreateButton(string text, int x, int y, Color color)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(70, 35),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private void LoadStudents()
        {
            try
            {
                dgvStudents.DataSource = studentDAO.GetAllStudents();

                if (dgvStudents.Columns.Contains("StudentID"))
                    dgvStudents.Columns["StudentID"].HeaderText = "Student ID";
                if (dgvStudents.Columns.Contains("FirstName"))
                    dgvStudents.Columns["FirstName"].HeaderText = "First Name";
                if (dgvStudents.Columns.Contains("LastName"))
                    dgvStudents.Columns["LastName"].HeaderText = "Last Name";
                if (dgvStudents.Columns.Contains("DateOfBirth"))
                    dgvStudents.Columns["DateOfBirth"].HeaderText = "Date of Birth";
                if (dgvStudents.Columns.Contains("Email"))
                    dgvStudents.Columns["Email"].HeaderText = "Email";
                if (dgvStudents.Columns.Contains("Phone"))
                    dgvStudents.Columns["Phone"].HeaderText = "Phone";
                if (dgvStudents.Columns.Contains("DepartmentName"))
                    dgvStudents.Columns["DepartmentName"].HeaderText = "Department";
                if (dgvStudents.Columns.Contains("Address"))
                    dgvStudents.Columns["Address"].Visible = false;
                if (dgvStudents.Columns.Contains("EnrollmentDate"))
                    dgvStudents.Columns["EnrollmentDate"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDepartments()
        {
            try
            {
                DataTable dt = departmentDAO.GetAllDepartments();
                cmbDepartment.DataSource = dt;
                cmbDepartment.DisplayMember = "DepartmentName";
                cmbDepartment.ValueMember = "DepartmentID";
                cmbDepartment.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading departments: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                Student student = new Student
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    DateOfBirth = dtpDateOfBirth.Value,
                    Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                    Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim(),
                    Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
                    DepartmentID = cmbDepartment.SelectedIndex >= 0 ? (int?)cmbDepartment.SelectedValue : null
                };

                if (studentDAO.AddStudent(student))
                {
                    MessageBox.Show("Student added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStudents();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding student: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedStudentID == null)
            {
                MessageBox.Show("Please select a student first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput()) return;

            try
            {
                Student student = new Student
                {
                    StudentID = selectedStudentID.Value,
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    DateOfBirth = dtpDateOfBirth.Value,
                    Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                    Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim(),
                    Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
                    DepartmentID = cmbDepartment.SelectedIndex >= 0 ? (int?)cmbDepartment.SelectedValue : null
                };

                if (studentDAO.UpdateStudent(student))
                {
                    MessageBox.Show("Student updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStudents();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating student: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedStudentID == null)
            {
                MessageBox.Show("Please select a student first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this student?",
                "Delete Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (studentDAO.DeleteStudent(selectedStudentID.Value))
                    {
                        MessageBox.Show("Student deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadStudents();
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting student: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dgvStudents.DataSource = string.IsNullOrWhiteSpace(txtSearch.Text)
                    ? studentDAO.GetAllStudents()
                    : studentDAO.SearchStudents(txtSearch.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching students: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStudents.Rows[e.RowIndex];
                selectedStudentID = Convert.ToInt32(row.Cells["StudentID"].Value);

                txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
                txtLastName.Text = row.Cells["LastName"].Value.ToString();
                dtpDateOfBirth.Value = Convert.ToDateTime(row.Cells["DateOfBirth"].Value);
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtPhone.Text = row.Cells["Phone"].Value?.ToString() ?? "";

                Student student = studentDAO.GetStudentByID(selectedStudentID.Value);
                if (student != null)
                {
                    txtAddress.Text = student.Address ?? "";
                    cmbDepartment.SelectedValue = student.DepartmentID ?? -1;
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter the first name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter the last name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            selectedStudentID = null;
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            dtpDateOfBirth.Value = new DateTime(2000, 1, 1);
            cmbDepartment.SelectedIndex = -1;
            txtFirstName.Focus();
        }
    }
}
