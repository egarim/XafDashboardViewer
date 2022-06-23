using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafDashboardViewer.Module.Web.Controllers
{
    using System;
    using System.IO;
    using DevExpress.DashboardCommon;
    using DevExpress.DashboardWeb;
    using DevExpress.ExpressApp;
    using DevExpress.ExpressApp.Dashboards.Web;
    using DevExpress.Persistent.Base;
    using DevExpress.Persistent.BaseImpl;

    // ...
    public class WebDashboardController : ObjectViewController<DetailView, IDashboardData>
    {
        private WebDashboardViewerViewItem dashboardViewerViewItem;
        protected override void OnActivated()
        {
            base.OnActivated();
            dashboardViewerViewItem = View.FindItem("DashboardViewer") as WebDashboardViewerViewItem;
            if (dashboardViewerViewItem != null)
            {
                if (dashboardViewerViewItem.DashboardControl != null)
                {
                    SetHeight(dashboardViewerViewItem.DashboardControl);
                }
                else
                {
                    dashboardViewerViewItem.ControlCreated += DashboardViewerViewItem_ControlCreated;
                }
            }
        }
        private void DashboardViewerViewItem_ControlCreated(object sender, EventArgs e)
        {
            ASPxDashboard dashboardControl = ((WebDashboardViewerViewItem)sender).DashboardControl;

            dashboardControl.DashboardLoading += DashboardControl_DashboardLoading;


            SetHeight(dashboardControl);
        }

        private void DashboardControl_DashboardLoading(object sender, DashboardLoadingWebEventArgs e)
        {
            var test=e.DashboardXml;
            var D2= this.View.ObjectSpace.GetObjectsQuery<DashboardData>().FirstOrDefault(d => d.Title == "D2");

            var Content=D2.Content;

            
            //MemoryStream stream = new MemoryStream();
            //dashboard.SaveToXml(stream);

            System.Xml.Linq.XDocument xDocument = System.Xml.Linq.XDocument.Parse(Content);

            Dashboard dashboard = new Dashboard();
            dashboard.LoadFromXDocument(xDocument);


           

            //TODO douglas pasar los nuevo connection parameters
            //var SqlDataSource = dashboard.DataSources[0] as DashboardSqlDataSource;
            //SqlDataSource.ConnectionParameters=

            var UpdatedDashboard= dashboard.SaveToXDocument();

            e.DashboardXml = UpdatedDashboard;
        }

        private void SetHeight(ASPxDashboard dashboardControl)
        {
            dashboardControl.Height = 760;
        }
        protected override void OnDeactivated()
        {
            if (dashboardViewerViewItem != null)
            {
                dashboardViewerViewItem.ControlCreated -= DashboardViewerViewItem_ControlCreated;
                dashboardViewerViewItem = null;
            }
            base.OnDeactivated();
        }
    }
}
