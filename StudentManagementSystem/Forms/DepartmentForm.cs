using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using StudentManagementSystem.DataAccess;

namespace StudentManagementSystem.Forms
{
    public partial class DepartmentForm : Form
    {
        private DepartmentDAO departmentDAO;
        private int? selectedDepartmentID = null;

        private DataGridView dgvDepartments;
        private TextBox txtName, txtCode;
        private Button btnAdd, btnUpdate, btnDelete, btnClear;

        public DepartmentForm()
        {
            departmentDAO = new DepartmentDAO();
            InitializeComponent();
            LoadDepartments();
        }

        private void InitializeComponent()
        {
            this.Text = "Department Management";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            Panel leftPanel = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(300, 420),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(leftPanel);

            int y = 20;

            Label lblTitle = new Label
            {
                Text = "Department Information",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(40, y),
                Size = new Size(220, 25)
            };
            leftPanel.Controls.Add(lblTitle);
            y += 50;

            leftPanel.Controls.Add(CreateLabel("Department Name:", 20, y));
            txtName = CreateTextBox(20, y + 25);
            leftPanel.Controls.Add(txtName);
            y += 70;

            leftPanel.Controls.Add(CreateLabel("Department Code:", 20, y));
            txtCode = CreateTextBox(20, y + 25);
            leftPanel.Controls.Add(txtCode);
            y += 70;

            btnAdd = CreateButton("Add", 20, y, Color.FromArgb(40, 167, 69));
            btnAdd.Click += BtnAdd_Click;
            leftPanel.Controls.Add(btnAdd);

            btnUpdate = CreateButton("Update", 95, y, Color.FromArgb(0, 123, 255));
            btnUpdate.Click += BtnUpdate_Click;
            leftPanel.Controls.Add(btnUpdate);

            btnDelete = CreateButton("Delete", 170, y, Color.FromArgb(220, 53, 69));
            btnDelete.Click += BtnDelete_Click;
            leftPanel.Controls.Add(btnDelete);

            btnClear = CreateButton("Clear", 245, y, Color.FromArgb(108, 117, 125));
            btnClear.Click += BtnClear_Click;
            leftPanel.Controls.Add(btnClear);

            // Right Panel
            Panel rightPanel = new Panel
            {
                Location = new Point(340, 20),
                Size = new Size(420, 420),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(rightPanel);

            dgvDepartments = new DataGridView
            {
                Location = new Point(10, 10),
                Size = new Size(400, 390),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            dgvDepartments.CellClick += DgvDepartments_CellClick;
            rightPanel.Controls.Add(dgvDepartments);
        }

        private Label CreateLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(x, y),
                Size = new Size(200, 20)
            };
        }

        private TextBox CreateTextBox(int x, int y)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(250, 25),
                Font = new Font("Segoe UI", 10)
            };
        }

        private Button CreateButton(string text, int x, int y, Color color)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(65, 35),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
        }

        private void LoadDepartments()
        {
            dgvDepartments.DataSource = departmentDAO.GetAllDepartments();

            if (dgvDepartments.Columns.Contains("DepartmentID"))
                dgvDepartments.Columns["DepartmentID"].HeaderText = "ID";
            if (dgvDepartments.Columns.Contains("DepartmentName"))
                dgvDepartments.Columns["DepartmentName"].HeaderText = "Department Name";
            if (dgvDepartments.Columns.Contains("DepartmentCode"))
                dgvDepartments.Columns["DepartmentCode"].HeaderText = "Code";
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            departmentDAO.AddDepartment(txtName.Text.Trim(), txtCode.Text.Trim());
            LoadDepartments();
            ClearForm();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedDepartmentID == null)
            {
                MessageBox.Show("Please select a department first.", "Warning");
                return;
            }

            if (!ValidateInput()) return;

            if (departmentDAO.UpdateDepartment(
                selectedDepartmentID.Value,
                txtName.Text.Trim(),
                txtCode.Text.Trim()))
            {
                MessageBox.Show("Department updated successfully.", "Success");
                LoadDepartments();
                ClearForm();
            }
        }


        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedDepartmentID == null)
            {
                MessageBox.Show("Please select a department first.", "Warning");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this department?",
                "Delete Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (departmentDAO.DeleteDepartment(selectedDepartmentID.Value))
                {
                    MessageBox.Show("Department deleted successfully.", "Success");
                    LoadDepartments();
                    ClearForm();
                }
            }
        }


        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void DgvDepartments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDepartments.Rows[e.RowIndex];
                selectedDepartmentID = Convert.ToInt32(row.Cells["DepartmentID"].Value);

                txtName.Text = row.Cells["DepartmentName"].Value.ToString();
                txtCode.Text = row.Cells["DepartmentCode"].Value.ToString();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter department name.");
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Please enter department code.");
                txtCode.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            selectedDepartmentID = null;
            txtName.Clear();
            txtCode.Clear();
            txtName.Focus();
        }
    }
}
