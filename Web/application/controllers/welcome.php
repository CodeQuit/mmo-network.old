<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Welcome extends CI_Controller {

	function __construct()
	{
		parent::__construct();
		$this->load->library('config_sql');
	}

	public function index()
	{
		if($this->config_sql->config_load('maintenance') == "false")
		{
			$data['version'] = $this->config_sql->config_load('version');
			$this->load->view('welcome_message', $data);
		}
		else
		{
			echo $this->config_sql->config_load('maintenance_text');
		}
	}
}

/* End of file welcome.php */
/* Location: ./application/controllers/welcome.php */