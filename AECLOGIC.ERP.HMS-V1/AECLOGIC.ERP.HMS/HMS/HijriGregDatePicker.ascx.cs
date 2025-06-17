
///////////////////////////////////////////////////////////////////////
///This User Control has been developed by : Yahya Mohammed Ammouri
///please send any commments or suggestion to yahya.ammouri@gmail.com
////////////////////////////////////////////////////////////////////////

using System;
using System.Web.UI;
using System.Globalization;
using System.ComponentModel;
using Aeclogic.Common.DAL;
using System.Web.UI.WebControls;
namespace AECLOGIC.ERP.COMMON
{
    public partial class HijriGregDatePicker : System.Web.UI.UserControl
    {
        // I used these classes for date conversion between Hijri and Gregorian
        private UmAlQuraCalendar hijriCal = new UmAlQuraCalendar();
        private GregorianCalendar gregCal = new GregorianCalendar(GregorianCalendarTypes.USEnglish);

        private CultureInfo arabicCulture = new CultureInfo("ar-SA");
        private CultureInfo englishCulture = new CultureInfo("en-US");

        private CultureInfo selected_culture;

        // the date format
        private string strFormat = "dd/MM/yyyy";

        // expose the selected Date (read/write)
        public DateTime SelectedCalendareDate
        {
            set
            {
                ctlCalendarLocalized.SelectedDate = value;
                ctlCalendarLocalized.VisibleDate = value;
                ctlCalendarLocalized_SelectionChanged(null, null);
                ddlYear.SelectedValue = ctlCalendarLocalized.SelectedDate.ToString(strFormat).Substring(6, 4).ToString();
                ddlMonth.SelectedIndex = int.Parse(ctlCalendarLocalized.SelectedDate.ToString(strFormat).Substring(3, 2).ToString()) - 1;
            }
            get { return ctlCalendarLocalized.SelectedDate; }
        }

        //I used Enum to show the little Intellisense box pop up with your options for culture
        public enum DefaultCultureOption
        {
            Hijri,
            Grgorian
        }
        public DefaultCultureOption DefaultCalendarCulture { get; set; }

        // expose the property to get Hijri text date
        public string getHijriDateText
        {
            get { return txtHijri.Text; }
        }

        // expose the property to get Gregorian text date
        public string getGregorianDateText
        {
            get { return txtGreg.Text; }
        }

        private int m_minValue = -10;
        private int m_maxValue = 10;

        //expose the property Minimum Year before current Year (Read/Write)
        public int MinYearCountFromNow
        {
            get
            {
                return m_minValue;
            }
            set
            {
                if (value >= this.MaxYearCountFromNow)
                {
                    throw new Exception("MinValue must be less than MaxValue.");
                }
                else
                {
                    m_minValue = value;
                }
            }
        }

        //expose the property Maximum Year After current Year (Read/Write)
        public int MaxYearCountFromNow
        {
            get
            {
                return m_maxValue;
            }
            set
            {
                if (value <= this.MinYearCountFromNow)
                {
                    throw new
                        Exception("MaxValue must be greater than MinValue.");
                }
                else
                {
                    m_maxValue = value;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                
                //Load the default culture assigned by user
                if (DefaultCalendarCulture == DefaultCultureOption.Grgorian)
                {
                    ddlLocaleChoice.SelectedValue = englishCulture.Name;
                }
                else
                {
                    ddlLocaleChoice.SelectedValue = arabicCulture.Name;
                }

                //incase the DefaultCalendarCulture=null the user control will take the first item in the dropdown list
                selected_culture = new System.Globalization.CultureInfo(ddlLocaleChoice.SelectedValue);
                System.Threading.Thread.CurrentThread.CurrentCulture = selected_culture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = selected_culture;

                //to populate the dropdown lists according to the selected culture
                if (selected_culture.Name == arabicCulture.Name)
                {
                    Populate_Hijri_Month_ddl();
                    Populate_Hijri_Years_ddl();
                    ddlLocaleChoice.SelectedValue = arabicCulture.Name;
                }
                else if (selected_culture.Name == englishCulture.Name)
                {
                    Populate_Greg_Month_ddl();
                    Populate_Greg_Years_ddl();
                    ddlLocaleChoice.SelectedValue = englishCulture.Name;
                }
              
                // to show hide the date picker div when user click on textbox or calendar image
                txtHijri.Attributes.Add("onclick", "showHide('" + this.whole_calendar.ClientID + "');");
                //txtGreg.Attributes.Add("onclick", "showHide('" + this.whole_calendar.ClientID + "');");
                imgCalendar.Attributes.Add("onclick", "showHide('" + this.whole_calendar.ClientID + "');");

                //To set the default view of date picker div to hidden
                ScriptManager.RegisterStartupScript(CalendarUpdatePanel, typeof(string), "ShowPopup" + this.whole_calendar.ClientID,
                                                    "document.getElementById('" + this.whole_calendar.ClientID + "').style.display = 'none';", true);

                //set the default calendar date to today
                ctlCalendarLocalized.TodaysDate = DateTime.Now;

                //in case the calendar control have selected date, populate the year and month drop down lists according to it
                if (ctlCalendarLocalized.SelectedDate.Date == DateTime.MinValue)
                {
                    ddlYear.SelectedValue = DateTime.Now.ToString(strFormat, selected_culture.DateTimeFormat).Substring(6, 4).ToString();
                    ddlMonth.SelectedIndex = int.Parse(DateTime.Now.ToString(strFormat).Substring(3, 2).ToString()) - 1;
                }

            }
            else
            {
                if (DefaultCalendarCulture == DefaultCultureOption.Grgorian)
                {
                    ddlLocaleChoice.SelectedValue = englishCulture.Name;
                }
                else
                {
                    ddlLocaleChoice.SelectedValue = arabicCulture.Name;
                }

                //incase the DefaultCalendarCulture=null the user control will take the first item in the dropdown list
                selected_culture = new System.Globalization.CultureInfo(ddlLocaleChoice.SelectedValue);
                System.Threading.Thread.CurrentThread.CurrentCulture = selected_culture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = selected_culture;

                //to populate the dropdown lists according to the selected culture
                if (selected_culture.Name == arabicCulture.Name)
                {
                    Populate_Hijri_Month_ddl();
                    Populate_Hijri_Years_ddl();
                    ddlLocaleChoice.SelectedValue = arabicCulture.Name;
                }
                else if (selected_culture.Name == englishCulture.Name)
                {
                    Populate_Greg_Month_ddl();
                    Populate_Greg_Years_ddl();
                    ddlLocaleChoice.SelectedValue = englishCulture.Name;
                }
                              //if (IsPostBack)
                //{
                //    string txtTemFrom = DateTime.Now.ToString().Replace('-','/');
                //    //txtTemFrom = txtTemFrom.ToString().Replace('-', '/');
                //    CultureInfo arabicCulture = new CultureInfo("ar-SA");
                //    DateTime tempFromDate = DateTime.ParseExact(txtTemFrom.ToString(), "dd/MM/yyyy", arabicCulture.DateTimeFormat);
                //    txtHijri.Text = tempFromDate.ToString();
                //    txtGreg.Text = txtTemFrom.ToString();
                //}
                //var senderAsControl = sender as Control;
                //string ParentUCname = senderAsControl.UniqueID;

                ////To get the potpack control name
                //string strPostBackControlName = getPostBackControlName();

                ////If the post back triggered from LocaleCalendar dropdown list, year dropdown list or month dropdown list the calendar div 
                ////will stay visible but if triggered by other controls the calendar div will be changed to hidden.
                //if (strPostBackControlName != ParentUCname + "$" + "ddlYear" && strPostBackControlName != ParentUCname + "$" + "ddlMonth"
                //    && strPostBackControlName != ParentUCname + "$" + "ddlLocaleChoice")
                //{
                //    //to manage multiple instances of user control postback, incase the postback happend due to culture changeed in current control,
                //    //the other user contrls culture drop down list to be changed accordingly. Also year and month dropdown lists according to culture 
                //    if (strPostBackControlName != "" && strPostBackControlName.Substring(strPostBackControlName.LastIndexOf("$")) == "$ddlLocaleChoice")
                //    {
                //        if (ddlLocaleChoice.SelectedValue == arabicCulture.Name)
                //        {
                //            ddlLocaleChoice.SelectedValue = englishCulture.Name;
                //            ddlMonth.Items.Clear();
                //            ddlYear.Items.Clear();
                //            Populate_Greg_Month_ddl();
                //            Populate_Greg_Years_ddl();

                //        }
                //        else if (ddlLocaleChoice.SelectedValue == englishCulture.Name)
                //        {
                //            ddlLocaleChoice.SelectedValue = arabicCulture.Name;
                //            ddlMonth.Items.Clear();
                //            ddlYear.Items.Clear();
                //            Populate_Hijri_Month_ddl();
                //            Populate_Hijri_Years_ddl();

                //        }
                //    }
                //    //To hide the calendar div in case of any postback other than the three controls (Culture ddl, Year ddl, Month ddl)
                ScriptManager.RegisterStartupScript(CalendarUpdatePanel, typeof(string), "ShowPopup" + this.whole_calendar.ClientID,
                                                    "document.getElementById('" + this.whole_calendar.ClientID + "').style.display = 'none';", true);
                //}
                txtHijri.Attributes.Add("onclick", "showHide('" + this.whole_calendar.ClientID + "');");
                //txtGreg.Attributes.Add("onclick", "showHide('" + this.whole_calendar.ClientID + "');");
                imgCalendar.Attributes.Add("onclick", "showHide('" + this.whole_calendar.ClientID + "');");
                selected_culture = new System.Globalization.CultureInfo(englishCulture.Name);
                System.Threading.Thread.CurrentThread.CurrentCulture = selected_culture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = selected_culture;
                //to keep the selected culture in case the post back triggered by any control    
                // selected_culture = new System.Globalization.CultureInfo(ddlLocaleChoice.SelectedValue);
                //System.Threading.Thread.CurrentThread.CurrentCulture = selected_culture;
                // System.Threading.Thread.CurrentThread.CurrentUICulture = selected_culture;

            }
        }

        //when culture changed in drodon list 
        protected void ddlLocaleChoice_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddlMonth.Items.Clear();
            ddlYear.Items.Clear();
            if (ddlLocaleChoice.SelectedValue == arabicCulture.Name)
            {
                Populate_Hijri_Month_ddl();
                Populate_Hijri_Years_ddl();
            }
            else if (ddlLocaleChoice.SelectedValue == englishCulture.Name)
            {
                Populate_Greg_Month_ddl();
                Populate_Greg_Years_ddl();
            }

            try
            {
                //Sates are represented by DateTime type in .NET. And DateTime is a struct e.g a value type, not a reference type and therefore it can never 
                //be null reference. Instead with valuer types, there's always a default value, which in this case is DateTime.MinValue
                if (ctlCalendarLocalized.SelectedDate.Date != DateTime.MinValue)
                {
                    // to change the year and month when the user change the locale calendar
                    //ddlYear.SelectedValue = ctlCalendarLocalized.SelectedDate.ToString(strFormat).Substring(6, 4).ToString();
                    ddlYear.SelectedValue = ctlCalendarLocalized.SelectedDate.ToString(strFormat, selected_culture.DateTimeFormat).Substring(6, 4).ToString();

                    ddlMonth.SelectedIndex = int.Parse(ctlCalendarLocalized.SelectedDate.ToString(strFormat).Substring(3, 2).ToString()) - 1;
                }
                else
                {
                    // to set current date as default date incase of the user did not select 
                    //ddlYear.SelectedValue = DateTime.Now.ToString(strFormat).Substring(6, 4).ToString();
                    ddlYear.SelectedValue = DateTime.Now.ToString(strFormat, selected_culture.DateTimeFormat).Substring(6, 4).ToString();
                    ddlMonth.SelectedIndex = int.Parse(DateTime.Now.ToString(strFormat).Substring(3, 2).ToString()) - 1;
                }
            }
            catch (System.ArgumentOutOfRangeException ex)
            {
                //this error happened when the selected year in one calendar does not have equivalant in the year dropdwon list
                //but the result will not be affected
                //do nothing
            }
            finally
            {

            }
        }

        //to populate hijri and gregorian textboxes when user select the day
        protected void ctlCalendarLocalized_SelectionChanged(object sender, EventArgs e)
        {
            if (ddlLocaleChoice.SelectedValue == arabicCulture.Name)
            {
                txtHijri.Text = ctlCalendarLocalized.SelectedDate.ToString("dd/MM/yyyy", new CultureInfo("ar-SA").DateTimeFormat);
                DateTime tempDate = DateTime.ParseExact(txtHijri.Text, strFormat, arabicCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                txtGreg.Text = tempDate.ToString(strFormat, englishCulture.DateTimeFormat);
            }
            else if (ddlLocaleChoice.SelectedValue == englishCulture.Name)
            {
                txtGreg.Text = ctlCalendarLocalized.SelectedDate.ToString("dd/MM/yyyy", new CultureInfo("en-US").DateTimeFormat);

                DateTime tempDate = DateTime.ParseExact(txtGreg.Text, strFormat, englishCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                txtHijri.Text = tempDate.ToString(strFormat, arabicCulture.DateTimeFormat);
            }

        }

        //to change the year in calendar control according to the selected month in the dropdown list 
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLocaleChoice.SelectedValue == arabicCulture.Name)
            {
                //here i added 1 since the index start from zero
                ctlCalendarLocalized.SelectedDate = new DateTime(int.Parse(ddlYear.SelectedValue), ddlMonth.SelectedIndex + 1, 1, hijriCal);
            }
            else if (ddlLocaleChoice.SelectedValue == englishCulture.Name)
            {
                //here i added 1 since the index start from zero
                ctlCalendarLocalized.SelectedDate = new DateTime(int.Parse(ddlYear.SelectedValue), ddlMonth.SelectedIndex + 1, 1, gregCal);
            }

            ctlCalendarLocalized.VisibleDate = ctlCalendarLocalized.SelectedDate;

        }

        //to change the year in calendar control according to the selected year in the dropdown list 
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLocaleChoice.SelectedValue == arabicCulture.Name)
            {
                //here I added 1 since the index start from zero
                ctlCalendarLocalized.SelectedDate = new DateTime(int.Parse(ddlYear.SelectedValue), ddlMonth.SelectedIndex + 1, 1, hijriCal);
            }
            else if (ddlLocaleChoice.SelectedValue == englishCulture.Name)
            {
                //here I added 1 since the index start from zero
                ctlCalendarLocalized.SelectedDate = new DateTime(int.Parse(ddlYear.SelectedValue), ddlMonth.SelectedIndex + 1, 1, gregCal);
            }
            ctlCalendarLocalized.VisibleDate = ctlCalendarLocalized.SelectedDate;

        }

        //To populate the Higri Months in dropdown list
        private void Populate_Hijri_Month_ddl()
        {
            ddlMonth.Items.Add("محرم");
            ddlMonth.Items.Add("صفر");
            ddlMonth.Items.Add("ربيع الأول");
            ddlMonth.Items.Add("ربيع الثاني");
            ddlMonth.Items.Add("جمادي الأول");
            ddlMonth.Items.Add("جمادي الثاني");
            ddlMonth.Items.Add("رجب");
            ddlMonth.Items.Add("شعبان");
            ddlMonth.Items.Add("رمضان");
            ddlMonth.Items.Add("شوال");
            ddlMonth.Items.Add("ذو القعدة");
            ddlMonth.Items.Add("ذو الحجة");
        }

        //To populate the Gregorian Months in dropdown list
        private void Populate_Greg_Month_ddl()
        {
            ddlMonth.Items.Add("January");
            ddlMonth.Items.Add("February");
            ddlMonth.Items.Add("March");
            ddlMonth.Items.Add("April");
            ddlMonth.Items.Add("May");
            ddlMonth.Items.Add("June");
            ddlMonth.Items.Add("July");
            ddlMonth.Items.Add("August");
            ddlMonth.Items.Add("September");
            ddlMonth.Items.Add("October");
            ddlMonth.Items.Add("November");
            ddlMonth.Items.Add("December");
        }

        //To populate the Grgorian Years in dropdown list
        private void Populate_Greg_Years_ddl()
        {
            //Year list can be extended
            int intYear;
            for (intYear = Convert.ToInt16(DateTime.Now.Year) + MinYearCountFromNow; intYear <= DateTime.Now.Year + MaxYearCountFromNow; intYear++)
            {
                ddlYear.Items.Add(Convert.ToString(intYear));
            }
            ddlYear.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;
        }

        //To populate the Hijri Years in dropdown list
        private void Populate_Hijri_Years_ddl()
        {
            //To Get the year according to Hijri Calendar
            string strCurrentYear = DateTime.Now.ToString("yyyy", new System.Globalization.CultureInfo(arabicCulture.Name));

            //Year list can be extended
            int intYear;
            for (intYear = Convert.ToInt16(strCurrentYear) + MinYearCountFromNow; intYear <= Convert.ToInt16(strCurrentYear) + MaxYearCountFromNow; intYear++)
            {
                ddlYear.Items.Add(Convert.ToString(intYear));
            }
            ddlYear.Items.FindByValue(strCurrentYear).Selected = true;
        }

        //To get the potpack control name
        private string getPostBackControlName()
        {
            Control control = null;
            //first we will check the "__EVENTTARGET" because if post back made by the controls
            //which used "_doPostBack" function also available in Request.Form collection.
            string ctrlname = Page.Request.Params["__EVENTTARGET"];
            if (ctrlname != null && ctrlname != String.Empty)
            {
                control = Page.FindControl(ctrlname);
            }

            if (control == null)
            {
                return string.Empty;
            }
            else
            {
                //to catch the control name in case of multiple instances
                return control.UniqueID;
            }
        }
        public void setHijriDateText(string value)
        {
            this.txtHijri.Text = value;
        }

        public void setGregorianText(string value)
        {
            this.txtGreg.Text = value;
        }
    }

}