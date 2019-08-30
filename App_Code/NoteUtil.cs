using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// NoteUtil의 요약 설명입니다.
/// </summary>
public class NoteUtil
{
    public static string SetNavigator(int curPage, int totalRecord, int pageRow, string pagingURL)
    {
        string txtFirst = ""; //"<img src='/UserNote/images/bt/nav_prev_.gif' border='0' align='absmiddle'>";
        string txtPrev = "<img src='http://img.imbc.com/mini/UserNote/images/bt/nav_prev.gif' border='0' align='absmiddle'>";
        string txtNext = "<img src='http://img.imbc.com/mini/UserNote/images/bt/nav_next.gif' border='0' align='absmiddle'>";
        string txtLast = ""; //"<img src='/UserNote/images/bt/nav_next_.gif' border='0' align='absmiddle'>";

        return IMBC.FW.Util.WebUtil.SetNavigator(curPage, totalRecord, pageRow, pagingURL, txtFirst, txtFirst, txtPrev, txtPrev, txtNext, txtNext, txtLast, txtLast);
    }

    public static string SetNavigatorBlank(int curPage, int totalRecord, int pageRow, string pagingURL)
    {
        string txtFirst = string.Empty;
        string txtPrev = string.Empty;
        string txtNext = string.Empty;
        string txtLast = string.Empty;

        return IMBC.FW.Util.WebUtil.SetNavigatorBlank(curPage, totalRecord, pageRow, pagingURL);
    }


    public static string BoldSearchWord(string content, string searchWord)
    {
        if (searchWord.Length == 0)
            return content;

        return content.Replace(searchWord, "<font color='#0064A1'><b>" + searchWord + "</b></font>");
    }


    public static string GetProgImg(string progCode, string progImage)
    {
        return "http://mini1.imbc.com/manager/upload/" + progCode + "/" + progImage;
    }


    public static string GetParentCode(string progCode)
    {
        progCode = progCode.ToUpper();

        switch (progCode)
        {
            case "STFM000001130":
                return "RASFM150";
                break;
            case "STFM000001050":
                return "RASFM230";
                break;
            case "STFM000001210":
                return "RASFM170";
                break;
            case "STFM000001240":
                return "STFM000000950";
                break;
            case "STFM000001450":
                return "STFM000001430";
                break;
            case "STFM000000960":
                return "STFM000001370";
                break;
            case "STFM000001090":
                return "RASFM130";
                break;
            case "STFM000001290":
                return "RASFM210";
                break;
            case "STFM000001190":
                return "STFM000000990";
                break;
            case "STFM000001460":
                return "RASFM360";
                break;
            case "STFM000001350":
                return "STFM000001020";
                break;
            case "STFM000001490":
                return "RASFM360";
                break;
            case "STFM000001160":
                return "STFM000001040";
                break;
            case "RDMB000000320":
                return "FM4U000001070";
                break;
            case "RDMB000000330":
                return "RAMFM260";
                break;
            case "RDMB000000300":
                return "FM4U000001130";
                break;
            case "RDMB000000410":
                return "RAMFM300";
                break;
            case "RDMB000000340":
                return "RAMFM270";
                break;
            case "RDMB000000443":
                return "FM4U000001227";
                break;
            case "STFM000001717":
                return "STFM000001715";
                break;
            case "RDMB000000442":
                return "FM4U000001226";
                break;
            case "RDMB000000456": //CHANNEL M 추가
                return "CHAM000000000";
                break;
            case "RDMB000000457":
                return "CHAM000000000";
                break;
            case "RDMB000000458":
                return "CHAM000000000";
                break;
            case "RDMB000000459":
                return "CHAM000000000";
                break;
            case "RDMB000000460":
                return "CHAM000000000";
                break;
            case "RDMB000000461":
                return "CHAM000000000";
                break;
            default:
                return progCode;
                break;
        }
      			
    }
}