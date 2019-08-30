<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RetrieveUserNoteInfo.aspx.cs" Inherits="RetrieveUserNoteInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RetrieveUserNoteInfo</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="http://www.imbc.com/common/css/style2005.css" type="text/css" rel="stylesheet">
		<style>P {MARGIN: 0px; WORD-BREAK: break-all; LINE-HEIGHT: 120%}</style>
		<script language="javascript">
		    function showReply() {
		        document.all.commentTd.style.display = "none";
		        document.all.commentTd2.style.display = "";
		        document.all.btnReply.style.display = "none";
		        document.all.btnReply2.style.display = "";
		        document.all.txtComment.value = "";
		        document.all.txtComment.focus();
		    }

		    function doReply() {
		        if (document.all.txtComment.value != "") {
		            __doPostBack('btnSave', '');
		        }
		    }
		</script>
	</HEAD>
	<body leftmargin="0" topmargin="0" onmouseover="document.location.href='EVENT:MOUSEOVER';">
		<form id="Form1" method="post" runat="server">
			<table height="129" cellspacing="0" cellpadding="0" width="257" background="http://img.imbc.com/mini/UserNote/img/all_bg.jpg"
				border="0">
				<tr>
					<td valign="top" align="left">
						<table cellspacing="0" cellpadding="0" border="0">
							<tr>
								<td style="PADDING-RIGHT: 0px; PADDING-LEFT: 15px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px"><font class="small" color="#717171">보낸사람 
										:
										<asp:label id="progTitle" runat="server"></asp:label></font></td>
							</tr>
							<tr>
								<td style="PADDING-RIGHT: 0px; PADDING-LEFT: 15px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px"><font class="small" color="#717171">받는 
										사람 :
										<asp:label id="userNm" runat="server"></asp:label></font></td>
							</tr>
						</table>
						<table cellspacing="0" cellpadding="0" width="250" border="0">
							<tr>
								<td style="PADDING-RIGHT: 0px; PADDING-LEFT: 15px; PADDING-BOTTOM: 0px; PADDING-TOP: 10px"
									valign="top" width="85"><asp:image id="progImg" runat="server" height="65" width="73"></asp:image></td>
								<td id="commentTd" style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 10px"
									valign="top"><font color="#5f4d84"><asp:label id="comment" style="OVERFLOW-Y: auto" runat="server" height="61" width="155"></asp:label></font></td>
								<td id="commentTd2" style="PADDING-RIGHT: 0px; DISPLAY: none; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 10px"
									valign="top"><asp:textbox id="txtComment" runat="server" height="60" width="100%" textmode="MultiLine"></asp:textbox></td>
							</tr>
						</table>
						<table width="250" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td id="btnReply" align="right">
									<!---답장아이콘-<img src="img/reply.jpg" width="56" height="19">--->
									<img src="http://img.imbc.com/mini/UserNote/img/reply.jpg" border="0" style="CURSOR:hand" onclick="showReply();"></td>
								<td align="right" id="btnReply2" style="DISPLAY:none">
									<img src="http://img.imbc.com/mini/UserNote/img/send.jpg" border="0" style="CURSOR:hand" onclick="doReply();"> <img src="http://img.imbc.com/mini/UserNote/img/re_w.jpg" border="0" style="CURSOR:hand" onclick="showReply();">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<asp:LinkButton id="btnSave" runat="server" OnClick="btnSave_Click"></asp:LinkButton></form>
	</body>
</HTML>
