using ProductManage.DAL;
using ProductManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductManage.Report
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                List<Product> fdata= new List<Product>();
                Product_DAL pro = new Product_DAL();
                fdata= pro.GetAllProducts();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("ListOfProducts.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add
                    (new Microsoft.Reporting.WebForms.ReportDataSource("DataProduct", fdata));
            
            }

        }
    }
}