using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// DBCommand의 요약 설명입니다.
/// </summary>
public class DBCommand : DBConnection
{
    public DBCommand()
    {
        //
        // TODO: 여기에 생성자 논리를 추가합니다.
        //
    }

    #region 라디오통합Admin에서 이용하는 DB명령문
    /// <summary>
    /// 라디오DB에 등록된 관리자 인지를 확인하고 정보를 반환합니다.
    /// </summary>
    /// <param name="UserID"></param>
    /// <returns></returns>
    public DataSet GetRadioProgramUser(string UserID)
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "GetRadioAdminData",
                    new SqlParameter("@UserID", UserID));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 미니 보라 p2p 설정 정보를 반환한다.
    /// </summary>
    /// <returns></returns>
    public string GetMiniBoraP2PInfo()
    {
        string strBora = string.Empty;

        if (RadioAdmin11DBOpen())
        {
            try
            {
                strBora = (string)SqlHelper.ExecuteScalar(Conn, CommandType.StoredProcedure,
                    "USP_TBL_MINIBORA_P2P_SELECT");
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdmin11DBClose();
            }
        }
        return strBora;
    }

    /// <summary>
    /// 보이는 라디오 P2P 설정을 저장한다.
    /// </summary>
    /// <param name="BoraP2P"></param>
    /// <param name="UserID"></param>
    /// <returns></returns>
    public int SetMiniBoraP2PInfo(string BoraP2P, string UserID)
    {
        int iReturn = 0;
        if (RadioAdmin11DBOpen())
        {
            try
            {
                iReturn = SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "USP_TBL_MINIBORA_P2P_INSERT",
                    new SqlParameter("@p2p", BoraP2P),
                    new SqlParameter("@updater", UserID));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdmin11DBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 보라 스케쥴 정보를 입력한다.
    /// </summary>
    /// <param name="progCode"></param>
    /// <param name="progTitle"></param>
    /// <param name="running_starttime"></param>
    /// <param name="running_endtime"></param>
    /// <param name="updateID"></param>
    /// <param name="strMon"></param>
    /// <param name="strTue"></param>
    /// <param name="strWed"></param>
    /// <param name="strThu"></param>
    /// <param name="strFri"></param>
    /// <param name="strSat"></param>
    /// <param name="strSun"></param>
    /// <param name="strRepeat"></param>
    /// <returns></returns>
    public int SetMiniBoraSchedule(string progCode, string progTitle, string running_starttime,
        string running_endtime, string updateID, string strMon,
        string strTue, string strWed, string strThu, string strFri,
        string strSat, string strSun, string strRepeat)
    {
        int iReturn = 0;
        if (RadioAdmin11DBOpen())
        {
            try
            {
                iReturn = SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "USP_TBL_MINIBORA_SCHEDULE_INSERT",
                    new SqlParameter("@progCode", progCode),
                    new SqlParameter("@progTitle", progTitle),
                    new SqlParameter("@startTime", running_starttime),
                    new SqlParameter("@endTime", running_endtime),
                    new SqlParameter("@updater", updateID),
                    new SqlParameter("@mon", strMon),
                    new SqlParameter("@tue", strTue),
                    new SqlParameter("@wed", strWed),
                    new SqlParameter("@thu", strThu),
                    new SqlParameter("@fri", strFri),
                    new SqlParameter("@sat", strSat),
                    new SqlParameter("@sun", strSun),
                    new SqlParameter("@repeat", strRepeat));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdmin11DBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 보이는 라디오 스케쥴 정보를 반환합니다.
    /// </summary>
    /// <param name="progCode"></param>
    /// <returns></returns>
    public DataSet GetBoraScheduleData(string progCode)
    {
        DataSet ds = new DataSet();
        if (RadioAdmin11DBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_TBL_MINIBORA_SCHEDULE_SELECT",
                    new SqlParameter("@progCode", progCode));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdmin11DBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 라디오 채널 리스트를 반환합니다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetRadioChannelList()
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_TBL_SELECTION_MST_SUBCLASS_SELECT");
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 라디오 방송 프로그램 리스트를 반환합니다.
    /// </summary>
    /// <param name="SubClass"></param>
    /// <returns></returns>
    public DataSet GetRadioProgramList(string SubClass)
    {
        DataSet ds = new DataSet();
        if (RadioAdmin11DBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_TBL_RADIO_PROGRAM_LIST",
                    new SqlParameter("@subclass", SubClass));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdmin11DBClose();
            }
        }
        return ds;
    }
    /// <summary>
    /// 미니 방문자 & 다운로드 수 통계 결과 리스트를 반환합니다.
    /// </summary>
    /// <param name="Gubun">쿼리 조건 구분자 1: 월별 통계 2: 일별 통계 3:그래프 용(총합이 빠진 것)</param>
    /// <returns></returns>
    public DataSet GetMINIDownVisitStat(int Gubun, string SearchText)
    {
        DataSet ds = new DataSet();
        if (RadioAdmin11DBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "dbowr_music.sp_GetMiniLogDV",
                    new SqlParameter("@Flag", Gubun),
                    new SqlParameter("@SearchText", SearchText));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdmin11DBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 미니 방문자 & 다운로드 수 통계 결과 리스트를 반환합니다.
    /// </summary>
    /// <param name="Gubun">쿼리 조건 구분자 1: 월별 통계 2: 일별 통계 3:그래프 용(총합이 빠진 것)</param>
    /// <returns></returns>
    public DataSet GetMINIDownVisitStat_BETA(int Gubun, string SearchText)
    {
        DataSet ds = new DataSet();
        if (RadioAdmin11DBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "dbowr_music.sp_GetMiniLogDV_BETA",
                    new SqlParameter("@Flag", Gubun),
                    new SqlParameter("@SearchText", SearchText));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdmin11DBClose();
            }
        }
        return ds;
    }



    /// <summary>
    /// 로그 쌓는 프로그램에서 이용하는 DB 쿼리
    /// </summary>
    /// <returns></returns>
    public bool SetNationCode(int Usr_No, string Peop_no, string gender, string job_code, string NationCode, string Remote_Addr, string Reg_Date, string Reg_Time)
    {
        bool bReturn = false;
        if (RadioAdminDBOpen())
        {
            try
            {
                if (SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "sq_SetMiniLogNationCode",
                    new SqlParameter("@Usr_no", Usr_No),
                    new SqlParameter("@Peop_no", Peop_no),
                    new SqlParameter("@gender", gender),
                    new SqlParameter("@job_code", job_code),
                    new SqlParameter("@NationCode", NationCode),
                    new SqlParameter("@Remote_Addr", Remote_Addr),
                    new SqlParameter("@Reg_Date", Reg_Date),
                    new SqlParameter("@Reg_Time", Reg_Time)) > 0)

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
        return bReturn;
    }

    /// <summary>
    /// 미니 사용자들의 국가 코드를 변환하기 위해 IP 정보와 사용자 정보를 가져온다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetMiniUserNationCode()
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "sp_GetMiniLogUserInfo");

            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 사용자의 성별에 따른 사용자 수를 반환합니다.
    /// </summary>
    /// <param name="SexType"></param>
    /// <returns></returns>
    public string GetUserSexCnt(string SexType)
    {
        string GenderCnt = "0";

        if (RadioAdminDBOpen())
        {
            try
            {
                GenderCnt = SqlHelper.ExecuteScalar(Conn, CommandType.StoredProcedure,
                    "sp_GetMiniUserGenderCnt",
                    new SqlParameter("@gender", SexType)).ToString();

            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return GenderCnt;
    }
    /// <summary>
    /// 연령대별 통계 리스트를 반환합니다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetMiniAgeList()
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "sp_GetMiniLogAge");

            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            };
        }
        return ds;
    }

    /// <summary>
    /// 지역별 통계 리스트를 반환합니다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetMiniAreaList()
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "sp_GetMiniLogArea");
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 직업별 통계 리스트를 반환합니다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetMiniJobList()
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "sp_GetMiniLogJob");
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 국가별 통계 리스트를 반환합니다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetMiniNationCodeList(string SearchParam)
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                if (SearchParam == "전체")
                    SearchParam = "";

                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "sp_GetMiniLogNationCode",
                    new SqlParameter("@SearchText", SearchParam));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 국가별 사용자 수 통계를 반환합니다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetMiniNationCount()
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "sp_GetMiniLogNationCount");
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 라디오 동시 접속자 통계 결과를 반환합니다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetRadioConCurrentUserCount(int Gubun, string SearchText)
    {
        DataSet ds = new DataSet();

        if (ApplicationDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "sp_Get_WmsLogRadioData",
                    new SqlParameter("@Flag", Gubun),
                    new SqlParameter("@SearchText", SearchText));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                ApplicationDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 미니 1주년 이벤트 결과 통계를 반환한다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetMiniEventResult()
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "sp_GetEventResult");
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 미니 500만 다운로드 이벤트 결과 통계를 반환한다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetMini500EventResult()
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_miniMBC500DownEvent_ResultList");
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 최초 로그인 사용자 리스트를 반환한다.
    /// </summary>
    /// <param name="SearchDate"></param>
    /// <returns></returns>
    public DataSet GetMiniFirstLogin(string SearchDate)
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "sp_GetFirstLoginInfo",
                    new SqlParameter("@today", SearchDate));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 예약 정보를 취소한다.
    /// </summary>
    /// <param name="iSeq"></param>
    /// <returns></returns>
    public bool DeleteReservInfo(int iSeq)
    {
        bool bReturn = false;
        if (RadioAdminDBOpen())
        {
            try
            {
                int iReturn = SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "sp_DeleteReservInfo",
                    new SqlParameter("@SEQNO", iSeq));
                if (iReturn > 0)
                {
                    bReturn = true;
                }
            }
            catch
            {
                bReturn = false;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return bReturn;
    }

    /// <summary>
    /// 광고 예약 리스트를 반환한다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetReservAdvertList()
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "sp_GetReservAdvertList");
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 예약 정보 DB 등록 
    /// </summary>
    /// <param name="AdvertUrl"></param>
    /// <param name="StartTime"></param>
    /// <param name="RegUser"></param>
    /// <param name="ReservationYN"></param>
    /// <param name="OpenDate"></param>
    /// <param name="Channel"></param>
    /// <param name="UserIp"></param>
    /// <param name="Comment"></param>
    /// <returns></returns>
    public int SetReservationAdvertInfo(string AdvertUrl, string StartTime, string RegUser,
        string ReservationYN, string OpenDate, string Channel, string UserIp, string Comment, string Openflag)
    {
        int iResult = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iResult = SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "sp_SetReservAdvertInfo",
                    new SqlParameter("@ADVERT_URL", AdvertUrl),
                    new SqlParameter("@STARTTIME", StartTime),
                    new SqlParameter("@REGUSER", RegUser),
                    new SqlParameter("@RESERVATION_YN", ReservationYN),
                    new SqlParameter("@OPENDATE", OpenDate),
                    new SqlParameter("@CHANNEL", Channel),
                    new SqlParameter("@REGUSERIP", UserIp),
                    new SqlParameter("@COMMENT", Comment),
                    new SqlParameter("@OPENFLAG", Openflag));
            }
            catch (System.Exception e)
            {
                iResult = -1;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iResult;
    }

    /// <summary>
    /// 광고주 정보 리스트를 가져온다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetSponserInfo(string SponserType)
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "sp_GetSponserList",
                    new SqlParameter("@sponserType", SponserType));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 광고주 정보를 넣는다.
    /// </summary>
    /// <param name="SponserID"></param>
    /// <param name="AdvertURL"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    public bool SetSponserInfo(string SponserID, string AdvertURL, string startTime, string endTime, string regUserID)
    {
        bool bReturn = false;
        if (RadioAdminDBOpen())
        {
            try
            {
                if (SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "sp_SetSponserInfo",
                    new SqlParameter("@sponserID", SponserID),
                    new SqlParameter("@advertURL", AdvertURL),
                    new SqlParameter("@startTime", startTime),
                    new SqlParameter("@endTime", endTime),
                    new SqlParameter("@regUser", regUserID)) > 0)
                {
                    bReturn = true;
                }
                else
                {
                    bReturn = false;
                }
            }
            catch (System.Exception e)
            {
                return false;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return bReturn;
    }

    /// <summary>
    /// 미니 보이는 라디오 시간표 리스트를 가져온다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetMiniBoraScheduleList()
    {
        DataSet resultSet = new DataSet();
        if (RadioAdmin11DBOpen())
        {
            try
            {
                resultSet = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_MINIBORA_SCHEDULE_SELECT");


            }
            catch (System.Exception e)
            {

            }
            finally
            {
                RadioAdmin11DBClose();
            }
        }
        return resultSet;
    }

    /// <summary>
    /// 라디오 1318 모니터 요원 모집 결과 리스트를 가져온다.
    /// </summary>
    /// <param name="ProgCode"></param>
    /// <returns></returns>
    public DataSet GetRadioMoniterResultList(string ProgCode)
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_RADIOMONITER_SELECT",
                    new SqlParameter("@progcode", ProgCode));
            }
            catch (System.Exception e)
            {

            }
            finally
            {
                RadioAdminDBClose();
            }
        }

        return ds;
    }

    /// <summary>
    /// 미니 천만 다운로드 현황 리스트를 반환한다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetMini1000EventList()
    {
        DataSet ds = new DataSet();
        if (RadioEventDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_TBL_MINI_1000EVENT_SELECT");
            }
            catch (System.Exception e)
            {

            }
            finally
            {
                RadioEventDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 미니 천만 다운로드 로그 현황을 반환한다.
    /// </summary>
    /// <param name="ProgCode"></param>
    /// <returns></returns>
    public DataSet GetMini1000EventUserList(string ProgCode, int curPage, int pageRow)
    {
        DataSet ds = new DataSet();
        if (RadioEventDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_TBL_MINI_1000EVENTLOG_SELECT",
                    new SqlParameter("@progCode", ProgCode),
                    new SqlParameter("@curPage", curPage),
                    new SqlParameter("@pageRow", pageRow));

            }
            catch (System.Exception e)
            {

            }
            finally
            {
                RadioEventDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 미니 최종 당첨자 수를 가져온다.
    /// </summary>
    /// <returns></returns>
    public int GetMiniFinalUserCount()
    {
        int iReturn = 0;
        if (RadioEventDBOpen())
        {
            try
            {
                iReturn = (int)SqlHelper.ExecuteScalar(Conn, CommandType.StoredProcedure,
                    "USP_MINI_1000EVENT_FINALCOUNT");
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                RadioEventDBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 최종 당첨자 명단 리스트를 가져온다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetMini1000EventFinalUserList()
    {
        DataSet ds = new DataSet();
        if (RadioEventDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_MINI_1000EVENT_FINALLIST");
            }
            catch (System.Exception e)
            {

            }
            finally
            {
                RadioEventDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 최종 당첨자 뽑기에서 한명을 추출한다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetMini1000EventFinal()
    {
        DataSet ds = new DataSet();
        if (RadioEventDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_MINI_1000EVENT_FINAL");
            }
            catch (System.Exception e)
            {

            }
            finally
            {
                RadioEventDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 당첨자로 확정시킴
    /// </summary>
    /// <param name="LogIdx"></param>
    /// <param name="UserNo"></param>	
    public void SetMini1000EventFinalUser(int LogIdx, int UserNo)
    {
        if (RadioEventDBOpen())
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "USP_MINI_1000EVENTLOG_UPDATE",
                    new SqlParameter("@LogIdx", LogIdx),
                    new SqlParameter("@userno", UserNo));
            }
            catch (System.Exception e)
            {

            }
            finally
            {
                RadioEventDBClose();
            }
        }
    }

    /// <summary>
    /// 미니 천만 다운로드에 응모처리 한다.
    /// </summary>
    /// <param name="EventIdx"></param>
    /// <param name="UserNo"></param>
    /// <param name="UserId"></param>
    /// <param name="UserName"></param>
    /// <param name="Cellphone"></param>
    /// <param name="Phone"></param>
    /// <param name="Addr"></param>
    /// <param name="ProgCode"></param>
    public void SetMini1000EventApply(int EventIdx, int UserNo, string UserId, string UserName, string Cellphone, string Phone, string Addr, string ProgCode)
    {
        if (RadioEventDBOpen())
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                "USP_MINI_1000EVENTLOG_TEMP_INSERT",
                    new SqlParameter("@EventIdx", EventIdx),
                    new SqlParameter("@UserNo", UserNo),
                    new SqlParameter("@UserID", UserId),
                    new SqlParameter("@UserName", UserName),
                    new SqlParameter("@CellPhone", Cellphone),
                    new SqlParameter("@Phone", Phone),
                    new SqlParameter("@Addr", Addr),
                    new SqlParameter("@ProgCode", ProgCode));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                RadioEventDBClose();
            }
        }
    }

    /// <summary>
    /// 이벤트에 참여 여부를 확인한다.
    /// </summary>
    /// <param name="UserNo"></param>
    /// <returns></returns>
    public DataSet GetMini1000EventUserCheck(int UserNo)
    {
        DataSet ds = new DataSet();
        if (RadioEventDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_MINI_1000EVENTLOG_USERCHECK",
                    new SqlParameter("@userno", UserNo));
            }
            catch (System.Exception e)
            {

            }
            finally
            {
                RadioEventDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 이벤트 마감 여부를 체크한다.
    /// </summary>
    /// <param name="ProgCode"></param>
    /// <returns></returns>
    public int GetMini1000EventInfo(int EventIdx)
    {
        int iResult = 0;
        if (RadioEventDBOpen())
        {
            try
            {
                iResult = (int)SqlHelper.ExecuteScalar(Conn, CommandType.StoredProcedure,
                    "USP_MINI_1000EVENT_MEMBERCHECK",
                    new SqlParameter("@eventIdx", EventIdx));
                if (iResult <= 1800)
                {
                    iResult = 0;
                }
                else
                {
                    iResult = 1;
                }
            }
            catch (System.Exception e)
            {
                iResult = 1;
            }
            finally
            {
                RadioEventDBClose();
            }
        }
        return iResult;
    }

    /// <summary>
    /// 2009 07 이벤트 쪽지 발송 리스트를 반환한다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetMini200907EventList()
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure, "GetMiniEvent200907List");

            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    public void SetMini200907EventSendLog(string Memono)
    {
        if (RadioAdminDBOpen())
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure, "SetMiniEvent200907SendLog",
                    new SqlParameter("@memono", Memono));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
    }

    public DataSet GetMini200907EventUserList(string prod)
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure, "GetMiniEvent200907UserLog",
                    new SqlParameter("@prd", prod));

            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }


    #endregion


    #region MiniWeb 관련 DB 명령문 처리
    /// <summary>
    /// 선곡리스트를 불러온다.
    /// </summary>
    /// <param name="ProgID"></param>
    /// <returns></returns>
    public DataSet GetSomList(string ProgID)
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "dbowr_select.usp_get_som_list",
                    new SqlParameter("@strProgCd", ProgID));

            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 500만 다운로드 이벤트 정보를 가져온다.
    /// </summary>
    /// <param name="Usr_no"></param>
    /// <param name="Usr_id"></param>
    /// <returns></returns>
    public DataSet GetMini500EventInfo(int Usr_no, string Usr_id)
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_MINIMBC500DOWNEVENT_SELECT",
                    new SqlParameter("@USR_NO", Usr_no),
                    new SqlParameter("@USR_ID", Usr_id));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 500만 다운로드 이벤트 미니 url이 있는지 확인한다.
    /// </summary>
    /// <param name="MyMiniURL"></param>
    /// <returns></returns>
    public bool GetMini500EventInfo(string MyMiniURL)
    {
        bool bReturn = false;
        if (RadioAdminDBOpen())
        {
            try
            {
                string strReturn = SqlHelper.ExecuteScalar(Conn, CommandType.StoredProcedure,
                    "USP_MINIMBC500EVENT_URLSELECT",
                    new SqlParameter("@miniURL", MyMiniURL)).ToString();

                if (strReturn == "" || strReturn == null)
                {
                    bReturn = false;
                }
                else
                {
                    bReturn = true;
                }

            }
            catch
            {
                bReturn = false;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return bReturn;
    }

    /// <summary>
    /// 미니 500만 다운로드 이벤트 응모권 숫자 가져오기
    /// </summary>
    /// <param name="Usr_no"></param>
    /// <param name="Usr_id"></param>
    /// <returns></returns>
    public int GetMini500EventStampCnt(int Usr_no, string Usr_id)
    {
        int iReturn = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = Convert.ToInt32(SqlHelper.ExecuteScalar(Conn, CommandType.StoredProcedure,
                    "USP_MINIMBC500DOWNEVENT_SELECTSTAMPCOUNT",
                    new SqlParameter("@USR_NO", Usr_no),
                    new SqlParameter("@USR_ID", Usr_id)));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 미니 이벤트 정보가 있는지 있으면 정보 가져온다.
    /// </summary>
    /// <param name="Usr_no"></param>
    /// <param name="Usr_id"></param>
    /// <returns></returns>
    public DataSet GetMiniEventInfo(int Usr_no, string Usr_id)
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "sp_GetEventInfo",
                    new SqlParameter("@USR_NO", Usr_no),
                    new SqlParameter("@USR_ID", Usr_id));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 미니 이벤트 생성된 주소가 있는지 확인한다.
    /// </summary>
    /// <param name="MiniURL"></param>
    /// <returns></returns>
    public bool GetMiniEventInfo(string MiniURL)
    {
        bool bReturn = false;
        if (RadioAdminDBOpen())
        {
            try
            {
                string strReturn = SqlHelper.ExecuteScalar(Conn, CommandType.StoredProcedure,
                    "sp_GetMyMiniURL",
                    new SqlParameter("@miniURL", MiniURL)).ToString();

                if (strReturn == "" || strReturn == null)
                {
                    bReturn = false;
                }
                else
                {
                    bReturn = true;
                }

            }
            catch
            {
                bReturn = false;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return bReturn;
    }

    /// <summary>
    /// 500만 다운 이벤트 정보를 저장한다.
    /// </summary>
    /// <param name="Usr_no"></param>
    /// <param name="Usr_id"></param>
    /// <param name="MyMiniURL"></param>
    /// <param name="Remote_Addr"></param>
    /// <returns></returns>
    public int SetMini500EventInfo(int Usr_no, string Usr_id, string MyMiniURL, string Remote_Addr)
    {
        int iReturn = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = (int)SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "USP_MINIMBC500DOWNEVENT_INSERT",
                    new SqlParameter("@usr_no", Usr_no),
                    new SqlParameter("@usr_id", Usr_id),
                    new SqlParameter("@myminiurl", MyMiniURL),
                    new SqlParameter("@remote_addr", Remote_Addr));

            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iReturn;
    }



    /// <summary>
    /// 500만 다운로드의 다운로드 로그를 저장한다.
    /// </summary>
    /// <param name="MyMiniURL"></param>
    /// <param name="Remote_Addr"></param>
    /// <returns></returns>
    public int SetMini500EventLogInfo(string MyMiniURL, string Remote_Addr)
    {
        int iReturn = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = (int)SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "USP_MINIMBC500EVENTLOG_INSERT",
                    new SqlParameter("@myminiurl", MyMiniURL),
                    new SqlParameter("@remote_addr", Remote_Addr));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBOpen();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 미니 영화 이벤트 관련 로그 쌓기
    /// </summary>
    /// <param name="UserNo"></param>
    /// <param name="UserID"></param>
    /// <param name="UserIP"></param>
    /// <returns></returns>
    public int SetMiniMovieEventLog(int UserNo, string UserID, string UserIP)
    {
        int iReturn = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = (int)SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "USP_MINIMOVIEEVENTLOG_INSERT",
                    new SqlParameter("@userno", UserNo),
                    new SqlParameter("@userid", UserID),
                    new SqlParameter("@userip", UserIP));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBOpen();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 미니 이벤트 정보를 저장한다.
    /// </summary>
    /// <param name="Usr_no"></param>
    /// <param name="Usr_id"></param>
    /// <param name="MyMiniURL"></param>
    /// <returns></returns>
    public int SetMiniEventInfo(int Usr_no, string Usr_id, string MyMiniURL)
    {
        int iReturn = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = (int)SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "sp_AddEventInfo",
                    new SqlParameter("@USR_NO", Usr_no),
                    new SqlParameter("@USR_ID", Usr_id),
                    new SqlParameter("@MYMINIURL", MyMiniURL));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 미니 500만 다운로드 이벤트 정보를 수정한다.
    /// </summary>
    /// <param name="Usr_no"></param>
    /// <param name="Usr_id"></param>
    /// <returns></returns>
    public int UpdateMini500EVENTApplyInfo(int Usr_no, string Usr_id)
    {
        int iReturn = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = (int)SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "USP_MINI500DOWNEVENT_UPDATE",
                    new SqlParameter("@usr_no", Usr_no),
                    new SqlParameter("@usr_id", Usr_id));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iReturn;
    }
    /// <summary>
    /// 미니 1주년 이벤트의 내 미니주소로 접근하여 조회,다운로드한 사용자 수를 업데이트 한다.
    /// </summary>
    /// <param name="MyMiniURL"></param>
    /// <returns></returns>
    public int UpdateMiniUrlPage(string MyMiniURL)
    {
        int iReturn = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = (int)SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                        "sp_SetEventView",
                        new SqlParameter("@MYMINIURL", MyMiniURL));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 라디오 게시판 정보 가져온다.
    /// </summary>
    /// <param name="ProgCode"></param>
    /// <returns></returns>
    public DataSet GetMemoInfo(string ProgCode)
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "dbowr_select.RetrieveMemoMstInfo",
                    new SqlParameter("@progCode", ProgCode));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 프로그램 별 미니메세지 게시판의 웹 노출용 URL 정보를 반환한다.
    /// </summary>
    /// <param name="ProgCode"></param>
    /// <returns></returns>
    public string GetMemoWebURLInfo(string ProgCode)
    {
        string MiniWebURL = string.Empty;
#if DEBUG
	if (LocalDBOpen())
#else
        if (RadioAdmin11DBOpen())
#endif
        {
            try
            {
                MiniWebURL = (string)SqlHelper.ExecuteScalar(Conn, CommandType.StoredProcedure,
                    "GetMemoWebURLInfo",
                    new SqlParameter("@progCode", ProgCode));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
#if DEBUG
	LocalDBClose();
#else
                RadioAdmin11DBClose();
#endif

            }
        }

        return MiniWebURL;
    }

    /// <summary>
    /// 각 질문별 답변 통계치를 반환한다.
    /// </summary>
    /// <param name="PollNum"></param>
    /// <param name="PollSubNum"></param>
    /// <param name="Pollidx"></param>
    /// <returns></returns>
    public DataSet GetPollResult(string PollNum, string PollSubNum, int Pollidx)
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_TBL_RADIOPOLL_RESULT_SELECTSTATE",
                    new SqlParameter("@pollnum", PollNum),
                    new SqlParameter("@pollsubnum", PollSubNum),
                    new SqlParameter("@pollidx", Pollidx));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }

        return ds;
    }
    /// <summary>
    /// 총 설문 참여수를 반환한다.
    /// </summary>
    /// <param name="PollIdx"></param>
    /// <returns></returns>
    public int GetPollTotalCount(int PollIdx)
    {
        int iReturn = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = (int)SqlHelper.ExecuteScalar(Conn, CommandType.StoredProcedure,
                    "USP_TBL_RADIOPOLL_RESULT_SELECTCOUNT",
                    new SqlParameter("@pollidx", PollIdx));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 설문 리스트를 가져온다.
    /// </summary>
    /// <param name="PollIdx">설문고유번호</param>
    /// <returns></returns>
    public DataSet GetPollList(int PollIdx)
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_TBL_RADIOPOLL_QUESTION_SELECT",
                    new SqlParameter("@pollidx", PollIdx));

            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 설문 답변 리스트를 가져온다.
    /// </summary>
    /// <param name="PollIdx"></param>
    /// <param name="Pollnum"></param>
    /// <param name="pollSubNum"></param>
    /// <returns></returns>
    public DataSet GetPollAnswerList(int PollIdx, string Pollnum, string pollSubNum)
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_TBL_RADIOPOLL_ANSWER_SELECT",
                    new SqlParameter("@pollidx", PollIdx),
                    new SqlParameter("@pollnum", Convert.ToInt32(Pollnum)),
                    new SqlParameter("@pollsubnum", Convert.ToInt32(pollSubNum)));

            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    public struct PollStruct
    {
        public string Answer1_1;
        public string Answer1_2;
        public string Answer1_3;
        public string Answer1_4;
        public string Answer1_5;
        public string Answer2;
        public string Answer3;
        public string Answer4;
        public string Answer5_1;
        public string Answer5_2;
        public string Answer5_3;
        public string Answer5_4;
        public string Answer5_5;
        public string Answer5_6;
        public string Answer5_7;
        public string Answer5_8;
        public string Answer6_1;
        public string Answer6_2;
        public string Answer6_3;
        public string Answer6_4;
        public string Answer6_5;
        public string Answer7_1;
        public string Answer7_2;
        public string Answer7_3;
        public string Answer7_4;
        public string Answer7_5;
        public string Answer7_6;
        public string Answer7_7;
        public string Answer8_1;
        public string Answer8_2;
        public string Answer8_3;
        public string Answer8_4;
        public string Answer8_5;
        public string Answer8_6;
        public string Answer9_1;
        public string Answer9_2;
        public string Answer9_3;
        public string Answer10_1;
        public string Answer10_2;
        public string Answer10_3;
        public string Answer10_4;
        public string Answer10_5;
        public string Answer10_6;
        public string Answer10_7;
        public string Answer10_8;
        public string Answer10_9;
        public string Answer10_10;
        public string Answer10_11;
        public string Answer10_12;
        public string Answer10_13;
        public string Answer10_14;
        public string Answer11_1;
        public string Answer11_2;
        public string Answer11_3;
        public string Answer11_4;
        public string Answer11_5;
        public string Answer11_6;
        public string Answer11_7;
        public string Answer11_8;
        public string Answer12_1;
        public string Answer12_2;
        public string Answer12_3;
        public string Answer12_4;
        public string Answer12_5;
        public string Answer13_1;
        public string Answer13_2;
        public string Answer13_3;
        public string Answer13_4;
        public string Answer14_1;
        public string Answer14_2;
        public string Answer14_3;
        public string Answer14_4;
        public string Answer14_5;
        public string Answer14_6;
        public string Answer14_7;
        public string Answer14_8;
        public string Answer15_1;
        public string Answer15_2;
        public string Answer15_3;
        public string Answer15_4;
        public string Answer15_5;
        public string Answer15_6;
        public string Answer16;
        public string Answer17_1;
        public string Answer17_2;
        public string Answer17_3;
        public string Answer17_4;
        public string Answer17_5;
        public string Answer17_6;
        public string Answer18_1;
        public string Answer18_2;
        public string Answer18_3;
        public string Answer18_4;
        public string Answer18_5;
        public string Answer18_6;
        public string Answer18_7;
        public string Answer18_8;
        public string Answer19_1;
        public string Answer19_2;
        public string Answer19_3;
        public string Answer19_4;
        public string Answer19_5;
        public string Answer19_6;
        public string Answer19_7;
        public string Answer19_8;
        public string Answer19_9;
        public string Answer19_10;
        public string Answer19_11;
        public string Answer19_12;
        public string Answer19_13;
    }

    /// <summary>
    /// 설문 투표 결과를 저장한다.
    /// </summary>
    /// <param name="pollIdx"></param>
    /// <param name="UserNo"></param>
    /// <param name="UserID"></param>
    /// <param name="strcutPoll"></param>
    /// <returns></returns>
    public bool SetPollInfo(int pollIdx, int UserNo, string UserID, PollStruct strcutPoll)
    {
        bool bReturn = false;
        if (RadioAdminDBOpen())
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "USP_TBL_RADIOPOLL_RESULT_INSERT",
                    new SqlParameter("@pollidx", pollIdx),
                    new SqlParameter("@userno", UserNo),
                    new SqlParameter("@userid", UserID),
                    new SqlParameter("@answer1_1", strcutPoll.Answer1_1),
                    new SqlParameter("@answer1_2", strcutPoll.Answer1_2),
                    new SqlParameter("@answer1_3", strcutPoll.Answer1_3),
                    new SqlParameter("@answer1_4", strcutPoll.Answer1_4),
                    new SqlParameter("@answer1_5", strcutPoll.Answer1_5),
                    new SqlParameter("@answer2", strcutPoll.Answer2),
                    new SqlParameter("@answer3", strcutPoll.Answer3),
                    new SqlParameter("@answer4", strcutPoll.Answer4),
                    new SqlParameter("@answer5_1", strcutPoll.Answer5_1),
                    new SqlParameter("@answer5_2", strcutPoll.Answer5_2),
                    new SqlParameter("@answer5_3", strcutPoll.Answer5_3),
                    new SqlParameter("@answer5_4", strcutPoll.Answer5_4),
                    new SqlParameter("@answer5_5", strcutPoll.Answer5_5),
                    new SqlParameter("@answer5_6", strcutPoll.Answer5_6),
                    new SqlParameter("@answer5_7", strcutPoll.Answer5_7),
                    new SqlParameter("@answer5_8", strcutPoll.Answer5_8),
                    new SqlParameter("@answer6_1", strcutPoll.Answer6_1),
                    new SqlParameter("@answer6_2", strcutPoll.Answer6_2),
                    new SqlParameter("@answer6_3", strcutPoll.Answer6_3),
                    new SqlParameter("@answer6_4", strcutPoll.Answer6_4),
                    new SqlParameter("@answer6_5", strcutPoll.Answer6_5),
                    new SqlParameter("@answer7_1", strcutPoll.Answer7_1),
                    new SqlParameter("@answer7_2", strcutPoll.Answer7_2),
                    new SqlParameter("@answer7_3", strcutPoll.Answer7_3),
                    new SqlParameter("@answer7_4", strcutPoll.Answer7_4),
                    new SqlParameter("@answer7_5", strcutPoll.Answer7_5),
                    new SqlParameter("@answer7_6", strcutPoll.Answer7_6),
                    new SqlParameter("@answer7_7", strcutPoll.Answer7_7),
                    new SqlParameter("@answer8_1", strcutPoll.Answer8_1),
                    new SqlParameter("@answer8_2", strcutPoll.Answer8_2),
                    new SqlParameter("@answer8_3", strcutPoll.Answer8_3),
                    new SqlParameter("@answer8_4", strcutPoll.Answer8_4),
                    new SqlParameter("@answer8_5", strcutPoll.Answer8_5),
                    new SqlParameter("@answer8_6", strcutPoll.Answer8_6),
                    new SqlParameter("@answer9_1", strcutPoll.Answer9_1),
                    new SqlParameter("@answer9_2", strcutPoll.Answer9_2),
                    new SqlParameter("@answer9_3", strcutPoll.Answer9_3),
                    new SqlParameter("@answer10_1", strcutPoll.Answer10_1),
                    new SqlParameter("@answer10_2", strcutPoll.Answer10_2),
                    new SqlParameter("@answer10_3", strcutPoll.Answer10_3),
                    new SqlParameter("@answer10_4", strcutPoll.Answer10_4),
                    new SqlParameter("@answer10_5", strcutPoll.Answer10_5),
                    new SqlParameter("@answer10_6", strcutPoll.Answer10_6),
                    new SqlParameter("@answer10_7", strcutPoll.Answer10_7),
                    new SqlParameter("@answer10_8", strcutPoll.Answer10_8),
                    new SqlParameter("@answer10_9", strcutPoll.Answer10_9),
                    new SqlParameter("@answer10_10", strcutPoll.Answer10_10),
                    new SqlParameter("@answer10_11", strcutPoll.Answer10_11),
                    new SqlParameter("@answer10_12", strcutPoll.Answer10_12),
                    new SqlParameter("@answer10_13", strcutPoll.Answer10_13),
                    new SqlParameter("@answer10_14", strcutPoll.Answer10_14),
                    new SqlParameter("@answer11_1", strcutPoll.Answer11_1),
                    new SqlParameter("@answer11_2", strcutPoll.Answer11_2),
                    new SqlParameter("@answer11_3", strcutPoll.Answer11_3),
                    new SqlParameter("@answer11_4", strcutPoll.Answer11_4),
                    new SqlParameter("@answer11_5", strcutPoll.Answer11_5),
                    new SqlParameter("@answer11_6", strcutPoll.Answer11_6),
                    new SqlParameter("@answer11_7", strcutPoll.Answer11_7),
                    new SqlParameter("@answer11_8", strcutPoll.Answer11_8),
                    new SqlParameter("@answer12_1", strcutPoll.Answer12_1),
                    new SqlParameter("@answer12_2", strcutPoll.Answer12_2),
                    new SqlParameter("@answer12_3", strcutPoll.Answer12_3),
                    new SqlParameter("@answer12_4", strcutPoll.Answer12_4),
                    new SqlParameter("@answer12_5", strcutPoll.Answer12_5),
                    new SqlParameter("@answer13_1", strcutPoll.Answer13_1),
                    new SqlParameter("@answer13_2", strcutPoll.Answer13_2),
                    new SqlParameter("@answer13_3", strcutPoll.Answer13_3),
                    new SqlParameter("@answer13_4", strcutPoll.Answer13_4),
                    new SqlParameter("@answer14_1", strcutPoll.Answer14_1),
                    new SqlParameter("@answer14_2", strcutPoll.Answer14_2),
                    new SqlParameter("@answer14_3", strcutPoll.Answer14_3),
                    new SqlParameter("@answer14_4", strcutPoll.Answer14_4),
                    new SqlParameter("@answer14_5", strcutPoll.Answer14_5),
                    new SqlParameter("@answer14_6", strcutPoll.Answer14_6),
                    new SqlParameter("@answer14_7", strcutPoll.Answer14_7),
                    new SqlParameter("@answer14_8", strcutPoll.Answer14_8),
                    new SqlParameter("@answer15_1", strcutPoll.Answer15_1),
                    new SqlParameter("@answer15_2", strcutPoll.Answer15_2),
                    new SqlParameter("@answer15_3", strcutPoll.Answer15_3),
                    new SqlParameter("@answer15_4", strcutPoll.Answer15_4),
                    new SqlParameter("@answer15_5", strcutPoll.Answer15_5),
                    new SqlParameter("@answer15_6", strcutPoll.Answer15_6),
                    new SqlParameter("@answer16", strcutPoll.Answer16),
                    new SqlParameter("@answer17_1", strcutPoll.Answer17_1),
                    new SqlParameter("@answer17_2", strcutPoll.Answer17_2),
                    new SqlParameter("@answer17_3", strcutPoll.Answer17_3),
                    new SqlParameter("@answer17_4", strcutPoll.Answer17_4),
                    new SqlParameter("@answer17_5", strcutPoll.Answer17_5),
                    new SqlParameter("@answer17_6", strcutPoll.Answer17_6),
                    new SqlParameter("@answer18_1", strcutPoll.Answer18_1),
                    new SqlParameter("@answer18_2", strcutPoll.Answer18_2),
                    new SqlParameter("@answer18_3", strcutPoll.Answer18_3),
                    new SqlParameter("@answer18_4", strcutPoll.Answer18_4),
                    new SqlParameter("@answer18_5", strcutPoll.Answer18_5),
                    new SqlParameter("@answer18_6", strcutPoll.Answer18_6),
                    new SqlParameter("@answer18_7", strcutPoll.Answer18_7),
                    new SqlParameter("@answer18_8", strcutPoll.Answer18_8),
                    new SqlParameter("@answer19_1", strcutPoll.Answer19_1),
                    new SqlParameter("@answer19_2", strcutPoll.Answer19_2),
                    new SqlParameter("@answer19_3", strcutPoll.Answer19_3),
                    new SqlParameter("@answer19_4", strcutPoll.Answer19_4),
                    new SqlParameter("@answer19_5", strcutPoll.Answer19_5),
                    new SqlParameter("@answer19_6", strcutPoll.Answer19_6),
                    new SqlParameter("@answer19_7", strcutPoll.Answer19_7),
                    new SqlParameter("@answer19_8", strcutPoll.Answer19_8),
                    new SqlParameter("@answer19_9", strcutPoll.Answer19_9),
                    new SqlParameter("@answer19_10", strcutPoll.Answer19_10),
                    new SqlParameter("@answer19_11", strcutPoll.Answer19_11),
                    new SqlParameter("@answer19_12", strcutPoll.Answer19_12),
                    new SqlParameter("@answer19_13", strcutPoll.Answer19_13));
            }
            catch (Exception e)
            {
                bReturn = false;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return bReturn;
    }

    /// <summary>
    /// 해당 사용자의 설문 툴 내역이 있는지를 확인한다.
    /// </summary>
    /// <param name="UserNo"></param>
    /// <param name="UserID"></param>
    /// <param name="PollIdx"></param>
    /// <returns></returns>
    public int GetPollHistory(int UserNo, string UserID, int PollIdx)
    {
        int iReturn = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = (int)SqlHelper.ExecuteScalar(Conn, CommandType.StoredProcedure,
                    "USP_TBL_RADIOPOLL_RESULT_SELECT",
                    new SqlParameter("@userno", UserNo),
                    new SqlParameter("@userid", UserID),
                    new SqlParameter("@pollidx", PollIdx));
            }
            catch (Exception e)
            {
                iReturn = 0;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 라디오 모니터 요원 모집 
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="Tel1"></param>
    /// <param name="Tel2"></param>
    /// <param name="Tel3"></param>
    /// <param name="ZipCode"></param>
    /// <param name="Addr1"></param>
    /// <param name="Addr2"></param>
    /// <param name="School"></param>
    /// <param name="ProfileName"></param>
    /// <param name="ProgramFileName"></param>
    /// <param name="ProgCode"></param>
    /// <returns></returns>
    public int SetRadioMoniter(string UserName, string Tel1, string Tel2, string Tel3, string ZipCode,
        string Addr1, string Addr2, string School, string ProfileName, string ProgramFileName,
        string ProgCode, string MoniterID)
    {
        int iReturn = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = (int)SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                        "USP_TBL_RADIOMONITER_INSERT",
                        new SqlParameter("@username", UserName),
                        new SqlParameter("@tel1", Tel1),
                        new SqlParameter("@tel2", Tel2),
                        new SqlParameter("@tel3", Tel3),
                        new SqlParameter("@zipcode", ZipCode),
                        new SqlParameter("@addr1", Addr1),
                        new SqlParameter("@addr2", Addr2),
                        new SqlParameter("@school", School),
                        new SqlParameter("@profilename", ProfileName),
                        new SqlParameter("@moniterfilename", ProgramFileName),
                        new SqlParameter("@progcode", ProgCode),
                        new SqlParameter("@moniterid", MoniterID));

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 미니 메일 보내기 기능
    /// </summary>
    /// <param name="MailServiceID"></param>
    /// <param name="MailTo"></param>
    /// <param name="MailFrom"></param>
    /// <param name="Subject"></param>
    /// <param name="MailContents"></param>
    /// <returns></returns>
    public int SetSendMail(int MailServiceID, string MailTo, string MailFrom, string Subject, string MailContents)
    {
        int iReturn = 0;
        if (MailServiceDBOpen())
        {
            try
            {
                iReturn = (int)SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "RegisterMailQueueInfo",
                    new SqlParameter("@serviceID", MailServiceID),
                    new SqlParameter("@mailTo", MailTo),
                    new SqlParameter("@mailFrom", MailFrom),
                    new SqlParameter("@subject", Subject),
                    new SqlParameter("@content", MailContents));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                MailServiceDBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 미니 ECard 이벤트 발송자에게 포인트 선물하기
    /// </summary>
    /// <param name="iPoint"></param>
    /// <param name="UserNo"></param>
    /// <returns></returns>
    public int SendEcardPoint(int iPoint, int UserNo, string RemoteAddr)
    {
        int iReturn = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = (int)SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "USP_MINIECARD_POINT_INSERT",
                    new SqlParameter("@point", iPoint),
                    new SqlParameter("@userno", UserNo),
                    new SqlParameter("@remote_addr", RemoteAddr));
            }
            catch (Exception e)
            {
                iReturn = -1;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 이카드 포인트 가져오기
    /// </summary>
    /// <param name="UserNo"></param>
    /// <returns></returns>
    public int GetMiniEcardPoint(int UserNo)
    {
        int iReturn = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = Convert.ToInt32(SqlHelper.ExecuteScalar(Conn, CommandType.StoredProcedure,
                    "dbowr_select.USP_TB_MINI_ECARD_POINT_SELECT",
                    new SqlParameter("@userno", UserNo)));

            }
            catch (Exception e)
            {
                iReturn = 0;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 이카드 메일 보내는 로그 남기기
    /// </summary>
    /// <param name="UserNo"></param>
    /// <param name="UserID"></param>
    /// <param name="UserName"></param>
    /// <param name="Email"></param>
    /// <param name="PersonNo"></param>
    /// <param name="ReceiverMail"></param>
    /// <param name="strSubject"></param>
    /// <param name="strBody"></param>
    /// <returns></returns>
    public int SendEcardMail(int UserNo, string UserID, string UserName, string Email, string PersonNo,
                            string PhoneNum, string Address, string ZipCode,
                            string ReceiverMail, string strSubject, string strBody, string hostAddr)
    {
        int iReturn = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = (int)SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "USP_MINIECARD_LOG_INSERT",
                    new SqlParameter("@userno", UserNo),
                    new SqlParameter("@userid", UserID),
                    new SqlParameter("@username", UserName),
                    new SqlParameter("@usermail", Email),
                    new SqlParameter("@persono", PersonNo),
                    new SqlParameter("@phone", PhoneNum),
                    new SqlParameter("@address", Address),
                    new SqlParameter("@zipcode", ZipCode),
                    new SqlParameter("@receivermail", ReceiverMail),
                    new SqlParameter("@subject", strSubject),
                    new SqlParameter("@strMailContents", strBody),
                    new SqlParameter("@remoteaddr", hostAddr));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// OX 퀴즈 이벤트 내역을 가져온다. 1교시 이벤트 
    /// </summary>
    /// <param name="UserUno"></param>
    /// <returns></returns>
    public int GetMiniOXEventInfo(int UserUno)
    {
        int iResult = 0;

        if (RadioAdminDBOpen())
        {
            try
            {
                iResult = (int)SqlHelper.ExecuteScalar(Conn, CommandType.StoredProcedure,
                    "USP_MINIOXQUIZ_1NumCount",
                    new SqlParameter("@userno", UserUno));
            }
            catch (Exception e)
            {
                //					throw e;
                iResult = 0;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iResult;
    }

    /// <summary>
    /// 미니 OX 퀴즈 이벤트 1교시 이벤트 로그를 저장한다.
    /// </summary>
    /// <param name="UserUno"></param>
    /// <param name="Num1"></param>
    public void SetMiniOXEvent1ClassLog(int UserUno, int Num1)
    {
        if (RadioAdminDBOpen())
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "USP_MINI_OXQUIZ1ClassLog_INSERT",
                    new SqlParameter("@userno", UserUno),
                    new SqlParameter("@num1", Num1));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
    }

    /// <summary>
    /// 이벤트 랭킹 순위를 반환한다.
    /// </summary>
    /// <returns></returns>
    public DataSet GetMiniOXEventRankUserList()
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_MINI_OXQUIZ_RANKUSERLIST");

            }
            catch (Exception e)
            {
                ds = null;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 미니 이벤트 클래스 2 로그 저장하기
    /// </summary>
    /// <param name="UserUno"></param>
    public int SetMiniOXEvent2ClassLog(int UserUno, int QuizIdx)
    {
        int iReturn = 0;

        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "USP_MINI_OXQUIZ_CLASS2INSERT",
                    new SqlParameter("@userno", UserUno),
                    new SqlParameter("@quizIdx", QuizIdx));
            }
            catch (Exception e)
            {
                iReturn = -1;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 미니 퀴즈 이벤트 2교시 문제 가져오기
    /// </summary>
    /// <param name="UserUno"></param>
    /// <returns></returns>
    public DataSet GetMiniQuizList(int UserUno)
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                "USP_MINI_OXQUIZRANDOMSELECT",
                new SqlParameter("@userno", UserUno));

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 미니 OX 퀴즈 자신의 점수를 가져온다.
    /// </summary>
    /// <param name="UserUno"></param>
    /// <returns></returns>
    public int GetMiniOXMyScore(int UserUno)
    {
        int iReturn = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = (int)SqlHelper.ExecuteScalar(Conn, CommandType.StoredProcedure,
                    "USP_MINI_OXQUIZ_MYSCORE",
                    new SqlParameter("@userno", UserUno));
            }
            catch (Exception e)
            {
                iReturn = 0;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 2교시 퀴즈 로그를 지운다.(랜덤문제 처리를 위해)
    /// </summary>
    /// <param name="UserUno"></param>
    public void SetMiniOXEvent2ClassEnding(int UserUno)
    {
        if (RadioAdminDBOpen())
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "USP_MINI_OXQUIZ_CLASS2LOG_DELETE",
                    new SqlParameter("@userno", UserUno));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
    }

    /// <summary>
    /// 퀴즈 로그를 수정한다.
    /// </summary>
    /// <param name="UserUno"></param>
    /// <param name="UserID"></param>
    /// <param name="UserName"></param>
    /// <param name="Email"></param>
    /// <param name="Address"></param>
    /// <param name="Phone"></param>
    /// <param name="Score"></param>
    public void SetMiniOXEventQuizLogUpdate(string Score, int UserNo)
    {
        if (RadioAdminDBOpen())
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "USP_TBL_MINI_OXQUIZLOG_UPDATE",
                    new SqlParameter("@userScore", int.Parse(Score)),
                    new SqlParameter("@userno", UserNo));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
    }

    /// <summary>
    /// 퀴즈 로그를 insert 한다. 1교시 끝날때 넣는다.
    /// </summary>
    /// <param name="UserUno"></param>
    /// <param name="UserID"></param>
    /// <param name="UserName"></param>
    /// <param name="Email"></param>
    /// <param name="Address"></param>
    /// <param name="Phone"></param>
    /// <returns></returns>
    public int SetMiniOXEventQuizLog(int UserUno, string UserID, string UserName, string Email, string Address, string Phone)
    {
        int iReturn = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "USP_TBL_MINI_OXQUIZLOG_INSERT",
                    new SqlParameter("@userno", UserUno),
                    new SqlParameter("@userid", UserID),
                    new SqlParameter("@username", UserName),
                    new SqlParameter("@email", Email),
                    new SqlParameter("@address", Address),
                    new SqlParameter("@phone", Phone));

            }
            catch (Exception e)
            {
                iReturn = -1;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 문제 만점 받을 시 총합 점수 반환
    /// </summary>
    /// <returns></returns>
    public int GetMiniOXQuizSumPont()
    {
        int iReturn = 0;
        if (RadioAdminDBOpen())
        {
            try
            {
                iReturn = (int)SqlHelper.ExecuteScalar(Conn, CommandType.StoredProcedure,
                    "USP_MINI_OXQUIZ_SUMPOINT");
            }
            catch (Exception e)
            {
                iReturn = 0;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return iReturn;
    }

    #endregion


    #region Mini 어플과 연동하는 페이지에서 이용하는 DB 명령문

    /// <summary>
    /// 어플리케이션 미니 보이는 라디오 2줄 게시판
    /// </summary>
    /// <param name="progCode"></param>
    /// <returns></returns>
    public DataSet RetrieveMemoListForminiBora(string progCode)
    {
        DataSet ds = new DataSet();
        if (RadioDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "RetrieveMiniMemoListForMiniBora",
                    new SqlParameter("@progCode", progCode));
            }
            catch
            {
                ds = null;
            }
            finally
            {
                RadioDBClose();
            }
        }

        return ds;
    }

    /// <summary>
    /// 라디오 편성표 리스트를 가져온다.
    /// </summary>
    /// <param name="strType"></param>
    /// <returns></returns>
    public DataSet GetRadioScheduleList(string strType, string strView)
    {
        DataSet ds = new DataSet();
        if (RadioAdmin11DBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "dbowr_select.USP_MINISCHEDULE_SELECT",
                    new SqlParameter("@radioType", strType),
                    new SqlParameter("@viewAll", strView));
            }
            catch
            {
                ds = null;
            }
            finally
            {
                RadioAdmin11DBClose();
            }
        }
        //			if (ContentsMasterDBOpen())
        //			{
        //				try 
        //				{
        //					ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
        //						"Mini@usp_GetRadioScheduleList",
        //						new SqlParameter("@radioType", strType), 
        //						new SqlParameter("@viewAll", strView));
        //				}
        //				catch(System.Exception e)
        //				{
        //					throw e;
        //				}
        //				finally
        //				{
        //					ContentsMasterDBClose();
        //				}
        //			}
        return ds;
    }

    /// <summary>
    /// 어플리케이션 로그에 에러 로그를 쌓는다.
    /// </summary>
    /// <param name="ProgramCode"></param>
    /// <param name="mediaURL1"></param>
    /// <param name="mediaURl2"></param>
    /// <param name="errCode"></param>
    /// <param name="errDesript"></param>
    /// <param name="imbcID"></param>
    /// <param name="MPVer"></param>
    /// <param name="OSVer"></param>
    /// <param name="IEVer"></param>
    /// <param name="RemoteIP"></param>
    /// <param name="AppName"></param>
    /// <param name="AppVer"></param>
    /// <param name="Lang1"></param>
    /// <param name="Lang2"></param>
    /// <param name="Lang3"></param>
    /// <param name="ServerIP"></param>
    /// <returns></returns>
    public int SetApplicationLog(string ProgramCode, string mediaURL1, string mediaURl2, string errCode, string errDesript, string imbcID, string MPVer, string OSVer, string IEVer, string RemoteIP, string AppName, string AppVer, string Lang1, string Lang2, string Lang3, string ServerIP)
    {
        int iReturn = 0;
        if (ApplicationDBOpen())
        {
            try
            {
                iReturn = (int)SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "sp_insert_log",
                    new SqlParameter("@programCode", ProgramCode),
                    new SqlParameter("@mediaUrl1", mediaURL1),
                    new SqlParameter("@mediaUrl2", mediaURl2),
                    new SqlParameter("@errCode", errCode),
                    new SqlParameter("@errDescription", errDesript),
                    new SqlParameter("@imbcId", imbcID),
                    new SqlParameter("@MPVer", MPVer),
                    new SqlParameter("@OSVer", OSVer),
                    new SqlParameter("@IEVer", IEVer),
                    new SqlParameter("@RemoteIP", RemoteIP),
                    new SqlParameter("@AppName", AppName),
                    new SqlParameter("@AppVer", AppVer),
                    new SqlParameter("@Lang1", Lang1),
                    new SqlParameter("@Lang2", Lang2),
                    new SqlParameter("@Lang3", Lang3),
                    new SqlParameter("@ServerIP", ServerIP));

            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                ApplicationDBClose();
            }
        }
        return iReturn;
    }

    /// <summary>
    /// 설정된 시간에 예약된 에러 메세지가 있는지를 확인한다.
    /// </summary>
    /// <param name="curTime"></param>
    /// <returns></returns>
    public string GetMiniErrorMsg(string curTime)
    {
        string ReturnMsg = string.Empty;

        try
        {
            if (ApplicationDBOpen())
            {
                ReturnMsg = SqlHelper.ExecuteScalar(Conn, CommandType.StoredProcedure,
                    "sp_GetApplicationErrorMsg",
                    new SqlParameter("@currentDateTime", curTime)).ToString();
            }
        }
        catch (System.Exception e)
        {
            throw e;
        }
        finally
        {
            ApplicationDBClose();
        }
        return ReturnMsg;
    }

    #endregion

    #region MiniCast에서 쓰이는 DB 명령문

    /// <summary>
    /// 미니 캐스트 다운로드 시 로그를 쌓아준다.
    /// </summary>
    /// <param name="FileURL"></param>
    /// <param name="FileName"></param>
    /// <param name="UserIP"></param>
    public void SetMiniCastDownLoadLog(string FileURL, string FileName, string UserIP)
    {
        if (RadioAdminDBOpen())
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "sp_SetMiniCastDownLog",
                    new SqlParameter("@FileURL", FileURL),
                    new SqlParameter("@FileName", FileName),
                    new SqlParameter("@UserIP", UserIP));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
    }

    /// <summary>
    /// 미니 캐스트 다운로드 결과를 반환한다.
    /// </summary>
    /// <param name="SearchDate"></param>
    /// <returns></returns>
    public DataSet GetMiniCastDownLog(string SearchDate)
    {
        DataSet ds = new DataSet();
        if (RadioAdminDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "sp_GetMiniCastDownLog",
                    new SqlParameter("@SearchDate", SearchDate));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 미니 캐스트 다운로드 로그 파일 Data를 Db에 저장한다.
    /// </summary>
    /// <param name="DownDate"></param>
    /// <param name="FileName"></param>
    /// <param name="FileURL"></param>
    /// <param name="UserIP"></param>
    public void InsertMiniCastLog(string DownDate, string FileName, string FileURL, string UserIP)
    {
        if (RadioAdminDBOpen())
        {
            try
            {
                DownDate = DownDate.Replace("\r\n", "");
                SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure,
                    "sp_InsertMiniCastLog",
                    new SqlParameter("@DownDate", DownDate),
                    new SqlParameter("@FileName", FileName),
                    new SqlParameter("@FileURL", FileURL),
                    new SqlParameter("@UserIP", UserIP));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
    }

    #endregion

    /// <summary>
    /// GridCast 용 관리 통계 반환
    /// </summary>
    /// <returns></returns>
    public DataSet GetGridCastStatic(int Hour)
    {
        DataSet ds = new DataSet();
        if (ApplicationDBOpen())
        {
            try
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure, "GetGridCastTable",
                    new SqlParameter("@iHour", Hour));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                ApplicationDBClose();
            }
        }
        return ds;
    }

    /// <summary>
    /// 2009 07 미니 이벤트 로그를 저장한다.
    /// </summary>
    /// <param name="UserNo"></param>
    /// <param name="UserID"></param>
    /// <param name="UserName"></param>
    /// <param name="ZipCode"></param>
    /// <param name="Addr"></param>
    /// <param name="Email"></param>
    /// <param name="Phone"></param>
    /// <param name="Prod"></param>
    public void SetMini200907EvevntLog(int UserNo, string UserID, string UserName, string ZipCode, string Addr, string Email, string Phone, string Prod, int MemoNo, string sID, string sKey)
    {
        if (RadioAdminDBOpen())
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure, "USP_MINI200907Event_Log_Insert",
                    new SqlParameter("@uno", UserNo),
                    new SqlParameter("@userid", UserID),
                    new SqlParameter("@username", UserName),
                    new SqlParameter("@zipcode", ZipCode),
                    new SqlParameter("@addr", Addr),
                    new SqlParameter("@email", Email),
                    new SqlParameter("@phone", Phone),
                    new SqlParameter("@prod", Prod),
                    new SqlParameter("@memono", MemoNo),
                    new SqlParameter("@sid", sID),
                    new SqlParameter("@skey", sKey));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                RadioAdminDBClose();
            }
        }
    }

    #region Test
    public DataSet GetTVScheduleList()
    {
        DataSet ds = new DataSet();
        try
        {
            if (DRMDBOpen())
            {
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure,
                    "USP_SCHEDULE_DAILY_SELECT");
            }
        }
        catch (System.Exception e)
        {
            throw e;
        }
        finally
        {
            DRMDBClose();
        }
        return ds;
    }

    public void SetTest()
    {
        if (RadioDBOpen())
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Conn, CommandType.Text,
                    "insert into TB_INDEXTEST (content) values ('aaaaaaaaaa') ");
            }
            catch { }
            finally
            {
                RadioDBClose();
            }
        }
    }

    #endregion

}