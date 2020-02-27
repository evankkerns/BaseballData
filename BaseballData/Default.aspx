<%@ Page Title="Kerns: BaseballData" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <meta charset="utf-8">
    <script src="Scripts/jquery-1.10.2.js"></script>
    <script src="https://d3js.org/d3.v4.js"></script>
</asp:Content>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" CssClass="styleclass">
    <style>
        body {
            background-color: #DED6D0;
        }
    </style>
    <div class="jumbotron" style="background-color: #473729;">
        <h1 style="color: #FFC72C;">Baseball Data</h1>
        <form method="post" name="ChooseChartForm" action="/Default.aspx">
            <select name="SelectChart">
                <option value="era" <% If UCase(HttpContext.Current.Request.Form("SelectChart")) = "ERA" Then Response.Write("selected") %>>ERA</option>
                <option value="whip" <% If UCase(HttpContext.Current.Request.Form("SelectChart")) = "WHIP" Then Response.Write("selected") %>>WHIP</option>
                <option value="innings" <% If UCase(HttpContext.Current.Request.Form("SelectChart")) = "INNINGS" Then Response.Write("selected") %>>Innings Pitch</option>
                <option value="k" <% If UCase(HttpContext.Current.Request.Form("SelectChart")) = "K" Then Response.Write("selected") %>>Strikeouts</option>
                <option value="kPerc" <% If UCase(HttpContext.Current.Request.Form("SelectChart")) = "KPERC" Then Response.Write("selected") %>>K%</option>
                <option value="bbPerc" <% If UCase(HttpContext.Current.Request.Form("SelectChart")) = "BBPERC" Then Response.Write("selected") %>>BB%</option>
                <option value="kbb" <% If UCase(HttpContext.Current.Request.Form("SelectChart")) = "KBB" Then Response.Write("selected") %>>K/BB</option>
                <option value="avg" <% If UCase(HttpContext.Current.Request.Form("SelectChart")) = "AVG" Then Response.Write("selected") %>>AVG</option>
                <option value="obp" <% If UCase(HttpContext.Current.Request.Form("SelectChart")) = "OBP" Then Response.Write("selected") %>>OBP</option>
                <option value="slug" <% If UCase(HttpContext.Current.Request.Form("SelectChart")) = "SLUG" Then Response.Write("selected") %>>SLUG</option>
                <option value="velocity" <% If UCase(HttpContext.Current.Request.Form("SelectChart")) = "VELOCITY" Then Response.Write("selected") %>>Velocity</option>
                <option value="pitchPerc" <% If UCase(HttpContext.Current.Request.Form("SelectChart")) = "PITCHPERC" Then Response.Write("selected") %>>Pitch Percentage</option>
                <option value="zoneLoc" <% If UCase(HttpContext.Current.Request.Form("SelectChart")) = "ZONELOC" Then Response.Write("selected") %>>Zone Location</option>
                <option value="relLoc" <% If UCase(HttpContext.Current.Request.Form("SelectChart")) = "RELLOC" Then Response.Write("selected") %>>Release Location</option>
                <option value="stats" <% If UCase(HttpContext.Current.Request.Form("SelectChart")) = "STATS" Then Response.Write("selected") %>>Text Stats</option>
            </select>

            <select name="SelectSoG">
                <% If UCase(HttpContext.Current.Request.Form("SelectSoG")) = "GAME" THEN %>
                <option value="season">Season</option>
                <option value="game" selected>Game</option>
                <% Else %>
                <option value="season" selected>Season</option>
                <option value="game">Game</option>
                <% End If %>
            </select>

            <select name="SelectPitcher">
                <option value="all" <% If UCase(HttpContext.Current.Request.Form("SelectPitcher")) = "ALL" Then Response.Write("selected") %>>All Pitchers</option>
                <option value="1" <% If HttpContext.Current.Request.Form("SelectPitcher") = "1" Then Response.Write("selected") %>>Pitcher 1</option>
                <option value="2" <% If HttpContext.Current.Request.Form("SelectPitcher") = "2" Then Response.Write("selected") %>>Pitcher 2</option>
                <option value="3" <% If HttpContext.Current.Request.Form("SelectPitcher") = "3" Then Response.Write("selected") %>>Pitcher 3</option>
                <option value="4" <% If HttpContext.Current.Request.Form("SelectPitcher") = "4" Then Response.Write("selected") %>>Pitcher 4</option>
            </select>

            <input type="submit" value="View" />
        </form>
    </div>
    
    <br />
    <div id="my_dataviz"></div>
    <div id="my_dataviz0"></div>
    <div id="my_dataviz1"></div>
    <div id="my_dataviz2"></div>
    <div id="my_dataviz3"></div>
    <div id="my_dataviz4"></div>
    <div id="my_dataviz5"></div>
    <div id="my_dataviz6"></div>
    <div id="my_dataviz7"></div>
    <div id="my_dataviz8"></div>
    <div id="my_dataviz9"></div>
    <div id="my_dataviz10"></div>
    <div id="my_dataviz11"></div>
    <div id="my_dataviz12"></div>
    <div id="my_dataviz13"></div>
    <div class = "graph" id="wordCountContainer"></div>

    <%
        If HttpContext.Current.Request.Form("SelectChart") <> "" Then
            Response.Write(PrintCharts.ChooseChart(UCase(HttpContext.Current.Request.Form("SelectChart")), UCase(HttpContext.Current.Request.Form("SelectSoG")), UCase(HttpContext.Current.Request.Form("SelectPitcher"))))
        End If
        'FormatScripts.GamesToCsvScript()
        'FormatScripts.SeasonToCsvScript()
    %>

</asp:Content>
