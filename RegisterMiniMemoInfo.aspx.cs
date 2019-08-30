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
using System.Text.RegularExpressions;
using IMBC.FW.Util;

public partial class RegisterMiniMemoInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo uInfo = new UserInfo();
        string progCode = IMBC.FW.Util.WebUtil.GetRequestForm("sPrgID", "");

        int uno = uInfo.Uno;
        string userID = uInfo.UserID;
        string userNm = uInfo.UserName;
        string comment = IMBC.FW.Util.WebUtil.GetRequestForm("TEXT", "");

        comment = IMBC.FW.Util.WebUtil.PreventHTML(comment);


        if (progCode == "" || uno == 0 || userID == "" || userNm == "" || comment == "")
        {
            Response.End();
        }
        else
        {
            if (System.Web.Configuration.WebConfigurationManager.AppSettings["pmstart"].ToString() == "ON")
            {
                Response.Write("OK$");
                Response.Write("RETURN|0$");
                Response.Write("MSG|'죄송합니다. 지금은 시스템 점검중입니다.$");
                //				Response.Status = "200 OK";
                Response.End();
            }

            NoteData data = new NoteData();
            progCode = NoteUtil.GetParentCode(progCode);

          
            bool bXCharCheck = false;
          

            if (bXCharCheck)
            {
                Response.Write("OK$");
                Response.Write("RETURN|0$");
                Response.Write("MSG|'비속어를 입력할 수 없습니다.$");
                //				Response.Status = "200 OK";
                Response.End();
            }
            else
            {
                if (CheckNum(comment))
                {
                    Response.Write("OK$");
                    Response.Write("RETURN|0$");
                    Response.Write("MSG|'입력하신 숫자는 개인정보에 해당 하는 숫자이므로 입력이 불가능합니다.$");
                    //				Response.Status = "200 OK";
                    Response.End();
                }
                else
                {



                    if (data.RegisterMemoInfo(progCode, 3, "#000000", uno, userID, userNm, comment, 0))
                    {
                        //					Response.Write("OK\r\n");
                        //					Response.Write("RETURN VALUE : 1/전송이 완료되었습니다.");
                        //					//				Response.Status = "200 OK";
                        //					Response.End();
                        //
                        if (!uInfo.IsIdentity())
                        {
                            Response.Write("OK$");
                            Response.Write("RETURN|1$");
                            Response.Write("MSG|전송이 완료되었습니다.         '제한적 본인확인제' 시행으로 본인확인이 필요합니다.$");
                            Response.Write("POPUP_ONCE|http://login.imbc.com/iMBC/CleanCampaign/IdentiFormPop.asp$");
                            Response.End();
                        }
                        else
                        {
                            Response.Write("OK$");
                            Response.Write("RETURN|1$");
                            Response.Write("MSG|전송이 완료되었습니다.$");
                            Response.End();
                        }
                    }
                    else
                    {
                        Response.Write("OK$");
                        Response.Write("RETURN|0$");
                        Response.Write("MSG|'전송이 실패하였습니다.$");
                        //				Response.Status = "200 OK";
                        Response.End();
                    }

                }
            }
        }
    }

    public bool CheckNum(string strMessage)
    {
        bool bReturn = false;
        string strReturn = Regex.Replace(strMessage, @"\D", "");
        if (strReturn.IndexOf("010") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("011") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("016") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("017") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("018") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("019") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("02") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("031") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("032") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("033") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("041") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("043") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("051") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("052") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("053") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("054") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("055") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("061") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("062") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("063") >= 0)
        {
            bReturn = true;
        }
        else if (strReturn.IndexOf("064") >= 0)
        {
            bReturn = true;
        }
        return bReturn;
    }
}