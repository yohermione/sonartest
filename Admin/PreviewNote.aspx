<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreviewNote.aspx.cs" Inherits="Admin_PreviewNote" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PreviewNote</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="http://www.imbc.com/common/css/style2005.css" rel="stylesheet" type="text/css">
		<style>P {margin-top:0px;margin-bottom:0px;margin-left:0;margin-right:0;line-height:120%;word-break:break-all;}
		</style>
		<script language="javascript">
		    function init() {
		        document.all.progImg.src = opener.document.all.progImg.src;
		        document.all.preview.innerHTML = opener.editBox.get_html();
		        document.all.progTitle.innerHTML = opener.document.all.progTitle.innerHTML;
		    }
		</script>
	</HEAD>
	<body leftmargin="0" topmargin="0" onload="init();">
		<form id="Form1" method="post" runat="server">
			<table width="257" height="129" border="0" cellpadding="0" cellspacing="0" background="/UserNote/img/all_bg.jpg">
				<tr>
					<td align="left" valign="top">
						<table border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td style="PADDING-RIGHT:0px; PADDING-LEFT:15px; PADDING-BOTTOM:0px; PADDING-TOP:0px"><font color="#717171" class="small">보낸사람 
										: <span id="progTitle"></span></font></td>
							</tr>
							<tr>
								<td style="PADDING-RIGHT:0px; PADDING-LEFT:15px; PADDING-BOTTOM:0px; PADDING-TOP:0px"><font color="#717171" class="small">받는 
										사람 : 정해영</font></td>
							</tr>
						</table>
						<table width="250" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td style="PADDING-RIGHT:0px; PADDING-LEFT:15px; PADDING-BOTTOM:0px; PADDING-TOP:10px"
									valign="top" width="85"><img id="progImg" src="" width="73" height="65"></td>
								<td valign="top" style="PADDING-RIGHT:0px; PADDING-LEFT:0px; PADDING-BOTTOM:0px; PADDING-TOP:10px">
									<font color="#5f4d84"><span id="preview" style="OVERFLOW-Y:auto; WIDTH:155px; HEIGHT:61px">
										</span></font>
								</td>
							</tr>
						</table>
						<table width="250" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td align="right">
									<!---답장아이콘-<img src="img/reply.jpg" width="56" height="19">--->
									<img src="/UserNote/img/re_w.jpg" border="0"><img src="/UserNote/img/send.jpg" width="64" height="19" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>			
		</form>
	</body>
</HTML>