<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RetrieveMemoList.aspx.cs" Inherits="RetrieveMemoList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RetrieveMemoList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="http://www.imbc.com/common/css/style2005.css" type="text/css" rel="stylesheet">
		<style>
			.b_tx11b                  {font-size:11px; color:#000000; font-weight:bold; line-height:10pt;}
			a.b_tx11b:link          {font-size:11px; font-family:verdana; color:#000000; font-weight:bold;}
			a.b_tx11b:visited	     {font-size:11px; font-family:verdana; color:#000000; font-weight:bold;}
			a.b_tx11b :active      {font-size:11px; font-family:verdana; color:#000000; font-weight:bold;}
			a.b_tx11b:hover        {font-size:11px; font-family:verdana; color:#E93B72; font-weight:bold;}
			.p_tx11b                  {font-size:11px; font-family:verdana; color:#E93B72; font-weight:bold;}
			a.p_tx11b:link           {font-size:11px; font-family:verdana; color:#E93B72; font-weight:bold;}
			a.p_tx11b:visited	      {font-size:11px; font-family:verdana; color:#E93B72; font-weight:bold;}
			a.p_tx11b:active        {font-size:11px; font-family:verdana; color:#E93B72; font-weight:bold;}
			a.p_tx11b:hover         {font-size:11px; font-family:verdana; color:#E93B72; font-weight:bold;}
		</style>
		<script language="javascript">
		    var sInitColor = null;
		    function callColorDlg() {

		        if (sInitColor == null)
		        //display color dialog box
		            var sColor = document.all.dlgHelper.ChooseColorDlg();
		        else
		            var sColor = document.all.dlgHelper.ChooseColorDlg(sInitColor);
		        //change decimal to hex
		        sColor = sColor.toString(16);

		        //add extra zeroes if hex number is less than 6 digits
		        if (sColor.length < 6) {
		            var sTempString = "000000".substring(0, 6 - sColor.length);
		            sColor = sTempString.concat(sColor);
		        }
		        //change color of the text in the div
		        document.all.hdnFontColor.value = "#" + sColor.toUpperCase();
		        sInitColor = sColor;
		        document.all.txtComment.style.color = sColor;
		    }

		    function doSave() {
		        if (document.all.txtComment.value == "") {
		            alert("내용을 입력하세요.");
		            return false;
		        }
		        else {
		            var look4 = document.all.txtComment.value;
		            var speChar = commentCheck(look4);
		            if (speChar == true) {
		                alert('비속어를 입력할 수 없습니다');
		                return false;
		            }

		            if (commentNumCheck(look4)) {
		                alert('입력하신 숫자는 개인정보에 해당하는 숫자이므로 입력이 불가능합니다.');
		                return false;
		            }
		        }
		        /*			if (commentCheck(document.all.txtComment.value)) 
		        {
		        alert('비속어를 입력할 수 없습니다');
		        return false;
		        }
		        */
		        __doPostBack('btnSave', '');
		    }

		    function commentNumCheck(txtCom) {
		        if (/\d{3}-\d{4}-\d{4}/.test(txtCom)) {
		            return true;
		        }
		        else if (/\d{11}/.test(txtCom)) {
		            return true;
		        }
		        else if (/\d{10}/.test(txtCom)) {
		            return true;
		        }
		        else if (/\d{2}-\d{3}-\d{4}/.test(txtCom)) {
		            return true;
		        }
		        else if (/\d{2}-\d{4}-\d{4}/.test(txtCom)) {
		            return true;
		        }
		        else if (/\d{3}-\d{3}-\d{4}/.test(txtCom)) {
		            return true;
		        }
		        else if (/\d{3}\.\d{3}\.\d{4}/.test(txtCom)) {
		            return true;
		        }
		        else if (/\d{3}\.\d{4}\.\d{4}/.test(txtCom)) {
		            return true;
		        }
		        else if (/\d{2}\.\d{3}\.\d{4}/.test(txtCom)) {
		            return true;
		        }
		        else if (/\d{2}\.\d{4}\.\d{4}/.test(txtCom)) {
		            return true;
		        }
		        else {
		            return false;
		        }
		    }

		    function commentCheck(txtCom) {
		        xspecialChar = new Array('씨발', '지랄', '니미', '존나', 'fuck', '새끼', '디질', '꺼져', 'ㅅㅂ년', '시발', '미친', '또라이', '병신', '좃불', '똘아이', '씨방새', '쌕끼', '섹휘', '졎', '색히', '개새끼', '미친새끼', '쉬팔', '쪼다새끼들', '또라이', '씹새', '개잡년', '븅신', '걸래년', '걸레년', '쌍뇬', '섹스', 'sex');
		        for (var i = 0; i < xspecialChar.length; i++) {
		            if (txtCom.indexOf(xspecialChar[i]) >= 0) {
		                return true;
		            }
		        }
		        return false;
		    }

		    function fIdentity() {
		        var turl;

		        try {
		            turl = top.location.href;
		            document.cookie = "IMBCURL=" + escape(turl) + "; path=/; domain=imbc.com;";
		            top.location.href = "https://member.imbc.com/Login/Identity.aspx?PageType=M";

		        } catch (E) {
		            document.domain = 'imbc.com'
		            turl = self.location.href;
		            document.cookie = "IMBCURL=" + escape(turl) + "; path=/; domain=imbc.com;";
		            window.open("https://member.imbc.com/Login/Identity.aspx?PageType=I", "IdentityPop", "resizable=no,status=no,toolbar=no,menubar=no,location=no,width=587,height=662");
		        }
		    }

		    function CharToByte0() {
		        var one;
		        var count;
		        var sss;
		        var tstr;
		        var c;
		        count = 0;
		        leng = document.all.txtComment.value.length;
		        for (c = 0; c < leng; c++) {
		            one = document.all.txtComment.value.charAt(c);
		            if (escape(one).length > 4) {
		                count += 2;
		            } else if (one == "\r") {
		                count++;
		            } else {
		                count++;
		            }
		        }
		        if (count > 120) {
		            tstr = "";
		            alert('내용이 120바이트 이상이면 등록할수 없습니다');
		            for (c = 0; c < leng - 1; c++) {
		                tstr = tstr + document.all.txtComment.value.charAt(c);
		            }
		            document.all.txtComment.value = tstr;
		            return;
		        }

		        document.all.byte.innerText = count;
		    }

		    function doSearch() {

		        var sch = document.all.search.options[document.all.search.selectedIndex].value;

		        location.href = "RetrieveMemoList.aspx?progCode=<%=progCode%>&search=" + sch + "&searchWord=" + document.all.searchWord.value;
		    }

		    function doList() {
		        location.href = "RetrieveMemoList.aspx?progCode=<%=progCode%>";
		    }

		    function confirmDelete() {
		        return confirm("삭제하시겠습니까?");
		    }
		</script>
	</HEAD>
	<body bgcolor="transparent" allowTransparency="true" >
		
		<form id="Form1" method="post" runat="server">		
			<input id="hdnFontColor" type="hidden" runat="server" value="#000000">
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td>
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td width="50"><IMG height="27" src="http://img.imbc.com/mini/UserNote/images/mini_memo_logo.jpg" width="46"></td>
								<td valign="bottom"><font color="#8b67c4"><b><asp:Label id="progTitle" runat="server"></asp:Label></b></font>
								<!--a href="http://www.imbc.com/broad/radio/minimbc/new_notice/1568159_20381.html"><img src="/UserNote/images/mini_p2p_icon.gif" border="0"></a-->
								</td>
								<td align="right">&nbsp;<a onclick="javascript:window.open('http://www.imbc.com/common/html/notice.html', 'notice', 'width=670,height=750');" style="cursor:pointer"><img src="http://img.imbc.com/broad/radio/minimbc/images3/mini_notice_icon.jpg" border="0"></a>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center"><br>
						<table cellspacing="0" cellpadding="0" width="97%" border="0">
							<tr>
								<td width="35"><A onfocus="this.blur();" href="javascript:callColorDlg();"><IMG height="19" alt="컬러선택" src="http://img.imbc.com/mini/UserNote/images/mini_memo_color.jpg" width="27" border="0"></A></td>
								<td class="small"><asp:textbox id="txtComment" style="BORDER-RIGHT: #c7acdf 1px solid; BORDER-TOP: #c7acdf 1px solid; BORDER-LEFT: #c7acdf 1px solid; COLOR: #000000; BORDER-BOTTOM: #c7acdf 1px solid; FONT-FAMILY: 돋움; BACKGROUND-COLOR: #f5f5f5"
										runat="server" width="300" columns="40" onchange="CharToByte0();" onkeyup="CharToByte0();"></asp:textbox>
									<img id="imgSave" runat="server" src="http://img.imbc.com/mini/UserNote/images/mini_memo_ok.jpg" width="76" height="26"
										align="absMiddle" style="CURSOR:hand">
									<asp:LinkButton id="btnSave" runat="server" OnClick="btnSave_Click"></asp:LinkButton>
									[<span id="byte">0</span>/120 Byte]
									<script language="javascript">
									function document.all.txtComment::onkeydown(){
										if (event.keyCode == 13) {
											doSave();
											return false;
										}
									}
									</script>
									
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<asp:PlaceHolder ID="pnlList" Runat="server">
				<tr>
					<td align="center"><br>
						<table cellspacing="0" cellpadding="0" width="97%" border="0">
							<tr>
								<td width="2"><!--IMG height="25" src="/UserNote/images/mini_memo_table1.gif" width="2"--></td>
								<td id="tblColor" runat="server" bgcolor="#ae91d5">
									<table cellspacing="3" cellpadding="0" width="100%" border="0">
										<tr align="center">
											<td width="50"><b><font color="#ffffff">no</font></b></td>
											<td width="30"><b><font color="#ffffff">종류</font></b></td>
											<td width="50"><b><font color="#ffffff">이름</font></b></td>
											<td><b><font color="#ffffff">내용</font></b></td>
											<td width="67"><b><font color="#ffffff">날짜</font></b></td>
										</tr>
									</table>
								</td>
								<td width="3"><!--IMG height="25" src="/UserNote/images/mini_memo_table2.gif" width="3"--></td>
							</tr>
						</table>
						<table cellspacing="3" cellpadding="0" width="97%" border="0">
							<asp:repeater id="listRepeater" runat="server" OnItemDataBound="listRepeater_ItemDataBound" OnItemCommand="listRepeater_ItemCommand">
								<ItemTemplate>
									<tr>
										<td width="50" align="center"><font color="#999999">
												<asp:Label id="seqID" runat="server"></asp:Label></font></td>
										<td width="30" align="center">
											<asp:Image id="imgType" runat="server"></asp:Image></td>
										<td width="50" align="center"><font color="#999999">
												<asp:Label id="userNm" runat="server"></asp:Label></font></td>
										<td style="word-break:break-all"><font color="#666666">
												<asp:Label id="comment" runat="server"></asp:Label></font>
											<asp:ImageButton id="btnDelete" runat="server" imageurl="http://img.imbc.com/mini/UserNote/images/bt/review_x.gif" imagealign="AbsMiddle"></asp:ImageButton>
										</td>
										<td width="70" align="center">
											<asp:Label id="rgDt" runat="server"></asp:Label></td>
									</tr>
									<tr>
										<td colspan="6" height="1" align="center" bgcolor="#e6e6e6"><img src="http://img.imbc.com/mini/UserNote/images/x.gif" width="1" height="1"></td>
									</tr>
								</ItemTemplate>
							</asp:repeater></table>
						<table cellspacing="0" cellpadding="0" width="97%" border="0">
							<tr>
								<td id="tblColor2" runat="server" width="2" bgcolor="#ae91d5"><IMG height="2" src="http://img.imbc.com/mini/UserNote/images/x.gif" width="2"></td>
							</tr>
						</table>
						<table cellspacing="0" cellpadding="5" width="97%" border="0">
							<tr>
								<td align="center"><asp:label id="pageNavigator" runat="server"></asp:label></td>
							</tr>
						</table>
						<table cellspacing="0" cellpadding="5" width="97%" border="0">
							<tr>
								<td align="center">
									<select id="search" style="BORDER-RIGHT: #c7acdf 1px solid; BORDER-TOP: #c7acdf 1px solid; BORDER-LEFT: #c7acdf 1px solid; COLOR: #000000; BORDER-BOTTOM: #c7acdf 1px solid; FONT-FAMILY: 돋움; BACKGROUND-COLOR: #f5f5f5">
										<option value="1">내용</option>
										<option value="2" selected>이름</option>
									</select>
									<input id="searchWord" type="text" name="searchWord" style="BORDER-RIGHT: #c7acdf 1px solid; BORDER-TOP: #c7acdf 1px solid; BORDER-LEFT: #c7acdf 1px solid; COLOR: #000000; BORDER-BOTTOM: #c7acdf 1px solid; FONT-FAMILY: 돋움; BACKGROUND-COLOR: #f5f5f5"> <img src="http://img.imbc.com/mini/UserNote/images/mini_memo_search2.jpg" width="44" height="21" align="absMiddle" alt="검색"
										style="CURSOR:hand" onclick="doSearch();">
									<img src="http://img.imbc.com/mini/UserNote/images/bt_list.gif" style="cursor:hand" onclick="doList();" align="absmiddle">
									<script language="javascript">
									function document.all.searchWord::onkeydown(){
										if (event.keyCode == 13) {
											doSearch();
											return false;
										}
									}
									</script>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				</asp:PlaceHolder>
                <asp:PlaceHolder ID="pnlNoMsgList" runat="server" Visible="false">
                    <tr>
                        <td align="center">
                            본 프로그램은 mini메시지 작성만 가능합니다
                        </td>
                    </tr>
                </asp:PlaceHolder>
			</table>
			<OBJECT id="dlgHelper" height="0px" width="0px" classid="clsid:3050f819-98b5-11cf-bb82-00aa00bdce0b"
				viewastext>
			</OBJECT>
		</form>
	</body>
</HTML>
