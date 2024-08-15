using QL_XEKHACH.DAO;
using QL_XEKHACH.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace QL_XEKHACH.UserControls
{
    public partial class Page_LoTrinh : UserControl
    {
        int matuyen = 0;
        int maChuyen = 0;
        public Page_LoTrinh()
        {
            InitializeComponent();
            
        }
        private void Page_LoTrinh_Load(object sender, EventArgs e)
        {
            //tuyen
            lsvTuyenXe.Columns.Add("Tên tuyến");
            lsvTuyenXe.Columns.Add("Điểm xuất phát");
            lsvTuyenXe.Columns.Add("Điểm đến");
            lsvTuyenXe.Columns.Add("Giá tiền");
            lsvTuyenXe.Columns.Add("Quảng đường");
            lsvTuyenXe.GridLines = true;
            lsvTuyenXe.FullRowSelect = true;
            load_lsvTuyenXe();
            //chuyen
            lsvChuyenXe.Columns.Add("Mã tuyến");
            lsvChuyenXe.Columns.Add("Giờ chạy");
            lsvChuyenXe.Columns.Add("Giờ đến");
            lsvChuyenXe.Columns.Add("Mã tài xế");
            lsvChuyenXe.Columns.Add("Mã xe");
            lsvChuyenXe.Columns.Add("Ghế trống");
            lsvChuyenXe.GridLines = true;
            lsvChuyenXe.FullRowSelect = true;
            load_lsvAllChuyenXe();
            LoadTuyenToComboBox();
            loadTaixeToComboBox();
            loadLoaiXeToComboBox();
        }
        private void load_lsvTuyenXe()
        {
           
            List<TuyenXe> tuyenXeList = TuyenXeDAO.Instance.GetAllTuyenXe();
            load_itemTuyenXe(tuyenXeList);
        }
        private void load_itemTuyenXe(List<TuyenXe> tuyenXeList)
        {
            lsvTuyenXe.Items.Clear();
            foreach (TuyenXe tuyenXe in tuyenXeList)
            {
                ListViewItem item = new ListViewItem(tuyenXe.TenTuyen.ToString());
                item.SubItems.Add(tuyenXe.DiemXuatPhat.ToString());
                item.SubItems.Add(tuyenXe.DiemDen.ToString());
                item.SubItems.Add(tuyenXe.Gia.ToString());
                item.SubItems.Add(tuyenXe.QuangDuong.ToString());
                lsvTuyenXe.Items.Add(item);
            }
            foreach (ColumnHeader column in lsvTuyenXe.Columns)
            {
                column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                int headerWidth = TextRenderer.MeasureText(column.Text, lsvTuyenXe.Font).Width + 10;
                column.Width = Math.Max(column.Width, headerWidth);
            }
        }
        private void load_lsvChuyenXeFromChuyenXe(int matuyen)
        {
            
            List<ChuyenXe> ChuyenXeList = ChuyenXeDAO.Instance.GetChuyenXeFromTuyenXe(matuyen);
            load_itemChuyenXe(ChuyenXeList);
        }
        private void load_lsvAllChuyenXe()
        {
            List<ChuyenXe> ChuyenXeList = ChuyenXeDAO.Instance.GetAllChuyenXe();
            load_itemChuyenXe(ChuyenXeList);
        }
        private void load_itemChuyenXe(List<ChuyenXe> ChuyenXeList)
        {
            lsvChuyenXe.Items.Clear();
            foreach (ChuyenXe chuyenXe in ChuyenXeList)
            {
                ListViewItem item = new ListViewItem(chuyenXe.MaTuyen.ToString());
                item.SubItems.Add(chuyenXe.Start.ToString());
                item.SubItems.Add(chuyenXe.End.ToString());
                item.SubItems.Add(chuyenXe.MaTaiXe.ToString());
                item.SubItems.Add(chuyenXe.MaXe.ToString());
                item.SubItems.Add(chuyenXe.GheTrong.ToString());
                lsvChuyenXe.Items.Add(item);
            }
            foreach (ColumnHeader column in lsvChuyenXe.Columns)
            {
                column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                int headerWidth = TextRenderer.MeasureText(column.Text, lsvChuyenXe.Font).Width + 10;
                column.Width = Math.Max(column.Width, headerWidth);
            }
        }
        private void lsvTuyenXe_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (lsvTuyenXe.SelectedItems.Count > 0)
            {
               
                string tenTuyen = lsvTuyenXe.SelectedItems[0].Text;
                txtLoTrinh.Text = tenTuyen;
                txtStart.Text = lsvTuyenXe.SelectedItems[0].SubItems[1].Text;
                txtEnd.Text = lsvTuyenXe.SelectedItems[0].SubItems[2].Text;
                txtMoney.Text = lsvTuyenXe.SelectedItems[0].SubItems[3].Text;
                txtLong.Text = lsvTuyenXe.SelectedItems[0].SubItems[4].Text;
                matuyen = TuyenXeDAO.Instance.GetMaTuyen(tenTuyen);
                load_lsvChuyenXeFromChuyenXe(matuyen);
            }
            else
                txtLoTrinh.ReadOnly = false;
        }
        private void Page_LoTrinh_MouseClick(object sender, MouseEventArgs e)
        {
            lsvTuyenXe.SelectedItems.Clear();
        }
        void LoadTuyenToComboBox()
        {
            List<TuyenXe> listTuyenXe = TuyenXeDAO.Instance.GetAllTuyenXe();
            cbxTuyenXe.DataSource = listTuyenXe;
            cbxTuyenXe.DisplayMember = "TenTuyen";
            cbxTuyenXe.ValueMember = "MaTuyen";
        }
        void loadTaixeToComboBox()
        {
            List<TaiXe> listLoaiXe = TaiXeDAO.Instance.GetAllTaixe();
            cbxTaiXe.DataSource = listLoaiXe;
            cbxTaiXe.DisplayMember = "TenTaixe";
            cbxTaiXe.ValueMember = "MaTaiXe";
        }
        
        
        void loadLoaiXeToComboBox()
        {
            List<LoaiXe> listLoaiXe = LoaiXeDAO.Instance.GetALLLoaiXe();
            cbxLoaiXe.DataSource = listLoaiXe;
            cbxLoaiXe.DisplayMember = "tenLoaiXe";
            cbxLoaiXe.ValueMember = "maLoaiXe";
        }
        private void cbxLoaiXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoaiXe selectedLoaiXe = (LoaiXe)cbxLoaiXe.SelectedItem;
            int maLoaiXe = selectedLoaiXe.MaLoaixe;
            loadXeFromLoaiXeToComboBox(maLoaiXe);
            SelectComboBoxValue(cbxXe, (Xe xe) => xe.MaXe.ToString(), maxe);
            Xe selectedXe = (Xe)cbxXe.SelectedItem;
        }
        void loadXeFromLoaiXeToComboBox(int maloaixe)
        {
            List<Xe> listLoaiXe = XeDAO.Instance.GetALLXeFromMaLoaiXe(maloaixe);
            cbxXe.DataSource = listLoaiXe;
            cbxXe.DisplayMember = "BienSoXe";
            cbxXe.ValueMember = "MaXe";
        }
        void loadAllXeToComboBox()
        {
            List<Xe> listLoaiXe = XeDAO.Instance.GetALLXe();
            cbxXe.DataSource = listLoaiXe;
            cbxXe.DisplayMember = "BienSoXe";
            cbxXe.ValueMember = "MaXe";
        }
        private void btnShowAllTuyen_Click(object sender, EventArgs e)
        {
            load_lsvTuyenXe();
        }
        private void btnShowAllChuyen_Click(object sender, EventArgs e)
        {
            load_lsvAllChuyenXe();
        }
        private void btnTimKiemTuyen_Click(object sender, EventArgs e)
        {
            string tenTuyen = txtLoTrinh.Text;
            string diemxuatphat = txtStart.Text;
            string diemden = txtEnd.Text;
            int quangduong = 0;
            float giatien = 0;
            if (!string.IsNullOrEmpty(txtLong.Text))
            {
                quangduong = int.Parse(txtLong.Text);
            }
            if (!string.IsNullOrEmpty(txtMoney.Text))
            {
                giatien = float.Parse(txtMoney.Text);
            }

            List<TuyenXe> tuyenXeList = TuyenXeDAO.Instance.GetALLLisTuyenTimKiem(tenTuyen, diemxuatphat, diemden, quangduong, giatien);
            load_itemTuyenXe(tuyenXeList);
        }
        private void btnTimkiemChuyen_Click(object sender, EventArgs e)
        {
            int maTuyen = 0;
            if (!string.IsNullOrEmpty(cbxTuyenXe.Text))
            {
                TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
                maTuyen = selectedTuyen.MaTuyen;
            }
            int mataixe = 0;
            if (!string.IsNullOrEmpty(cbxTaiXe.Text))
            {
                TaiXe selectedTaixe = (TaiXe)cbxTaiXe.SelectedItem;
                mataixe = selectedTaixe.MaTaiXe;
            }
            int maloaiXe = 0;
            if (!string.IsNullOrEmpty(cbxLoaiXe.Text))
            {
                LoaiXe selectedLoaiXe = (LoaiXe)cbxLoaiXe.SelectedItem;
               maloaiXe = selectedLoaiXe.MaLoaixe;
            }
            int maxe = 0;
            if (!string.IsNullOrEmpty(cbxXe.Text))
            {
                Xe selectedXe = (Xe)cbxXe.SelectedItem;
                maxe = selectedXe.MaXe;
            }
            DateTime selectedDateXuatphat = dtpGioChay.Value;
            string Stringformat = "yyyy-MM-dd HH:mm:ss";
            string gioxuatphat = selectedDateXuatphat.ToString(Stringformat);
            
            DateTime selectedDateDateDen = dtpGioDen.Value;
            string gioden = selectedDateDateDen.ToString(Stringformat);
            List<ChuyenXe> chuyenXeList = ChuyenXeDAO.Instance.GetAllistChuyenTimKiem(maTuyen, gioxuatphat, gioden, mataixe,maloaiXe, maxe);
            load_itemChuyenXe(chuyenXeList);
        }
        private void SelectComboBoxValue<T>(ComboBox comboBox, Func<T, string> valueSelector, string valueToSelect)
        {
            foreach (var item in comboBox.Items)
            {
                if (item is T yourObject && valueSelector(yourObject) == valueToSelect)
                {
                    comboBox.SelectedItem = item;
                    break;
                }
            }
        }
        string maxe = "";
        private void lsvChuyenXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvChuyenXe.SelectedItems.Count > 0)
            {     
                loadAllXeToComboBox();
                string matuyen = lsvChuyenXe.SelectedItems[0].Text;
                string giochay = lsvChuyenXe.SelectedItems[0].SubItems[1].Text;
                DateTime dateTimeNgayChay = DateTime.Parse(giochay);
                dtpGioChay.Value = dateTimeNgayChay;
                string gioden = lsvChuyenXe.SelectedItems[0].SubItems[2].Text;
               
                DateTime dateTimeGioDen = DateTime.Parse(gioden);
                dtpGioDen.Value = dateTimeGioDen;
                string mataixe = lsvChuyenXe.SelectedItems[0].SubItems[3].Text;
                maxe = lsvChuyenXe.SelectedItems[0].SubItems[4].Text;

                SelectComboBoxValue(cbxTuyenXe, (TuyenXe tuyenxe) => tuyenxe.MaTuyen.ToString(), matuyen);
                SelectComboBoxValue(cbxTaiXe, (TaiXe taixe) => taixe.MaTaiXe.ToString(), mataixe);
                
                SelectComboBoxValue(cbxXe, (Xe xe) => xe.MaXe.ToString(), maxe);
                Xe selectedXe = (Xe)cbxXe.SelectedItem;
                int maloaixe = selectedXe.MaLoaiXe;
                SelectComboBoxValue(cbxLoaiXe, (LoaiXe loaixe) => loaixe.MaLoaixe.ToString(), maloaixe.ToString());
                string giochay1 = dateTimeNgayChay.ToString("dd/MM/yyyy HH:mm");
                maChuyen = ChuyenXeDAO.Instance.GetMAChuyen(maloaixe, int.Parse(matuyen), giochay1, int.Parse(mataixe));
            }
        }
        // Thêm xoá sửa Tuyến
        private void btnAddTuyen_Click(object sender, EventArgs e)
        {
            
            int quangduong = 0;
            float giatien = 0;
            if (!string.IsNullOrEmpty(txtLong.Text))
            {
                bool isSo = DataProvider.Instance.IsNumeric(txtLong.Text);
                if (!isSo)
                {
                    MessageBox.Show("Vui lòng nhập vào quảng đường là 1 số!");
                    txtLong.Focus();
                    return;
                }    
                quangduong = int.Parse(txtLong.Text);
                
            }
            if (!string.IsNullOrEmpty(txtMoney.Text))
            {
                bool isSo = DataProvider.Instance.IsNumeric(txtMoney.Text);
                if (!isSo)
                {
                    MessageBox.Show("Vui lòng nhập vào giá tiền là 1 số!");
                    txtMoney.Focus();
                    return;
                }
                giatien = float.Parse(txtMoney.Text);
            }
            if(string.IsNullOrEmpty(txtLoTrinh.Text) || string.IsNullOrEmpty(txtStart.Text) || string.IsNullOrEmpty(txtEnd.Text) ||
                quangduong == 0 || giatien == 0)
            {
                MessageBox.Show("Vui lòng điền đầy đủ các thông tin cần thiết!");
                return;
            }
            string tenTuyen = txtLoTrinh.Text;
            string diemxuatphat = txtStart.Text;
            string diemden = txtEnd.Text;
           
            DialogResult result = MessageBox.Show("Xác nhận thêm tuyến xe mới?", "Xác nhận", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                bool kq = TuyenXeDAO.Instance.InsertTuyen(tenTuyen, diemxuatphat, diemden, giatien, quangduong);
                if (kq == true)
                {
                    MessageBox.Show("Thêm thành công ");
                    load_lsvTuyenXe();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại ");
                }
            }
            else
                return;
        }

        private void btnEditTuyen_Click(object sender, EventArgs e)
        {
            int quangduong = 0;
            float giatien = 0;
            if (!string.IsNullOrEmpty(txtLong.Text))
            {
                quangduong = int.Parse(txtLong.Text);
            }
            if (!string.IsNullOrEmpty(txtMoney.Text))
            {
                giatien = float.Parse(txtMoney.Text);
            }
            string tenTuyen = txtLoTrinh.Text;
            string diemxuatphat = txtStart.Text;
            string diemden = txtEnd.Text;
            if(matuyen == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 tuyến xe để thực hiện hành động này!");
                return;
            }    
            DialogResult result = MessageBox.Show("Xác nhận cập nhật lại thông tin tuyến xe có mã tuyến :" + matuyen + " không ?", "Xác nhận", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                bool kq = TuyenXeDAO.Instance.UpdateTuyen(matuyen,tenTuyen, diemxuatphat, diemden, giatien, quangduong);
                if (kq == true)
                {
                    MessageBox.Show("cập nhật tuyến thành công ");
                    load_lsvTuyenXe();
                }
                else
                {
                    MessageBox.Show("cập nhật tuyến thất bại ");
                }
            }
            else
                return;
        }
        private void btnDeleteTuyen_Click(object sender, EventArgs e)
        {
            if (matuyen == 0 && string.IsNullOrEmpty(txtLoTrinh.Text))
            {
                MessageBox.Show("Vui lòng chọn 1 tuyến xe để thực hiện hành động này!");
                return;
            }
            DialogResult result = MessageBox.Show("Xác nhận xoá tuyến có mã tuyến: "+ matuyen+ " không?", "Xác nhận", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                bool kq = TuyenXeDAO.Instance.DeleteTuyen(matuyen);
                if (kq == true)
                {
                    MessageBox.Show("Xóa tuyến thành công ");
                    load_lsvTuyenXe();
                }
                else
                {
                    MessageBox.Show("Xóa tuyến thất bại ");
                }
            }
            else
                return;
        }
        // thêm xoá sửa chuyến
        private void btnAddChuyen_Click(object sender, EventArgs e)
        {
            int maTuyen = 0;
            if (!string.IsNullOrEmpty(cbxTuyenXe.Text))
            {
                TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
                maTuyen = selectedTuyen.MaTuyen;
            }
            int mataixe = 0;
            if (!string.IsNullOrEmpty(cbxTaiXe.Text))
            {
                TaiXe selectedTaixe = (TaiXe)cbxTaiXe.SelectedItem;
                mataixe = selectedTaixe.MaTaiXe;
            }
            int maxe = 0;
            if (!string.IsNullOrEmpty(cbxXe.Text))
            {
                Xe selectedXe = (Xe)cbxXe.SelectedItem;
                maxe = selectedXe.MaXe;
            }
            DateTime selectedDateXuatphat = dtpGioChay.Value;
            string Stringformat = "yyyy-MM-dd HH:mm:ss";
            string gioxuatphat = selectedDateXuatphat.ToString(Stringformat);
            DateTime selectedDateDateDen = dtpGioDen.Value;
            if(selectedDateXuatphat> selectedDateDateDen)
            {
                MessageBox.Show("Thời gian xuất phát không thể lớn hơn thời gian đến!");
                return;
            } 
                
            string gioden = selectedDateDateDen.ToString(Stringformat);

            DialogResult result = MessageBox.Show("Xác nhận thêm chuyến xe mới?", "Xác nhận", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                bool kq = ChuyenXeDAO.Instance.InserChuyen(maTuyen, maxe, gioxuatphat, gioden, mataixe);
                if (kq == true)
                {
                    MessageBox.Show("Thêm chuyến thành công ");
                    load_lsvAllChuyenXe();
                }
                else
                {
                    MessageBox.Show("Thêm chuyến thất bại ");
                }
            }
            else
                return;
        }

        private void btnEditChuyen_Click(object sender, EventArgs e)
        {
            if(maChuyen==0)
            {
                MessageBox.Show("Vui lòng chọn 1 chuyến xe để thực hiện hành động này!");
                return;
            }    
            int mataixe = 0;
            if (!string.IsNullOrEmpty(cbxTaiXe.Text))
            {
                TaiXe selectedTaixe = (TaiXe)cbxTaiXe.SelectedItem;
                mataixe = selectedTaixe.MaTaiXe;
            }
            int maxe = 0;
            if (!string.IsNullOrEmpty(cbxXe.Text))
            {
                Xe selectedXe = (Xe)cbxXe.SelectedItem;
                maxe = selectedXe.MaXe;
            }
            DateTime selectedDateXuatphat = dtpGioChay.Value;
            string Stringformat = "yyyy-MM-dd HH:mm:ss";
            string gioxuatphat = selectedDateXuatphat.ToString(Stringformat);
            DateTime selectedDateDateDen = dtpGioDen.Value;
            if (selectedDateXuatphat > selectedDateDateDen)
            {
                MessageBox.Show("Thời gian xuất phát không thể lớn hơn thời gian đến!");
                return;
            }
            string gioden = selectedDateDateDen.ToString(Stringformat);
            DialogResult result = MessageBox.Show("Xác nhận cập nhật chuyến xe?", "Xác nhận", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                bool kq = ChuyenXeDAO.Instance.UpdateChuyen(maChuyen, maxe, gioxuatphat, gioden, mataixe);
                if (kq == true)
                {
                    MessageBox.Show("Cập nhật chuyến thành công ");
                    load_lsvAllChuyenXe();
                }
                else
                {
                    MessageBox.Show("Cập nhật chuyến thất bại ");
                }
            }
            else
                return;
        }

        private void btnDeleteChuyen_Click(object sender, EventArgs e)
        {
            if (maChuyen == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 chuyến xe để thực hiện hành động này!");
                return;
            }
            DialogResult result = MessageBox.Show("Xác nhận xoá chuyến có mã chuyến: " + maChuyen + " không?", "Xác nhận", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                bool kq = ChuyenXeDAO.Instance.DeleteChuyen(maChuyen);
                if (kq == true)
                {
                    MessageBox.Show("Xóa chuyến thành công ");
                    load_lsvAllChuyenXe();
                }
                else
                {
                    MessageBox.Show("Xóa chuyến thất bại ");
                }
            }
            else
                return;
        }
    }
}
