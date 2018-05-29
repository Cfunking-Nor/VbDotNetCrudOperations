Imports System.Data.SqlClient

Public Class Form1
    Private row As Integer
    Private table As New DataTable
    Dim conn As New SqlConnection("server=DESKTOP-4TQU2TJ;database=StudentInfoSystem;integrated security=SSPI")

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim searchQuery As String = "Select * from studentInfoTable"
        Dim cmd As New SqlCommand(searchQuery, conn)

        Dim da As New SqlDataAdapter(cmd)
        da.Fill(table)

        Me.showrec()

    End Sub

    Private Sub showrec()
        studentIDtxt.Text = table.Rows(row)("studentID").ToString
        lastNametxt.Text = table.Rows(row)("lastName").ToString
        firstNametxt.Text = table.Rows(row)("firstName").ToString
        addresstxt.Text = table.Rows(row)("Address").ToString
        contacttxt.Text = table.Rows(row)("ContactNo").ToString
        gendertxt.Text = table.Rows(row)("gender").ToString
    End Sub


    Private Sub btnInsert_Click(sender As Object, e As EventArgs) Handles btnInsert.Click

        Dim insertQuery As String = "insert into studentInfoTable 
        (studentID,lastName,firstName,Address,ContactNo,gender) VALUES 
       (@studentID,@lastName,@firstName,@Address,@contactNo,@gender)"
        executeYourQuery(insertQuery)
        MsgBox("Record inserted Sucessfully")
        Clear()

    End Sub



    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim updateQuery As String = "update studentInfoTable set
        lastName=@lastName,firstName=@firstName,Address=@Address,ContactNo=@contactNo,gender=@gender
        where studentID=@studentID"
        executeYourQuery(updateQuery)

        MsgBox("Record Updated Sucessfully")
        Clear()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim deleteQuery As String = "delete from studentInfoTable where studentID=@studentID"
        executeYourQuery(deleteQuery)
        MsgBox("Record Deleted")
        Clear()

    End Sub

    Private Sub btSearch_Click(sender As Object, e As EventArgs)
        Dim searchQuery As String = "Select * from studentInfoTable where studentID=@studentID"
        Dim cmd As New SqlCommand(searchQuery, conn)
        cmd.Parameters.AddWithValue("@studentID", studentIDtxt.Text)

        Dim da As New SqlDataAdapter(cmd)

        da.Fill(table)

        If table.Rows.Count > 0 Then
            lastNametxt.Text = table.Rows(0)(1).ToString()
            firstNametxt.Text = table.Rows(0)(2).ToString()
            addresstxt.Text = table.Rows(0)(3).ToString()
            contacttxt.Text = table.Rows(0)(4).ToString()
            gendertxt.Text = table.Rows(0)(5).ToString()
        Else
            MsgBox("No Record Found")
        End If

    End Sub

    Private Sub BtnPrevious_Click(sender As Object, e As EventArgs) Handles BtnPrevious.Click

        If (row > 0) Then
            row = row - 1
            Me.showrec()
        Else
            MsgBox("End of Record!")
        End If


    End Sub

    Private Sub BtnForward_Click(sender As Object, e As EventArgs) Handles BtnForward.Click
        If (row < table.Rows.Count - 1) Then
            row = row + 1
            Me.showrec()
        Else
            MsgBox("End of Record!")
        End If
    End Sub

    Public Sub executeYourQuery(ByVal query As String)
        Dim sqlCommand As New SqlCommand(query, conn)
        sqlCommand.Parameters.AddWithValue("@studentID", studentIDtxt.Text)
        sqlCommand.Parameters.AddWithValue("@lastName", lastNametxt.Text)
        sqlCommand.Parameters.AddWithValue("@firstName", firstNametxt.Text)
        sqlCommand.Parameters.AddWithValue("@Address", addresstxt.Text)
        sqlCommand.Parameters.AddWithValue("@contactNo", contacttxt.Text)
        sqlCommand.Parameters.AddWithValue("@gender", gendertxt.Text)
        conn.Open()
        sqlCommand.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Sub Clear()
        studentIDtxt.Clear()
        lastNametxt.Clear()
        firstNametxt.Clear()
        addresstxt.Clear()
        contacttxt.Clear()
        gendertxt.SelectedIndex = 0
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Clear()

    End Sub
End Class
