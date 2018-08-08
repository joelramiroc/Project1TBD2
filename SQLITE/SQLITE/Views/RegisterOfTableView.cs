using System.Data;
using System.Windows.Forms;

namespace SQLITE.Views
{
    public partial class RegisterOfTableView : Form
    {
        public string TableName { get; set; }

        public DataSet DataSet { get; set; }

        public RegisterOfTableView(string tableName,DataSet dataSet)
        {
            InitializeComponent();
            this.TableName = tableName;
            this.DataSet = dataSet;
            this.LoadData();
            this.dataGridView1.RowsAdded += DataGridView1_RowsAdded;
            this.dataGridView1.RowErrorTextChanged += DataGridView1_RowErrorTextChanged;
            this.dataGridView1.RowErrorTextNeeded += DataGridView1_RowErrorTextNeeded;
            this.dataGridView1.RowsRemoved += DataGridView1_RowsRemoved;
            this.dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;
        }

        private void DataGridView1_CurrentCellChanged(object sender, System.EventArgs e)
        {
            string myString = "CurrentCellChanged event raised, cell focus is at ";
            string myPoint = dataGridView1.CurrentCell.ColumnIndex + "," +
                           dataGridView1.CurrentCell.RowIndex;
            myString = myString + "(" + myPoint + ")";
            MessageBox.Show(myString, "Current cell co-ordinates");
        }

        private void DataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            var row = e.RowIndex;
        }

        private void DataGridView1_RowErrorTextNeeded(object sender, DataGridViewRowErrorTextNeededEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void DataGridView1_RowErrorTextChanged(object sender, DataGridViewRowEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            var row = e.RowIndex;
        }

        public async void LoadData()
        {
            this.dataGridView1.DataSource = this.DataSet.Tables[0];
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            string caption = "Advertence";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show("Are you sure", caption, buttons);
            if (result == DialogResult.Yes)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void dataGridView1_AllowUserToDeleteRowsChanged(object sender, System.EventArgs e)
        {
            var t = e;
        }
    }
}
