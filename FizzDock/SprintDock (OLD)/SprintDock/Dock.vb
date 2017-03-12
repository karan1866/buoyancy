Module Dock
    Public AppPath As String
    Public PreviewEx As Boolean
    Public OpacityEx As String
    Public BlurEx As String
    Public GlowSizeEx As String
    Public SkinEx As String
    Public GlowColorEx As SolidColorBrush
    Public AutoHideOption As Boolean = True
    Public DialogResult As String

    Public Sub GetIcon()

    End Sub

    Public Sub LoadIcons()

    End Sub

    Public Sub CheckAutoHide()
        AutoHideOption = IO.File.ReadAllText(AppPath & "\Settings\ahide.h")
    End Sub

    Public Sub Preview(ByVal Opacity As String, ByVal Blur As String, ByVal GlowSize As String, ByVal Skin As String, ByVal GlowColor As SolidColorBrush)
        PreviewEx = True
        OpacityEx = Opacity
        BlurEx = Blur
        GlowSizeEx = GlowSize
        SkinEx = Skin
        GlowColorEx = GlowColor
    End Sub
End Module
