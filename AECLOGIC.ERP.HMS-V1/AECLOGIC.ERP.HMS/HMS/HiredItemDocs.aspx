<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="HiredItemDocs.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Default4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quotations</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DataList ID="dlQuotations" runat="server" OnItemCommand="dlQuotations_ItemCommand">
            <HeaderTemplate>
                <table>
                    <tr>
                        <td>
                            <strong>Hired Documents</strong>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblfile" runat="server" Text='<%#Eval("fileName") %>'></asp:Label>
                          
                        </td>
                        <td>
                           
                            <a id="lnkFile" runat="server" target="_blank" href='<%# ShowQuotations(DataBinder.Eval(Container.DataItem,"fileName").ToString()) %>'>
                                View</a>
                        </td>
                    </tr>
                </table>
               
            </ItemTemplate>
        </asp:DataList>
        <asp:Label ID="lblEmptyData" runat="server" Visible="false" 
            style="color: #FF0000" ></asp:Label>
    </div>
    </form>
</body>
</html>
