using QL_XEKHACH.DTO;
using System.Collections.Generic;
using System.Data;


namespace QL_XEKHACH.DAO
{
    internal class TuyenXeDAO
    {
        private static TuyenXeDAO instance;

        public static TuyenXeDAO Instance
        {
            get { if (instance == null) instance = new TuyenXeDAO(); return TuyenXeDAO.instance; }
            private set { TuyenXeDAO.instance = value; }
        }

        private TuyenXeDAO() { }

        public List<TuyenXe> GetAllTuyenXe()
        {
            List<TuyenXe> list = new List<TuyenXe>();

            string query = "SELECT * FROM dbo.AllTuyenXe;";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            
            foreach (DataRow item in data.Rows)
            {
                TuyenXe tuyenxe = new TuyenXe(item);
                list.Add(tuyenxe);
            }
            return list;
        }
        public int GetMaTuyen(string TenTuyen)
        {
            int maTuyen = -1;
            string query = string.Format("SELECT MATUYEN FROM TUYENXE WHERE TENTUYEN = N'{0}'", TenTuyen);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                maTuyen = (int)item["Matuyen"];
            }
            return maTuyen;
        }
        public List<TuyenXe> GetALLLisTuyenTimKiem(string tentuyen, string diemxuatphat, string diemden, int quangduong ,float giatien)
        {
            List<TuyenXe> ListTuyenXe = new List<TuyenXe>();
            string chuoi = "SELECT * FROM dbo.GetTuyenXeFiltered(";
            if (!string.IsNullOrEmpty(tentuyen))
            {
                chuoi += "'" + tentuyen + "',";
            }
            else
            {
                chuoi += "null,";
            }
            if (!string.IsNullOrEmpty(diemxuatphat))
            {
                chuoi += "'" + diemxuatphat + "',";
            }
            else
            {
                chuoi += "null,";
            }
            if (!string.IsNullOrEmpty(diemden))
            {
                chuoi += "'" + diemden + "',";
            }
            else
            {
                chuoi += "null,";
            }
            if (quangduong != 0)
            {
                chuoi += "" + quangduong + ","+giatien+")";
            }
            DataTable data = DataProvider.Instance.ExecuteQuery(chuoi);
            foreach (DataRow item in data.Rows)
            {
                TuyenXe tuyenxe = new TuyenXe(item);
                ListTuyenXe.Add(tuyenxe);
            }
            return ListTuyenXe;
        }
        public bool InsertTuyen(string tentuyen,string diembatdau, string diemden,float giatien, int quangduong)
        {
            string query = string.Format("INSERT INTO TUYENXE (TENTUYEN, DIEMXUATPHAT, DIEMDEN, BANGGIA,QUANGDUONG) VALUES  ( N'{0}', N'{1}', N'{2}',{3},{4})", tentuyen, diembatdau,diemden,giatien,quangduong);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateTuyen(int matuyen ,string tentuyen, string diembatdau, string diemden, float giatien, int quangduong)
        {
            string query = "UPDATE TUYENXE SET ";
            if (!string.IsNullOrEmpty(diembatdau))
            {
                query += "DIEMXUATPHAT = N'" + diembatdau + "',";
            }
            if (!string.IsNullOrEmpty(diemden))
            {
                query += "DIEMDEN = N'" + diemden + "',";
            }
            if (giatien!=0)
            {
                query += "BANGGIA = " + giatien + ",";
            }
            if (quangduong != 0)
            {
                query += "QUANGDUONG = " + quangduong + ",";
            }
            query += "TENTUYEN = N'" + tentuyen + "' WHERE MATUYEN = " + matuyen + ";";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteTuyen(int maTuyen)
        {
            string query = string.Format("DELETE FROM TUYENXE WHERE MATUYEN ='{0}'", maTuyen);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
