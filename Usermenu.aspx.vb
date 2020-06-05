Imports System.Data.SqlClient

Public Class novouser
    Inherits System.Web.UI.Page

    Dim caminho As String = "C:\Users\Ricardo\source\repos\Exerc_18\Exerc_18\App_Data\bdexerc18.mdf"
    Dim conn As New SqlConnection
    Dim mycommand As New SqlCommand

    Protected Sub userinfo()
        Label5.Text = CType(Session.Item("username"), String)
        Dim dr As SqlDataReader
        conn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & caminho & ";Integrated Security=True;Connect Timeout=30")
        conn.Open()
        mycommand = New SqlCommand("Select * from Utilizadores WHERE login='" + Label5.Text + "'", conn)
        dr = mycommand.ExecuteReader()
        If dr.HasRows Then
            dr.Read()
            txtnome.Text = dr.Item("nome")
            'txtlogin.Text = dr.Item("login")
            txtpass.Text = dr.Item("pass")
            txtemail.Text = dr.Item("email")
        Else
            Response.Write("<script>alert('UserName ou Password Inválidos!');</script>")
        End If
        conn.Close()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            userinfo()
        End If
    End Sub

    Protected Sub btelimina_Click(sender As Object, e As EventArgs) Handles btelimina.Click
        conn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & caminho & ";Integrated Security=True;Connect Timeout=30")
        conn.Open()
        mycommand = New SqlCommand("Delete From Utilizadores where login= @a1 and pass= @a2", conn)
        mycommand.Parameters.Add(New SqlParameter("a1", Label5.Text))
        mycommand.Parameters.Add(New SqlParameter("a2", txtpass.Text))
        mycommand.ExecuteNonQuery()
        If mycommand.ExecuteNonQuery() = 0 Then
            Response.Write("<script>alert('Utilizador eliminado!');</script>")
            Server.Transfer("Login.aspx")
            'MsgBox("Utilizador eliminado")
        End If
        conn.Close()

    End Sub

    Protected Sub btatualiza_Click(sender As Object, e As EventArgs) Handles btatualiza.Click
        conn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & caminho & ";Integrated Security=True;Connect Timeout=30")
        conn.Open()
        mycommand = New SqlCommand("Update Utilizadores SET nome = @a1, pass = @a3, email = @a4 WHERE login = @a2", conn)
        mycommand.Parameters.AddWithValue("a1", txtnome.Text)
        mycommand.Parameters.AddWithValue("a2", Label5.Text)
        mycommand.Parameters.AddWithValue("a3", txtpass.Text)
            mycommand.Parameters.AddWithValue("a4", txtemail.Text)
            mycommand.ExecuteNonQuery()
            If mycommand.ExecuteNonQuery() = 1 Then
            Response.Write("<script>alert('Utilizador actualizado com sucesso!');</script>")
        End If
        conn.Close()
        userinfo()
    End Sub

    Protected Sub btlimpa_Click(sender As Object, e As EventArgs) Handles btlimpa.Click
        Dim dr As SqlDataReader
        conn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & caminho & ";Integrated Security=True;Connect Timeout=30")
        conn.Open()
        mycommand = New SqlCommand("Select * from Utilizadores WHERE login='" + Label5.Text + "'", conn)
        dr = mycommand.ExecuteReader()
        If dr.HasRows Then
            dr.Read()
            txtnome.Text = dr.Item("nome")
            'txtlogin.Text = dr.Item("login")
            txtpass.Text = dr.Item("pass")
            txtemail.Text = dr.Item("email")
        Else
            Response.Write("<script>alert('UserName ou Password Inválidos!');</script>")
        End If
        conn.Close()
    End Sub

    Protected Sub btmenu_Click(sender As Object, e As EventArgs) Handles btmenu.Click
        Response.Redirect("Login.aspx?")
    End Sub

    Protected Sub Btcriarevento_Click(sender As Object, e As EventArgs) Handles Btcriarevento.Click
        Session("username") = Label5.Text
        Response.Redirect("Criar.aspx?")
    End Sub

    Protected Sub Btalterarevento_Click(sender As Object, e As EventArgs) Handles Btalterarevento.Click
        Response.Redirect("Alterar.aspx?")
    End Sub

    Protected Sub Bteliminarevento_Click(sender As Object, e As EventArgs) Handles Bteliminarevento.Click
        Response.Redirect("Eliminar.aspx?")
    End Sub
End Class