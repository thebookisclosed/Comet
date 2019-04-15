using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Comet.UI
{
    public partial class Scanner : Form
    {
        private Thread HandlerThread;

        public Scanner()
        {
            InitializeComponent();
            Icon = Properties.Resources._1;
            PtbLogo.Image = Icon.ToBitmap();
            LblIntro.Text = string.Format(LblIntro.Text, Preferences.SelectedDrive.Name);
            HandlerThread = new Thread(new ThreadStart(() => {
                Preferences.CleanupHandlers = new List<CleanupHandler>();
                using (RegistryKey rKey = Registry.LocalMachine.OpenSubKey(Api.VolumeCacheStoreKey, false))
                {
                    string[] subKeyNames = rKey.GetSubKeyNames();
                    // Adjust progress bar maximum to discovered handler count
                    Invoke((MethodInvoker)delegate
                    {
                        PrgScan.Maximum = subKeyNames.Length;
                    });
                    // Set up a dummy callback because COM stuff goes haywire for particular handlers if we supply none
                    EmptyVolumeCacheCallBacks callBacks = new EmptyVolumeCacheCallBacks();
                    for (int i = 0; i < subKeyNames.Length; i++)
                    {
                        CleanupHandler evp = Api.InitializeHandler(subKeyNames[i], Preferences.SelectedDrive.Letter);
                        if (evp != null)
                        {
                            if (LblHandler.IsHandleCreated)
                                Invoke((MethodInvoker)delegate {
                                    LblHandler.Text = evp.DisplayName;
                                    PrgScan.Value = i + 1;
                                });
                            long spaceUsed = 0;
                            try { evp.Instance.GetSpaceUsed(out spaceUsed, callBacks); }
                            catch
                            {
                                //MessageBox.Show(string.Format("Can't get used space for {0}, Flags {1}", evp.DisplayName, evp.Flags));
                            }
                            if (spaceUsed == 0 &&
                                ((evp.Flags & HandlerFlags.DontShowIfZero) == HandlerFlags.DontShowIfZero ||
                                (evp.DataDrivenFlags & DDCFlags.DontShowIfZero) == DDCFlags.DontShowIfZero))
                            {
                                //MessageBox.Show(string.Format("Discarding {0} {1}", evp.DisplayName, evp.BytesUsed));
                                try { evp.Instance.Deactivate(out uint dummy); } catch { }
                                Marshal.FinalReleaseComObject(evp.Instance);
                            }
                            else
                            {
                                evp.BytesUsed = spaceUsed;
                                Preferences.CleanupHandlers.Add(evp);
                                //MessageBox.Show(string.Format("Adding {0} {1}", evp.DisplayName, evp.BytesUsed));
                            }
                        }
                        //else
                        //{
                        //    MessageBox.Show(string.Format("{0} init ended with NULL", subKeyNames[i]));
                        //}
                    }
                }
                Api.DeactivateHandlers(Preferences.CleanupHandlers);
                // Sort handlers by priority, making sure they'll run in correct order
                Preferences.CleanupHandlers.Sort((x, y) => y.Priority.CompareTo(x.Priority));
                // A (stupid?) way to close the form once we are done cleaning
                Invoke((MethodInvoker)delegate {
                    Close();
                });
            }));
            HandlerThread.SetApartmentState(ApartmentState.STA);
            HandlerThread.Start();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            HandlerThread.Abort();
            Close();
        }
    }
}
