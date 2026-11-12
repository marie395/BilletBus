Imports System.Data.SqlClient
Imports System.Web.UI.WebControls

Partial Class Admin
    Inherits System.Web.UI.Page

    Protected WithEvents txtDepart As TextBox
    Protected WithEvents txtArrivee As TextBox
    Protected WithEvents txtDate As TextBox
    Protected WithEvents txtPrix As TextBox
    Protected WithEvents txtPlaces As TextBox
    Protected WithEvents lblMessage As Label
    Protected WithEvents lblTotal As Label
    Protected WithEvents gvTrajets As GridView
    Protected WithEvents gvReservations As GridView


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Page réservée à l'administrateur
        If Session("UserId") Is Nothing OrElse Session("Role") Is Nothing OrElse Session("Role").ToString() <> "Admin" Then
            Response.Redirect("~/Connexion.aspx")
            Return
        End If
        If Not IsPostBack Then Charger()
    End Sub

    Private Sub Charger()
        gvTrajets.DataSource = Db.Table("SELECT Id, VilleDepart, VilleArrivee, DateDepart, Prix, PlacesTotal, PlacesRestantes FROM Trajets ORDER BY DateDepart")
        gvTrajets.DataBind()

        gvReservations.DataSource = Db.Table(
            "SELECT u.Prenom + ' ' + u.Nom AS Client, u.Email, t.VilleDepart + N' → ' + t.VilleArrivee AS Trajet, " &
            "r.NombrePlaces, r.MontantTotal, r.DateReservation " &
            "FROM Reservations r INNER JOIN Utilisateurs u ON u.Id = r.UtilisateurId " &
            "INNER JOIN Trajets t ON t.Id = r.TrajetId ORDER BY r.DateReservation DESC")
        gvReservations.DataBind()

        lblTotal.Text = Convert.ToDecimal(Db.Scalaire("SELECT ISNULL(SUM(MontantTotal), 0) FROM Reservations")).ToString("N0")
    End Sub

    Protected Sub btnAjouter_Click(sender As Object, e As EventArgs)
        Dim date1 As DateTime, prix As Decimal, places As Integer
        If txtDepart.Text.Trim() = "" OrElse txtArrivee.Text.Trim() = "" OrElse
           Not DateTime.TryParse(txtDate.Text, date1) OrElse
           Not Decimal.TryParse(txtPrix.Text, prix) OrElse prix <= 0 OrElse
           Not Integer.TryParse(txtPlaces.Text, places) OrElse places <= 0 Then
            Message("Veuillez remplir correctement tous les champs.", False)
            Return
        End If

        Db.Executer("INSERT INTO Trajets (VilleDepart, VilleArrivee, DateDepart, Prix, PlacesTotal, PlacesRestantes) " &
                    "VALUES (@d, @a, @dt, @p, @n, @n)",
                    New SqlParameter("@d", txtDepart.Text.Trim()),
                    New SqlParameter("@a", txtArrivee.Text.Trim()),
                    New SqlParameter("@dt", date1),
                    New SqlParameter("@p", prix),
                    New SqlParameter("@n", places))

        txtDepart.Text = "" : txtArrivee.Text = "" : txtDate.Text = "" : txtPrix.Text = "" : txtPlaces.Text = ""
        Message("Trajet ajouté.", True)
        Charger()
    End Sub

    Protected Sub gvTrajets_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim id As Integer = CInt(gvTrajets.DataKeys(e.RowIndex).Value)
        ' Supprimer d'abord les réservations liées (clé étrangère)
        Db.Executer("DELETE FROM Reservations WHERE TrajetId = @id; DELETE FROM Trajets WHERE Id = @id;",
                    New SqlParameter("@id", id))
        Message("Trajet supprimé.", True)
        Charger()
    End Sub

    Private Sub Message(texte As String, ok As Boolean)
        lblMessage.Text = texte
        lblMessage.CssClass = If(ok, "msg-ok", "msg-err")
        lblMessage.Visible = True
    End Sub

End Class