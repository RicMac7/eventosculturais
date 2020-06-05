Imports System.Data.SqlClient

Public Class Eliminar
    Inherits System.Web.UI.Page

    Dim caminho As String = "C:\Users\Ricardo\source\repos\Exerc_18\Exerc_18\App_Data\bdexerc18.mdf"
    Dim conn As New SqlConnection
    Dim mycommand As New SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = CType(Session.Item("username"), String)
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & caminho & ";Integrated Security=True;Connect Timeout=30")
        conn.Open()
        mycommand = New SqlCommand("Delete From Eventos where [Id]= @a1 and [User]= @a2", conn)
        mycommand.Parameters.Add(New SqlParameter("a1", TextBox1.Text))
        mycommand.Parameters.Add(New SqlParameter("a2", Label1.Text))
        mycommand.ExecuteNonQuery()
        If mycommand.ExecuteNonQuery() = 0 Then
            Response.Write("<script>alert('Evento eliminado!');</script>")
            Server.Transfer("Eliminar.aspx")
            'MsgBox("Utilizador eliminado")
        End If
        conn.Close()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("Usermenu.aspx?")
    End Sub
End Class