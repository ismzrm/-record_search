Imports System.Data.OleDb

Public Class Form1
    Dim baglan As OleDbConnection = New OleDbConnection _
    ("Provider=Microsoft.Jet.Oledb.4.0;Data Source=bilgiler1.mdb")
    Sub gnc()
        Dim tablo As DataTable = New DataTable()
        Dim adaptor As OleDbDataAdapter = New OleDbDataAdapter _
        ("SELECT * FROM TABLO1", baglan)
        adaptor.Fill(tablo)
        dataGridView1.DataSource = tablo

    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        gnc()

    End Sub

    Private Sub textBox1_TextChanged(sender As Object, e As EventArgs) Handles textBox1.TextChanged
        Dim tablo As DataTable = New DataTable()
        Dim adaptor As OleDbDataAdapter = New OleDbDataAdapter _
        ("SELECT * FROM TABLO1 WHERE ADI+SOYADI+MEMLEKET LIKE '%" + textBox1.Text + "%'", baglan)
        adaptor.Fill(tablo)
        dataGridView1.DataSource = tablo

    End Sub

    Private Sub textBox2_TextChanged(sender As Object, e As EventArgs) Handles textBox2.TextChanged
        Dim tablo As New DataTable()
        Dim adaptor As New OleDbDataAdapter _
            ("SELECT * FROM TABLO1 WHERE ADI+SOYADI+MEMLEKET LIKE '%" + textBox2.Text + "%'", baglan)
        adaptor.Fill(tablo)
        dataGridView1.DataSource = tablo
    End Sub

    Private Sub button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        If (comboBox1.SelectedIndex = -1) Then
            MsgBox("Lütfen seviye seçiniz")
        End If
        If (textBox3.Text.Trim() = "") Then
            MsgBox("Lütfen boş bırakmayınız")
        End If
        Dim sql1 As String = "SELECT * FROM TABLO1 WHERE ADI=?  "
        Dim sql2 As String = "SELECT * FROM TABLO1 WHERE SOYADI=?"
        Dim sql3 As String = "SELECT * FROM TABLO1 WHERE YAS=?"

        Dim ara As OleDbCommand = New OleDbCommand()
        ara.Connection = baglan
        ara.Parameters.AddWithValue("?", textBox3.Text)
        Select Case comboBox1.SelectedIndex
            Case 0
                ara.CommandText = sql1
            Case 1
                ara.CommandText = sql2
            Case 2
                ara.CommandText = sql3
        End Select
        Dim adaptor As OleDbDataAdapter = New OleDbDataAdapter(ara)
        Dim tablo As DataTable = New DataTable()
        adaptor.Fill(tablo)
        If (tablo.Rows.Count = 0) Then
            MessageBox.Show("ARANAN KAYIT SİSTEMDE YOK")
            gnc()
        Else
            dataGridView1.DataSource = tablo
            textBox1.Text = tablo.Rows(0)("ADI").ToString()
            textBox2.Text = tablo.Rows(0)("SOYADI").ToString()
        End If



    End Sub
End Class
