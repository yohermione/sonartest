<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RetrieveAdminNoteList.aspx.cs" Inherits="Admin_RetrieveAdminNoteList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RetrieveAdminNoteList</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/UserNote/css/style.css" rel="stylesheet" type="text/css">
		<style>P {margin-top:0px;margin-bottom:0px;margin-left:0;margin-right:0;line-height:120%;word-break:break-all;}
		</style>	
		<script language="javascript">
		    function doDelete() {
		        checkSelect();

		        if (document.all.hdnCheck.value == "")
		            alert("삭제할 쪽지를 선택하세요.");
		        else if (confirm("선택한 쪽지를 삭제하시겠습니까?"))
		            __doPostBack('btnDelete', '');
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
		</script>
	</HEAD>
	<body leftmargin="0" topmargin="0">
		<form id="Form1" method="post" runat="server">
			<input type="hidden" id="hdnCheck" runat="server" name="hdnCheck">
			<table width="550" border="0" cellspacing="0" cellpadding="10">
				<tr>
					<td bgcolor="#cccccc" height="1"></td>
				</tr>
				<tr>
					<td bgcolor="#f5f4ef" width="530">
						<table width="530" border="0" cellspacing="0" cellpadding="5">
							<tr>
								<td bgcolor="#cccccc" height="1" colspan="3"></td>
							</tr>
							<tr align="center" bgcolor="#edeadc">
								<td><input id="chkall" type="checkbox" onclick="checkAll(this);"></td>
								<td class="gray_b"><b>내용</b></td>
								<td class="gray_b" width="60"><b>보낸시간</b></td>
							</tr>
							<asp:Repeater id="listRepeater" runat="server" OnItemDataBound="listRepeater_ItemDataBound">
								<ItemTemplate>
									<tr>
										<td bgcolor="#cccccc" height="1" colspan="3"></td>
									</tr>
									<tr>
										<td align="center"><input type="checkbox" id="chkbox" runat="server" name="chkbox"></td>
										<td valign="top">																						
											<table border="0" cellspacing="0" cellpadding="0">
												<tr>
													<td style="PADDING-RIGHT:0px; PADDING-LEFT:15px; PADDING-BOTTOM:5px; PADDING-TOP:5px"
														valign="top" width="85"><asp:image id="progImg" runat="server" width="73" height="65"></asp:image></td>
													<td valign="top" style="PADDING-RIGHT:0px; PADDING-LEFT:0px; PADDING-BOTTOM:5px; PADDING-TOP:5px">
														<font color="#5f4d84">
														<asp:Label id="comment" runat="server"></asp:Label></font></td>
												</tr>
											</table>
										</td>
										<td align="center"><asp:Label id="rgDt" runat="server"></asp:Label></td>
									</tr>
								</ItemTemplate>
							</asp:Repeater>
						</table>
					</td>
				</tr>
				<tr>
					<td bgcolor="#cccccc" height="1"></td>
				</tr>
				<tr>
					<td>
						<table width="100%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td><a href="#" onfocus='this.blur()' onclick="doDelete();"><img src="http://img.imbc.com/mini/UserNote/images/bt_choice_del.gif" border="0"></a></td>
								<td align="right" height="20"><font color="#999999">* 보낸 쪽지는 1개월간 보관됩니다.</font></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table width="550" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<Td align="center">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<Td>
									<asp:Label id="pageNavigator" runat="server"></asp:Label>
								</Td>
							</tr>
						</table>
					</Td>
				</tr>
			</table>
			<asp:LinkButton id="btnDelete" runat="server"></asp:LinkButton>
		</form>
	</body>
</HTML>
