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

public partial class Admin_RegisterAdminNoteInfo : System.Web.UI.Page
{
    private string receiver;
    private string progCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.progCode = IMBC.FW.Util.WebUtil.GetRequestQueryString("progCode", "");
        this.receiver = IMBC.FW.Util.WebUtil.GetRequestQueryString("receiver", "");

        ArrayList al = this.getUser();

        for (int i = 0; i < al.Count; i++)
        {
            if (receiverNm.Text != "") receiverNm.Text += ", ";
            receiverNm.Text += ((NoteUser)al[i]).userNm;
        }

        if (!IsPostBack)
        {
            NoteData data = new NoteData();
            RadioProgramInfo prog = data.RetrieveProgramInfo(this.progCode);
            ViewState["subClassCode"] = prog.subClassCode;
            this.progTitle.Text = prog.progTitle;
            this.progImg.ImageUrl = NoteUtil.GetProgImg(this.progCode, prog.progImage);
        }
    }

    private ArrayList getUser()
    {
        string[] arrReceiver = receiver.Split(',');

        ArrayList al = new ArrayList();

        NoteUser user;

        for (int i = 0; i < arrReceiver.Length; i++)
        {
            string[] arrRec = arrReceiver[i].Split('│');

            user = new NoteUser();
            user.uno = int.Parse(arrRec[0]);
            user.userID = arrRec[1];
            user.userNm = arrRec[2];

            al.Add(user);
        }

        return al;
    }

    protected void btnSend_Click(object sender, System.EventArgs e)
    {
        ArrayList al = this.getUser();

        NoteData data = new NoteData();
        data.RegisterAdminNoteInfo(this.progCode, ViewState["subClassCode"].ToString(), comment.Value, this.progImg.ImageUrl, al);

        JS.AlertCommand("쪽지를 보냈습니다.", "window.close();");
    }
}