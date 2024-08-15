using QL_XEKHACH.DAO;
using QL_XEKHACH.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization; 

using System.Windows.Forms;


namespace QL_XEKHACH.UserControls
{
    public partial class Page_HuyVe : UserControl
    {
        public Page_HuyVe()
        {
            InitializeComponent();
        }
        private void Page_HuyVe_Load(object sender, EventArgs e)
        {
            lsvVeXe.View = View.Details;
            taoColumn();
            load_lsvChuyenXe();
            LoadTuyenToComboBox();
            load_LoaiXeToComboBox();
        }
        void load_LoaiXeToComboBox()
        {
            List<LoaiXe> Loaixe = new List<LoaiXe>
            {
                new LoaiXe(1, "Thường" ,41),
                new LoaiXe(2, "Luxury",36),
                new LoaiXe(3, "Limousine",22),
                new LoaiXe(4, "Xe 29 chỗ",29),
                new LoaiXe(5, "Xe 16 chỗ",16),
            };

            cbxLoaiXe.DataSource = Loaixe;
            cbxLoaiXe.DisplayMember = "tenLoaiXe";
            cbxLoaiXe.ValueMember = "maLoaixe";
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
                item.SubItems.Add(row["BANGGIA"].ToString());
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
            string query = "SELECT * FROM ViewAllTTVeXe;";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            loadDuLieuVaoListView(data);
        }

        private void lsvVeXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvVeXe.SelectedItems.Count > 0)
            {   
                txtMaVe.Text = lsvVeXe.SelectedItems[0].Text;

                txtName.Text = lsvVeXe.SelectedItems[0].SubItems[1].Text;
                txtSDT.Text = lsvVeXe.SelectedItems[0].SubItems[2].Text;
                cbxTinhTrang.Text = lsvVeXe.SelectedItems[0].SubItems[9].Text;
                string ngaychay  = lsvVeXe.SelectedItems[0].SubItems[4].Text;
                string dateFormat = "yyyy-MM-dd";
                DateTime ngayThang = DateTime.ParseExact(ngaychay, dateFormat, CultureInfo.InvariantCulture);
                dtpNgayChay.Value = ngayThang;
                double giave = double.Parse(lsvVeXe.SelectedItems[0].SubItems[10].Text);
                txtGiaVe.Text = ((int)giave).ToString();
                DateTime ngayHienTai = DateTime.Now;
                TimeSpan ketQua = (ngayThang - ngayHienTai);
                if ((int)(ketQua.TotalDays) > 3)
                {
                    txtHoanTien.Text = giave.ToString();
                }
                else
                {
                    double giaSauGiamGia = giave - (giave * 0.1);
                    txtHoanTien.Text = ((int)giaSauGiamGia).ToString();
                }    
            }
        }
        void LoadTuyenToComboBox()
        {
            List<TuyenXe> listTuyenXe = TuyenXeDAO.Instance.GetAllTuyenXe();    
            cbxTuyenXe.DataSource = listTuyenXe;
            cbxTuyenXe.DisplayMember = "TenTuyen";
            cbxTuyenXe.ValueMember = "MaTuyen";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            int mave = 0;
            int.TryParse(txtMaVe.Text, out mave);
            int maTuyen = 0;
            int maloaixe = 0;
            string ngaychay = string.Empty;
            string TenKH = string.Empty;
            string SDT = string.Empty;
            string TinhTrang = string.Empty;
            if (checkBoxTuyenXe.Checked)
            {
                TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
                maTuyen = selectedTuyen.MaTuyen;
            }
            if (checkBoxLoaiXe.Checked)
            {
                LoaiXe selectedLoaixe = (LoaiXe)cbxLoaiXe.SelectedItem;
                maloaixe = selectedLoaixe.MaLoaixe;
            }
            if (checkBoxNgayXuatPhat.Checked)
            {
                ngaychay = dtpNgayChay.Text;
            }
            if (checkBoxTTKhachHang.Checked)
            {
                TenKH = txtName.Text;
                SDT = txtSDT.Text;
                TinhTrang = cbxTinhTrang.Text;
            }
            lsvVeXe.Items.Clear();
            DataTable data = VeDAO.Instance.GetALLListTIMKIEMHUYVE(mave, maTuyen, maloaixe, ngaychay, TenKH, SDT, TinhTrang);
            loadDuLieuVaoListView(data);

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            load_lsvChuyenXe();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            int mave = 0;
            int.TryParse(txtMaVe.Text, out mave);
            if(mave == 0)
            {
                MessageBox.Show("Chọn 1 hành hành khách để thực hiện tác vụ này!");
                return;

            }
            else
            {
                TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
                string tenTuyen = selectedTuyen.TenTuyen;
                string gioxuatphat = dtpNgayChay.Text;
                string TenKH = txtName.Text;
                string giave = txtGiaVe.Text;
                string tinhtrang = cbxTinhTrang.Text;
                if (tinhtrang == "Đã thanh toán")
                {
                    MessageBox.Show("Khách hành này đã thanh toàn rồi!");
                    return;
                }
                else
                {
                    DialogResult result = MessageBox.Show("Xác nhận thanh toán vé xe của hành khách " + TenKH + " đi từ " + tenTuyen + " vào lúc " + gioxuatphat + " hay không ? Số tiền hành khách phải thanh toán là: " + giave + " VNĐ", "Xác nhận", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        bool kq = VeDAO.Instance.ThanhToan(mave);
                        if (kq == true)
                        {
                            MessageBox.Show("Thanh toán thành công ");
                            load_lsvChuyenXe();
                        }
                        else
                        {
                            MessageBox.Show("Thanh toán thất bại ");
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }    
        }

        private void btnHuyVe_Click(object sender, EventArgs e)
        {
            int mave = 0;
            int.TryParse(txtMaVe.Text, out mave);
            if (mave == 0)
            {
                MessageBox.Show("Chọn 1 hành hành khách để thực hiện tác vụ này!");
                return;
            }
            {
                TuyenXe selectedTuyen = (TuyenXe)cbxTuyenXe.SelectedItem;
                string tenTuyen = selectedTuyen.TenTuyen;
                string gioxuatphat = dtpNgayChay.Text;
                string TenKH = txtName.Text;
                string tinhtrang = cbxTinhTrang.Text;
                string tentra = "";
                if (tinhtrang == "Đã thanh toán")
                {
                    string hoantienve = txtHoanTien.Text;
                    tentra = "Số tiền nhà xe phải hoàn tiền cho khách là: " + hoantienve + " VNĐ ";
                }    
                DialogResult result = MessageBox.Show("Xác nhận huỷ vé xe của hành khách " + TenKH + " đi từ " + tenTuyen + " vào lúc " + gioxuatphat + " hay không ? " + tentra, "Xác nhận", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    bool kq = VeDAO.Instance.HuyVe(mave);
                    if (kq == true)
                    {
                        MessageBox.Show("Huỷ vé thành công ");
                        load_lsvChuyenXe();
                    }
                    else
                    {
                        MessageBox.Show("Huỷ vé thất bại ");
                    }
                }
                else
                {
                    return;
                }
            }
            
        }
    }
}
