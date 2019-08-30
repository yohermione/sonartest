<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RetrieveMemoAdminList.aspx.cs" Inherits="Admin_RetrieveMemoAdminList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RetrieveMemoAdminList</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="http://www.imbc.com/common/css/style2005.css" rel="stylesheet" type="text/css">
		<style>
		.b_tx11b { FONT-WEIGHT: bold; FONT-SIZE: 11px; COLOR: #000000; LINE-HEIGHT: 10pt }
		A.b_tx11b:link { FONT-WEIGHT: bold; FONT-SIZE: 11px; COLOR: #000000; FONT-FAMILY: verdana }
		A.b_tx11b:visited { FONT-WEIGHT: bold; FONT-SIZE: 11px; COLOR: #000000; FONT-FAMILY: verdana }
		A.b_tx11b :active { FONT-WEIGHT: bold; FONT-SIZE: 11px; COLOR: #000000; FONT-FAMILY: verdana }
		A.b_tx11b:hover { FONT-WEIGHT: bold; FONT-SIZE: 11px; COLOR: #e93b72; FONT-FAMILY: verdana }
		.p_tx11b { FONT-WEIGHT: bold; FONT-SIZE: 11px; COLOR: #e93b72; FONT-FAMILY: verdana }
		A.p_tx11b:link { FONT-WEIGHT: bold; FONT-SIZE: 11px; COLOR: #e93b72; FONT-FAMILY: verdana }
		A.p_tx11b:visited { FONT-WEIGHT: bold; FONT-SIZE: 11px; COLOR: #e93b72; FONT-FAMILY: verdana }
		A.p_tx11b:active { FONT-WEIGHT: bold; FONT-SIZE: 11px; COLOR: #e93b72; FONT-FAMILY: verdana }
		A.p_tx11b:hover { FONT-WEIGHT: bold; FONT-SIZE: 11px; COLOR: #e93b72; FONT-FAMILY: verdana }
		</style>
		<script language="javascript" src="../Js/protoajax.js"></script>
		<script language="javascript">

		    function fNewWin(strUsrNo, struserID) {
		        window.open('http://member.imbc.com/admin/BbsUserInfo.aspx?uno=' + strUsrNo+'&uid='+struserID, 'usrinfo', 'resizable=no,scrollbars=no,x=100,y=200,width=400,height=120');
		        //			window.open('http://login.imbc.com/imbc/bbsinfo/bbsinfo.asp?myid='+strUsrId, 'usrinfo', 'resizable=no,scrollbars=no,x=100,y=200,width=400,height=120'); 
		    }

		    function doSearch() {
		        var stDtVal = "";
		        var spDtVal = "";

		        if (document.all.radioDt2.checked) {
		            if (document.all.stDt.value == "" || document.all.spDt.value == "") {
		                alert("기간을 정확히 입력하세요.");
		                return;
		            }

		            stDtVal = document.all.stDt.value;
		            spDtVal = document.all.spDt.value;
		        }

		        var type = document.all.memoType.options[document.all.memoType.selectedIndex].value;
		        var sch = document.all.search.options[document.all.search.selectedIndex].value;

		        location.href = "RetrieveMemoAdminList.aspx?progCode=<%=progCode%>&memoType=" + type + "&search=" + sch + "&searchWord=" + document.all.searchWord.value + "&stDt=" + stDtVal + "&spDt=" + spDtVal;
		    }

		    function enableDt() {
		        document.all.stDt.disabled = "";
		        document.all.spDt.disabled = "";
		    }

		    function disableDt() {
		        document.all.stDt.value = "";
		        document.all.spDt.value = "";

		        document.all.stDt.disabled = "disabled";
		        document.all.spDt.disabled = "disabled";
		    }


		    function sendSelect() {
		        var f = document.Form1;
		        var strValue = "";
		        var arr = new Array();
		        var exist = false;
		        var k = 0;

		        for (var i = 0; i < f.elements.length; i++) {
		            exist = false;
		            if (f.elements[i].type == "checkbox" && f.elements[i].id.indexOf("chkbox") > 0 && f.elements[i].checked) {
		                for (var j = 0; j < arr.length; j++) {
		                    if (arr[j] == f.elements[i].value)
		                        exist = true;
		                }

		                if (exist == false) {
		                    if (strValue != "") strValue += ",";
		                    strValue += f.elements[i].value;

		                    arr[k++] = f.elements[i].value;
		                }
		            }
		        }

		        if (strValue == "")
		            alert("쪽지 보낼 사용자를 선택하세요.");
		        else
		            window.open("RegisterAdminNoteInfo.aspx?progCode=<%=progCode%>&receiver=" + strValue, "_RegiNote", "width=450, height=300");
		    }

		    function selDelete() {
		        var f = document.forms[0];
		        var strValue = "";
		        var arr = new Array();
		        var exist = false;
		        var k = 0;

		        for (var i = 0; i < f.elements.length; i++) {
		            exist = false;
		            if (f.elements[i].type == "checkbox" && f.elements[i].id.indexOf("chkbox") > 0 && f.elements[i].checked) {
		                for (var j = 0; j < arr.length; j++) {
		                    if (arr[j] == f.elements[i + 1].value)
		                        exist = true;
		                }

		                if (exist == false) {
		                    if (strValue != "") strValue += ",";
		                    strValue += f.elements[i + 1].value;
		                    arr[k++] = f.elements[i + 1].value;
		                }
		            }
		        }

		        if (strValue == "") {
		            alert("삭제할 메세지를 선택하세요");
		        }
		        else {
		            var pars = "progCode=<%=progCode%>&seq=" + strValue;
		            var myAjax = new Ajax.Request("ProcessMemoMstInfo.aspx", { method: 'get', parameters: pars, onComplete: ShowResult });
		        }

		    }

		    function ShowResult(originalRequest) {
		        try {
		            if (parseInt(originalRequest.responseText) > 0) {
		                alert('메세지 글 ' + originalRequest.responseText + ' 개가 삭제되었습니다');
		                location.href = 'RetrieveMemoAdminList.aspx?progCode=<%=progCode%>&curPage=<%=nCurPage%>&memoType=<%=memoType%>&search=<%=search%>&searchWord=<%=searchWord%>&stDt=<%=stDt%>&spDt=<%=spDt%>';
		            }
		            else {
		                alert('글이 삭제되지 않았습니다');
		            }
		        }
		        catch (e) {
		            alert('글을 삭제하던 중 오류가 발생하였습니다.');
		        }
		    }


		    function checkAll(chk) {
		        var f = document.Form1;

		        for (var i = 0; i < f.elements.length; i++) {
		            if (f.elements[i].type == "checkbox" && f.elements[i].id.indexOf("chkbox") > 0) {
		                f.elements[i].checked = chk.checked;
		            }
		        }
		    }
		</script>
	</HEAD>
	<body bgcolor="#ffffff">
		<form id="Form1" method="post" runat="server">
			<table width="99%" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td><table width="100%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td width="50"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_logo.jpg" width="46" height="27"></td>
								<td valign="bottom"><asp:Label id="progTitle" runat="server"></asp:Label>
									<font color="#8b67c4">(새 메시지
										<asp:Label id="newCnt" runat="server"></asp:Label>
										개) </font>
								</td>
								<!--td align="right">
									<a onclick="javascript:selDelete();" style="cursor:pointer"><img src="/UserNote/images/mini_del.gif" width="96" height="21" border="0" alt="메세지 삭제하기"></a>
									<!--a href="#" onfocus="this.blur()" onclick="document.all.tblSearch.style.display='';"><img src="/UserNote/images/mini_memo_search.jpg" width="82" height="21" border="0" alt="상세검색"></a>
									<a href="#" onfocus="this.blur()" onclick="sendSelect();"><img src="/UserNote/images/mini_memo_send.jpg" width="93" height="21" border="0" alt="쪽지보내기"></a></td-->
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><br>
						<table border="0" cellpadding="0" cellspacing="0" background="http://img.imbc.com/mini/UserNote/images/mini_memo_box2.jpg"
							id="tblSearch">
							<tr>
								<td><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box1.jpg" width="551" height="3"></td>
							</tr>
							<tr>
								<td align="center" style="PADDING-RIGHT:0px; PADDING-LEFT:0px; PADDING-BOTTOM:8px; PADDING-TOP:8px">
									<table width="97%" border="0" cellspacing="0" cellpadding="3">
										<tr>
											<td width="10"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_point.jpg" width="9" height="9"></td>
											<td width="70"><font color="#966ecb"><b>상세검색 ㅣ </b></font>
											</td>
											<td>
												<select id="memoType">
													<option value="0" selected>전체</option>
													<option value="1">게시판</option>
													<option value="2">쪽지</option>
													<option value="3">사연</option>
												</select>
												<select id="search">
													<option value="" selected>전체</option>
													<option value="1">내용</option>
													<option value="2">글쓴이</option>
												</select>
												<input id="searchWord" type="text" name="searchWord"> <img src="http://img.imbc.com/mini/UserNote/images/mini_memo_search2.jpg" width="44"
													height="21" align="absMiddle" alt="검색" style="CURSOR:hand" onclick="doSearch();">
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
										<tr>
											<td><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_point.jpg" width="9" height="9"></td>
											<td><font color="#966ecb"><b>기간 ㅣ </b></font>
											</td>
											<td>
												<input type="radio" id="radioDt1" name="radioDt" value="" checked onclick="disableDt()">
												전체입력 / <input type="radio" id="radioDt2" name="radioDt" value="1" onclick="enableDt()">
												직접입력 <input type="text" id="stDt" name="stDt" size="10" maxlength="8" disabled> 
												~ <input type="text" id="spDt" name="spDt" size="10" maxlength="8" disabled> ex) 
												20061102
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_box3.jpg" width="551" height="3"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="30">
					<td></td>
				</tr>
				<tr>
					<td align="left">
						<a onclick="javascript:selDelete();" style="CURSOR:pointer"><img src="../images/mini_del.gif" width="96" height="21"
								border="0" alt="메세지 삭제하기"></a> 
						<!--a href="#" onfocus="this.blur()" onclick="document.all.tblSearch.style.display='';"><img src="/UserNote/images/mini_memo_search.jpg" width="82" height="21" border="0" alt="상세검색"></a-->
						<!--a href="#" onfocus="this.blur()" onclick="sendSelect();"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_send.jpg" width="93" height="21"
								border="0" alt="쪽지보내기"></a--></td>
				</tr>
				<tr>
					<td>
						<b>검색 수 : 총
						<%=nTotalRecord%></b>
					</td>
				</tr>
				<tr>
					<td align="center"><br>
						<table width="97%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td width="2"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_table1.gif" width="2" height="25"></td>
								<td bgcolor="#ae91d5">
									<table width="100%" border="0" cellspacing="3" cellpadding="0">
										<tr align="center">
											<td width="33"><b><font color="#ffffff"><input id="chkall" type="checkbox" onclick="checkAll(this);"></font></b></td>
											<td width="50"><b><font color="#ffffff">no</font></b></td>
											<td width="30"><b><font color="#ffffff">종류</font></b></td>
											<td width="80"><b><font color="#ffffff">이름</font></b></td>
											<td><b><font color="#ffffff">내용</font></b></td>
											<td width="67"><b><font color="#ffffff">날짜</font></b></td>
										</tr>
									</table>
								</td>
								<td width="3"><img src="http://img.imbc.com/mini/UserNote/images/mini_memo_table2.gif" width="3" height="25"></td>
							</tr>
						</table>
						<table width="97%" border="0" cellspacing="3" cellpadding="0">
							<asp:Repeater id="listRepeater" runat="server" OnItemDataBound="listRepeater_ItemDataBound">
								<ItemTemplate>
									<tr>
										<td width="35" align="center">
											<input type="checkbox" id="chkbox" runat="server"> <input type="hidden" id="hdnSeqID" runat="server">
										</td>
										<td width="50" align="center"><font color="#999999">
												<asp:Label id="seqID" runat="server"></asp:Label></font></td>
										<td width="30" align="center">
											<asp:Image id="imgType" runat="server"></asp:Image></td>
										<td width="80" align="center"><font color="#999999">
												<asp:Label id="userNm" runat="server"></asp:Label></font></td>
										<td style="word-break:break-all"><font color="#666666">
												<asp:Label id="comment" runat="server"></asp:Label></font></td>
										<td width="70" align="center">
											<asp:Label id="rgDt" runat="server"></asp:Label></td>
									</tr>
									<tr>
										<td colspan="6" height="1" align="center" bgcolor="#e6e6e6"><img src="http://img.imbc.com/mini/UserNote/images/x.gif" width="1" height="1"></td>
									</tr>
								</ItemTemplate>
							</asp:Repeater>
						</table>
						<table width="97%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td width="2" bgcolor="#ae91d5"><img src="http://img.imbc.com/mini/UserNote/images/x.gif" width="2" height="2"></td>
							</tr>
						</table>
						<table width="97%" border="0" cellspacing="0" cellpadding="5">
							<tr>
								<td align="center"><asp:Label id="pageNavigator" runat="server"></asp:Label></td>
							</tr>
						</table>
						<!--table cellspacing="0" cellpadding="5" width="97%" border="0">
							<tr>
								<td align="center">
									<select id="search" style="BORDER-RIGHT: #c7acdf 1px solid; BORDER-TOP: #c7acdf 1px solid; BORDER-LEFT: #c7acdf 1px solid; COLOR: #000000; BORDER-BOTTOM: #c7acdf 1px solid; FONT-FAMILY: 돋움; BACKGROUND-COLOR: #f5f5f5">
										<option value="1">내용</option>
										<option value="2" selected>이름</option>
									</select>
									<input id="searchWord" type="text" name="searchWord" style="BORDER-RIGHT: #c7acdf 1px solid; BORDER-TOP: #c7acdf 1px solid; BORDER-LEFT: #c7acdf 1px solid; COLOR: #000000; BORDER-BOTTOM: #c7acdf 1px solid; FONT-FAMILY: 돋움; BACKGROUND-COLOR: #f5f5f5"> <img src="/UserNote/images/mini_memo_search2.jpg" width="44" height="21" align="absMiddle" alt="검색"
										style="CURSOR:hand" onclick="doSearch();">
									<img src="/UserNote/images/bt_list.gif" style="cursor:hand" onclick="doList();" align="absmiddle">
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
						</table-->
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
