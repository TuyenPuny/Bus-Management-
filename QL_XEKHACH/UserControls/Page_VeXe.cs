using QL_XEKHACH.DAO;
using QL_XEKHACH.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace QL_XEKHACH.UserControls
{
    public partial class Page_VeXe : UserControl
    {
        private NhanVien loginAccount;

        internal NhanVien LoginAccount
        {
            get => loginAccount;
            set { loginAccount = value; }
        }

        public Page_VeXe(NhanVien nv)
        {
            InitializeComponent();
            this.LoginAccount = nv;

        }
        private void Page_VeXe_Load(object sender, EventArgs e)
        {
            LoadTuyenToComboBox();
           
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
            int giave = selectedTuyen.Gia;
            txtGiaVe.Text = giave.ToString();

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
            int maTuyen = selectedTuyen.MaTuyen;
            int giave = selectedTuyen.Gia;
           
            if(maLoaiXe == 2)
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
           
            txtGiaVe.Text = giave.ToString();

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
            Load_soDoXe();
        }
        void Load_soDoXe()
        {
            TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
            int maTuyen = selectedTuyen.MaTuyen;
            string gioxuatphat = cbxNgayChay.SelectedItem.ToString() + " " + cbxGio.SelectedItem.ToString();
            TaiXe selectedTaiXe = (TaiXe)cbxTaiXe.SelectedItem;
            int mataixe = selectedTaiXe.MaTaiXe;
            LoaiXe selectedLoaixe = (LoaiXe)cbxLoaiXe.SelectedItem;
            int maLoaiXe = selectedLoaixe.MaLoaixe;
            loadBienSoXe(mataixe);
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
        private void btnAdd_Click(object sender, EventArgs e)
        {

            TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
            string tenTuyen = selectedTuyen.TenTuyen;
            string gioxuatphat = cbxNgayChay.SelectedItem.ToString() + " " + cbxGio.SelectedItem.ToString();
            string TenKH = txtName.Text;
            string SDT = txtSDT.Text;
            if(string.IsNullOrEmpty(SDT))
            {
                MessageBox.Show("Vui Lòng nhập số điện thoại!");
                return;
            }    
            string GhiChu = rtbGhiChu.Text;
            string TinhTrang = cbxTinhTrang.Text;
            if (string.IsNullOrEmpty(TinhTrang))
            {
                TinhTrang = "Đang giữ chỗ";
            }
            string ViTri = txtViTri.Text;
            if (string.IsNullOrEmpty(ViTri))
            {
                MessageBox.Show("Vui Lòng chọn vị trí chỗ ngồi!");
                return;
            }
            int machuyen = getMachuyen();
            int manv = loginAccount.MaNV;

            DialogResult result = MessageBox.Show("Xác nhận đặt vé xe đi từ " + tenTuyen+ " vào " + gioxuatphat+ " hay không ? ", "Xác nhận", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                bool kq = VeDAO.Instance.InsertVeXe(manv, machuyen, TenKH, SDT, ViTri, GhiChu, TinhTrang);
                if (kq == true)
                {
                    MessageBox.Show("Đặt vé thành công ");
                    Load_soDoXe();  
                }
                else
                {
                    MessageBox.Show("Đặt vé thất bại ");
                }
            }
            else
            {
                return;
            }
        }
    }
}
