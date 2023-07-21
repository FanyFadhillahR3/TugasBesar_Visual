Imports System.Text
Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Net
Public Class Form_customer
    Private Sub TambahData()

        ' Set the data you want to send as key-value pairs
        Dim kode_customer As String = txtKode_customer.Text
        Dim nama_customer As String = txtNama_customer.Text
        Dim alamat As String = txtAlamat.Text
        Dim email As String = txtEmail.Text

        Dim postData As String = $"kode_customer={kode_customer}&nama_customer={nama_customer}&alamat={alamat}&email={email}"

        ' Create a new WebClient instance
        Dim client As New WebClient()

        ' Set the content type header
        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")

        Try
            ' Encode the data as a byte array
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(postData)

            ' Send the POST request and get the response
            Dim responseBytes As Byte() = client.UploadData(customer_api, "POST", byteArray)

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
        Dim kode_customer As String = txtKode_customer.Text
        Dim nama_customer As String = txtNama_customer.Text
        Dim alamat As String = txtAlamat.Text
        Dim email As String = txtEmail.Text

        Dim postData As String = $"kode_customer={kode_customer}&nama_customer={nama_customer}&alamat={alamat}&email={email}"

        ' Create a new WebClient instance
        Dim client As New WebClient()

        ' Set the content type header
        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")

        Try
            ' Encode the data as a byte array
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(postData)

            ' Send the PUT request and get the response
            Dim responseBytes As Byte() = client.UploadData(customer_api & "?kode_customer=" & txtKode_customer.Text, "PUT", byteArray)

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
        Dim kode_customer As String = txtKode_customer.Text
        Dim nama_customer As String = txtNama_customer.Text
        Dim alamat As String = txtAlamat.Text
        Dim email As String = txtEmail.Text

        Dim postData As String = $"kode_customer={kode_customer}&nama_customer={nama_customer}&alamat={alamat}&email={email}"

        ' Create a new WebClient instance
        Dim client As New WebClient()

        ' Set the content type header
        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")

        Try
            ' Encode the data as a byte array
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(postData)

            ' Send the PUT request and get the response
            Dim responseBytes As Byte() = client.UploadData(customer_api & "?kode_customer=" & txtKode_customer.Text, "DELETE", byteArray)

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
            Dim response As HttpResponseMessage = client.GetAsync(customer_api & "?kode_customer=" & txtKode_customer.Text).Result

            ' Check if the request was successful
            If response.IsSuccessStatusCode Then
                ' Read the response content as a string
                Dim jsonString As String = response.Content.ReadAsStringAsync().Result
                Try
                    If (jsonString = "[]") Then
                        customer_baru = True
                        MessageBox.Show("Data Not Found")
                        Exit Sub
                    Else
                        customer_baru = False
                        Exit Sub
                    End If
                Catch ex As Exception

                Finally
                    If (customer_baru = False) Then
                        ' Deserialize the JSON into objects
                        Dim data As List(Of Customer) = JsonConvert.DeserializeObject(Of List(Of Customer))(jsonString)

                        ' Create textboxes dynamically and set their values
                        For Each mydata As Customer In data
                            txtKode_customer.Text = mydata.kode_customer
                            txtNama_customer.Text = mydata.nama_customer
                            txtAlamat.Text = mydata.alamat
                            txtEmail.Text = mydata.email
                        Next
                    End If

                End Try

            Else
                ' Request failed, handle the error
                MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}")

            End If
        End Using
    End Sub
    Private Sub GetClear()
        txtKode_customer.Clear()
        txtNama_customer.Clear()
        txtAlamat.Clear()
        txtEmail.Clear()
        Reloaded()
        txtKode_customer.Focus()
    End Sub
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If (customer_baru = True) Then
            TambahData()
        Else
            UpdateData()
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        GetClear()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If (customer_baru = False) Then
            Dim result As DialogResult = MessageBox.Show("Apakah data akan dihapus?", "Confirmation", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                DeleteData()
            Else
                MessageBox.Show("Data batal dihapus.")
            End If
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reloaded()
    End Sub

    Private Sub Reloaded()

        ' Create a WebClient instance to make the HTTP request
        Dim client As New WebClient()

        ' Make the GET request and retrieve the response
        Dim response As String = client.DownloadString(customer_api)

        ' Deserialize the JSON response into a list of objects
        Dim data As List(Of Customer) = JsonConvert.DeserializeObject(Of List(Of Customer))(response)

        ' Bind the data to the DataGridView
        dgvData.DataSource = data
    End Sub

    Private Sub txtKode_customer_KeyDown(sender As Object, e As KeyEventArgs) Handles txtKode_customer.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            GetData()
        End If
    End Sub

    Private Sub dgvData_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvData.CellContentClick

    End Sub

    Private Sub txtemail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub txtAlamat_TextChanged(sender As Object, e As EventArgs) Handles txtAlamat.TextChanged

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub txtNama_customer_TextChanged(sender As Object, e As EventArgs) Handles txtNama_customer.TextChanged

    End Sub

    Private Sub txtKode_customer_TextChanged(sender As Object, e As EventArgs) Handles txtKode_customer.TextChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)

    End Sub
End Class
