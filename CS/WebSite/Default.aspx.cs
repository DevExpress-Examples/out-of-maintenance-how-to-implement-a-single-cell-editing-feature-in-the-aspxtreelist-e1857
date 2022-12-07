using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Web;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ASPxTreeList1.IsEditing && Session["column"] != null)
        {
            string fieldName = Convert.ToString(Session["column"]);
            for (int i = 0; i < ASPxTreeList1.Columns.Count; i++)
            { 
                TreeListDataColumn column = (TreeListDataColumn)ASPxTreeList1.Columns[i];
                if (column != null && column.FieldName != fieldName)
                    column.EditCellTemplate = new MyTemplate();
            }
        }

    }
    protected void ASPxTreeList1_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
    {
        e.Cell.Attributes.Add("onclick", "onCellClick(" + e.NodeKey + ", '" + e.Column.FieldName + "')");
    }

    protected void ASPxTreeList1_CustomCallback(object sender, TreeListCustomCallbackEventArgs e)
    {
        ASPxTreeList treeList = (ASPxTreeList)sender;
        treeList.UpdateEdit();
        
        string[] data = e.Argument.Split(new char[] { '|' });
        treeList.FindNodeByKeyValue(data[0]).Focus();
        for (int i = 0; i < treeList.Columns.Count; i++)
        {
            TreeListDataColumn column = (TreeListDataColumn)treeList.Columns[i];
            if (column != null)
                if (column.FieldName != data[1])
                    column.EditCellTemplate = new MyTemplate();
                else
                {
                    Session["column"] = column.FieldName;
                    column.EditCellTemplate = null;
                }
        }
        treeList.StartEdit(data[0]);

    }
    protected void ASPxTreeList1_CellEditorInitialize(object sender, TreeListColumnEditorEventArgs e)
    {
        e.Editor.Enabled = !e.Column.ReadOnly;
        if (e.Editor.Enabled)
        {
            ((ASPxTextBox)e.Editor).ClientSideEvents.KeyPress = "function(s,e) {OnEditorKeyPress(s, e);}";            
        }

    }
    protected void ASPxTreeList1_NodeUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxTreeList treeList = (ASPxTreeList)sender;
        for (int i = 0; i < treeList.Columns.Count; i++)
            if (treeList.Columns[i] is TreeListDataColumn)
            {
                ((TreeListDataColumn)treeList.Columns[i]).EditCellTemplate = null;
                Session["column"] = null;
            }
        treeList.CancelEdit();
        

    }
}
