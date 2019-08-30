<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RetrieveUserReplyNoteList.aspx.cs" Inherits="RetrieveUserReplyNoteList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>쪽지함</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="http://www.imbc.com/common/css/style2005.css" rel="stylesheet" type="text/css">
	</HEAD>
	<body bgcolor="#ffffff" text="#000000" topmargin="0" leftmargin="0">
		<form id="Form1" method="post" runat="server">
			<table width="800" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td>
						<table width="800" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_01.jpg" width="205" height="75"></td>
								<td><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_02_1.jpg" width="565" height="75"></td>
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
											<td width="205"><a href="RetrieveUserNoteList.aspx" onfocus="this.blur();"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_04_off.jpg" width="205" height="130" border="0" alt="받은쪽지함"></a></td>
										</tr>
										<tr>
											<td><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_06.jpg" width="205" height="90" alt="보낸쪽지함"></td>
										</tr>
										<tr>
											<td><a href="RetrieveUserKeepNoteList.aspx" onfocus="this.blur();"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_07_off.jpg" width="205" height="95" alt="보관함" border="0"></a></td>
										</tr>
										<tr>
											<td><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_08.jpg" width="205" height="210"></td>
										</tr>
									</table>
								</td>
								<td align="center" valign="top">
									<table width="590" border="0" cellspacing="0" cellpadding="0">
										<tr>
											<td align="right" style="PADDING-RIGHT:20px; PADDING-LEFT:0px; PADDING-BOTTOM:0px; PADDING-TOP:0px"><a href="#"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_dele.jpg" width="62" height="21" vspace="15" hspace="4"
														border="0" alt="삭제"></a></td>
										</tr>
										<tr>
											<td align="center">
												<table width="570" border="0" cellspacing="0" cellpadding="0">
													<tr>
														<td>
															<table width="100%" border="0" cellspacing="0" cellpadding="0" background="http://img.imbc.com/mini/UserNote/images/mini_memo_box_t2.jpg">
																<tr>
																	<td width="5"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box_t1.jpg" width="5" height="29"></td>
																	<td>
																		<table width="100%" border="0" cellspacing="0" cellpadding="0">
																			<tr align="center">
																				<td width="50"><b>전체</b></td>
																				<td width="120"><b>받은사람</b></td>
																				<td><b>제목</b>(내용)</td>
																				<td width="80"><b>보낸날짜</b></td>
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
															<table width="100%" border="0" cellspacing="2" cellpadding="0">
																<asp:Repeater id="listRepeater" runat="server" OnItemDataBound="listRepeater_ItemDataBound">
																	<ItemTemplate>
																		<tr>
																			<td width="55" align="center">
																				<input type="checkbox" id="chkbox" runat="server" name="chkbox">
																			</td>
																			<td width="120" align="center">
																				<asp:Label id="receiverNm" runat="server"></asp:Label></td>
																			<td>
																				<asp:Label id="comment" runat="server"></asp:Label></td>
																			<td width="85" align="center">
																				<asp:Label id="rgDt" runat="server"></asp:Label>
																			</td>
																		</tr>
																		<tr bgcolor="#e2e2e2">
																			<td colspan="4" align="center" height="1"></td>
																		</tr>
																	</ItemTemplate>
																</asp:Repeater>
															</table>
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
											<td>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
