using LKS_SMK.DataAccess;
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
using System.Xml.Linq;

namespace LKS_SMK.Tampilan.Admin
{
    
    public partial class MasterRoomAdmin : Form
    {
        MasterRoomDataAccess da = new MasterRoomDataAccess();

        List<MasterRoomModel> masterRooms;
        MasterRoomModel selectedRooms;
        List<RoomTypeModel> roomTypes;

        List<MasterRoomModel> insertList;
        List<MasterRoomModel> updateList;
        List<MasterRoomModel> deleteList;
        public MasterRoomAdmin()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            insertList = new List<MasterRoomModel>();
            updateList = new List<MasterRoomModel>();
            deleteList = new List<MasterRoomModel>();

            Clear();

            masterRooms = da.GetMasterRooms();
            roomTypes = da.GetRoomTypes();

            var mapRoomTypes = roomTypes.Select(x => x.Name).ToList();

            TabelRoom.DataSource = masterRooms;
            boxRoomType.DataSource = mapRoomTypes;

            LoadTable();

            btnInsert.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        void Reload()
        {
            Clear();

            masterRooms = da.GetMasterRooms();
            TabelRoom.DataSource = masterRooms;
        }

        public void LoadTable()
        {

            var mapRooms = masterRooms.Select(data => new
            {
                RoomNumber = data.RoomNumber,
                RoomType = data.RoomTypeID,
                RoomFloor = data.RoomFloor,
                Description = data.Description
            }).ToList();

            TabelRoom.DataSource = mapRooms;
        }

        public void Clear()
        {
            selectedRooms = null;

            txtRoomNumber.Clear();
            boxRoomType.SelectedIndex = -1;
            txtRoomFloor.Clear();
            txtDescription.Clear();
        }

        private void TabelRoom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            selectedRooms = masterRooms[e.RowIndex];

            var roomTypeIndex = roomTypes.FindIndex(x => x.ID == selectedRooms.RoomTypeID);

            boxRoomType.SelectedIndex = roomTypeIndex;
            txtRoomNumber.Text = selectedRooms.RoomNumber;
            txtRoomFloor.Text = selectedRooms.RoomFloor;
            txtDescription.Text = selectedRooms.Description;

            btnInsert.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        bool Validasi()
        {
            var validasi = new List<string>();

            if (String.IsNullOrEmpty(txtRoomNumber.Text)) validasi.Add("Room Number tidak boleh kosong");

            try
            {
                double.Parse(txtRoomFloor.Text);
            }
            catch
            {
                validasi.Add("Room tidak valid");
            }

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

            var roomType = roomTypes[boxRoomType.SelectedIndex];

            var newRoom = new MasterRoomModel
            {
                RoomTypeID = roomType.ID,
                RoomNumber = txtRoomNumber.Text,
                RoomFloor = txtRoomFloor.Text,
                Description = txtDescription.Text
            };

            insertList.Add(newRoom);

            masterRooms.Add(newRoom);

            Clear();

            LoadTable();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!Validasi()) return;

            var roomType = roomTypes[boxRoomType.SelectedIndex];

            var updatedRooms = new MasterRoomModel
            {
                ID = selectedRooms.ID,
                RoomTypeID = roomType.ID,
                RoomNumber = txtRoomNumber.Text,
                RoomFloor = txtRoomFloor.Text,
                Description = txtDescription.Text
            };

            bool addToList = true;

            //cek insert list
            if (insertList.FirstOrDefault(x => x.ID == selectedRooms.ID) != null)
            {
                //update di list insert
                var insertIndex = insertList.FindIndex(x => x.ID == selectedRooms.ID);

                insertList[insertIndex] = updatedRooms;

                addToList = false;
            }

            if (addToList) updateList.Add(updatedRooms);

            var indexRoom = masterRooms.FindIndex(x => x.ID == selectedRooms.ID);

            masterRooms[indexRoom] = updatedRooms;

            LoadTable();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var doDelete = new Konfirmasi().ShowDialog();

            if (doDelete == DialogResult.Cancel) return;

            bool addToList = true;

            //cek insert list
            if (insertList.FirstOrDefault(x => x.ID == selectedRooms.ID) != null)
            {
                var insertIndex = masterRooms.FindIndex(x => x.ID == selectedRooms.ID);

                insertList.RemoveAt(insertIndex);

                addToList = false;
            }

            //cek update list
            if (updateList.FirstOrDefault(x => x.ID == selectedRooms.ID) != null)
            {
                var updateIndex = updateList.FindIndex(x => x.ID == selectedRooms.ID);

                updateList.RemoveAt(updateIndex);

                addToList = false;
            }

            var indexRoom = masterRooms.FindIndex(x => x.ID == selectedRooms.ID);

            if (addToList) deleteList.Add(masterRooms[indexRoom]);

            masterRooms.RemoveAt(indexRoom);

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

                //if(errors.Count > 0) MessageBox.Show(String.Join("\n", errors));

                Init();
                MessageBox.Show("Data Berhasil Disimpan");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Gagal Disimpan");
            }
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
    }
}
