Imports System.Data.SqlClient

Public Class marcacao
    Inherits System.Web.UI.Page

    Dim caminho As String = "C:\Users\Ricardo\source\repos\Exerc_18\Exerc_18\App_Data\bdexerc18.mdf"
    Dim conn As New SqlConnection
    Dim mycommand As New SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = CType(Session.Item("username"), String)
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("Usermenu.aspx?")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & caminho & ";Integrated Security=True;Connect Timeout=30")
        conn.Open()
        mycommand = New SqlCommand("Insert Into Eventos([User], [Titulo], [Local], [Data], [Categoria], [Descricao], [Hora]) values(@a1, @a2, @a3, @a4, @a5, @a6, @a7)", conn)
        mycommand.Parameters.AddWithValue("a1", Label1.Text)
        mycommand.Parameters.AddWithValue("a2", Txttitulo.Text)
        mycommand.Parameters.AddWithValue("a3", Txtlocal.Text)
        mycommand.Parameters.AddWithValue("a4", Txtdata.Text)
        mycommand.Parameters.AddWithValue("a5", DListcatego.Text)
        mycommand.Parameters.AddWithValue("a6", Txtdescricao.Text)
        mycommand.Parameters.AddWithValue("a7", Txthora.Text)

        If mycommand.ExecuteNonQuery() = 1 Then
            Response.Write("<script>alert('Evento criado com sucesso!');</script>")
            Server.Transfer("Usermenu.aspx")
        End If
        conn.Close()

    End Sub
End Class