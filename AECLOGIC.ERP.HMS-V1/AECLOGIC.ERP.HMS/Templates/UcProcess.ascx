<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcProcess.ascx.cs" Inherits="AECLOGIC.ERP.COMMON.UcProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<style type="text/css">
    #crumbs {
        width: 82%;
        text-align: center;
        margin: 0;
        padding: 0;
        border: 0;
    }

        #crumbs ul {
            list-style: none;
            display: inline-table;
        }

            #crumbs ul li {
                display: inline;
            }

                #crumbs ul li a {
                    display: block;
                    float: left;
                    height: 40px;
                    background: #3498db;
                    text-align: center;
                    padding: 20px 20px 20px 20px;
                    position: relative;
                    margin: 0 5px 0 0;
                    font-size: 12px;
                    text-decoration: none;
                    color: #fff;
                }

                    #crumbs ul li a:after {
                        content: "";
                        border-top: 20px solid transparent;
                        border-bottom: 20px solid transparent;
                        border-left: 20px solid #3498db;
                        position: absolute;
                        right: -20px;
                        top: 0;
                        z-index: 1;
                    }

                    #crumbs ul li a:before {
                        content: "";
                        border-top: 20px solid transparent;
                        border-bottom: 20px solid transparent;
                        border-left: 20px solid #d4f2ff;
                        position: absolute;
                        left: 0;
                        top: 0;
                    }

                #crumbs ul li:first-child a {
                    border-top-left-radius: 10px;
                    border-bottom-left-radius: 10px;
                }

                    #crumbs ul li:first-child a:before {
                        display: none;
                    }

                #crumbs ul li:last-child a {
                    padding-right: 15px;
                    border-top-right-radius: 10px;
                    border-bottom-right-radius: 10px;
                }

                    #crumbs ul li:last-child a:after {
                        display: none;
                    }

                #crumbs ul li a:hover {
                    background: #fa5ba5;
                }

                    #crumbs ul li a:hover:after {
                        border-left-color: #fa5ba5;
                    }
</style>
<div>   
    <div id="divPath_wfProcesses" runat="server">        
        <div id="crumbs">       
        </div>
    </div>
    <asp:Label runat="server" ID="lblError"></asp:Label>
</div>
