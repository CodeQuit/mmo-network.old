<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Launch extends CI_Controller
{

	function __construct()
	{
		parent::__construct();
		$this->load->library('logger');
		$this->load->library('ion_auth');
		$this->load->library('session');
		$this->load->library('form_validation');
		$this->load->database();
		$this->load->helper('url');
	}

	public function index()
	{
		$mmo_server_revision = $this->config->item('version');
		echo 'revision ' . $mmo_server_revision;
	}

	public function launcher()
	{
		$this->load->view('/launcher/launcher_body', $data);
	}

	public function launcher_rev()
	{
		echo $this->config->item('version_client');
	}

	public function server()
	{
		$maint = $this->config->item('maintenance');
		if($maint == true)
		{
			echo "offline";
		}
		else
		{
			echo "online";
		}
	}

	public function ru($login, $password)
	{
		if ($this->ion_auth->login($login, $password, false))
		{
			$this->session->set_flashdata('message', $this->ion_auth->messages());
			$Data_new = $this->ion_auth->user()->row();
			$login_log = $Data_new->username;
			$email_log = $Data_new->email;
			$ipaddress_log = $_SERVER["REMOTE_ADDR"];
			$this->logger->writter($login, "Вход успешно выполнен пользователем с ip адресом $ipaddress_log в игровой лаунчер с версией 2.х.х.х.");
			echo "OK";
		}
		else
		{
			echo "NO";
		}

	}

	public function logout($username)
	{
		$Data_new = $this->ion_auth->user()->row();
		$login_log = $Data_new->username;
		$email_log = $Data_new->email;
		$ipaddress_log = $_SERVER["REMOTE_ADDR"];
		$this->logger->writter($login, "Выход успешно выполнен пользователем с ip адресом $ipaddress_log из игрового лаунчера.");
		echo "OK";
	}

	public function glist()
	{
		echo "0 0 1 0 offline offline online offline";
	}

	public function game_list()
	{
	  echo "1 PointBlank";
	}

	public function game_mmonetwork($gameid)
	{
		echo "PointBlank, Lineage2, AION, World of Warcraft";
	}

	public function gamestartid($gameid)
	{
		if($gameid > 0 && $gameid < 5)
		{
			echo 'game_ready';
		}
		else
		{
			echo 'ERR#102';
		}
	}
}
