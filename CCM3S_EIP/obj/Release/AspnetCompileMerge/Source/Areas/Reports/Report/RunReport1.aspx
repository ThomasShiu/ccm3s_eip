<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RunReport1.aspx.cs" Inherits="CCM3S_EIP.Areas.MIS.Report.RunReport1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Report報表測試</title>
</head>
<body>
    <form id="form1" runat="server">
 
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <rsweb:ReportViewer ID="RptViewer" runat="server" Height="500px" Width="1000px"></rsweb:ReportViewer>
        </div>
   
    </form>
</body>
</html>
