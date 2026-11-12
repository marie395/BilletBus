<%@ Page Title="Accueil" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Billet_de_Bus._Default" %>

<asp:Content ID="c1" ContentPlaceHolderID="Contenu" runat="server">

    <div class="carte">
        <h2>Rechercher un trajet</h2>
        <div class="ligne">
            <div><label>Ville de départ</label><asp:TextBox ID="txtDepart" runat="server" /></div>
            <div><label>Ville d'arrivée</label><asp:TextBox ID="txtArrivee" runat="server" /></div>
        </div>
        <asp:Button ID="btnRechercher" runat="server" Text="Rechercher" CssClass="btn" OnClick="btnRechercher_Click" />
        <asp:Label ID="lblMessage" runat="server" Visible="false" />
    </div>

    <div class="carte">
        <h2>Trajets disponibles</h2>
        <asp:GridView ID="gvTrajets" runat="server" AutoGenerateColumns="False" CssClass="grille"
            GridLines="None" DataKeyNames="Id" OnRowCommand="gvTrajets_RowCommand"
            EmptyDataText="Aucun trajet disponible.">
            <Columns>
                <asp:BoundField DataField="VilleDepart" HeaderText="Départ" />
                <asp:BoundField DataField="VilleArrivee" HeaderText="Arrivée" />
                <asp:BoundField DataField="DateDepart" HeaderText="Date de départ" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:BoundField DataField="Prix" HeaderText="Prix (FCFA)" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="PlacesRestantes" HeaderText="Places libres" />
                <asp:TemplateField HeaderText="Réserver">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPlaces" runat="server" Text="1" TextMode="Number" Width="60px" />
                        <asp:Button ID="btnReserver" runat="server" Text="Réserver" CssClass="btn btn-petit"
                            CommandName="Reserver" CommandArgument="<%# Container.DataItemIndex %>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <asp:Panel ID="pnlMesReservations" runat="server" Visible="false" CssClass="carte">
        <h2>Mes réservations</h2>
        <asp:GridView ID="gvMesReservations" runat="server" AutoGenerateColumns="False" CssClass="grille"
            GridLines="None" EmptyDataText="Vous n'avez aucune réservation.">
            <Columns>
                <asp:BoundField DataField="Trajet" HeaderText="Trajet" />
                <asp:BoundField DataField="DateDepart" HeaderText="Départ" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:BoundField DataField="NombrePlaces" HeaderText="Places" />
                <asp:BoundField DataField="MontantTotal" HeaderText="Montant (FCFA)" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="DateReservation" HeaderText="Réservé le" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            </Columns>
        </asp:GridView>
    </asp:Panel>

</asp:Content>