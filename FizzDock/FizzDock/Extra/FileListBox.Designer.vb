<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FileListBox
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.file1 = New Microsoft.VisualBasic.Compatibility.VB6.FileListBox
        Me.SuspendLayout()
        '
        'file1
        '
        Me.file1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.file1.FormattingEnabled = True
        Me.file1.Location = New System.Drawing.Point(0, 0)
        Me.file1.Name = "file1"
        Me.file1.Pattern = "*.png"
        Me.file1.Size = New System.Drawing.Size(150, 147)
        Me.file1.TabIndex = 0
        '
        'FileListBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.file1)
        Me.Name = "FileListBox"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents file1 As Microsoft.VisualBasic.Compatibility.VB6.FileListBox

End Class
