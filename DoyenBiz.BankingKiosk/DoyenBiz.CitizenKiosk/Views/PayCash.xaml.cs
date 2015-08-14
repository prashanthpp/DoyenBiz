
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace DoyenBiz.CitizenKiosk.Views
{
    /// <summary>
    /// Interaction logic for PayCash.xaml
    /// </summary>
    public partial class PayCash : Flyout
    {
        public PayCash()
        {
            InitializeComponent();

            BindDataTable();
        }

        private void BindDataTable()
        {
            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("Denomination", typeof(string));
            dt.Columns.Add("Notes", typeof(string));

            var dr = dt.NewRow();
            dr["Denomination"] = "100 INR";
            dr["Notes"] = "1 Note";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Denomination"] = "50 INR";
            dr["Notes"] = "1 Note";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Denomination"] = "10 INR";
            dr["Notes"] = "0 Note";
            dt.Rows.Add(dr);
            dt.AcceptChanges();

            dgDetails.ItemsSource = dt.DefaultView;
        }
    }
}