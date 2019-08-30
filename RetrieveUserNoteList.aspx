<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RetrieveUserNoteList.aspx.cs" Inherits="RetrieveUserNoteList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>쪽지함</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="http://www.imbc.com/common/css/style2005.css" rel="stylesheet" type="text/css">
		<style>P { MARGIN: 0px; WORD-BREAK: break-all; LINE-HEIGHT: 120% }
		</style>
		<script language="javascript">
		    function doDelete() {
		        checkSelect();

		        if (document.all.hdnCheck.value == "")
		            alert("삭제할 쪽지를 선택하세요.");
		        else if (confirm("선택한 쪽지를 삭제하시겠습니까?"))
		            __doPostBack('btnDelete', '');
		    }


		    function doMove() {
		        checkSelect();

		        if (document.all.hdnCheck.value == "")
		            alert("보관함으로 이동할 쪽지를 선택하세요.");
		        else if (confirm("선택한 쪽지를 보관함으로 이동하시겠습니까?"))
		            __doPostBack('btnMove', '');
		    }


		    function doReply(noteID, progCode) {
		        window.open("RegisterUserReplyNoteInfo.aspx?noteID=" + noteID + "&progCode=" + progCode, "_ReplyNote", "width=450, height=300");
		    }


		    function checkAll(chk) {
		        var f = document.Form1;

		        for (var i = 0; i < f.elements.length; i++) {
		            if (f.elements[i].type == "checkbox" && f.elements[i].id.indexOf("chkbox") > 0) {
		                f.elements[i].checked = chk.checked;
		            }
		        }
		    }


		    function checkSelect() {
		        var f = document.Form1;
		        var strValue = "";

		        for (var i = 0; i < f.elements.length; i++) {
		            if (f.elements[i].type == "checkbox" && f.elements[i].id.indexOf("chkbox") > 0 && f.elements[i].checked) {
		                if (strValue != "") strValue += ",";
		                strValue += f.elements[i].value;
		            }
		        }

		        document.all.hdnCheck.value = strValue;
		    }

		    window.resizeTo(840, 620);

		    function init() {
		        document.body.bgColor = "#ffffff";
		    }
		</script>
	</HEAD>
	<body bgcolor="#ffffff" text="#000000" topmargin="0" leftmargin="0" scroll="no" onload="init();">
		<form id="Form1" method="post" runat="server">
			<input type="hidden" id="hdnCheck" runat="server">
			<table width="800" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td>
						<table width="800" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_01.jpg" width="205" height="75"></td>
								<td><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_02.jpg" width="565" height="75"></td>
								<td><a href="#" onfocus="this.blur();" onclick="window.close();"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_03.jpg" width="30" height="75" alt="닫기" border="0"></a></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="top" width="595">
						<table width="800" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td width="205">
									<table width="205" border="0" cellspacing="0" cellpadding="0">
										<tr>
											<td width="205"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_04.jpg" width="205" height="130" border="0"
													alt="받은쪽지함"></td>
										</tr>
										<tr>
											<td><a href="RetrieveUserReplyNoteList.aspx" onfocus="this.blur();"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_06_off.jpg" width="205" height="90" alt="보낸쪽지함"
														border="0"></a></td>
										</tr>
										<tr>
											<td><a href="RetrieveUserKeepNoteList.aspx" onfocus="this.blur();"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_07_off.jpg" width="205" height="95" alt="보관함"
														border="0"></a></td>
										</tr>
										<tr>
											<td><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_08.jpg" width="205" height="210"></td>
										</tr>
									</table>
								</td>
								<td align="center" valign="top">
									<table width="590" border="0" cellspacing="0" cellpadding="0">
										<tr>
											<td align="right" style="PADDING-RIGHT:20px; PADDING-LEFT:0px; PADDING-BOTTOM:0px; PADDING-TOP:0px"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_text.jpg" width="347" height="36" vspace="15"><br>
											</td>
											<td align="right" width="200" style="PADDING-RIGHT:20px; PADDING-LEFT:0px; PADDING-BOTTOM:0px; PADDING-TOP:0px"><a href="#" onfocus="this.blur()" onclick="doDelete();"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_dele.jpg" width="62" height="21" vspace="15"
														hspace="4" border="0" alt="삭제"></a><a href="#" onfocus="this.blur()" onclick="doMove();"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_move.jpg" width="94" height="21" vspace="15"
														border="0" alt="보관함이동"></a></td>
										</tr>
										<tr>
											<td align="center" colspan="2">
												<table width="570" border="0" cellspacing="0" cellpadding="0">
													<tr>
														<td>
															<table width="100%" border="0" cellspacing="0" cellpadding="0" background="http://img.imbc.com/mini/UserNote/images/mini_memo_box_t2.jpg">
																<tr>
																	<td width="5"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_t1.jpg" width="5" height="29"></td>
																	<td>
																		<table width="100%" border="0" cellspacing="0" cellpadding="0">
																			<tr align="center">
																				<td width="30"><input id="chkall" type="checkbox" onclick="checkAll(this);"></td>
																				<td width="120"><b>보낸사람</b></td>
																				<td><b>제목</b>(내용)</td>
																				<td width="80"><b>받은날짜</b></td>
																			</tr>
																		</table>
																	</td>
																	<td align="right" width="5"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_t3.jpg" width="5" height="29"></td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td style="PADDING-RIGHT:0px; PADDING-LEFT:0px; PADDING-BOTTOM:0px; PADDING-TOP:5px">
															<div style="OVERFLOW-Y:scroll; HEIGHT:360px">
																<table width="100%" border="0" cellspacing="2" cellpadding="0">
																	<asp:Repeater id="logRepeater" runat="server" OnItemDataBound="logRepeater_ItemDataBound">
																		<ItemTemplate>
																			<tr>
																				<td width="35" align="center"><img src="http://mini.imbc.com/UserNote/images/mini_memo_icon_web.gif" alt="이것은 전체쪽지입니다. 전체쪽지는 삭제 및 보관함으로 이동되지 않습니다."></td>
																				<td width="120" align="center">
																					<asp:Label id="logSenderNm" runat="server"></asp:Label></td>
																				<td>
																					<font color="#5f4d84">
																						<asp:Label id="logComment" runat="server"></asp:Label></font>
																				</td>
																				<td width="85" align="center">
																					<asp:Label id="logRgDt" runat="server"></asp:Label>
																				</td>
																			</tr>
																			<tr bgcolor="#e2e2e2">
																				<td colspan="4" align="center" height="1"></td>
																			</tr>
																		</ItemTemplate>
																	</asp:Repeater>
																	<asp:Repeater id="listRepeater" runat="server" OnItemDataBound="listRepeater_ItemDataBound">
																		<ItemTemplate>
																			<tr>
																				<td width="35" align="center">
																					<input type="checkbox" id="chkbox" runat="server">
																				</td>
																				<td width="120" align="center">
																					<asp:Label id="senderNm" runat="server"></asp:Label></td>
																				<td>
																					<table border="0" cellspacing="0" cellpadding="0">
																						<tr>
																							<td style="PADDING-RIGHT:0px; PADDING-LEFT:15px; PADDING-BOTTOM:2px; PADDING-TOP:2px"
																								valign="top" width="85">
																								<asp:image id="progImg" runat="server" width="73" height="65"></asp:image></td>
																							<td valign="top" style="PADDING-RIGHT:0px; PADDING-LEFT:0px; PADDING-BOTTOM:2px; PADDING-TOP:2px">
																								<font color="#5f4d84">
																									<asp:Label id="comment" runat="server"></asp:Label></font>
																							</td>
																						</tr>
																					</table>
																				</td>
																				<td width="85" align="center">
																					<asp:Label id="rgDt" runat="server"></asp:Label><br>
																					<asp:Image id="btnReply" runat="server" imageurl="http://img.imbc.com/mini/UserNote/images/mini_memo_re.jpg" width="56"
																						height="19"></asp:Image>
																				</td>
																			</tr>
																			<tr bgcolor="#e2e2e2">
																				<td colspan="4" align="center" height="1"></td>
																			</tr>
																		</ItemTemplate>
																	</asp:Repeater>
																</table>
															</div>
														</td>
													</tr>
												</table>
												<table width="570" border="0" cellspacing="0" cellpadding="0" bgcolor="#dcdadf">
													<tr>
														<td height="4"></td>
													</tr>
												</table>
												<br>
												<asp:Label id="pageNavigator" runat="server"></asp:Label>
											</td>
										</tr>
										<tr>
											<td colspan="2">&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<asp:LinkButton id="btnDelete" runat="server" OnClick="btnDelete_Click"></asp:LinkButton>
			<asp:LinkButton id="btnMove" runat="server" OnClick="btnMove_Click"></asp:LinkButton>
		</form>
	</body>
</HTML>
