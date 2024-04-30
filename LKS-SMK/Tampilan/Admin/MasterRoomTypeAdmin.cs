using LKS_SMK.DataAccess;
using LKS_SMK.db;
using LKS_SMK.Model;
using LKS_SMK.Tampilan.etc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LKS_SMK.Tampilan.Admin
{
    public partial class MasterRoomTypeAdmin : Form
    {
        RoomTypeDataAccess da = new RoomTypeDataAccess();

        List<RoomTypeModel> roomtypes;
        RoomTypeModel selectedRoomType;

        List<RoomTypeModel> insertList;
        List<RoomTypeModel> updateList;
        List<RoomTypeModel> deleteList;
        public MasterRoomTypeAdmin()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            insertList = new List<RoomTypeModel>();
            updateList = new List<RoomTypeModel>();
            deleteList = new List<RoomTypeModel>();

            roomtypes = da.GetRoomTypes();

            LoadTable();

            btnInsert.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void Clear()
        {
            selectedRoomType = null;

            txtName.Clear();
            txtRoomPrice.Clear();
            txtCapacity.Value = 1;
            openFileDialog1.FileName = "";
            pictureBox1.ImageLocation = "";
        }

        public void LoadTable()
        {
            var mapRoomTypes = roomtypes.Select(data => new {
                Name = data.Name,
                Capacity = data.Capacity,
                RoomPrice = data.RoomPrice,
            }).ToList();

            TabelRoomType.DataSource = mapRoomTypes;
        }

        bool Validasi()
        {
            var validasi = new List<string>();

            if (String.IsNullOrEmpty(txtName.Text)) validasi.Add("Room Name tidak boleh kosong");

            try
            {
                double.Parse(txtCapacity.Text);
            }
            catch
            {
                validasi.Add("Nilai Room Price tidak valid");
            }

            try
            {
                double.Parse(txtRoomPrice.Text);
            }
            catch (Exception)
            {
                validasi.Add("Nilai Room Price tidak valid");
            }

            if (string.IsNullOrEmpty(pictureBox1.ImageLocation)) validasi.Add("Gambar tidak boleh kosong");

            if (validasi.Count > 0)
            {
                MessageBox.Show(String.Join("\n", validasi));
                return false;
            }
            else
            {
                return true;
            }


        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (!Validasi()) return;

            var newRoomType = new RoomTypeModel
            {
                Name = txtName.Text,
                Capacity = int.Parse(txtCapacity.Value.ToString()),
                RoomPrice = double.Parse(txtRoomPrice.Text),
                Photo = pictureBox1.ImageLocation
            };

            insertList.Add(newRoomType);

            roomtypes.Add(newRoomType);

            Clear();

            LoadTable();
        }

        private void btnPoto_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
        }

        private void TabelRoomType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0) return;

            if (e.RowIndex == -1) return;

            selectedRoomType = roomtypes[e.RowIndex];

            txtName.Text = selectedRoomType.Name;
            txtCapacity.Value = selectedRoomType.Capacity;
            txtRoomPrice.Text = selectedRoomType.RoomPrice.ToString();

            pictureBox1.ImageLocation = selectedRoomType.Photo;

            btnInsert.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();

            LoadTable();

            btnInsert.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var errors = da.Save(
                   insert: insertList,
                   update: updateList,
                   delete: deleteList);

                Init();

                MessageBox.Show("Data Berhasil Disimpan");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Data Gagal Disimpan");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!Validasi()) return;

            var updatedRoomType = new RoomTypeModel
            {
                ID = selectedRoomType.ID,
                Name = txtName.Text,
                Capacity = int.Parse(txtCapacity.Value.ToString()),
                RoomPrice = double.Parse(txtRoomPrice.Text),
                Photo = pictureBox1.ImageLocation
            };

            var indexData = roomtypes.FindIndex(x => x.ID == updatedRoomType.ID);

            roomtypes[indexData] = updatedRoomType;

            LoadTable();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var doDelete = new Konfirmasi().ShowDialog();

            if (doDelete == DialogResult.Cancel) return;

            bool addToList = true;

            
            if (insertList.FirstOrDefault(x => x.ID == selectedRoomType.ID) != null)
            {
                var insertIndex = insertList.FindIndex(x => x.ID == selectedRoomType.ID);

                insertList.RemoveAt(insertIndex);

                addToList = false;
            }

            
            if (updateList.FirstOrDefault(x => x.ID == selectedRoomType.ID) != null)
            {
                var updateIndex = updateList.FindIndex(x => x.ID == selectedRoomType.ID);

                updateList.RemoveAt(updateIndex);

                addToList = false;
            }

            var indexRoom = roomtypes.FindIndex(x => x.ID == selectedRoomType.ID);

            if (addToList) deleteList.Add(roomtypes[indexRoom]);

            roomtypes.RemoveAt(indexRoom);

            Clear();

            LoadTable();

            btnInsert.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }
    }
}
