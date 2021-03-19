<?php defined('BASEPATH') OR exit('No direct script access allowed');

class launcher extends CI_Controller
{
	function __construct()
	{
		parent::__construct();
		$this->load->library('logger');
		$this->load->library('launcher_lib');
		$this->load->library('config_sql');
		$this->load->helper('url');
	}

	public function index()
	{
	}

	public function launcher_rev()
	{
		$data['opcode'] = 550;
		$data['version'] = $this->config_sql->config_load('cl_version');
		$finalstring = json_encode($data);
		echo $finalstring;
	}
	
	public function getGameList()
	{
		echo $this->launcher_lib->getGameList();
	}

	public function login($login, $password, $version)
	{
		$Json_pre['opcode'] = 2020;
		if($this->launcher_lib->auth($login, $password, $version) == 1)
		{
			$text = "MMo-Network заметил, что вы вошли в лаунчер с версией $version.";
			$this->logger->writter($login, $text);
			$Json_pre['status_code'] = 80000;
		}
		else
		{
			$Json_pre['status_code'] = 80001;
		}

		$finalstring = json_encode($Json_pre);
		echo $finalstring;
	}

	public function inMaintenance()
	{
		$Json_pre['opcode'] = 2030;
		$Json_pre['Maintenance'] = 'false';
		$finalstring = json_encode($Json_pre);
		echo $finalstring;
	}

	public function news($gameid)
	{
		if($gameid > 0 && $gameid < 255)
		{
			echo $this->launcher_lib->News($gameid);
		}
		else
		{
			echo $this->launcher_lib->News($gameid);
		}
	}

	public function logout($login)
	{
		$Json_pre['opcode'] = 2022;

		if($login != null && $login != "")
		{
			$ipaddress_log = $_SERVER["REMOTE_ADDR"];
			$this->logger->writter($login, "MMo-Network заметил, что вы вышли из игрового лаунчера.");
			$Json_pre['status_code'] = 80000;
		}
		else
		{
			$Json_pre['status_code'] = 80001;
		}
		$finalstring = json_encode($Json_pre);
		return $finalstring;
	}

	public function request_start_game($gameid, $login)
	{
		if($gameid > 0 && $login != null)
		{
			$data['opcode'] = 500;
			$data['status_code'] = 80000;
			$this->logger->writter($login, "MMo-Network заметил, что был выполнен запрос на запуск игры с номером " . $gameid . ".");
			$finalstring = json_encode($data);
		}
		else
		{
			$data['opcode'] = 500;
			$data['status_code'] = 80001;
		}
		echo $finalstring;
	}
}
?>