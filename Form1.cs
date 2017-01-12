using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.IO;
using NuGet;
using System.Xml;
using System.Net;


namespace CallMeCS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = ReleaseToNuget();

            PowerShellExecutor ps = new PowerShellExecutor();

            ps.DeployToMaven();
        }



        private string ReleaseToNuget()
        {

            var localRepo =
                PackageRepositoryFactory.Default.CreateRepository(
                    @"C:\Program Files (x86)\Jenkins\workspace\GitReleaseTest");
            var package = localRepo.FindPackagesById("Package").First();

            var packageFile =
                new FileInfo(
                    @"C:\Program Files (x86)\Jenkins\workspace\GitReleaseTest\Package.1.0.0.nupkg");
            var size = packageFile.Length;
            try
            {
                var ps = new PackageServer("http://localhost:15202", "catnado");
            
                ps.PushPackage("", package, size, 18000, false);

                return package.ReleaseNotes;
            }
            catch (WebException a)
            {
                throw new WebException("Server not found", a);
            }

        }
    }
}

