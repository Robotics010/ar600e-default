<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TCPTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.st_r_forearm = New System.Windows.Forms.TextBox()
        Me.st_l_forearm = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.st_r_elbow2 = New System.Windows.Forms.TextBox()
        Me.st_l_elbow2 = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.st_r_elbow1 = New System.Windows.Forms.TextBox()
        Me.st_l_elbow1 = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.st_r_shoulder2 = New System.Windows.Forms.TextBox()
        Me.st_l_shoulder2 = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.st_r_shoulder1 = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.st_l_shoulder1 = New System.Windows.Forms.TextBox()
        Me.r_forearm = New System.Windows.Forms.TextBox()
        Me.lbl_l_forearm = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.l_forearm = New System.Windows.Forms.TextBox()
        Me.r_elbow2 = New System.Windows.Forms.TextBox()
        Me.lbl_l_elbow2 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.l_elbow2 = New System.Windows.Forms.TextBox()
        Me.r_elbow1 = New System.Windows.Forms.TextBox()
        Me.lbl_l_elbow1 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.l_elbow1 = New System.Windows.Forms.TextBox()
        Me.r_shoulder2 = New System.Windows.Forms.TextBox()
        Me.lbl_l_shoulder2 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.l_shoulder2 = New System.Windows.Forms.TextBox()
        Me.r_shoulder1 = New System.Windows.Forms.TextBox()
        Me.lbl_l_shoulder1 = New System.Windows.Forms.Label()
        Me.l_shoulder1 = New System.Windows.Forms.TextBox()
        Me.Panel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(30, 31)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(132, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Start TCP Server"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TCPTimer
        '
        Me.TCPTimer.Interval = 50
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Label44)
        Me.Panel7.Controls.Add(Me.Label27)
        Me.Panel7.Controls.Add(Me.st_r_forearm)
        Me.Panel7.Controls.Add(Me.st_l_forearm)
        Me.Panel7.Controls.Add(Me.Label43)
        Me.Panel7.Controls.Add(Me.Label28)
        Me.Panel7.Controls.Add(Me.st_r_elbow2)
        Me.Panel7.Controls.Add(Me.st_l_elbow2)
        Me.Panel7.Controls.Add(Me.Label41)
        Me.Panel7.Controls.Add(Me.Label29)
        Me.Panel7.Controls.Add(Me.st_r_elbow1)
        Me.Panel7.Controls.Add(Me.st_l_elbow1)
        Me.Panel7.Controls.Add(Me.Label40)
        Me.Panel7.Controls.Add(Me.Label30)
        Me.Panel7.Controls.Add(Me.st_r_shoulder2)
        Me.Panel7.Controls.Add(Me.st_l_shoulder2)
        Me.Panel7.Controls.Add(Me.Label39)
        Me.Panel7.Controls.Add(Me.st_r_shoulder1)
        Me.Panel7.Controls.Add(Me.Label31)
        Me.Panel7.Controls.Add(Me.Label38)
        Me.Panel7.Controls.Add(Me.st_l_shoulder1)
        Me.Panel7.Controls.Add(Me.r_forearm)
        Me.Panel7.Controls.Add(Me.lbl_l_forearm)
        Me.Panel7.Controls.Add(Me.Label35)
        Me.Panel7.Controls.Add(Me.l_forearm)
        Me.Panel7.Controls.Add(Me.r_elbow2)
        Me.Panel7.Controls.Add(Me.lbl_l_elbow2)
        Me.Panel7.Controls.Add(Me.Label34)
        Me.Panel7.Controls.Add(Me.l_elbow2)
        Me.Panel7.Controls.Add(Me.r_elbow1)
        Me.Panel7.Controls.Add(Me.lbl_l_elbow1)
        Me.Panel7.Controls.Add(Me.Label33)
        Me.Panel7.Controls.Add(Me.l_elbow1)
        Me.Panel7.Controls.Add(Me.r_shoulder2)
        Me.Panel7.Controls.Add(Me.lbl_l_shoulder2)
        Me.Panel7.Controls.Add(Me.Label32)
        Me.Panel7.Controls.Add(Me.l_shoulder2)
        Me.Panel7.Controls.Add(Me.r_shoulder1)
        Me.Panel7.Controls.Add(Me.lbl_l_shoulder1)
        Me.Panel7.Controls.Add(Me.l_shoulder1)
        Me.Panel7.Location = New System.Drawing.Point(186, 31)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(404, 196)
        Me.Panel7.TabIndex = 83
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(282, 157)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(65, 13)
        Me.Label44.TabIndex = 102
        Me.Label44.Text = "st_r_forearm"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(142, 156)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(64, 13)
        Me.Label27.TabIndex = 102
        Me.Label27.Text = "st_l_forearm"
        '
        'st_r_forearm
        '
        Me.st_r_forearm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.st_r_forearm.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.st_r_forearm.Location = New System.Drawing.Point(282, 173)
        Me.st_r_forearm.Name = "st_r_forearm"
        Me.st_r_forearm.Size = New System.Drawing.Size(50, 21)
        Me.st_r_forearm.TabIndex = 101
        Me.st_r_forearm.Text = "-"
        Me.st_r_forearm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'st_l_forearm
        '
        Me.st_l_forearm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.st_l_forearm.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.st_l_forearm.Location = New System.Drawing.Point(142, 172)
        Me.st_l_forearm.Name = "st_l_forearm"
        Me.st_l_forearm.Size = New System.Drawing.Size(50, 21)
        Me.st_l_forearm.TabIndex = 101
        Me.st_l_forearm.Text = "-"
        Me.st_l_forearm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(282, 113)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(64, 13)
        Me.Label43.TabIndex = 100
        Me.Label43.Text = "st_r_elbow2"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(142, 112)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(63, 13)
        Me.Label28.TabIndex = 100
        Me.Label28.Text = "st_l_elbow2"
        '
        'st_r_elbow2
        '
        Me.st_r_elbow2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.st_r_elbow2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.st_r_elbow2.Location = New System.Drawing.Point(282, 129)
        Me.st_r_elbow2.Name = "st_r_elbow2"
        Me.st_r_elbow2.Size = New System.Drawing.Size(50, 21)
        Me.st_r_elbow2.TabIndex = 99
        Me.st_r_elbow2.Text = "-"
        Me.st_r_elbow2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'st_l_elbow2
        '
        Me.st_l_elbow2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.st_l_elbow2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.st_l_elbow2.Location = New System.Drawing.Point(142, 128)
        Me.st_l_elbow2.Name = "st_l_elbow2"
        Me.st_l_elbow2.Size = New System.Drawing.Size(50, 21)
        Me.st_l_elbow2.TabIndex = 99
        Me.st_l_elbow2.Text = "-"
        Me.st_l_elbow2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(282, 75)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(64, 13)
        Me.Label41.TabIndex = 98
        Me.Label41.Text = "st_r_elbow1"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(142, 74)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(63, 13)
        Me.Label29.TabIndex = 98
        Me.Label29.Text = "st_l_elbow1"
        '
        'st_r_elbow1
        '
        Me.st_r_elbow1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.st_r_elbow1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.st_r_elbow1.Location = New System.Drawing.Point(282, 91)
        Me.st_r_elbow1.Name = "st_r_elbow1"
        Me.st_r_elbow1.Size = New System.Drawing.Size(50, 21)
        Me.st_r_elbow1.TabIndex = 97
        Me.st_r_elbow1.Text = "-"
        Me.st_r_elbow1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'st_l_elbow1
        '
        Me.st_l_elbow1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.st_l_elbow1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.st_l_elbow1.Location = New System.Drawing.Point(142, 90)
        Me.st_l_elbow1.Name = "st_l_elbow1"
        Me.st_l_elbow1.Size = New System.Drawing.Size(50, 21)
        Me.st_l_elbow1.TabIndex = 97
        Me.st_l_elbow1.Text = "-"
        Me.st_l_elbow1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(282, 37)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(76, 13)
        Me.Label40.TabIndex = 96
        Me.Label40.Text = "st_r_shoulder2"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(142, 36)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(75, 13)
        Me.Label30.TabIndex = 96
        Me.Label30.Text = "st_l_shoulder2"
        '
        'st_r_shoulder2
        '
        Me.st_r_shoulder2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.st_r_shoulder2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.st_r_shoulder2.Location = New System.Drawing.Point(282, 52)
        Me.st_r_shoulder2.Name = "st_r_shoulder2"
        Me.st_r_shoulder2.Size = New System.Drawing.Size(50, 21)
        Me.st_r_shoulder2.TabIndex = 95
        Me.st_r_shoulder2.Text = "-"
        Me.st_r_shoulder2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'st_l_shoulder2
        '
        Me.st_l_shoulder2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.st_l_shoulder2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.st_l_shoulder2.Location = New System.Drawing.Point(142, 51)
        Me.st_l_shoulder2.Name = "st_l_shoulder2"
        Me.st_l_shoulder2.Size = New System.Drawing.Size(50, 21)
        Me.st_l_shoulder2.TabIndex = 95
        Me.st_l_shoulder2.Text = "-"
        Me.st_l_shoulder2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(282, 1)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(76, 13)
        Me.Label39.TabIndex = 94
        Me.Label39.Text = "st_r_shoulder1"
        '
        'st_r_shoulder1
        '
        Me.st_r_shoulder1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.st_r_shoulder1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.st_r_shoulder1.Location = New System.Drawing.Point(282, 14)
        Me.st_r_shoulder1.Name = "st_r_shoulder1"
        Me.st_r_shoulder1.Size = New System.Drawing.Size(50, 21)
        Me.st_r_shoulder1.TabIndex = 93
        Me.st_r_shoulder1.Text = "-"
        Me.st_r_shoulder1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(142, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(75, 13)
        Me.Label31.TabIndex = 94
        Me.Label31.Text = "st_l_shoulder1"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(219, 157)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(51, 13)
        Me.Label38.TabIndex = 92
        Me.Label38.Text = "r_forearm"
        '
        'st_l_shoulder1
        '
        Me.st_l_shoulder1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.st_l_shoulder1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.st_l_shoulder1.Location = New System.Drawing.Point(142, 13)
        Me.st_l_shoulder1.Name = "st_l_shoulder1"
        Me.st_l_shoulder1.Size = New System.Drawing.Size(50, 21)
        Me.st_l_shoulder1.TabIndex = 93
        Me.st_l_shoulder1.Text = "-"
        Me.st_l_shoulder1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'r_forearm
        '
        Me.r_forearm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.r_forearm.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.r_forearm.Location = New System.Drawing.Point(219, 173)
        Me.r_forearm.Name = "r_forearm"
        Me.r_forearm.Size = New System.Drawing.Size(50, 21)
        Me.r_forearm.TabIndex = 91
        Me.r_forearm.Text = "-"
        Me.r_forearm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_l_forearm
        '
        Me.lbl_l_forearm.AutoSize = True
        Me.lbl_l_forearm.Location = New System.Drawing.Point(79, 156)
        Me.lbl_l_forearm.Name = "lbl_l_forearm"
        Me.lbl_l_forearm.Size = New System.Drawing.Size(50, 13)
        Me.lbl_l_forearm.TabIndex = 92
        Me.lbl_l_forearm.Text = "l_forearm"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(219, 113)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(50, 13)
        Me.Label35.TabIndex = 90
        Me.Label35.Text = "r_elbow2"
        '
        'l_forearm
        '
        Me.l_forearm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.l_forearm.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.l_forearm.Location = New System.Drawing.Point(79, 172)
        Me.l_forearm.Name = "l_forearm"
        Me.l_forearm.Size = New System.Drawing.Size(50, 21)
        Me.l_forearm.TabIndex = 91
        Me.l_forearm.Text = "-"
        Me.l_forearm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'r_elbow2
        '
        Me.r_elbow2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.r_elbow2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.r_elbow2.Location = New System.Drawing.Point(219, 129)
        Me.r_elbow2.Name = "r_elbow2"
        Me.r_elbow2.Size = New System.Drawing.Size(50, 21)
        Me.r_elbow2.TabIndex = 89
        Me.r_elbow2.Text = "-"
        Me.r_elbow2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_l_elbow2
        '
        Me.lbl_l_elbow2.AutoSize = True
        Me.lbl_l_elbow2.Location = New System.Drawing.Point(79, 112)
        Me.lbl_l_elbow2.Name = "lbl_l_elbow2"
        Me.lbl_l_elbow2.Size = New System.Drawing.Size(49, 13)
        Me.lbl_l_elbow2.TabIndex = 90
        Me.lbl_l_elbow2.Text = "l_elbow2"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(219, 75)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(50, 13)
        Me.Label34.TabIndex = 88
        Me.Label34.Text = "r_elbow1"
        '
        'l_elbow2
        '
        Me.l_elbow2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.l_elbow2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.l_elbow2.Location = New System.Drawing.Point(79, 128)
        Me.l_elbow2.Name = "l_elbow2"
        Me.l_elbow2.Size = New System.Drawing.Size(50, 21)
        Me.l_elbow2.TabIndex = 89
        Me.l_elbow2.Text = "-"
        Me.l_elbow2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'r_elbow1
        '
        Me.r_elbow1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.r_elbow1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.r_elbow1.Location = New System.Drawing.Point(219, 91)
        Me.r_elbow1.Name = "r_elbow1"
        Me.r_elbow1.Size = New System.Drawing.Size(50, 21)
        Me.r_elbow1.TabIndex = 87
        Me.r_elbow1.Text = "-"
        Me.r_elbow1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_l_elbow1
        '
        Me.lbl_l_elbow1.AutoSize = True
        Me.lbl_l_elbow1.Location = New System.Drawing.Point(79, 74)
        Me.lbl_l_elbow1.Name = "lbl_l_elbow1"
        Me.lbl_l_elbow1.Size = New System.Drawing.Size(49, 13)
        Me.lbl_l_elbow1.TabIndex = 88
        Me.lbl_l_elbow1.Text = "l_elbow1"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(219, 37)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(62, 13)
        Me.Label33.TabIndex = 86
        Me.Label33.Text = "r_shoulder2"
        '
        'l_elbow1
        '
        Me.l_elbow1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.l_elbow1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.l_elbow1.Location = New System.Drawing.Point(79, 90)
        Me.l_elbow1.Name = "l_elbow1"
        Me.l_elbow1.Size = New System.Drawing.Size(50, 21)
        Me.l_elbow1.TabIndex = 87
        Me.l_elbow1.Text = "-"
        Me.l_elbow1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'r_shoulder2
        '
        Me.r_shoulder2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.r_shoulder2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.r_shoulder2.Location = New System.Drawing.Point(219, 52)
        Me.r_shoulder2.Name = "r_shoulder2"
        Me.r_shoulder2.Size = New System.Drawing.Size(50, 21)
        Me.r_shoulder2.TabIndex = 85
        Me.r_shoulder2.Text = "-"
        Me.r_shoulder2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_l_shoulder2
        '
        Me.lbl_l_shoulder2.AutoSize = True
        Me.lbl_l_shoulder2.Location = New System.Drawing.Point(79, 36)
        Me.lbl_l_shoulder2.Name = "lbl_l_shoulder2"
        Me.lbl_l_shoulder2.Size = New System.Drawing.Size(61, 13)
        Me.lbl_l_shoulder2.TabIndex = 86
        Me.lbl_l_shoulder2.Text = "l_shoulder2"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(219, 1)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(62, 13)
        Me.Label32.TabIndex = 84
        Me.Label32.Text = "r_shoulder1"
        '
        'l_shoulder2
        '
        Me.l_shoulder2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.l_shoulder2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.l_shoulder2.Location = New System.Drawing.Point(79, 51)
        Me.l_shoulder2.Name = "l_shoulder2"
        Me.l_shoulder2.Size = New System.Drawing.Size(50, 21)
        Me.l_shoulder2.TabIndex = 85
        Me.l_shoulder2.Text = "-"
        Me.l_shoulder2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'r_shoulder1
        '
        Me.r_shoulder1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.r_shoulder1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.r_shoulder1.Location = New System.Drawing.Point(219, 14)
        Me.r_shoulder1.Name = "r_shoulder1"
        Me.r_shoulder1.Size = New System.Drawing.Size(50, 21)
        Me.r_shoulder1.TabIndex = 83
        Me.r_shoulder1.Text = "-"
        Me.r_shoulder1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_l_shoulder1
        '
        Me.lbl_l_shoulder1.AutoSize = True
        Me.lbl_l_shoulder1.Location = New System.Drawing.Point(79, 0)
        Me.lbl_l_shoulder1.Name = "lbl_l_shoulder1"
        Me.lbl_l_shoulder1.Size = New System.Drawing.Size(61, 13)
        Me.lbl_l_shoulder1.TabIndex = 84
        Me.lbl_l_shoulder1.Text = "l_shoulder1"
        '
        'l_shoulder1
        '
        Me.l_shoulder1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.l_shoulder1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.l_shoulder1.Location = New System.Drawing.Point(79, 13)
        Me.l_shoulder1.Name = "l_shoulder1"
        Me.l_shoulder1.Size = New System.Drawing.Size(50, 21)
        Me.l_shoulder1.TabIndex = 83
        Me.l_shoulder1.Text = "-"
        Me.l_shoulder1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(660, 262)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents TCPTimer As Timer
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label44 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents st_r_forearm As TextBox
    Friend WithEvents st_l_forearm As TextBox
    Friend WithEvents Label43 As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents st_r_elbow2 As TextBox
    Friend WithEvents st_l_elbow2 As TextBox
    Friend WithEvents Label41 As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents st_r_elbow1 As TextBox
    Friend WithEvents st_l_elbow1 As TextBox
    Friend WithEvents Label40 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents st_r_shoulder2 As TextBox
    Friend WithEvents st_l_shoulder2 As TextBox
    Friend WithEvents Label39 As Label
    Friend WithEvents st_r_shoulder1 As TextBox
    Friend WithEvents Label31 As Label
    Friend WithEvents Label38 As Label
    Friend WithEvents st_l_shoulder1 As TextBox
    Friend WithEvents r_forearm As TextBox
    Friend WithEvents lbl_l_forearm As Label
    Friend WithEvents Label35 As Label
    Friend WithEvents l_forearm As TextBox
    Friend WithEvents r_elbow2 As TextBox
    Friend WithEvents lbl_l_elbow2 As Label
    Friend WithEvents Label34 As Label
    Friend WithEvents l_elbow2 As TextBox
    Friend WithEvents r_elbow1 As TextBox
    Friend WithEvents lbl_l_elbow1 As Label
    Friend WithEvents Label33 As Label
    Friend WithEvents l_elbow1 As TextBox
    Friend WithEvents r_shoulder2 As TextBox
    Friend WithEvents lbl_l_shoulder2 As Label
    Friend WithEvents Label32 As Label
    Friend WithEvents l_shoulder2 As TextBox
    Friend WithEvents r_shoulder1 As TextBox
    Friend WithEvents lbl_l_shoulder1 As Label
    Friend WithEvents l_shoulder1 As TextBox
End Class
