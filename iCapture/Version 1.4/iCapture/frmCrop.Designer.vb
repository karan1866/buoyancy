﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCrop
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCrop))
        Me.cropper = New System.Windows.Forms.PictureBox
        CType(Me.cropper, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cropper
        '
        Me.cropper.BackColor = System.Drawing.Color.Magenta
        Me.cropper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cropper.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cropper.Location = New System.Drawing.Point(32767, 32767)
        Me.cropper.Name = "cropper"
        Me.cropper.Size = New System.Drawing.Size(0, 0)
        Me.cropper.TabIndex = 0
        Me.cropper.TabStop = False
        Me.cropper.Visible = False
        '
        'frmCrop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(684, 423)
        Me.Controls.Add(Me.cropper)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCrop"
        Me.Opacity = 0.75
        Me.Padding = New System.Windows.Forms.Padding(50000)
        Me.ShowInTaskbar = False
        Me.Text = "Cropped Snap"
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.Magenta
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.cropper, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cropper As System.Windows.Forms.PictureBox
End Class
