<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DirListBox
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
        Me.dir1 = New Microsoft.VisualBasic.Compatibility.VB6.DirListBox
        Me.SuspendLayout()
        '
        'dir1
        '
        Me.dir1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dir1.FormattingEnabled = True
        Me.dir1.IntegralHeight = False
        Me.dir1.Location = New System.Drawing.Point(0, 0)
        Me.dir1.Name = "dir1"
        Me.dir1.Size = New System.Drawing.Size(152, 190)
        Me.dir1.TabIndex = 0
        '
        'FileListBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.dir1)
        Me.Name = "FileListBox"
        Me.Size = New System.Drawing.Size(152, 190)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dir1 As Microsoft.VisualBasic.Compatibility.VB6.DirListBox

End Class
