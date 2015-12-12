//vCenter Connect 1.0
// 12/11/2015 Matt Bradford
//www.VMSpot.com
//Twitter @VMSpot

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VMware.Vim;
using System.Collections.Specialized;



namespace vCenter_Inventory
{
    public partial class Inventory : Form
    {
        List<EntityViewBase> vmlist = new List<EntityViewBase>();
        List<EntityViewBase> hostlist = new List<EntityViewBase>();
        List<EntityViewBase> clusterlist = new List<EntityViewBase>();

        //Create a new VIM client object that will be used to connect to vCenter's SDK service
        VimClient Client = new VimClientImpl();


        public Inventory()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            lbVM.Items.Clear();
            lbHost.Items.Clear();
            lbCluster.Items.Clear();

            // Connect to the VMware vCenter SDK service  
            Client.Connect("https://" + tbVCS.Text + "/sdk");
            Client.Login(tbUN.Text, tbPW.Text);

            // Get a list of Windows VM's
            NameValueCollection filter = new NameValueCollection();
            filter.Add("Config.GuestFullName", "Windows");
            vmlist = Client.FindEntityViews(typeof(VirtualMachine), null, filter, null);

            //Populate the VM names into the VM ListBox
            foreach (VirtualMachine vm in vmlist)
            {

                lbVM.Items.Add(vm.Name);
            }

            //Get a list of ESXi hosts
            hostlist = Client.FindEntityViews(typeof(HostSystem), null, null, null);

            //Populate the Host names into the Host ListBox
            foreach (HostSystem vmhost in hostlist)
            {
                lbHost.Items.Add(vmhost.Name);
            }

            //Get a list of Clusters
            clusterlist = Client.FindEntityViews(typeof(ClusterComputeResource), null, null, null);

            //Populate the Cluster names into the Cluster ListBox
            foreach (ClusterComputeResource cluster in clusterlist)
            {

                lbCluster.Items.Add(cluster.Name);
            }

        }
    }
}
