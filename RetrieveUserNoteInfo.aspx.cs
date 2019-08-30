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

public partial class RetrieveUserNoteInfo : System.Web.UI.Page
{
    private int noteID;
    private NoteInfo note;
    private UserInfo uInfo;

    protected void Page_Load(object sender, EventArgs e)
    {

        this.noteID = int.Parse(IMBC.FW.Util.WebUtil.GetRequestQueryString("noteID", "0"));

        this.uInfo = new UserInfo();

        NoteData data = new NoteData();
        this.note = data.RetrieveUserNoteInfo(noteID);

        userNm.Text = uInfo.UserName;
        progTitle.Text = note.progTitle;
        progImg.ImageUrl = note.progImage;
        comment.Text = note.comment;
    }

    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        NoteUser user = new NoteUser();

        user.uno = uInfo.Uno;
        user.userID = uInfo.UserID;
        user.userNm = uInfo.UserName;

        NoteData data = new NoteData();
        data.RegisterUserReplyNoteInfo(this.noteID, note.progCode, user, txtComment.Text);

        JS.AlertCommand("쪽지주셔서 감사합니다.", "document.location.href='EVENT:CLOSE';");
    }
}