Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page

    Dim caminho As String = "C:\Users\Ricardo\source\repos\Exerc_18\Exerc_18\App_Data\bdexerc18.mdf"
    Dim conn As New SqlConnection
    Dim mycommand As New SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Protected Sub btlogin_Click(sender As Object, e As EventArgs) Handles btlogin.Click
        Session("username") = txtlogin.Text
        Dim dr As SqlDataReader
        conn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & caminho & ";Integrated Security=True;Connect Timeout=30")
        conn.Open()
        mycommand = New SqlCommand("Select * from Utilizadores WHERE Login='" + txtlogin.Text + "' And Pass='" + txtpass.Text + "'", conn)
        dr = mycommand.ExecuteReader()
        If (dr.HasRows) Then
            lbmensagem.Text = "Dados Correctos"
            Response.AddHeader("REFRESH", "2;URL=Usermenu.aspx")
        Else
            Response.Write("<script>alert('UserName ou Password Inválidos!');</script>")
            'Server.Transfer("Usermenu.aspx")
            'MsgBox("UserName ou Password Inválidos", 64, "Acesso ao Programa")
            lbmensagem.Text = "UserName e Password Inválidos"
            'Response.AddHeader("REFRESH", "2;URL=Login.aspx")
            'Page.Response.Redirect(Page.Request.RawUrl)
        End If
        conn.Close()
    End Sub

    Protected Sub btreset_Click(sender As Object, e As EventArgs) Handles btreset.Click
        txtlogin.Text = ""
        txtpass.Text = ""
        lbmensagem.Text = ""
    End Sub

    Protected Sub btnovo_Click(sender As Object, e As EventArgs) Handles btnovo.Click
        Dim novo As String = "Novouser.aspx?"
        Response.Redirect(novo)
    End Sub
End Class