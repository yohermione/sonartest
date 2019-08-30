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
public partial class Admin_RetrieveMemoMstInfo : System.Web.UI.Page
{
    protected string progCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.progCode = IMBC.FW.Util.WebUtil.GetRequestQueryString("progCode", "");

        if (!IsPostBack)
        {
            NoteData data = new NoteData();

            if (data.IsAdminUser(this.progCode, new UserInfo().UserID) == false)
            {
                Response.Write("관리자가 아닙니다.");
                Response.End();
            }

            MemoMstInfo mstInfo = data.RetrieveMemoMstInfo(this.progCode);

            memoUrl.Text = "http://mini.imbc.com/UserNote/RetrieveMemoList.aspx?progCode=" + this.progCode;

            if (mstInfo.memoType == "1")
                memoType1.Checked = true;
            else
                memoType2.Checked = true;

            pageRow.Text = mstInfo.pageRow.ToString();
            tblColor.Style.Add("background-color", mstInfo.tblColor);
            hdnFontColor.Value = mstInfo.tblColor;

            if (mstInfo.showType == "1")
                showType1.Checked = true;
            else
                showType2.Checked = true;

            txtMiniBoardURL.Text = mstInfo.topHtml;
        }

    }

    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        MemoMstInfo mstInfo = new MemoMstInfo();
        mstInfo.progCode = this.progCode;
        mstInfo.memoType = (memoType1.Checked) ? "1" : "2";
        mstInfo.pageRow = int.Parse(pageRow.Text);
        mstInfo.tblColor = hdnFontColor.Value;
        mstInfo.showType = (showType1.Checked) ? "1" : "2";
        mstInfo.topHtml = txtMiniBoardURL.Text;

        NoteData data = new NoteData();
        data.UpdateMemoMstInfo(mstInfo);

        Response.Redirect(Request.RawUrl, true);
    }
}