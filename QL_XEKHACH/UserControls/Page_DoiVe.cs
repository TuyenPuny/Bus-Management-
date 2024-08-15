using QL_XEKHACH.DAO;
using QL_XEKHACH.DTO;
using System;
using System.Collections.Generic;
using System.Data;

using System.Windows.Forms;


namespace QL_XEKHACH.UserControls
{
    public partial class Page_DoiVe : UserControl
    {
        int mave = 0;
        int tiencu = 0;
        int tienmoi = 0;
        public Page_DoiVe()
        {
            InitializeComponent();
            

        }
        private void Page_DoiVe_Load(object sender, EventArgs e)
        {
            LoadTuyenToComboBox();
            lsvVeXe.View = View.Details;
            taoColumn();
            load_lsvChuyenXe();
        }
        void LoadTuyenToComboBox()
        {
            List<TuyenXe> listTuyenXe = TuyenXeDAO.Instance.GetAllTuyenXe();
            cbxTuyenXe.DataSource = listTuyenXe;
            cbxTuyenXe.DisplayMember = "TenTuyen";
            cbxTuyenXe.ValueMember = "MaTuyen";
        }
        private void cbxTuyenXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTuyenXe.DataSource == null)
            {
                return;
            }
            TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
            int maTuyen = selectedTuyen.MaTuyen;
            LoadNgayChayFromTuyenXeToComboBox(maTuyen);

        }
        void LoadNgayChayFromTuyenXeToComboBox(int matuyen)
        {

            List<string> listNgayChay = ChuyenXeDAO.Instance.GetNgayChayFromTuyenXe(matuyen);
            if (listNgayChay.Count > 0)
            {
                cbxNgayChay.DataSource = listNgayChay;
                cbxNgayChay.DisplayMember = "NgayThangNam";
            }
            else
            {
                cbxNgayChay.DataSource = null;
                cbxNgayChay.Text = string.Empty;
            }
        }
        private void cbxChuyenXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxNgayChay.DataSource == null)
            {
                return;
            }
            TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
            int maTuyen = selectedTuyen.MaTuyen;
            string ngay = cbxNgayChay.Text;
            LoadGioChayFromTuyenXeToComboBox(maTuyen, ngay);
        }
        void LoadGioChayFromTuyenXeToComboBox(int matuyen, string date)
        {
            List<string> listGioChay = ChuyenXeDAO.Instance.GetGioChayFromTuyenXe(matuyen, date);
            if (listGioChay.Count > 0)
            {
                cbxGio.DataSource = listGioChay;
                cbxGio.DisplayMember = "GioChay";
            }
            else
            {
                cbxGio.DataSource = null;
                cbxGio.Text = string.Empty;
            }
        }
        private void cbxGio_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbxGio.DataSource == null)
                return;
            string gioxuatphat = cbxNgayChay.SelectedItem.ToString() + " " + cbxGio.SelectedItem.ToString();
            TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
            int maTuyen = selectedTuyen.MaTuyen;
            LoadLoaiXeFromChuyenXeToComboBox(maTuyen, gioxuatphat);
        }

        void LoadLoaiXeFromChuyenXeToComboBox(int matuyen, string gioxuatphat)
        {
            List<LoaiXe> listLoaiXe = LoaiXeDAO.Instance.GetLoaiXeFormChuyenXe(matuyen, gioxuatphat);
            cbxLoaiXe.DataSource = listLoaiXe;
            cbxLoaiXe.DisplayMember = "tenLoaiXe";
            cbxLoaiXe.ValueMember = "maLoaixe";
        }
        private void cbxLoaiXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelLoaiXe.Controls.Clear();
            if (cbxLoaiXe.DataSource == null)
                return;
            LoaiXe selectedLoaixe = (LoaiXe)cbxLoaiXe.SelectedItem;
            int maLoaiXe = selectedLoaixe.MaLoaixe;
            loadTaixeToComboBox(maLoaiXe);
            TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
            int giave = selectedTuyen.Gia;

            if (maLoaiXe == 2)
            {
                giave += 150000;
            }
            if (maLoaiXe == 3)
            {
                giave += 300000;
            }
            if (maLoaiXe == 4)
            {
                giave += 200000;
            }
            if (maLoaiXe == 5)
            {
                giave += 150000;
            }
            txtGiaVeMoi.Text = giave.ToString();
            
        }
        void loadTaixeToComboBox(int maloaixe)
        {
            TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
            int maTuyen = selectedTuyen.MaTuyen;
            string gioxuatphat = cbxNgayChay.SelectedItem.ToString() + " " + cbxGio.SelectedItem.ToString();
            List<TaiXe> listLoaiXe = TaiXeDAO.Instance.GetALLTaixeFormTuyen(maTuyen, maloaixe, gioxuatphat);
            cbxTaiXe.DataSource = listLoaiXe;
            cbxTaiXe.DisplayMember = "TenTaixe";
            cbxTaiXe.ValueMember = "MaTaiXe";
        }
        private void cbxTaiXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTaiXe.DataSource == null)
                return;
            TaiXe selectedTaiXe = (TaiXe)cbxTaiXe.SelectedItem;
            int mataixe = selectedTaiXe.MaTaiXe;
            loadBienSoXe(mataixe);
            load_PanelSoDoXe();
            getTienChenhLech();
        }
        void load_PanelSoDoXe()
        {
            TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
            string gioxuatphat = cbxNgayChay.SelectedItem.ToString() + " " + cbxGio.SelectedItem.ToString();
            TaiXe selectedTaiXe = (TaiXe)cbxTaiXe.SelectedItem;
            LoaiXe selectedLoaixe = (LoaiXe)cbxLoaiXe.SelectedItem;
            int maLoaiXe = selectedLoaixe.MaLoaixe;
            int mataixe = selectedTaiXe.MaTaiXe;
            int maTuyen = selectedTuyen.MaTuyen;
            panelLoaiXe.Controls.Clear();
            if (maLoaiXe == 1)
            {
                Page_Xe41Cho xe41cho = new Page_Xe41Cho(maTuyen, maLoaiXe, gioxuatphat, mataixe);
                xe41cho.ButtonClick += UserControl_ButtonClick;
                panelLoaiXe.Controls.Add(xe41cho);
                return;
            }
            if (maLoaiXe == 2)
            {
                Page_XeLuxury luxury = new Page_XeLuxury(maTuyen, maLoaiXe, gioxuatphat, mataixe);
                luxury.ButtonClick += UserControl_ButtonClick;
                panelLoaiXe.Controls.Add(luxury);
                return;
            }
            if (maLoaiXe == 3)
            {
                Page_XeLimousine Limousine = new Page_XeLimousine(maTuyen, maLoaiXe, gioxuatphat, mataixe);
                Limousine.ButtonClick += UserControl_ButtonClick;
                panelLoaiXe.Controls.Add(Limousine);
                return;
            }
            if (maLoaiXe == 4)
            {
                Page_xe29Cho Xe29cho = new Page_xe29Cho(maTuyen, maLoaiXe, gioxuatphat, mataixe);
                Xe29cho.ButtonClick += UserControl_ButtonClick;
                panelLoaiXe.Controls.Add(Xe29cho);
                return;
            }
            if (maLoaiXe == 5)
            {
                Page_Xe16Cho Xe16cho = new Page_Xe16Cho(maTuyen, maLoaiXe, gioxuatphat, mataixe);
                Xe16cho.ButtonClick += UserControl_ButtonClick;
                panelLoaiXe.Controls.Add(Xe16cho);
                return;
            }
        }
        void loadBienSoXe(int mataixe)
        {
            TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
            int maTuyen = selectedTuyen.MaTuyen;
            string gioxuatphat = cbxNgayChay.SelectedItem.ToString() + " " + cbxGio.SelectedItem.ToString();
            LoaiXe selectedLoaixe = (LoaiXe)cbxLoaiXe.SelectedItem;
            int maLoaiXe = selectedLoaixe.MaLoaixe;
            string BienSoXe = XeDAO.Instance.GetBienSoXeFromChuyenXe(maTuyen, maLoaiXe, gioxuatphat, mataixe);
            txtBienSoXe.Text = BienSoXe;

        }


        private void UserControl_ButtonClick(object sender, ButtonClickEventArgs e)
        {
            // Xử lý sự kiện click từ UserControl ở đây
            string buttonText = e.ButtonName;
            txtViTri.Text = buttonText;
        }
        private int getMachuyen()
        {
            TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
            int maTuyen = selectedTuyen.MaTuyen;
            string gioxuatphat = cbxNgayChay.SelectedItem.ToString() + " " + cbxGio.SelectedItem.ToString();
            LoaiXe selectedLoaixe = (LoaiXe)cbxLoaiXe.SelectedItem;
            int maLoaiXe = selectedLoaixe.MaLoaixe;
            TaiXe selectedTaiXe = (TaiXe)cbxTaiXe.SelectedItem;
            int mataixe = selectedTaiXe.MaTaiXe;
            int machuyen = ChuyenXeDAO.Instance.GetMAChuyen(maLoaiXe, maTuyen, gioxuatphat, mataixe);
            return machuyen;
        }
        void loadDuLieuVaoListView(DataTable data)
        {
            lsvVeXe.Items.Clear();
            foreach (DataRow row in data.Rows)
            {
                ListViewItem item = new ListViewItem(row["MAVE"].ToString());
                item.SubItems.Add(row["TENKH"].ToString());
                item.SubItems.Add(row["SDT"].ToString());
                item.SubItems.Add(row["TENTUYEN"].ToString());
                DateTime ngayGioString = (DateTime)row["GIOXUATPHAT"];
                string ngay = ngayGioString.ToString("yyyy-MM-dd");
                string gio = ngayGioString.ToString("HH:mm");
                item.SubItems.Add(ngay);
                item.SubItems.Add(gio);
                item.SubItems.Add(row["TENLOAIXE"].ToString());
                item.SubItems.Add(row["TENTAIXE"].ToString());
                item.SubItems.Add(row["VITRI"].ToString());
                item.SubItems.Add(row["TINHTRANG"].ToString());
                int giave = int.Parse(row["BANGGIA"].ToString());
                item.SubItems.Add(giave.ToString());
                lsvVeXe.Items.Add(item);
            }
            foreach (ColumnHeader column in lsvVeXe.Columns)
            {
                column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                int headerWidth = TextRenderer.MeasureText(column.Text, lsvVeXe.Font).Width + 10;
                column.Width = Math.Max(column.Width, headerWidth);
            }
        }
        void taoColumn()
        {
            lsvVeXe.Items.Clear();
            lsvVeXe.Columns.Add("Mã vé");
            lsvVeXe.Columns.Add("Tên hành khách");
            lsvVeXe.Columns.Add("SDT");
            lsvVeXe.Columns.Add("Tên tuyến");
            lsvVeXe.Columns.Add("Ngày xuất phát");
            lsvVeXe.Columns.Add("giờ chạy");
            lsvVeXe.Columns.Add("Loại xe");
            lsvVeXe.Columns.Add("tài xế");
            lsvVeXe.Columns.Add("Vị trí");
            lsvVeXe.Columns.Add("Tình trạng");
            lsvVeXe.Columns.Add("Giá vé");
            lsvVeXe.GridLines = true;
            lsvVeXe.FullRowSelect = true;
            
        }
        private void load_lsvChuyenXe()
        {

            string query = "SELECT * FROM dbo.ViewAllTTVeXe";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            loadDuLieuVaoListView(data);
        }
        
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string TenKH =string.Empty;
            string SDT = string.Empty;
            if (checkBoxHanhKhach.Checked)
            {
                TenKH = txtName.Text;
                 SDT = txtSDT.Text;
            }   
            int machuyen = 0;
            if (checkBoxChuyenXe.Checked)
            {
                
                machuyen = getMachuyen();
            }
            int maTuyen = 0;
            if (checkBoxTuyenXe.Checked)
            {
                TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
                maTuyen = selectedTuyen.MaTuyen;
            }

            
            lsvVeXe.Items.Clear();
            DataTable data = VeDAO.Instance.GetALLListTIMKIEM(machuyen, TenKH, SDT, maTuyen);
            loadDuLieuVaoListView(data);
        }

        private void btnShowALL_Click(object sender, EventArgs e)
        {
            load_lsvChuyenXe();
        }


        private void lsvVeXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvVeXe.SelectedItems.Count > 0)
            {
                mave = int.Parse(lsvVeXe.SelectedItems[0].Text);
                txtName.Text = lsvVeXe.SelectedItems[0].SubItems[1].Text;
                txtSDT.Text = lsvVeXe.SelectedItems[0].SubItems[2].Text;
                string ngay = lsvVeXe.SelectedItems[0].SubItems[4].Text;
                string gio = lsvVeXe.SelectedItems[0].SubItems[5].Text;
                string tenloaixe = lsvVeXe.SelectedItems[0].SubItems[6].Text;
                string tenTaixe = lsvVeXe.SelectedItems[0].SubItems[7].Text;
                int giavecu = int.Parse(lsvVeXe.SelectedItems[0].SubItems[10].Text);
                txtGiaVeCu.Text = giavecu.ToString();
                LoaiXe selectedLoaixe = (LoaiXe)cbxLoaiXe.SelectedItem;
                int maLoaiXe = selectedLoaixe.MaLoaixe;
                TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
                
                int giavemoi = selectedTuyen.Gia;

                if (maLoaiXe == 2)
                {
                    giavemoi += 150000;
                }
                if (maLoaiXe == 3)
                {
                    giavemoi += 300000;
                }
                if (maLoaiXe == 4)
                {
                    giavemoi += 200000;
                }
                if (maLoaiXe == 5)
                {
                    giavemoi += 150000;
                }
                txtGiaVeMoi.Text = giavemoi.ToString();
                getTienChenhLech();
                string tentuyen = lsvVeXe.SelectedItems[0].SubItems[3].Text;
                SelectComboBoxValue(cbxTuyenXe, (TuyenXe tuyenXe) => tuyenXe.TenTuyen, tentuyen);
                SelectComboBoxValue(cbxNgayChay, (string ngaychay) => ngaychay, ngay);
                SelectComboBoxValue(cbxGio, (string giochay) => giochay, gio);
                SelectComboBoxValue(cbxLoaiXe, (LoaiXe loaixe) => loaixe.TenLoaiXe, tenloaixe);
                SelectComboBoxValue(cbxTaiXe, (TaiXe taixe) => taixe.TenTaiXe, tenTaixe);
            }
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
        void getTienChenhLech()
        {
            if(!string.IsNullOrEmpty(txtGiaVeCu.Text) && !string.IsNullOrEmpty(txtGiaVeMoi.Text))
            {
                int machuyen = getMachuyen();
                int tienchenhlech = VeDAO.Instance.giavechenhlech(mave, machuyen);
                txtGiaChenhLech.Text = tienchenhlech.ToString();
            }    
        }

        private void btnDoiVe_Click(object sender, EventArgs e)
        {
            if(mave == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 thông tin vé xe để thực hiện đổi vé này!");
            } 
            string TenKH = txtName.Text;
            string SDT =  txtSDT.Text;
            int machuyen = getMachuyen();
            string vittri = txtViTri.Text;
            string TextGiave; 
            int giavechenhlech = int.Parse(txtGiaChenhLech.Text);
            if(giavechenhlech > 0)
            {
                TextGiave = "Giá vé hành khách phải trả thêm là: " + giavechenhlech + "VNĐ";
            }
            else
            {
                if (giavechenhlech < 0)
                {
                    TextGiave = "Giá vé nhà xe phải trả lại cho hành khách là: " + Math.Abs(giavechenhlech) +"VNĐ";
                }
                else
                {
                    TextGiave = "Hành khách Không phải trả chi phí gì thêm!";
                }    
            }
                
            DialogResult result = MessageBox.Show("Xác nhận đổi vé hay không? " + TextGiave, "Xác nhận", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                bool kq = VeDAO.Instance.UpdateVeXe(mave, machuyen, TenKH, SDT, vittri);
                if (kq == true)
                {
                    MessageBox.Show("Đổi vé thành công ");
                    load_lsvChuyenXe();
                    load_PanelSoDoXe();
                }
                else
                {
                    MessageBox.Show("Đổi vé thất bại ");
                }
            }
            else
            {
                return;
            }
            
        }
    }
}
