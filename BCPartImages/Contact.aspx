<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="BCPartImages.Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Your contact page.</h2>
    </hgroup>

    <section class="contact">
        <header>
            <h3>Phone:</h3>
        </header>
        <p>
            <span class="label">Main:</span>
            <span>972.812.7000</span>
        </p>
        <p>
            <span class="label">After Hours:</span>
            <span>972.812.7000</span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Email:</h3>
        </header>
        <p>
            <span class="label">Support:</span>
            <span><a href="mailto:Support@omega-usa.com">Support@omega-usa.com</a></span>
        </p>
        <p>
            <span class="label">Marketing:</span>
            <span><a href="mailto:Marketing@omega-usa.com">Marketing@omega-usa.com</a></span>
        </p>
        <p>
            <span class="label">General:</span>
            <span><a href="mailto:General@omega-usa.com">General@omega-usa.com</a></span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Address:</h3>
        </header>
        <p>
            1401 Valley View Lane, STE 100<br />
            Irving, TX 75061-3604
        </p>
    </section>
</asp:Content>