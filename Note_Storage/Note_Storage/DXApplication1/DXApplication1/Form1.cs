using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DXApplication1
{
    public partial class Form1 : DevExpress.XtraBars.TabForm
    {
        public Form1()
        {
            InitializeComponent();
            accordionControl1.Elements.Clear();
            //Root level elements 
            AccordionControlElement gr1 = new AccordionControlElement(ElementStyle.Group) { Text = "Root Group 1" };
            AccordionControlElement i1 = new AccordionControlElement(ElementStyle.Item) { Text = "Item 1" };
            AccordionControlElement i2 = new AccordionControlElement(ElementStyle.Item) { Text = "Item 2" };
            AccordionControlElement gr2 = new AccordionControlElement(ElementStyle.Group) { Text = "Root Group 2" };
            AccordionControlElement i3 = new AccordionControlElement(ElementStyle.Item) { Text = "Item 3" };
            AccordionControlElement i4 = new AccordionControlElement(ElementStyle.Item) { Text = "Item 4" };
            AccordionControlElement gr3 = new AccordionControlElement(ElementStyle.Group) { Text = "Root Group 1" };
            AccordionControlElement i5 = new AccordionControlElement(ElementStyle.Item) { Text = "Item 5" };
            AccordionControlElement i6 = new AccordionControlElement(ElementStyle.Item) { Text = "Item 6" };

            //Level 2 elements 
            AccordionControlElement gr4 = new AccordionControlElement(ElementStyle.Group) { Text = "Level 2 Group 1" };
            AccordionControlElement i7 = new AccordionControlElement(ElementStyle.Item) { Text = "Level 2 Item 1" };
            AccordionControlElement i8 = new AccordionControlElement(ElementStyle.Item) { Text = "Level 2 Item 2" };
            AccordionControlElement gr5 = new AccordionControlElement(ElementStyle.Group) { Text = "Level 2 Group 2" };
            AccordionControlElement i9 = new AccordionControlElement(ElementStyle.Item) { Text = "Level 2 Item 3" };
            AccordionControlElement i10 = new AccordionControlElement(ElementStyle.Item) { Text = "Level 2 Item 4" };
            AccordionControlElement gr6 = new AccordionControlElement(ElementStyle.Group) { Text = "Level 2 Group 1" };
            AccordionControlElement i11 = new AccordionControlElement(ElementStyle.Item) { Text = "Level 2 Item 5" };
            AccordionControlElement i12 = new AccordionControlElement(ElementStyle.Item) { Text = "Level 2 Item 6" };

            //Level 3 elements 
            AccordionControlElement gr7 = new AccordionControlElement(ElementStyle.Group) { Text = "Level 3 Group 1" };
            AccordionControlElement i13 = new AccordionControlElement(ElementStyle.Item) { Text = "Level 3 Item 1" };
            AccordionControlElement i14 = new AccordionControlElement(ElementStyle.Item) { Text = "Level 3 Item 2" };
            AccordionControlElement gr8 = new AccordionControlElement(ElementStyle.Group) { Text = "Level 3 Group 2" };
            AccordionControlElement i15 = new AccordionControlElement(ElementStyle.Item) { Text = "Level 3 Item 3" };
            AccordionControlElement i16 = new AccordionControlElement(ElementStyle.Item) { Text = "Level 3 Item 4" };
            AccordionControlElement gr9 = new AccordionControlElement(ElementStyle.Group) { Text = "Level 3 Group 1" };
            AccordionControlElement i17 = new AccordionControlElement(ElementStyle.Item) { Text = "Level 3 Item 5" };
            AccordionControlElement i18 = new AccordionControlElement(ElementStyle.Item) { Text = "Level 3 Item 6" };

            gr7.Elements.AddRange(new AccordionControlElement[] { i13, i14 });
            gr8.Elements.AddRange(new AccordionControlElement[] { i15, i16 });
            gr9.Elements.AddRange(new AccordionControlElement[] { i17, i18 });

            gr4.Elements.AddRange(new AccordionControlElement[] { i7, i8, gr7 });
            gr5.Elements.AddRange(new AccordionControlElement[] { i9, i10, gr8 });
            gr6.Elements.AddRange(new AccordionControlElement[] { i11, i12, gr9 });

            gr1.Elements.AddRange(new AccordionControlElement[] { i1, i2, gr4 });
            gr2.Elements.AddRange(new AccordionControlElement[] { i3, i4, gr5 });
            gr3.Elements.AddRange(new AccordionControlElement[] { i5, i6, gr6 });

            accordionControl1.Elements.AddRange(new AccordionControlElement[] { gr1, gr2, gr3 }); //대그룹 Elements 아코디언 

            //대 중 소 그룹 세분화후 accordionControl 에 넣고 중 소 넣자
            accordionControl1.AllowItemSelection = true;
            accordionControl1.ExpandAll();

            Bar_Manager();
        }
        void OnOuterFormCreating(object sender, OuterFormCreatingEventArgs e)
        {
            Form1 form = new Form1();
            form.TabFormControl.Pages.Clear();
            e.Form = form;
            OpenFormCount++;
        }
        static int OpenFormCount = 1;
    }
}
