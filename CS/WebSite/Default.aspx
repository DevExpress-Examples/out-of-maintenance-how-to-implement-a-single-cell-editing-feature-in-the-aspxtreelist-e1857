<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTreeList" tagprefix="dxwtl" %>
<%@ Register assembly="DevExpress.Web.v15.1, Version=15.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function onCellClick(nodeKey, fieldName) {
            treeList.PerformCallback(nodeKey + "|" + fieldName);
        }

        function OnEditorKeyPress(editor, e) {
            
            if (e.htmlEvent.keyCode == 13 || e.htmlEvent.keyCode == 9) {
                treeList.UpdateEdit();                
            }
            else
                if (e.htmlEvent.keyCode == 27)
                    treeList.CancelEdit();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dxwtl:ASPxTreeList ID="ASPxTreeList1" runat="server"  ClientInstanceName="treeList"        
            AutoGenerateColumns="False" DataSourceID="AccessDataSource1" 
            KeyFieldName="ProductID" oncustomcallback="ASPxTreeList1_CustomCallback" 
            onhtmldatacellprepared="ASPxTreeList1_HtmlDataCellPrepared" 
            ParentFieldName="CategoryID" 
            oncelleditorinitialize="ASPxTreeList1_CellEditorInitialize"              
            onnodeupdated="ASPxTreeList1_NodeUpdated" Width="545px">
            <SettingsBehavior AllowFocusedNode="True" />
            <Columns>
                <dxwtl:TreeListTextColumn FieldName="ProductName" VisibleIndex="0">
                </dxwtl:TreeListTextColumn>
                <dxwtl:TreeListTextColumn FieldName="CategoryID" VisibleIndex="1" 
                    ReadOnly="True">
                </dxwtl:TreeListTextColumn>
                <dxwtl:TreeListTextColumn FieldName="UnitPrice" VisibleIndex="2">
                </dxwtl:TreeListTextColumn>
                <dxwtl:TreeListTextColumn FieldName="UnitsInStock" VisibleIndex="3">
                </dxwtl:TreeListTextColumn>
                <dxwtl:TreeListTextColumn FieldName="UnitsOnOrder" VisibleIndex="4">
                </dxwtl:TreeListTextColumn>
            </Columns>
        </dxwtl:ASPxTreeList>
        <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
            DataFile="~/App_Data/nwind.mdb" 
            SelectCommand="SELECT [ProductID], [ProductName], [CategoryID], [UnitPrice], [UnitsInStock], [UnitsOnOrder] FROM [Products]" >
           <%-- Read only --%>
           <%-- DeleteCommand="DELETE FROM [Products] WHERE [ProductID] = ?" --%>
           <%-- InsertCommand="INSERT INTO [Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [UnitsInStock], [UnitsOnOrder]) VALUES (?, ?, ?, ?, ?, ?)" --%>
           <%-- UpdateCommand="UPDATE [Products] SET [ProductName] = ?, [CategoryID] = ?, [UnitPrice] = ?, [UnitsInStock] = ?, [UnitsOnOrder] = ? WHERE [ProductID] = ?" --%>
            <DeleteParameters>
                <asp:Parameter Name="ProductID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="ProductName" Type="String" />
                <asp:Parameter Name="CategoryID" Type="Int32" />
                <asp:Parameter Name="UnitPrice" Type="Decimal" />
                <asp:Parameter Name="UnitsInStock" Type="Int16" />
                <asp:Parameter Name="UnitsOnOrder" Type="Int16" />
                <asp:Parameter Name="ProductID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="ProductID" Type="Int32" />
                <asp:Parameter Name="ProductName" Type="String" />
                <asp:Parameter Name="CategoryID" Type="Int32" />
                <asp:Parameter Name="UnitPrice" Type="Decimal" />
                <asp:Parameter Name="UnitsInStock" Type="Int16" />
                <asp:Parameter Name="UnitsOnOrder" Type="Int16" />
            </InsertParameters>
        </asp:AccessDataSource>
    
    </div>
    <div style="visibility:hidden">
            <input id="inputIE" type="text"/>
        </div>

    </form>
    </body>
</html>
