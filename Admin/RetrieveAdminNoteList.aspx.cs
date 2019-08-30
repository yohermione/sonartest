using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IMBC.FW.Util;
using IMBC.FW.DB;

public partial class Admin_RetrieveAdminNoteList : System.Web.UI.Page
{
    private string progCode;
    private int nCurPage;
    private int nPageRow = 5;
    private int nTotalRecord;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.progCode = IMBC.FW.Util.WebUtil.GetRequestQueryString("progCode", "");
        this.nCurPage = int.Parse(IMBC.FW.Util.WebUtil.GetRequestQueryString("curPage", "1"));

        if (!IsPostBack)
        {
            NoteData data = new NoteData();
            ListDataView ldv = data.RetrieveAdminNoteList(this.progCode, nCurPage, nPageRow);

            this.nTotalRecord = ldv.TotalCount;
            this.listRepeater.DataSource = ldv.DV;
            this.listRepeater.DataBind();

            string pagingURL = "RetrieveAdminNoteList.aspx?curPage={0}&progCode=" + this.progCode;

            this.pageNavigator.Text = NoteUtil.SetNavigator(this.nCurPage, this.nTotalRecord, this.nPageRow, pagingURL);
        }
    }

    protected void listRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        HtmlInputCheckBox chkbox = (HtmlInputCheckBox)e.Item.FindControl("chkbox");
        Label rgDt = (Label)e.Item.FindControl("rgDt");
        System.Web.UI.WebControls.Image progImg = (System.Web.UI.WebControls.Image)e.Item.FindControl("progImg");
        Label comment = (Label)e.Item.FindControl("comment");

        chkbox.Value = DataBinder.Eval(e.Item.DataItem, "NoteID").ToString();
        rgDt.Text = ((DateTime)DataBinder.Eval(e.Item.DataItem, "RgDt")).ToString("yyyy-MM-dd<BR>HH:mm:ss");
        progImg.ImageUrl = DataBinder.Eval(e.Item.DataItem, "ProgImg").ToString();
        comment.Text = DataBinder.Eval(e.Item.DataItem, "Comment").ToString();
    }

    protected void btnDelete_Click(object sender, System.EventArgs e)
    {
        NoteData data = new NoteData();
        data.DeleteAdminNoteInfo(this.hdnCheck.Value);

        Response.Redirect(Request.RawUrl, true);
    }
}