using QL_XEKHACH.DAO;
using QL_XEKHACH.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QL_XEKHACH.UserControls
{
    public partial class Page_XeLuxury : UserControl
    {
        public event EventHandler<ButtonClickEventArgs> ButtonClick;
        private List<VeXe> danhSachViTri;
        public Page_XeLuxury(int matuyen, int maloaixe, string ngaychay, int mataixe)
        {
            InitializeComponent();
            danhSachViTri = VeDAO.Instance.getAllVeXeFormChuyen(matuyen, maloaixe, ngaychay, mataixe);
            WireUpButtons();
        }
        private void WireUpButtons()
        {
            foreach (Control control in panel1.Controls)
            {
                if (control is Button button)
                {
                    loadViTriDaDat(button);
                }
            }
            foreach (Control control in panel2.Controls)
            {
                if (control is Button button)
                {
                    loadViTriDaDat(button);
                }
            }
        }
        private void loadViTriDaDat(Button btn)
        {

            btn.Click += Button_Click;
            btn.Click -= CancelButton_Click;
            btn.Cursor = Cursors.Hand;

            foreach (VeXe vexe in danhSachViTri)
            {
                if (btn.Text == vexe.ViTri.ToString())
                {
                    btn.BackColor = Color.Red;
                    btn.Click -= Button_Click;
                    btn.Click += CancelButton_Click;
                }
            }
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("vị trị này đã có người đặt");
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string buttonName = clickedButton.Text;
            ButtonClick?.Invoke(this, new ButtonClickEventArgs(buttonName));
        }
    }
}
