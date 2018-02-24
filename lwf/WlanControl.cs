using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lwf
{
    class WlanControl
    {
        public void on(String ssid, String key)
        {
            off();
            netsh("wlan set hostednetwork mode=allow ssid=" + ssid + " key=" + key);
            on();
        }
        public void off()
        {
            netsh("wlan stop hostednetwork");
        }
        private void on()
        {
            netsh("wlan start hostednetwork");
        }
        private void netsh(String arg)
        {
            int ret = runCmd("netsh " + arg);
            if (ret != 0)
                throw new System.ArgumentException("netsh returned " + ret.ToString());
        }
        private int runCmd(String cmd)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + cmd;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }
    }
}
