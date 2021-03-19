<?php  if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class game_model extends CI_Model
{
	public function __construct()
	{
		parent::__construct();
		$this->load->database();
		$this->load->helper('date');
	}

	public function game_list_model($login)
	{
		$game_list = "";
		$query = $this->db->query("SELECT id, name, exename, status, onRegister FROM game_list;");
		foreach ($query->result() as $row)
		{
			$game_list=$game_list . "<li class='nav-header'>$row->name</li>";
			if($row->onRegister <= 0)
			{
				$game_list=$game_list . "Сервис не доступен.";
			}
			else
			{

				$count = $this->db->query("SELECT * FROM services_account WHERE login='$login' AND gameid=$row->id;");
				if($count->num_rows() > 0)
				{
					$game_list=$game_list . "Учетная запись уже существует! ";
				}
				else
				{
					$game_list=$game_list . "<li><a href=/panel/ServiceCreate/$row->id>Регистрация</a></li>";
				}
			}
		}

		return $game_list;
	}

	public function create_account_model($login,$password,$gameid,$status)
	{
		$result = $this->db->query("SELECT * FROM services_account WHERE '$login';");
		if($result->num_rows() > 0)
		{
			return false;
		}
		else
		{
			$query = $this->db->query("INSERT INTO services_account (login, password, gameid, status) VALUES ('$login', '$password', '$gameid', '$status');");
			return true;
		}
	}

	public function status_server_model()
	{
		$this->load->database();
		$query = $this->db->query("SELECT host, port, name, type, active FROM server_status ORDER BY name ASC;" );
		foreach ($query->result() as $row)
		{
			if($row->active > 0)
			{
				$STATUS = @fsockopen($row->host, $row->port, $errno, $errstr, 1);
				$STATUSD1= $STATUS ? "<small>$row->name: <font color=00ff00>Работает</font><br></small>" : "<small>$row->name: <font color=ff0000>Отключен</font><br></small>";
				$status_final = $status_final . $STATUSD1;
			}
		}

		return $status_final;
	}
}