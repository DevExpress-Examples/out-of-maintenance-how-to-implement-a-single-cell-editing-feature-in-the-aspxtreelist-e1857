Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		If ASPxTreeList1.IsEditing AndAlso Session("column") IsNot Nothing Then
			Dim fieldName As String = Convert.ToString(Session("column"))
			For i As Integer = 0 To ASPxTreeList1.Columns.Count - 1
				Dim column As TreeListDataColumn = CType(ASPxTreeList1.Columns(i), TreeListDataColumn)
				If column IsNot Nothing AndAlso column.FieldName <> fieldName Then
					column.EditCellTemplate = New MyTemplate()
				End If
			Next i
		End If

	End Sub
	Protected Sub ASPxTreeList1_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As TreeListHtmlDataCellEventArgs)
		e.Cell.Attributes.Add("onclick", "onCellClick(" & e.NodeKey & ", '" & e.Column.FieldName & "')")
	End Sub

	Protected Sub ASPxTreeList1_CustomCallback(ByVal sender As Object, ByVal e As TreeListCustomCallbackEventArgs)
		Dim treeList As ASPxTreeList = CType(sender, ASPxTreeList)
		treeList.UpdateEdit()

		Dim data() As String = e.Argument.Split(New Char() { "|"c })
		treeList.FindNodeByKeyValue(data(0)).Focus()
		For i As Integer = 0 To treeList.Columns.Count - 1
			Dim column As TreeListDataColumn = CType(treeList.Columns(i), TreeListDataColumn)
			If column IsNot Nothing Then
				If column.FieldName <> data(1) Then
					column.EditCellTemplate = New MyTemplate()
				Else
					Session("column") = column.FieldName
					column.EditCellTemplate = Nothing
				End If
			End If
		Next i
		treeList.StartEdit(data(0))

	End Sub
	Protected Sub ASPxTreeList1_CellEditorInitialize(ByVal sender As Object, ByVal e As TreeListColumnEditorEventArgs)
		e.Editor.Enabled = Not e.Column.ReadOnly
		If e.Editor.Enabled Then
			CType(e.Editor, ASPxTextBox).ClientSideEvents.KeyPress = "function(s,e) {OnEditorKeyPress(s, e);}"
		End If

	End Sub
	Protected Sub ASPxTreeList1_NodeUpdated(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatedEventArgs)
		Dim treeList As ASPxTreeList = CType(sender, ASPxTreeList)
		For i As Integer = 0 To treeList.Columns.Count - 1
			If TypeOf treeList.Columns(i) Is TreeListDataColumn Then
				CType(treeList.Columns(i), TreeListDataColumn).EditCellTemplate = Nothing
				Session("column") = Nothing
			End If
		Next i
		treeList.CancelEdit()


	End Sub
End Class
