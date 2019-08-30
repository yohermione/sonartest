using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// NoteDTO의 요약 설명입니다.
/// </summary>
public class NoteUser
{
    public int uno;
    public string userID;
    public string userNm;
}


public class RadioProgramInfo
{
    public string progCode;
    public string subClassCode;
    public string progTitle;
    public string progImage;
}


public class NoteInfo
{
    public int noteID;
    public string progCode;
    public string progTitle;
    public string comment;
    public string progImage;
    public DateTime rgDt;
}


public class MemoMstInfo
{
    public string progCode;
    public string progTitle;
    public string memoType;
    public int pageRow;
    public string tblColor;
    public string showType;
    public string topHtml;
    public string bottomHtml;
    public string adminID;
}

public class ScheduleMBC 
{
    public string Channel;
    public long BroadCastID;
    public int GroupID;
    public string StartTime;
    public string EndTime;
    public string EndHour;
    public string ProgramTitle;
    public string Picture;
    public string Day;
}
public class ScheduleMBCList : List<ScheduleMBC>
{
    public int TotalCount;
}
