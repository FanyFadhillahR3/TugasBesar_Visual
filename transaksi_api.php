<?php

require_once 'database.php';
require_once 'Transaksi.php';

$db = new MySQLDatabase();
$transaksi = new Transaksi($db);

$id=0;
$kode_transaksi=0;

// Check the HTTP request method
$method = $_SERVER['REQUEST_METHOD'];
// Handle the different HTTP methods
switch ($method) {
    case 'GET':
        if(isset($_GET['id'])){
            $id = $_GET['id'];
        }
        if(isset($_GET['kode_transaksi'])){
            $kode_transaksi = $_GET['kode_transaksi'];
        }
        if($id>0){    
            $result = $transaksi->get_by_id($id);
        }elseif($kode_transaksi>0){
            $result = $transaksi->get_by_kode($kode_transaksi);
        } else {
            $result = $transaksi->get_all();
        }        
       
        $val = array();
        while ($row = $result->fetch_assoc()) {
            $val[] = $row;
        }
        
        header('Content-Type: application/json');
        echo json_encode($val);
        break;
    
    case 'POST':
        // Add a new transaksi
        $transaksi->kode_transaksi = $_POST['kode_transaksi'];
        $transaksi->kode_barang = $_POST['kode_barang'];
        $transaksi->kode_customer = $_POST['kode_customer'];
        $transaksi->harga = $_POST['harga'];
       
        $transaksi->insert();
        $a = $db->affected_rows();
        if($a>0){
            $data['status']='success';
            $data['message']='Data Transaksi created successfully.';
        } else {
            $data['status']='failed';
            $data['message']='Data Transaksi not created.';
        }
        header('Content-Type: application/json');
        echo json_encode($data);
        break;
        
    case 'PUT':
        // Update an existing data
        $_PUT = [];
        if(isset($_GET['id'])){
            $id = $_GET['id'];
        }
        if(isset($_GET['kode_transaksi'])){
            $kode_transaksi = $_GET['kode_transaksi'];
        }
        parse_str(file_get_contents("php://input"), $_PUT);
        
        $transaksi->kode_transaksi = $_PUT['kode_transaksi'];
        $transaksi->kode_barang = $_PUT['kode_barang'];
        $transaksi->kode_customer = $_PUT['kode_customer'];
        $transaksi->harga = $_PUT['harga'];

        if($id>0){    
            $transaksi->update($id);
        }elseif($kode_transaksi<>""){
            $transaksi->update_by_kode($kode_transaksi);
        } else {
            
        } 
        
        $a = $db->affected_rows();
        if($a>0){
            $data['status']='success';
            $data['message']='Data Transaksi updated successfully.';
        } else {
            $data['status']='failed';
            $data['message']='Data Transaksi update failed.';
        }        
        header('Content-Type: application/json');
        echo json_encode($data);
        break;

    case 'DELETE':
        // Delete a user
        if(isset($_GET['id'])){
            $id = $_GET['id'];
        }
        if(isset($_GET['kode_transaksi'])){
            $kode_transaksi = $_GET['kode_transaksi'];
        }
        if($id>0){    
            $transaksi->delete($id);
        }elseif($kode_transaksi>0){
            $transaksi->delete_by_kode($kode_transaksi);
        } else {
            
        } 
        
        $a = $db->affected_rows();
        if($a>0){
            $data['status']='success';
            $data['message']='Data Transaksi deleted successfully.';
        } else {
            $data['status']='failed';
            $data['message']='Data Transaksi delete failed.';
        }        
        header('Content-Type: application/json');
        echo json_encode($data);
        break;
    default:
        header("HTTP/1.0 405 Method Not Allowed");
        break;
    }
$db->close()
?>