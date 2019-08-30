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

public partial class Admin_RetrieveMemoAdminList : System.Web.UI.Page
{
    protected string progCode;
    protected int nCurPage;
    private int nPageRow = 100;
    protected int nTotalRecord;

    protected int memoType;
    protected string search;
    protected string searchWord;
    protected string stDt;
    protected string spDt;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.progCode = IMBC.FW.Util.WebUtil.GetRequestQueryString("progCode", "RAMFM300");
        this.nCurPage = int.Parse(IMBC.FW.Util.WebUtil.GetRequestQueryString("curPage", "1"));
        this.memoType = int.Parse(IMBC.FW.Util.WebUtil.GetRequestQueryString("memoType", "0"));
        this.search = IMBC.FW.Util.WebUtil.GetRequestQueryString("search", "");
        this.searchWord = IMBC.FW.Util.WebUtil.GetRequestQueryString("searchWord", "");
        this.stDt = IMBC.FW.Util.WebUtil.GetRequestQueryString("stDt", "");
        this.spDt = IMBC.FW.Util.WebUtil.GetRequestQueryString("spDt", "");
        this.newCnt.Text = IMBC.FW.Util.WebUtil.GetRequestQueryString("newCnt", "");

        if (this.progCode.Substring(0, 4) == "RDMB")
        {
            this.progCode = NoteUtil.GetParentCode(this.progCode);
        }

        if (!IsPostBack)
        {
            NoteData data = new NoteData();

            UserInfo uInfo = new UserInfo();
            if (data.IsAdminUserDB(this.progCode, uInfo.UserID) == false)
            {
                Response.Write("관리자가 아닙니다.");
                Response.End();
            }

            this.progTitle.Text = data.RetrieveProgramInfo(this.progCode).progTitle;

            if (newCnt.Text == "")
                this.newCnt.Text = data.CountMemoNewInfo(this.progCode).ToString();

            ListDataView ldv = data.RetrieveMemoAdminList(this.progCode, this.memoType, search, searchWord, stDt, spDt, this.nCurPage, this.nPageRow);

            this.nTotalRecord = ldv.TotalCount;

            this.listRepeater.DataSource = ldv.DV;
            this.listRepeater.DataBind();

            string pagingURL = "RetrieveMemoAdminList.aspx?curPage={0}&progCode=" + this.progCode + "&memoType=" + memoType + "&seach=" + search + "&searchWord=" + searchWord + "&stDt=" + stDt + "&spDt=" + spDt + "&newCnt=" + this.newCnt.Text;

            this.pageNavigator.Text = NoteUtil.SetNavigator(this.nCurPage, this.nTotalRecord, this.nPageRow, pagingURL);
        }
    }

    protected void listRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        HtmlInputCheckBox chkbox = (HtmlInputCheckBox)e.Item.FindControl("chkbox");
        HtmlInputHidden hdnSeqID = (HtmlInputHidden)e.Item.FindControl("hdnSeqID");

        Label seqID = (Label)e.Item.FindControl("seqID");
        System.Web.UI.WebControls.Image imgType = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgType");
        Label userNm = (Label)e.Item.FindControl("userNm");
        Label comment = (Label)e.Item.FindControl("comment");
        Label rgDt = (Label)e.Item.FindControl("rgDt");


        string rank = DataBinder.Eval(e.Item.DataItem, "rank").ToString();

        seqID.Text = DataBinder.Eval(e.Item.DataItem, "SeqID").ToString();
        userNm.Text = DataBinder.Eval(e.Item.DataItem, "UserNm").ToString();
        userNm.Text = "<a href=\"javascript:fNewWin('" + DataBinder.Eval(e.Item.DataItem, "Uno").ToString() + "', '"+DataBinder.Eval(e.Item.DataItem, "UserID").ToString()+"');\">" + userNm.Text + "</a>";

        comment.Text = NoteUtil.BoldSearchWord(DataBinder.Eval(e.Item.DataItem, "Comment").ToString(), this.searchWord);
        comment.Style.Add("color", DataBinder.Eval(e.Item.DataItem, "FontColor").ToString());
        rgDt.Text = ((DateTime)DataBinder.Eval(e.Item.DataItem, "rgDt")).ToString("yyyy-MM-dd HH:mm:ss");

        string type = DataBinder.Eval(e.Item.DataItem, "MemoType").ToString();

        if (type == "1")
            imgType.ImageUrl = "http://img.imbc.com/mini/UserNote/images/mini_memo_icon_web.gif";
        else if (type == "2")
            imgType.ImageUrl = "http://img.imbc.com/mini/UserNote/images/mini_memo_icon_memo.gif";
        else if (type == "4")
            imgType.ImageUrl = "http://img.imbc.com/mini/UserNote/images/mini_memo_icon_phone.gif";
        else
            imgType.ImageUrl = "http://img.imbc.com/mini/UserNote/images/mini_memo_icon_mini.gif";

        int uno = (int)DataBinder.Eval(e.Item.DataItem, "Uno");
        string userID = DataBinder.Eval(e.Item.DataItem, "UserID").ToString();

        chkbox.Value = uno.ToString() + "│" + userID + "│" + DataBinder.Eval(e.Item.DataItem, "UserNm").ToString();
        hdnSeqID.Value = DataBinder.Eval(e.Item.DataItem, "SeqID").ToString();

        if (rank != "0")
        {
            seqID.Text = "■";
            userNm.Font.Bold = true;
            comment.Font.Bold = true;
            comment.ForeColor = Color.DarkBlue;
        }
    }
}