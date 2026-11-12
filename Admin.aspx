<%@ Page Title="Administration" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeBehind="Admin.aspx.vb" Inherits="Billet_de_Bus.Admin" %>

<asp:Content ID="c1" ContentPlaceHolderID="Contenu" runat="server">

    <div class="carte">
        <h2>Ajouter un trajet</h2>
        <div class="ligne">
            <div><label>Ville de départ</label><asp:TextBox ID="txtDepart" runat="server" /></div>
            <div><label>Ville d'arrivée</label><asp:TextBox ID="txtArrivee" runat="server" /></div>
        </div>
        <div class="ligne">
            <div><label>Date et heure de départ</label><asp:TextBox ID="txtDate" runat="server" TextMode="DateTimeLocal" /></div>
            <div><label>Prix (FCFA)</label><asp:TextBox ID="txtPrix" runat="server" TextMode="Number" /></div>
            <div><label>Nombre de places</label><asp:TextBox ID="txtPlaces" runat="server" TextMode="Number" /></div>
        </div>
        <asp:Button ID="btnAjouter" runat="server" Text="Ajouter le trajet" CssClass="btn" OnClick="btnAjouter_Click" />
        <asp:Label ID="lblMessage" runat="server" Visible="false" />
    </div>

    <div class="carte">
        <h2>Trajets</h2>
        <asp:GridView ID="gvTrajets" runat="server" AutoGenerateColumns="False" CssClass="grille"
            GridLines="None" DataKeyNames="Id" OnRowDeleting="gvTrajets_RowDeleting"
            EmptyDataText="Aucun trajet.">
            <Columns>
                <asp:BoundField DataField="VilleDepart" HeaderText="Départ" />
                <asp:BoundField DataField="VilleArrivee" HeaderText="Arrivée" />
                <asp:BoundField DataField="DateDepart" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:BoundField DataField="Prix" HeaderText="Prix (FCFA)" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="PlacesRestantes" HeaderText="Libres" />
                <asp:BoundField DataField="PlacesTotal" HeaderText="Total" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" Text="Supprimer" CommandName="Delete" CssClass="btn btn-danger"
                            OnClientClick="return confirm('Supprimer ce trajet et ses réservations ?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div class="carte">
        <h2>Réservations</h2>
        <asp:GridView ID="gvReservations" runat="server" AutoGenerateColumns="False" CssClass="grille"
            GridLines="None" EmptyDataText="Aucune réservation.">
            <Columns>
                <asp:BoundField DataField="Client" HeaderText="Client" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Trajet" HeaderText="Trajet" />
                <asp:BoundField DataField="NombrePlaces" HeaderText="Places" />
                <asp:BoundField DataField="MontantTotal" HeaderText="Montant (FCFA)" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="DateReservation" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            </Columns>
        </asp:GridView>
        <p><strong>Total des ventes :</strong> <asp:Label ID="lblTotal" runat="server" /> FCFA</p>
    </div>

</asp:Content>