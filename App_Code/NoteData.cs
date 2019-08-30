using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web;
using IMBC.FW.DB;
/// <summary>
/// NoteData의 요약 설명입니다.
/// </summary>
public class NoteData
{
    public NoteData()
    {
        //
        // TODO: 여기에 생성자 논리를 추가합니다.
        //
    }

    /// <summary>
    /// 한줄톡 오늘의 새 메세지 갯수
    /// </summary>
    /// <param name="progCode">프로그램코드</param>
    /// <returns>메세지 갯수</returns>
    public int CountMemoNewInfo(string progCode)
    {
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "CountMemoNewInfo";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = progCode;

        return SQLHelper.ExecuteScalarRetInt(sqlCmd);
    }



    /// <summary>
    /// 한줄톡 게시물 삭제
    /// </summary>
    /// <param name="progCode">프로그램코드</param>
    /// <param name="seqID">게시물번호</param>
    /// <returns>성공여부</returns>
    public bool DeleteMemoInfo(string progCode, int seqID)
    {
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "DeleteMemoInfo";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = progCode;
        sqlCmd.Parameters.Add("@seqID", SqlDbType.Int).Value = seqID;

        return SQLHelper.ExecuteNonQuery(sqlCmd);
    }



    /// <summary>
    /// 관리자 개인 쪽지 보내기
    /// </summary>
    /// <param name="progCode">프로그램코드</param>
    /// <param name="subClassCode">서브클래스코드</param>
    /// <param name="comment">쪽지내용</param>
    /// <param name="progImg">프로그램Img</param>
    /// <param name="al">받는사용자정보들</param>
    /// <returns>성공여부</returns>
    public bool RegisterAdminNoteInfo(string progCode, string subClassCode, string comment, string progImg, ArrayList al)
    {
        //			comment = "<table cellspacing=0 cellpadding=0><tr><td><img src='http://mini1.imbc.com/manager/upload/RAMFM300/bcs.jpg'></td><td>" + comment + "</td></tr></table>";

        //			SqlConnection conn = DbConnection.DbCon;
        SqlConnection conn = DbConnection.DbCon;

        SQLHelper.OpenConnection(conn);

        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "RegisterAdminNoteInfo";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = progCode;
        sqlCmd.Parameters.Add("@comment", SqlDbType.Text).Value = comment;
        sqlCmd.Parameters.Add("@progImg", SqlDbType.VarChar, 255).Value = progImg;

        int noteID = SQLHelper.ExecuteScalarRetInt(conn, sqlCmd);

        sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "ReigsterUserNoteInfo";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@noteID", SqlDbType.Int).Value = noteID;
        sqlCmd.Parameters.Add("@uno", SqlDbType.Int);
        sqlCmd.Parameters.Add("@userID", SqlDbType.VarChar, 30);
        sqlCmd.Parameters.Add("@userNm", SqlDbType.VarChar, 30);

        string userID = "";

        for (int i = 0; i < al.Count; i++)
        {
            NoteUser user = (NoteUser)al[i];
            sqlCmd.Parameters["@uno"].Value = user.uno;
            sqlCmd.Parameters["@userID"].Value = user.userID;
            sqlCmd.Parameters["@userNm"].Value = user.userNm;

            SQLHelper.ExecuteNonQuery(conn, sqlCmd);

            userID += user.userID + (char)9;
        }

        SQLHelper.CloseConnection(conn);


        EncoderUtils.EncScriptSender enc = new EncoderUtils.EncScriptSender();
        //			enc.SendScript("211.233.27.178", 7000, "0002", subClassCode, "http://mini.imbc.com/UserNote/RetrieveUserNoteInfo.aspx?noteID=" + noteID, " ", " ", userID);			
        enc.SendScript("relay2.imbc.com", 7000, "0002", subClassCode, "http://mini.imbc.com/UserNote/RetrieveUserNoteInfo.aspx?noteID=" + noteID, " ", " ", userID);
        return true;
    }

    /// <summary>
    /// 사용자 답변쪽지 보내기
    /// </summary>
    /// <param name="noteID">쪽지ID</param>
    /// <param name="progCode"></param>
    /// <param name="user"></param>
    /// <param name="comment"></param>
    /// <returns></returns>
    public bool RegisterUserReplyNoteInfo(int noteID, string progCode, NoteUser user, string comment)
    {
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "RegisterUserReplyNoteInfo";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@noteID", SqlDbType.Int).Value = noteID;
        sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = progCode;
        sqlCmd.Parameters.Add("@uno", SqlDbType.Int).Value = user.uno;
        sqlCmd.Parameters.Add("@userID", SqlDbType.VarChar, 30).Value = user.userID;
        sqlCmd.Parameters.Add("@userNm", SqlDbType.VarChar, 30).Value = user.userNm;
        sqlCmd.Parameters.Add("@comment", SqlDbType.Text).Value = comment;

        return SQLHelper.ExecuteNonQuery(sqlCmd);
    }

    /// <summary>
    /// 게시물 쓰기
    /// </summary>
    /// <param name="progCode">프로그램코드</param>
    /// <param name="memoType">메모타입</param>
    /// <param name="fontColor">색상</param>
    /// <param name="uno">사용자번호</param>
    /// <param name="userID">사용자아이디</param>
    /// <param name="userNm">사용자이름</param>
    /// <param name="comment">내용</param>
    /// <param name="rank">우선순위</param>
    /// <returns>성공여부</returns>
    public bool RegisterMemoInfo(string progCode, int memoType, string fontColor, int uno, string userID, string userNm, string comment, int rank)
    {
        if (this.IsBadUser(progCode, userID))
        {
            return false;
        }
        else
        {
            if (memoType == 1)
            {
                if (IMBC.FW.Util.WebUtil.GetSession("IsAdmin_" + progCode) == "TRUE")
                    rank = 1;
            }

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "dbowr_select.RegisterMemoInfo_" + progCode;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.Add("@memoType", SqlDbType.SmallInt).Value = memoType;
            sqlCmd.Parameters.Add("@fontColor", SqlDbType.VarChar, 7).Value = fontColor;
            sqlCmd.Parameters.Add("@uno", SqlDbType.Int).Value = uno;
            sqlCmd.Parameters.Add("@userID", SqlDbType.VarChar, 30).Value = userID;
            sqlCmd.Parameters.Add("@userNm", SqlDbType.VarChar, 30).Value = userNm;
            sqlCmd.Parameters.Add("@comment", SqlDbType.Text).Value = comment;
            sqlCmd.Parameters.Add("@rank", SqlDbType.SmallInt).Value = rank;

            return SQLHelper.ExecuteNonQuery(sqlCmd);
        }
    }

    /// <summary>
    /// 게시판 목록 - 보이는 라디오 용
    /// </summary>
    /// <param name="progCode"></param>
    /// <param name="curPage"></param>
    /// <param name="pageRow"></param>
    /// <returns></returns>
    public ListDataView RetrieveMemoListForBora(string progCode, int curPage, int pageRow)
    {
        ListDataView ldv = new ListDataView();

        SqlConnection conn = DbConnection.DbCon;
        SQLHelper.OpenConnection(conn);

        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "CountMemoList";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = progCode;
        sqlCmd.Parameters.Add("@search", SqlDbType.Char, 1).Value = "";
        sqlCmd.Parameters.Add("@searchWord", SqlDbType.VarChar, 20).Value = "";

        ldv.TotalCount = SQLHelper.ExecuteScalarRetInt(conn, sqlCmd);

        sqlCmd.CommandText = "RetrieveMiniMemoListForBora";

        sqlCmd.Parameters.Add("@curPage", SqlDbType.Int).Value = curPage;
        sqlCmd.Parameters.Add("@pageRow", SqlDbType.Int).Value = pageRow;

        ldv.DV = SQLHelper.FillDataAdapter(conn, sqlCmd);

        SQLHelper.CloseConnection(conn);

        return ldv;
    }


    /// <summary>
    /// 게시판 목록 (사용자용)
    /// </summary>
    /// <param name="progCode">프로그램코드</param>
    /// <param name="curPage">현재페이지</param>
    /// <param name="pageRow">페이지크기</param>
    /// <param name="search">검색방식</param>
    /// <param name="searchWord">검색어</param>
    /// <returns>게시판 목록, 총 레코드 수</returns>
    public ListDataView RetrieveMemoList(string progCode, int curPage, int pageRow, string search, string searchWord)
    {
        ListDataView ldv = new ListDataView();
        try
        {
            SqlConnection conn = DbConnection.DbCon;
            SQLHelper.OpenConnection(conn);

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "dbowr_select.CountMemoList";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = progCode;
            sqlCmd.Parameters.Add("@search", SqlDbType.Char, 1).Value = search;
            sqlCmd.Parameters.Add("@searchWord", SqlDbType.VarChar, 20).Value = searchWord;

            ldv.TotalCount = SQLHelper.ExecuteScalarRetInt(conn, sqlCmd);

            sqlCmd.CommandText = "dbowr_select.RetrieveMemoList";

            sqlCmd.Parameters.Add("@curPage", SqlDbType.Int).Value = curPage;
            sqlCmd.Parameters.Add("@pageRow", SqlDbType.Int).Value = pageRow;

            ldv.DV = SQLHelper.FillDataAdapter(conn, sqlCmd);

            SQLHelper.CloseConnection(conn);
        }
        catch (System.NullReferenceException exNull)
        {
            ldv = null;
        }
        return ldv;
    }



    /// <summary>
    /// 게시물 목록 (관리자용)
    /// </summary>
    /// <param name="progCode">프로그램코드</param>
    /// <param name="memoType">게시물 종류(1:웹, 2:쪽지, 3:사연)</param>
    /// <param name="search">검색방식</param>
    /// <param name="searchWord">검색어</param>
    /// <param name="stDt">시작일</param>
    /// <param name="spDt">종료일</param>
    /// <param name="curPage">현재페이지</param>
    /// <param name="pageRow">페이지크기</param>
    /// <returns>게시물 목록, 총 레코드 수</returns>
    public ListDataView RetrieveMemoAdminList(string progCode, int memoType, string search, string searchWord, string stDt, string spDt, int curPage, int pageRow)
    {
        ListDataView ldv = new ListDataView();

        SqlConnection conn = DbConnection.DbCon;
        SQLHelper.OpenConnection(conn);

        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "CountMemoAdminList";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = progCode;
        sqlCmd.Parameters.Add("@memoType", SqlDbType.Int).Value = memoType;
        sqlCmd.Parameters.Add("@search", SqlDbType.Char, 1).Value = search;
        sqlCmd.Parameters.Add("@searchWord", SqlDbType.VarChar, 50).Value = searchWord;
        sqlCmd.Parameters.Add("@stDt", SqlDbType.VarChar, 8).Value = stDt;
        sqlCmd.Parameters.Add("@spDt", SqlDbType.VarChar, 8).Value = spDt;

        ldv.TotalCount = SQLHelper.ExecuteScalarRetInt(conn, sqlCmd);

        sqlCmd.CommandText = "RetrieveMemoAdminList";

        sqlCmd.Parameters.Add("@curPage", SqlDbType.Int).Value = curPage;
        sqlCmd.Parameters.Add("@pageRow", SqlDbType.Int).Value = pageRow;

        ldv.DV = SQLHelper.FillDataAdapter(conn, sqlCmd, (curPage - 1) * pageRow, pageRow);

        SQLHelper.CloseConnection(conn);

        return ldv;
    }

    /// <summary>
    /// 관리자용 보낸 쪽지함 목록
    /// </summary>
    /// <param name="progCode">프로그램코드</param>
    /// <param name="curPage">현재페이지</param>
    /// <param name="pageRow">페이지크기</param>
    /// <returns>보낸 쪽지함 목록, 총 레코드 수</returns>
    public ListDataView RetrieveAdminNoteList(string progCode, int curPage, int pageRow)
    {
        ListDataView ldv = new ListDataView();

        //			SqlConnection conn = DbConnection.DbCon;
        SqlConnection conn = DbConnection.DbCon;
        SQLHelper.OpenConnection(conn);

        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "CountAdminNoteList";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = progCode;

        ldv.TotalCount = SQLHelper.ExecuteScalarRetInt(conn, sqlCmd);

        sqlCmd.CommandText = "RetrieveAdminNoteList";

        sqlCmd.Parameters.Add("@curPage", SqlDbType.Int).Value = curPage;
        sqlCmd.Parameters.Add("@pageRow", SqlDbType.Int).Value = pageRow;

        ldv.DV = SQLHelper.FillDataAdapter(conn, sqlCmd, (curPage - 1) * pageRow, pageRow);

        SQLHelper.CloseConnection(conn);

        return ldv;
    }

    /// <summary>
    /// 사용자 받은 쪽지함 또는 쪽지 보관함 목록
    /// </summary>
    /// <param name="uno">사용자번호</param>
    /// <param name="isKeeping">보관여부(Y:보관함, N:쪽지함)</param>
    /// <param name="curPage">현재페이지</param>
    /// <param name="pageRow">페이지크기</param>
    /// <returns>쪽지함 목록</returns>
    public ListDataView RetrieveUserNoteList(int uno, string isKeeping, int curPage, int pageRow)
    {
        ListDataView ldv = new ListDataView();

        //			SqlConnection conn = DbConnection.DbCon;
        SqlConnection conn = DbConnection.DbCon;
        SQLHelper.OpenConnection(conn);

        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "CountUserNoteList";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@uno", SqlDbType.Int).Value = uno;
        sqlCmd.Parameters.Add("@isKeeping", SqlDbType.Char, 1).Value = isKeeping;

        ldv.TotalCount = SQLHelper.ExecuteScalarRetInt(conn, sqlCmd);

        sqlCmd.CommandText = "RetrieveUserNoteList";

        sqlCmd.Parameters.Add("@curPage", SqlDbType.Int).Value = curPage;
        sqlCmd.Parameters.Add("@pageRow", SqlDbType.Int).Value = pageRow;

        ldv.DV = SQLHelper.FillDataAdapter(conn, sqlCmd, (curPage - 1) * pageRow, pageRow);

        SQLHelper.CloseConnection(conn);

        return ldv;
    }

    /// <summary>
    /// 사용자 보낸 쪽지함 목록
    /// </summary>
    /// <param name="uno">사용자번호</param>
    /// <param name="curPage">현재페이지</param>
    /// <param name="pageRow">페이지크기</param>
    /// <returns>보낸 쪽지함 목록</returns>
    public ListDataView RetrieveUserReplyNoteList(int uno, int curPage, int pageRow)
    {
        ListDataView ldv = new ListDataView();

        //			SqlConnection conn = DbConnection.DbCon;
        SqlConnection conn = DbConnection.DbCon;
        SQLHelper.OpenConnection(conn);

        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "CountUserReplyNoteList";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@uno", SqlDbType.Int).Value = uno;

        ldv.TotalCount = SQLHelper.ExecuteScalarRetInt(conn, sqlCmd);

        sqlCmd.CommandText = "RetrieveUserReplyNoteList";

        sqlCmd.Parameters.Add("@curPage", SqlDbType.Int).Value = curPage;
        sqlCmd.Parameters.Add("@pageRow", SqlDbType.Int).Value = pageRow;

        ldv.DV = SQLHelper.FillDataAdapter(conn, sqlCmd, (curPage - 1) * pageRow, pageRow);

        SQLHelper.CloseConnection(conn);

        return ldv;
    }



    /// <summary>
    /// 사용자 쪽지 보관함으로 이동
    /// </summary>
    /// <param name="seqIDs">이동할 쪽지ID들</param>
    /// <returns>성공여부</returns>
    public bool MoveUserNoteInfo(string seqIDs)
    {
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "MoveUserNoteInfo";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@seqIDs", SqlDbType.VarChar, 1000).Value = seqIDs;

        return SQLHelper.ExecuteNonQuery(sqlCmd);
    }

    /// <summary>
    /// 사용자 쪽지 삭제
    /// </summary>
    /// <param name="seqIDs">삭제할 쪽지ID들</param>
    /// <returns>성공여부</returns>
    public bool DeleteUserNoteInfo(string seqIDs)
    {
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "DeleteUserNoteInfo";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@seqIDs", SqlDbType.VarChar, 1000).Value = seqIDs;

        return SQLHelper.ExecuteNonQuery(sqlCmd);
    }

    /// <summary>
    /// 라디오 프로그램 정보
    /// </summary>
    /// <param name="progCode">프로그램코드</param>
    /// <returns>프로그램 정보</returns>
    public RadioProgramInfo RetrieveProgramInfo(string progCode)
    {
        RadioProgramInfo prog = new RadioProgramInfo();

        SqlConnection conn = DbConnection.DbCon;
        SQLHelper.OpenConnection(conn);


        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "music_RetrieveProgramInfo";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = progCode;

        SqlDataReader reader = SQLHelper.ExecuteReader(conn, sqlCmd);

        if (reader.Read())
        {
            prog.progCode = progCode;
            prog.subClassCode = reader["SUBCLASS_CODE"].ToString();
            prog.progTitle = reader["prog_title"].ToString();
            prog.progImage = reader["prog_image"].ToString();
        }

        reader.Close();

        SQLHelper.CloseConnection(conn);


        return prog;
    }



    /// <summary>
    /// 쪽지 내용 보기
    /// </summary>
    /// <param name="noteID">쪽지ID</param>
    /// <returns>쪽지내용</returns>
    public NoteInfo RetrieveUserNoteInfo(int noteID)
    {
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "RetrieveUserNoteInfo";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@noteID", SqlDbType.Int).Value = noteID;

        NoteInfo note = new NoteInfo();

        SqlDataReader reader = SQLHelper.ExecuteReader(sqlCmd);

        if (reader.Read())
        {
            note.noteID = noteID;
            note.progCode = reader["ProgCode"].ToString();
            note.progTitle = reader["Prog_Title"].ToString();
            note.comment = reader["comment"].ToString();
            note.progImage = reader["ProgImg"].ToString();
            note.rgDt = (DateTime)reader["rgDt"];
        }

        reader.Close();

        return note;
    }



    /// <summary>
    /// 관리자 보낸쪽지 삭제
    /// </summary>
    /// <param name="noteIDs">쪽지ID들</param>
    /// <returns>성공여부</returns>
    public bool DeleteAdminNoteInfo(string noteIDs)
    {
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "DeleteAdminNoteInfo";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@noteIDs", SqlDbType.VarChar, 1000).Value = noteIDs;

        return SQLHelper.ExecuteNonQuery(sqlCmd);
    }



    /// <summary>
    /// 미니 메세지 게시판 정보 보기
    /// </summary>
    /// <param name="progCode">프로그램코드</param>
    /// <returns>게시판 정보</returns>
    public MemoMstInfo RetrieveMemoMstInfo(string progCode)
    {
        MemoMstInfo mstInfo = new MemoMstInfo();

        try
        {
            SqlConnection conn = DbConnection.DbCon;
            SQLHelper.OpenConnection(conn);

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "dbowr_select.RetrieveMemoMstInfo";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = progCode;



            SqlDataReader reader = SQLHelper.ExecuteReader(conn, sqlCmd);

            if (reader.Read())
            {
                mstInfo.progCode = progCode;
                mstInfo.progTitle = reader["Prog_Title"].ToString();
                mstInfo.memoType = reader["memoType"].ToString();
                mstInfo.pageRow = (int)reader["pageRow"];
                mstInfo.tblColor = reader["TblColor"].ToString();
                mstInfo.showType = reader["ShowType"].ToString();
                mstInfo.topHtml = reader["TopHTML"].ToString(); //미니 게시판 주소 입력으로 사용하기 위해 추가함 (문현선. 2007.03.07)
            }

            reader.Close();

            SQLHelper.CloseConnection(conn);
        }
        catch (System.NullReferenceException exNull)
        {
            mstInfo = null;
        }

        return mstInfo;
    }



    /// <summary>
    /// 미니 메세지 게시판 정보 수정
    /// </summary>
    /// <param name="mstInfo">게시판 정보</param>
    /// <returns>성공여부</returns>
    public bool UpdateMemoMstInfo(MemoMstInfo mstInfo)
    {
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "UpdateMemoMstInfo";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = mstInfo.progCode;
        sqlCmd.Parameters.Add("@memoType", SqlDbType.Char, 1).Value = mstInfo.memoType;
        sqlCmd.Parameters.Add("@pageRow", SqlDbType.Int).Value = mstInfo.pageRow;
        sqlCmd.Parameters.Add("@tblColor", SqlDbType.VarChar, 7).Value = mstInfo.tblColor;
        sqlCmd.Parameters.Add("@showType", SqlDbType.Char, 1).Value = mstInfo.showType;
        sqlCmd.Parameters.Add("@topHTML", SqlDbType.Text).Value = mstInfo.topHtml;

        return SQLHelper.ExecuteNonQuery(sqlCmd);
    }


    /// <summary>
    /// 관리자인지 여부
    /// </summary>
    /// <param name="progCode">프로그램코드</param>
    /// <param name="userID">사용자ID</param>
    /// <returns>관리자 여부</returns>
    public bool IsAdminUser(string progCode, string userID)
    {
        if (userID == "") return false;

        string ADMINISTRATOR = IMBC.FW.Util.WebUtil.GetCookie("ADMINISTRATOR");

        userID = userID.ToLower();

        if (userID == "sunny177" || userID == "nawe00" || userID == "march1004" || userID == "mbctc0115" || userID == "imradio" || userID == "hestia76" || userID == "hoicey" || userID == "pumpkinp" || userID == "sosmini" || userID == "molko79" || userID == "imp2joo" || userID == "march1004")
        {
            return true;
        }
        else
        {
            return false;
        }

        if (ADMINISTRATOR == "MBC")
        {
            string PROGCODE = IMBC.FW.Util.WebUtil.GetCookie("PROGCODE");

            if (progCode.ToLower() == PROGCODE.ToLower())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }



    /// <summary>
    /// 관리자 인지 여부 (웹에서 바로 로그인했을경우 DB에서 체크)
    /// </summary>
    /// <param name="progCode">프로그램코드</param>
    /// <param name="userID">사용자ID</param>
    /// <returns>관리자여부</returns>
    public bool IsAdminUserDB(string progCode, string userID)
    {
        userID = userID.ToLower();

        if (this.IsAdminUser(progCode, userID))
        {

            IMBC.FW.Util.WebUtil.SetSession("IsAdmin_" + progCode, "TRUE");
            return true;
        }
        else if (userID.Trim() != "")
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "IsAdminUser";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = progCode;
            sqlCmd.Parameters.Add("@userID", SqlDbType.VarChar, 20).Value = userID;

            int count = SQLHelper.ExecuteScalarRetInt(sqlCmd);

            if (count > 0)
            {
                IMBC.FW.Util.WebUtil.SetSession("IsAdmin_" + progCode, "TRUE");
                return true;
            }
            else
            {
                IMBC.FW.Util.WebUtil.SetSession("IsAdmin_" + progCode, "FALSE");
                return false;
            }
        }
        else
        {
            return false;
        }
    }



    /// <summary>
    /// 불량 이용자 인지 여부
    /// </summary>
    /// <param name="progCode">프로그램코드</param>
    /// <param name="userID">사용자ID</param>
    /// <returns>불량 이용자 인지 여부</returns>
    public bool IsBadUser(string progCode, string userID)
    {
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "IsBadUser";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = progCode;
        sqlCmd.Parameters.Add("@userID", SqlDbType.VarChar, 20).Value = userID;

        int count = SQLHelper.ExecuteScalarRetInt(sqlCmd);

        if (count > 0)
            return true;
        else
            return false;
    }



    /// <summary>
    /// 미니 메시지 관리자, 불량이용자 목록
    /// </summary>
    /// <param name="progCode">프로그램코드</param>
    /// <returns>관리자, 불량이용자 목록</returns>
    public DataView RetrieveAdminBadUserList(string progCode)
    {
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "RetrieveAdminBadUserList";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = progCode;

        return SQLHelper.FillDataAdapter(sqlCmd);
    }



    /// <summary>
    /// 미니 메시지 관리자, 불량이용자 등록
    /// </summary>
    /// <param name="progCode">프로그램코드</param>
    /// <param name="userID">사용자ID</param>
    /// <param name="usrType">A:관리자, B:불량이용자</param>
    /// <returns>성공여부</returns>
    public bool RegisterAdminBadUserInfo(string progCode, string userID, string usrType)
    {
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "RegisterAdminBadUserInfo";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = progCode;
        sqlCmd.Parameters.Add("@userID", SqlDbType.VarChar, 20).Value = userID;
        sqlCmd.Parameters.Add("@usrType", SqlDbType.Char, 1).Value = usrType;

        return SQLHelper.ExecuteNonQuery(sqlCmd);
    }

    /// <summary>
    /// 미니 메세지 관리자, 불량이용자 등록
    /// </summary>
    /// <param name="progCode"></param>
    /// <param name="userID"></param>
    /// <param name="usrType"></param>
    /// <param name="comment"></param>
    /// <returns></returns>
    public bool RegisterAdminBadUserInfo(string progCode, string userID, string usrType, string comment)
    {
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "RegisterAdminBadUserInfo2";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = progCode;
        sqlCmd.Parameters.Add("@userID", SqlDbType.VarChar, 20).Value = userID;
        sqlCmd.Parameters.Add("@usrType", SqlDbType.Char, 1).Value = usrType;
        sqlCmd.Parameters.Add("@comment", SqlDbType.VarChar, 100).Value = comment;

        return SQLHelper.ExecuteNonQuery(sqlCmd);
    }
    /// <summary>
    /// 미니 메시지 관리자, 불량이용자 삭제
    /// </summary>
    /// <param name="progCode">프로그램코드</param>
    /// <param name="userID">사용자ID</param>
    /// <returns>성공여부</returns>
    public bool DeleteAdminBadUserInfo(string progCode, string userID)
    {
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "DeleteAdminBadUserInfo";
        sqlCmd.CommandType = CommandType.StoredProcedure;

        sqlCmd.Parameters.Add("@progCode", SqlDbType.VarChar, 13).Value = progCode;
        sqlCmd.Parameters.Add("@userID", SqlDbType.VarChar, 20).Value = userID;

        return SQLHelper.ExecuteNonQuery(sqlCmd);
    }

    /// <summary>
    /// 미니 전체 쪽지 최신 3개 목록
    /// </summary>
    /// <returns>전체 쪽지 목록</returns>
    public DataView RetrieveScriptLogList()
    {
        DataView dv = new DataView();
        try
        {
            SqlConnection conn = DbConnection.DbCon;
            SQLHelper.OpenConnection(conn);

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "RetrieveScriptLogList";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            dv = SQLHelper.FillDataAdapter(conn, sqlCmd);
            SQLHelper.CloseConnection(conn);
        }
        catch (System.NullReferenceException exNull)
        {
            dv = null;
        }
        return dv;
    }

    public ScheduleMBCList RetrieveScheduleListForMBC(string channel)
    {

        ScheduleMBCList scList = HttpContext.Current.Cache.Get("ScheduleListMor" + channel) as ScheduleMBCList;
        if (scList == null)
        {
            SqlConnection conn = DbConnection.DbConn(System.Web.Configuration.WebConfigurationManager.AppSettings["AdamConnectString"]);
            SQLHelper.OpenConnection(conn);

            scList = new ScheduleMBCList();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "mini@USP_Schedule_MiniMornitering";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@channel", SqlDbType.VarChar, 4).Value = channel;
            SqlDataReader reader = SQLHelper.ExecuteReader(conn, sqlCmd);
            
            try
            {
                while (reader.Read())
                {
                    ScheduleMBC sc = new ScheduleMBC();
                    sc.Channel = reader["Channel"].ToString();
                    sc.BroadCastID = long.Parse(reader["BroadCastID"].ToString());
                    sc.GroupID = int.Parse(reader["ProgramGroupID"].ToString());
                    sc.ProgramTitle = reader["Title"].ToString();
                    sc.Picture = reader["HDPicture"].ToString();
                    sc.StartTime = reader["STARTTIME"].ToString();
                    sc.EndTime = reader["EndTime"].ToString();
                    sc.EndHour = reader["EndHour"].ToString();
                    sc.Day = reader["Day"].ToString();
                    scList.Add(sc);
                }
                HttpContext.Current.Cache.Insert("ScheduleListMor" + channel, scList, null, DateTime.Now.AddSeconds(60), TimeSpan.Zero);

            }
            catch { }
            finally
            {
                reader.Close();
                SQLHelper.CloseConnection(conn);
            }

        }
        return scList;
    }

    public string GetProgCode(long broadcastid, int groupid)
    {
        string ProgCode = HttpContext.Current.Cache.Get("ProgCode" + broadcastid.ToString() + groupid.ToString()) as string;
        if (ProgCode == null || ProgCode == "")
        {
            SqlConnection conn = DbConnection.DbConn(System.Web.Configuration.WebConfigurationManager.AppSettings["AdamConnectString"]);
            SQLHelper.OpenConnection(conn);

            SqlCommand sqlCmdAdam = new SqlCommand();
            sqlCmdAdam.CommandText = "mini@USP_TOUCHMATCHINGTABLE_SELECT2";
            sqlCmdAdam.CommandType = CommandType.StoredProcedure;
            sqlCmdAdam.Parameters.Add("@broadcastid", SqlDbType.BigInt).Value = broadcastid;
            sqlCmdAdam.Parameters.Add("@groupid", SqlDbType.Int).Value = groupid;
            SqlDataReader readerAdam = SQLHelper.ExecuteReader(conn, sqlCmdAdam);
            try
            {
                if (readerAdam.Read())
                {
                    ProgCode = readerAdam["prog_code"].ToString();
                    HttpContext.Current.Cache.Insert("ProgCode" + broadcastid.ToString() + groupid.ToString(), ProgCode, null,  DateTime.Now.AddSeconds(100), TimeSpan.Zero);
                }
            }
            catch
            {
            }
            finally {
                readerAdam.Close();
                SQLHelper.CloseConnection(conn);
            }


        }
        return ProgCode;
    }
}