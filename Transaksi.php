<?php
//Simpanlah dengan nama file : Barang.php
require_once 'database.php';
class Transaksi
{
    private $db;
    private $table = 'transaksi';
    public $kode_transaksi = "";
    public $kode_barang = "";
    public $kode_customer = "";
    public $harga = "" ;

    public function __construct(MySQLDatabase $db)
    {
        $this->db = $db;
    }
    public function get_all() 
    {
        $query = "SELECT * FROM $this->table";
        $result_set = $this->db->query($query);
        return $result_set;
    }
    public function get_by_id(int $id)
    {
        $query = "SELECT * FROM $this->table WHERE id = $id";
        $result_set = $this->db->query($query);   
        return $result_set;
    }
    public function get_by_kode(int $kode_transaksi)
    {
        $query = "SELECT * FROM $this->table WHERE kode_transaksi = $kode_transaksi";
        $result_set = $this->db->query($query);   
        return $result_set;
    }
    public function insert(): int
    {
        $query = "INSERT INTO $this->table (`kode_transaksi`, `kode_barang`, `kode_customer`, `harga`) VALUES ('$this->kode_transaksi','$this->kode_barang','$this->kode_customer','$this->harga')";
        $this->db->query($query);
        return $this->db->insert_id();
    }
    public function update(int $id): int
    {
        $query = "UPDATE $this->table SET kode_transaksi = '$this->kode_transaksi', kode_barang = '$this->kode_barang', kode_customer = '$this->kode_customer', harga = '$this->harga'
        WHERE id = $id";
        $this->db->query($query);
        return $this->db->affected_rows();
    }
    public function update_by_kode($kode_transaksi): int
    {
        $query = "UPDATE $this->table SET kode_transaksi = '$this->kode_transaksi', kode_barang = '$this->kode_barang', kode_customer = '$this->kode_customer', harga = '$this->harga'
        WHERE kode_transaksi  = $kode_transaksi";
        $this->db->query($query);
        return $this->db->affected_rows();
    }
    public function delete(int $id): int
    {
        $query = "DELETE FROM $this->table WHERE id = $id";
        $this->db->query($query);
        return $this->db->affected_rows();
    }
    public function delete_by_kode($kode_transaksi): int
    {
        $query = "DELETE FROM $this->table WHERE kode_transaksi = $kode_transaksi";
        $this->db->query($query);
        return $this->db->affected_rows();
    }
}
?>