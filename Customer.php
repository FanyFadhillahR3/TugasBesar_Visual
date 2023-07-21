<?php
//Simpanlah dengan nama_customer file : Customer.php
require_once 'database.php';
class Customer 
{
    private $db;
    private $table = 'customer';
    public $kode_customer = "";
    public $nama_customer = "";
    public $alamat = "";
    public $email = "";
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
    public function get_by_kode(int $kode_customer)
    {
        $query = "SELECT * FROM $this->table WHERE kode_customer = $kode_customer";
        $result_set = $this->db->query($query);   
        return $result_set;
    }
    public function insert(): int
    {
        $query = "INSERT INTO $this->table (`kode_customer`,`nama_customer`,`alamat`,`email`) VALUES ('$this->kode_customer','$this->nama_customer','$this->alamat','$this->email')";
        $this->db->query($query);
        return $this->db->insert_id();
    }
    public function update(int $id): int
    {
        $query = "UPDATE $this->table SET kode_customer = '$this->kode_customer', nama_customer = '$this->nama_customer', alamat = '$this->alamat', email = '$this->email' 
        WHERE id = $id";
        $this->db->query($query);
        return $this->db->affected_rows();
    }
    public function update_by_kode($kode_customer): int
    {
        $query = "UPDATE $this->table SET kode_customer = '$this->kode_customer', nama_customer = '$this->nama_customer', alamat = '$this->alamat', email = '$this->email' 
        WHERE kode_customer  = $kode_customer";
        $this->db->query($query);
        return $this->db->affected_rows();
    }
    public function delete(int $id): int
    {
        $query = "DELETE FROM $this->table WHERE id = $id";
        $this->db->query($query);
        return $this->db->affected_rows();
    }
    public function delete_by_kode($kode_customer): int
    {
        $query = "DELETE FROM $this->table WHERE kode_customer = $kode_customer";
        $this->db->query($query);
        return $this->db->affected_rows();
    }
}
?>