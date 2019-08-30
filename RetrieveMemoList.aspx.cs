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
using IMBC.FW.DB;
using IMBC.FW.Util;

public partial class RetrieveMemoList : PageBase
{
    protected string progCode;
    private int nCurPage;
    private int nPageRow = 10;
    private int nTotalRecord;
    protected UserInfo uInfo;
    private MemoMstInfo mstInfo;
    protected string topHtml;
    protected string bottomHtml;
    private NoteData data;
    private bool IsAdmin = false;

    private string search;
    private string searchWord;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.progCode = WebUtil.replaceSQLInjections(IMBC.FW.Util.WebUtil.GetRequestQueryString("progCode", ""));
        this.nCurPage = int.Parse(IMBC.FW.Util.WebUtil.GetRequestQueryString("curPage", "1"));
        this.search = IMBC.FW.Util.WebUtil.EncodeHTML(WebUtil.replaceSQLInjections(IMBC.FW.Util.WebUtil.GetRequestQueryString("search", "")));
        this.searchWord = IMBC.FW.Util.WebUtil.RemoveHTMLTag(WebUtil.replaceSQLInjections(IMBC.FW.Util.WebUtil.GetRequestQueryString("searchWord", ""))).Replace("+", "").Replace("%", "").Replace("'", "");

        if (this.progCode.Substring(0, 4) == "RDMB")
        {
            this.progCode = NoteUtil.GetParentCode(this.progCode);
        }


        //			Response.Cache.SetCacheability(HttpCacheability.Public);
        //			Response.Cache.SetExpires(DateTime.Now.AddSeconds(10));
        //			Response.Cache.VaryByParams["progCode"] = true;
        //			Response.Cache.VaryByParams["curPage"] = true;
        //			Response.Cache.VaryByParams["search"] = true;
        //			Response.Cache.VaryByParams["searchWord"] = true;

     

        this.uInfo = new UserInfo();
        this.data = new NoteData();


        if (IMBC.FW.Util.WebUtil.GetSession("IsAdmin_" + progCode) == "TRUE")
        {
            this.IsAdmin = true;
        }
        else if (IMBC.FW.Util.WebUtil.GetSession("IsAdmin_" + progCode) == "FALSE")
        {
            this.IsAdmin = false;
        }
        //else
        //{
        //    this.IsAdmin = data.IsAdminUserDB(this.progCode, uInfo.UserID);
        //}


        if (this.uInfo.IsLogin == false)
        {
            imgSave.Src = "http://img.imbc.com/mini/UserNote/images/mini_memo_login.jpg";

            System.Web.HttpContext httpContext = System.Web.HttpContext.Current;

            httpContext.Response.Cookies["IMBCURL"].Value = httpContext.Server.UrlEncode("http://" + httpContext.Request.ServerVariables["SERVER_NAME"] + httpContext.Request.RawUrl);
            httpContext.Response.Cookies["IMBCURL"].Domain = "imbc.com";
            httpContext.Response.Cookies["IMBCURL"].Path = "/";
            httpContext.Response.Cookies["IMBCURL"].Secure = false;

            imgSave.Attributes.Add("onclick", "location.href='http://member.imbc.com/Login/Login.aspx?TemplateId=Popup'; return false;");
            txtComment.Enabled = false;
        }
        else
        {
            this.IsAdmin = data.IsAdminUserDB(this.progCode, uInfo.UserID);

            imgSave.Attributes.Add("onclick", "doSave();");

            //if (!this.uInfo.IsIdentity())
            //{
            //    if (!this.IsAdmin)
            //        imgSave.Attributes.Add("onclick", "fIdentity();");
            //    else
            //        imgSave.Attributes.Add("onclick", "doSave();");
            //}
            //else
            //{
            //    imgSave.Attributes.Add("onclick", "doSave();");
            //}
        }

        if (this.IsAdmin)
        {
            string script = "<script>function fNewWin(strUsrId, strUserNo)";
            script += "{ window.open('http://member.imbc.com/admin/BbsUserInfo.aspx?uno='+strUserNo+'&uid='+strUsrId, 'usrinfo', 'resizable=no,scrollbars=no,x=100,y=200,width=400,height=120'); }";
            script += "</script>";

            Response.Write(script);
        }

        if (!IsPostBack)
        {
            this.mstInfo = data.RetrieveMemoMstInfo(this.progCode);
            this.progTitle.Text = mstInfo.progTitle;
            this.tblColor.BgColor = mstInfo.tblColor;
            this.tblColor2.BgColor = mstInfo.tblColor;
            this.nPageRow = mstInfo.pageRow;

            if (this.mstInfo.memoType == "2")
            {
                imgSave.Visible = false;
                txtComment.Text = "mini 에서만 작성하실 수 있습니다.";
                txtComment.Enabled = false;
            }

            try
            {
                if (this.progCode == "STFM000001715" || this.progCode == "STFM000001746")
                {
                    pnlList.Visible = false;
                    this.listRepeater.Visible = false;
                    pnlNoMsgList.Visible = true;
                }
                else
                {
                    pnlList.Visible = true;
                    pnlNoMsgList.Visible = false;
                    this.listRepeater.Visible = true;

                    ListDataView ldv = data.RetrieveMemoList(this.progCode, this.nCurPage, this.nPageRow, this.search, this.searchWord);

                    this.nTotalRecord = ldv.TotalCount;
                    this.listRepeater.DataSource = ldv.DV;
                    this.listRepeater.DataBind();

                    string pagingURL = "RetrieveMemoList.aspx?curPage={0}&progCode=" + this.progCode + "&search=" + this.search + "&searchWord=" + this.searchWord;

                    this.pageNavigator.Text = NoteUtil.SetNavigator(this.nCurPage, this.nTotalRecord, this.nPageRow, pagingURL);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
                //					Response.Write("잠시만 기다려 주십시오..");
                //					Response.End();
            }
        }
    }

    protected void listRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label seqID = (Label)e.Item.FindControl("seqID");
        System.Web.UI.WebControls.Image imgType = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgType");
        Label userNm = (Label)e.Item.FindControl("userNm");
        Label comment = (Label)e.Item.FindControl("comment");
        ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
        Label rgDt = (Label)e.Item.FindControl("rgDt");

        string rank = DataBinder.Eval(e.Item.DataItem, "rank").ToString();

        seqID.Text = DataBinder.Eval(e.Item.DataItem, "SeqID").ToString();

        if (this.mstInfo.showType == "1")
            userNm.Text = DataBinder.Eval(e.Item.DataItem, "UserID").ToString();
        else
            userNm.Text = DataBinder.Eval(e.Item.DataItem, "UserNm").ToString();

        comment.Text = Server.HtmlEncode(DataBinder.Eval(e.Item.DataItem, "Comment").ToString()).Replace("<", "&lt;").Replace(">", "&gt;");


        comment.Style.Add("color", DataBinder.Eval(e.Item.DataItem, "FontColor").ToString());
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
        data.DeleteMemoInfo(this.progCode, int.Parse(e.CommandArgument.ToString()));

        Response.Redirect("RetrieveMemoList.aspx?progCode=" + this.progCode, true);
    }

    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        NoteData data = new NoteData();

        if (!this.uInfo.IsIdentity())
        {
            //본인 확인제 관련 추가 함
            uInfo.RedirectIdentityPopupPage();
        }

        string strText = IMBC.FW.Util.WebUtil.PreventHTML(this.txtComment.Text);
        strText = strText.Replace("'", "''");
        strText = strText.Replace(";", "");


        data.RegisterMemoInfo(this.progCode, 1, this.hdnFontColor.Value, uInfo.Uno, uInfo.UserID, uInfo.UserName, strText, 0);

        Response.Redirect("RetrieveMemoList.aspx?progCode=" + this.progCode, true);
    }	
}