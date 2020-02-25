using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraTab;

namespace Note_Storage
{
    public partial class Form1 : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        private String Constr = "server=127.0.0.1;uid=cyj;pwd=admin1234;database=My_Toy";

        public Form1()
        {

            InitializeComponent();
            Menu_Elements_Manager();
        }


        private void Menu_Elements_Manager()
        {

            accordionControl1.Elements.Clear();
            AccordionControlElement G_Category1 = new AccordionControlElement(ElementStyle.Group) { Text = "프로그램" };
           // accordionControl1.Elements.Add(G_Category1);
            AccordionControlElement I_Category1_1 = new AccordionControlElement(ElementStyle.Item) { Text = "Java" };
            I_Category1_1.Name = "Java";
            AccordionControlElement I_Category1_2 = new AccordionControlElement(ElementStyle.Item) { Text = "C#" };
            I_Category1_2.Name = "C#";
            AccordionControlElement I_Category1_3 = new AccordionControlElement(ElementStyle.Item) { Text = "DB" };
            I_Category1_3.Name = "DB";

            AccordionControlElement G_Category2 = new AccordionControlElement(ElementStyle.Group) { Text = "최영준" };
            AccordionControlElement I_Category2_1 = new AccordionControlElement(ElementStyle.Item) { Text = "개인노트" };
            I_Category2_1.Name = "개인노트";
            AccordionControlElement I_Category2_2 = new AccordionControlElement(ElementStyle.Item) { Text = "Calender" };
            I_Category2_2.Name = "Calender";

            //AccordionControlElement G_Category3 = new AccordionControlElement(ElementStyle.Group) { Text = "일상" };
            //AccordionControlElement I_Category3_1 = new AccordionControlElement(ElementStyle.Item) { Text = "맛집" };
            //AccordionControlElement I_Category3_2 = new AccordionControlElement(ElementStyle.Item) { Text = "여행" };
            //AccordionControlElement I_Category3_3 = new AccordionControlElement(ElementStyle.Item) { Text = "기타" };


            G_Category1.Elements.AddRange(new AccordionControlElement[] { I_Category1_1, I_Category1_2, I_Category1_3 });
            G_Category2.Elements.AddRange(new AccordionControlElement[] { I_Category2_1, I_Category2_2});

    
            accordionControl1.Elements.AddRange(new AccordionControlElement[] { G_Category1, G_Category2}); //대그룹 Elements 아코디언 

            //대 중 소 그룹 세분화후 accordionControl 에 넣고 중 소 넣자
            //accordionControl1.AllowItemSelection = true;
            accordionControl1.ExpandAll();
        }


        /// <summary>
        /// TabPageHeader CloseButton클릭시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
            (arg.Page as XtraTabPage).PageVisible = false;
        }


        /// <summary>
        /// Element클릭시 발생이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accordionControl1_ElementClick(object sender, ElementClickEventArgs e)
        {
            if(e.Element.Name == "Calender")
            {
                User_Calendar user_Calendar = new User_Calendar();
                DevExpress.XtraTab.XtraTabPage page = new DevExpress.XtraTab.XtraTabPage();
                page.Text = e.Element.Name;
                page.Controls.Add(user_Calendar);
                xtraTabControl1.TabPages.Add(page);
                user_Calendar.Dock = DockStyle.Fill;
                user_Calendar.BringToFront();
                xtraTabControl1.SelectedTabPage = page;

            }
            else if(e.Element.Name != "")
            {
                NotePad notePad = new NotePad(e.Element.Name); //Category의 이름을 매개변수로 넘긴다.
                DevExpress.XtraTab.XtraTabPage page = new DevExpress.XtraTab.XtraTabPage();
                page.Text = e.Element.Name;
                page.Controls.Add(notePad);
                xtraTabControl1.TabPages.Add(page);
                notePad.Dock = DockStyle.Fill;
                notePad.BringToFront();
                xtraTabControl1.SelectedTabPage = page;
            }
            else
            {
                return;
            }
                
           
        }


    }
}
