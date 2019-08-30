<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterUserReplyNoteInfo.aspx.cs" Inherits="RegisterUserReplyNoteInfo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RegisterUserReplyNoteInfo</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/UserNote/css/style.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		    function doSend() {
		        if (confirm("쪽지를 보내시겠습니까?"))
		            __doPostBack('btnSend', '');
		    }
		</script>
	</HEAD>
	<body leftmargin="0" topmargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellspacing="0" cellpadding="0" border="0">
				<tr>
					<td style="PADDING-RIGHT: 20px; PADDING-LEFT: 20px; PADDING-BOTTOM: 20px; PADDING-TOP: 20px"
						valign="top" align="center">
						<!---------본문 시작--------->
						<table cellspacing="0" cellpadding="5" width="400" border="0">
							<tr>
								<td class="gray_b" colspan="2"><b><font color="#000000">보내는 사람 " <font color="#ff0033">
												<asp:Label id="userNm" runat="server"></asp:Label></font> "</font></b></td>
							</tr>
							<tr>
								<td bgcolor="#cccccc" colspan="2" height="1"></td>
							</tr>
							<tr>
								<td class="gray_b" align="center" width="100" bgcolor="#edeadc"><b>받을 사람</b></td>
								<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
									valign="top" width="300" bgcolor="#f5f4ef"><asp:label id="receiverNm" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td bgcolor="#cccccc" colspan="2" height="1"></td>
							</tr>
							<tr>
								<td class="gray_b" align="center" width="100" bgcolor="#edeadc"><b>실시간 쪽지</b></td>
								<td width="300" bgcolor="#f5f4ef">
									<table cellspacing="5" cellpadding="0" border="0">
										<tr>
											<td valign="top" width="290">
												<asp:TextBox id="comment" runat="server" textmode="MultiLine" width="290" height="132" style="BORDER-RIGHT:#aaaaaa 1px solid;PADDING-RIGHT:5px;BORDER-TOP:#aaaaaa 1px solid;PADDING-LEFT:5px;IME-MODE:active;PADDING-BOTTOM:5px;BORDER-LEFT:#aaaaaa 1px solid;COLOR:#666666;PADDING-TOP:5px;BORDER-BOTTOM:#aaaaaa 1px solid"></asp:TextBox>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td bgcolor="#cccccc" colspan="2" height="1"></td>
							</tr>
						</table>
						<table height="40" cellspacing="0" cellpadding="0" width="400" border="0">
							<tr>
								<td><!--A href="javascript:previewNote();"><IMG src="/UserNote/images/bt_view_01.gif" border="0"></A--></td>
								<td align="right"><IMG style="CURSOR: hand" onclick="doSend();" src="/UserNote/images/l_send.gif">
									&nbsp;<IMG style="CURSOR: hand" onclick="window.close();" src="/UserNote/images/bt_cancel_01.gif"></td>
							</tr>
						</table>
						<!---------본문 끄읕---------></td>
				</tr>
			</table>
			<asp:linkbutton id="btnSend" runat="server" OnClick="btnSend_Click"></asp:linkbutton>
		</form>
	</body>
</HTML>
