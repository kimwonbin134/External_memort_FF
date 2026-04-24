using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Memory;
using System.Diagnostics;

namespace External
{
    public partial class Form1 : Form
    {
        Mem Memory = new Mem();
        private Dictionary<string, bool> featureStates = new Dictionary<string, bool>();

        public Form1()
        {
            InitializeComponent();
            InitializeFeatureStates();
        }

        private void InitializeFeatureStates()
        {
            featureStates["Speed"] = false;
            featureStates["CamLeft"] = false;
            featureStates["FastSwitch"] = false;
            featureStates["GuestReset"] = false;
            featureStates["Aimbot"] = false;
            featureStates["BlackSky"] = false;
        }

        private bool IsEmulatorOpen()
        {
            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                sts.Text = "Status : Open emu Kid";
                Console.Beep(250, 300);
                return false;
            }
            return true;
        }

        private async Task PerformMemoryOperation(string search, string replace, string featureName)
        {
            if (!IsEmulatorOpen())
                return;

            sts.Text = "Status : Processing...";

            try
            {
                Memory.OpenProcess("HD-Player");

                bool success = false;
                IEnumerable<long> wl = await Memory.AoBScan(search, writable: true);

                if (wl != null && wl.Count() != 0)
                {
                    foreach (var address in wl)
                    {
                        Memory.WriteMemory(address.ToString("X"), "bytes", replace);
                    }
                    success = true;
                }

                if (success)
                {
                    sts.Text = "Status : " + featureName + " Activated";
                    Console.Beep(250, 300);
                }
                else
                {
                    sts.Text = "Status : " + featureName + " Failed";
                    Console.Beep(230, 280);
                }
            }
            catch (Exception ex)
            {
                sts.Text = "Status : Error - " + ex.Message;
                Console.Beep(230, 280);
            }
        }

        private async void Speed_Click(object sender, EventArgs e)
        {
            if (!featureStates["Speed"])
            {
                // Speed ON
                string search = "40 02 2B 07 3D 02 2B 07 3D 02 2B 07 3D 00 00 00 00 9B 6C F2";
                string replace = "40 02 2B 9B 3C 02 2B 9B 3C 02 2B 07 3D 00 00 00";
                await PerformMemoryOperation(search, replace, "Speed");
                featureStates["Speed"] = true;
            }
            else
            {
                // Speed OFF
                string search = "40 02 2B 9B 3C 02 2B 9B 3C 02 2B 07 3D 00 00 00";
                string replace = "40 02 2B 07 3D 02 2B 07 3D 02 2B 07 3D 00 00 00 00 9B 6C F2";
                await PerformMemoryOperation(search, replace, "Speed (OFF)");
                featureStates["Speed"] = false;
            }
        }

        private async void CamLeft_Click(object sender, EventArgs e)
        {
            if (!featureStates["CamLeft"])
            {
                // Camera Lift ON
                string search = "00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00";
                string replace = "00 00 00 00 00 00 80 40 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 80 BF";
                await PerformMemoryOperation(search, replace, "Camera Lift");
                featureStates["CamLeft"] = true;
            }
            else
            {
                // Camera Lift OFF
                string search = "00 00 00 00 00 00 80 40 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 80 BF";
                string replace = "00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00";
                await PerformMemoryOperation(search, replace, "Camera Lift (OFF)");
                featureStates["CamLeft"] = false;
            }
        }

        private async void Sts_Click(object sender, EventArgs e)
        {
            if (!IsEmulatorOpen())
                return;

            sts.Text = "Status : Checking Emulator...";
            await Task.Delay(500);
            sts.Text = "Status : Emulator Connected";
            Console.Beep(250, 150);
        }

        private async void FastSwitch_Click(object sender, EventArgs e)
        {
            if (!featureStates["FastSwitch"])
            {
                // Fast Switch ON - Add your AOB values here
                string search = ""; // Add your search AOB for Fast Switch
                string replace = ""; // Add your replace AOB for Fast Switch
                await PerformMemoryOperation(search, replace, "Fast Switch");
                featureStates["FastSwitch"] = true;
            }
            else
            {
                // Fast Switch OFF - Add your AOB values here
                string search = ""; // Add your search AOB for turning OFF
                string replace = ""; // Add your replace AOB for turning OFF
                await PerformMemoryOperation(search, replace, "Fast Switch (OFF)");
                featureStates["FastSwitch"] = false;
            }
        }

        private async void GuestReset_Click(object sender, EventArgs e)
        {
            if (!featureStates["GuestReset"])
            {
                // Guest Reset ON - Add your AOB values here
                string search = ""; // Add your search AOB for Guest Reset
                string replace = ""; // Add your replace AOB for Guest Reset
                await PerformMemoryOperation(search, replace, "Guest Reset");
                featureStates["GuestReset"] = true;
            }
            else
            {
                // Guest Reset OFF - Add your AOB values here
                string search = ""; // Add your search AOB for turning OFF
                string replace = ""; // Add your replace AOB for turning OFF
                await PerformMemoryOperation(search, replace, "Guest Reset (OFF)");
                featureStates["GuestReset"] = false;
            }
        }

        private async void Aimbot_Click(object sender, EventArgs e)
        {
            if (!featureStates["Aimbot"])
            {
                // Aimbot ON - Add your AOB values here
                string search = ""; // Add your search AOB for Aimbot
                string replace = ""; // Add your replace AOB for Aimbot
                await PerformMemoryOperation(search, replace, "Aimbot");
                featureStates["Aimbot"] = true;
            }
            else
            {
                // Aimbot OFF - Add your AOB values here
                string search = ""; // Add your search AOB for turning OFF
                string replace = ""; // Add your replace AOB for turning OFF
                await PerformMemoryOperation(search, replace, "Aimbot (OFF)");
                featureStates["Aimbot"] = false;
            }
        }

        private async void BlackSky_Click(object sender, EventArgs e)
        {
            if (!featureStates["BlackSky"])
            {
                // Black Sky ON - Add your AOB values here
                string search = ""; // Add your search AOB for Black Sky
                string replace = ""; // Add your replace AOB for Black Sky
                await PerformMemoryOperation(search, replace, "Black Sky");
                featureStates["BlackSky"] = true;
            }
            else
            {
                // Black Sky OFF - Add your AOB values here
                string search = ""; // Add your search AOB for turning OFF
                string replace = ""; // Add your replace AOB for turning OFF
                await PerformMemoryOperation(search, replace, "Black Sky (OFF)");
                featureStates["BlackSky"] = false;
            }
        }
    }
}