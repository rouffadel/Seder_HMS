<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="PaySlipNew.aspx.cs" Inherits="AECLOGIC.ERP.HMS.PaySlipNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divMenu" style="height: 25px; position: absolute; top: 60px; visibility: visible;
        width: 100px">
        <table border="0" cellpadding="0" cellspacing="0">
            <tbody>
                <tr>
                    <td>
                        <img border="0" alt="Print" onclick="javascript: divMenu.style.display='none'; window.print();  window.close();"
                            class="right" src="Images/print.png" /><img border="0" alt="Close" onclick="javascript: divMenu.style.display='none'; window.close();"
                                class="right" src="Images/close.png" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>


    <div align="center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" align="left">

            <tr>
            <td>
            <asp:Image ID="ImgLogo" runat="server" ImageUrl="~/Images/Logo.gif" />
            </td>
             <td>
            
            </td>
             <td>
            
            </td>
             <td>
            
            </td>
             <td>
            
            </td>
             <td>
            
            </td>
            </tr>

            <tr>
                <td align="center" colspan="6">
                    <b>
                        <asp:Label ID="lblCompanyName" runat="server"></asp:Label>
                    </b>
                    <br />
                    <b>PAY SLIP
                       
                    </b>
                </td>
            </tr>
           
           <tr>
               <td align="left">
               EMP NAME:&nbsp;
               <asp:Label ID="lblName" runat="server"></asp:Label>
               </td>
                <td align="left">
               
               
               </td>
                <td align="left">
               DESIGN:
                <asp:Label ID="lbldesig" runat="server"></asp:Label>
               </td>
           <td align="left">
               
               
               </td>
                <td align="left">
               DEPT:
               <asp:Label ID="lblDept" runat="server"></asp:Label>
               </td>
                <td align="left">
               
               
               </td>
           </tr>

            <tr>
               <td align="left">
               Bank A/c No :
               <asp:Label ID="lblbankACno" runat="server"></asp:Label>
               </td>
                <td align="left">
               Bank Name  
               
               </td>
                <td align="left">
               Pay Mode: 
               
               </td>
           <td align="left">
               Month:
               
               </td>
                <td align="left">
               No. of Days Worked:
               <asp:Label ID="lblNODW" runat="server"></asp:Label>
               </td>
                <td align="left">
               
               LOP:
               </td>
           </tr>
            <tr>
               <td colspan="2" align="center">
               EARNINGS
               
               </td>
               
                <td  colspan="2" align="center">
               REIMBURSEMENTS
               
               </td>
          
               
                <td  colspan="2" align="center">
               DEDUCTIONS
               
               </td>
           </tr>
            <tr>
               <td align="left">
               
               DETAILS
               </td>
                <td align="left">
               AMOUNT in Rupees
               
               </td>
                <td  align="left">
               
               DETAILS
               </td>
           <td  align="left">
               AMOUNT in Rupees
               
               </td>
                <td  align="left">
               DETAILS
               
               </td>
                <td  align="left">
               
               AMOUNT in Rupees
               </td>
           </tr>
            <tr>
               <td  align="left">
               BASIC PAY
               
               </td>
                <td  align="left">
               
               
               </td>
                <td  align="left">
               OVER TIME PAY
               
               </td>
           <td  align="left">
               
               
               </td>
                <td  align="left">
               SALARY ADVANCE
               
               </td>
                <td  align="left">
               
               
               </td>
           </tr>
            <tr>
               <td  align="left">
               H.R.A.
               
               </td>
                <td  align="left">
               
               
               </td>
                <td  align="left">
               SALARY ARREARS
               
               </td>
           <td  align="left">
               
               
               </td>
                <td  align="left">
               LOAN/ADVANCES
               
               </td>
                <td  align="left">
               
               
               </td>
           </tr>
            <tr>
               <td  align="left">
               TRANSPORT ALLOW.
               
               </td>
                <td  align="left">
               
               
               </td>
                <td  align="left">
               LEAVE SALARY
               
               </td>
           <td  align="left">
               
               
               </td>
                <td  align="left">
               
               LOSS OF PAY
               </td>
                <td  align="left">
               
               
               </td>
           </tr>
            <tr>
               <td  align="left">
               OTHER ALLOW.
               
               </td>
                <td  align="left">
               
               
               </td>
                <td  align="left">
               AIR TICKET
               
               </td>
           <td  align="left">
               
               
               </td>
                <td  align="left">
               FINES & PENALITIES
               
               </td>
                <td  align="left">
               
               
               </td>
           </tr>
            <tr>
               <td  align="left">
               SPECIAL ALLOW.
               
               </td>
                <td  align="left">
               
               
               </td>
                <td  align="left">
               OTHER REIMB. 
               
               </td>
           <td  align="left">
               
               
               </td>
                <td  align="left">
               
               OTHER Deductions
               (Misc. Ded & Salary Hold)
               </td>
                <td  align="left">
               
               
               </td>
           </tr>
            <tr>
               <td  align="left">
               Total Earnings: 
               
               </td>
                <td  align="left">
               
               
               </td>
                <td  align="left">
               Total Reimb. : 
               
               </td>
           <td  align="left">
               
               
               </td>
                <td  align="left">
               Total Deductions: 
               
               </td>
                <td  align="left">
               
               
               </td>
           </tr>
            <tr>
               <td  align="left">
               Net Salary in Words:   
               
               </td>
                <td  align="left">
               
               
               </td>
                <td  align="left">
               
               
               </td>
           <td  align="left">
               
               
               </td>
                <td  align="left">
               NET SALARY:
               
               </td>
                <td  align="left">
               
               
               </td>
           </tr>
            <tr>
               <td colspan="6"  align="left">
               Remarks:  
               
               </td>
                
           </tr>

            <tr>
               <td  align="left">
               Date:
               
               </td>
                <td>
               
               
               </td>
               
           <td colspan="4"  align="right">
                Note: This is a system generated payslip and does not require authorization
               
               </td>
               
           </tr>
            <tr>
               <td>
               
               
               </td>
                <td>
               
               
               </td>
                <td>
               
               
               </td>
           <td>
               
               
               </td>
                <td>
               
               
               </td>
                <td>
               
               
               </td>
           </tr>
            <tr>
               <td>
               
               
               </td>
                <td>
               
               
               </td>
                <td>
               
               
               </td>
           <td>
               
               
               </td>
                <td>
               
               
               </td>
                <td>
               
               
               </td>
           </tr>
            

            <tr>
                <td colspan="6" align="left">
                   
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
