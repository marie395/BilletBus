Partial Class Site1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim connecte As Boolean = Session("UserId") IsNot Nothing
        lnkInscription.Visible = Not connecte
        lnkConnexion.Visible = Not connecte
        lnkDeconnexion.Visible = connecte
        lblUtilisateur.Visible = connecte
        If connecte Then
            lblUtilisateur.Text = "Bonjour, " & Session("Prenom").ToString()
            lnkAdmin.Visible = (Session("Role").ToString() = "Admin")
        End If
    End Sub

    Protected Sub lnkDeconnexion_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Session.Abandon()
        Response.Redirect("~/Default.aspx")
    End Sub

End Class