Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls

Partial Class _Default
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ChargerTrajets()
            ChargerMesReservations()
        End If
    End Sub

    Private Sub ChargerTrajets()
        Dim sql As String = "SELECT Id, VilleDepart, VilleArrivee, DateDepart, Prix, PlacesRestantes " &
                            "FROM Trajets WHERE DateDepart >= GETDATE() " &
                            "AND VilleDepart LIKE @d AND VilleArrivee LIKE @a ORDER BY DateDepart"
        gvTrajets.DataSource = Db.Table(sql,
            New SqlParameter("@d", "%" & txtDepart.Text.Trim() & "%"),
            New SqlParameter("@a", "%" & txtArrivee.Text.Trim() & "%"))
        gvTrajets.DataBind()
    End Sub

    Private Sub ChargerMesReservations()
        If Session("UserId") Is Nothing Then
            pnlMesReservations.Visible = False
            Return
        End If
        pnlMesReservations.Visible = True
        Dim sql As String = "SELECT t.VilleDepart + N' → ' + t.VilleArrivee AS Trajet, t.DateDepart, " &
                            "r.NombrePlaces, r.MontantTotal, r.DateReservation " &
                            "FROM Reservations r INNER JOIN Trajets t ON t.Id = r.TrajetId " &
                            "WHERE r.UtilisateurId = @u ORDER BY r.DateReservation DESC"
        gvMesReservations.DataSource = Db.Table(sql, New SqlParameter("@u", CInt(Session("UserId"))))
        gvMesReservations.DataBind()
    End Sub

    Protected Sub btnRechercher_Click(sender As Object, e As EventArgs)
        ChargerTrajets()
    End Sub

    Protected Sub gvTrajets_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName <> "Reserver" Then Return

        ' Un client doit être connecté pour réserver
        If Session("UserId") Is Nothing Then
            Response.Redirect("~/Connexion.aspx")
            Return
        End If

        Dim index As Integer = CInt(e.CommandArgument)
        Dim ligne As GridViewRow = gvTrajets.Rows(index)
        Dim trajetId As Integer = CInt(gvTrajets.DataKeys(index).Value)
        Dim places As Integer
        If Not Integer.TryParse(CType(ligne.FindControl("txtPlaces"), TextBox).Text, places) OrElse places < 1 Then
            Message("Nombre de places invalide.", False)
            Return
        End If

        ' Décrément des places + création de la réservation dans une transaction
        Using cn As SqlConnection = Db.Connexion()
            cn.Open()
            Using tx As SqlTransaction = cn.BeginTransaction()
                Try
                    Dim upd As New SqlCommand(
                        "UPDATE Trajets SET PlacesRestantes = PlacesRestantes - @n " &
                        "WHERE Id = @t AND PlacesRestantes >= @n AND DateDepart >= GETDATE()", cn, tx)
                    upd.Parameters.AddWithValue("@n", places)
                    upd.Parameters.AddWithValue("@t", trajetId)

                    If upd.ExecuteNonQuery() = 0 Then
                        tx.Rollback()
                        Message("Plus assez de places disponibles pour ce trajet.", False)
                        Return
                    End If

                    Dim ins As New SqlCommand(
                        "INSERT INTO Reservations (UtilisateurId, TrajetId, NombrePlaces, MontantTotal) " &
                        "SELECT @u, Id, @n, Prix * @n FROM Trajets WHERE Id = @t", cn, tx)
                    ins.Parameters.AddWithValue("@u", CInt(Session("UserId")))
                    ins.Parameters.AddWithValue("@n", places)
                    ins.Parameters.AddWithValue("@t", trajetId)
                    ins.ExecuteNonQuery()

                    tx.Commit()
                    Message("Réservation confirmée pour " & places & " place(s).", True)
                Catch ex As Exception
                    tx.Rollback()
                    Message("Erreur lors de la réservation : " & ex.Message, False)
                End Try
            End Using
        End Using

        ChargerTrajets()
        ChargerMesReservations()
    End Sub

    Private Sub Message(texte As String, ok As Boolean)
        lblMessage.Text = texte
        lblMessage.CssClass = If(ok, "msg-ok", "msg-err")
        lblMessage.Visible = True
    End Sub

End Class