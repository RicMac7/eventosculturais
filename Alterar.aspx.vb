Imports System.Data.SqlClient

Public Class Alterar
    Inherits System.Web.UI.Page

    Dim caminho As String = "C:\Users\Ricardo\source\repos\Exerc_18\Exerc_18\App_Data\bdexerc18.mdf"
    Dim conn As New SqlConnection
    Dim mycommand As New SqlCommand


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = CType(Session.Item("username"), String)
        ' If Not Page.IsPostBack Then
        'userinfo()
        'End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("Usermenu.aspx?")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & caminho & ";Integrated Security=True;Connect Timeout=30")
        conn.Open()
        mycommand = New SqlCommand("Update Eventos SET [Titulo] = @a2, [Local] = @a3, [Data] = @a4, [Categoria] = @a5, [Descricao] = @a6, [hora] = @a7 WHERE [Id]=@a8 And [User] = @a1", conn)
        mycommand.Parameters.AddWithValue("a1", Label1.Text)
        mycommand.Parameters.AddWithValue("a2", Txttitulo.Text)
        mycommand.Parameters.AddWithValue("a3", Txtlocal.Text)
        mycommand.Parameters.AddWithValue("a4", Txtdata.Text)
        mycommand.Parameters.AddWithValue("a5", DListcatego.Text)
        mycommand.Parameters.AddWithValue("a6", Txtdescricao.Text)
        mycommand.Parameters.AddWithValue("a7", Txthora.Text)
        mycommand.Parameters.AddWithValue("a8", Txtprocurar.Text)
        mycommand.ExecuteNonQuery()
        If mycommand.ExecuteNonQuery() = 1 Then
            Response.Write("<script>alert('Evento actualizado com sucesso!');</script>")
            Server.Transfer("Alterar.aspx")
        End If
        conn.Close()
    End Sub

    Protected Sub Btprocurar_Click(sender As Object, e As EventArgs) Handles Btprocurar.Click
        Dim dr As SqlDataReader
        Try
            conn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & caminho & ";Integrated Security=True;Connect Timeout=30")
            conn.Open()
            mycommand = New SqlCommand("Select * from Eventos WHERE [User]='" + Label1.Text + "' And [ID] ='" + Txtprocurar.Text + "'", conn)
            dr = mycommand.ExecuteReader()
            If dr.HasRows Then
                dr.Read()
                Label1.Text = dr.Item("User")
                Txttitulo.Text = dr.Item("Titulo")
                Txtlocal.Text = dr.Item("Local")
                Txtdata9.Text = Convert.ToDateTime(dr.Item("Data")).ToString("dd/MM/yyyy")
                DListcatego.Text = dr.Item("Categoria")
                Txtdescricao.Text = dr.Item("Descricao")
                Txthora9.Text = Convert.ToString(dr.Item("hora"))
            End If
            conn.Close()
        Catch ex As Exception
            Response.Write("<script>alert('" + ex.Message.Replace("\'", " ") + "')</script>")
        End Try
    End Sub
End Class