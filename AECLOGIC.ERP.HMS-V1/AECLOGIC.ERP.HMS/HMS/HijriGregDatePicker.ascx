<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HijriGregDatePicker.ascx.cs" Inherits="AECLOGIC.ERP.COMMON.HijriGregDatePicker" %>

<style type="text/css">

    .whole_calendar /* whole Calendar Header Div Style without textboxes*/
    {
        width: 280px;
        height: 180px;
        position:absolute;            
        border-style:solid;
        border-width:1px;       
        border-color:black;       
        overflow:auto;          
        background-color: #E6E6FA;  
    }
        
    #Header_Div         /* Calendar Header Div Style */
    {
        position:relative;
	    height: 20px;
	    padding-top:5px;
	    padding-bottom:10px;
	    vertical-align: middle;
	    background-color: #E6E6FA;
    }        
    .clsText
    {
        font-family: Tahoma;
        font-size: 8pt;
        width: 50px;
        border: solid 1px #000;
    }

    .fldLabel
    {
        font-family: Tahoma;
        color: #58595b;
        font-size: 8pt;
        width: 50px;
    }

    .ctlHeader
    {
        padding-left:5px;
        padding-right:5px;
    }
    .clsDDL
    {
        font-size: 10px;
        color: black;
        vertical-align: bottom;
        padding: 0;
        border: 1px #b5b7b9 solid;
        font-family:Tahoma;
    }
    #clsLocCalendar
    {
        padding-right:2px;
        padding-left:2px;
        text-align:center;
    }

    #Progress_bar
    {
        width: 280px;
        height: 180px;
        position:absolute;            
        border-style:solid;
        border-width:1px;       
        border-color:black;       
        overflow:auto;          

    }

</style>
 
    <script type="text/javascript">
        //To Show hide the div when user click on calendar image or the date text boxes
        function showHide(div) {
            if (document.getElementById(div).style.display == "none") {
                document.getElementById(div).style.display = "block";
            }
            else { document.getElementById(div).style.display = "none"; }
        }
    </script>

<script type="text/javascript">

    //to hide the date picker when user click outside the date picker box
    document.onclick = function (e) {
        e = e || event
        var target = e.target || e.srcElement
        var box = document.getElementById('<% =this.whole_calendar.ClientID %>')
        var imgCal = document.getElementById('<% =this.imgCalendar.ClientID %>')
        var txtHijri = document.getElementById('<% =this.txtHijri.ClientID %>')
        var txtGreg = document.getElementById('<% =this.txtGreg.ClientID %>')
        do {
            if (box == target | imgCal == target | txtHijri == target | txtGreg == target) {
                // Click occured inside the box, do nothing.
                return
            }
            target = target.parentNode
        }
        while (target)
        // Click was outside the box, hide it.
        box.style.display = "none"
    }

</script>

<asp:UpdatePanel ID="CalendarUpdatePanel" runat="server">
    <ContentTemplate>

        <table style="width: 160px;">
            <tr>
                <td>
                    <asp:TextBox ID="txtHijri" runat="server" ReadOnly="true"  style="width:75px"></asp:TextBox><br />
                    <asp:Label ID="lblHijri" runat="server" Text="Hijri" style="width:75px" ></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtGreg" runat="server" ReadOnly="true" WatermarkText="[Gregorian]" style="width:75px" ></asp:TextBox><br />
                    <asp:Label ID="lblGregorian" runat="server" Text="Gregorian" style="width:75px" ></asp:Label>
                </td>
                <td style="vertical-align:center;">
                    <asp:Image  ID="imgCalendar" AlternateText="calendar" ImageUrl="~/Images/Calendar_scheduleHS.png" runat="server" />
                </td>
            </tr>
        </table>

        <div id="whole_calendar" runat="server"  class="whole_calendar" >
            <div id="Header_Div">
                <table>
                    <tr>
                        <td class="ctlHeader">
                            <asp:DropDownList ID="ddlLocaleChoice" runat="server" width="77px" AutoPostBack="True" 
                            OnSelectedIndexChanged="ddlLocaleChoice_SelectedIndexChanged" CssClass="clsDDL">
                                <asp:ListItem Value="ar-SA">Hijri</asp:ListItem>
                                <asp:ListItem Value="en-US">Gregorian</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td class="ctlHeader">
                            <asp:DropDownList ID="ddlYear" runat="server" width="77px" AutoPostBack="True" 
                                OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" CssClass="clsDDL">
                            </asp:DropDownList>            
                        </td>
                        <td class="ctlHeader">
                            <asp:DropDownList ID="ddlMonth" runat="server" width="83px" AutoPostBack="True" 
                                OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" CssClass="clsDDL">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="clsLocCalendar">
                <asp:Calendar ID="ctlCalendarLocalized" runat="server" BackColor="White" 
                    BorderColor="White" 
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="125px"  
                    Width="275px" BorderWidth="1px" NextPrevFormat="FullMonth" 
                    ShowDayHeader="True" ShowNextPrevMonth="False"
                    OnSelectionChanged="ctlCalendarLocalized_SelectionChanged">
                    <DayHeaderStyle Font-Bold="True" Font-Size="7pt"  Font-Names="Tahoma" />
                    <NextPrevStyle VerticalAlign="Bottom" Font-Bold="True" Font-Size="8pt" 
                        ForeColor="#333333" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                    <TitleStyle BackColor="White" BorderColor="Black" Font-Bold="True" 
                        BorderWidth="1px" Font-Size="9pt" ForeColor="#333399" BorderStyle="Ridge" Font-Names="sans-serif" />
                    <TodayDayStyle BackColor="#CCCCCC" />
                </asp:Calendar>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdateProgress ID="CalendarUpdateProgress" runat="server" AssociatedUpdatePanelID="CalendarUpdatePanel"  DynamicLayout ="true">
    <ProgressTemplate>
        <div id="Progress_bar" > 
            <center>
                <img id="Img1" src="Images/ajax-loader.gif" runat="server" />
            </center>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>