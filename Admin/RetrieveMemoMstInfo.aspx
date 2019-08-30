<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RetrieveMemoMstInfo.aspx.cs" Inherits="Admin_RetrieveMemoMstInfo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RetrieveMemoMstInfo</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/UserNote/css/style.css" rel="stylesheet" type="text/css">
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
		        document.all.tblColor.style.backgroundColor = sColor;
		    }


		    function doSave() {
		        if (document.all.pageRow.value == "") {
		            alert("페이지별 게시물수를 입력하세요.");
		            document.all.pageRow.focus();
		        }
		        else if (confirm("게시판 환경을 저장하시겠습니까?")) {
		            __doPostBack('btnSave', '');
		        }
		    }
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div id="header">
				<ul>
					<li id="current">
						<a href="RetrieveMemoMstInfo.aspx?progCode=<%=progCode%>">환경설정</a>
					<li>
						<a href="RetrieveAdminList.aspx?progCode=<%=progCode%>">이용자 관리</a></li>
				</ul>
			</div>
			<br>
			<br>
			<input id="hdnFontColor" type="hidden" runat="server" name="hdnFontColor">
			<table width="600" cellpadding="0" cellspacing="3" border="0">
				<tr align="left">
					<td width="150">게시판 URL</td>
					<td><asp:Label id="memoUrl" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td colspan="2" height="1" align="center" bgcolor="#e6e6e6"><img src="http://img.imbc.com/mini/UserNote/images/x.gif" width="1" height="1"></td>
				</tr>
				<tr align="left">
					<td>메시지 유형</td>
					<td>
						<asp:RadioButton id="memoType1" runat="server" groupname="memoType" text="mini + 한줄톡"></asp:RadioButton>
						<asp:RadioButton id="memoType2" runat="server" groupname="memoType" text="mini 전용"></asp:RadioButton>
					</td>
				</tr>
				<tr>
					<td colspan="2" height="1" align="center" bgcolor="#e6e6e6"><img src="http://img.imbc.com/mini/UserNote/images/x.gif" width="1" height="1"></td>
				</tr>
				<tr align="left">
					<td>페이지별 게시물수</td>
					<td><asp:TextBox id="pageRow" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="2" height="1" align="center" bgcolor="#e6e6e6"><img src="http://img.imbc.com/mini/UserNote/images/x.gif" width="1" height="1"></td>
				</tr>
				<tr align="left">
					<td>테이블 색상</td>
					<td>
						<A onfocus="this.blur();" href="javascript:callColorDlg();"><IMG height="19" alt="컬러선택" src="http://img.imbc.com/mini/UserNote/images/mini_memo_color.jpg" width="27" border="0"></A>
						<asp:TextBox id="tblColor" runat="server" enabled="False"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td colspan="2" height="1" align="center" bgcolor="#e6e6e6"><img src="/UserNote/images/x.gif" width="1" height="1"></td>
				</tr>
				<tr align="left">
					<td>아이디/이름</td>
					<td>
						<asp:RadioButton id="showType1" runat="server" groupname="showType" text="아이디"></asp:RadioButton>
						<asp:RadioButton id="showType2" runat="server" groupname="showType" text="이름"></asp:RadioButton>
					</td>
				</tr>
				<tr>
					<td colspan="2" height="1" align="center" bgcolor="#e6e6e6"><img src="http://img.imbc.com/mini/UserNote/images/x.gif" width="1" height="1"></td>
				</tr>
				<tr align="left">
					<td>mini게시판 웹 주소</td>
					<td><asp:TextBox ID="txtMiniBoardURL" Runat="server" Width="400px"></asp:TextBox>
					<br>* 주소를 입력 안하면 웹에서 게시판을 사용 안하는 것으로 인식함.
					</td>
				</tr>
				<tr>
					<td colspan="2" align="right">
						<input type="button" value="저 장" onclick="doSave();"> <input type="button" value="취 소" onclick="history.back();">
					</td>
				</tr>
			</table>
			<asp:LinkButton id="btnSave" runat="server" OnClick="btnSave_Click"></asp:LinkButton>
			<OBJECT id="dlgHelper" height="0px" width="0px" classid="clsid:3050f819-98b5-11cf-bb82-00aa00bdce0b"
				viewastext>
			</OBJECT>
		</form>
	</body>
</HTML>
