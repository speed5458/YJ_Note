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
using DevExpress.XtraScheduler;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using DevExpress.XtraScheduler.Services;
using DevExpress.XtraScheduler.Commands;
using DevExpress.Utils.Menu;

namespace Note_Storage
{
    public partial class User_Calendar : DevExpress.XtraEditors.XtraUserControl
    {
        private String Constr = "server=127.0.0.1;uid=cyj;pwd=admin1234;database=My_Toy";
        DataSet _ds;

        public User_Calendar()
        {

            InitializeComponent();
            schedulerControl1.WorkDays.BeginUpdate();
            schedulerControl1.WorkDays.Clear();
            schedulerControl1.WorkDays.Add(WeekDays.Monday | WeekDays.Tuesday | WeekDays.Wednesday);
            schedulerControl1.WorkDays.EndUpdate();
            this.schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Month;
            Data_Load();
            MapAppointmentData();

        }


        private void Data_Load()
        {
            var Conn = new SqlConnection(Constr);
            Conn.Open();
            SqlCommand sc = new SqlCommand();
            sc.Connection = Conn;
            sc.CommandText = "SELECT * FROM Appointments";
            sc.CommandType = CommandType.Text;

            SqlDataAdapter sda = new SqlDataAdapter();
            _ds = new DataSet();
            sda.SelectCommand = sc;
            sda.Fill(_ds);
        }

        private void MapAppointmentData()
        {
            //required mappings
            schedulerDataStorage1.Appointments.Mappings.Start = "StartDate";
            schedulerDataStorage1.Appointments.Mappings.End = "EndDate";
            //optional mappings 
            schedulerControl1.DataStorage.Appointments.Mappings.Subject = "Subject";
            schedulerControl1.DataStorage.Appointments.Mappings.Location = "Location";
            schedulerControl1.DataStorage.Appointments.Mappings.Description = "Description";

            schedulerControl1.DataStorage.Appointments.Mappings.Label = "Label";
            schedulerControl1.DataStorage.Appointments.Mappings.Type = "Type";
            schedulerControl1.DataStorage.Appointments.Mappings.AllDay = "AllDay";
            schedulerControl1.DataStorage.Appointments.Mappings.Status = "Status";
            schedulerControl1.DataStorage.Appointments.Mappings.ResourceId = "ResourceId";
            schedulerControl1.DataStorage.Appointments.Mappings.ReminderInfo = "ReminderInfo";
            schedulerControl1.DataStorage.Appointments.Mappings.RecurrenceInfo = "RecurrenceInfo";
            schedulerControl1.DataStorage.Appointments.Mappings.TimeZoneId = "TimeZoneId";

            schedulerControl1.DataStorage.Appointments.DataSource  = _ds.Tables[0];
        }


        private void schedulerControl1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.Menu.Id == DevExpress.XtraScheduler.SchedulerMenuItemId.DefaultMenu)
            {
                // Hide the "Change View To" menu item. 
                SchedulerPopupMenu itemChangeViewTo = e.Menu.GetPopupMenuById(SchedulerMenuItemId.SwitchViewMenu);
                //itemChangeViewTo.Visible = false;
                itemChangeViewTo.Caption = "&보기방식 변경";

                // Remove unnecessary items. 
                e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewAllDayEvent);
                // Disable the "New Recurring Appointment" menu item. 
                e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringAppointment);
                e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringEvent);
                e.Menu.RemoveMenuItem(SchedulerMenuItemId.SwitchToGroupByResource);

                // Find the "New Appointment" menu item and rename it. 
                SchedulerMenuItem item = e.Menu.GetMenuItemById(SchedulerMenuItemId.NewAppointment);
                if (item != null)
                {
                    item.Caption = "&일정추가";
                   // item.ImageOptions.SvgImage = DevExpress.Utils.Svg.SvgImage.FromFile("NewItem.svg");
                }
                SchedulerMenuItem item2 = e.Menu.GetMenuItemById(SchedulerMenuItemId.GotoDate);
                if (item2 != null)
                {
                    item2.Caption = "&설정날짜이동";
                    // item.ImageOptions.SvgImage = DevExpress.Utils.Svg.SvgImage.FromFile("NewItem.svg");
                }
                SchedulerMenuItem item3 = e.Menu.GetMenuItemById(SchedulerMenuItemId.GotoThisDay);
                if (item3 != null)
                {
                    item3.Caption = "&선택된날짜이동";
                }
                SchedulerMenuItem item4 = e.Menu.GetMenuItemById(SchedulerMenuItemId.GotoToday);
                if (item4 != null)
                {
                    item4.Caption = "&오늘날짜이동";
                }

                // Create a menu item for the Scheduler command. 
                ISchedulerCommandFactoryService service = schedulerControl1.GetService<ISchedulerCommandFactoryService>();
                SchedulerCommand cmd = service.CreateCommand(SchedulerCommandId.SwitchToGroupByResource);
                SchedulerMenuItemCommandWinAdapter menuItemCommandAdapter = new SchedulerMenuItemCommandWinAdapter(cmd);
                DXMenuItem menuItem = (DXMenuItem)menuItemCommandAdapter.CreateMenuItem(DXMenuItemPriority.Normal);
                menuItem.BeginGroup = true;
                e.Menu.Items.Add(menuItem);

                // Insert a new item into the Scheduler popup menu and handle its click event. 
                e.Menu.Items.Add(new SchedulerMenuItem("저장", MyClickHandler));
            }
        }

        public void MyClickHandler(object sender, EventArgs e)
        {
            MessageBox.Show("My Menu Item Clicked!");
            IAppointmentStorage get_data = schedulerControl1.DataStorage.Appointments;
            DataTable dt = new DataTable();
            dt = (DataTable)get_data.DataSource;
            int i = 0;

        }


    }
}
