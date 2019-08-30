using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;
/// <summary>
/// WebUtil의 요약 설명입니다.
/// </summary>
public class WebUtil
{
	public WebUtil()
	{
		//
		// TODO: 여기에 생성자 논리를 추가합니다.
		//
	}


    public static string[] blacklist = { "--", ";--", ";", "/*", "*/", "@@", "@", "char", "nchar", "varchar", "nvarchar", "alter", "begin", "cast", "create", "cursor", "declare", "delete", "drop", "end", "exec", "execute", "fetch", "insert", "kill", "open", "select", "sys", "sysobjects", "syscolumns", "table", "update" };

    public static string replaceSQLInjections(string s)
    {
        string str = s;

        for (int i = 0; i < blacklist.Length; i++)
        {
            if (blacklist[i].IndexOf(s) >= 0)
            {
                str = s.Replace(blacklist[i], "");
                break;
            }
        }
        return str;
    }

    /// <summary>
    /// 성별 구하기
    /// </summary>
    /// <param name="SSN">주민등록번호 (7자리 이상)</param>
    /// <returns>남:M, 여:F</returns>
    public static string GetSex(string SSN)
    {
        string rtnValue = "M";
        if (SSN.Length > 6)
        {
            rtnValue = (SSN.Substring(6, 1) == "1" || SSN.Substring(6, 1) == "3") ? "M" : "F";
        }
        return rtnValue;
    }

    /// <summary>
    /// 나이 구하기
    /// </summary>
    /// <param name="SSN">주민등록번호</param>
    /// <returns>나이</returns>
    public static int GetAge(string SSN)
    {
        string BirthYear = SSN.Substring(0, 2);

        if (BirthYear != "")
        {
            DateTime objDateTimeNow = DateTime.Now;

            int NowYear = objDateTimeNow.Year;

            int Age = 0;

            if (Convert.ToInt32(BirthYear) > 10)
            {
                Age = 100 - Convert.ToInt32(BirthYear) + Convert.ToInt32(NowYear.ToString().Substring(2, 2));
            }
            else
            {
                Age = Convert.ToInt32(NowYear.ToString().Substring(2, 2)) - Convert.ToInt32(BirthYear);
            }

            return Age;
        }
        else
        {
            return -1;
        }
    }

    /// <summary>
    /// 년 월 문자열 리스트를 반환합니다.
    /// </summary>
    /// <returns></returns>
    public static ArrayList GetDateList(int iGubun)
    {
        ArrayList arrList = new ArrayList();

        string strView = string.Empty;
        if (iGubun == 3)
            arrList.Add("전체");

        for (int i = 2006; i < DateTime.Now.Year + 1; i++)
        {
            string strYear = string.Empty;
            strYear = i.ToString() + "년";
            if (iGubun == 1 || iGubun == 3)
            {
                for (int j = 1; j < 13; j++)
                {
                    strView += strYear + j.ToString() + "월";
                    arrList.Add(strView);
                    strView = "";
                }
            }
            else
            {
                arrList.Add(strYear);
            }
        }
        return arrList;
    }

    /// <summary>
    /// 국가 이름을 반환한다.
    /// </summary>
    /// <param name="CountryCode"></param>
    /// <returns></returns>
    public static string GetCountryName(string CountryCode)
    {
        string[] countryCode = 
								{
									"AP","EU","AD","AE","AF","AG","AI","AL","AM","AN","AO","AQ","AR","AS","AT","AU","AW","AZ","BA","BB","BD","BE","BF","BG","BH","BI","BJ","BM","BN","BO","BR","BS","BT","BV","BW","BY","BZ","CA","CC","CD","CF","CG","CH","CI","CK","CL","CM","CN","CO","CR","CU","CV","CX","CY","CZ","DE","DJ","DK","DM","DO","DZ",
									"EC","EE","EG","EH","ER","ES","ET","FI","FJ","FK","FM","FO","FR","FX","GA","GB","GD","GE","GF","GH","GI","GL","GM","GN","GP","GQ","GR","GS","GT","GU","GW","GY","HK","HM","HN","HR","HT","HU","ID","IE","IL","IN","IO","IQ","IR","IS","IT","JM","JO","JP","KE","KG","KH","KI","KM","KN","KP","KR","KW","KY","KZ",
									"LA","LB","LC","LI","LK","LR","LS","LT","LU","LV","LY","MA","MC","MD","MG","MH","MK","ML","MM","MN","MO","MP","MQ","MR","MS","MT","MU","MV","MW","MX","MY","MZ","NA","NC","NE","NF","NG","NI","NL","NO","NP","NR","NU","NZ","OM","PA","PE","PF","PG","PH","PK","PL","PM","PN","PR","PS","PT","PW","PY","QA",
									"RE","RO","RU","RW","SA","SB","SC","SD","SE","SG","SH","SI","SJ","SK","SL","SM","SN","SO","SR","ST","SV","SY","SZ","TC","TD","TF","TG","TH","TJ","TK","TM","TN","TO","TP","TR","TT","TV","TW","TZ","UA","UG","UM","US","UY","UZ","VA","VC","VE","VG","VI","VN","VU","WF","WS","YE","YT","CS","ZA","ZM","ZR","ZW","A1","A2"};
        string[] countryName = 
								{
									"Asia/Pacific Region","Europe","Andorra","United Arab Emirates","Afghanistan","Antigua and Barbuda","Anguilla","Albania","Armenia","Netherlands Antilles","Angola","Antarctica","Argentina","American Samoa","Austria","Australia","Aruba","Azerbaijan","Bosnia and Herzegovina","Barbados","Bangladesh","Belgium",
									"Burkina Faso","Bulgaria","Bahrain","Burundi","Benin","Bermuda","Brunei Darussalam","Bolivia","Brazil","Bahamas","Bhutan","Bouvet Island","Botswana","Belarus","Belize","Canada","Cocos (Keeling) Islands","Congo, The Democratic Republic of the","Central African Republic","Congo","Switzerland","Cote D'Ivoire",
									"Cook Islands","Chile","Cameroon","China","Colombia","Costa Rica","Cuba","Cape Verde","Christmas Island","Cyprus","Czech Republic","Germany","Djibouti","Denmark","Dominica","Dominican Republic","Algeria","Ecuador","Estonia","Egypt","Western Sahara","Eritrea","Spain","Ethiopia","Finland","Fiji","Falkland Islands (Malvinas)",
									"Micronesia, Federated States of","Faroe Islands","France","France, Metropolitan","Gabon","United Kingdom","Grenada","Georgia","French Guiana","Ghana","Gibraltar","Greenland","Gambia","Guinea","Guadeloupe","Equatorial Guinea","Greece","South Georgia and the South Sandwich Islands","Guatemala","Guam","Guinea-Bissau","Guyana",
									"Hong Kong","Heard Island and McDonald Islands","Honduras","Croatia","Haiti","Hungary","Indonesia","Ireland","Israel","India","British Indian Ocean Territory","Iraq","Iran, Islamic Republic of","Iceland","Italy","Jamaica","Jordan","Japan","Kenya","Kyrgyzstan","Cambodia","Kiribati","Comoros","Saint Kitts and Nevis",
									"Korea, Democratic People's Republic of","Korea, Republic of","Kuwait","Cayman Islands","Kazakstan","Lao People's Democratic Republic","Lebanon","Saint Lucia","Liechtenstein","Sri Lanka","Liberia","Lesotho","Lithuania","Luxembourg","Latvia","Libyan Arab Jamahiriya","Morocco","Monaco","Moldova, Republic of","Madagascar",
									"Marshall Islands","Macedonia","Mali","Myanmar","Mongolia","Macau","Northern Mariana Islands","Martinique","Mauritania","Montserrat","Malta","Mauritius","Maldives","Malawi","Mexico","Malaysia","Mozambique","Namibia","New Caledonia","Niger","Norfolk Island","Nigeria","Nicaragua","Netherlands",
									"Norway","Nepal","Nauru","Niue","New Zealand","Oman","Panama","Peru","French Polynesia","Papua New Guinea","Philippines","Pakistan","Poland","Saint Pierre and Miquelon","Pitcairn Islands","Puerto Rico","Palestinian Territory","Portugal","Palau","Paraguay","Qatar","Reunion","Romania","Russian Federation","Rwanda","Saudi Arabia",
									"Solomon Islands","Seychelles","Sudan","Sweden","Singapore","Saint Helena","Slovenia","Svalbard and Jan Mayen","Slovakia","Sierra Leone","San Marino","Senegal","Somalia","Suriname","Sao Tome and Principe","El Salvador","Syrian Arab Republic","Swaziland","Turks and Caicos Islands","Chad","French Southern Territories","Togo",
									"Thailand","Tajikistan","Tokelau","Turkmenistan","Tunisia","Tonga","East Timor","Turkey","Trinidad and Tobago","Tuvalu","Taiwan","Tanzania, United Republic of","Ukraine","Uganda","United States Minor Outlying Islands","United States","Uruguay","Uzbekistan","Holy See (Vatican City State)","Saint Vincent and the Grenadines",
									"Venezuela","Virgin Islands, British","Virgin Islands, U.S.","Vietnam","Vanuatu","Wallis and Futuna","Samoa","Yemen","Mayotte","Serbia and Montenegro","South Africa","Zambia","Zaire","Zimbabwe","Anonymous Proxy","Satellite Provider"};

        int iArr = 0;
        for (int i = 0; i < countryCode.Length; i++)
        {
            if (CountryCode == countryCode[i].ToString())
            {
                iArr = i;
            }
        }

        return countryName[iArr].ToString();
    }
}