using System;
using System.Configuration;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Data;

/// <summary>
/// PageBase의 요약 설명입니다.
/// </summary>
public class PageBase : System.Web.UI.Page
{
    protected UserSettings userSettings;

	public PageBase()
	{
		//
		// TODO: 여기에 생성자 논리를 추가합니다.
		//
	}
    override protected void OnInit(EventArgs e)
    {
        base.OnInit(e);

        userSettings = Context.Items["UserSettings"] as UserSettings;
        this.Error += new EventHandler(PageBase_Error);
    }
    /// <summary>
    /// Config 파일에 등록된 Admin ID에 존재하는 관리자 아이디인지를 확인한다.
    /// </summary>
    /// <param name="strUserID"></param>
    /// <returns></returns>
    public bool IsIMBCAdmin(string strUserID)
    {
        bool bReturn = false;
        string[] strAdminUser = System.Web.Configuration.WebConfigurationManager.AppSettings["AdminUsers"].ToString().Split(';');


        for (int i = 0; i < strAdminUser.Length; i++)
        {
            if (strUserID == strAdminUser[i].ToString().Trim())
            {
                bReturn = true;
            }
        }

        return bReturn;
    }

    private void PageBase_Error(object sender, EventArgs e)
    {
        Exception currentErr = Server.GetLastError();

        if (!(currentErr is System.ApplicationException))
        {   
            string strMsg = "에러발생시각: " + DateTime.Now.ToString();
            System.ApplicationException appException = new System.ApplicationException(strMsg, currentErr);
        }
    }
}

/// <summary>
/// User 정보를 간단하게 셋팅하는 클래스
/// </summary>
public class UserSettings : PageBase
{
    public string AdminUserID;
    public string Administrator;
    public string ProgCode;

    public UserSettings(string UserID)
    {
        try
        {
            if (IsIMBCAdmin(UserID)) //iMBC 관리자인지 확인
            {
                AdminUserID = UserID;
                Administrator = "IMBC";
            }
            else
            {
                if (IsAuthAdmin(UserID)) //MBC 관리자인지 확인
                {
                    UserID = UserID;
                    Administrator = "MBC";
                }
                else
                {
                    if (UserID == "kimkw10" || UserID == "imbctc025") //설문 결과툴을 위해 조회 할 수 있는 아이디 제작국 추가
                    {
                        UserID = UserID;
                        Administrator = "MBC";
                    }
                    else
                    {
                        Administrator = "";
                    }
                }

            }
        }
        catch
        {
            UserID = "";
            Administrator = "";
            ProgCode = "";
        }
    }

    /// <summary>
    /// iMBC 관리자는 아니지만 관리자 권한이 있는지 확인한다.
    /// </summary>
    /// <param name="UserID"></param>
    /// <returns></returns>
    public bool IsAuthAdmin(string UserID)
    {
        bool bReturn = false;
        DBCommand dbQuery = new DBCommand();
        DataSet ds = dbQuery.GetRadioProgramUser(UserID);

        if (ds.Tables[0].Rows.Count > 0)
            bReturn = true;
        else
            bReturn = false;

        return bReturn;
    }
}