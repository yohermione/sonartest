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

public partial class RetrieveUserKeepNoteList : System.Web.UI.Page
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
            ListDataView ldv = data.RetrieveUserNoteList(this.uInfo.Uno, "Y", this.nCurPage, this.nPageRow);

            this.nTotalRecord = ldv.TotalCount;
            this.listRepeater.DataSource = ldv.DV;
            this.listRepeater.DataBind();

            string pagingURL = "RetrieveUserNoteList.aspx?curPage={0}";

            this.pageNavigator.Text = NoteUtil.SetNavigator(this.nCurPage, this.nTotalRecord, this.nPageRow, pagingURL);
        }
    }

    protected void listRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlInputCheckBox chkbox = (HtmlInputCheckBox)e.Item.FindControl("chkbox");
        Label senderNm = (Label)e.Item.FindControl("senderNm");
        System.Web.UI.WebControls.Image progImg = (System.Web.UI.WebControls.Image)e.Item.FindControl("progImg");
        Label comment = (Label)e.Item.FindControl("comment");
        Label rgDt = (Label)e.Item.FindControl("rgDt");
        System.Web.UI.WebControls.Image btnReply = (System.Web.UI.WebControls.Image)e.Item.FindControl("btnReply");

        chkbox.Value = DataBinder.Eval(e.Item.DataItem, "SeqID").ToString();
        senderNm.Text = DataBinder.Eval(e.Item.DataItem, "Prog_Title").ToString();
        progImg.ImageUrl = DataBinder.Eval(e.Item.DataItem, "ProgImg").ToString();
        comment.Text = DataBinder.Eval(e.Item.DataItem, "Comment").ToString();
        rgDt.Text = ((DateTime)DataBinder.Eval(e.Item.DataItem, "RgDt")).ToString("yyyy-MM-dd HH:mm:ss");

        if ("Y" == DataBinder.Eval(e.Item.DataItem, "IsReply").ToString())
        {
            btnReply.Visible = false;
        }
        else
        {
            btnReply.Style.Add("cursor", "hand");
            btnReply.Attributes.Add("onclick", "doReply('" + DataBinder.Eval(e.Item.DataItem, "NoteID").ToString() + "', '" + DataBinder.Eval(e.Item.DataItem, "ProgCode").ToString() + "');");
        }

    }

    protected void btnDelete_Click(object sender, System.EventArgs e)
    {
        NoteData data = new NoteData();
        data.DeleteUserNoteInfo(this.hdnCheck.Value);

        Response.Redirect(Request.RawUrl, true);
    }
}