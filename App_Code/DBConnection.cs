// ***************************************************************
using System;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
/// DBConnection의 요약 설명입니다.
/// </summary>
public class DBConnection
{
    public SqlConnection Conn;

    public DBConnection()
    {
        //
        // TODO: 여기에 생성자 논리를 추가합니다.
        //
    }

    public bool DRMDBOpen()
    {
        try
        {
            Conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.AppSettings["drmDB01Con"]);
            Conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void DRMDBClose()
    {
        Conn.Close();
    }
    /// <summary>
    /// RadioDB 관련 연결을 엽니다.
    /// </summary>
    /// <returns></returns>
    public bool RadioDBOpen()
    {

        try
        {
            Conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.AppSettings["RadioCon"]);
            Conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// RadioDB 연결을 닫습니다.
    /// </summary>
    public void RadioDBClose()
    {
        Conn.Close();
    }

    /// <summary>
    /// TouchDB 연결을 엽니다.
    /// </summary>
    /// <returns></returns>
    public bool TouchDBOpen()
    {

        try
        {
            Conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.AppSettings["TouchCon"]);
            Conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Touch Db 연결을 닫습니다.
    /// </summary>
    public void TouchDBClose()
    {
        Conn.Close();
    }

    /// <summary>
    /// Content DB연결을 엽니다.
    /// </summary>
    /// <returns></returns>
    public bool ContentDBOpen()
    {
        try
        {
            Conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.AppSettings["ContentCon"]);
            Conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Content DB 연결을 닫습니다.
    /// </summary>
    public void ContentDBClose()
    {
        Conn.Close();
    }

    /// <summary>
    /// radioAdmin DB 연결을 엽니다.
    /// </summary>
    /// <returns></returns>
    public bool RadioAdminDBOpen()
    {
        try
        {
            //	Conn = new SqlConnection(ConfigurationSettings.AppSettings["RadioAdmin"]);
            Conn = new SqlConnection("server=121.254.134.11;database=RadioAdmin;uid=dbowr_music;pwd=roqkfxla;Connect Timeout=120");
            Conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            RadioAdminDBClose();
        }
    }

    /// <summary>
    /// radioAdmin DB 연결을 닫습니다.
    /// </summary>
    public void RadioAdminDBClose()
    {
        Conn.Close();
    }

    public bool RadioEventDBOpen()
    {
        try
        {
            //	Conn = new SqlConnection(ConfigurationSettings.AppSettings["RadioAdmin"]);
            Conn = new SqlConnection("server=121.254.134.85;database=dbRadioEvent;uid=radioUser;pwd=dpvmdpavhdb;Connect Timeout=120");
            Conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            RadioEventDBClose();
        }
    }

    public void RadioEventDBClose()
    {
        Conn.Close();
    }

    public bool RadioAdmin11DBOpen()
    {
        try
        {
            Conn = new SqlConnection("server=121.254.134.11;database=RadioAdmin;uid=dbowr_music;pwd=roqkfxla;Connect Timeout=120");
            Conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            RadioAdmin11DBClose();
        }
    }

    /// <summary>
    /// radioAdmin DB 연결을 닫습니다.
    /// </summary>
    public void RadioAdmin11DBClose()
    {
        Conn.Close();
    }


    /// <summary>
    /// 메일 서비스 DB를 엽니다.
    /// </summary>
    /// <returns></returns>
    public bool MailServiceDBOpen()
    {
        try
        {
            Conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.AppSettings["MailService"]);
            Conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            MailServiceDBClose();
        }
    }

    public void MailServiceDBClose()
    {
        Conn.Close();
    }

    /// <summary>
    /// Music DB 연결을 엽니다.
    /// </summary>
    /// <returns></returns>
    public bool MusicDBOpen()
    {
        try
        {
            Conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.AppSettings["MusicCon"]);
            Conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Music DB 연결을 닫습니다.
    /// </summary>
    public void MusicDBClose()
    {
        Conn.Close();
    }

    /// <summary>
    /// ApplicationLog DB연결을 엽니다.
    /// </summary>
    /// <returns></returns>
    public bool ApplicationDBOpen()
    {
        try
        {
            Conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.AppSettings["ApplicationLog"]);
            Conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// ApplicationLog DB 연결을 닫습니다.
    /// </summary>
    /// <returns></returns>
    public void ApplicationDBClose()
    {
        Conn.Close();
    }

    /// <summary>
    /// ContentsMaster DB 연결을 엽니다.
    /// </summary>
    /// <returns></returns>
    public bool ContentsMasterDBOpen()
    {
        try
        {
            Conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.AppSettings["ContentMaster"]);
            Conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// ContentsDB 연결을 닫습니다.
    /// </summary>
    public void ContentsMasterDBClose()
    {
        Conn.Close();
    }

    public bool LocalDBOpen()
    {
        try
        {
            Conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.AppSettings["LocalDB"]);
            Conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public void LocalDBClose()
    {
        Conn.Close();
    }


}