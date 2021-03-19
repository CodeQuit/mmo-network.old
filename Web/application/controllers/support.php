<?php defined('BASEPATH') OR exit('No direct script access allowed');

class support extends CI_Controller
{

	function __construct()
	{
		parent::__construct();
		$this->load->library('logger');
		$this->load->library('support_library');
		$this->load->library('services');
		$this->load->library('config_sql');
		$this->load->library('ion_auth');
		$this->load->library('session');
		$this->load->library('form_validation');
		$this->load->database();
		$this->load->helper('url');
	}
	
	public function index()
	{
		$data['version'] = $this->config_sql->config_load('version');
		$data_info = $this->ion_auth->user()->row();
		$username = $data_info->username;
		$data['question'] = $this->support_library->getQuestion($username);
		if($this->config_sql->config_load('maintenance') == "false")
		{
			if($this->ion_auth->logged_in())
			{
				$this->load->view('body', $data);
				$this->load->view('/support/support_main', $data);
				$this->load->view('footer', $data);
			}
		}
	}
	
	public function addQuestion()
	{
		$data['version'] = $this->config_sql->config_load('version');
		if($this->config_sql->config_load('maintenance') == "false")
		{
			if($this->ion_auth->logged_in())
			{
				
				$this->load->view('body', $data);
				$this->load->view('/support/question_add', $this->services->getAllName());
				$this->load->view('footer', $data);
			}
		}
	}
	
	//$question, $account, $status
	public function questionAdded()
	{
		$data['version'] = $this->config_sql->config_load('version');
		if($this->config_sql->config_load('maintenance') == "false")
		{
			if($this->ion_auth->logged_in())
			{
				$question = $this->input->post('question');
				$username = $this->ion_auth->user()->row()->username;
				$serviceid = $this->input->post('services') + 1;
				//echo $serviceid;
				$this->support_library->addQuestion($question, $username, "Новый", $seviceid);
				echo 'Saving....';
				redirect('support', 'refresh');
			}
		}
	}
}