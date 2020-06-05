Imports System.Data.SqlClient

Public Class Usermenu
    Inherits System.Web.UI.Page


    Dim caminho As String = "C:\Users\Ricardo\source\repos\Exerc_18\Exerc_18\App_Data\bdexerc18.mdf"
    Dim conn As New SqlConnection
    Dim mycommand As New SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnovo_Click(sender As Object, e As EventArgs) Handles btnovo.Click
        conn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & caminho & ";Integrated Security=True;Connect Timeout=30")
        conn.Open()
        Dim checkuser As String = "select count(*) from Utilizadores where login ='" + txtlogin.Text + "'"
        mycommand = New SqlCommand(checkuser, conn)
        Dim temp As Integer = Convert.ToInt32(mycommand.ExecuteScalar().ToString())
        conn.Close()
        If temp = 1 Then
            Response.Write("Esse Utilizador já existe")

        ElseIf txtpass.Text = txtpass1.Text Then
            conn.Open()
            mycommand = New SqlCommand("Insert into Utilizadores(nome, login, pass, email) values (@a1,@a2,@a3,@a4)", conn)
            mycommand.Parameters.AddWithValue("a1", txtnome.Text)
            mycommand.Parameters.AddWithValue("a2", txtlogin.Text)
            mycommand.Parameters.AddWithValue("a3", txtpass.Text)
            mycommand.Parameters.AddWithValue("a4", txtemail.Text)
            If mycommand.ExecuteNonQuery() = 1 Then
                conn.Close()
                Response.Write("<script>alert('Utilizador criado com sucesso!');</script>")
                Server.Transfer("Login.aspx")
            End If
        Else
            Response.Write("<script>alert('Password nao esta igual!');</script>")
        End If

    End Sub

    Protected Sub btlimpa_Click(sender As Object, e As EventArgs) Handles btlimpa.Click
        txtnome.Text = ""
        txtlogin.Text = ""
        txtpass.Text = ""
        txtemail.Text = ""
    End Sub

    Protected Sub btmenu_Click(sender As Object, e As EventArgs) Handles btmenu.Click
        Response.Redirect("Login.aspx?")
    End Sub
End Class