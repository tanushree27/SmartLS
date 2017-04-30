using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    class Results
    {
        public StackPanel st = new StackPanel();

        public TextBlock name = new TextBlock();
        public TextBlock fileaddress = new TextBlock();
        public TextBlock filetype = new TextBlock();
        public TextBlock size = new TextBlock();
       
        private string address;
        public string Address { get => address; set => address = value; }

        public Results ()
        {

        }
    
        public void Fill(string nam, string link, string type, double sz)
        {
            
            address = link;
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#eeeeee");
            brush.Freeze();

            name.Text = "File Name: " + nam;
            name.FontSize = 20;
            name.Margin = new Thickness(20, 10, 0, 0);

            fileaddress.Text = "Address: " + link;
            fileaddress.FontSize = 10;
            fileaddress.Foreground = Brushes.Blue;
            fileaddress.Margin = new Thickness(20, 5, 0, 0);

            filetype.Text = "Type: " + type;
            filetype.FontSize = 15;
            filetype.Margin = new Thickness(20, 10, 0, 0);

            size.Text = "Size: " + sz.ToString() + "kB";
            size.FontSize = 15;
            size.Margin = new Thickness(20, 5, 0, 10);

            st.MouseEnter += new MouseEventHandler(stackpanel_MouseEnter);
            st.MouseLeave += new MouseEventHandler(stackpanel_MouseLeave);
            st.MouseLeftButtonDown += new MouseButtonEventHandler(stackpanel_MouseClick);
            st.Margin = new Thickness(10, 10, 10, 10);
            st.Background = brush;
            st.Opacity = 0.5 ;


            st.Children.Add(name);
            if (!nam.Equals("NO RESULTS FOUND"))
            {
                st.Children.Add(fileaddress);
                st.Children.Add(filetype);
                st.Children.Add(size);
            }
            else
            {
                name.Text = "NO RESULTS FOUND";
                name.Margin = new Thickness(20, 10, 0, 10);
            }
        }

        void stackpanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (!address.Equals (""))
                System.Diagnostics.Process.Start(@address);
        }

        void stackpanel_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel stackpanel = (StackPanel)sender;
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#eeeeee");
            brush.Freeze();
            stackpanel.Background = brush;
        }

        void stackpanel_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel stackpanel = (StackPanel)sender;
            stackpanel.Background = Brushes.LightGray;
        }
    }
}
