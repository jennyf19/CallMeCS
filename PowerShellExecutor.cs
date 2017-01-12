using System.Management.Automation;

namespace CallMeCS
{
    class PowerShellExecutor
    {
        public void DeployToMaven()
        {
            using (PowerShell powerShellInstance = PowerShell.Create())
            {
                string filePath = @"c:\users\jeferrie\sample\mavenDeploy.ps1";

                powerShellInstance.Commands.AddScript(filePath);
                powerShellInstance.Invoke(filePath);
            }
        }
    }
}
