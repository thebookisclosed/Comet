using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Comet.UI
{
    public partial class Cleaner : Form
    {
        private Thread HandlerThread;
        private long TotalBytesDeleted;
        private int LastPercent;

        public Cleaner()
        {
            InitializeComponent();
            Icon = Properties.Resources._1;
            PtbLogo.Image = Icon.ToBitmap();
            LblIntro.Text = string.Format(LblIntro.Text, Preferences.SelectedDrive.Name);
            LblHandler.Text = Preferences.CleanupHandlers[0].DisplayName;
            HandlerThread = new Thread(new ThreadStart(() => {
                Api.ReinstateHandlers(Preferences.CleanupHandlers, Preferences.SelectedDrive.Letter);
                // Set up a callbacks for progress reporting
                EmptyVolumeCacheCallBacks callBacks = new EmptyVolumeCacheCallBacks();
                callBacks.PurgeProgressChanged += CallBacks_PurgeProgressChanged;
                TotalBytesDeleted = 0;
                for (int i = 0; i < Preferences.CleanupHandlers.Count; i++)
                {
                    CleanupHandler oHandler = Preferences.CleanupHandlers[i];
                    if (oHandler.PreProcHint != null)
                        RunProcHint(oHandler.PreProcHint);
                    if (LblHandler.IsHandleCreated)
                        Invoke((MethodInvoker)delegate {
                            LblHandler.Text = oHandler.DisplayName;
                        });
                    try
                    {
                        int purgeResult = oHandler.Instance.Purge(oHandler.BytesUsed, callBacks);
                        if (purgeResult != 0 && purgeResult != -2147287022) // -2147287022 == 0x80030012 == STG_E_NOMOREFILES
                        {
                            MessageBox.Show("Purging " + oHandler.DisplayName + " failed with error 0x" + purgeResult.ToString("x8"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (oHandler.PostProcHint != null)
                            RunProcHint(oHandler.PostProcHint);
                        try { oHandler.Instance.Deactivate(out uint dummy); } catch { }
                        Marshal.FinalReleaseComObject(oHandler.Instance);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), oHandler.DisplayName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // A (stupid?) way to close the form once we are done cleaning
                Invoke((MethodInvoker)delegate {
                    Close();
                });
            }));
            HandlerThread.SetApartmentState(ApartmentState.STA);
            HandlerThread.Start();
        }

        private void CallBacks_PurgeProgressChanged(object sender, PurgeProgressChangedEventArgs e)
        {
            int cPerc = (int)((double)(TotalBytesDeleted + e.SpaceFreed) / Preferences.CurrentSelectionSavings * 100);
            if (cPerc != LastPercent)
            {
                LastPercent = cPerc;
                Invoke((MethodInvoker)delegate {
                    PrgClean.Value = cPerc;
                });
            }
            if (e.Flags == CallbackFlags.LastNotification)
            {
                TotalBytesDeleted += e.SpaceToFree;
            }
        }

        // Get effective filename of process to run for pre/post cleanup actions
        private static string GetFirstParam(string line)
        {
            char prevChar = '\0';
            char nextChar = '\0';
            char currentChar = '\0';
            bool inString = false;
            for (int i = 0; i < line.Length; i++)
            {
                currentChar = line[i];
                if (i > 0)
                    prevChar = line[i - 1];
                if (i + 1 < line.Length)
                    nextChar = line[i + 1];
                else
                    nextChar = '\0';
                if (currentChar == '"' && (prevChar == '\0' || prevChar == ' ') && !inString)
                    inString = true;
                if (currentChar == '"' && (nextChar == '\0' || nextChar == ' ') && inString)
                    inString = false;
                if (currentChar == ' ' && !inString)
                    return line.Substring(0, i);
            }
            return line;
        }

        private void RunProcHint(string ProcHint)
        {
            string fileName = GetFirstParam(ProcHint);
            Process p = new Process();
            p.StartInfo.FileName = fileName.Trim('"');
            if (ProcHint.Length > fileName.Length + 1)
                p.StartInfo.Arguments = ProcHint.Substring(fileName.Length + 1);
            p.Start();
            p.WaitForExit();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            HandlerThread.Abort();
            Api.DeactivateHandlers(Preferences.CleanupHandlers);
            Close();
        }
    }
}
