﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestCertificate_PDF.aspx.cs" Inherits="Admin_TestCertificate_PDF" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="updatepnl" runat="server">
            <ContentTemplate>

                <div class="container-fluid">         
                    <div class="row" style="display: block; margin-top: 10px;">
                        <iframe id="ifrRight6" runat="server" enableviewstate="false" style="width: 100%; -ms-zoom: 0.75; height: 685px;"></iframe>
                    </div>
                </div>

            </ContentTemplate>
    
        </asp:UpdatePanel>
    </form>
</body>
</html>
