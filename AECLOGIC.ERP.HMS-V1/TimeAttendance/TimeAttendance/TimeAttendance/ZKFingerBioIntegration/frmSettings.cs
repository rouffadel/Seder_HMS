using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZKFingerBioIntegration
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }
        dsSettings ds;
        public zkemkeeper.CZKEM axCZKEM1 = new zkemkeeper.CZKEM();
        private void frmSettings_Load(object sender, EventArgs e)
        {
            try
            {
                tsStatusLable.Text = "Ready";
                ds = new dsSettings();
                ds.ReadXml(Application.StartupPath + @"\AECBioSet.xml");
                dsSettingsBindingSource.DataSource = ds;

                // ConnectBioMetric();
                DataSet dsAppConfig = new DataSet();
                dsAppConfig.ReadXml(Application.StartupPath + "\\AppConfig.xml");
                txtURL.Text = dsAppConfig.Tables[0].Rows[0]["url"].ToString();
                txtNameApp.Text = dsAppConfig.Tables[0].Rows[0]["Cname"].ToString();
                dsAppConfig.Dispose();
                // ConnectAECERPAPP();
            }
            catch (Exception ex) { wtirnotepadloagfile("004: " + ex.Message.ToString() + " " + DateTime.Today.ToLongTimeString()); }
        }

        private void ConnectBioMetric()
        {
            try
            {
                IsDiviceConnected = false;
                foreach (DataGridViewRow dgrow in dgvSettings.Rows)
                {
                    try
                    {
                        if (dgrow.Cells[2].Value.ToString().Trim() == "")
                            break;
                        Boolean bIsConnected = axCZKEM1.Connect_Net(dgrow.Cells[2].Value.ToString().Trim(),
                            Convert.ToInt32(dgrow.Cells[3].Value));   // 4370 is port no of attendance machine
                        if (bIsConnected == true)
                        {
                            dgrow.Cells[5].Value = "Connected";
                            dgrow.Cells[5].Style.ForeColor = Color.Green;
                            IsDiviceConnected = true;
                            axCZKEM1.Disconnect();
                            // MessageBox.Show("Device Connected Successfully");
                        }
                        else
                        {
                            dgrow.Cells[5].Value = "Not Connect";
                            dgrow.Cells[5].Style.ForeColor = Color.Red;
                            // MessageBox.Show("Device Not Connect");
                        }

                    }
                    catch (Exception ex) { wtirnotepadloagfile("005: " + ex.Message.ToString() + " " + DateTime.Today.ToLongTimeString()); }
                }
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds.WriteXml(Application.StartupPath + @"\AECBioSet.xml");
                DataSet dsAppConfig = new DataSet();
                dsAppConfig.ReadXml(Application.StartupPath + "\\AppConfig.xml");
                dsAppConfig.Tables[0].Rows[0]["url"] = txtURL.Text;
                dsAppConfig.Tables[0].Rows[0]["Cname"] = txtNameApp.Text;
                dsAppConfig.WriteXml(Application.StartupPath + "\\AppConfig.xml");
                dsAppConfig = null;
                MessageBox.Show("Saved Successfully");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        dsSettings dstem = new dsSettings();
        private void btnGetData_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            int getsetval = Convert.ToInt32(btn.Tag);
            CallBiTimeData(getsetval);

            //Cursor = Cursors.Default;


        }

        private void CallBiTimeData(int getsetval)
        {
            int CurMonth = 0, CurYear = 0, CurDay = 0;
            CurDay = DateTime.Today.Day;
            CurMonth = DateTime.Today.Month;
            CurYear = DateTime.Today.Year;
            string sdwEnrollNumber = "";
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;
            int idwErrorCode = 0;
            int iGLCount = 0;
            DataTable dt = new DataTable();
            dstem = new dsSettings();
            if (getsetval == 0)
            {
                dt.Columns.Add("Count", typeof(string));
                dt.Columns.Add("EnrollNumbe", typeof(string));
                dt.Columns.Add("idwVerifyMode", typeof(string));
                dt.Columns.Add("idwInOutMode", typeof(string));
                dt.Columns.Add("idwYear", typeof(string));
                dt.Columns.Add("idwWorkcode", typeof(string));
            }
            else
            {
                try { dstem.ReadXml(Application.StartupPath + @"\AECBioAttLog_" + CurMonth + "_" + CurYear + ".xml"); }
                catch (Exception ex) { wtirnotepadloagfile("003: " + ex.Message.ToString() + " " + DateTime.Today.ToLongTimeString()); }
            }
            foreach (DataGridViewRow dgrow in dgvSettings.Rows)
            {
                try
                {
                    int iMachineNumber = Convert.ToInt32(dgrow.Cells[4].Value);
                    if (dgrow.Cells[2].Value.ToString().Trim() == "")
                        break;
                    Boolean bIsConnected = axCZKEM1.Connect_Net(dgrow.Cells[2].Value.ToString().Trim(),
                        Convert.ToInt32(dgrow.Cells[3].Value));   // 4370 is port no of attendance machine
                    if (bIsConnected == true)
                    {
                        axCZKEM1.EnableDevice(iMachineNumber, true);//disable the device
                        axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
                        if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
                        {
                            while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
                                       out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                            {
                                try
                                {
                                    if (idwMonth == CurMonth && idwYear == CurYear)
                                    {
                                        iGLCount++;
                                        if (getsetval == 0)
                                        {
                                            DataRow drnew = dt.NewRow();
                                            drnew["Count"] = iGLCount.ToString();
                                            drnew["EnrollNumbe"] = sdwEnrollNumber.ToString();
                                            drnew["idwVerifyMode"] = idwVerifyMode.ToString();
                                            drnew["idwInOutMode"] = idwInOutMode.ToString();
                                            drnew["idwYear"] = idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString().ToString();
                                            drnew["idwWorkcode"] = idwWorkcode.ToString();
                                            dt.Rows.Add(drnew);
                                        }
                                        else
                                        {
                                            string Typevalue = "";
                                            dsSettings.dtAttLogRow dtattRow = null;
                                            dtattRow = dstem.dtAttLog.FindByEMPIDDayMonthYear(Convert.ToInt32(sdwEnrollNumber), idwDay, idwMonth, idwYear);
                                            //DataRow[] drs = dstem.dtAttLog.Select("Day=" + idwDay + "and Month" + idwMonth + " adn Year=" + idwYear);
                                            //dsSettings.dtAttLogRow dtattRow = null;
                                            string INTime = "", OutTime = "";
                                            string INOutTime = idwHour.ToString("00") + ":" + idwMinute.ToString("00"); ;
                                            if (idwInOutMode == 0)
                                            {
                                                Typevalue = "IN";
                                                INTime = INOutTime;//idwHour.ToString("00") + ":" + idwMinute.ToString("00");// +":" + idwSecond.ToString("00");
                                                if (dtattRow != null)
                                                {
                                                    if (dtattRow.INTime.Trim() == "")
                                                        dtattRow.INTime = INTime;
                                                    if (System.TimeSpan.Parse(dtattRow.INTime) > System.TimeSpan.Parse(INTime))
                                                    {
                                                        dtattRow.INTime = INTime;
                                                        dtattRow.SendToERP = 0;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Typevalue = "OUT";
                                                OutTime = INOutTime;// idwHour.ToString("00") + ":" + idwMinute.ToString("00");// +":" + idwSecond.ToString("00");
                                                if (dtattRow != null)
                                                {
                                                    if (dtattRow.OutTime.Trim() == "")
                                                        dtattRow.OutTime = dtattRow.INTime;
                                                    if (System.TimeSpan.Parse(dtattRow.OutTime) < System.TimeSpan.Parse(OutTime))
                                                    {
                                                        dtattRow.OutTime = OutTime;
                                                        dtattRow.SendToERP = 0;
                                                    }
                                                }
                                            }
                                            if (dtattRow == null)
                                                dtattRow = dstem.dtAttLog.AdddtAttLogRow(iGLCount, Convert.ToInt32(sdwEnrollNumber), idwDay, idwMonth, idwYear,
                                                       new DateTime(idwYear, idwMonth, idwDay), INTime, OutTime, 0);
                                            if (idwInOutMode == 2)
                                                Typevalue = "Break-In";
                                            if (idwInOutMode == 3)
                                                Typevalue = "Break-Out";
                                            if (idwInOutMode == 4)
                                                Typevalue = "OT-In";
                                            if (idwInOutMode == 5)
                                                Typevalue = "OT-Out";
                                            dstem.dtAttRealLog.AdddtAttRealLogRow(dtattRow.AttID, Typevalue, INOutTime, dgrow.Cells[1].Value.ToString());

                                        }

                                        //lvLogs.Items.Add(iGLCount.ToString());
                                        //lvLogs.Items[iIndex].SubItems.Add(sdwEnrollNumber);//modify by Darcy on Nov.26 2009
                                        //lvLogs.Items[iIndex].SubItems.Add(idwVerifyMode.ToString());
                                        //lvLogs.Items[iIndex].SubItems.Add(idwInOutMode.ToString());
                                        //lvLogs.Items[iIndex].SubItems.Add(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString());
                                        //lvLogs.Items[iIndex].SubItems.Add(idwWorkcode.ToString());
                                        //iIndex++;
                                    }
                                }
                                catch { }
                            }
                            dstem.AcceptChanges();
                        }
                        else
                        {
                            //Cursor = Cursors.Default;
                            axCZKEM1.GetLastError(ref idwErrorCode);
                            if (idwErrorCode != 0)
                            {
                                //MessageBox.Show("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString(), "Error");
                            }
                            else
                            {
                                // MessageBox.Show("No data from terminal returns!", "Error");
                            }
                        }
                        axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
                        axCZKEM1.Disconnect();
                    }
                    else
                    {
                        dgrow.Cells[5].Value = "Not Connect";
                        dgrow.Cells[5].Style.ForeColor = Color.Red;
                        // MessageBox.Show("Device Not Connect");
                    }

                    AECERP.SERLink.AECERPWin srv = new AECERP.SERLink.AECERPWin();
                    srv.Url = "http://" + txtURL.Text.Trim().Replace("http://", "") + "/SER/aecerpwin.asmx";// txtUrl.Text.Trim() + "/OMSConn/ConnString.asmx";//http://localhost/OMSCON
                    // SER.BIO_ATT.AECERPWinSoapClient srv = new SER.BIO_ATT.AECERPWinSoapClient();
                    //srv.Endpoint.Address = new System.ServiceModel.EndpointAddress("http://" + txtURL.Text.Trim().Replace("http://", "") + "/SER/aecerpwin.asmx");
                    // srv.AddEditEMPAttFromBioCompleted += srv_AddEditEMPAttFromBioCompleted;
                    if (getsetval == 1)
                    {
                        foreach (dsSettings.dtAttLogRow item in dstem.dtAttLog.Select("SendToERP=0"))
                        {
                            try
                            {
                                if (item.SendToERP == 0)
                                {
                                    foreach (dsSettings.dtAttRealLogRow drRL in dstem.dtAttRealLog.Select("AttID=" + item.AttID))
                                    {
                                        string val = srv.SaveImage((byte[])null, 0, item.EMPID, item.Date_att, item.INTime, item.OutTime,
                                            dgrow.Cells[1].Value.ToString(), drRL.TymType, drRL.INOUTTym);
                                        if (val.ToString() == "1")
                                            item.SendToERP = 0;
                                    }
                                }

                                // object rtval=  srv.GetString(1, ""); 
                                //string str2=srv.cr
                                //  srv.Open();
                                // System.Threading.Tasks.Task< SER.BIO_ATT.AddEditEMPAttFromBioResponse> x=
                                //srv.AddEditEMPAttFromBioCompleted += srv_AddEditEMPAttFromBioCompleted;
                                //srv.AddEditEMPAttFromBioAsync(item.EMPID, item.Date_att, item.INTime, item.OutTime, dgrow.Cells[1].Value.ToString());
                                //srv.AddEditEMPAttFromBioCompleted += srv_AddEditEMPAttFromBioCompleted;

                            }
                            catch (Exception ex) { wtirnotepadloagfile("001: " + ex.Message.ToString() + " " + DateTime.Today.ToLongTimeString()); }
                        }
                    }
                }
                catch (Exception ex) { wtirnotepadloagfile("002: " + ex.Message.ToString() + " " + DateTime.Today.ToLongTimeString()); }
            }
            if (getsetval == 0)
            {
                dt.AcceptChanges();
                dgvAttLog.DataSource = dt;

            }
            else
            {

                dstem.AcceptChanges();
                //dgvAttLog.DataSource = dstem.dtAttLog;
                dstem.WriteXml(Application.StartupPath + @"\AECBioAttLog_" + CurMonth + "_" + CurYear + ".xml");
                MessageBox.Show("Saved Successfully");
            }
        }

        private void CallBiTimeData(int getsetval, BackgroundWorker worker1)
        {
            int CurMonth = 0, CurYear = 0, CurDay = 0;
            CurDay = DateTime.Today.Day;
            CurMonth = DateTime.Today.Month;
            CurYear = DateTime.Today.Year;
            string sdwEnrollNumber = "";
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;
            int idwErrorCode = 0;
            int iGLCount = 0;
            DataTable dt = new DataTable();
            dstem = new dsSettings();
            if (getsetval == 0)
            {
                dt.Columns.Add("Count", typeof(string));
                dt.Columns.Add("EnrollNumbe", typeof(string));
                dt.Columns.Add("idwVerifyMode", typeof(string));
                dt.Columns.Add("idwInOutMode", typeof(string));
                dt.Columns.Add("idwYear", typeof(string));
                dt.Columns.Add("idwWorkcode", typeof(string));
            }
            else
            {
                try { dstem.ReadXml(Application.StartupPath + @"\AECBioAttLog_" + CurMonth + "_" + CurYear + ".xml"); }
                catch (Exception ex) { wtirnotepadloagfile("003: " + ex.Message.ToString() + " " + DateTime.Today.ToLongTimeString()); }
            }
            foreach (DataGridViewRow dgrow in dgvSettings.Rows)
            {
                try
                {
                    worker1.ReportProgress(15, "Row" + (dgrow.Index + 1).ToString());
                    int iMachineNumber = Convert.ToInt32(dgrow.Cells[4].Value);
                    if (dgrow.Cells[2].Value.ToString().Trim() == "")
                        break;
                    Boolean bIsConnected = axCZKEM1.Connect_Net(dgrow.Cells[2].Value.ToString().Trim(),
                        Convert.ToInt32(dgrow.Cells[3].Value));   // 4370 is port no of attendance machine
                    if (bIsConnected == true)
                    {
                        #region BioMetric


                        axCZKEM1.EnableDevice(iMachineNumber, true);//disable the device
                        axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
                        if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
                        {
                            while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
                                       out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                            {
                                try
                                {
                                    worker1.ReportProgress(30, "Row" + (dgrow.Index + 1).ToString() + "Data get from Divice of Day:" + idwDay.ToString());
                                    if (idwMonth == CurMonth && idwYear == CurYear)
                                    {
                                        iGLCount++;
                                        if (getsetval == 0)
                                        {
                                            DataRow drnew = dt.NewRow();
                                            drnew["Count"] = iGLCount.ToString();
                                            drnew["EnrollNumbe"] = sdwEnrollNumber.ToString();
                                            drnew["idwVerifyMode"] = idwVerifyMode.ToString();
                                            drnew["idwInOutMode"] = idwInOutMode.ToString();
                                            drnew["idwYear"] = idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString().ToString();
                                            drnew["idwWorkcode"] = idwWorkcode.ToString();
                                            dt.Rows.Add(drnew);
                                        }
                                        else
                                        {
                                            string Typevalue = "";
                                            dsSettings.dtAttLogRow dtattRow = null;
                                            dtattRow = dstem.dtAttLog.FindByEMPIDDayMonthYear(Convert.ToInt32(sdwEnrollNumber), idwDay, idwMonth, idwYear);
                                            //DataRow[] drs = dstem.dtAttLog.Select("Day=" + idwDay + "and Month" + idwMonth + " adn Year=" + idwYear);
                                            //dsSettings.dtAttLogRow dtattRow = null;
                                            string INTime = "", OutTime = "";
                                            string INOutTime = idwHour.ToString("00") + ":" + idwMinute.ToString("00"); ;
                                            if (idwInOutMode == 0)
                                            {
                                                Typevalue = "IN";
                                                INTime = INOutTime;//idwHour.ToString("00") + ":" + idwMinute.ToString("00");// +":" + idwSecond.ToString("00");
                                                if (dtattRow != null)
                                                {
                                                    if (dtattRow.INTime.Trim() == "")
                                                        dtattRow.INTime = INTime;
                                                    if (System.TimeSpan.Parse(dtattRow.INTime) > System.TimeSpan.Parse(INTime))
                                                    {
                                                        dtattRow.INTime = INTime;
                                                        dtattRow.SendToERP = 0;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Typevalue = "OUT";
                                                OutTime = INOutTime;// idwHour.ToString("00") + ":" + idwMinute.ToString("00");// +":" + idwSecond.ToString("00");
                                                if (dtattRow != null)
                                                {
                                                    if (dtattRow.OutTime.Trim() == "")
                                                        dtattRow.OutTime = dtattRow.INTime;
                                                    if (System.TimeSpan.Parse(dtattRow.OutTime) < System.TimeSpan.Parse(OutTime))
                                                    {
                                                        dtattRow.OutTime = OutTime;
                                                        dtattRow.SendToERP = 0;
                                                    }
                                                }
                                            }
                                            if (dtattRow == null)
                                                dtattRow = dstem.dtAttLog.AdddtAttLogRow(iGLCount, Convert.ToInt32(sdwEnrollNumber), idwDay, idwMonth, idwYear,
                                                       new DateTime(idwYear, idwMonth, idwDay), INTime, OutTime, 0);
                                            if (idwInOutMode == 2)
                                                Typevalue = "Break-In";
                                            if (idwInOutMode == 3)
                                                Typevalue = "Break-Out";
                                            if (idwInOutMode == 4)
                                                Typevalue = "OT-In";
                                            if (idwInOutMode == 5)
                                                Typevalue = "OT-Out";
                                            dstem.dtAttRealLog.AdddtAttRealLogRow(dtattRow.AttID, Typevalue, INOutTime, dgrow.Cells[1].Value.ToString());

                                        }

                                        //lvLogs.Items.Add(iGLCount.ToString());
                                        //lvLogs.Items[iIndex].SubItems.Add(sdwEnrollNumber);//modify by Darcy on Nov.26 2009
                                        //lvLogs.Items[iIndex].SubItems.Add(idwVerifyMode.ToString());
                                        //lvLogs.Items[iIndex].SubItems.Add(idwInOutMode.ToString());
                                        //lvLogs.Items[iIndex].SubItems.Add(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString());
                                        //lvLogs.Items[iIndex].SubItems.Add(idwWorkcode.ToString());
                                        //iIndex++;
                                    }
                                }
                                catch { }
                            }
                            dstem.AcceptChanges();
                        }
                        else
                        {
                            //Cursor = Cursors.Default;
                            axCZKEM1.GetLastError(ref idwErrorCode);
                            if (idwErrorCode != 0)
                            {
                                //MessageBox.Show("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString(), "Error");
                            }
                            else
                            {
                                // MessageBox.Show("No data from terminal returns!", "Error");
                            }
                        }
                        axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
                        axCZKEM1.Disconnect();
                        #endregion
                    }
                    else
                    {
                        dgrow.Cells[5].Value = "Not Connect";
                        dgrow.Cells[5].Style.ForeColor = Color.Red;
                        // MessageBox.Show("Device Not Connect");
                    }

                    AECERP.SERLink.AECERPWin srv = new AECERP.SERLink.AECERPWin();
                    srv.Url = "http://" + txtURL.Text.Trim().Replace("http://", "") + "/SER/aecerpwin.asmx";// txtUrl.Text.Trim() + "/OMSConn/ConnString.asmx";//http://localhost/OMSCON
                    // SER.BIO_ATT.AECERPWinSoapClient srv = new SER.BIO_ATT.AECERPWinSoapClient();
                    //srv.Endpoint.Address = new System.ServiceModel.EndpointAddress("http://" + txtURL.Text.Trim().Replace("http://", "") + "/SER/aecerpwin.asmx");
                    // srv.AddEditEMPAttFromBioCompleted += srv_AddEditEMPAttFromBioCompleted;
                    if (getsetval == 1)
                    {
                        int NoofRe = dstem.dtAttLog.Select("SendToERP=0").Length;
                        int NoofRe_count = 1;
                        int fact = 5;
                        foreach (dsSettings.dtAttLogRow item in dstem.dtAttLog.Select("SendToERP=0"))
                        {
                            try
                            {
                                if (item.SendToERP == 0)
                                {
                                    int NoCOunt = 1;
                                    int NoofRe_2 = dstem.dtAttRealLog.Select("AttID=" + item.AttID).Length;
                                    foreach (dsSettings.dtAttRealLogRow drRL in dstem.dtAttRealLog.Select("AttID=" + item.AttID))
                                    {
                                        string Mach_name = "";
                                        if (drRL.Mach_Name.Trim() == "")
                                            Mach_name = dgrow.Cells[1].Value.ToString();
                                        string val = srv.SaveImage((byte[])null, 1, item.EMPID, item.Date_att, item.INTime, item.OutTime,
                                            drRL.Mach_Name, drRL.TymType, drRL.INOUTTym);
                                        if (val.ToString() == "1")
                                            item.SendToERP = 0;
                                        worker1.ReportProgress(50, "Row" + (dgrow.Index + 1).ToString() + "Data Send to Server of Day:" + item.Date_att.ToString() + " -("
                                            + NoCOunt.ToString() + "/" + NoofRe_2.ToString() + ")/" + NoofRe_count.ToString() + "/" + NoofRe.ToString());
                                        NoCOunt++;
                                    }
                                }

                                // object rtval=  srv.GetString(1, ""); 
                                //string str2=srv.cr
                                //  srv.Open();
                                // System.Threading.Tasks.Task< SER.BIO_ATT.AddEditEMPAttFromBioResponse> x=
                                //srv.AddEditEMPAttFromBioCompleted += srv_AddEditEMPAttFromBioCompleted;
                                //srv.AddEditEMPAttFromBioAsync(item.EMPID, item.Date_att, item.INTime, item.OutTime, dgrow.Cells[1].Value.ToString());
                                //srv.AddEditEMPAttFromBioCompleted += srv_AddEditEMPAttFromBioCompleted;
                                NoofRe_count++;
                                if (fact == NoofRe_count)
                                {
                                    dstem.AcceptChanges();
                                    //dgvAttLog.DataSource = dstem.dtAttLog;
                                    dstem.WriteXml(Application.StartupPath + @"\AECBioAttLog_" + CurMonth + "_" + CurYear + ".xml");
                                    fact = fact + 5;
                                }
                            }
                            catch (Exception ex) { wtirnotepadloagfile("001: " + ex.Message.ToString() + " " + DateTime.Today.ToLongTimeString()); }
                        }
                    }
                }
                catch (Exception ex) { wtirnotepadloagfile("002: " + ex.Message.ToString() + " " + DateTime.Today.ToLongTimeString()); }
            }
            if (getsetval == 0)
            {
                dt.AcceptChanges();
                dgvAttLog.DataSource = dt;

            }
            else
            {

                dstem.AcceptChanges();
                //dgvAttLog.DataSource = dstem.dtAttLog;
                dstem.WriteXml(Application.StartupPath + @"\AECBioAttLog_" + CurMonth + "_" + CurYear + ".xml");
                MessageBox.Show("Saved Successfully");
            }
        }
        void srv_AddEditEMPAttFromBioCompleted(object sender, AECERP.SERLink.AddEditEMPAttFromBioCompletedEventArgs e)
        {
            string val = e.Result;
            //throw new NotImplementedException();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {

        }

        private void dgvSettings_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        int ValueConnect = 0;
        private void btnConnect_Click(object sender, EventArgs e)
        {

            try
            {
                ValueConnect = 1;
                if (!backgroundWorker1.IsBusy)
                {

                    ConnectAECERPAPP();
                    ConnectBioMetric();
                    // tsStatusLable.Text = "Ready"; tsStatusBar.Visible = true; backgroundWorker1.RunWorkerAsync();
                }
            }
            catch { }
            //ConnectAECERPAPP();
            //ConnectBioMetric();
        }

        private void ConnectAECERPAPP()
        {
            try
            {
                IsAECERPConnected = false;
                DataSet dsAppConfig = new DataSet();
                dsAppConfig.ReadXml(Application.StartupPath + "\\AppConfig.xml");
                //txtUrl.Text = dsAppConfig.Tables[0].Rows[0]["url"].ToString();
                AECERP.SERLink.AECERPWin srv = new AECERP.SERLink.AECERPWin();
                srv.Url = "http://" + txtURL.Text.Trim().Replace("http://", "") + "/SER/aecerpwin.asmx";// txtUrl.Text.Trim() + "/OMSConn/ConnString.asmx";//http://localhost/OMSCON
                //SER.BIO_ATT.AECERPWinSoapClient srv = new SER.BIO_ATT.AECERPWinSoapClient();
                //srv.Endpoint.Address = new System.ServiceModel.EndpointAddress("http://" + txtURL.Text.Trim().Replace("http://", "") + "/SER/aecerpwin.asmx");
                srv.GetString(1, ""); ;
                lblConnect.Text = "Connected";
                lblConnect.ForeColor = Color.Green;
                IsAECERPConnected = true;
                //dgrow.Cells[4].Value = "Connected";
                //           dgrow.Cells[4].Style.BackColor = Color.Green;
                //           // MessageBox.Show("Device Connected Successfully");
                //       }
                //       else
                //       {
                //     dgrow.Cells[4].Value = "Not Connect";
                //   dgrow.Cells[4].Style.BackColor = Color.Red;
                //                srv.Endpoint.Address. = "http://" + txtURL.Text.Trim().Replace("http://", "") + "/SER/aecerpwin.asmx";// txtUrl.Text.Trim() + "/OMSConn/ConnString.asmx";//http://localhost/OMSCON

                //            Uri baseAddress = new Uri("http://localhost:8000/HelloService");
                //string address = "http://localhost:8000/HelloService/MyService";

                //using (SER.BIO_ATT.AECERPWinSoapClient serviceHost = new SER.BIO_ATT.AECERPWinSoapClient(typeof(HelloService), baseAddress))
                //{
                //    serviceHost.AddServiceEndpoint(typeof(IHello), new BasicHttpBinding(), address);
                //    serviceHost.Open();
                //    Console.WriteLine("Press <enter> to terminate service");
                //    Console.ReadLine();
                //    serviceHost.Close();
                //}
            }
            catch (Exception ex)
            {
                wtirnotepadloagfile("006: " + ex.Message.ToString() + " " + DateTime.Today.ToLongTimeString());
                //MessageBox.Show(ex.Message.ToString());
                lblConnect.Text = "Not Connect";
                lblConnect.ForeColor = Color.Red;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                ValueConnect = 0;
                if (!backgroundWorker1.IsBusy) { tsStatusLable.Text = "Ready"; tsStatusBar.Visible = true; backgroundWorker1.RunWorkerAsync(); }

            }
            catch { }
        }


        void wtirnotepadloagfile(string msg)
        {
            string Path = Application.StartupPath + "\\AECLog.txt";
            StreamWriter fs = new StreamWriter(Path);
            fs.WriteLine(msg);
            //fs.AutoFlush = true;
            fs.Close();
            //System.Diagnostics.Process.Start(Path);
        }
        Boolean IsAECERPConnected = false;
        Boolean IsDiviceConnected = false;
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (backgroundWorker1.IsBusy)
                    backgroundWorker1.CancelAsync();
            }
            catch { }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker1 = sender as BackgroundWorker;
                worker1.ReportProgress(10, "Start");
                if (ValueConnect == 0)
                {
                    timer1.Stop();
                    timer2.Start();
                    ConnectAECERPAPP();
                    ConnectBioMetric();
                    if (IsAECERPConnected == true & IsDiviceConnected == true)
                        CallBiTimeData(1, worker1);
                }
                else if (ValueConnect == 1)
                {
                    ConnectAECERPAPP();
                    ConnectBioMetric();
                }


                // wtirnotepadloagfile("Start uploading aata: " + DateTime.Today.ToLongTimeString());
                //btnGetData_Click(btnSet, null);
                worker1.ReportProgress(99, "Done");
            }
            catch { }

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage > 100)
                tsStatusBar.Value = 100;
            else
                tsStatusBar.Value = e.ProgressPercentage;
            tsStatusLable.Text = e.UserState.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                tsStatusLable.Text = "Ready";
                tsStatusBar.Visible = false;
                timer1.Start();
                timer2.Stop();
                dgvAttLog.DataSource = dstem.dtAttLog;
                dstem = null;
            }
            catch { }
        }
    }
}
