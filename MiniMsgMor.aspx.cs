using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using IMBC.FW.DB;
using IMBC.FW.Util;

public partial class MiniMsgMor : PageBase
{
    protected string progCode_STFM;
    protected string progCode_FM4U;

    private int nCurPage;
    private int nCurPage2;
    private int nPageRow = 10;
    private int nPageRow2 = 10;
    private int nTotalRecord;
    private int nTotalRecord2;
    protected UserInfo uInfo;
    protected MemoMstInfo mstInfo;
    protected MemoMstInfo mstInfo2;
    protected string topHtml;
    protected string bottomHtml;
    private NoteData data;
    private bool IsAdmin = false;

    private string search;
    private string search2;
    private string searchWord;
    private string searchWord2;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.nCurPage = int.Parse(IMBC.FW.Util.WebUtil.GetRequestQueryString("curPage", "1"));
        this.nCurPage2 = int.Parse(IMBC.FW.Util.WebUtil.GetRequestQueryString("curPage2", "1"));

        this.search = IMBC.FW.Util.WebUtil.EncodeHTML(WebUtil.replaceSQLInjections(IMBC.FW.Util.WebUtil.GetRequestQueryString("search", "")));
        this.search2 = IMBC.FW.Util.WebUtil.EncodeHTML(WebUtil.replaceSQLInjections(IMBC.FW.Util.WebUtil.GetRequestQueryString("search2", "")));
        this.searchWord = IMBC.FW.Util.WebUtil.RemoveHTMLTag(WebUtil.replaceSQLInjections(IMBC.FW.Util.WebUtil.GetRequestQueryString("searchWord", ""))).Replace("+", "").Replace("%", "").Replace("'", "");
        this.searchWord2 = IMBC.FW.Util.WebUtil.RemoveHTMLTag(WebUtil.replaceSQLInjections(IMBC.FW.Util.WebUtil.GetRequestQueryString("searchWord2", ""))).Replace("+", "").Replace("%", "").Replace("'", "");

        this.uInfo = new UserInfo();
        this.data = new NoteData();


        if (this.uInfo.IsLogin == false)
        {
            
            imgSave.Src = "http://img.imbc.com/mini/UserNote/images/mini_memo_login.jpg";
            img1.Src = "http://img.imbc.com/mini/UserNote/images/mini_memo_login.jpg";

            System.Web.HttpContext httpContext = System.Web.HttpContext.Current;

            httpContext.Response.Cookies["IMBCURL"].Value = httpContext.Server.UrlEncode("http://" + httpContext.Request.ServerVariables["SERVER_NAME"] + httpContext.Request.RawUrl);
            httpContext.Response.Cookies["IMBCURL"].Domain = "imbc.com";
            httpContext.Response.Cookies["IMBCURL"].Path = "/";
            httpContext.Response.Cookies["IMBCURL"].Secure = false;

            imgSave.Attributes.Add("onclick", "location.href='http://member.imbc.com/Login/Login.aspx?TemplateId=Popup'; return false;");
            txtComment.Enabled = false;

            img1.Attributes.Add("onclick", "location.href='http://member.imbc.com/Login/Login.aspx?TemplateId=Popup'; return false;");
            txtComment1.Enabled = false;
        }
        else
        {
            this.IsAdmin = data.IsAdminUserDB(this.progCode_STFM, uInfo.UserID);
            imgSave.Attributes.Add("onclick", "doSave();");
            img1.Attributes.Add("onclick", "doSave();");

        }

        if (!IsPostBack)
        {
            // 현재 시간의 편성 정보 가져오기
            ScheduleMBCList STFM = new NoteData().RetrieveScheduleListForMBC("STFM");
            List<ScheduleMBC> newSTFM = new List<ScheduleMBC>();

            newSTFM = STFM.FindAll(
                   delegate(ScheduleMBC sc)
                   {
                       return DateTime.Parse(sc.EndTime) > DateTime.Parse(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) && sc.Day == GetDayOfWeek(DateTime.Now);
                   }
            );
            newSTFM = newSTFM.GetRange(0, 1);

            foreach (ScheduleMBC sc in newSTFM)
            {
                progCode_STFM = new NoteData().GetProgCode(sc.BroadCastID, sc.GroupID).Trim();
                progTitle.Text = sc.ProgramTitle + progCode_STFM;
//                Response.Write(sc.ProgramTitle + " : " + sc.BroadCastID + " :" + sc.GroupID);
            }

            try
            {
                this.mstInfo = data.RetrieveMemoMstInfo(this.progCode_STFM);
                this.progTitle.Text = mstInfo.progTitle;
                this.tblColor.BgColor = mstInfo.tblColor;
                this.nPageRow = mstInfo.pageRow;

                if (this.mstInfo.memoType == "2")
                {
                    imgSave.Visible = false;
                    txtComment.Text = "mini 에서만 작성하실 수 있습니다.";
                    txtComment.Enabled = false;
                }
            }
            catch { }


            try
            {
                ListDataView ldv = data.RetrieveMemoList(this.progCode_STFM, this.nCurPage, this.nPageRow, this.search, this.searchWord);
                if (ldv != null)
                {
                    this.nTotalRecord = ldv.TotalCount;
                    this.listRepeater.DataSource = ldv.DV;
                    this.listRepeater.DataBind();

                    string pagingURL = "MiniMsgMor.aspx?curPage={0}&progCode=" + this.progCode_STFM + "&search=" + this.search + "&searchWord=" + this.searchWord;

                    this.pageNavigator.Text = NoteUtil.SetNavigator(this.nCurPage, this.nTotalRecord, this.nPageRow, pagingURL);
                }
            }
            catch { }

          

         


            ScheduleMBCList FM4U = new NoteData().RetrieveScheduleListForMBC("FM4U");
            List<ScheduleMBC> newFM4U = new List<ScheduleMBC>();

            newFM4U = FM4U.FindAll(
                  delegate(ScheduleMBC sc)
                  {
                      return DateTime.Parse(sc.EndTime) > DateTime.Parse(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) && sc.Day == GetDayOfWeek(DateTime.Now);
                  }
                );
            newFM4U = newFM4U.GetRange(0, 1);
            foreach (ScheduleMBC sc in newFM4U)
            {
                progCode_FM4U = new NoteData().GetProgCode(sc.BroadCastID, sc.GroupID).Trim();
                Label1.Text = sc.ProgramTitle;
                //Response.Write(sc.ProgramTitle + ":" + sc.BroadCastID + " :" + sc.GroupID);
            }

            try
            {
                this.mstInfo2 = data.RetrieveMemoMstInfo(this.progCode_FM4U);
                this.tblColor.BgColor = mstInfo2.tblColor;
                this.tblColor2.BgColor = mstInfo2.tblColor;
                this.nPageRow2 = mstInfo2.pageRow;

                if (this.mstInfo2.memoType == "2")
                {
                    img1.Visible = false;
                    txtComment1.Text = "mini 에서만 작성하실 수 있습니다.";
                    txtComment1.Enabled = false;
                }
            }
            catch { }

            try
            {
                ListDataView ldv2 = data.RetrieveMemoList(this.progCode_FM4U, this.nCurPage2, this.nPageRow2, this.search2, this.searchWord2);

                this.nTotalRecord2 = ldv2.TotalCount;
                this.listRepeater2.DataSource = ldv2.DV;
                this.listRepeater2.DataBind();

                string pagingURL2 = "MiniMsgMor.aspx?curPage2={0}&progCode=" + this.progCode_FM4U + "&search2=" + this.search + "&searchWord2=" + this.searchWord;

                this.Label2.Text = NoteUtil.SetNavigator(this.nCurPage2, this.nTotalRecord2, this.nPageRow2, pagingURL2);

            }
            catch { }

         
        }
    }

    private string GetDayOfWeek(DateTime dateTime)
    {
        DayOfWeek day = dateTime.DayOfWeek;
        string week = string.Empty;
        switch (day)
        {
            case DayOfWeek.Monday:
                week = "월";
                break;
            case DayOfWeek.Tuesday:
                week = "화";
                break;
            case DayOfWeek.Wednesday:
                week = "수";
                break;
            case DayOfWeek.Thursday:
                week = "목";
                break;
            case DayOfWeek.Friday:
                week = "금";
                break;
            case DayOfWeek.Saturday:
                week = "토";
                break;
            case DayOfWeek.Sunday:
                week = "일";
                break;
            default:
                break;
        }
        return week;
    }

    protected void listRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label seqID = (Label)e.Item.FindControl("seqID");
        System.Web.UI.WebControls.Image imgType = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgType");
        Label userNm = (Label)e.Item.FindControl("userNm");
        Label comment = (Label)e.Item.FindControl("comment");
        ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
        Label rgDt = (Label)e.Item.FindControl("rgDt");
        Literal lblDevice = (Literal)e.Item.FindControl("lblDevice");


        string rank = DataBinder.Eval(e.Item.DataItem, "rank").ToString();

        seqID.Text = DataBinder.Eval(e.Item.DataItem, "SeqID").ToString();

        if (this.mstInfo.showType == "1")
            userNm.Text = DataBinder.Eval(e.Item.DataItem, "UserID").ToString();
        else
            userNm.Text = DataBinder.Eval(e.Item.DataItem, "UserNm").ToString();

        comment.Text = Server.HtmlEncode(DataBinder.Eval(e.Item.DataItem, "Comment").ToString()).Replace("<", "&lt;").Replace(">", "&gt;");


        comment.Style.Add("color", DataBinder.Eval(e.Item.DataItem, "FontColor").ToString());

        lblDevice.Text = DataBinder.Eval(e.Item.DataItem, "Device").ToString();

        rgDt.Text = ((DateTime)DataBinder.Eval(e.Item.DataItem, "rgDt")).ToString("yyyy-MM-dd HH:mm:ss");

        string type = DataBinder.Eval(e.Item.DataItem, "MemoType").ToString();

        if (type == "1")
        {
            imgType.ImageUrl = "http://img.imbc.com/mini/UserNote/images/mini_memo_icon_web.gif";
            imgType.AlternateText = "웹사연";
        }
        else if (type == "2")
        {
            imgType.ImageUrl = "http://img.imbc.com/mini/UserNote/images/mini_memo_icon_memo.gif";
            if (this.IsAdmin == false)
            {
                comment.Text = "<img src='http://img.imbc.com/mini/UserNote/images/mini_memo_lock.gif' align=absmiddle>";
                imgType.AlternateText = "개인쪽지";
            }
        }
        else if (type == "4")
        {
            imgType.ImageUrl = "http://img.imbc.com/mini/UserNote/images/mini_memo_icon_phone.gif";
        }
        else
        {
            imgType.ImageUrl = "http://img.imbc.com/mini/UserNote/images/mini_memo_icon_mini.gif";
            imgType.AlternateText = "미니사연";
        }

        btnDelete.CommandArgument = seqID.Text;
        btnDelete.Attributes.Add("onclick", "return confirmDelete();");

        if (uInfo.Uno.ToString() != DataBinder.Eval(e.Item.DataItem, "Uno").ToString() && uInfo.UserID.ToLower() != "imbc" && uInfo.UserID.ToLower() != "imradio")
            btnDelete.Visible = false;

        if (rank != "0")
        {
            seqID.Text = "■";
            userNm.Font.Bold = true;
            comment.Font.Bold = true;
            comment.ForeColor = Color.DarkBlue;

        }

        if (this.IsAdmin)
        {
            userNm.Text = "<a href=\"javascript:fNewWin('" + DataBinder.Eval(e.Item.DataItem, "UserID").ToString() + "', '" + DataBinder.Eval(e.Item.DataItem, "Uno").ToString() + "');\">" + userNm.Text + "</a>";
        }
    }

    protected void listRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label seqID = (Label)e.Item.FindControl("seqID");
        System.Web.UI.WebControls.Image imgType = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgType");
        Label userNm = (Label)e.Item.FindControl("userNm");
        Label comment = (Label)e.Item.FindControl("comment");
        ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
        Label rgDt = (Label)e.Item.FindControl("rgDt");
        Literal lblDevice = (Literal)e.Item.FindControl("lblDevice");

        string rank = DataBinder.Eval(e.Item.DataItem, "rank").ToString();

        seqID.Text = DataBinder.Eval(e.Item.DataItem, "SeqID").ToString();

        if (this.mstInfo2.showType == "1")
            userNm.Text = DataBinder.Eval(e.Item.DataItem, "UserID").ToString();
        else
            userNm.Text = DataBinder.Eval(e.Item.DataItem, "UserNm").ToString();

        comment.Text = Server.HtmlEncode(DataBinder.Eval(e.Item.DataItem, "Comment").ToString()).Replace("<", "&lt;").Replace(">", "&gt;");


        comment.Style.Add("color", DataBinder.Eval(e.Item.DataItem, "FontColor").ToString());

        lblDevice.Text = DataBinder.Eval(e.Item.DataItem, "Device").ToString();

        rgDt.Text = ((DateTime)DataBinder.Eval(e.Item.DataItem, "rgDt")).ToString("yyyy-MM-dd HH:mm:ss");

        string type = DataBinder.Eval(e.Item.DataItem, "MemoType").ToString();

        if (type == "1")
        {
            imgType.ImageUrl = "http://img.imbc.com/mini/UserNote/images/mini_memo_icon_web.gif";
            imgType.AlternateText = "웹사연";
        }
        else if (type == "2")
        {
            imgType.ImageUrl = "http://img.imbc.com/mini/UserNote/images/mini_memo_icon_memo.gif";
            if (this.IsAdmin == false)
            {
                comment.Text = "<img src='http://img.imbc.com/mini/UserNote/images/mini_memo_lock.gif' align=absmiddle>";
                imgType.AlternateText = "개인쪽지";
            }
        }
        else if (type == "4")
        {
            imgType.ImageUrl = "http://img.imbc.com/mini/UserNote/images/mini_memo_icon_phone.gif";
        }
        else
        {
            imgType.ImageUrl = "http://img.imbc.com/mini/UserNote/images/mini_memo_icon_mini.gif";
            imgType.AlternateText = "미니사연";
        }

        btnDelete.CommandArgument = seqID.Text;
        btnDelete.Attributes.Add("onclick", "return confirmDelete();");

        if (uInfo.Uno.ToString() != DataBinder.Eval(e.Item.DataItem, "Uno").ToString() && uInfo.UserID.ToLower() != "imbc" && uInfo.UserID.ToLower() != "imradio")
            btnDelete.Visible = false;

        if (rank != "0")
        {
            seqID.Text = "■";
            userNm.Font.Bold = true;
            comment.Font.Bold = true;
            comment.ForeColor = Color.DarkBlue;

        }

        if (this.IsAdmin)
        {
            userNm.Text = "<a href=\"javascript:fNewWin('" + DataBinder.Eval(e.Item.DataItem, "UserID").ToString() + "', '" + DataBinder.Eval(e.Item.DataItem, "Uno").ToString() + "');\">" + userNm.Text + "</a>";
        }
    }

    protected void listRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        NoteData data = new NoteData();
        data.DeleteMemoInfo(this.progCode_STFM, int.Parse(e.CommandArgument.ToString()));

        Response.Redirect("MiniMsgMor.aspx?progCode=" + this.progCode_STFM, true);
    }

    protected void listRepeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        NoteData data = new NoteData();
        data.DeleteMemoInfo(this.progCode_FM4U, int.Parse(e.CommandArgument.ToString()));

        Response.Redirect("MiniMsgMor.aspx?progCode=" + this.progCode_FM4U, true);
    }

    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        NoteData data = new NoteData();

        string strText = IMBC.FW.Util.WebUtil.PreventHTML(this.txtComment.Text);
        strText = strText.Replace("'", "''");
        strText = strText.Replace(";", "");


        data.RegisterMemoInfo(this.progCode_STFM, 1, this.hdnFontColor.Value, uInfo.Uno, uInfo.UserID, uInfo.UserName, strText, 0);

        Response.Redirect("MiniMsgMor.aspx?progCode=" + this.progCode_STFM, true);
    }

    protected void btnSave1_Click(object sender, System.EventArgs e)
    {
        NoteData data = new NoteData();

        string strText = IMBC.FW.Util.WebUtil.PreventHTML(this.txtComment1.Text);
        strText = strText.Replace("'", "''");
        strText = strText.Replace(";", "");


        data.RegisterMemoInfo(this.progCode_FM4U, 1, this.hdnFontColor.Value, uInfo.Uno, uInfo.UserID, uInfo.UserName, strText, 0);

        Response.Redirect("MiniMsgMor.aspx?progCode=" + this.progCode_STFM, true);
    }
}