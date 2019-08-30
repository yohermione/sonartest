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

public partial class RegisterUserReplyNoteInfo : System.Web.UI.Page
{
    private string progCode;
    private int noteID;
    private UserInfo uInfo;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.progCode = WebUtil.replaceSQLInjections(IMBC.FW.Util.WebUtil.GetRequestQueryString("progCode", ""));
        this.noteID = int.Parse(IMBC.FW.Util.WebUtil.GetRequestQueryString("noteID", "0"));

        this.uInfo = new UserInfo();

        if (!IsPostBack)
        {
            userNm.Text = uInfo.UserName;

            NoteData data = new NoteData();
            receiverNm.Text = data.RetrieveProgramInfo(this.progCode).progTitle;
        }
    }

    protected void btnSend_Click(object sender, System.EventArgs e)
    {
        NoteUser user = new NoteUser();
        user.uno = uInfo.Uno;
        user.userID = uInfo.UserID;
        user.userNm = uInfo.UserName;

        NoteData data = new NoteData();
        data.RegisterUserReplyNoteInfo(this.noteID, this.progCode, user, comment.Text);

        JS.AlertCommand("쪽지를 보냈습니다.", "opener.location.reload(); window.close();");
    }
}