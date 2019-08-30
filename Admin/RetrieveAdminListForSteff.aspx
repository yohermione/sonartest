<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RetrieveAdminListForSteff.aspx.cs" Inherits="Admin_RetrieveAdminListForSteff" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RetrieveAdminListForSteff</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/UserNote/css/style.css" rel="stylesheet" type="text/css">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:DropDownList id="usrType" runat="server">
				<asp:ListItem value="B">불량이용자</asp:ListItem>
			</asp:DropDownList>
			아이디 :<asp:TextBox id="userID" runat="server"></asp:TextBox>
			설명 :<asp:TextBox ID="comment" Runat="server"></asp:TextBox>
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