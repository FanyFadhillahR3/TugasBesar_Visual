Imports System.Text
Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Net

Public Class Form_transaksi

    Private Sub TambahData()

        ' Set the data you want to send as key-value pairs
        Dim kode_transaksi As String = txtKode_transaksi.Text
        Dim kode_barang As String = txtKode_barang.Text
        Dim kode_customer As String = txtKode_customer.Text
        Dim harga As String = txtHarga.Text

        Dim postData As String = $"kode_transaksi={kode_transaksi}&kode_barang={kode_barang}&kode_customer={kode_customer}&harga={harga}"

        ' Create a new WebClient instance
        Dim client As New WebClient()

        ' Set the content type header
        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")

        Try
            ' Encode the data as a byte array
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(postData)

            ' Send the POST request and get the response
            Dim responseBytes As Byte() = client.UploadData(transaksi_api, "POST", byteArray)

            ' Convert the response bytes to a string
            Dim responseBody As String = Encoding.ASCII.GetString(responseBytes)

            ' Display the response
            MessageBox.Show(responseBody, "Response")
        Catch ex As Exception
            ' Handle any errors that occur during the request
            MessageBox.Show("An error occurred: " & ex.Message, "Error")
        End Try
        GetClear()
    End Sub


    Private Sub UpdateData()

        ' Set the data you want to send as key-value pairs
        Dim kode_transaksi As String = txtKode_transaksi.Text
        Dim kode_barang As String = txtKode_barang.Text
        Dim kode_customer As String = txtKode_customer.Text
        Dim harga As String = txtHarga.Text


        Dim postData As String = $"kode_transaksi={kode_transaksi}&kode_barang={kode_barang}&kode_customer={kode_customer}&harga={harga}"

        ' Create a new WebClient instance
        Dim client As New WebClient()

        ' Set the content type header
        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")

        Try
            ' Encode the data as a byte array
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(postData)

            ' Send the PUT request and get the response
            Dim responseBytes As Byte() = client.UploadData(transaksi_api & "?kode_transaksi=" & txtKode_transaksi.Text, "PUT", byteArray)

            ' Convert the response bytes to a string
            Dim responseBody As String = Encoding.ASCII.GetString(responseBytes)

            ' Display the response
            MessageBox.Show(responseBody, "Response")
        Catch ex As Exception
            ' Handle any errors that occur during the request
            MessageBox.Show("An error occurred: " & ex.Message, "Error")
        End Try
        GetClear()
    End Sub

    Private Sub DeleteData()

        ' Set the data you want to send as key-value pairs
        Dim kode_transaksi As String = txtKode_transaksi.Text
        Dim kode_barang As String = txtKode_barang.Text
        Dim kode_customer As String = txtKode_customer.Text
        Dim harga As String = txtHarga.Text
        Dim postData As String = $"kode_transaksi={kode_transaksi}&kode_barang={kode_barang}&kode_customer={kode_customer}&harga={harga}"


        ' Create a new WebClient instance
        Dim client As New WebClient()

        ' Set the content type header
        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")

        Try
            ' Encode the data as a byte array
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(postData)

            ' Send the PUT request and get the response
            Dim responseBytes As Byte() = client.UploadData(transaksi_api & "?kode_transaksi=" & txtKode_transaksi.Text, "DELETE", byteArray)

            ' Convert the response bytes to a string
            Dim responseBody As String = Encoding.ASCII.GetString(responseBytes)

            ' Display the response
            MessageBox.Show(responseBody, "Response")
        Catch ex As Exception
            ' Handle any errors that occur during the request
            MessageBox.Show("An error occurred: " & ex.Message, "Error")
        End Try
        GetClear()
    End Sub
    Private Sub GetData()
        Using client As New HttpClient()
            ' Send a GET request to the API endpoint
            Dim response As HttpResponseMessage = client.GetAsync(transaksi_api & "?kode_transaksi=" & txtKode_transaksi.Text).Result

            ' Check if the request was successful
            If response.IsSuccessStatusCode Then
                ' Read the response content as a string
                Dim jsonString As String = response.Content.ReadAsStringAsync().Result
                Try
                    If (jsonString = "[]") Then
                        transaksi_baru = True
                        MessageBox.Show("Data Not Found")
                        Exit Sub
                    Else
                        transaksi_baru = False
                        Exit Sub
                    End If
                Catch ex As Exception

                Finally
                    If (transaksi_baru = False) Then
                        ' Deserialize the JSON into objects
                        Dim data As List(Of Transaksi) = JsonConvert.DeserializeObject(Of List(Of Transaksi))(jsonString)

                        ' Create textboxes dynamically and set their values
                        For Each mydata As Transaksi In data
                            txtKode_transaksi.Text = mydata.kode_transaksi
                            txtKode_barang.Text = mydata.kode_barang
                            txtKode_customer.Text = mydata.kode_customer
                            txtHarga.Text = mydata.harga
                        Next
                    End If

                End Try

            Else
                ' Request failed, handle the error
                MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}")

            End If
        End Using
    End Sub
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If (transaksi_baru = True) Then
            TambahData()
        Else
            UpdateData()
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If (transaksi_baru = False) Then
            Dim result As DialogResult = MessageBox.Show("Apakah data akan dihapus?", "Confirmation", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                DeleteData()
            Else
                MessageBox.Show("Data batal dihapus.")
            End If
        End If
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        GetClear()
    End Sub
    Private Sub GetClear()
        txtKode_transaksi.Clear()
        txtKode_barang.Clear()
        txtKode_customer.Clear()
        txtHarga.Clear()
        Reloaded()
        txtKode_transaksi.Focus()
    End Sub
    Private Sub Reloaded()

        ' Create a WebClient instance to make the HTTP request
        Dim client As New WebClient()

        ' Make the GET request and retrieve the response
        Dim response As String = client.DownloadString(transaksi_api)

        ' Deserialize the JSON response into a list of objects
        Dim data As List(Of Transaksi) = JsonConvert.DeserializeObject(Of List(Of Transaksi))(response)

        ' Bind the data to the DataGridView
        dgvData.DataSource = data
    End Sub


    Private Sub Form_transaksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reloaded()
    End Sub

    Private Sub txtKode_transaksi_KeyDown(sender As Object, e As KeyEventArgs) Handles txtKode_transaksi.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            GetData()
        End If
    End Sub

    Private Sub dgvData_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvData.CellContentClick

    End Sub
End Class