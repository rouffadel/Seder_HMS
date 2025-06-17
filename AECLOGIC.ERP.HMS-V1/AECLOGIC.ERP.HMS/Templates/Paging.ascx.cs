using System;
using System.Web.UI.WebControls;


namespace AECLOGIC.ERP.COMMON
{
    public partial class Paging : System.Web.UI.UserControl
    {
        #region delegate

        public delegate void PageFirst(object sender, EventArgs e);
        public event PageFirst FirstClick;

        public delegate void PagePrevious(object sender, EventArgs e);
        public event PagePrevious PreviousClick;

        public delegate void PageNext(object sender, EventArgs e);
        public event PageNext NextClick;

        public delegate void PageLast(object sender, EventArgs e);
        public event PageLast LastClick;

        public delegate void PageChange(object sender, EventArgs e);
        public event PageChange ChangeClick;

        public delegate void ShowRowsChange(object sender, EventArgs e);
        public event ShowRowsChange ShowRowsClick;

        #endregion

        #region private
        private int _CurrentPage;
        private int _NoOfPages;
        private int _ShowRows;

        public int ShowRows
        {
            get
            {
                return Convert.ToInt32(ddlShowRows.SelectedValue);
            }
            set
            {
                ViewState["ShowRows"] = value;
                ddlShowRows.SelectedValue = value.ToString();
            }
        }

        public int ShowPage
        {
            get
            {
                return ddlGotoPages == null ? 1 : Convert.ToInt32(ddlGotoPages.SelectedValue);
            }
        }

        public int NoOfPages
        {
            get { return _NoOfPages; }
            set
            {
                _NoOfPages = value;
                BindPageDropdownList();
            }
        }

        #endregion

        #region Methods
        private void BindPageDropdownList()
        {
            ddlGotoPages.Items.Clear();
            for (int index = 1; index <= _NoOfPages; index++)
            {
                ddlGotoPages.Items.Add(index.ToString());
            }
            if (ddlGotoPages.Items.Count == 0)
            {
                this.CurrentPage = 1;
                ddlGotoPages.Items.Add("1");
            }
            else
            {
                if (ddlGotoPages.Items.Count >= CurrentPage)
                    ddlGotoPages.SelectedValue = CurrentPage.ToString();
            }
        }

        public int CurrentPage
        {
            get { return Convert.ToInt32(ViewState["CurrentPage"]); }
            set { ViewState["CurrentPage"] = value; }
        }

        new public bool Visible
        {
            get { return dvPager.Visible; }
            set { dvPager.Visible = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  ViewState["ShowRows"] = 10;
                ViewState["CurrentPage"] = "1";
            }
        }
        public void Bind(int CurrentPage, int NoOfPages, int NoofRecords, int PageSize, int ShowRows)
        {
            ddlShowRows.Items.Clear();
            for (int i = PageSize; i <= 500; i += PageSize)
                ddlShowRows.Items.Add(new ListItem(i.ToString(), i.ToString()));
            ddlShowRows.SelectedValue = ShowRows.ToString();
            this.ShowRows = ShowRows;
            Bind(CurrentPage, NoOfPages, NoofRecords, ShowRows);
        }
        public void Bind(int CurrentPage, int NoOfPages, int NoofRecords, int _ShowRows)
        {
            if (NoofRecords == 0)
            {
                this.Visible = false;
            }
            else
            {
                this.Visible = true;
                this.CurrentPage = CurrentPage;
                bool AllSeelcted = false;
                ListItem Li = ddlShowRows.Items.FindByText("ALL");
                if (Li != null)
                {
                    AllSeelcted = Li.Selected;
                    ddlShowRows.Items.Remove(Li);
                }
                ddlShowRows.Items.Add(new ListItem("ALL", NoofRecords.ToString()));
                if (AllSeelcted)
                    ddlShowRows.SelectedValue = NoofRecords.ToString();
                ddlGotoPages.SelectedValue = CurrentPage.ToString();
                this.NoOfPages = NoOfPages;
                if ((this.ShowRows * CurrentPage) > NoofRecords)
                    lblShowPages.Text = ((this.ShowRows * (CurrentPage - 1)) + 1).ToString() + " - " + (NoofRecords).ToString() + " of " + NoofRecords.ToString();
                else
                    lblShowPages.Text = ((this.ShowRows * (CurrentPage - 1)) + 1).ToString() + " - " + (this.ShowRows * CurrentPage).ToString() + " of " + NoofRecords.ToString();
                if (CurrentPage == 1)
                {
                    lnkBtnFirst.Enabled = false;
                    lnkBtnPrevious.Enabled = false;
                }
                else
                {
                    lnkBtnFirst.Enabled = true;
                    lnkBtnPrevious.Enabled = true;
                }
                if (CurrentPage == Convert.ToInt32(ddlGotoPages.Items[ddlGotoPages.Items.Count - 1].Value))
                {
                    lnkBtnNext.Enabled = false;
                    lnkBtnLast.Enabled = false;
                }
                else
                {
                    lnkBtnNext.Enabled = true;
                    lnkBtnLast.Enabled = true;
                }
            }
        }

        public void Bind_New(int CurrentPage, int NoOfPages, int NoofRecords)
        {
            if (NoofRecords == 0)
            {
                this.Visible = false;
            }
            else
            {
                ViewState["PageNo"] = 0;
                ViewState["PageNo"] = Convert.ToInt32(ddlGotoPages.SelectedValue);
                this.Visible = true;
                this.CurrentPage = CurrentPage;
                ddlGotoPages.SelectedValue = CurrentPage.ToString();
                this.NoOfPages = NoOfPages;
                lblShowPages.Text = ((this.ShowRows * (CurrentPage - 1)) + 1).ToString() + " - " + (this.ShowRows * CurrentPage).ToString() + " of " + NoofRecords.ToString();
                if (CurrentPage == 1)
                {
                    lnkBtnFirst.Enabled = false;
                    lnkBtnPrevious.Enabled = false;
                }
                else
                {
                    lnkBtnFirst.Enabled = true;
                    lnkBtnPrevious.Enabled = true;
                }
                if (CurrentPage == Convert.ToInt32(ddlGotoPages.Items[ddlGotoPages.Items.Count - 1].Value))
                {
                    lnkBtnNext.Enabled = false;
                    lnkBtnLast.Enabled = false;
                }
                else
                {
                    lnkBtnNext.Enabled = true;
                    lnkBtnLast.Enabled = true;
                }
            }
        }

        protected void ddlGotoPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlGotoPages.Items.Count > 0)
                {
                    this.CurrentPage = Convert.ToInt32(ddlGotoPages.SelectedValue);
                    ChangeClick(this, e);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void lnkBtnFirst_Click(object sender, EventArgs e)
        {
            try
            {
                this.CurrentPage = 1;
                if (FirstClick != null)
                    FirstClick(this, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void lnkBtnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlGotoPages.Items.Count > 0)
                {
                    this.CurrentPage = Convert.ToInt32(ddlGotoPages.SelectedValue);
                    if (this.CurrentPage > 1)
                        this.CurrentPage -= 1;
                    if (PreviousClick != null)
                        PreviousClick(this, e);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void lnkBtnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlGotoPages.Items.Count > 0)
                {
                    this.CurrentPage = Convert.ToInt32(ddlGotoPages.SelectedValue);
                    if (this.CurrentPage < Convert.ToInt32(ddlGotoPages.Items[ddlGotoPages.Items.Count - 1].Value))
                        this.CurrentPage += 1;
                    if (NextClick != null)
                        NextClick(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void lnkBtnLast_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlGotoPages.Items.Count > 0)
                {
                    this.CurrentPage = Convert.ToInt32(ddlGotoPages.Items[ddlGotoPages.Items.Count - 1].Value);
                    this.ddlGotoPages.SelectedValue = this.CurrentPage.ToString();
                    if (LastClick != null)
                        LastClick(this, e);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ddlShowRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.CurrentPage = 1;
                ShowRowsClick(this, e);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion Methods
    }

}