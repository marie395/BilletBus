<%@ Page Title="Connexion" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeBehind="Connexion.aspx.vb" Inherits="Billet_de_Bus.Connexion" %>

<asp:Content ID="c1" ContentPlaceHolderID="Contenu" runat="server">
    <div class="carte formulaire">
        <h2>Connexion</h2>
        <asp:Label ID="lblInfo" runat="server" Visible="false" CssClass="msg-ok" />

        <label>Email</label>
        <asp:TextBox ID="txtEmail" runat="server" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Email obligatoire." ForeColor="#b91c1c" Display="Dynamic" />

        <label>Mot de passe</label>
        <asp:TextBox ID="txtMdp" runat="server" TextMode="Password" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMdp" ErrorMessage="Mot de passe obligatoire." ForeColor="#b91c1c" Display="Dynamic" />

        <asp:Button ID="btnConnexion" runat="server" Text="Se connecter" CssClass="btn" OnClick="btnConnexion_Click" />
        <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="msg-err" />
        <p style="font-size:13px;margin-top:16px;">Pas encore de compte ? <a href="Inscription.aspx">S'inscrire</a></p>
    </div>
</asp:Content>