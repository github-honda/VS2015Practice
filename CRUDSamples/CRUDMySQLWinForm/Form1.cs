using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// add
using System.Globalization;
using MySql.Data.MySqlClient;
using ZLib;

namespace CRUDMySQLWinForm
{
    public partial class Form1 : Form
    {
        string _sConnectionString;
        bool _FormLoad = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _sConnectionString = Properties.Settings.Default.ConnectionString;

            Text = "WinForm CRUD Template for MySQL, v " + Application.ProductVersion;


            Shown += Form1_Shown;
            ImeMode = ImeMode.Off;

            panelInfo.Left = 0;
            panelInfo.Top = 0;

            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.BorderStyle = BorderStyle.Fixed3D;
            splitContainer1.SplitterDistance = panelInfo.Height;
            splitContainer1.SplitterWidth = 1;
            splitContainer1.Panel1MinSize = panelInfo.Height;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Panel1Collapsed = false;
            splitContainer1.Panel2Collapsed = false;

            dataGridView1.CellClick += DataGridView1_CellClick;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AllowDrop = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToResizeColumns = true;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dataGridView1.RowHeadersWidth = 30; // 寬度要能看到error icon!

            cboOrderBy.Items.Add("FParentID, FKey");
            cboOrderBy.Items.Add("FParentID, FSeqNo");
            cboOrderBy.Items.Add("FID");
            cboOrderBy.Items.Add("FUpdateTime DESC");


            myClearQuery();
            lblCount.Text = string.Empty;

            myLoadKeyList();
            EntryClear();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            EntryDelete();

        }
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtFID.Text = string.Empty;
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex < 0)
                return;
            DataGridViewRow row1 = dataGridView1.CurrentRow;
            if (row1 == null)
                return;
            if (row1.Cells[0].Value == DBNull.Value)
                return;

            int iFID = (int)row1.Cells["FID"].Value;
            txtFID.Text = iFID.ToString();
            EntryRead(iFID);
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            _FormLoad = false;
        }
        void EntryClear()
        {
            txtFID.Clear();
            txtFKey.Clear();
            chkReadonly.Checked = false;
            txtFValue.Clear();
            txtFValueB.Clear();
            txtFParentID.Clear();
            txtFSeqNo.Clear();
            txtFNote.Clear();
            lblFCreateTime.Text = string.Empty;
            lblFUpdateTime.Text = string.Empty;
        }
        void myLoadGrid(DataTable t1)
        {
            dataGridView1.DataSource = t1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView1.Columns["FID"].HeaderText = "ID";
            dataGridView1.Columns["FParentID"].HeaderText = "ParentID";
            dataGridView1.Columns["FKey"].HeaderText = "Key";
            dataGridView1.Columns["FValue"].HeaderText = "Value";
            dataGridView1.Columns["FValueB"].HeaderText = "Bilingual Value";
            dataGridView1.Columns["FSeqNo"].HeaderText = "Seq.No.";
            dataGridView1.Columns["FReadOnly"].HeaderText = "Readonly";
            dataGridView1.Columns["FUpdateTime"].HeaderText = "Last Update Time";
            dataGridView1.Columns["FUpdateTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

            lblCount.Text = dataGridView1.RowCount.ToString();
        }
        DataTable DBReadTable_Parents(int iParentID = -1)
        {
            string sCmd;
            try
            {
                using (MySqlConnection cn1 = new MySqlConnection(_sConnectionString))
                {
                    DataSet ds1 = null;
                    MySqlCommand command = cn1.CreateCommand();
                    if (iParentID < 0)
                        sCmd = "select FID, FKey, FValue from TConfig where FParentID = 0 or FParentId is null order by FKey";
                    else
                    {
                        sCmd = "select FID, FKey, FValue from TConfig where FParentID = 0 or FParentId is null AND FID = @ParentID";
                        command.Parameters.AddWithValue("@ParentID", iParentID);
                    }
                    command.CommandText = sCmd;
                    cn1.Open();
                    using (MySqlDataAdapter adapter1 = new MySqlDataAdapter(command))
                    {
                        ds1 = new DataSet();
                        adapter1.Fill(ds1);
                        return ds1.Tables[0];
                    }
                }
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message + _sConnectionString);
            }
            return null;
        }
        Boolean DBExistParentID(int iID)
        {
            if (iID < 0)
                return false;

            DataTable t1 = DBReadTable_Parents(iID);
            if (t1 == null)
                return false;
            if (t1.Rows.Count < 1)
                return false;

            return true;
        }
        Boolean DBExistChild(int iID)
        {
            try
            {
                using (MySqlConnection cn1 = new MySqlConnection(_sConnectionString))
                {
                    DataSet ds1 = null;
                    MySqlCommand command = cn1.CreateCommand();
                    command.Parameters.AddWithValue("@FID", iID);
                    command.CommandText = "select 1 from TConfig where exists (select 1 from TConfig WHERE FParentID = @FID)";
                    cn1.Open();
                    using (MySqlDataAdapter adapter1 = new MySqlDataAdapter(command))
                    {
                        ds1 = new DataSet();
                        adapter1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                            return true;
                    }
                }
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message + _sConnectionString);
            }
            return false;
        }
        Boolean DBExistParentIDAndKey(int iParentID, string sKey)
        {
            try
            {
                using (MySqlConnection cn1 = new MySqlConnection(_sConnectionString))
                {
                    DataSet ds1 = null;
                    MySqlCommand command = cn1.CreateCommand();
                    command.Parameters.AddWithValue("@ParentID", iParentID);
                    command.Parameters.AddWithValue("@Key", sKey);
                    command.CommandText = "select 1 from TConfig where exists (select 1 from TConfig WHERE FParentID = @ParentID and FKey=@Key)";
                    cn1.Open();
                    using (MySqlDataAdapter adapter1 = new MySqlDataAdapter(command))
                    {
                        ds1 = new DataSet();
                        adapter1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                            return true;
                    }
                }
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message + _sConnectionString);
            }
            return false;
        }
        Boolean DBExistIDLastUpdate(int iID, DateTime tUpdate)
        {
            try
            {
                using (MySqlConnection cn1 = new MySqlConnection(_sConnectionString))
                {
                    DataSet ds1 = null;
                    MySqlCommand command = cn1.CreateCommand();
                    command.Parameters.AddWithValue("@FID", iID);
                    command.Parameters.AddWithValue("@FUpdateTime", tUpdate);
                    command.CommandText = "select 1 from TConfig where exists (select 1 from TConfig WHERE FID = @FID and FUpdateTime=@FUpdateTime)";
                    cn1.Open();
                    using (MySqlDataAdapter adapter1 = new MySqlDataAdapter(command))
                    {
                        ds1 = new DataSet();
                        adapter1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                            return true;
                    }
                }
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message + _sConnectionString);
            }
            return false;
        }

        void myLoadKeyList()
        {
            cboKeyList.Items.Clear();
            DataTable t1 = DBReadTable_Parents();
            foreach (DataRow row1 in t1.Rows)
            {
                string s1 = row1["FKey"].ZToString() + ":" + row1["FValue"].ZToString() + ":" + row1["FID"].ZToInt().ZToString();
                cboKeyList.Items.Add(s1);
            }
        }

        private void cboKeyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_FormLoad)
                return;

            if (cboKeyList.SelectedIndex < 0)
                return;

            string s1 = cboKeyList.SelectedItem.ToString();
            string[] sa1 = s1.Split(new char[] { ':' });
            string sParentID = sa1[sa1.Length - 1];
            int iParentID = 0;
            if (!int.TryParse(sParentID, out iParentID))
                return;

            myClearQuery();
            txtParentIDQ.Text = sParentID;
            myLoadGrid(DBReadTable_Grid());

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            myLoadGrid(DBReadTable_Grid());

        }
        void EntryRead(int iID)
        {
            DataRow row1 = DBReadRow_ID(iID);
            if (row1 == null)
            {
                EntryClear();
                MessageBox.Show($"No data found. ID={iID}.");
                return;
            }

            // Fetch to buffers.
            txtFID.Text = row1["FID"].ZToInt().ZToString();
            txtFParentID.Text = row1["FParentID"].ZToInt().ZToString();
            txtFSeqNo.Text = row1["FSeqNo"].ZToInt().ZToString();
            txtFKey.Text = row1["FKey"].ZToString();
            txtFValue.Text = row1["FValue"].ZToString();
            txtFValueB.Text = row1["FValueB"].ZToString();
            chkReadonly.Checked = row1["FReadonly"].ZToInt() == 1;
            txtFNote.Text = row1["FNote"].ZToString();
            lblFCreateTime.Text = row1["FCreateTime"].ZToDateTime().ZToString_MsDash();
            lblFUpdateTime.Text = row1["FUpdateTime"].ZToDateTime().ZToString_MsDash();
        }
        DataRow DBReadRow_ID(int iID)
        {
            try
            {
                using (MySqlConnection cn1 = new MySqlConnection(_sConnectionString))
                using (MySqlCommand command = cn1.CreateCommand())
                {
                    command.CommandText = "Select * from TConfig where FID=@FID";
                    command.Parameters.AddWithValue("@FID", iID);
                    cn1.Open();
                    using (MySqlDataAdapter adapter1 = new MySqlDataAdapter(command))
                    {
                        DataSet ds1 = null;
                        ds1 = new DataSet();
                        adapter1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count < 1)
                            return null;

                        return ds1.Tables[0].Rows[0];
                    }
                }
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message + _sConnectionString);
            }
            return null;
        }

        DataTable DBReadTable_Grid()
        {
            string sParentID = txtParentIDQ.Text;
            string sKey = txtKeyQ.Text;
            string sValue = txtValueQ.Text;
            try
            {
                StringBuilder sb1 = new StringBuilder();
                sb1.Append("Select");
                sb1.Append(" FID, FKey, FValue, FValueB, FParentID, FSeqNo, FReadonly, FUpdateTime");
                sb1.Append(" from TConfig where 1=1");
                using (MySqlConnection cn1 = new MySqlConnection(_sConnectionString))
                using (MySqlCommand command = cn1.CreateCommand())
                {
                    if (!string.IsNullOrEmpty(sParentID))
                    {
                        int iParentID = 0;
                        if (int.TryParse(sParentID, out iParentID))
                        {
                            sb1.Append(" and FParentID = @FParentID");
                            command.Parameters.AddWithValue("@FParentID", iParentID);
                        }
                    }
                    if (!string.IsNullOrEmpty(sKey))
                    {
                        sb1.Append(" and FKey like @FKey");
                        command.Parameters.AddWithValue("@FKey", "%" + sKey + "%");
                    }
                    if (!string.IsNullOrEmpty(sValue))
                    {
                        sb1.Append(" and FValue like @FValue");
                        command.Parameters.AddWithValue("@FValue", "%" + sValue + "%");
                    }
                    sb1.Append(" order by ");
                    if (cboOrderBy.SelectedIndex < 0)
                        cboOrderBy.SelectedIndex = 0;

                    sb1.Append(cboOrderBy.Items[cboOrderBy.SelectedIndex].ToString());
                    command.CommandText = sb1.ToString();

                    cn1.Open();
                    using (MySqlDataAdapter adapter1 = new MySqlDataAdapter(command))
                    {
                        DataSet ds1 = null;
                        ds1 = new DataSet();
                        adapter1.Fill(ds1);
                        return ds1.Tables[0];
                    }
                }
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message + _sConnectionString);
                return null;
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            string sID = txtFID.Text;
            if (string.IsNullOrEmpty(sID))
                return;
            int iID = 0;
            if (!int.TryParse(sID, out iID))
                return;
            EntryRead(iID);

        }

        private void btnClearQ_Click(object sender, EventArgs e)
        {
            cboKeyList.SelectedIndex = -1;
            myClearQuery();

        }
        void myClearQuery()
        {
            //cboOrderBy.SelectedIndex = -1;
            txtParentIDQ.Clear();
            txtKeyQ.Clear();
            txtValueQ.Clear();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            EntryClear();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EntryAdd();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            EntryUpdate();

        }
        void EntryAdd()
        {
            int FParentID;
            int FSeqNo;

            // ID check. 
            if (!string.IsNullOrEmpty(txtFID.Text))
            {
                MessageBox.Show("ID is not empty. ID is an IDENTITY field in DB.");
                return;
            }

            // Fields check.
            if (string.IsNullOrEmpty(txtFKey.Text))
            {
                MessageBox.Show("Key is empty.");
                return;
            }

            FParentID = -1;
            if (string.IsNullOrEmpty(txtFParentID.Text))
            {
                MessageBox.Show("Parent ID is empty.");
                return;
            }
            else
            {
                if (!int.TryParse(txtFParentID.Text, out FParentID))
                {
                    MessageBox.Show("Parent ID is not an integer.");
                    return;
                }
            }

            FSeqNo = -1;
            if (!string.IsNullOrEmpty(txtFSeqNo.Text))
                if (!int.TryParse(txtFSeqNo.Text, out FSeqNo))
                {
                    MessageBox.Show("SeqNo is not an integer.");
                    return;
                }

            // IO Check.
            int iAffected = 0;
            string sCmd = string.Empty;
            if (!DBExistParentID(FParentID))
            {
                MessageBox.Show($"Parent record does not exist. ParentID={FParentID}.");
                return;
            }
            // You can skip the validation if DB. supports unique index.
            if (DBExistParentIDAndKey(FParentID, txtFKey.Text))
            {
                MessageBox.Show($"Record exists. ParentID={FParentID}, Key={txtFKey.Text}.");
                return;
            }

            // Action.
            iAffected = 0;
            sCmd = "INSERT INTO `TConfig` (`FParentID`, `FSeqNo`, `FKey`, `FValue`, `FValueB`, `FReadonly`, `FNote`) VALUES (@FParentID, @FSeqNo, @FKey, @FValue, @FValueB, @FReadonly, @FNote)";
            try
            {
                using (MySqlConnection cn1 = new MySqlConnection(_sConnectionString))
                using (MySqlCommand command = cn1.CreateCommand())
                {
                    command.CommandText = sCmd;
                    command.Parameters.AddWithValue("@FParentID", FParentID.ZToObject_DBNull());
                    command.Parameters.AddWithValue("@FSeqNo", FSeqNo.ZToObject_DBNull());
                    command.Parameters.AddWithValue("@FKey", txtFKey.Text.ZToObject_DBNull());
                    command.Parameters.AddWithValue("@FValue", txtFValue.Text.ZToObject_DBNull());
                    command.Parameters.AddWithValue("@FValueB", txtFValueB.Text.ZToObject_DBNull());
                    command.Parameters.AddWithValue("@FReadonly", chkReadonly.Checked.ZToObject_DBNull());
                    command.Parameters.AddWithValue("@FNote", txtFNote.Text.ZToObject_DBNull());
                    cn1.Open();
                    iAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
                return;
            }
            if (iAffected != 1)
            {
                MessageBox.Show("Fail.");
                return;
            }
            myLoadKeyList();
            MessageBox.Show("OK.");
        }
        void EntryUpdate()
        {
            int FID;
            int FParentID;
            DateTime FUpdateTime;
            int FSeqNo;

            // ID check. 
            if (string.IsNullOrEmpty(txtFID.Text))
            {
                MessageBox.Show("ID is empty.");
                return;
            }
            if (!int.TryParse(txtFID.Text, out FID))
            {
                MessageBox.Show("ID is not a integer value.");
                return;
            }

            // Fields check.
            if (string.IsNullOrEmpty(lblFUpdateTime.Text))
            {
                MessageBox.Show("Update time is empty.");
                return;
            }
            if (!lblFUpdateTime.Text.ZParseTime_MsDash(out FUpdateTime))
            {
                MessageBox.Show("Update time is not a valid datetime value.");
                return;
            }

            FParentID = -1;
            if (string.IsNullOrEmpty(txtFParentID.Text))
            {
                MessageBox.Show("Parent ID is empty.");
                return;
            }
            else
            {
                if (!int.TryParse(txtFParentID.Text, out FParentID))
                {
                    MessageBox.Show("Parent ID is not an integer.");
                    return;
                }
            }

            FSeqNo = -1;
            if (!string.IsNullOrEmpty(txtFSeqNo.Text))
                if (!int.TryParse(txtFSeqNo.Text, out FSeqNo))
                {
                    MessageBox.Show("SeqNo is not an integer.");
                    return;
                }

            // IO Check.
            DataRow row1 = DBReadRow_ID(FID);
            if (row1 == null)
            {
                MessageBox.Show($"Record does not exist. ID={FID}.");
                return;
            }
            string sFUpdateTime_DB = row1["FUpdateTime"].ZToDateTime().ZToString_MsDash();
            if (lblFUpdateTime.Text != sFUpdateTime_DB)
            {
                MessageBox.Show($"Someone else may change the record already. ID={FID}, Last Update Time={lblFUpdateTime.Text}");
                return;
            }

            Boolean bKeyChanged = false;
            int FParentID_DB = row1["FParentID"].ZToInt();
            string FKey_DB = row1["FKey"].ZToString();
            if (FParentID != FParentID_DB) bKeyChanged = true;
            if (txtFKey.Text != FKey_DB) bKeyChanged = true;
            if (bKeyChanged)
            {
                if (!DBExistParentID(FParentID))
                {
                    MessageBox.Show($"Parent record does not exist. ParentID={FParentID}.");
                    return;
                }

                // You can skip the validation if DB. supports unique index.
                if (DBExistParentIDAndKey(FParentID, txtFKey.Text))
                {
                    MessageBox.Show($"Record exists. ParentID={FParentID}, Key={txtFKey.Text}");
                    return;
                }

                if (DBExistChild(FID))
                {
                    MessageBox.Show($"Parent record can not update (ParentID and Key) fields if any child record exists. ID={FID}");
                    return;
                }
            }


            // IO Action.
            int iAffected = 0;
            string sCmd = "Update `TConfig` set `FParentID`=@FParentID, `FSeqNo`=@FSeqNo, `FKey`=@FKey, `FValue`=@FValue, `FValueB`=@FValueB, `FReadonly`=@FReadonly, `FNote`=@FNote, `FUpdateTime`=now() where `FID`=@FID and `FUpdateTime`=@FUpdateTime";
            try
            {
                using (MySqlConnection cn1 = new MySqlConnection(_sConnectionString))
                using (MySqlCommand command = cn1.CreateCommand())
                {
                    command.CommandText = sCmd;
                    command.Parameters.AddWithValue("@FParentID", FParentID.ZToObject_DBNull());
                    command.Parameters.AddWithValue("@FSeqNo", FSeqNo.ZToObject_DBNull());
                    command.Parameters.AddWithValue("@FKey", txtFKey.Text.ZToObject_DBNull());
                    command.Parameters.AddWithValue("@FValue", txtFValue.Text.ZToObject_DBNull());
                    command.Parameters.AddWithValue("@FValueB", txtFValueB.Text.ZToObject_DBNull());
                    command.Parameters.AddWithValue("@FReadonly", chkReadonly.Checked.ZToObject_DBNull());
                    command.Parameters.AddWithValue("@FNote", txtFNote.Text.ZToObject_DBNull());
                    command.Parameters.AddWithValue("@FID", FID.ZToObject_DBNull());
                    command.Parameters.AddWithValue("@FUpdateTime", FUpdateTime.ZToObject_DBNull());
                    cn1.Open();
                    iAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
                return;
            }
            if (iAffected != 1)
            {
                MessageBox.Show("Fail.");
                return;
            }
            myLoadKeyList();
            MessageBox.Show("OK.");
        }
        void EntryDelete()
        {
            int FID;
            DateTime FUpdateTime;

            // ID check. 
            if (string.IsNullOrEmpty(txtFID.Text))
            {
                MessageBox.Show("ID is empty.");
                return;
            }
            if (!int.TryParse(txtFID.Text, out FID))
            {
                MessageBox.Show("ID is not a integer value.");
                return;
            }

            // Fields check.
            if (string.IsNullOrEmpty(lblFUpdateTime.Text))
            {
                MessageBox.Show("Update time is empty.");
                return;
            }
            if (!lblFUpdateTime.Text.ZParseTime_MsDash(out FUpdateTime))
            {
                MessageBox.Show("Update time is not a valid datetime value.");
                return;
            }

            // IO Check.
            if (!DBExistIDLastUpdate(FID, FUpdateTime))
            {
                MessageBox.Show($"Record does not exist or someone else may change the record already. ID={FID}, Last Update Time={lblFUpdateTime.Text}");
                return;
            }
            if (DBExistChild(FID))
            {
                MessageBox.Show($"Parent record can not delete if any child record exists. ID={FID}");
                return;
            }

            // IO Action.
            int iAffected = 0;
            try
            {
                using (MySqlConnection cn1 = new MySqlConnection(_sConnectionString))
                using (MySqlCommand command = cn1.CreateCommand())
                {
                    command.CommandText = "delete from `TConfig` where `FID`=@FID and `FUpdateTime`=@FUpdateTime";
                    command.Parameters.AddWithValue("@FID", FID);
                    command.Parameters.AddWithValue("@FUpdateTime", FUpdateTime);
                    cn1.Open();
                    iAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
                return;
            }
            if (iAffected != 1)
            {
                MessageBox.Show("Fail.");
                return;
            }
            myLoadKeyList();
            MessageBox.Show("OK.");
        }
        public static void AddParameters(MySqlCommand command, Dictionary<string, object> parameters)
        {
            if (parameters == null)
                return;

            foreach (var param in parameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = param.Key;
                parameter.Value = param.Value ?? DBNull.Value;  // CodeHelper.
                command.Parameters.Add(parameter);
            }
        }

    }
}
