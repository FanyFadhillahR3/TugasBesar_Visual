Module MyMod
    Public api_folder As String = "uasvisual"
    Public barang_api As String = "http://f0843766.xsph.ru/" & api_folder & "/barang_api.php"
    Public barang_baru As Boolean
    Public customer_api As String = "http://f0843766.xsph.ru/" & api_folder & "/customer_api.php"
    Public customer_baru As Boolean
    Public transaksi_api As String = "http://f0843766.xsph.ru/" & api_folder & "/transaksi_api.php"
    Public transaksi_baru As Boolean
    Public users_api As String = "http://f0843766.xsph.ru/" & api_folder & "/users_api.php"
    Public admin_role As Boolean = False
    Public pelanggan_role As Boolean = False
    Public login_valid As Boolean = False
    Public LoginForm As New Form_login
    Public Dashboard As New Dashboard
    Public CustomerForm As New Form_customer
    Public BarangForm As New Form_barang
    Public TransaksiForm As New Form_transaksi

End Module
