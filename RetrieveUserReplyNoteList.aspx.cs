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
using IMBC.FW.DB;
using IMBC.FW.Util;


public partial class RetrieveUserReplyNoteList : System.Web.UI.Page
{
    private int nCurPage;
    private int nPageRow = 4;
    private int nTotalRecord;

    private UserInfo uInfo;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.nCurPage = int.Parse(IMBC.FW.Util.WebUtil.GetRequestQueryString("curPage", "1"));

        this.uInfo = new UserInfo();

        if (this.uInfo.IsLogin == false)
        {
            this.uInfo.RedirectLoginPagePopup();
            Response.End();
        }

        if (!IsPostBack)
        {
            NoteData data = new NoteData();
            ListDataView ldv = data.RetrieveUserReplyNoteList(this.uInfo.Uno, this.nCurPage, this.nPageRow);

            this.nTotalRecord = ldv.TotalCount;
            this.listRepeater.DataSource = ldv.DV;
            this.listRepeater.DataBind();

            string pagingURL = "RetrieveUserReplyNoteList.aspx?curPage={0}";

            this.pageNavigator.Text = NoteUtil.SetNavigator(this.nCurPage, this.nTotalRecord, this.nPageRow, pagingURL);
        }
    }

    protected void listRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        HtmlInputCheckBox chkbox = (HtmlInputCheckBox)e.Item.FindControl("chkbox");
        Label receiverNm = (Label)e.Item.FindControl("receiverNm");
        Label comment = (Label)e.Item.FindControl("comment");
        Label rgDt = (Label)e.Item.FindControl("rgDt");

        receiverNm.Text = DataBinder.Eval(e.Item.DataItem, "Prog_Title").ToString();
        comment.Text = IMBC.FW.Util.WebUtil.ReplaceViewData(DataBinder.Eval(e.Item.DataItem, "Comment").ToString());
        rgDt.Text = ((DateTime)DataBinder.Eval(e.Item.DataItem, "RgDt")).ToString("yyyy-MM-dd HH:mm:ss");
    }
}