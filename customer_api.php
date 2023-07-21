<?php
require_once 'database.php';
require_once 'Customer.php';
$db = new MySQLDatabase();
$customer = new Customer($db);
$id=0;
$kode_customer=0;
// Check the HTTP request method
$method = $_SERVER['REQUEST_METHOD'];
// Handle the different HTTP methods
switch ($method) {
    case 'GET':
        if(isset($_GET['id'])){
            $id = $_GET['id'];
        }
        if(isset($_GET['kode_customer'])){
            $kode_customer = $_GET['kode_customer'];
        }
        if($id>0){    
            $result = $customer->get_by_id($id);
        }elseif($kode_customer>0){
            $result = $customer->get_by_kode($kode_customer);
        } else {
            $result = $customer->get_all();
        }        
       
        $val = array();
        while ($row = $result->fetch_assoc()) {
            $val[] = $row;
        }
        
        header('Content-Type: application/json');
        echo json_encode($val);
        break;
    
    case 'POST':
        // Add a new customer
        $customer->kode_customer = $_POST['kode_customer'];
        $customer->nama_customer = $_POST['nama_customer'];
        $customer->alamat = $_POST['alamat'];
        $customer->email = $_POST['email'];
       
        $customer->insert();
        $a = $db->affected_rows();
        if($a>0){
            $data['status']='success';
            $data['message']='Data Customer created successfully.';
        } else {
            $data['status']='failed';
            $data['message']='Data Customer not created.';
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
        if(isset($_GET['kode_customer'])){
            $kode_customer = $_GET['kode_customer'];
        }
        parse_str(file_get_contents("php://input"), $_PUT);
        $customer->kode_customer = $_PUT['kode_customer'];
        $customer->nama_customer = $_PUT['nama_customer'];
        $customer->alamat = $_PUT['alamat'];
        $customer->email = $_PUT['email'];
        if($id>0){    
            $customer->update($id);
        }elseif($kode_customer<>""){
            $customer->update_by_kode($kode_customer);
        } else {
            
        } 
        
        $a = $db->affected_rows();
        if($a>0){
            $data['status']='success';
            $data['message']='Data Customer updated successfully.';
        } else {
            $data['status']='failed';
            $data['message']='Data Customer update failed.';
        }        
        header('Content-Type: application/json');
        echo json_encode($data);
        break;
    case 'DELETE':
        // Delete a user
        if(isset($_GET['id'])){
            $id = $_GET['id'];
        }
        if(isset($_GET['kode_customer'])){
            $kode_customer = $_GET['kode_customer'];
        }
        if($id>0){    
            $customer->delete($id);
        }elseif($kode_customer>0){
            $customer->delete_by_kode($kode_customer);
        } else {
            
        } 
        
        $a = $db->affected_rows();
        if($a>0){
            $data['status']='success';
            $data['message']='Data Customer deleted successfully.';
        } else {
            $data['status']='failed';
            $data['message']='Data Customer delete failed.';
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