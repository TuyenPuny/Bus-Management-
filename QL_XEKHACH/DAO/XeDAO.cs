using QL_XEKHACH.DTO;
using System.Collections.Generic;
using System.Data;


namespace QL_XEKHACH.DAO
{
    internal class XeDAO
    {
        private static XeDAO instance;

        public static XeDAO Instance
        {
            get { if (instance == null) instance = new XeDAO(); return instance; }
            private set { instance = value; }
        }

        private XeDAO() { }

        public List<Xe> GetALLXe()
        {
            List<Xe> listXe = new List<Xe>();

            string query = "SELECT * FROM XE WHERE TRANGTHAI <> N'Dừng hoạt động';";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Xe xe = new Xe(item);
                listXe.Add(xe);
            }
            return listXe;
        }
        public List<Xe> GetALLXeFromMaLoaiXe(int maloaixe)
        {
            List<Xe> listXe = new List<Xe>();

            string query = string.Format("SELECT * FROM XE WHERE TRANGTHAI <> N'Dừng hoạt động' AND  XE.MALOAIXE = {0};", maloaixe);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Xe xe = new Xe(item);
                listXe.Add(xe);
            }
            return listXe;
        }
        public bool InsertXe(string biensoxe, string trangthai, int maloaixe)
        {
            string query = string.Format("INSERT INTO XE ( BIENSOXE,TRANGTHAI ,MALOAIXE ) VALUES ( '{0}', N'{1}', {2})", biensoxe, trangthai,maloaixe);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateXe(string MAXE, string BIENSOXE, string TRANGTHAI, int MALOAIXE)
        {
            string query = "UPDATE XE SET";
            if(!string.IsNullOrEmpty(BIENSOXE))
            {
                query += " BIENSOXE = '" + BIENSOXE + "',";
            }
            if (MALOAIXE !=0)
            {
                query += " MALOAIXE = " + MALOAIXE + ",";
            }
            query += "TRANGTHAI = N'" + TRANGTHAI + "' WHERE MAXE = " + MAXE;

            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteXe(string MAXE)
        {
            string query = string.Format("UPDATE XE SET TRANGTHAI = N'Dừng hoạt động' WHERE MAXE = {0}", MAXE);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public string GetBienSoXeFromChuyenXe(int matuyen,int maloaixe,string ngayxuatphat,int mataixe)
        {
            string query = string.Format("EXEC GetBienSoByMaTuyenAndGioXuatPhat {0},{1},'{2}',{3}", matuyen, maloaixe, ngayxuatphat, mataixe);
            string BienSoXe = "";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                BienSoXe = item["BIENSOXE"].ToString();
            }
            return BienSoXe;
        }


        public List<Xe> GetALLListXeTimKiem(int maxe, string bienso, string trangthai, int maloaixe)
        {
            List<Xe> ListXe = new List<Xe>();
            string chuoi = "SELECT * FROM XE WHERE 1 = 1 AND TRANGTHAI <> N'Dừng hoạt động'";
            if (maxe!= 0)
            {
                chuoi += " AND MAXE = " + maxe;
            }
            if (!string.IsNullOrEmpty(bienso))
            {
                chuoi += " AND BIENSOXE LIKE '%" + bienso + "%'";
            }
            if (!string.IsNullOrEmpty(trangthai))
            {
                chuoi += " AND TRANGTHAI = N'" + trangthai + "'";
            }
            if (maloaixe != 0)
            {
                chuoi += " AND MALOAIXE = " + maloaixe;
            }
            DataTable data = DataProvider.Instance.ExecuteQuery(chuoi);

            foreach (DataRow item in data.Rows)
            {
                Xe nhanvien = new Xe(item);
                ListXe.Add(nhanvien);
            }
            return ListXe;
        }

    }
}
