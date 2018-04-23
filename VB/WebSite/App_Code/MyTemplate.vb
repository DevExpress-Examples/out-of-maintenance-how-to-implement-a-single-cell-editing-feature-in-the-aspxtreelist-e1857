Imports Microsoft.VisualBasic
Imports System
Imports System.Web.UI
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web.ASPxEditors

Public Class MyTemplate
	Implements ITemplate

	#Region "ITemplate Members"

	Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
		Dim label As New ASPxLabel()
		label.ID = "label"
		Dim templateContainer As TreeListEditCellTemplateContainer = TryCast(container, TreeListEditCellTemplateContainer)
		label.Text = templateContainer.Text
		label.Width = System.Web.UI.WebControls.Unit.Percentage(100)
		label.ClientSideEvents.Click = "function(s, e){onCellClick(" & templateContainer.NodeKey & ", '" & templateContainer.Column.FieldName & "'); }"
		container.Controls.Add(label)
	End Sub
	#End Region
End Class
