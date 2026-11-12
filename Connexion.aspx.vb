Imports System.Data
Imports System.Data.SqlClient

Partial Class Connexion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack AndAlso Request.QueryString("inscrit") = "1" Then
            lblInfo.Text = "Inscription réussie. Vous pouvez vous connecter."
            lblInfo.Visible = True
        End If
    End Sub

    Protected Sub btnConnexion_Click(sender As Object, e As EventArgs)
        If Not Page.IsValid Then Return

        Dim dt As DataTable = Db.Table(
            "SELECT Id, Prenom, Role FROM Utilisateurs WHERE Email = @e AND MotDePasse = @m",
            New SqlParameter("@e", txtEmail.Text.Trim().ToLower()),
            New SqlParameter("@m", Db.Hacher(txtMdp.Text)))

        If dt.Rows.Count = 0 Then
            lblMessage.Text = "Email ou mot de passe incorrect."
            lblMessage.Visible = True
            Return
        End If

        Session("UserId") = CInt(dt.Rows(0)("Id"))
        Session("Prenom") = dt.Rows(0)("Prenom").ToString()
        Session("Role") = dt.Rows(0)("Role").ToString()

        ' Redirection selon le rôle
        If Session("Role").ToString() = "Admin" Then
            Response.Redirect("~/Admin.aspx")
        Else
            Response.Redirect("~/Default.aspx")
        End If
    End Sub

End Class