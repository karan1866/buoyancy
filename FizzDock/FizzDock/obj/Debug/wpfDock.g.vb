﻿#ExternalChecksum("..\..\wpfDock.xaml","{406ea660-64cf-4c82-b6f0-42d48172a799}","A202636FD148EF1BBDDBC4875A4ED6D1")
'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.1433
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Forms
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Effects
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Media3D
Imports System.Windows.Media.TextFormatting
Imports System.Windows.Navigation
Imports System.Windows.Shapes


'''<summary>
'''wpfDock
'''</summary>
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
Partial Public Class wpfDock
    Inherits System.Windows.Window
    Implements System.Windows.Markup.IComponentConnector
    
    
    #ExternalSource("..\..\wpfDock.xaml",5)
    Friend WithEvents Window1 As wpfDock
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\wpfDock.xaml",7)
    Friend WithEvents Main As System.Windows.Controls.Grid
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\wpfDock.xaml",12)
    Friend WithEvents grdMain As System.Windows.Controls.Grid
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\wpfDock.xaml",13)
    Friend WithEvents grdBG As System.Windows.Controls.Grid
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\wpfDock.xaml",14)
    Friend WithEvents imgCenter As System.Windows.Controls.Image
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\wpfDock.xaml",15)
    Friend WithEvents imgLeft As System.Windows.Controls.Image
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\wpfDock.xaml",16)
    Friend WithEvents imgRight As System.Windows.Controls.Image
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\wpfDock.xaml",19)
    Friend WithEvents Icons2 As System.Windows.Controls.Grid
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\wpfDock.xaml",19)
    Friend WithEvents Icons As System.Windows.Controls.Grid
    
    #End ExternalSource
    
    Private _contentLoaded As Boolean
    
    '''<summary>
    '''InitializeComponent
    '''</summary>
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Public Sub InitializeComponent() Implements System.Windows.Markup.IComponentConnector.InitializeComponent
        If _contentLoaded Then
            Return
        End If
        _contentLoaded = true
        Dim resourceLocater As System.Uri = New System.Uri("/FizzDock;component/wpfdock.xaml", System.UriKind.Relative)
        
        #ExternalSource("..\..\wpfDock.xaml",1)
        System.Windows.Application.LoadComponent(Me, resourceLocater)
        
        #End ExternalSource
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")>  _
    Sub System_Windows_Markup_IComponentConnector_Connect(ByVal connectionId As Integer, ByVal target As Object) Implements System.Windows.Markup.IComponentConnector.Connect
        If (connectionId = 1) Then
            Me.Window1 = CType(target,wpfDock)
            Return
        End If
        If (connectionId = 2) Then
            Me.Main = CType(target,System.Windows.Controls.Grid)
            Return
        End If
        If (connectionId = 3) Then
            Me.grdMain = CType(target,System.Windows.Controls.Grid)
            Return
        End If
        If (connectionId = 4) Then
            Me.grdBG = CType(target,System.Windows.Controls.Grid)
            Return
        End If
        If (connectionId = 5) Then
            Me.imgCenter = CType(target,System.Windows.Controls.Image)
            Return
        End If
        If (connectionId = 6) Then
            Me.imgLeft = CType(target,System.Windows.Controls.Image)
            Return
        End If
        If (connectionId = 7) Then
            Me.imgRight = CType(target,System.Windows.Controls.Image)
            Return
        End If
        If (connectionId = 8) Then
            Me.Icons2 = CType(target,System.Windows.Controls.Grid)
            Return
        End If
        If (connectionId = 9) Then
            Me.Icons = CType(target,System.Windows.Controls.Grid)
            Return
        End If
        Me._contentLoaded = true
    End Sub
End Class

