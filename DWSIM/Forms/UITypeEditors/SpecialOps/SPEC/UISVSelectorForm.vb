﻿Imports DWSIM.DWSIM.SimulationObjects
Imports DWSIM.Interfaces.Enums.GraphicObjects.ObjectType

Public Class UISVSelectorForm

    Inherits System.Windows.Forms.Form

    Public formC As FormFlowsheet
    Public selectionData As DWSIM.SimulationObjects.SpecialOps.Helpers.Spec.SourceObjectInfo
    Public gi As GridItem

    Private Sub UISVSelectorForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        formC = My.Application.ActiveSimulation

        With TreeView1.Nodes
            .Clear()
            .Add(DWSIM.App.GetLocalString("CorrentedeMatria"), DWSIM.App.GetLocalString("CorrentedeMatria"))
            .Add(DWSIM.App.GetLocalString("CorrentedeEnergia"), DWSIM.App.GetLocalString("CorrentedeEnergia"))
            .Add(DWSIM.App.GetLocalString("Misturador"), DWSIM.App.GetLocalString("Misturador"))
            .Add(DWSIM.App.GetLocalString("Divisor"), DWSIM.App.GetLocalString("Divisor"))
            .Add(DWSIM.App.GetLocalString("Tubulao"), DWSIM.App.GetLocalString("Tubulao"))
            .Add(DWSIM.App.GetLocalString("Vlvulas"), DWSIM.App.GetLocalString("Vlvulas"))
            .Add(DWSIM.App.GetLocalString("Bombas"), DWSIM.App.GetLocalString("Bombas"))
            .Add(DWSIM.App.GetLocalString("Tanque"), DWSIM.App.GetLocalString("Tanque"))
            .Add(DWSIM.App.GetLocalString("VasoSeparadorGL"), DWSIM.App.GetLocalString("VasoSeparadorGL"))
            .Add(DWSIM.App.GetLocalString("CompressorAdiabtico"), DWSIM.App.GetLocalString("CompressorAdiabtico"))
            .Add(DWSIM.App.GetLocalString("TurbinaAdiabtica"), DWSIM.App.GetLocalString("TurbinaAdiabtica"))
            .Add(DWSIM.App.GetLocalString("Aquecedor"), DWSIM.App.GetLocalString("Aquecedor"))
            .Add(DWSIM.App.GetLocalString("Resfriador"), DWSIM.App.GetLocalString("Resfriador"))
            .Add(DWSIM.App.GetLocalString("ReatorConversao"), DWSIM.App.GetLocalString("ReatorConversao"))
            .Add(DWSIM.App.GetLocalString("ReatorEquilibrio"), DWSIM.App.GetLocalString("ReatorEquilibrio"))
            .Add(DWSIM.App.GetLocalString("ReatorGibbs"), DWSIM.App.GetLocalString("ReatorGibbs"))
            .Add(DWSIM.App.GetLocalString("ReatorCSTR"), DWSIM.App.GetLocalString("ReatorCSTR"))
            .Add(DWSIM.App.GetLocalString("ReatorPFR"), DWSIM.App.GetLocalString("ReatorPFR"))
            .Add(DWSIM.App.GetLocalString("HeatExchanger"), DWSIM.App.GetLocalString("HeatExchanger"))
            .Add(DWSIM.App.GetLocalString("ShortcutColumn"), DWSIM.App.GetLocalString("ShortcutColumn"))
            .Add(DWSIM.App.GetLocalString("DistillationColumn"), DWSIM.App.GetLocalString("DistillationColumn"))
            .Add(DWSIM.App.GetLocalString("AbsorptionColumn"), DWSIM.App.GetLocalString("AbsorptionColumn"))
            .Add(DWSIM.App.GetLocalString("ReboiledAbsorber"), DWSIM.App.GetLocalString("ReboiledAbsorber"))
            .Add(DWSIM.App.GetLocalString("RefluxedAbsorber"), DWSIM.App.GetLocalString("RefluxedAbsorber"))
            .Add(DWSIM.App.GetLocalString("Reciclo"), DWSIM.App.GetLocalString("Reciclo"))
            .Add(DWSIM.App.GetLocalString("EnergyRecycle"), DWSIM.App.GetLocalString("EnergyRecycle"))
            .Add(DWSIM.App.GetLocalString("Especificao"), DWSIM.App.GetLocalString("Especificao"))
            .Add(DWSIM.App.GetLocalString("Ajuste"), DWSIM.App.GetLocalString("Ajuste"))
            .Add(DWSIM.App.GetLocalString("ComponentSeparator"), DWSIM.App.GetLocalString("ComponentSeparator"))
            .Add(DWSIM.App.GetLocalString("OrificePlate"), DWSIM.App.GetLocalString("OrificePlate"))
            .Add(DWSIM.App.GetLocalString("CapeOpenUnitOperation1"), DWSIM.App.GetLocalString("CapeOpenUnitOperation1"))
            .Add(DWSIM.App.GetLocalString("CustomUnitOp"), DWSIM.App.GetLocalString("CustomUnitOp"))
            .Add(DWSIM.App.GetLocalString("ExcelUnitOp"), DWSIM.App.GetLocalString("ExcelUnitOp"))
            .Add(DWSIM.App.GetLocalString("FlowsheetUnitOp"), DWSIM.App.GetLocalString("FlowsheetUnitOp"))
            .Add(DWSIM.App.GetLocalString("SolidsSeparator"), DWSIM.App.GetLocalString("SolidsSeparator"))
            .Add(DWSIM.App.GetLocalString("Filter"), DWSIM.App.GetLocalString("Filter"))
        End With

        If formC.Collections.FlowsheetObjectCollection.ContainsKey(Me.selectionData.m_ID) Then
            If Not Me.selectionData Is Nothing Then
                If Not Me.selectionData.m_Name = "" Then
                    Me.TreeView1.SelectedNode = Me.TreeView1.Nodes.Find(Me.selectionData.m_Type, True)(0)
                    Call Me.ListBox1_SelectedIndexChanged(sender, New TreeViewEventArgs(Me.TreeView1.SelectedNode, TreeViewAction.ByKeyboard))
                    Me.TreeView2.SelectedNode = Me.TreeView2.Nodes.Find(Me.selectionData.m_ID, True)(0)
                    Call Me.ListBox2_SelectedIndexChanged(sender, New TreeViewEventArgs(Me.TreeView2.SelectedNode, TreeViewAction.ByKeyboard))
                    Me.TreeView3.SelectedNode = Me.TreeView3.Nodes.Find(Me.selectionData.m_Property, True)(0)
                End If
            End If
        Else
            With Me.selectionData
                .m_ID = ""
                .m_Name = ""
                .m_Property = ""
                .m_Type = ""
            End With
        End If

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

        Dim obj As GraphicObject

        TreeView2.Nodes.Clear()
        TreeView3.Nodes.Clear()

        With TreeView2.Nodes
            Select Case TreeView1.SelectedNode.Index
                Case 0
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = MaterialStream And obj.InputConnectors(0).IsAttached = False Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 1
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = EnergyStream And obj.InputConnectors(0).IsAttached = False Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 2
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = NodeIn Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 3
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = NodeIn Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 4
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = NodeIn Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 5
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = NodeIn Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 6
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = Pump Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 7
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = Tank Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 8
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = Vessel Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 9
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = Compressor Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 10
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = Expander Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 11
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = Heater Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 12
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = Cooler Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 13
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = RCT_Conversion Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 14
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = RCT_Equilibrium Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 15
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = RCT_Gibbs Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 16
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = RCT_CSTR Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 17
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = RCT_PFR Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 18
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = HeatExchanger Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 19
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = ShortcutColumn Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 20
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = DistillationColumn Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 21
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = AbsorptionColumn Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 22
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = ReboiledAbsorber Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 23
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = RefluxedAbsorber Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 24
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = OT_Recycle Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 25
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = OT_EnergyRecycle Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 26
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = OT_Spec Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 27
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = OT_Adjust Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 28
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = ComponentSeparator Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 29
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = OrificePlate Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 30
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = CapeOpenUO Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 31
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = CustomUO Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 32
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = ExcelUO Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 33
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = FlowsheetUO Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 34
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = SolidSeparator Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
                Case 35
                    For Each obj In formC.Collections.GraphicObjectCollection.Values
                        If obj.ObjectType = Filter Then .Add(obj.Name, obj.Tag).Tag = obj.Name
                    Next
            End Select
        End With

    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView2.AfterSelect

        TreeView3.Nodes.Clear()

        With TreeView3.Nodes
            Dim key As String = e.Node.Tag

            Dim properties As String() = formC.Collections.FlowsheetObjectCollection(key).GetProperties(Interfaces.Enums.PropertyType.ALL)

            For Each prop As String In properties
                .Add(prop, DWSIM.App.GetPropertyName(prop)).Tag = prop
            Next

        End With

    End Sub

    Private Sub KryptonButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KryptonButton2.Click
        Me.Close()
    End Sub

    Private Sub KryptonButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KryptonButton1.Click

        'se o spec ja tinha uma variavel como fonte, marca-la como desconectada do mesmo
        If formC.Collections.FlowsheetObjectCollection.ContainsKey(Me.selectionData.m_ID) Then
            With formC.Collections.FlowsheetObjectCollection(Me.selectionData.m_ID)
                .IsSpecAttached = False
                .AttachedSpecId = ""
                .SpecVarType = SpecVarType.None
            End With
        End If

        Me.selectionData.m_Type = TreeView1.SelectedNode.Text
        Me.selectionData.m_Name = TreeView2.SelectedNode.Text
        Me.selectionData.m_ID = TreeView2.SelectedNode.Tag.ToString
        Me.selectionData.m_Property = TreeView3.SelectedNode.Tag.ToString

        If formC.Collections.FlowsheetObjectCollection.ContainsKey(Me.selectionData.m_ID) Then
            With formC.Collections.FlowsheetObjectCollection(Me.selectionData.m_ID)
                .IsSpecAttached = True
                .AttachedSpecId = formC.FormSurface.FlowsheetDesignSurface.SelectedObject.Name
                .SpecVarType = SpecVarType.Source
            End With
        End If

        Me.Close()
        Call formC.FormProps.PGEx1_PropertyValueChanged(sender, New System.Windows.Forms.PropertyValueChangedEventArgs(gi, Nothing))
    End Sub

End Class