﻿ <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function alertMessage() {
            alert("yes")

        }
    </script>
    
    <style>
        body{
            display: flex;
            justify-content: center;
        }
        
        .label{
            font-size:  20px;
        }


        .items-gap{
            display:flex;
            justify-content:space-evenly;
        }

        .ops-button{
            border: none;
            background-color: bisque;
            font-weight:bold;
            padding:10px;
            margin: 10px 0;
            border-radius: 5px;
        }
        

    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="margin-bottom:50px; row-gap:30px">
                <tr class="row">
                    <td class="label">
                        Name:
                    </td>
                    <td style="column-span:all">
                        <asp:TextBox ID ="emp_name" runat ="server"></asp:TextBox>
                    </td>
                </tr>
                <tr class="row">
                    <td class="label">
                        Designation:
                    </td>
                    <td>
                        <asp:TextBox ID ="job_desg" runat ="server"></asp:TextBox>
                    </td>
                </tr>
                <tr  class="row">
                    <td class="label">
                        Manager ID:
                    </td>
                    <td>
                        <asp:TextBox ID ="mgr_id" runat ="server"></asp:TextBox>
                    </td>
                </tr>

                <tr class="row">
                    <td class="label">
                        Department No.:
                    </td>
                    <td>
                        <asp:TextBox ID ="dept_no" runat ="server"></asp:TextBox>
                    </td>

                    <td class="label">
                        Salary:
                    </td>
                    <td>
                        <asp:TextBox ID ="salary" runat ="server"></asp:TextBox>
                    </td>
                </tr>

                <tr class="row">
                    <td>
                        <asp:Button ID="insert_btn" OnClick="insert_btn_Click" Text="Insert" runat="server"/>
                   </td>
                   <%-- <td>
                        <asp:Button ID="update_btn"  Text="Update" runat="server"/>
                    </td>
                   <td>
                        <asp:Button ID="delete_btn" Text="Delete" runat="server"/>
                    </td>--%>
                </tr>
            </table>

            <div style="overflow-y:scroll;height:700px;">
                <asp:GridView ID="table_data_grid" style="border-block-color:gainsboro " runat="server" AutoGenerateColumns="False" 
                    AutoGenerateDeleteButton="True" OnRowEditing="table_data_grid_RowEditing" 
                    OnRowUpdating ="table_data_grid_RowUpdating" OnRowCancelingEdit="table_data_grid_RowCancelingEdit" 
                    OnRowDeleting="table_data_grid_RowDeleting" OnRowDataBound ="table_data_grid_RowDataBound" BackColor="White" 
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <Columns >
                        <%--<asp:BoundField DataField="Emp_no" HeaderText="Employee ID" />
                        <asp:BoundField DataField="Emp_name" HeaderText="Employee Name" />
                        <asp:BoundField DataField="Job" HeaderText=" Designation" />
                        <asp:BoundField DataField="Manager" HeaderText="Manager ID" />
                        <asp:BoundField DataField="Dept_no" HeaderText="Department No" />
                        <asp:BoundField DataField="Salary" HeaderText="Salary" />--%>


                        <asp:TemplateField HeaderText="Edit Option" HeaderStyle-Width="200" ItemStyle-HorizontalAlign="Center" 
                            ItemStyle-CssClass="items-gap" ControlStyle-BorderColor="White">
                            <ItemTemplate>
                                     <asp:Button CssClass="ops-button" ID="btn_Edit" runat="server" CausesValidation="False"
                                         CommandName="Edit" Text="Edit"></asp:Button>
                                    
                            </ItemTemplate >
                            <EditItemTemplate>
                                <asp:Button CssClass="ops-button" ID="btn_Update" runat="server" Text="Update" 
                                    CommandName="Update"></asp:Button>
                                <asp:Button CssClass="ops-button" ID="btn_Cancel" runat="server" Text="Cancel" 
                                    CommandName="Cancel"></asp:Button>
                                <%--<asp:LinkButton ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete"></asp:LinkButton>--%>
                                
                            </EditItemTemplate>

                            <ControlStyle BorderColor="White"></ControlStyle>

                            <HeaderStyle Width="200px"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Center" CssClass="items-gap"></ItemStyle>
                        </asp:TemplateField>




                        <asp:TemplateField HeaderText="Employee Id" HeaderStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate >
                                <asp:Label ID="lbl_emp_no" runat="server" Text='<%# Bind("Emp_no") %>'>
                                    
                                </asp:Label>

                            </ItemTemplate>
                            
                          <%--  <EditItemTemplate>
                                <asp:label ID="txt_Emp_no" runat="server" Text='<%# Bind("Emp_no") %>'></asp:label>
                            </EditItemTemplate>--%>


                        <HeaderStyle Width="50px"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>

                        </asp:TemplateField>




                        <asp:TemplateField HeaderText="Name" HeaderStyle-Width="250" >
                            <ItemTemplate >
                                <asp:Label ID="lbl_Emp_name" runat="server" Text='<%# Bind("Emp_name") %>'></asp:Label>
                            </ItemTemplate>

                            <EditItemTemplate>
                                <asp:TextBox ID="txt_Emp_name" runat="server" Text='<%# Bind("Emp_name") %>'></asp:TextBox>
                            </EditItemTemplate>

                            <HeaderStyle Width="250px"></HeaderStyle>

                        </asp:TemplateField>




                        <asp:TemplateField HeaderText="Designation" HeaderStyle-Width="250">
                            <ItemTemplate >
                                <asp:Label ID="lbl_Designation" runat="server" Text='<%# Bind("job") %>'></asp:Label>
                                <%--<asp:Label ID="lbl_emp_name" runat="server" Text='<%# Bind("Job") %>'></asp:Label>--%>
                            </ItemTemplate>

                            <EditItemTemplate>
                                <asp:TextBox ID="txt_Designation" runat="server" Text='<%# Bind("Job") %>'></asp:TextBox>
                            </EditItemTemplate>

                            <HeaderStyle Width="250px"></HeaderStyle>

                        </asp:TemplateField>


                        

                        <asp:TemplateField HeaderText="Manager ID" HeaderStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate >
                                <asp:Label ID="lbl_mgr_id" runat="server" Text='<%# Bind("Manager") %>'></asp:Label>
                                <%--<asp:Label ID="lbl_emp_name" runat="server" Text='<%# Bind("Job") %>'></asp:Label>--%>
                                
                            </ItemTemplate>

                            <EditItemTemplate>
                                <asp:TextBox ID="txt_mgr_id" runat="server" Text='<%# Bind("Manager") %>'></asp:TextBox>
                            </EditItemTemplate>

                            <HeaderStyle Width="120px"></HeaderStyle>
                            
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>

                        </asp:TemplateField>




                        <asp:TemplateField HeaderText="Department No" HeaderStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate >
                                <asp:Label ID="lbl_dept_no" runat="server" Text='<%# Bind("dept_no") %>'></asp:Label>
                                <%--<asp:Label ID="lbl_emp_name" runat="server" Text='<%# Bind("Job") %>'></asp:Label>--%>
                                
                            </ItemTemplate>


                            <EditItemTemplate>

                                <asp:TextBox ID="txt_dept_no" runat="server" Text='<%# Bind("Dept_no") %>'></asp:TextBox>
                            
                            </EditItemTemplate>

                            <HeaderStyle Width="120px"></HeaderStyle>
                            
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>

                        </asp:TemplateField>




                        <asp:TemplateField HeaderText="Salary" HeaderStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate >
                                <asp:Label ID="lbl_Salary" runat="server" Text='<%# Bind("salary") %>'></asp:Label>
                                <%--<asp:Label ID="lbl_emp_name" runat="server" Text='<%# Bind("Job") %>'></asp:Label>--%>
                                
                            </ItemTemplate>


                            <EditItemTemplate>

                                <asp:TextBox ID="txt_Salary" runat="server" Text='<%# Bind("salary") %>'></asp:TextBox>
                            
                            </EditItemTemplate>

                            <HeaderStyle Width="120px"></HeaderStyle>
                            
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>

                        </asp:TemplateField>



                    </Columns>
                  
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                  
                </asp:GridView>

                
            </div>
        </div>




        <%--<script>
            ScriptManager.RegisterStartupScript(Control control, Type type, string key, string script, bool addScriptTags);

        </script>--%>
     
        
    </form>
</body>
</html>
