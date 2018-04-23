using System;
using System.Web.UI;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Web.ASPxEditors;
  
public class MyTemplate : ITemplate
{

    #region ITemplate Members

    public void InstantiateIn(Control container)
    {
        ASPxLabel label = new ASPxLabel();
        label.ID = "label";
        TreeListEditCellTemplateContainer templateContainer = container as TreeListEditCellTemplateContainer;
        label.Text = templateContainer.Text;
        label.Width = System.Web.UI.WebControls.Unit.Percentage(100);
        label.ClientSideEvents.Click = "function(s, e){onCellClick(" + templateContainer.NodeKey + ", '" + templateContainer.Column.FieldName + "'); }";
        container.Controls.Add(label);
    }
    #endregion
}
