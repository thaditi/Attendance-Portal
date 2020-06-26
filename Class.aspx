<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Class.aspx.cs" Inherits="_Class" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
   
<head runat="server">
    
     <style>
        #class {
        padding:50px 0 50px 30px;
        height:50px;
        }

         #Add_panel {
         padding: 70px;
         }
       

    </style>
    <title>Attendance Sheet</title>
</head>
<body>
    <form id="form1" runat="server">

        <%--class dll--%>
    <div>
    <asp:Panel id="classpanel" runat="server" GroupingText="Select Your Class">
        <asp:DropDownList ID="ddlSections"  runat="server"  >
        
              </asp:DropDownList>

                <asp:Button id="btnSubmitClass" Text="Submit" OnClick="btnSubmitC_Click" Runat="server" />
        <asp:Button id="btnAddClass" Text="Add New Class" OnClick="btnAddC_Click" Runat="server" />


        <asp:Panel id="Add_Class" runat="server" Visible="false">
     <asp:Label id="lblnewClass" Text="New Class" AssociatedControlID="newClass" Runat="server" />
     <asp:TextBox id="newClass" Runat="server"  />
        <asp:Button id="btnAddC" Text="Add" OnClick="btnAddnewClass" Runat="server"/>

        </asp:Panel>

        <asp:Label ID="class_success" runat="server"></asp:Label>
        </asp:Panel>





        <%--Teacher ddl--%>
        <asp:Panel id="teacherpanel"  runat="server" GroupingText="Select Your teacher" Visible="false">
        <asp:DropDownList ID="ddlteacher" AutoPostBack="true"  runat="server" >
            
            </asp:DropDownList>

            

        <asp:Button id="btnSumbitTeach" Text="Submit" OnClick="btnSubmitT_Click" Runat="server" />
        <asp:Button id="btnAddTeach" Text="Add New Teacher" OnClick="btnAddT_Click" Runat="server" />



            
        <asp:Panel id="Add_Teach" runat="server" Visible="false">
     <asp:Label id="lblnewTeach" Text="New Teacher" AssociatedControlID="newTeach" Runat="server" />
     <asp:TextBox id="newTeach" Runat="server"  />
        <asp:Button id="btnAddT" Text="Add" OnClick="btnAddnewTeach" Runat="server" />

        </asp:Panel>

            </asp:Panel>
    </div>





        <%--Attendence gridview--%>
        
        <br />
        <asp:GridView ID="GridView1" onrowdatabound="GridView1_RowDataBound" runat="server"  Visible="False" AutoGenerateColumns="false" >
            <Columns>
<asp:BoundField DataField="rollno" HeaderText="Roll No." />
<asp:BoundField DataField="studentName" HeaderText="Student Name" />
<asp:BoundField DataField="attadate" HeaderText="Date" DataFormatString="{0:d}"/>
 <asp:templatefield HeaderText="Present">
                    <itemtemplate>
                        <asp:checkbox ID="present"  runat="server"></asp:checkbox>
                    </itemtemplate>
                </asp:templatefield>
</Columns>
           
           
        </asp:GridView>
        <br />
         <asp:Button ID="btnAddStud" runat="server" Text="Add New Student" OnClick="btnAddS_Click" Visible="false" />

        <asp:Panel id="studpanel" runat="server" Visible="false">
     <asp:Label id="L1" Text=" Roll Number" AssociatedControlID="newroll" Runat="server" />
     <asp:TextBox id="newroll" Runat="server"  />

      <asp:Label id="L2" Text=" Name" AssociatedControlID="newname" Runat="server" />
     <asp:TextBox id="newname" Runat="server"  />
        <asp:Button id="AddStud" Text="Add" OnClick="btnAddnewStud" Runat="server" />

        </asp:Panel>

        <asp:Button ID="SaveAttendance" runat="server" Text="Save Attendance" OnClick="Save_Attendance" Visible="false" />
       
        
    </form>
</body>
</html>

