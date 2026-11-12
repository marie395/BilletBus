<%@ Page Title="Inscription" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeBehind="Inscription.aspx.vb" Inherits="Billet_de_Bus.Inscription" %>

<asp:Content ID="c1" ContentPlaceHolderID="Contenu" runat="server">
    <div class="carte formulaire">
        <h2>Créer un compte client</h2>

        <label>Nom</label>
        <asp:TextBox ID="txtNom" runat="server" MaxLength="60" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNom" ErrorMessage="Le nom est obligatoire." ForeColor="#b91c1c" Display="Dynamic" />

        <label>Prénom</label>
        <asp:TextBox ID="txtPrenom" runat="server" MaxLength="60" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPrenom" ErrorMessage="Le prénom est obligatoire." ForeColor="#b91c1c" Display="Dynamic" />

        <label>Email</label>
        <asp:TextBox ID="txtEmail" runat="server" MaxLength="120" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="L'email est obligatoire." ForeColor="#b91c1c" Display="Dynamic" />
        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail"
            ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ErrorMessage="Email invalide." ForeColor="#b91c1c" Display="Dynamic" />

        <label>Mot de passe (6 caractères minimum)</label>
        <asp:TextBox ID="txtMdp" runat="server" TextMode="Password" />
        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtMdp"
            ValidationExpression=".{6,}" ErrorMessage="Minimum 6 caractères." ForeColor="#b91c1c" Display="Dynamic" />

        <label>Confirmer le mot de passe</label>
        <asp:TextBox ID="txtMdp2" runat="server" TextMode="Password" />
        <asp:CompareValidator runat="server" ControlToValidate="txtMdp2" ControlToCompare="txtMdp"
            ErrorMessage="Les mots de passe ne correspondent pas." ForeColor="#b91c1c" Display="Dynamic" />

        <asp:Button ID="btnInscrire" runat="server" Text="S'inscrire" CssClass="btn" OnClick="btnInscrire_Click" />
        <asp:Label ID="lblMessage" runat="server" Visible="false" />
        <p style="font-size:13px;margin-top:16px;">Déjà inscrit ? <a href="Connexion.aspx">Se connecter</a></p>
    </div>
</asp:Content>
