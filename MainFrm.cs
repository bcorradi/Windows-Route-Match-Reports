using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.Export;


namespace Windows_UltraReport
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {

            
        }

        private void btnRunReport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection("Data Source=hermes\\Routematch60;Initial Catalog=RM_MN_AEOA;Integrated Security=True");
            con.Open();
            SqlCommand myCmd = new SqlCommand("AEOA_UltraReport2", con);
            //SqlCommand myCmd = new SqlCommand("AEOA_HourlyNSE2", con);
            myCmd.CommandTimeout = 45;
            myCmd.CommandType = CommandType.StoredProcedure;
            myCmd.Parameters.AddWithValue("@ReportStartDateTime", dtpStartDate.Value);
            myCmd.Parameters.AddWithValue("@ReportEndDateTime", dtpEndDate.Value);
            myCmd.Parameters.AddWithValue("@areaname", cbArea.Text);
            myCmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(myCmd);
            da.Fill(dt);
            gridMain.DataSource = dt;

            gridMain.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;

            GridViewSummaryItem summaryPointsItem = new GridViewSummaryItem();
            summaryPointsItem.Name = "Points";
            summaryPointsItem.Aggregate = GridAggregateFunction.Sum;

            GridViewSummaryItem summaryTotalMilesItem = new GridViewSummaryItem();
            summaryTotalMilesItem.Name = "Total Miles";
            summaryTotalMilesItem.Aggregate = GridAggregateFunction.Sum;

            GridViewSummaryItem summaryAdultItem = new GridViewSummaryItem();
            summaryAdultItem.Name = "Adult";
            summaryAdultItem.Aggregate = GridAggregateFunction.Sum;

            GridViewSummaryItem summaryElderlyItem = new GridViewSummaryItem();
            summaryElderlyItem.Name = "Elderly";
            summaryElderlyItem.Aggregate = GridAggregateFunction.Sum;

            GridViewSummaryItem summaryChildItem = new GridViewSummaryItem();
            summaryChildItem.Name = "Child";
            summaryChildItem.Aggregate = GridAggregateFunction.Sum;

            GridViewSummaryItem summaryDisabledItem = new GridViewSummaryItem();
            summaryDisabledItem.Name = "Disabled";
            summaryDisabledItem.Aggregate = GridAggregateFunction.Sum;

            GridViewSummaryItem summaryNonAmbItem = new GridViewSummaryItem();
            summaryNonAmbItem.Name = "NonAmb";
            summaryNonAmbItem.Aggregate = GridAggregateFunction.Sum;

            GridViewSummaryItem summaryStudentItem = new GridViewSummaryItem();
            summaryStudentItem.Name = "Student";
            summaryStudentItem.Aggregate = GridAggregateFunction.Sum;

            GridViewSummaryItem summaryTotalPassengersItem = new GridViewSummaryItem();
            summaryTotalPassengersItem.Name = "Total Passengers";
            summaryTotalPassengersItem.Aggregate = GridAggregateFunction.Sum;

            GridViewSummaryItem summaryCashItem = new GridViewSummaryItem();
            summaryCashItem.Name = "People that paid cash";
            summaryCashItem.Aggregate = GridAggregateFunction.Sum;

            GridViewSummaryItem summaryPassItem = new GridViewSummaryItem();
            summaryPassItem.Name = "Pass";
            summaryPassItem.Aggregate = GridAggregateFunction.Sum;

            GridViewSummaryItem summaryTicketItem = new GridViewSummaryItem();
            summaryTicketItem.Name = "Ticket";
            summaryTicketItem.Aggregate = GridAggregateFunction.Sum;

            GridViewSummaryItem summaryFreeItem = new GridViewSummaryItem();
            summaryFreeItem.Name = "Free";
            summaryFreeItem.Aggregate = GridAggregateFunction.Sum;            

            GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
            summaryRowItem.Add(summaryPointsItem);
            summaryRowItem.Add(summaryTotalMilesItem);
            summaryRowItem.Add(summaryAdultItem);
            summaryRowItem.Add(summaryElderlyItem);
            summaryRowItem.Add(summaryChildItem);
            summaryRowItem.Add(summaryDisabledItem);
            summaryRowItem.Add(summaryNonAmbItem);
            summaryRowItem.Add(summaryStudentItem);
            summaryRowItem.Add(summaryTotalPassengersItem);
            summaryRowItem.Add(summaryCashItem);
            summaryRowItem.Add(summaryPassItem);
            summaryRowItem.Add(summaryTicketItem);
            summaryRowItem.Add(summaryFreeItem);
            gridMain.SummaryRowsTop.Clear();
            gridMain.SummaryRowsTop.Add(summaryRowItem);            
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now.AddMonths(-1);
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            dtpStartDate.Value = firstDayOfMonth;
            dtpEndDate.Value = lastDayOfMonth;

            cbArea.SelectedIndex = 0;
        }

        private void gridMain_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Telerik.WinControls.Export.GridViewPdfExport pdfExporter = new Telerik.WinControls.Export.GridViewPdfExport(gridMain);

           

            pdfExporter.FileExtension = "*.pdf";
            pdfExporter.ShowHeaderAndFooter = true;
            pdfExporter.SummariesExportOption = Telerik.WinControls.UI.Export.SummariesOption.ExportAll;
            pdfExporter.FitToPageWidth = true;

            IPdfExportRenderer a = null;
            
            pdfExporter.RunExportAsync("d:\\export.pdf");
        }
    }
}
