<?php
require_once 'database.php';
require_once 'Barang.php';
$db = new MySQLDatabase();
$barang = new Barang($db);
$id=0;
$kode_barang=0;
// Check the HTTP request method
$method = $_SERVER['REQUEST_METHOD'];
// Handle the different HTTP methods
switch ($method) {
    case 'GET':
        if(isset($_GET['id'])){
            $id = $_GET['id'];
        }
        if(isset($_GET['kode_barang'])){
            $kode_barang = $_GET['kode_barang'];
        }
        if($id>0){    
            $result = $barang->get_by_id($id);
        }elseif($kode_barang>0){
            $result = $barang->get_by_kode($kode_barang);
        } else {
            $result = $barang->get_all();
        }        
       
        $val = array();
        while ($row = $result->fetch_assoc()) {
            $val[] = $row;
        }
        
        header('Content-Type: application/json');
        echo json_encode($val);
        break;
    
    case 'POST':
        // Add a new barang
        $barang->kode_barang = $_POST['kode_barang'];
        $barang->nama = $_POST['nama'];
        $barang->berat_barang = $_POST['berat_barang'];
       
        $barang->insert();
        $a = $db->affected_rows();
        if($a>0){
            $data['status']='success';
            $data['message']='Data Barang created successfully.';
        } else {
            $data['status']='failed';
            $data['message']='Data Barang not created.';
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
        if(isset($_GET['kode_barang'])){
            $kode_barang = $_GET['kode_barang'];
        }
        parse_str(file_get_contents("php://input"), $_PUT);
        $barang->kode_barang = $_PUT['kode_barang'];
        $barang->nama = $_PUT['nama'];
        $barang->berat_barang = $_PUT['berat_barang'];
        if($id>0){    
            $barang->update($id);
        }elseif($kode_barang<>""){
            $barang->update_by_kode($kode_barang);
        } else {
            
        } 
        
        $a = $db->affected_rows();
        if($a>0){
            $data['status']='success';
            $data['message']='Data Barang updated successfully.';
        } else {
            $data['status']='failed';
            $data['message']='Data Barang update failed.';
        }        
        header('Content-Type: application/json');
        echo json_encode($data);
        break;
    case 'DELETE':
        // Delete a user
        if(isset($_GET['id'])){
            $id = $_GET['id'];
        }
        if(isset($_GET['kode_barang'])){
            $kode_barang = $_GET['kode_barang'];
        }
        if($id>0){    
            $barang->delete($id);
        }elseif($kode_barang>0){
            $barang->delete_by_kode($kode_barang);
        } else {
            
        } 
        
        $a = $db->affected_rows();
        if($a>0){
            $data['status']='success';
            $data['message']='Data Barang deleted successfully.';
        } else {
            $data['status']='failed';
            $data['message']='Data Barang delete failed.';
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