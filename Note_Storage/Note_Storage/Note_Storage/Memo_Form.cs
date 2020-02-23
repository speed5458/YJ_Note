using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace Note_Storage
{
    public partial class Memo_Form : DevExpress.XtraEditors.XtraForm
    {
        private String Constr = "server=127.0.0.1;uid=cyj;pwd=admin1234;database=My_Toy";
        private String _Category;
        DataSet _ds; //NotePad에서 선택되어 조회된 데이터테이블


        public Memo_Form() //작성누르면 활성
        {
            InitializeComponent();
            
        }
        public Memo_Form(String Category) //기존에 있는데이터 확인하고싶으면 활성.
        {
            InitializeComponent();
            this._Category = Category;
            simpleButton1.Visible = false;
        }
        public Memo_Form(DataSet ds)
        {
            InitializeComponent();
            simpleButton2.Visible = false;
            this._ds = ds;
            Data_Load();
        }

        /// <summary>
        /// 작성버튼 클릭시 발생이벤트, 대중소 나눠서 입력해준다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        { ///insert
            var Conn = new SqlConnection(Constr);
            Conn.Open();

            SqlCommand sc = new SqlCommand();
            sc.Connection = Conn;
            sc.CommandText = "usp_Programming_iud";
            sc.CommandType = CommandType.StoredProcedure;

            sc.Parameters.Add("@pDvision_Category", SqlDbType.NVarChar).Value = _Category; //메뉴선택시 받아와야한다.
            sc.Parameters.Add("@pBig_Category", SqlDbType.NVarChar).Value = textEdit2.Text; //대구분
            sc.Parameters.Add("@pMiddle_Category", SqlDbType.NVarChar).Value = textEdit3.Text; //중구분
            sc.Parameters.Add("@pSmall_Category", SqlDbType.NVarChar).Value = textEdit4.Text; //소구분
            sc.Parameters.Add("@pTitle", SqlDbType.NVarChar).Value = textEdit1.Text; //상세구분
            sc.Parameters.Add("@pContents", SqlDbType.NVarChar).Value = memoEdit1.Text; //상세구분
            sc.Parameters.Add("@pWorkTag", SqlDbType.NVarChar).Value = "IN";

            sc.ExecuteNonQuery();

            //SqlDataAdapter sda = new SqlDataAdapter();
            //sda.SelectCommand = sc;

            this.Close();
            
        }

        private void Data_Load()
        {
            
            DataTable dt = _ds.Tables[0];
            textEdit2.Text = dt.Rows[0]["Big_Category"].ToString();
            textEdit3.Text = dt.Rows[0]["Middle_Category"].ToString();
            textEdit4.Text = dt.Rows[0]["Small_Category"].ToString();   
            textEdit1.Text = dt.Rows[0]["Title"].ToString();
            memoEdit1.Text = dt.Rows[0]["Contents"].ToString();
        }


        /// <summary>
        /// 수정버튼 클릭, 버튼클릭시 내용이 update 된다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var Conn = new SqlConnection(Constr);
            Conn.Open();

            SqlCommand sc = new SqlCommand();
            sc.Connection = Conn;
            sc.CommandText = "usp_Programming_iud";
            sc.CommandType = CommandType.StoredProcedure;

            _ds.Tables[0].Rows[0]["Seq_Id"].ToString();

            sc.Parameters.Add("@pSeq_Id", SqlDbType.NVarChar).Value = _ds.Tables[0].Rows[0]["Seq_Id"].ToString();
            sc.Parameters.Add("@pBig_Category", SqlDbType.NVarChar).Value = textEdit2.Text; //대구분
            sc.Parameters.Add("@pMiddle_Category", SqlDbType.NVarChar).Value = textEdit3.Text; //중구분
            sc.Parameters.Add("@pSmall_Category", SqlDbType.NVarChar).Value = textEdit4.Text; //소구분
            sc.Parameters.Add("@pTitle", SqlDbType.NVarChar).Value = textEdit1.Text; //상세구분
            sc.Parameters.Add("@pContents", SqlDbType.NVarChar).Value = memoEdit1.Text; //상세구분
            sc.Parameters.Add("@pWorkTag", SqlDbType.NVarChar).Value = "UP";

            sc.ExecuteNonQuery();

            //SqlDataAdapter sda = new SqlDataAdapter();
            //sda.SelectCommand = sc;
            this.Close();
        }
    }
}