<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RetrieveAdminList.aspx.cs" Inherits="Admin_RetrieveAdminList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<htm>
	<head>
		<title>RetrieveAdminList</title>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
		<link href="/UserNote/css/style.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<div id="header">
				<ul>
					<li ><a href="RetrieveMemoMstInfo.aspx?progCode=<%=progCode%>">환경설정</a></li>
					<li id="current"><a href="RetrieveAdminList.aspx?progCode=<%=progCode%>">이용자 관리</a></li>
				</ul>
			</div>
			<br>
			<br>
			<asp:DropDownList id="usrType" runat="server">
				<asp:ListItem value="B">불량이용자</asp:ListItem>
			</asp:DropDownList>
			<asp:TextBox id="userID" runat="server"></asp:TextBox>
			<asp:Button id="btnSave" runat="server" text="등 록" OnClick="btnSave_Click"></asp:Button><br>
			<br>
			<table cellpadding="0" cellspacing="3" width="600">
				<tr align="center" bgcolor="#ae91d5">
					<td><b><font color="#ffffff">번호</font></b></td>
					<td><b><font color="#ffffff">아이디</font></b></td>
					<td><b><font color="#ffffff">타입</font></b></td>
					<td><b><font color="#ffffff">등록일</font></b></td>
					<td><b><font color="#ffffff">삭제</font></b></td>
				</tr>
				<asp:Repeater id="listRepeater" runat="server" OnItemDataBound="listRepeater_ItemDataBound" OnItemCommand="listRepeater_ItemCommand">
					<ItemTemplate>
						<tr align="center">
							<td>
								<asp:Label id="num" runat="server"></asp:Label></td>
							<td>
								<asp:Label id="lblUserID" runat="server"></asp:Label></td>
							<td>
								<asp:Label id="lblUsrType" runat="server"></asp:Label></td>
							<td>
								<asp:Label id="lblRgDt" runat="server"></asp:Label></td>
							<td>
								<asp:Button id="btnDelete" runat="server" text="삭 제"></asp:Button></td>
						</tr>
						<tr>
							<td colspan="5" height="1" align="center" bgcolor="#e6e6e6"><img src="http://img.imbc.com/mini/UserNote/images/x.gif" width="1" height="1"></td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
			</table>
		</form>
	</body>
</HTML>
