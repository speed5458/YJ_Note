using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace Note_Storage
{
    public partial class NotePad : DevExpress.XtraEditors.XtraUserControl
    {
        private String Constr = "server=127.0.0.1;uid=cyj;pwd=admin1234;database=My_Toy";
        private String _Category;
        private DataSet _ds;

        public NotePad()
        {
            InitializeComponent();
        }
        public NotePad(String Category)
        {
            InitializeComponent();
            this._Category = Category;
            Data_Load();
            ComboBox_binding();
        }
        private void ComboBox_binding() //
        {
            List<String> Deduplication_Big = new List<string>();
            foreach (DataTable dt in _ds.Tables)//대분류
            {
                foreach (DataRow dr in dt.Rows) //Row 하나씩 빼주면서 해당 열에 추가해준다.
                {
                    if (!Deduplication_Big.Contains(dr["Big_Category"].ToString())) //해당내용이 없으면
                    {
                        Deduplication_Big.Add(dr["Big_Category"].ToString());
                    }         
                }
            }
            comboBoxEdit1.Properties.Items.Clear();
            comboBoxEdit1.Properties.Items.AddRange(Deduplication_Big.ToArray());

            List<String> Deduplication_Middle = new List<string>();
            foreach (DataTable dt in _ds.Tables)//대분류
            {
                foreach (DataRow dr in dt.Rows) //Row 하나씩 빼주면서 해당 열에 추가해준다.
                {
                    if (!Deduplication_Middle.Contains(dr["Middle_Category"].ToString())) //해당내용이 없으면
                    {
                        Deduplication_Middle.Add(dr["Middle_Category"].ToString());
                    }
                }
            }
            comboBoxEdit2.Properties.Items.Clear();
            comboBoxEdit2.Properties.Items.AddRange(Deduplication_Middle.ToArray());

            List<String> Deduplication_Small = new List<string>();
            foreach (DataTable dt in _ds.Tables)//대분류
            {
                foreach (DataRow dr in dt.Rows) //Row 하나씩 빼주면서 해당 열에 추가해준다.
                {
                    if (!Deduplication_Small.Contains(dr["Small_Category"].ToString())) //해당내용이 없으면
                    {
                        Deduplication_Small.Add(dr["Small_Category"].ToString());
                    }
                }
            }
            comboBoxEdit3.Properties.Items.Clear();
            comboBoxEdit3.Properties.Items.AddRange(Deduplication_Small.ToArray());
        }

        private void Data_Load()
        {
            var Conn = new SqlConnection(Constr);
            Conn.Open();

            SqlCommand sc = new SqlCommand();
            sc.Connection = Conn;
            sc.CommandText = "SELECT * FROM Programming WHERE Division_Category = '" + _Category + "'"; // ' 필요하다
            sc.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter();
            _ds = new DataSet();

            sda.SelectCommand = sc;
            sda.Fill(_ds,"Load");
            Conn.Close();
            gridControl1.DataSource = _ds.Tables["Load"];
            gridView1.BestFitColumns();

        }


        /// <summary>
        /// 글쓰기 클릭버튼, 클릭시 새로운 폼이 뜨고 대중소 구분하여 글을 쓴다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            Memo_Form memo = new Memo_Form(_Category);
            memo.ShowDialog();
        }

        /// <summary>
        /// 검색버튼 클릭시 발생이벤트,
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var Conn = new SqlConnection(Constr);
            Conn.Open();

            SqlCommand sc = new SqlCommand();
            sc.Connection = Conn;
            sc.CommandText = "usp_Programming_get";
            sc.CommandType = CommandType.StoredProcedure;

            sc.Parameters.Add("@pDvision_Category", SqlDbType.NVarChar).Value = _Category; //메뉴선택시 받아와야한다.
            sc.Parameters.Add("@pBig_Category", SqlDbType.NVarChar).Value = comboBoxEdit1.Text; //대구분
            sc.Parameters.Add("@pMiddle_Category", SqlDbType.NVarChar).Value = comboBoxEdit2.Text; //중구분
            sc.Parameters.Add("@pSmall_Category", SqlDbType.NVarChar).Value = comboBoxEdit3.Text; //소구분
            sc.Parameters.Add("@pTitle", SqlDbType.NVarChar).Value = textEdit1.Text; //상세구분

            
            SqlDataAdapter sda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            sda.SelectCommand = sc;
            sda.Fill(ds, "Search");
            gridControl1.DataSource = ds.Tables["Search"];
            Conn.Close();
        }

        /// <summary>
        /// 삭제버튼 클릭시 삭제된다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            String Seq_Id = dr["Seq_Id"].ToString();

            var Conn = new SqlConnection(Constr);
            Conn.Open();

            SqlCommand sc = new SqlCommand();
            sc.Connection = Conn;
            sc.CommandText = "Delete FROM Programming WHERE Seq_Id = '" + Seq_Id + "'"; // ' 필요하다
            sc.CommandType = CommandType.Text;

            sc.ExecuteNonQuery();
            Conn.Close();

        }

        /// <summary>
        /// GridView 더블클릭이벤트, 더블클릭하게 되면 선택된 열의 정보를 갖고와 뿌려준다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            String Seq_Id = dr["Seq_Id"].ToString();
            var Conn = new SqlConnection(Constr);
            Conn.Open();
            SqlCommand sc = new SqlCommand();
            sc.Connection = Conn;
            sc.CommandText = "SELECT * FROM Programming WHERE Seq_Id = '" + Seq_Id + "'"; // ' 필요하다
            sc.CommandType = CommandType.Text;

            SqlDataAdapter sda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            sda.SelectCommand = sc;
            sda.Fill(ds);

            Memo_Form memo = new Memo_Form(ds); //memoForm으로 DataSet을 날려준다.
            memo.ShowDialog();

        }

    }
}
