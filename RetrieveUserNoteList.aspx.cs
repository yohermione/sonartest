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
using System.Configuration;
using IMBC.FW.DB;
using IMBC.FW.Util;

public partial class RetrieveUserNoteList : System.Web.UI.Page
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

        if (System.Web.Configuration.WebConfigurationManager.AppSettings["pmstart"].ToString() == "ON")
        {
            Response.Redirect("http://imbc.com/broad/radio/minimbc/new_notice/notice_con/index.html");
            Response.End();
        }

        if (!IsPostBack)
        {
            NoteData data = new NoteData();

            if (this.nCurPage == 1)
            {
                this.logRepeater.DataSource = data.RetrieveScriptLogList();
                this.logRepeater.DataBind();
            }

            ListDataView ldv = data.RetrieveUserNoteList(this.uInfo.Uno, "N", this.nCurPage, this.nPageRow);

            this.nTotalRecord = ldv.TotalCount;
            this.listRepeater.DataSource = ldv.DV;
            this.listRepeater.DataBind();

            string pagingURL = "RetrieveUserNoteList.aspx?curPage={0}";

            this.pageNavigator.Text = NoteUtil.SetNavigator(this.nCurPage, this.nTotalRecord, this.nPageRow, pagingURL);
        }
    }

    protected void listRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
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

    protected void btnMove_Click(object sender, System.EventArgs e)
    {
        NoteData data = new NoteData();
        data.MoveUserNoteInfo(this.hdnCheck.Value);

        Response.Redirect(Request.RawUrl, true);
    }

    protected void btnDelete_Click(object sender, System.EventArgs e)
    {
        NoteData data = new NoteData();
        data.DeleteUserNoteInfo(this.hdnCheck.Value);

        Response.Redirect(Request.RawUrl, true);
    }

    protected void logRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        Label senderNm = (Label)e.Item.FindControl("logSenderNm");
        Label comment = (Label)e.Item.FindControl("logComment");
        Label rgDt = (Label)e.Item.FindControl("logRgDt");

        senderNm.Text = DataBinder.Eval(e.Item.DataItem, "Prog_Title").ToString();
        comment.Text = DataBinder.Eval(e.Item.DataItem, "Msg").ToString();
        rgDt.Text = ((DateTime)DataBinder.Eval(e.Item.DataItem, "Reg_Date")).ToString("yyyy-MM-dd HH:mm:ss");

        comment.Text = comment.Text.Replace("<style>body {background-color:#FCF8FE;font-family:Dotum;font-size:9pt;padding:0px;margin:0px;}</style>", "");
        comment.Text = comment.Text.Replace("<base target='_blank' /><script>function openWin(aa){if((aa=='')||(aa=='http://')) {return;}else { window.open(aa); }document.location.href='EVENT:CLOSE'}</script>", "");
        comment.Text = comment.Text.Replace("<span style='cursor:hand' onclick=\"openWin('http://')\" onmouseover=\"javascript:{document.location.href='EVENT:MOUSEOVER'}\" >", "<span>");
        comment.Text = comment.Text.Replace("onmouseover=\"javascript:{document.location.href='EVENT:MOUSEOVER'}\"", "");
    }
}