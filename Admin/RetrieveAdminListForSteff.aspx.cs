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

public partial class Admin_RetrieveAdminListForSteff : System.Web.UI.Page
{
    protected string progCode;
    private UserInfo uInfo;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.progCode = IMBC.FW.Util.WebUtil.GetRequestQueryString("progCode", "");
        this.uInfo = new UserInfo();

        if (this.uInfo.IsLogin == false)
        {
            this.uInfo.RedirectLoginPage();
            Response.End();
        }

        if (!IsPostBack)
        {
            NoteData data = new NoteData();

            if (data.IsAdminUserDB(this.progCode, uInfo.UserID) == false)
            {
                Response.Write("관리자가 아닙니다.");
                Response.End();
            }

            listRepeater.DataSource = data.RetrieveAdminBadUserList(this.progCode);
            listRepeater.DataBind();
        }
    }

    protected void listRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label num = (Label)e.Item.FindControl("num");
        Label lblUserID = (Label)e.Item.FindControl("lblUserID");
        Label lblUsrType = (Label)e.Item.FindControl("lblUsrType");
        Label lblRgDt = (Label)e.Item.FindControl("lblRgDt");
        Button btnDelete = (Button)e.Item.FindControl("btnDelete");

        num.Text = (e.Item.ItemIndex + 1).ToString();
        string comment = DataBinder.Eval(e.Item.DataItem, "comment").ToString();

        lblUserID.Text = "<a style='cursor:pointer' title='" + comment + "'>" + DataBinder.Eval(e.Item.DataItem, "usr_id").ToString() + "</a>";
        lblUsrType.Text = (DataBinder.Eval(e.Item.DataItem, "usr_Type").ToString() == "A") ? "관리자" : "불량이용자";
        lblRgDt.Text = ((DateTime)DataBinder.Eval(e.Item.DataItem, "reg_date")).ToString();
        btnDelete.CommandArgument = lblUserID.Text;
    }

    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        NoteData data = new NoteData();
        data.RegisterAdminBadUserInfo(this.progCode, this.userID.Text, this.usrType.SelectedValue, this.comment.Text);

        Response.Redirect(Request.RawUrl, true);
    }

    protected void listRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        NoteData data = new NoteData();
        data.DeleteAdminBadUserInfo(this.progCode, e.CommandArgument.ToString());

        Response.Redirect(Request.RawUrl, true);
    }
}