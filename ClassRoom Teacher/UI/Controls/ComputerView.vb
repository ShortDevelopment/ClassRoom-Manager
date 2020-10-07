Imports System.ComponentModel

Public Class ComputerView

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles UserNameLabel.MouseEnter, PictureBox1.MouseEnter, Panel4.MouseEnter, StatusPanel1.MouseEnter, StatusPanel2.MouseEnter, Panel1.MouseEnter, ComputerNameLabel.MouseEnter
        If Checked Then Exit Sub
        StatusPanel1.BackColor = Color.FromArgb(192, 57, 43)
        StatusPanel2.BackColor = Color.FromArgb(192, 57, 43)
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles UserNameLabel.MouseLeave, PictureBox1.MouseLeave, Panel4.MouseLeave, StatusPanel1.MouseLeave, StatusPanel2.MouseLeave, Panel1.MouseLeave, ComputerNameLabel.MouseLeave
        If Checked Then Exit Sub
        StatusPanel1.BackColor = Me.BackColor
        StatusPanel2.BackColor = Me.BackColor
    End Sub

    Private Sub ComputerView_Load(sender As Object, e As EventArgs) Handles Me.Load
        PictureBox1.Image = UserProfile.DefaultAvatar
    End Sub

    <Browsable(True), EditorBrowsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property UserName As String
        Get
            Return UserNameLabel.Text
        End Get
        Set(value As String)
            UserNameLabel.Text = value
        End Set
    End Property
    <Browsable(True), EditorBrowsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ComputerName As String
        Get
            Return ComputerNameLabel.Text
        End Get
        Set(value As String)
            ComputerNameLabel.Text = value
        End Set
    End Property
    <Browsable(True), EditorBrowsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Avatar As Bitmap
        Get
            Return PictureBox1.Image
        End Get
        Set(value As Bitmap)
            PictureBox1.Image = value
        End Set
    End Property

    Dim _Checked As Boolean = False
    <Browsable(True), EditorBrowsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Checked As Boolean
        Get
            Return _Checked
        End Get
        Set(value As Boolean)
            If value Then
                StatusPanel1.BackColor = Color.FromArgb(142, 68, 173)
                StatusPanel2.BackColor = Color.FromArgb(142, 68, 173)
            Else
                StatusPanel1.BackColor = Me.BackColor
                StatusPanel2.BackColor = Me.BackColor
            End If
            _Checked = value
        End Set
    End Property

    Public Event Selected(pc As ComputerView)

    Private Sub Panel1_Click(sender As Object, e As EventArgs) Handles UserNameLabel.Click, StatusPanel2.Click, StatusPanel1.Click, PictureBox1.Click, Panel4.Click, Panel2.Click, Panel1.Click, ComputerNameLabel.Click
        RaiseEvent Selected(Me)
    End Sub
End Class
