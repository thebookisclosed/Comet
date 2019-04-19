using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Comet.UI
{
    public partial class Cleaner : Form
    {
        private Thread HandlerThread;
        private long TotalBytesDeleted;
        private int LastPercent;
        private EmptyVolumeCacheCallBacks CallBacks;
        private bool ProcessingHandlers;

        public Cleaner()
        {
            InitializeComponent();
            Icon = Properties.Resources.full;
            PtbLogo.Image = Icon.ToBitmap();
            LblClnUp.Text += Preferences.SelectedDrive.Name;
            LblHandler.Text = Preferences.CleanupHandlers[0].DisplayName;
            HandlerThread = new Thread(new ThreadStart(() => {
                Api.ReinstateHandlers(Preferences.CleanupHandlers, Preferences.SelectedDrive.Letter);
                // Set up a callback for progress reporting
                CallBacks = new EmptyVolumeCacheCallBacks();
                CallBacks.PurgeProgressChanged += CallBacks_PurgeProgressChanged;
                TotalBytesDeleted = 0;
                ProcessingHandlers = true;
                for (int i = 0; i < Preferences.CleanupHandlers.Count; i++)
                {
                    CleanupHandler oHandler = Preferences.CleanupHandlers[i];
                    if (oHandler.PreProcHint != null)
                        RunProcHint(oHandler.PreProcHint);
                    if (LblHandler.IsHandleCreated)
                        Invoke((MethodInvoker)delegate {
                            LblHandler.Text = oHandler.DisplayName;
                        });
                    int spaceResult = oHandler.Instance.GetSpaceUsed(out long newSpaceUsed, CallBacks);
                    if (spaceResult == -2147467260) // -2147467260 = 0x80004004 = E_ABORT
                        break;
                    else if (spaceResult == 0)
                    {
                        Preferences.CurrentSelectionSavings = Preferences.CurrentSelectionSavings - oHandler.BytesUsed + newSpaceUsed;
                        int purgeResult = oHandler.Instance.Purge(newSpaceUsed, CallBacks);
                        if (purgeResult == -2147467260) // -2147467260 = 0x80004004 = E_ABORT
                            break;
                        if (purgeResult != 0 && purgeResult != -2147287022) // -2147287022 == 0x80030012 == STG_E_NOMOREFILES
                            MessageBox.Show("Purging " + oHandler.DisplayName + " failed with error 0x" + purgeResult.ToString("x8"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (oHandler.PostProcHint != null)
                            RunProcHint(oHandler.PostProcHint);
                    }
                    try { oHandler.Instance.Deactivate(out uint dummy); } catch { }
                    Marshal.FinalReleaseComObject(oHandler.Instance);
                }
                ProcessingHandlers = false;
                Api.DeactivateHandlers(Preferences.CleanupHandlers);
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
                LastPercent = (int)((double)TotalBytesDeleted / Preferences.CurrentSelectionSavings * 100);
                Invoke((MethodInvoker)delegate {
                    PrgClean.Value = LastPercent;
                });
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
            try
            {
                p.Start();
                p.WaitForExit();
            }
            catch { MessageBox.Show("Couldn't start the following process: " + ProcHint, Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (HandlerThread.IsAlive && ProcessingHandlers)
                CallBacks.Abort = true;
            else
            {
                HandlerThread.Abort();
                Close();
            }
        }
    }
}
