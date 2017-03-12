Module Dock
    Public AppPath As String
    Public PreviewEx As Boolean
    Public OpacityEx As String
    Public BlurEx As String
    Public GlowSizeEx As String
    Public SkinEx As String
    Public GlowColorEx As SolidColorBrush
    Public AlignEx As String

    Public iOpacityEx As String
    Public iBlurEx As String
    Public iGlowSizeEx As String
    Public iGlowColorEx As SolidColorBrush

    Public AutoHideOption As Boolean = True
    Public DialogResult As String

    Public Sub CheckAutoHide()
        AutoHideOption = IO.File.ReadAllText(AppPath & "\Settings\ahide.h")
    End Sub

    Public Sub Preview(ByVal Opacity As String, ByVal Blur As String, ByVal GlowSize As String, ByVal Skin As String, ByVal GlowColor As SolidColorBrush, ByVal Align As String, ByVal iOpacity As String, ByVal iBlur As String, ByVal iGlowSize As String, ByVal iGlowColor As SolidColorBrush)
        PreviewEx = True
        OpacityEx = Opacity
        BlurEx = Blur
        GlowSizeEx = GlowSize
        SkinEx = Skin
        GlowColorEx = GlowColor
        AlignEx = Align
        iBlurEx = iBlur
        iOpacityEx = iOpacity
        iGlowColorEx = iGlowColor
        iGlowSizeEx = iGlowSize
    End Sub
End Module
