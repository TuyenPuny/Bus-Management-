using QL_XEKHACH.DTO;
using System.Collections.Generic;
using System.Data;

namespace QL_XEKHACH.DAO
{
    internal class ChuyenXeDAO
    {
        
        private static ChuyenXeDAO instance;

        public static ChuyenXeDAO Instance
        {
            get { if (instance == null) instance = new ChuyenXeDAO(); return instance; }
            private set { instance = value; }
        }
        private ChuyenXeDAO() { }
        public List<ChuyenXe> GetAllChuyenXe()
        {
            List<ChuyenXe> list = new List<ChuyenXe>();

            string query = "SELECT * FROM dbo.ActiveChuyenXeView;";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                ChuyenXe chuyenxe = new ChuyenXe(item);
                list.Add(chuyenxe);
            }
            return list;
        }
        public int GetMAChuyen(int maloaixe,int matuyen,string ngaychay,int mataixe)
        {
            string query = string.Format("SELECT dbo.GetMACHUYEN ({0},{1},'{2}',{3}) AS MACHUYENXE", maloaixe,matuyen, ngaychay, mataixe);
            int machuyen = 0;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                machuyen = (int)item["MACHUYENXE"];
            }
            return machuyen;
        }
        public List<ChuyenXe> GetChuyenXeFromTuyenXe(int maTuyen)
        {
            List<ChuyenXe> chuyenxeList = new List<ChuyenXe>();
            string query = "SELECT * FROM dbo.GetChuyenXeByMaTuyen( @MaTuyen )";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maTuyen });
            foreach (DataRow item in data.Rows)
            {
                ChuyenXe chuyenxe = new ChuyenXe(item);
                chuyenxeList.Add(chuyenxe);
            }
            return chuyenxeList;
        }
        
        public List<string> GetNgayChayFromTuyenXe(int maTuyen)
        {
            List<string> NgayChayList = new List<string>();
            string query = string.Format("USP_GetNgayXuatPhatFromTuyenXe {0}", maTuyen);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                string ngaythangnam = item["NgayThangNam"].ToString();
                NgayChayList.Add(ngaythangnam);
            }
            return NgayChayList;
        }
        public List<string> GetGioChayFromTuyenXe(int maTuyen,string NgayThangNam)
        {
            List<string> GioPhutList = new List<string>();
            string query = string.Format("EXEC USP_GetGioFromTuyenXe {0},'{1}'",maTuyen,NgayThangNam);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                string giophutString = item["GioPhut"].ToString();
                GioPhutList.Add(giophutString);
            }
            return GioPhutList;
        }
        public List<ChuyenXe> GetAllistChuyenTimKiem(int matuyen, string gioxuatphat, string gioden, int mataixe, int maloaixe,int maxe)
        {
            List<ChuyenXe> ListChuyenXe = new List<ChuyenXe>();
            string chuoi = "SELECT * FROM CHUYENXE CX " +
                "JOIN XE ON XE.MAXE = CX.MAXE " +
                "WHERE CX.GIOXUATPHAT > GETDATE()";
            if (matuyen != 0)
            {
                chuoi += " AND MATUYEN = " + matuyen + "";
            }
            if (maxe != 0)
            {
                chuoi += " AND XE.MAXE = " + maxe + "";
            }
            if (!string.IsNullOrEmpty(gioxuatphat))
            {
                chuoi += " AND GIOXUATPHAT = '" + gioxuatphat + "'";
            }
            if (!string.IsNullOrEmpty(gioden))
            {
                chuoi += " AND GIODEN = '" + gioden + "'";
            }
            if (maloaixe != 0)
            {
                chuoi += " AND MALOAIXE = " + maloaixe + "";
            }
            if (mataixe != 0)
            {
                chuoi += " AND MATAIXE = " + mataixe + "";
            }
            DataTable data = DataProvider.Instance.ExecuteQuery(chuoi);

            foreach (DataRow item in data.Rows)
            {
                ChuyenXe chuyenxe = new ChuyenXe(item);
                ListChuyenXe.Add(chuyenxe);
            }
            return ListChuyenXe;
        }

        public bool InserChuyen(int matuyen, int maxe, string giochay, string gioden, int mataixe)
        {
            string query = string.Format("INSERT INTO CHUYENXE (MATUYEN, MAXE, GIOXUATPHAT, GIODEN,MATAIXE) VALUES  ( {0}, {1}, '{2}','{3}',{4})", matuyen, maxe, giochay, gioden, mataixe);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateChuyen(int machuyen ,int maxe, string giochay, string gioden, int mataixe)
        {
            string query = "UPDATE CHUYENXE SET ";
           
            if (maxe != 0)
            {
                query += "MAXE = " + maxe + ",";
            }
            if (mataixe != 0)
            {
                query += "MATAIXE = " + mataixe + ",";
            }
            query += "GIOXUATPHAT = '" + giochay + "',GIODEN = N'" + gioden + "' WHERE MACHUYENXE = " + machuyen + ";";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteChuyen(int machuyen)
        {
            string query = string.Format("DELETE FROM CHUYENXE WHERE MACHUYENXE ='{0}'", machuyen);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}

