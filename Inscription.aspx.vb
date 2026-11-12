Imports System.Data.SqlClient

Partial Class Inscription
    Inherits System.Web.UI.Page

    Protected Sub btnInscrire_Click(sender As Object, e As EventArgs)
        If Not Page.IsValid Then Return

        Dim email As String = txtEmail.Text.Trim().ToLower()

        Dim existe As Integer = CInt(Db.Scalaire("SELECT COUNT(*) FROM Utilisateurs WHERE Email = @e",
                                                 New SqlParameter("@e", email)))
        If existe > 0 Then
            lblMessage.Text = "Cet email est déjà utilisé."
            lblMessage.CssClass = "msg-err"
            lblMessage.Visible = True
            Return
        End If

        Db.Executer("INSERT INTO Utilisateurs (Nom, Prenom, Email, MotDePasse, Role) VALUES (@n, @p, @e, @m, 'Client')",
                    New SqlParameter("@n", txtNom.Text.Trim()),
                    New SqlParameter("@p", txtPrenom.Text.Trim()),
                    New SqlParameter("@e", email),
                    New SqlParameter("@m", Db.Hacher(txtMdp.Text)))

        Response.Redirect("~/Connexion.aspx?inscrit=1")
    End Sub

End Class