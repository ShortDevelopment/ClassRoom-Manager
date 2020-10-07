<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ComputerView
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.StatusPanel1 = New System.Windows.Forms.Panel()
        Me.StatusPanel2 = New System.Windows.Forms.Panel()
        Me.UserNameLabel = New System.Windows.Forms.Label()
        Me.ComputerNameLabel = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.StatusPanel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(200, 48)
        Me.Panel1.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(0, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(200, 39)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 3)
        Me.Panel2.TabIndex = 3
        '
        'Panel4
        '
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 45)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(200, 3)
        Me.Panel4.TabIndex = 2
        '
        'StatusPanel1
        '
        Me.StatusPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.StatusPanel1.Location = New System.Drawing.Point(0, 0)
        Me.StatusPanel1.Name = "StatusPanel1"
        Me.StatusPanel1.Size = New System.Drawing.Size(200, 3)
        Me.StatusPanel1.TabIndex = 1
        '
        'StatusPanel2
        '
        Me.StatusPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.StatusPanel2.Location = New System.Drawing.Point(0, 87)
        Me.StatusPanel2.Name = "StatusPanel2"
        Me.StatusPanel2.Size = New System.Drawing.Size(200, 3)
        Me.StatusPanel2.TabIndex = 1
        '
        'UserNameLabel
        '
        Me.UserNameLabel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UserNameLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserNameLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.UserNameLabel.Location = New System.Drawing.Point(0, 48)
        Me.UserNameLabel.Name = "UserNameLabel"
        Me.UserNameLabel.Size = New System.Drawing.Size(200, 21)
        Me.UserNameLabel.TabIndex = 2
        Me.UserNameLabel.Text = "UserName"
        Me.UserNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ComputerNameLabel
        '
        Me.ComputerNameLabel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ComputerNameLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ComputerNameLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ComputerNameLabel.Location = New System.Drawing.Point(0, 69)
        Me.ComputerNameLabel.Name = "ComputerNameLabel"
        Me.ComputerNameLabel.Size = New System.Drawing.Size(200, 18)
        Me.ComputerNameLabel.TabIndex = 3
        Me.ComputerNameLabel.Text = "ComputerName"
        Me.ComputerNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ComputerView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.UserNameLabel)
        Me.Controls.Add(Me.ComputerNameLabel)
        Me.Controls.Add(Me.StatusPanel2)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ComputerView"
        Me.Size = New System.Drawing.Size(200, 90)
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents StatusPanel2 As Panel
    Friend WithEvents UserNameLabel As Label
    Friend WithEvents ComputerNameLabel As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents StatusPanel1 As Panel
    Friend WithEvents Panel2 As Panel
End Class
