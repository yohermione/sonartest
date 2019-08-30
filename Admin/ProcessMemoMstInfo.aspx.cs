using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMBC.FW.Util;

public partial class Admin_ProcessMemoMstInfo : System.Web.UI.Page
{
    protected string progCode = string.Empty;
    protected string deletedSeq = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        progCode = IMBC.FW.Util.WebUtil.GetRequestQueryString("progCode", "");
        deletedSeq = IMBC.FW.Util.WebUtil.GetRequestQueryString("seq", "");

        if (progCode != "" && deletedSeq != "")
        {
            NoteData data = new NoteData();
            UserInfo uInfo = new UserInfo();
            if (data.IsAdminUserDB(this.progCode, uInfo.UserID) == false)
            {
                Response.Clear();
                Response.Write("-9");
                Response.End();
            }

            int iResult = this.getSeq(); // 글 삭제 숫자를 담는다.

            Response.Clear();
            Response.Write(iResult.ToString());
            Response.End();
        }
    }

    private int getSeq()
    {
        int iReturn = 0;
        string[] arrReceiver = deletedSeq.Split(',');

        NoteData data = new NoteData();

        for (int i = 0; i < arrReceiver.Length; i++)
        {
            if (data.DeleteMemoInfo(progCode, Convert.ToInt32(arrReceiver[i].ToString())))
            {
                iReturn++;
            }
        }
        return iReturn;
    }
}