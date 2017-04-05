<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="samples_cs.Default" %>

<%@ Register Assembly="JQChart.Web" Namespace="JQChart.Web.UI.WebControls" TagPrefix="jqChart" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>jqChart ASP.NET Sample</title>
    <link rel="stylesheet" type="text/css" href="~/Content/jquery.jqChart.css" />
    <link rel="stylesheet" type="text/css" href="~/Content/jquery.jqRangeSlider.css" />
    <link rel="stylesheet" type="text/css" href="~/Content/themes/smoothness/jquery-ui-1.10.4.css" />
    <script src="<% = ResolveUrl("~/Scripts/jquery-1.11.1.min.js") %>" type="text/javascript"></script>
    <script src="<% = ResolveUrl("~/Scripts/jquery.jqRangeSlider.min.js") %>" type="text/javascript"></script>
    <script src="<% = ResolveUrl("~/Scripts/jquery.jqChart.min.js") %>" type="text/javascript"></script>
    <script src="<% = ResolveUrl("~/Scripts/jquery.mousewheel.js") %>" type="text/javascript"></script>
    <!--[if IE]><script lang="javascript" type="text/javascript" src="<% = ResolveUrl("~/Scripts/excanvas.js") %>"></script><![endif]-->
</head>
<body>
    <form id="form1" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
        TypeName="SamplesBrowser.Models.ChartData"></asp:ObjectDataSource>
    <jqChart:Chart ID="Chart1" Width="500px" Height="300px" runat="server" DataSourceID="ObjectDataSource1">
        <Title Text="Chart Title"></Title>
        <Animation Enabled="True" Duration="00:00:01" />
        <Axes>
            <jqChart:CategoryAxis Location="Bottom" ZoomEnabled="true">
            </jqChart:CategoryAxis>
        </Axes>
        <Series>
            <jqChart:ColumnSeries XValuesField="Label" YValuesField="Value1" Title="Column">
            </jqChart:ColumnSeries>
            <jqChart:LineSeries XValuesField="Label" YValuesField="Value2" Title="Line">
            </jqChart:LineSeries>
        </Series>
    </jqChart:Chart>
    </form>
</body>
</html>
