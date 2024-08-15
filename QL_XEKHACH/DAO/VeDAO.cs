using QL_XEKHACH.DTO;
using System.Collections;
using System.Collections.Generic;
using System.Data;
namespace QL_XEKHACH.DAO
{
    internal class VeDAO
    {
        private static VeDAO instance;
        public static VeDAO Instance
        { 
            get { if (instance == null) instance = new VeDAO(); return VeDAO.instance; }
            private set { instance = value; }   
        }
        private VeDAO()
        {
        }
        public List<VeXe> GetAllVeXe()
        {
            List<VeXe> listVeXe = new List<VeXe>();

            string query = "select * from VEXE";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                VeXe vexe = new VeXe(item);
                listVeXe.Add(vexe);
            }
            return listVeXe;
        }
        public List<VeXe> GetAllTTVeXe()
        {
            List<VeXe> listVeXe = new List<VeXe>();

            string query = "SELECT * FROM dbo.ViewAllTTVeXe;";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                VeXe vexe = new VeXe(item);
                listVeXe.Add(vexe);
            }
            return listVeXe;
        }
        public List<VeXe> getAllVeXeFormChuyen(int matuyen,int maloaixe,string ngaychay,int mataixe)
        {
            List<VeXe> listVeXe = new List<VeXe>();

            string query = string.Format("Exec USP_DSVeXe {0},{1},'{2}',{3}", matuyen, maloaixe, ngaychay, mataixe);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                VeXe vexe = new VeXe(item);
                listVeXe.Add(vexe);
            }
            return listVeXe;
        }
        public bool InsertVeXe(int manv, int machuyen, string tenkh,string sdt, string vitri, string ghichu, string tinhtrang)
        {
            string query = string.Format("INSERT INTO VeXe ( MANV, MACHUYENXE,TENKH,SDT,VITRI,GHICHU,TINHTRANG) VALUES  ( {0}, {1}, N'{2}','{3}', '{4}',N'{5}',N'{6}')", manv, machuyen, tenkh,sdt, vitri, ghichu, tinhtrang);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateVeXe(int mave, int machuyen, string tenkh, string sdt, string vitri)
        {
            string query = "UPDATE VeXe SET ";
            if (machuyen != 0)
            {
                query += "MACHUYENXE = " + machuyen + ",";
            }
            
            if (!string.IsNullOrEmpty(tenkh))
            {
                query += "TENKH = N'" + tenkh + "',";
            }
            if (!string.IsNullOrEmpty(sdt))
            {
                query += "SDT = '" + sdt + "',";
            }
            query += "VITRI = '"+ vitri+"' WHERE MAVE =" + mave + "";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteVeXe(int mave)
        {
            string query = string.Format("Detele Vexe Where MAVE = {0}", mave);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public int giavechenhlech(int mave, int machuyen)
        {
            int giave = 0;
            string chuoi = string.Format("Select dbo.UpdateVeXeWithFareDifference ( " + mave + "," + machuyen + " )  AS GIAVECHENHLECH");
            DataTable data = DataProvider.Instance.ExecuteQuery(chuoi);
            foreach (DataRow item in data.Rows)
            {
                giave = (int)item["GIAVECHENHLECH"];
            }
            return giave;

        }
        public DataTable GetALLListTIMKIEM(int machuyen, string tenkh, string sdt, int matuyen)
        {
            string chuoi = string.Format("SELECT * FROM dbo.GetVeXeFiltered("+ @machuyen +", N'"+ tenkh + "', '"+ sdt + "', "+ matuyen + ");");
            DataTable data = DataProvider.Instance.ExecuteQuery(chuoi);
            return data;
        }
        public DataTable GetALLListTIMKIEMHUYVE(int mave, int maTuyen, int maloaixe,string ngaychay,string TenKH,string SDT,string TinhTrang)
        {
            string chuoi = "SELECT * FROM dbo.GetVeXeFilteredByConditions( @mave , @maTuyen , @maloaixe ,";
            if (!string.IsNullOrEmpty(ngaychay))
            {
                chuoi += "'"+ngaychay+"' ,";
            }
            else
            {
                chuoi += " null ,";
            }
            if (!string.IsNullOrEmpty(TenKH))
            {
                chuoi += " N'"+ TenKH + "' ,";
            }
            else
            {
                chuoi += " null ,";
            }
            if (!string.IsNullOrEmpty(SDT))
            {
                chuoi += "'"+ SDT + "' ,";
            }
            else
            {
                chuoi += " null ,";
            }
            if (!string.IsNullOrEmpty(TinhTrang))
            {
                chuoi += " N'"+ TinhTrang + "')";
            }
            else
            {
                chuoi += " null)";
            }
            DataTable data = DataProvider.Instance.ExecuteQuery(chuoi, new object[] { mave, maTuyen, maloaixe });
            return data;
        }
        public bool HuyVe(int mave)
        {
            string query = string.Format("DELETE FROM VEXE WHERE MAVE ='{0}'", mave);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool ThanhToan(int mave)
        {
            string query = string.Format("ThanhToanVeXe {0}", mave);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
